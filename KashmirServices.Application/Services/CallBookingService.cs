using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Services;

internal sealed class CallBookingService : ICallBookingService
{
    private readonly ICallBookingRepository repository;
    private readonly IBaseRepository<AssignedEngineer> baseRepository;
    private readonly IAddressRepository addressRepository;
    private readonly IMapper mapper;
    private readonly IContextService context;

    public CallBookingService(
        ICallBookingRepository repository,
        IBaseRepository<AssignedEngineer> baseRepository,
        IAddressRepository addressRepository,

        IMapper mapper,
        IContextService context)
    {
        this.repository = repository;
        this.baseRepository = baseRepository;
        this.addressRepository = addressRepository;
        this.mapper = mapper;
        this.context = context;
    }
    public async Task<APIResponse<int>> Add(CallBookingRequest model)
    {
        var callBooking = mapper.Map<CallBooking>(model);
      
        callBooking.CustomerId = context.GetUserId().Value;
        callBooking.CreatedBy = context.GetUserId();
        callBooking.CallBookingStatus = CallBookingStatus.Pending;
        callBooking.RequestDate = DateTimeOffset.Now;
        var dbJobNo = await repository.GetJobNo();

        if (dbJobNo is null)
            callBooking.JobNo = "1000";
        else
        {
            long val =  Convert.ToInt64(dbJobNo)+1;
            callBooking.JobNo = val.ToString();
        }

        var isRegistered = await repository.IsExist(x => x.CustomerId == callBooking.CustomerId && x.AddressId == model.AddressId
                                 && x.ServiceTypeId == model.ServiceTypeId
                                 && x.CallBookingStatus != CallBookingStatus.Completed);

        if (isRegistered)
            return APIResponse<int>.ErrorResponse("Call is already registered", APIStatusCodes.Conflict);

        int insertResult = await repository.InsertAsync(callBooking);
        if (insertResult > 0) return APIResponse<int>.SuccessResponse(insertResult);

        return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError);
    }


    public async Task<APIResponse<IEnumerable<DetailedCallBookingResponse>>> GetAllBookings()
    {
        var bookingResponse = await repository.GetAllBookings();

        if (bookingResponse is null)
            return APIResponse<IEnumerable<DetailedCallBookingResponse>>.ErrorResponse("No Booking Found", APIStatusCodes.NotFound);

        return APIResponse<IEnumerable<DetailedCallBookingResponse>>.SuccessResponse(bookingResponse);
    }


    public async Task<APIResponse<IEnumerable<CustomerBookingResponse>>> GetMyBookings()
    {
        //var irfan = "2D354E4F-22D0-48F9-DB09-08DBD787320A";
        var abid = "94DC4290-ED56-4204-DB0A-08DBD787320A";
        var customerId = context.GetUserId() ?? Guid.Parse(abid);
        var bookingResponse = await repository.GetMyBookings(customerId);

        if (bookingResponse is null)
            return APIResponse<IEnumerable<CustomerBookingResponse>>.ErrorResponse("No Booking Found", APIStatusCodes.NotFound);

        return APIResponse<IEnumerable<CustomerBookingResponse>>.SuccessResponse(bookingResponse);
    }


    public async Task<APIResponse<int>> Add(CallBookingWithAddressRequest model)
    {
        Guid addressId = Guid.NewGuid();

        var address = mapper.Map<Address>(model);
        address.Id = addressId;

        var insertResult = await addressRepository.InsertAsync(address);

        if (insertResult > 0)
        {
            var productSolutionQuery = new CallBooking
            {
                Id = Guid.NewGuid(),
                AddressId = addressId,
                ServiceTypeId = model.ProductSolutionId,
                CustomerId = context.GetUserId() ?? Guid.Parse("FDBF212B-ADA6-40A9-B7DE-08DBBAB748DE"), //Use This After Login
                //CreatedBy = context.GetUserId(), //Use This After Login
            };

            var insertProductSolutionQueryResult = await repository.InsertAsync(productSolutionQuery);

            if (insertProductSolutionQueryResult > 0) return APIResponse<int>.SuccessResponse(insertProductSolutionQueryResult);

            return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
        }
        else
        {
            return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
        }
    }

    public async Task<APIResponse<int>> CancelBooking(Guid id)
    {
        var booking= await repository.GetByIdAsync(id);
        if(booking is null)
            return APIResponse<int>.ErrorResponse("no booking found", APIStatusCodes.NotFound);

        if (booking!.CallBookingStatus == CallBookingStatus.Assigned || booking.CallBookingStatus == CallBookingStatus.Completed)
            return APIResponse<int>.ErrorResponse("Booking cannot be cancelled", APIStatusCodes.BadRequest);

        booking.CallBookingStatus = CallBookingStatus.Cancelled;
        int returnValue= await repository.UpdateAsync(booking);
        if(returnValue > 0)
            return APIResponse<int>.SuccessResponse(1,"Booking cancelled successfully");

        return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<int>> BookingReminder(Guid id)
    {
        var booking = await repository.GetByIdAsync(id);

        if (booking is null)
            return APIResponse<int>.ErrorResponse("no booking found", APIStatusCodes.NotFound);

      if(booking.CallBookingStatus == CallBookingStatus.Cancelled)
            return APIResponse<int>.ErrorResponse("You have cancelled the booking", APIStatusCodes.BadRequest);


        if (booking.CallBookingStatus == CallBookingStatus.Completed)
            return APIResponse<int>.ErrorResponse("booking is already completed", APIStatusCodes.BadRequest);
        var assignedEngineer = await baseRepository.FirstOrDefaultAsync(x=>x.CallBookingId == booking.Id);

        if (DateTimeOffset.Now <= assignedEngineer?.ExpectedDate.Value.AddHours(1))
            return APIResponse<int>.ErrorResponse($"You can add reminder after {assignedEngineer?.ExpectedDate.Value.Hour - DateTimeOffset.Now.Hour} Hours ", APIStatusCodes.BadRequest);

        booking.Reminder += 1;
        int returnValue = await repository.UpdateAsync(booking);
        if (returnValue > 0)
            return APIResponse<int>.SuccessResponse(1, "Reminder added successfully");

        return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<string>> ReScheduleCallBookingByCustomer(CustomerReScheduleCallBookingRequest model)
    {
        var assignedEngineer = await baseRepository.GetByIdAsync(model.AssignedEngineerId);
        if (assignedEngineer is null)
            return APIResponse<string>.ErrorResponse("No Booking found", APIStatusCodes.NotFound);

        assignedEngineer.ExpectedDate = model.ExpectedDate;
        assignedEngineer.Remarks = model.Remarks;


        int returnValue = await baseRepository.UpdateAsync(assignedEngineer);

        if (returnValue > 0)
        {
            return APIResponse<string>.SuccessResponse("Call Rescheduled successfully");
        }
        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }
}

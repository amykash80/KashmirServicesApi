using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IServices;

public interface ICallBookingService
{
    Task<APIResponse<int>> Add(CallBookingRequest model);


    Task<APIResponse<int>> CancelBooking(Guid id);

    Task<APIResponse<int>> BookingReminder(Guid id);


    Task<APIResponse<int>> Add(CallBookingWithAddressRequest model);

    Task<APIResponse<IEnumerable<DetailedCallBookingResponse>>> GetAllBookings();


    Task<APIResponse<IEnumerable<CustomerBookingResponse>>> GetMyBookings();

    Task<APIResponse<string>> ReScheduleCallBookingByCustomer(CustomerReScheduleCallBookingRequest model);


}

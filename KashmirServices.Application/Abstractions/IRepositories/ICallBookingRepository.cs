using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IRepositories;

public interface ICallBookingRepository : IBaseRepository<CallBooking>
{
    Task<IEnumerable<CustomerBookingResponse>> GetMyBookings(Guid customerId);

    Task<IEnumerable<DetailedCallBookingResponse>> GetAllBookings();

    Task<IEnumerable<DetailedCallBookingResponse>> GetManagerBookings(Guid managerId);

    Task<DetailedCallBookingResponse> GetCallBookingById(Guid id);

    Task<int> UpdateCallBookingStatus(UpdateCallBookingStatusRequest model);

    Task<string> GetJobNo();
}

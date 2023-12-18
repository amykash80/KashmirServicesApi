using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace KashmirServices.Persistence.Repositories;

internal sealed class CallBookingRepository : BaseRepository<CallBooking>, ICallBookingRepository
{
    private readonly KashmirServicesDbContext context;

    public CallBookingRepository(KashmirServicesDbContext context) : base(context)
    {
        this.context = context;
    }
    private readonly string adminQuery= $@"SELECT CB.Id, C.Id as CategoryId, C.[Name] AS CategoryName, C.ManagerId, 
	                                            B.Id AS BrandId, B.[Name] AS BrandName, S.[Name] AS ServiceTypeName,
	                                            CB.ServiceTypeId, CB.JobNo, CB.CustomerId, CB.AddressId, CB.ModelNo, CB.SerialNo, CB.[Description],
	                                            CB.RequestDate, CB.CallBookingStatus, CB.Reminder, CB.SatisficationCode, CB.UnSatisficationCode, CB.Remarks

	                                            FROM Categories C
		                                        INNER JOIN Brands B
		                                        ON C.Id =  B.CategoryId
		                                        INNER JOIN ServiceTypes S
		                                        ON B.Id = S.BrandId
		                                        INNER JOIN CallBookings CB
		                                        ON S.Id = CB.ServiceTypeId ";



    public async Task<IEnumerable<CustomerBookingResponse>> GetMyBookings(Guid customerId)
    {
        string customerQuery = $@"SELECT CB.Id, C.Id as CategoryId, C.[Name] AS CategoryName, C.ManagerId, 
	                                        B.Id AS BrandId, B.[Name] AS BrandName, S.[Name] AS ServiceTypeName,
	                                        CB.ServiceTypeId, CB.JobNo, CB.CustomerId, CB.AddressId, CB.ModelNo, CB.SerialNo, CB.[Description],
	                                        CB.RequestDate, CB.CallBookingStatus, CB.Reminder,CB.Remarks

	                                        FROM Categories C
		                                    INNER JOIN Brands B
		                                    ON C.Id =  B.CategoryId
		                                    INNER JOIN ServiceTypes S
		                                    ON B.Id = S.BrandId
		                                    INNER JOIN CallBookings CB
		                                    ON S.Id = CB.ServiceTypeId  WHERE CB.CustomerId=@customerId";
        return await QueryAsync<CustomerBookingResponse>(customerQuery , new { customerId });
    }


    public async Task<IEnumerable<DetailedCallBookingResponse>> GetAllBookings()
    {
        return await QueryAsync<DetailedCallBookingResponse>(adminQuery);
    }


    public async Task<IEnumerable<DetailedCallBookingResponse>> GetManagerBookings(Guid managerId)
    {
        return await QueryAsync<DetailedCallBookingResponse>(adminQuery+ " WHERE C.ManagerId=@managerId", new { managerId });
    }

    public async Task<DetailedCallBookingResponse> GetCallBookingById(Guid id)
    {
        return await FirstOrDefaultAsync<DetailedCallBookingResponse>(adminQuery + " WHERE CB.Id=@id", new { id });
    }

    public async Task<int> UpdateCallBookingStatus(UpdateCallBookingStatusRequest model)
    {
        return await ExecuteAsync<int>($"UPDATE CallBookings SET Remarks=@remarks, CallBookingStatus={(int)model.CallBookingStatus} WHERE Id=@id", new { remarks = model.Remarks, id= model.Id });
    }


    public async Task<string> GetJobNo()
    {
        return await FirstOrDefaultAsync<string>(@"select top 1 JobNo from CallBookings order by JobNo  desc");
    }


}

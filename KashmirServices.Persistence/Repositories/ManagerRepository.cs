using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace KashmirServices.Persistence.Repositories;

internal sealed class ManagerRepository : BaseRepository<User>, IManagerRepository
{
    private readonly KashmirServicesDbContext context;

    public ManagerRepository(KashmirServicesDbContext context) : base(context)
    {
        this.context = context;
    }

   private readonly string baseQuery = $@"SELECT A.Id AS AssingedEngineerId, A.CallBookingId, C.JobNo, A.EngineerId, Eng.FullName AS EngineerName,
                            A.CallStatus, A.AssignmentDate, A.ExpectedDate,
                            A.Remarks AS EngineerRemarks, C.Reminder, C.CustomerId, C.CreatedOn
							C.CallBookingStatus, C.SatisficationCode, C.UnSatisficationCode,C.Remarks AS CallRemarks,
                            (CASE WHEN C.Reminder > 1 THEN 'Priority' ELSE 'Normal' END) AS CallPriority,
							C.ModelNo, C.SerialNo , C.[Description] AS CallDescription,
							Cust.FullName AS CustomerName, Cust.Email, Cust.PhoneNumber,
                            C.AddressId, Ad.City , Ad.Street , Ad.PostalCode  ,Ad.AddressLine, B.[Name] AS BrandName, 
                            ST.[Name] AS ServiceType

                            FROM AssignedEngineers A
                            INNER JOIN Users Eng
                            ON Eng.Id = A.EngineerId
                            INNER JOIN CallBookings C
                            ON A.CallBookingId = C.Id
                            INNER JOIN Users Cust
                            ON  Cust.Id = C.CustomerId
                            JOIN Addresses Ad
                            ON Ad.Id = C.AddressId
                            INNER JOIN ServiceTypes ST
                            ON ST.Id = C.ServiceTypeId
                            INNER JOIN Brands B
                            ON B.Id = ST.BrandId
							INNER JOIN Categories Cat
							ON Cat.Id = B.CategoryId  ";

    public async Task<DetailedJobSheet> GetJobSheetByJobNo(string jobNo)
    {
       
        return await FirstOrDefaultAsync<DetailedJobSheet>(baseQuery + $@" WHERE C.JobNo =@jobNo ", new { jobNo });
    }

    public async Task<IEnumerable<DetailedJobSheet>> GetMyEngineersJobSheet(Guid managerId)
    {
       
        return await QueryAsync<DetailedJobSheet>(baseQuery +$@" WHERE Cat.ManagerId =@managerId
							ORDER BY C.CreatedOn DESC ", new { managerId });
    }

    public async Task<IEnumerable<DetailedJobSheet>> GetJobSheetByCallStatus(Guid managerId, CallStatus CallStatus)
    {
        return await QueryAsync<DetailedJobSheet>(baseQuery + $@"  WHERE Cat.ManagerId =@managerId AND  A.CallStatus={(int)CallStatus} ORDER BY C.CreatedOn DESC", new { managerId });
    }

    public async Task<IEnumerable<DetailedJobSheet>> GetEngineersJobSheetByCallStatus(Guid managerId, Guid engineerId, CallStatus CallStatus)
    {
        return await QueryAsync<DetailedJobSheet>(baseQuery + $@"  WHERE Cat.ManagerId =@managerId AND Eng.Id = @engineerId AND A.CallStatus={(int)CallStatus} ORDER BY C.CreatedOn DESC", new { managerId,engineerId });
    }
}

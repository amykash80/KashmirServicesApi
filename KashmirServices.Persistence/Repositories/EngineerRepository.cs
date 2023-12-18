using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace KashmirServices.Persistence.Repositories;

internal sealed class EngineerRepository : BaseRepository<User>, IEngineerRepository
{
    private readonly KashmirServicesDbContext context;

    public EngineerRepository(KashmirServicesDbContext context) : base(context)
    {
        this.context = context;
    }


   private readonly string baseQuery = $@"SELECT A.Id AS AssingedEngineerId, A.CallBookingId, C.JobNo, A.EngineerId, Eng.FullName AS EngineerName,
                            A.CallStatus, A.AssignmentDate, A.ExpectedDate, A.Remarks AS EngineerRemarks, C.Reminder, 
                            (CASE WHEN C.Reminder > 1 THEN 'Priority' ELSE 'Normal' END) AS CallPriority,
                            C.CustomerId, Cust.FullName AS CustomerName, Cust.Email, Cust.PhoneNumber,
                            C.AddressId, Ad.City , Ad.Street , Ad.PostalCode  ,Ad.AddressLine, B.[Name] AS BrandName, 
                            ST.[Name] AS ServiceType, C.ModelNo, C.SerialNo , C.[Description] AS CallDescription, C.CreatedOn

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
                            ON B.Id = ST.BrandId  ";

    //
    public async Task<JobSheet> GetJobSheetByJobNo(Guid engineerId,  string jobNo)
    {
        return await FirstOrDefaultAsync<JobSheet>(baseQuery + $@"  WHERE Eng.Id= @engineerId AND C.JobNo =@jobNo ", new { engineerId, jobNo });
    }

    public async Task<IEnumerable<JobSheet>> GetJobSheetByCallStatus(Guid engineerId, CallStatus CallStatus)
    {
        return await QueryAsync<JobSheet>(baseQuery + $@"  WHERE Eng.Id= @engineerId AND  A.CallStatus={(int)CallStatus} ORDER BY C.CreatedOn DESC", new { engineerId });
    }


    public async Task<IEnumerable<JobSheet>> GetMyJobSheet(Guid engineerId)
    {
       
        return await QueryAsync<JobSheet>(baseQuery + $@"  WHERE Eng.Id= @engineerId ORDER BY C.CreatedOn DESC  ", new { engineerId });
    }
}

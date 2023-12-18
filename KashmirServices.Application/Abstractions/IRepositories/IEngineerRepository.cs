using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IRepositories;

public interface IEngineerRepository : IBaseRepository<User>
{
    public Task<IEnumerable<JobSheet>> GetMyJobSheet(Guid engineerId);

    public Task<JobSheet> GetJobSheetByJobNo(Guid engineerId, string jobNo);

    Task<IEnumerable<JobSheet>> GetJobSheetByCallStatus(Guid engineerId, CallStatus CallStatus);
}

using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IRepositories;

public interface IManagerRepository : IBaseRepository<User>
{

    public Task<IEnumerable<DetailedJobSheet>> GetMyEngineersJobSheet(Guid managerId);

    public Task<DetailedJobSheet> GetJobSheetByJobNo(string JobNo);

    Task<IEnumerable<DetailedJobSheet>> GetJobSheetByCallStatus(Guid managerId, CallStatus CallStatus);

    Task<IEnumerable<DetailedJobSheet>> GetEngineersJobSheetByCallStatus(Guid managerId, Guid engineerId, CallStatus CallStatus);


}

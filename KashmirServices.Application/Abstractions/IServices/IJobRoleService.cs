using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IJobRoleService
{
  //  Task<APIResponse<IEnumerable<ProductSolutionQueryResponse>>> GetAll();


    Task<APIResponse<IEnumerable<JobRolesResponse>>> GetMyJobRolesAsync();

    Task<APIResponse<IEnumerable<MyEngineersJobRoleResponse>>> GetMyEngineersByCategoryIdAsync(Guid id);


}

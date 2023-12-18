using KashmirServices.Application.RRModels.Service;
using KashmirServices.Application.Shared;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IServiceTypeService
{
    Task<APIResponse<int>> Add(ServiceTypeRequest model);


    Task<APIResponse<int>> Update(ServiceTypeUpdateRequest model);


    Task<APIResponse<IEnumerable<ServiceTypeResponse>>> GetAll();


    Task<APIResponse<IEnumerable<ServiceTypeResponse>>> GetAll(int pageNo, int pageSize);


    Task<APIResponse<ServiceTypeResponse>> GetById(Guid id);

    Task<APIResponse<IEnumerable<ServiceTypeResponse>>>  GetByCategoryId(Guid id);


    Task<APIResponse<int>> Delete(Guid id);


    //Task<bool> IsUniqueSeviceName(string name);
}

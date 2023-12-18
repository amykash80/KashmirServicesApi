using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IAddressService
{
    Task<APIResponse<IEnumerable<AddressResponse>>> GetAll();

    Task<APIResponse<AddressResponse>> Add(AddressRequest model);


    Task<APIResponse<IEnumerable<AddressResponse>>> GetAllAddressesByEntityId(Guid? id);


    Task<APIResponse<AddressResponse>> GetById(Guid id);
    

    Task<APIResponse<IEnumerable<AddressResponse>>> GetByEntityId();


    Task<APIResponse<AddressResponse>> Update(AddressUpdateRequest model);


    Task<APIResponse<AddressResponse>> Delete(Guid id);

}

using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IPartsService
{
    Task<APIResponse<PartsResponse>> AddParts(PartsRequest model);

    Task<APIResponse<PartsResponse>> UpdateParts(UpdatePartsRequest model);

    Task<APIResponse<PartsResponse>> GetById(Guid id);

    Task<APIResponse<IEnumerable<PartsResponse>>> GetPartByPartNo(string partNo);

    Task<APIResponse<IEnumerable<PartsResponse>>> GetAllParts();

    Task<APIResponse<IEnumerable<PartsResponse>>> GetAllPartsByBrandId(Guid brandId);
}

using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IVisitService
{
    Task<APIResponse<string>> AddVisit(VisitRequest model);

    Task<APIResponse<IEnumerable<VisitResponse>>> GetAllVisits(Guid id);
}

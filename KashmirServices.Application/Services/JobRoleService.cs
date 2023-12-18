using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.Services;

public sealed class JobRoleService : IJobRoleService
{
    private readonly IJobRoleRepository repository;
    private readonly IBaseRepository<JobRole> engineerRepository;
    private readonly IContextService contextService;
    private readonly IMapper mapper;

    public JobRoleService(
        IJobRoleRepository repository,
        IBaseRepository<JobRole> engineerRepository,
        IContextService contextService,
        IMapper mapper)
    {
        this.repository = repository;
        this.engineerRepository = engineerRepository;
        this.contextService = contextService;
        this.mapper = mapper;
    }

    public async Task<APIResponse<IEnumerable<MyEngineersJobRoleResponse>>> GetMyEngineersByCategoryIdAsync(Guid id)
    {
        var managerId = contextService.GetUserId().Value;
        var responses = await repository.GetMyEngineersByCategoryIdAsync(id,managerId);
        if (responses.Any())
            return APIResponse<IEnumerable<MyEngineersJobRoleResponse>>.SuccessResponse(responses, "success");

        return APIResponse<IEnumerable<MyEngineersJobRoleResponse>>.ErrorResponse(APIMessages.NotFound, APIStatusCodes.NotFound); ;

    }

    public async Task<APIResponse<IEnumerable<JobRolesResponse>>> GetMyJobRolesAsync()
    {
        var userId = Guid.Parse("5D60C0CC-0EB9-4ECB-ACDE-0DE7D1344D70");// contextService.GetUserId();
        var responses= await repository.GetMyJobRolesAsync(userId);
        if (responses.Any())
            return APIResponse< IEnumerable<JobRolesResponse> >.SuccessResponse(responses ,"success");

        return APIResponse<IEnumerable<JobRolesResponse>>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError); 
    }

}

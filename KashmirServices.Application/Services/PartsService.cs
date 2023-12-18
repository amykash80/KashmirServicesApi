using AutoMapper;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.Services;

public class PartsService : IPartsService
{
    private readonly IPartsRepository repository;
    private readonly IMapper mapper;

    public PartsService(IPartsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<APIResponse<PartsResponse>> AddParts(PartsRequest model)
    {
       var isExist=  await repository.IsExist(x=>x.BrandId == model.BrandId && x.SerialNo == model.SerialNo );

        if (isExist) return APIResponse<PartsResponse>.ErrorResponse("Part already added", APIStatusCodes.Conflict);

       var part =  mapper.Map<Parts>(model);
        int returnValue =  await repository.InsertAsync(part);
        if(returnValue > 0)
        return APIResponse<PartsResponse>.SuccessResponse(mapper.Map<PartsResponse>(part), "Part Added successfully");

            return APIResponse<PartsResponse>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<IEnumerable<PartsResponse>>> GetAllParts()
    {
        var parts = await repository.GetAllAsync();

        if (parts.Any())
            return APIResponse<IEnumerable<PartsResponse>>.SuccessResponse(mapper.Map<IEnumerable<PartsResponse>>(parts));

        return APIResponse<IEnumerable<PartsResponse>>.ErrorResponse("No part found", APIStatusCodes.NotFound);
    }

    public async Task<APIResponse<IEnumerable<PartsResponse>>> GetAllPartsByBrandId(Guid brandId)
    {
        var parts = await repository.FindByAsync(x => x.BrandId == brandId);

        if (parts.Any())
            return APIResponse<IEnumerable<PartsResponse>>.SuccessResponse(mapper.Map<IEnumerable<PartsResponse>>(parts));

        return APIResponse<IEnumerable<PartsResponse>>.ErrorResponse("No part found", APIStatusCodes.NotFound);
    }

    public async Task<APIResponse<PartsResponse>> GetById(Guid id)
    {
        var part = await repository.GetByIdAsync(id);

        if (part is null) return APIResponse<PartsResponse>.ErrorResponse("No part found", APIStatusCodes.NotFound);

            return APIResponse<PartsResponse>.SuccessResponse(mapper.Map<PartsResponse>(part), "Part Added successfully");
    }

    public async Task<APIResponse<IEnumerable<PartsResponse>>> GetPartByPartNo(string partNo)
    {
        var parts = await repository.FindByAsync(x=>x.PartNo == partNo);

        if (parts.Any())
            return APIResponse<IEnumerable<PartsResponse>>.SuccessResponse(mapper.Map<IEnumerable<PartsResponse>>(parts));

        return APIResponse<IEnumerable<PartsResponse>>.ErrorResponse("No part found", APIStatusCodes.NotFound);
    }

    public async Task<APIResponse<PartsResponse>> UpdateParts(UpdatePartsRequest model)
    {
        var part = await repository.GetByIdAsync(model.Id);

        if (part is null) return APIResponse<PartsResponse>.ErrorResponse("No part found", APIStatusCodes.NotFound);

        var updatedPart = mapper.Map(model, part);
        int returnValue = await repository.UpdateAsync(part);
        if (returnValue > 0)
            return APIResponse<PartsResponse>.SuccessResponse(mapper.Map<PartsResponse>(updatedPart), "Part Added successfully");

        return APIResponse<PartsResponse>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }
}

using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.Services;

public class VisitService : IVisitService
{
    private readonly IVisitRepository repository;
    private readonly IMapper mapper;

    public VisitService(IVisitRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<APIResponse<string>> AddVisit(VisitRequest model)
    {
        var isExist=  await repository.IsExist(x=>x.AssingedEngineerId == model.AssingedEngineerId && x.VisitDate == DateTimeOffset.Now);
        if(isExist) return APIResponse<string>.ErrorResponse("Visit already posted", APIStatusCodes.Conflict);


        var visit=mapper.Map<Visit>(model);

        int returnValue=await repository.InsertAsync(visit);
        if(returnValue >0)
         return APIResponse<string>.SuccessResponse("Visit added successfully");

          return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<IEnumerable<VisitResponse>>> GetAllVisits(Guid id)
    {
       var visits=  await repository.FindByAsync(x=>x.Id == id);
        if(visits.Any())
        {
            return APIResponse<IEnumerable<VisitResponse>>.SuccessResponse(mapper.Map<IEnumerable<VisitResponse>>(visits),"Visit added successfully");
        }
        return APIResponse<IEnumerable<VisitResponse>>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }
}

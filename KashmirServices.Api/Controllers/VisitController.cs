using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisitController : ApiController
{
    private readonly IVisitService service;

    public VisitController(
        IVisitService service)
    {
        this.service = service;
    }



    [HttpPost]
    public async Task<IResult>  PostVisit(VisitRequest model)
    {
        var result = await service.AddVisit(model);
        return this.APIResult(result);
    }



    [HttpGet("engineer-assingment-id/{id:guid}")]
    public async Task<IResult> Get(Guid id)
    {
        var result = await service.GetAllVisits(id);
        return this.APIResult(result);
    }
}

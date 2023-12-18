
using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartsController : ApiController
{
    private readonly IPartsService service;

    public PartsController(
        IPartsService service )
    {
        this.service = service;
    }


    [HttpPost]
    public async Task<IResult> PostParts(PartsRequest model)
    {
        var result = await service.AddParts(model);

        return this.APIResult(result);
    }


    [HttpPut]
    public async Task<IResult> PutPart(UpdatePartsRequest model)
    {
        var result = await service.UpdateParts(model);

        return this.APIResult(result);
    }


    [HttpGet]
    public async Task<IResult> GetAllParts()
    {
        var result = await service.GetAllParts();
        return this.APIResult(result);
    }



    [HttpGet("{id:guid}")]
    public async Task<IResult> GetAllBooking(Guid id)
    {
        var result = await service.GetById(id);

        return this.APIResult(result);
    }



    [HttpGet("brand/{id:guid}")]
    public async Task<IResult> GetByBrandId(Guid id)
    {
        var result = await service.GetAllPartsByBrandId(id);

        return this.APIResult(result);
    }


    [HttpGet("partNo/{partNo:alpha}")]
    public async Task<IResult> GetBookingById(string partNo)
    {
        var result = await service.GetPartByPartNo(partNo);

        return this.APIResult(result);
    }
}

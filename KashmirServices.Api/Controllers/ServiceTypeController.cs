using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels.Service;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceTypeController : ApiController
{
    private readonly IServiceTypeService service;

    public ServiceTypeController(
        IServiceTypeService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        var result = await service.GetAll();

        return Results.Ok(result);
    }

    [HttpGet("pages")]
    public async Task<IResult> Get([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 5)
    {
        var result = await service.GetAll(pageNo, pageSize);

        return Results.Ok(result);
    }


    [HttpPost]
    public async Task<IResult> Post([FromBody] ServiceTypeRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.Add(model);

        return Results.Ok(result);
    }


    [HttpPut]
    public async Task<IResult> Put([FromBody] ServiceTypeUpdateRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.Update(model);

        return Results.Ok(result);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return Results.BadRequest("Invalid ID. Please provide a valid ID.");
        }

        var result = await service.Delete(id);

        return Results.Ok(result);
    }


    [HttpGet("{id:guid}")]
    public async Task<IResult> Get([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return Results.BadRequest("Invalid ID. Please provide a valid ID.");
        }

        var result = await service.GetById(id);

        return Results.Ok(result);
    }


    [HttpGet("category/{id:guid}")]
    public async Task<IResult> GetByCategoryId([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return Results.BadRequest("Invalid ID. Please provide a valid ID.");
        }

        var result = await service.GetByCategoryId(id);

        return Results.Ok(result);
    }



    //[HttpGet("check-name/{name}")]
    //public async Task<IResult> IsUniqueName([FromRoute] string name)
    //{
    //    if (string.IsNullOrEmpty(name))
    //    {
    //        return Results.BadRequest("Name cannot be empty.");
    //    }

    //    bool isUnique = await service.IsUniqueSeviceName(name);

    //    return Results.Ok(isUnique);
    //}
}

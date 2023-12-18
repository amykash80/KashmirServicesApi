using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using Microsoft.AspNetCore.Mvc;


namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ApiController
{
    private readonly IAddressService service;

    public AddressesController(IAddressService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        return this.APIResult(await service.GetAll());
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] AddressRequest model) => this.APIResult(await service.Add(model));


    [HttpGet("{id:guid}")]
    public async Task<IResult> GetById([FromRoute] Guid id) => this.APIResult(await service.GetById(id));


    [HttpGet("all/{id:guid?}")]
    public async Task<IResult> AddressByEntityId(Guid? id) => this.APIResult(await service.GetAllAddressesByEntityId(id));


    [HttpGet("user")]
    public async Task<IResult> GetByEntityId() => this.APIResult(await service.GetByEntityId());


    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete([FromRoute] Guid id) => this.APIResult(await service.Delete(id));


    [HttpPut]
    public async Task<IResult> Update(AddressUpdateRequest model) => this.APIResult(await service.Update(model));
}
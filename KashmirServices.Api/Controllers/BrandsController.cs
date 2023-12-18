using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ApiController
{
    private readonly IBrandService service;

    public BrandsController(IBrandService service)
    {
        this.service = service;
    }


    [HttpGet("categoryId/{id:guid}")]
    public async Task<IResult> GetByCategoryId(Guid id) => this.APIResult(await service.GetByCategoryId(id));

    [HttpGet]
    public async Task<IResult> GetAll() => this.APIResult(await service.GetAll());


    [HttpPost]
    public async Task<IResult> Post(BrandRequest model) => this.APIResult(await service.Add(model));
  

    [HttpPut]
    public async Task<IResult> Update(UpdateBrandRequest model)=> this.APIResult(await service.Update(model));
   
    
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetById(Guid id) => this.APIResult(await service.GetById(id));
   

    [HttpGet("names")]
    public async Task<IResult> GetBrandNames()=>this.APIResult(await service.GetBrandNames());


    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id) => this.APIResult(await service.Delete(id));


    [HttpGet("check-brandname/{brandName}")]
    public async Task<IResult> IsBrandNameUnique(string brandName)
    {
        var isUnique = await service.IsBrandNameUnique(brandName);
        return Results.Ok(isUnique);
    }
}

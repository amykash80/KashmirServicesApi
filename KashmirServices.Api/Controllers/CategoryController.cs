using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ApiController
{
    private readonly ICategoryService service;

    public CategoryController(ICategoryService service)
    {
        this.service = service;
    }


  

    [HttpGet]
    public async Task<IResult> GetAll()
    {
        var result = await service.GetAll();

        return Results.Ok(result);
    }

    [HttpPost]
    public async Task<IResult> Post([FromForm]CategoryRequest model) => this.APIResult(await service.Add(model));


    [HttpPut]
    public async Task<IResult> Update([FromForm]UpdateCategoryRequest model) => this.APIResult(await service.Update(model));


    [HttpGet("{id:guid}")]
    public async Task<IResult> GetById(Guid id) => this.APIResult(await service.GetById(id));


    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id) => this.APIResult(await service.Delete(id));


    [HttpGet("check/{categoryName}")]
    public async Task<IResult> IsCategoryUnique(string categoryName)
    {
        var isUnique = await service.IsCategoryUnique(categoryName);
        return Results.Ok(isUnique);
    }
}

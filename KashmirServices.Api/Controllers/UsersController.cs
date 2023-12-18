using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KashmirServices.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ApiController
{
    private readonly IUserService service;

    public UsersController(
        IUserService service)
    {
        this.service = service;
    }


    [HttpGet("managers")]
    public async Task<IResult> GetAllManagers([FromQuery] string searchTerm = "", [FromQuery] UserStatus status = UserStatus.Active) => this.APIResult(await service.GetAllUsers(searchTerm,status,UserRole.Manager));


    [HttpGet("managers-names")]
    public async Task<IResult> GetManagerNames() => this.APIResult(await service.GetAllManagerBasicDetails());


    [HttpGet("engineer-names")]
    public async Task<IResult> GetEngineerNames() => this.APIResult(await service.GetAllEngineersBasicDetails());


    [HttpGet("engineers")]
    public async Task<IResult> GetAllEngineers([FromQuery] string searchTerm = "", [FromQuery] UserStatus status = UserStatus.Active) => this.APIResult(await service.GetAllUsers(searchTerm, status, UserRole.ServiceEngineer));



    [HttpGet("admins")]
    public async Task<IResult> GetAllAdmin([FromQuery] string searchTerm = "", [FromQuery] UserStatus status = UserStatus.Active) => this.APIResult(await service.GetAllUsers(searchTerm, status, UserRole.Admin));


    [HttpGet("costumers")]
    public async Task<IResult> GetAllCostumers([FromQuery] string searchTerm = "", [FromQuery] UserStatus status = UserStatus.Active) => this.APIResult(await service.GetAllUsers(searchTerm, status, UserRole.Customer));


    [HttpGet("{id:guid?}")]
    public async Task<IResult> GetById(Guid? id) => this.APIResult(await service.GetUserById(id));

    

    [HttpPut]
    public async Task<IResult> PutUser([FromForm]UserUpdateRequest model) => this.APIResult(await service.UpdateUser(model));



    [HttpGet("update-userstatus/id:guid")]
    public async Task<IResult> UpdateUserStatus([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return Results.BadRequest("Invalid ID. Please provide a valid ID.");
        }

        return this.APIResult(await service.UpdateUserStatus(id));
    }



    //[HttpGet]
    //public async Task<IResult> Get(
    //    [FromQuery] string searchTerm = "", 
    //    [FromQuery] UserStatus status = UserStatus.Active,
    //    [FromQuery] params UserRole[]? roles)
    //{
    //    if (roles == null || roles.Length == 0) roles = new UserRole[]
    //    { 
    //        UserRole.Admin, 
    //        UserRole.Manager, 
    //        UserRole.ServiceEngineer 
    //    };

    //    return this.APIResult(await service.GetAllUsers(searchTerm, status, roles));
    //}
}

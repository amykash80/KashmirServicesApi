
using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/manager")]
[ApiController]
public class ManagerController : ApiController
{
    private readonly IManagerService service;
    private readonly IJobRoleService jobRoleService;

    public ManagerController(
        IManagerService service , IJobRoleService jobRoleService)
    {
        this.service = service;
        this.jobRoleService = jobRoleService;
    }


    [HttpGet("categories")]
    public async Task<IResult> GetMyCategories([FromQuery] Guid? id)
    {
        var result = await service.GetCategoriesByManager(id);

        return this.APIResult(result);
    }


    [HttpGet("engineers/category/{id:guid}")]
    public async Task<IResult> GetMyEngineersByCategoryId(Guid id)
    {
        var result = await jobRoleService.GetMyEngineersByCategoryIdAsync(id);

        return this.APIResult(result);
    }


    [HttpPost("assign-engineer-jobrole")]
    public async Task<IResult> AssignToServiceEngineer(JobRoleRequest model)
    {
        var result = await service.AssignJobRoleToEngineer(model);
        return this.APIResult(result);
    }



    [HttpGet("manager-bookings")]
    public async Task<IResult> GetAllBooking([FromQuery] Guid? id)
    {
        var result = await service.GetManagerBookings(id);

        return this.APIResult(result);
    }


    [HttpGet("bookings/{id:guid}")]
    public async Task<IResult> GetBookingById(Guid id)
    {
        var result = await service.GetCallBookingById(id);

        return this.APIResult(result);
    }


    [HttpPost("update/booking-status")]
    public async Task<IResult> ChangeBookingStatus(UpdateCallBookingStatusRequest model)
    {
        var result = await service.UpdateCallBookingStatus(model);

        return this.APIResult(result);
    }


    [HttpPost("call-assign-engineer")]
    public async Task<IResult> CallAssignToServiceEngineer([FromBody] AssignEngineerRequest model)
    {
        var result = await service.CallAssignToEngineer(model);

        return this.APIResult(result);  
    }


    [HttpGet("my-engineers-jobsheet")]
    public async Task<IResult> GetMyEngineersJobSheet([FromQuery] Guid  managerId)
    {
        var result = await service.GetMyEngineersJobSheet(managerId);
        return this.APIResult(result);
    }


    [HttpGet("engineers-jobsheet/{jobNo}")]
    public async Task<IResult> GetJobSheetByJobNo(string jobNo)
    {
        var result = await service.GetJobSheetByJobNo(jobNo);
        return this.APIResult(result);
    }


    [HttpGet("engineers-jobsheet/callstatus")]
    public async Task<IResult> GetJobSheetByJobNo([FromQuery] CallStatus callStatus)
    {
        var result = await service.GetJobSheetByCallStatus(callStatus);
        return this.APIResult(result);
    }


    [HttpGet("jobsheet-engineer/id/callstatus")]
    public async Task<IResult> GetEngineersJobSheetByCallStatus([FromQuery]Guid engineerId, [FromQuery] CallStatus callStatus)
    {
        var result = await service.GetEngineersJobSheetByCallStatus(engineerId,callStatus);
        return this.APIResult(result);
    }


}

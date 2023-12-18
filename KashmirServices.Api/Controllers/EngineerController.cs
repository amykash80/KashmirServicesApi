using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EngineerController : ApiController
{
    private readonly IEngineerService service;
    private readonly IJobRoleService jobRoleService;

    public EngineerController(
        IEngineerService service,IJobRoleService jobRoleService)
    {
        this.service = service;
        this.jobRoleService = jobRoleService;
    }



    [HttpGet("my-jobroles")]
    public async Task<IResult>  GetAllJobRolesAsync()
    {
        var result = await jobRoleService.GetMyJobRolesAsync();
        return this.APIResult(result);
    }



    [HttpGet("my-jobsheet")]
    public async Task<IResult> GetMyJobSheet()
    {
        var result = await service.GetMyJobSheet();
        return this.APIResult(result);
    }


    [HttpPost("schedule/booking")]
    public async Task<IResult> ScheduleBooking(ScheduleCallBookingRequest model)
    {
        var result = await service.ScheduleCallBookingByEngineer(model);

        return this.APIResult(result);
    }


    [HttpGet("jobsheet/{jobNo}")]
    public async Task<IResult> GetJobSheetByJobNo(string jobNo)
    {
        var result = await service.GetJobSheetByJobNo(jobNo);
        return this.APIResult(result);
    }


    [HttpGet("jobsheet/callstatus")]
    public async Task<IResult> GetJobSheetByJobNo([FromQuery] CallStatus callStatus)
    {
        var result = await service.GetJobSheetByCallStatus(callStatus);
        return this.APIResult(result);
    }

}

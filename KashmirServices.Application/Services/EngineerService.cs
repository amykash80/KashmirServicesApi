using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using System.Collections.Generic;

namespace KashmirServices.Application.Services;

internal sealed class EngineerService : IEngineerService
{
    private readonly IEngineerRepository repository;
    private readonly IBaseRepository<AssignedEngineer> assignEngineerRepository;
    private readonly ICallBookingRepository callBookingRepository;
    private readonly IMapper mapper;
    private readonly IContextService contextService;

    public EngineerService(
         IEngineerRepository repository,
         IBaseRepository<AssignedEngineer> assignEngineerRepository,
         ICallBookingRepository callBookingRepository,
         IContextService contextService,
        IMapper mapper)
    {
        this.repository = repository;
        this.assignEngineerRepository = assignEngineerRepository;
        this.callBookingRepository = callBookingRepository;
        this.contextService = contextService;
        this.mapper = mapper;
    }


    public async Task<APIResponse<IEnumerable<JobSheet>>> GetMyJobSheet()
    {
        var engineerId =contextService.GetUserId();
        var jobSheetResponse = await repository.GetMyJobSheet(engineerId.Value);
        if (jobSheetResponse.Any())
            return APIResponse<IEnumerable<JobSheet>>.SuccessResponse(jobSheetResponse);

        return APIResponse<IEnumerable<JobSheet>>.ErrorResponse("No Job Sheet Found", APIStatusCodes.NotFound); ;
    }

    public async Task<APIResponse<JobSheet>> GetJobSheetByJobNo(string jobNo)
    {
        var engineerId = Guid.Parse("5D60C0CC-0EB9-4ECB-ACDE-0DE7D1344D70");//contextService.GetUserId();
        var jobSheetResponse = await repository.GetJobSheetByJobNo(engineerId, jobNo);
        if (jobSheetResponse is not null)
            return APIResponse<JobSheet>.SuccessResponse(jobSheetResponse);

        return APIResponse<JobSheet>.ErrorResponse("No Job Sheet Found", APIStatusCodes.NotFound); ;
    }

    public async Task<APIResponse<IEnumerable<JobSheet>>> GetJobSheetByCallStatus(CallStatus callStatus)
    {
        var engineerId = Guid.Parse("5D60C0CC-0EB9-4ECB-ACDE-0DE7D1344D70");//contextService.GetUserId();
        var jobSheetResponse = await repository.GetJobSheetByCallStatus(engineerId, callStatus);
        if (jobSheetResponse is not null)
            return APIResponse<IEnumerable<JobSheet>>.SuccessResponse(jobSheetResponse);

        return APIResponse<IEnumerable<JobSheet>>.ErrorResponse("No Job Sheet Found", APIStatusCodes.NotFound); ;
    }

    public async Task<APIResponse<string>> ScheduleCallBookingByEngineer(ScheduleCallBookingRequest model)
    {
        var assignedEngineer = await assignEngineerRepository.GetByIdAsync(model.AssignedEngineerId);
        if (assignedEngineer is null)
            return APIResponse<string>.ErrorResponse("No job found", APIStatusCodes.NotFound);

        assignedEngineer.ExpectedDate = model.ExpectedDate;
        assignedEngineer.Remarks = model.Remarks;

        var callBooking = await callBookingRepository.GetByIdAsync(model.CallBookingId);

        callBooking!.CallBookingStatus = CallBookingStatus.Assigned;

       int returnValue=  await assignEngineerRepository.UpdateAsync(assignedEngineer);

        if(returnValue > 0)
        {
            await callBookingRepository.UpdateAsync(callBooking);
            return APIResponse<string>.SuccessResponse("Call scheduled successfully");
        }
        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError); 
    }
}

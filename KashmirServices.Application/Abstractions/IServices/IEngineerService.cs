using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IEngineerService
{
    Task<APIResponse<IEnumerable<JobSheet>>> GetMyJobSheet();

    Task<APIResponse<JobSheet>> GetJobSheetByJobNo(string jobNo);

    Task<APIResponse<IEnumerable<JobSheet>>> GetJobSheetByCallStatus(CallStatus callStatus);

    Task<APIResponse<string>> ScheduleCallBookingByEngineer(ScheduleCallBookingRequest model);

}

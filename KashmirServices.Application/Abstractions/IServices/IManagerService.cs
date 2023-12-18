using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IManagerService
{
    Task<APIResponse<IEnumerable<CategoryResponse>>> GetCategoriesByManager(Guid? id);

    Task<APIResponse<string>> AssignJobRoleToEngineer(JobRoleRequest model);


    Task<APIResponse<int>> CallAssignToEngineer(AssignEngineerRequest model);

    Task<APIResponse<IEnumerable<DetailedJobSheet>>> GetMyEngineersJobSheet(Guid? managerId);

    Task<APIResponse<DetailedJobSheet>> GetJobSheetByJobNo(string jobNo);

    Task<APIResponse<IEnumerable<DetailedJobSheet>>> GetJobSheetByCallStatus(CallStatus callStatus);

    Task<APIResponse<IEnumerable<DetailedJobSheet>>> GetEngineersJobSheetByCallStatus(Guid engineerId,CallStatus callStatus);

    Task<APIResponse<IEnumerable<DetailedCallBookingResponse>>> GetManagerBookings(Guid? managerId);

    Task<APIResponse<DetailedCallBookingResponse>> GetCallBookingById(Guid id);

    Task<APIResponse<DetailedCallBookingResponse>> UpdateCallBookingStatus(UpdateCallBookingStatusRequest model);

}

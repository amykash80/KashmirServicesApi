using AutoMapper;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels.Service;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using static KashmirServices.Application.Shared.APIMessages;

namespace KashmirServices.Application.Services;

internal sealed class ServiceTypeService : IServiceTypeService
{
    private readonly IServiceTypeRepository repository;
    private readonly ICallBookingRepository callBookingRepository;
    private readonly IMapper mapper;

    public ServiceTypeService(
        IServiceTypeRepository repository,
        ICallBookingRepository queryRepository,
        IMapper mapper)
    {
        this.repository = repository;
        this.callBookingRepository = queryRepository;
        this.mapper = mapper;
    }

    public async Task<APIResponse<int>> Add(ServiceTypeRequest model)
    {
        var service = mapper.Map<ServiceTypeRequest, ServiceType>(model);
        var isExist=await repository.IsExist(x=>x.Name == model.Name  && x.BrandId == model.BrandId);
        if(isExist)
            return APIResponse<int>.ErrorResponse("Service Type already added ", APIStatusCodes.Conflict);

        int insertResult = await repository.InsertAsync(service);

        if (insertResult > 0) return APIResponse<int>.SuccessResponse(insertResult, APIMessages.ServiceManagement.ServiceAdded);

        return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError);
    }

    public async Task<APIResponse<int>> Delete(Guid id)
    {
        var service = await repository.GetByIdAsync(id);

        if (service is null) return APIResponse<int>.ErrorResponse("No service found", APIStatusCodes.NotFound);


       var proQuery=  await callBookingRepository.FindByAsync(x => x.ServiceTypeId == id);
        if (proQuery.Any())
            return APIResponse<int>.ErrorResponse("You cannot delete this Service, because it is being used",APIStatusCodes.Conflict);

        int returnValue = await repository.DeleteAsync(service);

        if (returnValue > 0) return APIResponse<int>.SuccessResponse(returnValue,"Service Deleted successfully");

        return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError,APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<IEnumerable<ServiceTypeResponse>>> GetAll()
    {
        var services = await repository.GetAllAsync();
        //Check here hardcode true
   
        var serviceResponse =  services?.Select(x => new ServiceTypeResponse(x!.Id, x.Name, x.Description, x.Charges, x.IsAvailable,
                                                 x.FreeServiceDistance, x.PerKilometerCharge,x.BrandId));
        
        return APIResponse<IEnumerable<ServiceTypeResponse>>.SuccessResponse(serviceResponse);
    }

    public async Task<APIResponse<IEnumerable<ServiceTypeResponse>>> GetAll(int pageNo, int pageSize)
    {
        var services = await repository.FetchAllAsync(pageNo, pageSize);
        //Check here hardcode true
        var serviceResponse = services?.Select(x => new ServiceTypeResponse(x!.Id, x.Name, x.Description, x.Charges, x.IsAvailable,
                                                 x.FreeServiceDistance, x.PerKilometerCharge, x.BrandId));

        return APIResponse<IEnumerable<ServiceTypeResponse>>.SuccessResponse(serviceResponse);
    }

    public async Task<APIResponse<ServiceTypeResponse>> GetById(Guid id)
    {
        var service = await repository.GetByIdAsync(id);

        if (service is null) return APIResponse<ServiceTypeResponse>.SuccessResponse(
            default,
            APIStatusCodes.NoContent,
            APIMessages.ServiceManagement.ServiceNotFound);

        var serviceResponse = mapper.Map<ServiceTypeResponse>(service);

        return APIResponse<ServiceTypeResponse>.SuccessResponse(serviceResponse);

    }

    public async Task<APIResponse<IEnumerable<ServiceTypeResponse>>> GetByCategoryId(Guid id)
    {
        var services = await repository.FindByAsync(x=>x.BrandId == id);
        //Check here hardcode true
        var serviceResponse = services?.Select(x => new ServiceTypeResponse(x!.Id, x.Name, x.Description, x.Charges, x.IsAvailable,
                                                 x.FreeServiceDistance, x.PerKilometerCharge, x.BrandId));

        return APIResponse<IEnumerable<ServiceTypeResponse>>.SuccessResponse(serviceResponse);
    }

    //public async Task<bool> IsUniqueSeviceName(string name)
    //{
    //    var result = await repository.FindByAsync(x  => x.Name == name);

    //    return result?.Count() > 0;
    //}

    public async Task<APIResponse<int>> Update(ServiceTypeUpdateRequest model)
    {
        var service = mapper.Map<ServiceType>(model);

        var isExist = await repository.IsExist(x => x.Name == model.Name && x.BrandId == model.BrandId && x.Id != model.Id);
        if (isExist)
            return APIResponse<int>.ErrorResponse("Service Type already added ", APIStatusCodes.Conflict);

        int updateResult = await repository.UpdateAsync(service);

        if (updateResult > 0) return APIResponse<int>.SuccessResponse(updateResult, APIMessages.ServiceManagement.ServiceUpdated);

        return APIResponse<int>.ErrorResponse(APIMessages.TechnicalError);
    }
}

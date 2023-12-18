using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository repository;
    private readonly IMapper mapper;
    private readonly IContextService contextAccessor;

    public AddressService(IAddressRepository repository, IMapper mapper, IContextService contextAccessor)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.contextAccessor = contextAccessor;
    }
    public async Task<APIResponse<IEnumerable<AddressResponse>>> GetAll()
    {
        var addressesResult = await repository.FindByAsync(x => x.IsDeleted == false);

        var addressesResponse = mapper.Map<IEnumerable<AddressResponse>>(addressesResult);

        return APIResponse<IEnumerable<AddressResponse>>.SuccessResponse(addressesResponse);
    }

    public async Task<APIResponse<AddressResponse>> Add(AddressRequest model)
    {
        var address = mapper.Map<Address>(model);
        address.EntityId=contextAccessor.GetUserId() ?? model.EntityId;
        var returnResult = await repository.InsertAsync(address);
        if (returnResult > 0)
        {
            var addressResponse = mapper.Map<AddressResponse>(address);
            return APIResponse<AddressResponse>.SuccessResponse(addressResponse);
        }
        return APIResponse<AddressResponse>.ErrorResponse("There is some issue please try after sometime", APIStatusCodes.InternalServerError);

    }

    public async Task<APIResponse<AddressResponse>> Delete(Guid id)
    {
        var address = await repository.GetByIdAsync(id);

        if (address is null)
            return APIResponse<AddressResponse>.ErrorResponse("No address found", APIStatusCodes.NotFound);

        address.IsDeleted = true;

        int returnValue = await repository.UpdateAsync(address);

        if (returnValue > 0)
            return APIResponse<AddressResponse>.SuccessResponse(mapper.Map<AddressResponse>(address), APIStatusCodes.OK);

        return APIResponse<AddressResponse>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);

    }

    public async Task<APIResponse<IEnumerable<AddressResponse>>> GetAllAddressesByEntityId(Guid? id)
    {
        var userId= id ?? contextAccessor.GetUserId();
        var addresses = await repository.FindByAsync(x => x.IsDeleted == false &&   x.EntityId ==userId);
        if (addresses.Any())
            return APIResponse<IEnumerable<AddressResponse>>.SuccessResponse(mapper.Map<IEnumerable<AddressResponse>>(addresses), APIStatusCodes.OK);

        return APIResponse<IEnumerable<AddressResponse>>.ErrorResponse(APIMessages.NotFound, APIStatusCodes.NotFound);
    }


    public async Task<APIResponse<IEnumerable<AddressResponse>>> GetByEntityId()
    {
        var addresses = await repository.FindByAsync(x => x.EntityId == contextAccessor.GetUserId() && x.IsDeleted == false);

        if (addresses.Any())
            return APIResponse<IEnumerable<AddressResponse>>.SuccessResponse(mapper.Map<IEnumerable<AddressResponse>>(addresses), APIStatusCodes.OK, $"Found ${addresses.Count()} addresses");

        return APIResponse<IEnumerable<AddressResponse>>.ErrorResponse(APIMessages.Addresses.NoAddressFound, APIStatusCodes.NotFound);

    }


    public async Task<APIResponse<AddressResponse>> GetById(Guid id)
    {
        var address = await repository.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        if (address is not null)
            return APIResponse<AddressResponse>.SuccessResponse(mapper.Map<AddressResponse>(address), APIStatusCodes.OK);

        return APIResponse<AddressResponse>.ErrorResponse(APIMessages.Addresses.NoAddressFound, APIStatusCodes.NotFound);

    }

    public async Task<APIResponse<AddressResponse>> Update(AddressUpdateRequest model)
    {
        var address = await repository.FirstOrDefaultAsync(x => x.EntityId == contextAccessor.GetUserId());

        if (address is null)
            return APIResponse<AddressResponse>.ErrorResponse(APIMessages.Addresses.NoAddressFound, APIStatusCodes.NotFound);

        var updatedAddress = mapper.Map(model, address);

        int returnValue = await repository.UpdateAsync(updatedAddress);

        if (returnValue > 0)
            return APIResponse<AddressResponse>.SuccessResponse(mapper.Map<AddressResponse>(updatedAddress), APIStatusCodes.OK, APIMessages.Addresses.AddressUpdated);

        return APIResponse<AddressResponse>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);

    }
}

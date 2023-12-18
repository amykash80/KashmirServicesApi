using AutoMapper;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.Service;
using KashmirServices.Domain.Entities;


namespace KashmirServices.Application.MapperProfile;

public sealed class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<LoginRequest, User>();
        CreateMap<User, LoginResponse>();
    }
}

public sealed class AssignedEngineerProfile : Profile
{
    public AssignedEngineerProfile()
    {
        CreateMap<AssignEngineerRequest, AssignedEngineer>();
        CreateMap<AssignedEngineer, AssignEngineerResponse>();
        CreateMap<AssignEngineerUpdateRequest, AssignedEngineer>();
    }
}

public sealed class CallBookingProfile : Profile
{
    public CallBookingProfile()
    {
        CreateMap<CallBookingRequest, CallBooking>();
        CreateMap<CallBookingWithAddressRequest, CallBooking>();
        CreateMap<CallBooking, CallBookingResponse>();
        CreateMap<CallBooking, CustomerBookingResponse>();
    }
}


public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignUpRequest, User>();
        CreateMap<AdminSignUpRequest, User>();
        CreateMap<User, SignUpResponse>();
        CreateMap<User, ManagerResponse>();
        CreateMap<SignUpRequest, SignUpResponse>();
        CreateMap<User, UserResponse>();
        CreateMap<User, UserBasicDetailsResponse>();
        CreateMap<UserUpdateRequest, User>();
    }
}

public sealed class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressRequest, Address>();
        CreateMap<Address, AddressResponse>();
    }
}


public sealed class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<ContactRequest, Contact>();
        CreateMap<Contact, ContactResponse>();
    }
}


public sealed class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandRequest, Brand>();
        CreateMap<UpdateBrandRequest, Brand>();
        CreateMap<UpdateBrandRequest, BrandResponse>();
        CreateMap<Brand, BrandResponse>();
        CreateMap<Brand, BrandNames>();
    }
}


public sealed class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryRequest, Category>();

        CreateMap<UpdateCategoryRequest, Category>();

        CreateMap<Category, CategoryResponse>();
    }
}


public sealed class ServiceTypeProfile : Profile
{
    public ServiceTypeProfile()
    {
        CreateMap<ServiceTypeRequest, ServiceType>();
        CreateMap<ServiceTypeUpdateRequest, ServiceType>();
        CreateMap<ServiceType, ServiceTypeResponse>();
    }
}


public sealed class VisitProfile : Profile
{
    public VisitProfile()
    {
        CreateMap<VisitRequest, Visit>();
        CreateMap<Visit, VisitResponse>();
    }
}



public sealed class PartsProfile : Profile
{
    public PartsProfile()
    {
        CreateMap<PartsRequest, Parts>();
        CreateMap<UpdatePartsRequest, Parts>();
        CreateMap<Parts, PartsResponse>();
    }
}

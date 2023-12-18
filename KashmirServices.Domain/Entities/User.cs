using KashmirServices.Domain.Enums;
using KashmirServices.Domain.Shared;

namespace KashmirServices.Domain.Entities;

public sealed class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;


    public string Username { get; set; } = string.Empty;


    public string Password { get; set; } = string.Empty;


    public string Salt { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;


    public string PhoneNumber { get; set; } = string.Empty;


    public string? ConfirmationCode { get; set; }


    public Gender Gender { get; set; }


    public UserRole UserRole { get; set; } = UserRole.Customer;


    public UserStatus UserStatus { get; set; } = UserStatus.InActive;


    public bool IsEmailVerified { get; set; } = false;



    public ICollection<JobRole> EngineerJobRoles { get; set; } = null!;
}
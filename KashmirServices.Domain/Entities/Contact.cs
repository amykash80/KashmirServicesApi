using KashmirServices.Domain.Shared;

namespace KashmirServices.Domain.Entities;

public sealed class Contact : BaseEntity
{
    public string Name { get; set; } = null!;


    public string Email { get; set; } = null!;


    public string PhoneNumber { get; set; } = null!;


    public string Message { get; set; } = null!;
}

using KashmirServices.Domain.Shared;

namespace KashmirServices.Domain.Entities;

public sealed class Address : BaseEntity
{
    public string State { get; set; } = string.Empty;


    public string City { get; set; } = string.Empty;


    public string Region { get; set; } = null!;


    public string Street { get; set; } = string.Empty;


    public int PostalCode { get; set; }


    public string AddressLine { get; set; } = null!;


    public double? Longitute { get; set; } = null;


    public double? Latitute { get; set; } = null;


    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// This Entity is Whome Address?
    /// In Which Entity This Address is Depended?
    /// </summary>
    public Guid? EntityId { get; set; } = null;
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Application.RRModels;

public class AddressRequest
{
    [Required]
    public string State { get; set; } = string.Empty;

    
    [Required]
    public string City { get; set; } = string.Empty;


    [Required]
    public string Region { get; set; } = null!;


    [Required]
    public string Street { get; set; } = string.Empty;


    [Required]
    public int PostalCode { get; set; }


    public double? Longitute { get; set; } = null;


    public double? Latitute { get; set; } = null;


    [Required]
    public string AddressLine { get; set; } = null!;


    public Guid? EntityId { get; set; }
}

public class AddressResponse : AddressRequest
{

    public Guid Id { get; set; }
}


public class AddressUpdateRequest : AddressRequest
{
    public Guid Id { get; set; }
}
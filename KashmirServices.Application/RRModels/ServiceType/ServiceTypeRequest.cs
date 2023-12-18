using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Application.RRModels.Service;

public  class ServiceTypeRequest
{

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public decimal Charges { get; set; }


    public int FreeServiceDistance { get; set; }


    public int PerKilometerCharge { get; set; }

    public Guid BrandId { get; set; }
}


public class ServiceTypeUpdateRequest : ServiceTypeRequest 
{
    public Guid Id { get; set; }
}


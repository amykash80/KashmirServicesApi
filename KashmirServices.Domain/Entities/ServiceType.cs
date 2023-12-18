using KashmirServices.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Domain.Entities;

public sealed class ServiceType : BaseEntity
{
    public Guid BrandId { get; set; }


    public string Name { get; set; } = null!;


    public string Description { get; set; } = null!;


    public decimal Charges { get; set; }


    public int FreeServiceDistance { get; set; }


    public int PerKilometerCharge { get; set; }


    public bool IsAvailable { get; set; } = true;



    [ForeignKey(nameof(BrandId))]
    public Brand Brand { get; set; } = null!;


    //public Guid CategoryId { get; set; }


    //[ForeignKey(nameof(CategoryId))]
    //public Category Category { get; set; } = null!;


    public ICollection<CallBooking> CallBookings { get; set; } = null!;
}

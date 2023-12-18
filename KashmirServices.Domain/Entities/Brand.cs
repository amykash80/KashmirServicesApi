using KashmirServices.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Domain.Entities;

public sealed class Brand : BaseEntity
{
    public Guid CategoryId  { get; set; }


    public string Name { get; set; } = null!;


    public string Description { get; set; } = string.Empty;


    public bool IsDeleted { get; set; }


    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;


    public ICollection<Parts> Parts { get; set; } = null!;


   // public ICollection<CallBooking> CallBookings { get; set; } = null!;


    public ICollection<ServiceType> ServiceTypes { get; set; } = null!;


}

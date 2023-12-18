using KashmirServices.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Domain.Entities;

public sealed class Category : BaseEntity
{
    public string Name { get; set; } = null!;


    public string Description { get; set; } = string.Empty;



    public Guid ManagerId { get; set; }



    public bool IsDeleted { get; set; } = false;



    [ForeignKey(nameof(ManagerId))]
    public User Manager { get; set; } = null!;


   // public ICollection<ServiceType> ServiceTypes { get; set; } = null!;


    public ICollection<JobRole> JobRoles { get; set; } = null!;


    public ICollection<Brand> Brands{ get; set; } = null!;

}

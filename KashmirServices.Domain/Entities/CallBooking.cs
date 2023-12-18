using KashmirServices.Domain.Enums;
using KashmirServices.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Domain.Entities;

public sealed class CallBooking : BaseEntity
{
    public Guid ServiceTypeId { get; set; }


    public string JobNo { get; set; } = string.Empty;


    public Guid CustomerId { get; set; }


    public Guid AddressId { get; set; }



    public string? ModelNo { get; set; }


    public string? SerialNo { get; set; }


    public string? Description { get; set; }


    public DateTimeOffset RequestDate { get; set; } = DateTimeOffset.Now;


    public CallBookingStatus CallBookingStatus { get; set; } = CallBookingStatus.Pending;


    public int Reminder { get; set; } = 0;


    public int SatisficationCode { get; set; }


    public int UnSatisficationCode { get; set; }


    public string? Remarks { get; set; } = string.Empty;



    [ForeignKey(nameof(ServiceTypeId))]
    public ServiceType ServiceType { get; set; } = null!;



    [ForeignKey(nameof(CustomerId))]
    public User Customer { get; set; } = null!;



    [ForeignKey(nameof(AddressId))]
    public Address Address { get; set; } = null!;


    public ICollection<AssignedEngineer> AssignedEngineers { get; set; } = null!;
}

using KashmirServices.Domain.Enums;
using KashmirServices.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Domain.Entities;

public sealed class AssignedEngineer : BaseEntity
{
    public DateTimeOffset AssignmentDate { get; set; } = DateTime.Now;


    public DateTimeOffset? ExpectedDate { get; set; }


    public CallStatus CallStatus { get; set; } = CallStatus.Open;


    public Guid CallBookingId { get; set; }


    public Guid EngineerId { get; set; }


    public string? Remarks { get; set; }


    [ForeignKey(nameof(EngineerId))]
    public User Engineer { get; set; } = null!;


    [ForeignKey(nameof(CallBookingId))]
    public CallBooking CallBooking { get; set; } = null!;
}
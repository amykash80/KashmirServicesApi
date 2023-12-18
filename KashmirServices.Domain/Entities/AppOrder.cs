using EMedAppointmentSystem.Domain.Entities;
using KashmirServices.Domain.Enums;
using KashmirServices.Domain.Shared;

namespace KashmirServices.Domain.Entities;

public class AppOrder : BaseEntity
{
    public string OrderId { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Guid AppointmentId { get; set; }

    public string Receipt { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public bool IsPartial { get; set; }

    public decimal AmountPaid { get; set; }

    public decimal AmountDue { get; set; }

    public int CreatedAt { get; set; }

    public string Currency { get; set; } = "INR";

    public AppOrderStatus OrderStatus { get; set; }


    public ICollection<AppPayment> Payments { get; set; } = null!;
}

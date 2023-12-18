using System.ComponentModel.DataAnnotations.Schema;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using KashmirServices.Domain.Shared;

namespace EMedAppointmentSystem.Domain.Entities;

public class AppPayment : BaseEntity
{
    public string TransactionId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "INR";


    public PaymentMethod PaymentMethod { get; set; }

    public AppPaymentStatus AppPaymentStatus { get; set; }

    public string RpOrderId { get; set; } = string.Empty;


    public Guid OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public AppOrder Order { get; set; } = null!;

}

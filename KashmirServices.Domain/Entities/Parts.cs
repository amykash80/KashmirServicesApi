using KashmirServices.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Domain.Entities;

public sealed class Parts : BaseEntity
{
    public Guid? BrandId { get; set; }


    public string Name { get; set; } = null!;


    public string PartNo { get; set; } = string.Empty;

    public string SerialNo { get; set; } = string.Empty;

    public int Quantity { get; set; }


    public double PurchasePrice { get; set; }


    public double SellPrice { get; set; }


    public string Description { get; set; } = null!;



    [ForeignKey(nameof(BrandId))]
    public Brand Brand { get; set; } = null!;


    public ICollection<InvoiceDetails> InvoiceDetails { get; set; } = null!;
}

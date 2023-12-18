using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.RRModels;



public class InvoiceBasicDetails 
{
    public Guid Id { get; set; }

    public DateTimeOffset InvoiceDate { get; set; }

    public string InvoiceNo { get; set; } = string.Empty;

    public Guid CallBookingId { get; set; }

    public string JobNo { get; set; } =null!;

    public string FullName { get; set; } =null!;

    public string ServiceType { get; set; } =null!;

    public double Charges { get; set; }

    public int  FreeServiceDistance { get; set; }

    public int PerKilometerCharge { get; set; }

    public double GrandTotal { get; set; }
}

public class InvoiceItems
{
    public Guid InvoiceId { get; set; }


    public Guid PartId { get; set; }


    public Double Discount { get; set; }
}



  


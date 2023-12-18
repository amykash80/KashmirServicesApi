using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Application.RRModels;

public class JobSheet
{
    public Guid AssingedEngineerId { get; set; }

    public Guid CallBookingId { get; set; }

    public Guid EngineerId { get; set; }

    public string JobNo { get; set; } = string.Empty;

    public string EngineerName { get; set; } = string.Empty;

    public CallStatus CallStatus { get; set; }

    public DateTimeOffset AssignmentDate { get; set; }

    public DateTimeOffset ExpectedDate { get; set; }

    public string EngineerRemarks { get; set; } = string.Empty;

    public int Reminder { get; set; }

    public Guid CustomerId { get; set; }

    public Guid AddressId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;
   
    public string AddressLine { get; set; } = string.Empty;
  
    public int PostalCode { get; set; }
  
    public string BrandName { get; set; } = string.Empty;
  
    public string ServiceType { get; set; } = string.Empty;
   
    public string ModelNo { get; set; } = string.Empty;
   
    public string SerialNo { get; set; } = string.Empty;
   
    public string CallDescription { get; set; } = string.Empty;

    public string CallPriority { get; set; } = "Normal";

    public DateTimeOffset CreatedOn { get; set; }
}

public class DetailedJobSheet : JobSheet
{
    public CallBookingStatus CallBookingStatus { get; set; }

    public int SatisficationCode { get; set; }

    public int UnSatisficationCode { get; set; }

    public string CallRemarks { get; set; } = string.Empty;
}
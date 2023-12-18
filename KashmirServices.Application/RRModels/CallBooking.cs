using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Application.RRModels;

public class CallBookingRequest
{
    [Required]
    public Guid ServiceTypeId { get; set; }


    [Required]
    public Guid AddressId { get; set; }


    public string? ModelNo { get; set; }


    public string? SerialNo { get; set; }


    public string? Description { get; set; }
}


public class CallBookingUpdateRequest : CallBookingRequest
{
    public Guid Id { get; set; }

    public int Reminder { get; set; }

    public bool CancelBooking { get; set; }
}

public class UpdateCallBookingStatusRequest 
{
    public Guid Id { get; set; }

    public string? Remarks { get; set; } = string.Empty;

    public CallBookingStatus CallBookingStatus { get; set; }
}


public class ScheduleCallBookingRequest
{
    public Guid AssignedEngineerId { get; set; }

    public Guid CallBookingId { get; set; }

    public DateTimeOffset ExpectedDate { get; set; }

    public string Remarks { get; set; } = string.Empty;
}


public class CustomerReScheduleCallBookingRequest
{
    public Guid AssignedEngineerId { get; set; }


    public DateTimeOffset ExpectedDate { get; set; }

    public string Remarks { get; set; } = string.Empty;
}



public class CustomerBookingResponse: CallBookingRequest
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public Guid ManagerId { get; set; }


    public Guid CustomerId { get; set; }

    public Guid BrandId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string BrandName { get; set; } = string.Empty;


    public string ServiceTypeName { get; set; } = string.Empty;


    public CallBookingStatus CallBookingStatus { get; set; }

    public int Reminder { get; set; }

    public string JobNo { get; set; } = string.Empty;

    public DateTimeOffset RequestDate { get; set; }

    public string Remarks { get; set; } = string.Empty;

}

public class DetailedCallBookingResponse : CustomerBookingResponse
{
    public string SatisficationCode { get; set; } = string.Empty;

    public string UnSatisficationCode { get; set; } = string.Empty;

    public string Remarks { get; set; } = string.Empty;
}
public class CallBookingResponse : CallBookingRequest
{
    public Guid Id { get; set; }


    public Guid CustomerId { get; set; }


    public DateTimeOffset RequestDate { get; set; }


    public string JobNo { get; set; } = string.Empty;


    public CallBookingStatus CallBookingStatus { get; set; }



    public Guid ManagerId { get; set; }


    public string CategoryName { get; set; } = null!;


    public string CategoryDescription { get; set; } = string.Empty;
}


public class CallBookingWithAddressRequest : AddressRequest
{
    [Required]
    public Guid ProductSolutionId { get; set; }
}
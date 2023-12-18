using KashmirServices.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Application.RRModels;

public class AssignEngineerRequest
{
    public Guid CallBookingId { get; set; }


    public Guid EngineerId { get; set; }
}

public class AssignEngineerUpdateRequest: AssignEngineerRequest
{
    public Guid Id { get; set; }

    public string? Remarks { get; set; }

}


public class AssignEngineerResponse : AssignEngineerRequest
{
    public Guid Id { get; set; }
}

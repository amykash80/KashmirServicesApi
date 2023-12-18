using KashmirServices.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KashmirServices.Application.RRModels;

public class VisitRequest
{
    public TimeOnly TimeIn { get; set; }


    public TimeOnly TimeOut { get; set; }


    public Guid AssingedEngineerId { get; set; }


    public string? TechnicalRemarks { get; set; }


    public double TotalDistance { get; set; }
}


public class VisitResponse : VisitRequest
{
    public Guid Id { get; set; }
}

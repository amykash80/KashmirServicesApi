using KashmirServices.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Domain.Entities
{
    public  class Visit : BaseEntity
    {
        public DateTimeOffset VisitDate { get; set; }


        public TimeOnly TimeIn { get; set; }


        public TimeOnly TimeOut { get; set; }


        public Guid AssingedEngineerId { get; set; }


        public string? TechnicalRemarks { get; set; }

        public double TotalDistance { get; set; }


        [ForeignKey(nameof(AssingedEngineerId))]
        public AssignedEngineer AssignedEngineer { get; set; } = null!;
    }
}

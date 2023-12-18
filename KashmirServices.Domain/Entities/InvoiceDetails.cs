using KashmirServices.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Domain.Entities
{
    public class InvoiceDetails : BaseEntity
    {
        public Guid InvoiceId  { get; set; }


        public Guid PartId { get; set; }


        public Double Discount { get; set; }



        [ForeignKey(nameof(PartId))]
        public Parts Parts { get; set; } = null!;


        [ForeignKey(nameof(InvoiceId))]
        public Invoice Invoice { get; set; } = null!;
    }
}

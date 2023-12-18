using KashmirServices.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Domain.Entities
{
    public class Invoice :BaseEntity
    {
        public string InvoiceNo { get; set; } = string.Empty;


        public DateTimeOffset InvoiceDate { get; set; }


        public Guid CallBookingId { get; set; }



        public double TotalDistance { get; set; }



        [ForeignKey(nameof(CallBookingId))]
        public CallBooking CallBooking { get; set; } = null!;

        public ICollection<InvoiceDetails> InvoiceDetails { get; set; } = null!;
    }
}

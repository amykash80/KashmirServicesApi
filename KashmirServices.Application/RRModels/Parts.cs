using KashmirServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.RRModels
{
    public class PartsRequest
    {
        public Guid? BrandId { get; set; }


        public string Name { get; set; } = null!;


        public string PartNo { get; set; } = string.Empty;

        public string SerialNo { get; set; } = string.Empty;

        public int Quantity { get; set; }


        public double PurchasePrice { get; set; }


        public double SellPrice { get; set; }


        public string Description { get; set; } = null!;
    }

    public   class UpdatePartsRequest : PartsRequest 
    {
        public Guid Id { get; set; } 
    }

    public class PartsResponse:PartsRequest
    {
        public Guid Id { get; set; }
    }
}

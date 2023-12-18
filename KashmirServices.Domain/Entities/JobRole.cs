using KashmirServices.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Domain.Entities
{
    public class JobRole :BaseEntity
    {
        public Guid CategoryId { get; set; }


        public Guid EngineerId{ get; set; }


        public bool IsDeleted { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }=null!;



        [ForeignKey(nameof(EngineerId))]
        public User Engineer { get; set; }= null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.RRModels
{
    public class BrandRequest
    {
        [Required(ErrorMessage ="Brand Name is required")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Category is required")]
        public Guid CategoryId { get; set; }
        

        public string Description { get; set; } = string.Empty;
    }

    public class BrandResponse : BrandRequest
    {
        public Guid Id { get; set; }


        public bool IsDeleted { get; set; }


        public DateTimeOffset CreatedOn { get; set; }


        public string CategoryName { get; set; } = string.Empty;
    }


    public class BrandNames
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }


    public class UpdateBrandRequest : BrandRequest
    {
        public Guid Id { get; set; }
    }
}

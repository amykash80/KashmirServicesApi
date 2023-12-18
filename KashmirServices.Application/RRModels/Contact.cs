using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.RRModels
{
    public class ContactRequest
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Phone No. is required")]
        public string PhoneNumber { get; set; } = null!;


        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; } = null!;
    }

    public class ContactResponse:ContactRequest
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}

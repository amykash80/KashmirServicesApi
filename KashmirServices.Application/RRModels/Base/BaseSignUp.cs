using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Application.RRModels.Base;

public class BaseSignUp
{
    [Required(ErrorMessage = "Name is required")]
    public string FullName { get; set; } = string.Empty;


    [Required]
    [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please Enter Valid Email"), MinLength(6)]
    public string Email { get; set; } = string.Empty;



    [Required(ErrorMessage = "Phone Number is required")]
    public string PhoneNumber { get; set; } = string.Empty;
}

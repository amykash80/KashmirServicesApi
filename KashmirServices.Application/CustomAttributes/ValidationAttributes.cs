using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Application.CustomAttributes;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? date)
    {
        return ((DateTimeOffset)date!) > DateTimeOffset.Now ? true : false;
    }
}

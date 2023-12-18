namespace KashmirServices.Application.Utils.Exceptions;

public sealed class APIError
{
    public APIError()
    {

    }


    public APIError(string message)
    {
        Message = message;
    }


    public APIError(string message, string field)
        : this(message)
    {
        Field = field;
    }


    public string? Field { get; set; }


    public string? Message { get; set; }
}

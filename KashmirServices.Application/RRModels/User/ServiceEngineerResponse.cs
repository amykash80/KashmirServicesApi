namespace KashmirServices.Application.RRModels.User;

public sealed class ServiceEngineerResponse
{
    public Guid Id { get; set; }


    public string FullName { get; set; } = null!;


    public string Username { get; set; } = null!;


    public string Email { get; set; } = null!;


    public string PhoneNumber { get; set; } = null!;


    public bool IsAvailable { get; set; }
}

public class ServiceEngineerBasicDetailsResponse
{
    public Guid Id { get; set; }


    public string FullName { get; set; } = null!;
}

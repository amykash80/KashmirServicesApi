namespace KashmirServices.Application.Abstractions.Identity;

public interface IContextService
{
    Guid? GetUserId();

    string? GetUserName();

    string? GetFullName();

    string? GetEmail();

    string? GetUserRole();


    string HttpContextClientURL();

    string HttpContextCurrentURL();
}

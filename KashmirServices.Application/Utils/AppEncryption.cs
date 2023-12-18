using System.Security.Cryptography;

namespace KashmirServices.Application.Utils;

public class AppEncryption
{
    public static string GetRandomConfirmationCode()
    {
        return RandomNumberGenerator.GetInt32(1111, 9999).ToString();
    }


    public static string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }



    public static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

}

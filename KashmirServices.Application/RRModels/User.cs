using KashmirServices.Application.RRModels.Base;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Application.RRModels;

public class AdminSignUpRequest : BaseSignUp
{
    public UserRole UserRole { get; set; } 
}


public class SignUpRequest : BaseSignUp
{
    [Required(ErrorMessage = "Please Enter Your UserName")]
    public string UserName { get; set; } = string.Empty;


    public Gender Gender { get; set; }


    [Required(ErrorMessage = "Please Enter Password"), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;


    [Compare(nameof(Password), ErrorMessage = "Password & Confirm do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}


public class SignUpResponse
{
    public string UserName { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;


    public string UserRole { get; set; } = null!;


    public string UserStatus { get; set; } = null!;


    public bool IsEmailVerified { get; set; } = false;
}

public class UserBasicDetailsResponse
{
    public Guid Id { get; set; }


    public string FullName { get; set; } = string.Empty;


    public string Username { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;


    public string PhoneNumber { get; set; } = string.Empty;


    public Gender Gender { get; set; }
}
public class UserResponse : UserBasicDetailsResponse
{
    public UserRole UserRole { get; set; }


    public UserStatus UserStatus { get; set; }


    public bool IsEmailVerified { get; set; } = false;


    public string FilePath { get; set; } = string.Empty;


    public Module Module { get; set; }


    public DateTimeOffset CreatedOn { get; set; }
}


public class UserUpdateRequest 
{
    public Guid Id { get; set; }


    public string FullName { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;


    public string PhoneNumber { get; set; } = string.Empty;


    public Gender Gender { get; set; }

    public IFormFile?  File{ get; set; }=null!;
}


public class ManagerResponse : UserBasicDetailsResponse
{
}

public class EngineerResponse : UserBasicDetailsResponse
{
}

public class LoginRequest
{
    [Required(ErrorMessage = "Please Enter Your Valid UserName")]
    public string UserName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Please Enter Password"), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;


    public bool? RememberMe { get; set; } = false;
}


public class LoginResponse
{
    public string FullName { get; set; } = string.Empty;


    public UserRole UserRole { get; set; }


    public string Token { get; set; } = null!;
}


public class ChangePasswordRequest
{

    [Required(ErrorMessage = "Enter Old Password")]
    public string OldPassword { get; set; } = string.Empty;


    [Required(ErrorMessage = "Please Enter Password"), DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;


    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; } = string.Empty;

}



public class ForgotPasswordRequest
{
    public string Email { get; set; } = null!;
}


public class EmailVerificationRequest
{
    public string ConfirmationCode { get; set; } = null!;
}


public class ResetPasswordRequest
{
    public string Token { get; set; } = null!;


    [Required]
    public string NewPassword { get; set; } = string.Empty;


    [Required]
    [Compare(nameof(NewPassword), ErrorMessage = "Password / Confirm password do not match.")]
    public string ConfrimPassword { get; set; } = string.Empty;

}
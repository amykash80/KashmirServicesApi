using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IAuthService
{
    Task<APIResponse<SignUpResponse>> SignUp(SignUpRequest model);


    Task<APIResponse<SignUpResponse>> SignUp(AdminSignUpRequest model);


    Task<APIResponse<LoginResponse>> LogIn(LoginRequest model);


    Task<APIResponse<string>> ForgotPassword(ForgotPasswordRequest model);


    Task<APIResponse<string>> ChangePassword(ChangePasswordRequest model);


    Task<APIResponse<string>> VerifyEmail(string token);


    Task<APIResponse<string>> ResetPassword(ResetPasswordRequest model);


    Task<bool> IsUsernameUnique(string username);

    Task<bool> IsEmailUnique(string email);

    Task<bool> IsPhoneNumberUnique(string phoneNumber);
}

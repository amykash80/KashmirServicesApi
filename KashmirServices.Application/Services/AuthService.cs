using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IEmailService;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.Abstractions.JWT;
using KashmirServices.Application.Abstractions.TemplateRenderer;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Application.Utils;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KashmirServices.Application.Services;

internal sealed class AuthService : IAuthService
{
    private readonly IAuthRepository repository;
    private readonly IMapper mapper;
    private readonly IContextService contextService;
    private readonly IJwtProvider jwtProvider;
    private readonly IEmailService emailService;
    private readonly IEmailTemplateRenderer emailTemplateRenderer;
    private static readonly Random random = new();

    public AuthService(
        IAuthRepository repository,
        IMapper mapper,
        IContextService contextService,
        IJwtProvider jwtProvider,
        IEmailService emailService,
        IEmailTemplateRenderer emailTemplateRenderer )
    {
        this.repository = repository;
        this.mapper = mapper;
        this.contextService = contextService;
        this.jwtProvider = jwtProvider;
        this.emailService = emailService;
        this.emailTemplateRenderer = emailTemplateRenderer;
    }

    public async Task<APIResponse<SignUpResponse>> SignUp(SignUpRequest model)
    {

        bool isUsernameTaken = await repository.FirstOrDefaultAsync(x => x.Username == model.UserName) != null;

        if (isUsernameTaken)
            return APIResponse<SignUpResponse>.ErrorResponse(APIMessages.Auth.UsernameAlreadyTaken, APIStatusCodes.Conflict);

        bool isEmailRegistered = await repository.FirstOrDefaultAsync(x => x.Email == model.Email) != null;

        if (isEmailRegistered)
            return APIResponse<SignUpResponse>.ErrorResponse(APIMessages.Auth.EmailAlreadyRegistered, APIStatusCodes.Conflict);

        bool isPhoneNumberRegistered = await repository.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber) != null;

        if (isPhoneNumberRegistered)
            return APIResponse<SignUpResponse>.ErrorResponse(APIMessages.Auth.PhoneNumberAlreadyRegistered, APIStatusCodes.Conflict);


        var user = mapper.Map<SignUpRequest, User>(model);
        user.UserStatus = UserStatus.Active;
        user.IsEmailVerified = false;
        user.Salt = AppEncryption.GenerateSalt();
        user.Password = AppEncryption.HashPassword(user.Password, user.Salt);
        user.ConfirmationCode = AppEncryption.GenerateSalt();

        int insertResult = await repository.InsertAsync(user);
        if (insertResult > 0)
        {
            var signupResponse = new SignUpResponse()
            {
                UserName = user.Username,
                Email = user.Email,
                UserRole = Enum.GetName(typeof(UserRole), user.UserRole)!,
                UserStatus = Enum.GetName(typeof(UserStatus), user.UserStatus)!,
                IsEmailVerified = user.IsEmailVerified
            };

            var emailSetting = new MailSetting()
            {
                To = new List<string>() { user.Email },

                Subject = APIMessages.ConfirmEmailSubject,

                Body = await emailTemplateRenderer.RenderTemplateAsync(APIMessages.TemplateNames.EmailConfirmation, new
                {
                    Name = user.FullName,
                    CompanyName = APIMessages.ProjectName,
                    Link = $"{contextService.HttpContextClientURL()}/{APIMessages.VerifyEmailLink}?token={user.ConfirmationCode}"
                })
            };

            await emailService.SendEmailAsync(emailSetting);

            return APIResponse<SignUpResponse>.SuccessResponse(signupResponse, "User has been created successfully");
        }

        return APIResponse<SignUpResponse>.ErrorResponse(APIMessages.TechnicalError);
    }

    public async Task<APIResponse<SignUpResponse>> SignUp(AdminSignUpRequest model)
    {
        bool isEmailRegistered = await repository.FirstOrDefaultAsync(x => x.Email == model.Email) != null;

        if (isEmailRegistered)
            return APIResponse<SignUpResponse>.ErrorResponse(APIMessages.Auth.EmailAlreadyRegistered, APIStatusCodes.Conflict);

        bool isPhoneNumberRegistered = await repository.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber) != null;

        if (isPhoneNumberRegistered)
            return APIResponse<SignUpResponse>.ErrorResponse(APIMessages.Auth.PhoneNumberAlreadyRegistered, APIStatusCodes.Conflict);

        var user = mapper.Map<AdminSignUpRequest, User>(model);
        user.Id = Guid.NewGuid();
        user.Username = await GenerateUsername(model.Email);
        user.Salt = AppEncryption.GenerateSalt();
        user.Password = AppEncryption.HashPassword(model.PhoneNumber, user.Salt);
        user.ConfirmationCode = AppEncryption.GenerateSalt();
        user.CreatedBy = contextService.GetUserId();
        user.UserStatus = UserStatus.Active;
        user.IsEmailVerified = true;
        int insertResult = await repository.InsertAsync(user);
        if (insertResult > 0)
        {
            var signupResponse = new SignUpResponse()
            {
                UserName = user.Username,
                Email = user.Email,
                UserRole = Enum.GetName(typeof(UserRole), user.UserRole)!,
                UserStatus = Enum.GetName(typeof(UserStatus), user.UserStatus)!,
                IsEmailVerified = user.IsEmailVerified
            };
       

            var emailSetting = new MailSetting()
            {
                To = new List<string>() { user.Email },

                Subject = APIMessages.ConfirmEmailSubject,
                Body = await emailTemplateRenderer.RenderTemplateAsync(APIMessages.TemplateNames.ConfirmEmailWithUsername, new
                {
                    Name = user.FullName,
                    UserName = user.Username,
                    PhoneNumber = user.PhoneNumber,
                    CompanyName = APIMessages.ProjectName,
                    Link = $"{contextService.HttpContextClientURL()}/{APIMessages.VerifyEmailLink}?token={user.ConfirmationCode}"
                })
            };

            await emailService.SendEmailAsync(emailSetting);

            return APIResponse<SignUpResponse>.SuccessResponse(signupResponse);
        }

        return APIResponse<SignUpResponse>.ErrorResponse(APIMessages.TechnicalError);
    }


    public async Task<APIResponse<LoginResponse>> LogIn(LoginRequest model)
    {
        var user = await repository.FirstOrDefaultAsync(x => x.Username == model.UserName || x.Email == model.UserName);

        if (user is null)
            return APIResponse<LoginResponse>.ErrorResponse(APIMessages.Auth.UsernameOrPasswordIsIncorrect, APIStatusCodes.BadRequest);

        if (AppEncryption.HashPassword(model.Password, user.Salt) != user.Password)
            return APIResponse<LoginResponse>.ErrorResponse(APIMessages.Auth.UsernameOrPasswordIsIncorrect,APIStatusCodes.BadRequest);

        if (!user.IsEmailVerified)
            return APIResponse<LoginResponse>.ErrorResponse(APIMessages.Auth.VerifyEmailError, APIStatusCodes.BadRequest);

        if (user.UserStatus == UserStatus.InActive)
            return APIResponse<LoginResponse>.ErrorResponse(APIMessages.Auth.InactiveUser, APIStatusCodes.BadRequest);

        var loginResponse = new LoginResponse()
        {
            FullName = user?.FullName ?? "",
            UserRole = user!.UserRole,
            Token = jwtProvider.GenerateToken(user)
        };

        return APIResponse<LoginResponse>.SuccessResponse(loginResponse);
    }


    public async Task<APIResponse<string>> ChangePassword(ChangePasswordRequest model)
    {
        var user = await repository.FirstOrDefaultAsync(user => user.Id == contextService.GetUserId());

        if (user is null) return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError);

        if (AppEncryption.HashPassword(model.OldPassword, user.Salt) != user.Password)
            return APIResponse<string>.ErrorResponse(APIMessages.Auth.UsernameOrPasswordIsIncorrect, APIStatusCodes.BadRequest);

        user.Salt = AppEncryption.GenerateSalt();
        user.Password = AppEncryption.HashPassword(model.NewPassword, user.Salt);

        int updateResult = await repository.UpdateAsync(user);

        if (updateResult > 0)
            return APIResponse<string>.SuccessResponse(APIMessages.Auth.PasswordChangedSuccess);


        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<string>> VerifyEmail(string token)
    {
        var user = await repository.FirstOrDefaultAsync(x => x.ConfirmationCode!.Trim() == token.Trim());

        if (user is null)
            return APIResponse<string>.ErrorResponse(APIMessages.Auth.LinkExpired, APIStatusCodes.NotFound);

        user.IsEmailVerified = true;
        user.UserStatus = UserStatus.Active;
        user.ConfirmationCode = string.Empty;

        int updateResult = await repository.UpdateAsync(user);

        if (updateResult > 0)
            return APIResponse<string>.SuccessResponse(APIMessages.Auth.EmailVerified);

        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError);
    }

    public async Task<APIResponse<string>> ForgotPassword(ForgotPasswordRequest model)
    {
        var user = await repository.FirstOrDefaultAsync(user => user.Email == model.Email);

        if (user is null) return APIResponse<string>.ErrorResponse(APIMessages.Auth.InVaildEmailAddress);

        user.ConfirmationCode = AppEncryption.GenerateSalt();

        int updateResult = await repository.UpdateAsync(user);

        if (updateResult > 0)
        {
            var emailSetting = new MailSetting()
            {
                To = new List<string>() { user.Email },

                Subject = APIMessages.PasswordResetEmailSubject,

                Body = await emailTemplateRenderer.RenderTemplateAsync(APIMessages.TemplateNames.PasswordReset, new
                {
                    CompanyName = APIMessages.ProjectName,
                    Link = $"{contextService.HttpContextClientURL()}/{AppRoutes.ClientResetPasswordRoute}?token={user.ConfirmationCode}"
                })
            };

            await emailService.SendEmailAsync(emailSetting);

            return APIResponse<string>.SuccessResponse(APIMessages.Auth.CheckEmailToResetPassword);
        }

        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError);
    }

    public async Task<APIResponse<string>> ResetPassword(ResetPasswordRequest model)
    {
        var user = await repository.FirstOrDefaultAsync(x => x.ConfirmationCode!.Trim() == model.Token.Trim());

        if (user is null)
            return APIResponse<string>.ErrorResponse(APIMessages.Auth.LinkExpired, APIStatusCodes.NotFound);

        user.Salt = AppEncryption.GenerateSalt();
        user.Password = AppEncryption.HashPassword(model.NewPassword, user.Salt);
        user.ConfirmationCode = string.Empty;

        int updateResult = await repository.UpdateAsync(user);

        if (updateResult > 0)
            return APIResponse<string>.SuccessResponse(APIMessages.Auth.PasswordResetSuccess);

        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError);
    }

    public async Task<bool> IsUsernameUnique(string username)
    {
        return await repository.FirstOrDefaultAsync(x => x.Username == username) == null;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return await repository.FirstOrDefaultAsync(x => x.Email == email) == null;
    }

    public async Task<bool> IsPhoneNumberUnique(string phoneNumber)
    {
        return await repository.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) == null;
    }

    #region Private Methods
    private async Task<string> GenerateUsername(string email)
    {
        int rand = 0;
        string cleanedUsername = Regex.Replace(email.Substring(0, email.IndexOf('@')), @"[^a-zA-Z0-9]", "").ToLower();
        string finalUsername = $"{cleanedUsername}";

        while (await repository.FirstOrDefaultAsync(x => x.Username == finalUsername) != null)
        {
            finalUsername = $"{cleanedUsername}{++rand}";
        }

        return finalUsername;
    }


    #endregion
}
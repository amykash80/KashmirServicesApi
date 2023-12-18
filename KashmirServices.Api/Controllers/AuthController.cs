using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ApiController
{
    private readonly IAuthService service;
    public AuthController(IAuthService service)
    {
        this.service = service;
    }

    [HttpPost("signup")]
    public async Task<IResult> SignUp([FromBody] SignUpRequest model)
    {
        if (!ModelState.IsValid) return Results.BadRequest(ModelState);

        var result = await service.SignUp(model);

        return this.APIResult(result);
    }

    [HttpPost("admin-signup")]
    public async Task<IResult> AdminSignUp([FromBody] AdminSignUpRequest model)
    {
        if (!ModelState.IsValid) return Results.BadRequest(ModelState);

        if (!Enum.IsDefined(typeof(UserRole), model.UserRole!))
        {
            return Results.BadRequest("Invalid role provided.");
        }

        var result = await service.SignUp(model);
        return this.APIResult(result);
    }


    [HttpPost("login")]
    public async Task<IResult> LogIn([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.LogIn(model);

        return this.APIResult(result);
    }


    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IResult> ChangePassword([FromBody] ChangePasswordRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.ChangePassword(model);

        return this.APIResult(result);
    }

    [HttpGet("verifyemail")]
    public async Task<IResult> VerifyEmail([FromQuery] string token)
    {
        if (token == string.Empty)
        {
            return Results.BadRequest("Link Expired");
        }
        var result = await service.VerifyEmail(token);
        return Results.Redirect("http://localhost:4200/verifyemail");
       // return this.APIResult(result);
    }


    [HttpPost("forgotPassword")]
    public async Task<IResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.ForgotPassword(model);

        return this.APIResult(result);
    }


    [HttpPost("resetpassword")]
    public async Task<IResult> ResetPassword([FromBody] ResetPasswordRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.ResetPassword(model);

        return this.APIResult(result);
    }


    [HttpGet("check-username/{username}")]
    public async Task<IActionResult> CheckUsernameAvailability([FromRoute] string username)
    {
        bool isUnique = await service.IsUsernameUnique(username);

        return Ok(new { isUniqueUserName = isUnique });
    }


    [HttpGet("check-email/{email}")]
    public async Task<IActionResult> CheckEmailAvailability([FromRoute] string email)
    {
        bool isUnique = await service.IsEmailUnique(email);

        return Ok(new { isUniqueEmail = isUnique });
    }


    [HttpGet("check-phonenumber/{phonenumber}")]
    public async Task<IResult> CheckPhoneNumberAvailability([FromRoute] string phoneNumber)
    {
        bool isUnique = await service.IsPhoneNumberUnique(phoneNumber);

        return Results.Ok(new { isUniquePhoneNumber = isUnique });
    }
}
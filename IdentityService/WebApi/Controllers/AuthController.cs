using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.RefreshTokens;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Responses;
using Core.Cqrs;
using Core.Security.Jwt;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await CqrsProcessor.SendAsync(command);
            return Created("", result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginUserCommand command = new LoginUserCommand() { UserForLoginDto = userForLoginDto };
            command.IpAddress = getIpAddress();

            LoggedResponse loggedResponse = await CqrsProcessor.SendAsync(command);
            if (loggedResponse.RefreshToken is not null)
                setRefreshTokenFromCookie(loggedResponse.RefreshToken);
            return Ok(loggedResponse.ToResponse());
        }

        [HttpGet("RefreshToken")]
        public async Task<ActionResult<AccessToken>> RefreshToken()
        {
            RefreshTokenCommand command = new RefreshTokenCommand() { RefreshToken = getRefreshTokenFromCookie(), IpAddress = getIpAddress() };
            RefreshedTokenResponse response = await CqrsProcessor.SendAsync(command);
            setRefreshTokenFromCookie(response.RefreshToken);
            return Created("", response.AccessToken);
        }

        //[HttpGet("EnableEmailAuthenticator")]
        //public async Task<IActionResult> EnableEmailAuthenticator()
        //{
        //    EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand =
        //        new() { UserId = getUserIdFromRequest(), VerifyEmailUrl = $"{_configuration["APIUrl"]}/Auth/VerifyEmail" };
        //    await CqrsProcessor.SendAsync(enableEmailAuthenticatorCommand);

        //    return Ok();
        //}

        //[HttpPost("VerifyEmail")]
        //public async Task<IActionResult> VerifyStatus([FromQuery] VerifyEmailAuthenticatorCommand command)
        //{
        //    await CqrsProcessor.SendAsync(command);

        //    return Ok();
        //}
    }
}

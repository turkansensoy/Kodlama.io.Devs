using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.Register;
using Application.Features.Users.Commands.CreateUser;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var userForRegisterDto = await Mediator.Send(registerCommand);
            return Ok(userForRegisterDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var result = await Mediator.Send(loginCommand);
            return Ok(result);
        }
    }
}

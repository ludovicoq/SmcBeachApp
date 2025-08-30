using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmBeachApp.Entities.Models;
using SmBeachApp.Services;

namespace SmBeachApp.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
   private readonly AuthService _authService;
   public AuthController(AuthService authService)
   {
      _authService = authService;
   }

   [HttpPost("register")]
   public async Task<ActionResult> Register([FromBody] UserDto request)
   {
      await _authService.Register(request);
      return Ok();
   }

   [HttpPost("login")]
   public async Task<ActionResult<string>> Login([FromBody] UserDto request)
   {
      var token =  await _authService.Login(request);
      return Ok(token);
   }

   [Authorize]
   [HttpGet]
   public IActionResult GetAuthApi()
   {
      return Ok("You are authenticated");
   }
}
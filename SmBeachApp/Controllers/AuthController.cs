using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmBeachApp.Entities;
using SmBeachApp.Entities.Models;
using SmBeachApp.Services;

namespace SmBeachApp.Controllers;
// todo: move all methods to a service class
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
   private readonly AuthService _authService;
   public AuthController(AuthService authService)
   {
      _authService = authService;
   }

   [HttpPost("register")]
   public ActionResult<User> Register([FromBody] UserDto request)
   {
      var user = _authService.Register(request);
      
      return Ok(user);
   }

   [HttpPost("login")]
   public ActionResult<string> Login([FromBody] UserDto request)
   {
      var token =  _authService.Login(request);
      return Ok(token);
   }
}
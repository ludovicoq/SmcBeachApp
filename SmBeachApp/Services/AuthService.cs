using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmBeachApp.Data;
using SmBeachApp.Entities;
using SmBeachApp.Entities.Models;
using SmBeachApp.Localization.Localization;
using UserRole = SmBeachApp.Entities.UserRole;

namespace SmBeachApp.Services;

public class AuthService(IConfiguration configuration, IServiceProvider _serviceProvider)
{
    public async Task Register(UserDto request)
    {
        using var ctx = _serviceProvider.GetRequiredService<SmBeachDbContext>();
        
        var userExists = await ctx.Users.AnyAsync(x => x.Username == request.Username);

        if (userExists)
        {
            throw new HttpException(HttpStatusCode.BadRequest, TranslationStrings.AUTH_USER_ALREADY_REGISTERED);
        }

        var user = new User();
        await ctx.Users.AddAsync(user);
        
        var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;
        user.Username = request.Username;
        user.Email = request.Email;
        user.ActiveType = request.ActiveType;
        foreach (var role in request.Roles)
        {
            user.UserRoles.Add(new UserRole()
            {
                UserId = user.UserId,
                RoleId = role.RoleId,
            });
        }
        
        await ctx.SaveChangesAsync();
    }

    public async Task<string> Login(UserDto request)
    {
        using var ctx = _serviceProvider.GetRequiredService<SmBeachDbContext>();
        var user = await ctx.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.BadRequest, TranslationStrings.AUTH_USER_NOT_FOUND);
        }
        
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            throw new HttpException(HttpStatusCode.BadRequest, TranslationStrings.AUTH_USER_PSW_INCORRECT);
        }
        
        string token = CreateToken(user);
        return token;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Appsettings:Token")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("Appsettings:Issuer"),
            audience: configuration.GetValue<string>("Appsettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
        
}
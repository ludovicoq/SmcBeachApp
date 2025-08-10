using System.Net;
using Mch.MainManagerSrv.Data.Models;
using Mch.MainManagerSrv.Resources.Localization;
using Microsoft.AspNetCore.Identity;
using SmBeachApp.Entities;
using SmBeachApp.Entities.Models;

namespace SmBeachApp.Services;

public class AuthService
{
    public static User user = new();
    
    public User Register(UserDto request)
    {
        var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;
        user.Username = request.Username;
        return user;
    }

    public string Login(UserDto request)
    {
        if (user.Username == request.Username)
        {
            // todo: translate
            throw new HttpException(HttpStatusCode.BadRequest, TranslationStrings.AUTH_USER_NOT_FOUND);
        }
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            // todo: translate here
            throw new HttpException(HttpStatusCode.BadRequest, TranslationStrings.AUTH_USER_PSW_INCORRECT);
        }
        // todo: create token here
        string token = "success";
        return token;
    }

    public string CreateToken(UserDto request)
    {
        return string.Empty;
    }
        
}
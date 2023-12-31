﻿using System.Security.Claims;
using cliph.Models;
using cliph.Models.Requests;
using cliph.Models.Responses;
using cliph.Models.User;
using MongoDB.Bson;

namespace cliph.Library;

public static class UserLib
{
    public static UserResponse CreateUser(UserRequest newUserData, UserType userType, IConfiguration configuration, string? adminApiKey)
    {
        var newUserId = ObjectId.GenerateNewId();

        if (newUserData.Email == null || newUserData.Password == null)
            throw new Exception("Unable to retrieve new user data");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, newUserData.Email),
            new Claim("UserId", newUserId.ToString()),
        };
        
        var jwt = Jwt.CreateJwt(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(configuration, "JWT:Secret"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(configuration, "JWT:Issuer"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(configuration, "JWT:Audience"),
            60.0,
            claims
        );

        User newUser;

        if (userType == UserType.Admin)
        {
            newUser = new AdminUser
            {
                Id = newUserId,
                Email = newUserData.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUserData.Password),
                ApiKey = new ApiKey
                {
                  Value  = Guid.NewGuid().ToString(),
                  CreatedAt = DateTime.Now
                },
                UserType = userType,
            };
            
            return new UserResponse
            {
                Jwt = jwt,
                User = newUser
            };
        }

        newUser = new ManagedUser
        {
            Id = newUserId,
            Email = newUserData.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUserData.Password),
            AdminApiKey = adminApiKey,
            UserType = userType,
        };

        return new UserResponse
        {
            Jwt = jwt,
            User = newUser
        };
    }

    public static string GetBearerTokenFromRequest(HttpRequest request)
    {
        var tokenWithTrimmedBearer = request.Headers.Authorization.ToString().Replace("Bearer ", "");

        if (string.IsNullOrWhiteSpace(tokenWithTrimmedBearer))
            throw new Exception("Unable to retrieve bearer token");

        return tokenWithTrimmedBearer;
    }
}
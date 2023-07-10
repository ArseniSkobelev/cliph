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

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, newUserData.Email),
            new Claim("user_id", newUserId.ToString()),
        };
        
        var jwt = Jwt.CreateJwt(
            configuration.GetValue<String>("JWT:Secret") ??
            throw new InvalidOperationException("Unable to retrieve configuration setup"),
            configuration.GetValue<String>("JWT:Issuer") ??
            throw new InvalidOperationException("Unable to retrieve configuration setup"),
            configuration.GetValue<String>("JWT:Audience") ??
            throw new InvalidOperationException("Unable to retrieve configuration setup"),
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
}
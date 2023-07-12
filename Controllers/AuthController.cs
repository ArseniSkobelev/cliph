using System.Security.Authentication;
using cliph.Models.Http;
using cliph.Models.Requests;
using cliph.Models.Responses;
using cliph.Services.AuthService;
using cliph.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace cliph.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("account")]
    public async Task<JsonResult> CreateUser([FromBody] UserRequest newUserData)
    {
        try
        {
            if (Request.Headers.TryGetValue("x-cliph-key", out var apiKeyHeaderValue))
            {
                if (string.IsNullOrWhiteSpace(apiKeyHeaderValue))
                    return new JsonResult(new Response(false, "Unable to verify administrator authentication token"));

                var managedUserResult = await _authService.CreateAccount(newUserData, apiKeyHeaderValue!);

                return new JsonResult(new Response<UserResponse>(true, managedUserResult, "Created a new user successfully"));
            }

            var adminUserResult = await _authService.CreateAccount(newUserData);
        
            return new JsonResult(new Response<UserResponse>(true, adminUserResult, "Created a new user successfully"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
    
    [HttpPost("session")]
    public async Task<JsonResult> CreateSession([FromBody] UserRequest existingUserData)
    {
        try
        {
            if (Request.Headers.TryGetValue("x-cliph-key", out var apiKeyHeaderValue))
            {
                if (string.IsNullOrWhiteSpace(apiKeyHeaderValue))
                    return new JsonResult(new Response(false, "Unable to verify administrator authentication token"));

                var managedUserResult = await _authService.CreateSession(existingUserData, apiKeyHeaderValue.ToString());

                return new JsonResult(new Response<UserResponse>(true, managedUserResult, "Created a session successfully"));
            }

            var adminUserResult = await _authService.CreateSession(existingUserData);
            
            return new JsonResult(new Response<UserResponse>(true, adminUserResult, "Created a session successfully"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            Console.WriteLine(e.Message);
            return new JsonResult(new Response(false, e.Message));
        }
    }
}
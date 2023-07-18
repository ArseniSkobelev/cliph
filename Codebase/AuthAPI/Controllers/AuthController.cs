using System.Security.Authentication;
using cliph.Library;
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
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }
    
    [HttpPost("account")]
    public async Task<JsonResult> CreateUser([FromBody] UserRequest newUserData)
    {
        // try
        // {
            if (Request.Headers.TryGetValue("x-cliph-key", out var apiKeyHeaderValue))
            {
                if (string.IsNullOrWhiteSpace(apiKeyHeaderValue))
                    return new JsonResult(new Response(false, "Unable to verify administrator authentication token"));

                var managedUserResult = await _authService.CreateAccount(newUserData, apiKeyHeaderValue!);

                Response.StatusCode = 201;
                return new JsonResult(new Response<UserResponse>(true, managedUserResult, "Created a new user successfully"));
            }
            
            if (Request.Headers.TryGetValue("x-cliph-cross-service-authentication", out var cscaHeaderValue))
            {
                string privateKey =
                    ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration,
                        "CrossServiceCommunicationAuthentication:Secret");
                
                if(cscaHeaderValue != privateKey)
                    return new JsonResult(new Response(false, "You don't have the required permission to access this resource"));
                
                var adminUserResult = await _authService.CreateAccount(newUserData);

                Response.StatusCode = 201;
                return new JsonResult(new Response<UserResponse>(true, adminUserResult, "Created a new user successfully"));
            }

            Response.StatusCode = 404;
            return new JsonResult(new Response(false, "Unable to determine the required user type"));
        // }
        // catch (Exception e)
        // {
        //     Response.StatusCode = 500;
        //     return new JsonResult(new Response(false, e.Message));
        // }
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
            
            if (Request.Headers.TryGetValue("x-cliph-cross-service-authentication", out var cscaHeaderValue))
            {
                string privateKey =
                    ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration,
                        "CrossServiceCommunicationAuthentication:Secret");
                
                if(cscaHeaderValue != privateKey)
                    return new JsonResult(new Response(false, "You don't have the required permission to access this resource"));
                
                var adminUserResult = await _authService.CreateSession(existingUserData);

                Response.StatusCode = 200;
                return new JsonResult(new Response<UserResponse>(true, adminUserResult, "Created a session successfully"));
            }
            
            Response.StatusCode = 401;
            return new JsonResult(new Response(false, "Unable to determine the required user type"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            Console.WriteLine(e.Message);
            return new JsonResult(new Response(false, e.Message));
        }
    }
}
using cliph.Library;
using cliph.Models.Http;
using cliph.Models.Responses;
using cliph.Models.User;
using cliph.Services.UserService;
using Cliph.AuthAPI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace cliph.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<JsonResult> GetUser()
    {
        try
        {
            if (Request.Headers.TryGetValue("x-cliph-key", out var apiKeyHeaderValue))
            {
                // managed user trying to recieve their data

                await Console.Out.WriteLineAsync("managed user");

                if (Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                {
                    if (string.IsNullOrWhiteSpace(authHeaderValue))
                        return new JsonResult(new Response(false, "Authentication token not found"));

                    var userData = await _userService.GetManagedUserData(authHeaderValue.ToString().Replace("Bearer ", ""));

                    return new JsonResult(new Response<ManagedUser>(true, userData, "Retrieved user data successfully"));
                }
            }

            if (Request.Headers.TryGetValue("x-cliph-cross-service-authentication", out var cscaHeaderValue))
            {
                // admin user

                string privateKey =
                    ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration,
                        "CrossServiceCommunicationAuthentication:Secret");

                if (cscaHeaderValue != privateKey)
                    return new JsonResult(new Response(false, "You don't have the required permission to access this resource"));

                if (Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                {
                    if (string.IsNullOrWhiteSpace(authHeaderValue))
                        return new JsonResult(new Response(false, "Authentication token not found"));

                    var userData = await _userService.GetAdminUserData(authHeaderValue.ToString().Replace("Bearer ", ""));

                    return new JsonResult(new Response<AdminUser>(true, userData, "Retrieved user data successfully"));
                }
            }
        
            return new JsonResult(new Response(false, "Unable to fetch the requested users data"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            await Console.Out.WriteLineAsync(e.Message);
            return new JsonResult(new Response(false, e.Message));
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<JsonResult> DeleteUser([FromBody] Email email)
    {
        try
        {
            await Console.Out.WriteLineAsync(email.EmailAddress);
            await _userService.DeleteAdminUser(Request.Headers.Authorization.ToString().Replace("Bearer ", ""));
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
}
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
    public async Task<JsonResult> DeleteUser([FromBody] Email? email)
    {
        try
        {
            // fetch headers
            string? apiKeyHeaderValue = Request.Headers["x-cliph-key"];
            string? cscaHeaderValue = Request.Headers["x-cliph-cross-service-authentication"];

            if (cscaHeaderValue != null)
            {
                // admin is trying to delete itself or a user managed by him
                if (email == null)
                {
                    // admin user is trying to delete itself
                    await _userService.DeleteAdminUser(UserLib.GetBearerTokenFromRequest(Request));
                    return new JsonResult(new Response(true, "Your admin user deleted successfully"));
                }

                // admin is trying to delete a user managed by him
                await _userService.DeleteManagedUser(UserLib.GetBearerTokenFromRequest(Request),
                    email.EmailAddress);
                return new JsonResult(new Response(true, "Deleted your managed user successfully"));
            }

            // here we already know that the user is a managed user, therefore,
            // check whether they have an API key present.
            if(apiKeyHeaderValue == null)
                return new JsonResult(new Response(false, "Unable to verify the provided API key"));

            // delete the managed user account
            await _userService.DeleteManagedUser(UserLib.GetBearerTokenFromRequest(Request));
            return new JsonResult(new Response(true, "Deleted your user account successfully"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
}
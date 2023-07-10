using cliph.Models.Http;
using cliph.Models.Requests;
using cliph.Models.Responses;
using cliph.Models.User;
using cliph.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cliph.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<JsonResult> CreateUser([FromBody] UserRequest newUserData)
    {
        try
        {
            if (Request.Headers.TryGetValue("x-cliph-key", out var apiKeyHeaderValue))
            {
                if (string.IsNullOrWhiteSpace(apiKeyHeaderValue))
                    return new JsonResult(new Response(false, "Unable to verify administrator authentication token"));

                var managedUserResult = await _userService.CreateUser(newUserData, apiKeyHeaderValue!);

                return new JsonResult(new Response<UserResponse>(true, managedUserResult, "Created a new user successfully"));
            }

            var adminUserResult = await _userService.CreateUser(newUserData);
        
            return new JsonResult(new Response<UserResponse>(true, adminUserResult, "Created a new user successfully"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
    
    [Authorize]
    [HttpGet]
    public async Task<JsonResult> GetUser()
    {
        try
        {
            if (Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
            {
                if (string.IsNullOrWhiteSpace(authHeaderValue))
                    return new JsonResult(new Response(false, "Authentication token not found"));

                var userData = await _userService.GetUserData(authHeaderValue.ToString().Replace("Bearer ", ""));

                return new JsonResult(new Response<User>(true, userData, "Retrieved user data successfully"));
            }
        
            return new JsonResult(new Response(false, "Unable to fetch the requested users data"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
}
using cliph.Models.Http;
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
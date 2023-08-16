using cliph.Library;
using cliph.Models;
using cliph.Models.Http;
using cliph.Models.User;
using cliph.Services.StatsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace cliph.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;

    public StatsController(IStatsService statsService) 
    {
        _statsService = statsService;
    }

    [HttpGet("users")]
    public async Task<JsonResult> GetUsersManagedByAdmin()
    {
        try
        {
            var adminUserId = Jwt.RetrieveClaimByClaimType(Request.Headers.Authorization.ToString().Replace("Bearer ", ""), ClaimType.UserId).Value.ToString();
            var users = await _statsService.GetManagedUsers(ObjectId.Parse(adminUserId));

            return new JsonResult(new Response<List<ManagedUser>>(true, users, "Fetched users successfully"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
}
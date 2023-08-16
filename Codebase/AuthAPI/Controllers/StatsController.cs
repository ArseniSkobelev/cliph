using cliph.Library;
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
        var adminUserId = Jwt.RetrieveClaimByClaimType(Request.Headers.Authorization.ToString(), "user_id").Value.ToString();
        var users = _statsService.GetManagedUsers(ObjectId.Parse(adminUserId));

        throw new NotImplementedException();
    }
}
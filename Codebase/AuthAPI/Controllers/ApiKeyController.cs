using cliph.Models;
using cliph.Models.Http;
using cliph.Models.User;
using cliph.Services.ApiKeyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cliph.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Authorize]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyService _apiKeyService;

    public ApiKeyController(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
    }
    
    [HttpGet]
    public async Task<JsonResult> GetApiKey()
    {
        try
        {
            if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                return new JsonResult(new Response(false, "Unable to fetch the API key for the requested user"));
            
            if (string.IsNullOrWhiteSpace(authHeaderValue))
                return new JsonResult(new Response(false, "Authentication token not found"));

            var apiKey = await _apiKeyService.GetApiKey(authHeaderValue.ToString().Replace("Bearer ", ""));

            return new JsonResult(new Response<ApiKey>(true, apiKey, "Retrieved API key successfully"));

        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
    
    [HttpPut]
    public async Task<JsonResult> InvalidateApiKey()
    {
        try
        {
            if (Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
            {
                if (string.IsNullOrWhiteSpace(authHeaderValue))
                    return new JsonResult(new Response(false, "Authentication token not found"));

                var apiKey = await _apiKeyService.InvalidateApiKey(authHeaderValue.ToString().Replace("Bearer ", ""));

                return new JsonResult(new Response<ApiKey>(true, apiKey, "API key invalidated successfully"));
            }
        
            return new JsonResult(new Response(false, "Unable to invalidate API key"));
        }
        catch (Exception e)
        {
            Response.StatusCode = 500;
            return new JsonResult(new Response(false, e.Message));
        }
    }
}
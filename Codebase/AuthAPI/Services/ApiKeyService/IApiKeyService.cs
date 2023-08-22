using cliph.Models;

namespace cliph.Services.ApiKeyService;

public interface IApiKeyService
{
    public Task<ApiKey> InvalidateApiKey(string userJwt);
    public Task<bool> ValidateApiKey(string apiKey);
    public Task<ApiKey> GetApiKey(string userJwt);
}
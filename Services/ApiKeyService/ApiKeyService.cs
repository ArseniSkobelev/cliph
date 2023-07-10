using System.IdentityModel.Tokens.Jwt;
using cliph.Library;
using cliph.Models;
using cliph.Models.User;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cliph.Services.ApiKeyService;

class ApiKeyService : IApiKeyService
{
    private readonly IConfiguration _configuration;

    public ApiKeyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<ApiKey> InvalidateApiKey(string userJwt)
    {
        using (var db = new Database(
                   _configuration.GetValue<String>("Database:ConnectionString") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup"),
                   _configuration.GetValue<String>("Database:Name") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup")
               ))
        {
            var newApiKey = Guid.NewGuid();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(userJwt);
            
            string? userId = token.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

            var apiKey = new ApiKey
            {
                Value = newApiKey.ToString(),
                CreatedAt = DateTime.Now
            };

            var userFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userId));
            var userUpdateBuilt = Builders<AdminUser>.Update.Set(doc => doc.ApiKey, apiKey);
            
            var userUpdate = await db.GetCollection<AdminUser>("users").UpdateOneAsync(userFilter, userUpdateBuilt);
            if (!userUpdate.IsAcknowledged || userUpdate.ModifiedCount < 1)
                throw new Exception("Unable to fetch API key for the provided user");

            return apiKey;
        }
    }

    public Task<bool> ValidateApiKey(string apiKey)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiKey> GetApiKey(string userJwt)
    {
        using (var db = new Database(
                   _configuration.GetValue<String>("Database:ConnectionString") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup"),
                   _configuration.GetValue<String>("Database:Name") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup")
               ))
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(userJwt);
            
            string? userId = token.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

            var userFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userId));
            
            var existingUser = await db.GetCollection<AdminUser>("users").Find(userFilter).FirstOrDefaultAsync();
            if (existingUser.ApiKey == null)
                throw new Exception("Unable to fetch API key for the provided user");

            return existingUser.ApiKey;
        }
    }
}
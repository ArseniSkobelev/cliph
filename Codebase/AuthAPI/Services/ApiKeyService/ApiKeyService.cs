using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using cliph.Library;
using cliph.Models;
using cliph.Models.User;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cliph.Services.ApiKeyService;

public class ApiKeyService : IApiKeyService
{
    private readonly IConfiguration _configuration;

    public ApiKeyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<ApiKey> InvalidateApiKey(string userJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
            
        var apiKey = new ApiKey
        {
            Value = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.Now
        };

        Claim userIdClaim = Jwt.RetrieveClaimByClaimType(userJwt, ClaimType.UserId);

        var userFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userIdClaim.Value));
        var userUpdate = Builders<AdminUser>.Update.Set(doc => doc.ApiKey, apiKey);
            
        var updatedUser = await db.GetCollection<AdminUser>("users").UpdateOneAsync(userFilter, userUpdate);
        
        if (!updatedUser.IsAcknowledged || updatedUser.ModifiedCount < 1)
            throw new Exception("Unable to invalidate the users API key");

        return apiKey;
    }

    public async Task<bool> ValidateApiKey(string apiKey)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
        
        if (string.IsNullOrWhiteSpace(apiKey))
            return false;
            
        var adminFilter = Builders<AdminUser>.Filter.Eq(doc => doc.ApiKey!.Value, apiKey);

        AdminUser admin;

        try
        {
            admin = await db.GetCollection<AdminUser>("users").Find(adminFilter).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw new Exception("Unable to verify administrator authentication key");
        }

        return admin != null;
    }

    public async Task<ApiKey> GetApiKey(string userJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
        
        Claim userIdClaim = Jwt.RetrieveClaimByClaimType(userJwt, ClaimType.UserId);

        var userFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userIdClaim.Value));

        AdminUser existingUser;

        try
        {
            existingUser = await db.GetCollection<AdminUser>("users").Find(userFilter).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw new Exception("Unable to fetch API key for the provided user");
        }
        
        if (existingUser.ApiKey == null)
            throw new Exception("Unable to fetch API key for the provided user");

        return existingUser.ApiKey;
    }
}
using System.Security.Claims;
using cliph.Library;
using cliph.Models.Requests;
using cliph.Models.Responses;
using cliph.Models.User;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cliph.Services.UserService;

class UserService : IUserService
{
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<User> UpdateUser(User updatedUserData, string userJwt)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteUser(string userJwt)
    {
        throw new NotImplementedException();
    }

    public async Task<AdminUser> GetAdminUserData(string userJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        Claim userIdClaim = Jwt.RetrieveClaimByClaimType(userJwt, "user_id");

        var userFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userIdClaim.Value));
            
        var existingUser = await db.GetCollection<AdminUser>("users").Find(userFilter).FirstOrDefaultAsync();
        if (existingUser == null)
            throw new Exception("No user found with the provided authentication data");

        return existingUser;
    }

    public async Task<ManagedUser> GetManagedUserData(string userJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        Claim userIdClaim = Jwt.RetrieveClaimByClaimType(userJwt, "user_id");

        var userFilter = Builders<ManagedUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userIdClaim.Value));

        var existingUser = await db.GetCollection<ManagedUser>("users").Find(userFilter).FirstOrDefaultAsync();
        if (existingUser == null)
            throw new Exception("No user found with the provided authentication data");

        return existingUser;
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using cliph.Library;
using cliph.Models;
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
    
    public async Task<UserResponse> CreateUser(UserRequest newUserData)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
        
        var existingUserFilterBuilder = Builders<User>.Filter;
        var existingUserFilter =
            existingUserFilterBuilder.Eq(doc => doc.Email, newUserData.Email) &
            existingUserFilterBuilder.Eq(doc => doc.UserType, UserType.Admin);

        var existingUser = await db.GetCollection<User>("users").Find(existingUserFilter).FirstOrDefaultAsync();
        if (existingUser != null)
            throw new Exception("User with the provided username/email already exists");

        var newUser = UserLib.CreateUser(newUserData, UserType.Admin, _configuration, null);

        await db.GetCollection<User>("users").InsertOneAsync(newUser.User);

        return newUser;
    }

    public async Task<UserResponse> CreateUser(UserRequest newUserData, string adminApiKey)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
        
        var existingUserFilterBuilder = Builders<User>.Filter;
        var existingUserFilter =
            existingUserFilterBuilder.Eq(doc => doc.Email, newUserData.Email) &
            existingUserFilterBuilder.Eq(doc => doc.UserType, UserType.Regular);

        var existingUser = await db.GetCollection<User>("users").Find(existingUserFilter).FirstOrDefaultAsync();
        if (existingUser != null)
            throw new Exception("User with the provided username/email already exists");

        var newUser = UserLib.CreateUser(newUserData, UserType.Regular, _configuration, adminApiKey);

        await db.GetCollection<User>("users").InsertOneAsync(newUser.User);

        return newUser;
    }

    public Task<User> UpdateUser(User updatedUserData, string userJwt)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteUser(string userJwt)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserData(string userJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        Claim userIdClaim = Jwt.RetrieveClaimByClaimType(userJwt, "user_id");

        var userFilter = Builders<User>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userIdClaim.Value));
            
        var existingUser = await db.GetCollection<User>("users").Find(userFilter).FirstOrDefaultAsync();
        if (existingUser == null)
            throw new Exception("No user found with the provided authentication data");

        return existingUser;
    }
}
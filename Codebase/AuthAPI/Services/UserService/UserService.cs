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

    public Task<User> UpdateUser(User updatedUserData, string userJwt)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAdminUser(string adminJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        var adminUserId = Jwt.RetrieveClaimByClaimType(adminJwt, ClaimType.UserId).Value;
        var adminUserDeletionFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(adminUserId));

        // TODO: Delete all managed users related to this admin account.
        //          change the DeleteOneAsync call to FindOneAndDeleteAsync to retrieve
        //          admin api key for further querying?

        var deleteResult = await db.GetCollection<AdminUser>("users").DeleteOneAsync(adminUserDeletionFilter);

        if (deleteResult.DeletedCount != 1)
            throw new Exception("Unable to delete your administrator account.");
    }

    public async Task DeleteManagedUser(string adminJwt, string email)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        var adminUserObjectId =
            Jwt.RetrieveClaimByClaimType(adminJwt, ClaimType.UserId).Value;

        var adminUserFilter =
            Builders<AdminUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(adminUserObjectId));

        AdminUser adminUser =
            await db.GetCollection<AdminUser>("users").Find(adminUserFilter).FirstOrDefaultAsync();

        var managedUserFilterBuilder = Builders<ManagedUser>.Filter;
        var managedUserFilter =
            managedUserFilterBuilder.Eq(doc => doc.Email, email) &
            managedUserFilterBuilder.Eq(doc => doc.AdminApiKey, adminUser.ApiKey!.Value);

        var userDeletionResult =
            await db.GetCollection<ManagedUser>("users").DeleteOneAsync(managedUserFilter);

        if (userDeletionResult.DeletedCount == 0)
            throw new Exception("Unable to delete the user with the provided email");
    }

    public async Task DeleteManagedUser(string userJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        var managedUserObjectId =
            Jwt.RetrieveClaimByClaimType(userJwt, ClaimType.UserId).Value;

        var managedUserFilter = Builders<ManagedUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(managedUserObjectId));

        var deletionResult = await db.GetCollection<ManagedUser>("users").DeleteOneAsync(managedUserFilter);

        if (deletionResult.DeletedCount == 0)
            throw new Exception("Unable to delete your user account");
    }

    public async Task<AdminUser> GetAdminUserData(string userJwt)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        Claim userIdClaim = Jwt.RetrieveClaimByClaimType(userJwt, ClaimType.UserId);

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

        Claim userIdClaim = Jwt.RetrieveClaimByClaimType(userJwt, ClaimType.UserId);

        var userFilter = Builders<ManagedUser>.Filter.Eq(doc => doc.Id, ObjectId.Parse(userIdClaim.Value));

        var existingUser = await db.GetCollection<ManagedUser>("users").Find(userFilter).FirstOrDefaultAsync();
        if (existingUser == null)
            throw new Exception("No user found with the provided authentication data");

        return existingUser;
    }
}
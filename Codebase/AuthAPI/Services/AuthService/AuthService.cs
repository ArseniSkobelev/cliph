using System.Security.Claims;
using cliph.Library;
using cliph.Models.Requests;
using cliph.Models.Responses;
using cliph.Models.User;
using cliph.Services.ApiKeyService;
using MongoDB.Driver;

namespace cliph.Services.AuthService;

class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IApiKeyService _apiKeyService;

    public AuthService(IConfiguration configuration, IApiKeyService apiKeyService)
    {
        _configuration = configuration;
        _apiKeyService = apiKeyService;
    }
    
    public async Task<UserResponse> CreateSession(UserRequest existingUserData)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
        
        var userFilterBuilder = Builders<AdminUser>.Filter;
        var userFilter = 
            userFilterBuilder.Eq(doc => doc.Email, existingUserData.Email) &
            userFilterBuilder.Eq(doc => doc.UserType, UserType.Admin);
            
        var existingUser = await db.GetCollection<AdminUser>("users").Find(userFilter).FirstOrDefaultAsync();

        if (existingUser == null)
            throw new NullReferenceException("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");
            
        if (!BCrypt.Net.BCrypt.Verify(existingUserData.Password, existingUser.PasswordHash))
            throw new Exception("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");

        List<Claim> claims;

        try
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, existingUser.Email),
                new Claim("UserId", existingUser.Id.ToString()),
            };
        }
        catch (Exception)
        {
            throw new Exception("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");
        }
        
        var jwt = Jwt.CreateJwt(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "JWT:Secret"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "JWT:Issuer"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "JWT:Audience"),
            60.0,
            claims
        );
            
        return new UserResponse
        {
            Jwt = jwt,
            User = existingUser
        };
    }

    public async Task<UserResponse> CreateSession(UserRequest existingUserData, string adminApiKey)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
        
        if(!await _apiKeyService.ValidateApiKey(adminApiKey))
            throw new Exception("The provided API key does not exist");

        var userFilterBuilder = Builders<ManagedUser>.Filter;
        var userFilter = 
            userFilterBuilder.Eq(doc => doc.Email, existingUserData.Email) &
            userFilterBuilder.Eq(doc => doc.UserType, UserType.Regular);

        ManagedUser existingUser;

        try
        {
            existingUser = await db.GetCollection<ManagedUser>("users").Find(userFilter).FirstOrDefaultAsync();

        }
        catch (Exception e)
        {
            throw new NullReferenceException(
                "Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");
        }    
        
        if (existingUser == null)
            throw new NullReferenceException("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");
            
        if (!BCrypt.Net.BCrypt.Verify(existingUserData.Password, existingUser.PasswordHash))
            throw new Exception("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");

        List<Claim> claims;
            
        try
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, existingUser.Email),
                new Claim("UserId", existingUser.Id.ToString()),
            };
        }
        catch (Exception)
        {
            throw new Exception("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");
        }
        
        var jwt = Jwt.CreateJwt(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "JWT:Secret"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "JWT:Issuer"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "JWT:Audience"),
            60.0,
            claims
        );
            
        return new UserResponse
        {
            Jwt = jwt,
            User = existingUser
        };
    }

    public async Task<UserResponse> CreateAccount(UserRequest newUserData)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );
        
        var existingUserFilterBuilder = Builders<AdminUser>.Filter;
        var existingUserFilter =
            existingUserFilterBuilder.Eq(doc => doc.Email, newUserData.Email) &
            existingUserFilterBuilder.Eq(doc => doc.UserType, UserType.Admin);

        var existingUser = await db.GetCollection<AdminUser>("users").Find(existingUserFilter).FirstOrDefaultAsync();
        if (existingUser != null)
            throw new Exception("User with the provided username/email already exists");

        var newUser = UserLib.CreateUser(newUserData, UserType.Admin, _configuration, null);

        await db.GetCollection<AdminUser>("users").InsertOneAsync((AdminUser)newUser.User! ?? throw new
            InvalidOperationException
            ("Unable to create a new managed user"));

        return newUser;
    }

    public async Task<UserResponse> CreateAccount(UserRequest newUserData, string adminApiKey)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        var existingAdminFilterBuilder = Builders<AdminUser>.Filter;
        var existingAdminFilter =
            existingAdminFilterBuilder.Eq(doc => doc.ApiKey.Value, adminApiKey);
        
        var existingAdmin = await db.GetCollection<AdminUser>("users").Find(existingAdminFilter).FirstOrDefaultAsync();
        if (existingAdmin == null)
            throw new Exception("Unknown API key provided. Please contact your system administrator for further assistance!");

        var existingUserFilterBuilder = Builders<User>.Filter;
        var existingUserFilter =
            existingUserFilterBuilder.Eq(doc => doc.Email, newUserData.Email) &
            existingUserFilterBuilder.Eq(doc => doc.UserType, UserType.Regular);

        var existingUser = await db.GetCollection<User>("users").Find(existingUserFilter).FirstOrDefaultAsync();
        if (existingUser != null)
            throw new Exception("User with the provided username/email already exists");

        var newUser = UserLib.CreateUser(newUserData, UserType.Regular, _configuration, adminApiKey);

        await db.GetCollection<User>("users").InsertOneAsync(newUser.User ?? throw new InvalidOperationException("Unable to create a new managed user"));

        return newUser;
    }
}
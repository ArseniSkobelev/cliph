using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using cliph.Library;
using cliph.Models.Requests;
using cliph.Models.Responses;
using cliph.Models.User;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cliph.Services.AuthService;

class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<UserResponse> CreateSession(UserRequest existingUserData)
    {
        using (var db = new Database(
                   _configuration.GetValue<String>("Database:ConnectionString") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup"),
                   _configuration.GetValue<String>("Database:Name") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup")
               ))
        {
            var userFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Email, existingUserData.Email);
            
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
                    new Claim("user_id", existingUser.Id.ToString()),
                };
            }
            catch (Exception e)
            {
                throw new Exception("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");
            }
        
            var jwt = Jwt.CreateJwt(
                _configuration.GetValue<String>("JWT:Secret") ??
                throw new InvalidOperationException("Unable to retrieve configuration setup"),
                _configuration.GetValue<String>("JWT:Issuer") ??
                throw new InvalidOperationException("Unable to retrieve configuration setup"),
                _configuration.GetValue<String>("JWT:Audience") ??
                throw new InvalidOperationException("Unable to retrieve configuration setup"),
                60.0,
                claims
            );
            
            return new UserResponse
            {
                Jwt = jwt,
                User = existingUser
            };
        }
    }

    public async Task<UserResponse> CreateSession(UserRequest existingUserData, string adminApiKey)
    {
        using (var db = new Database(
                   _configuration.GetValue<String>("Database:ConnectionString") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup"),
                   _configuration.GetValue<String>("Database:Name") ??
                   throw new InvalidOperationException("Unable to retrieve configuration setup")
               ))
        {
            if (string.IsNullOrWhiteSpace(adminApiKey))
                throw new Exception("Could not retrieve the provided administrator API token.");
            
            var adminFilter = Builders<AdminUser>.Filter.Eq(doc => doc.ApiKey.Value, adminApiKey);

            var admin = await db.GetCollection<AdminUser>("users").Find(adminFilter).FirstOrDefaultAsync();
            
            if (admin == null)
                throw new Exception("Unable to find an administrator account with the provided API key. Please try again later or contact your system administrator.");

            var userFilterBuilder = Builders<ManagedUser>.Filter;
            var userFilter = 
                userFilterBuilder.Eq(doc => doc.Email, existingUserData.Email) &
                userFilterBuilder.Eq(doc => doc.UserType, UserType.Regular);

            var existingUser = await db.GetCollection<ManagedUser>("users").Find(userFilter).FirstOrDefaultAsync();
            
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
                    new Claim("user_id", existingUser.Id.ToString()),
                };
            }
            catch (Exception e)
            {
                throw new Exception("Sorry, we can't find an account with this email and password combo. Please try again or create a new account.");
            }
        
            var jwt = Jwt.CreateJwt(
                _configuration.GetValue<String>("JWT:Secret") ??
                throw new InvalidOperationException("Unable to retrieve configuration setup"),
                _configuration.GetValue<String>("JWT:Issuer") ??
                throw new InvalidOperationException("Unable to retrieve configuration setup"),
                _configuration.GetValue<String>("JWT:Audience") ??
                throw new InvalidOperationException("Unable to retrieve configuration setup"),
                60.0,
                claims
            );
            
            return new UserResponse
            {
                Jwt = jwt,
                User = existingUser
            };
        }
    }
}
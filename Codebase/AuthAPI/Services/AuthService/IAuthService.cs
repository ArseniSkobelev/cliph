using cliph.Models;
using cliph.Models.Requests;
using cliph.Models.Responses;

namespace cliph.Services.AuthService;

public interface IAuthService
{
    public Task<UserResponse> CreateSession(UserRequest existingUserData);
    public Task<UserResponse> CreateSession(UserRequest existingUserData, string adminApiKey);
    public Task<UserResponse> CreateAccount(UserRequest existingUserData);
    public Task<UserResponse> CreateAccount(UserRequest existingUserData, string adminApiKey);
}
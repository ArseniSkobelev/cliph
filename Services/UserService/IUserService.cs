using cliph.Models.Requests;
using cliph.Models.Responses;
using cliph.Models.User;

namespace cliph.Services.UserService;

public interface IUserService
{
    public Task<UserResponse> CreateUser(UserRequest newUserData);
    public Task<UserResponse> CreateUser(UserRequest newUserData, string adminApiKey);
    public Task<User> UpdateUser(User updatedUserData, string userJwt);
    public Task<User> DeleteUser(string userJwt);
    public Task<User> GetUserData(string userJwt);
}
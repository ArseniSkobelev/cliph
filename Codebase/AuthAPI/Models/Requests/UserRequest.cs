using Newtonsoft.Json;

namespace cliph.Models.Requests;

public class UserRequest
{
    [JsonProperty("email")] public string? Email { get; set; }
    
    [JsonProperty("password")] public string? Password { get; set; }
}
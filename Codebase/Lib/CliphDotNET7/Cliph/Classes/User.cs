using System.Text.Json.Serialization;

namespace Cliph.Classes;

public class User
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
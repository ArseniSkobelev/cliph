using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace cliph.Models.Responses;

public class UserResponse
{
    [BsonIgnore]
    [JsonProperty("jwt")]
    public string Jwt { get; set; }
    
    [BsonIgnore]
    [JsonProperty("user")]
    public User.User User { get; set; }
}
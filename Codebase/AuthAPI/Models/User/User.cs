using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace cliph.Models.User;

public class User
{
    [BsonElement("_id")]
    [JsonProperty("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [BsonElement("user_type")]
    [JsonProperty("user_type")]
    public UserType UserType { get; set; }

    [BsonElement("username")]
    [JsonProperty("username")]
    public string? Username { get; set; }

    [BsonElement("email")]
    [JsonProperty("email")]
    public string Email { get; set; } = "user@default.com";

    [BsonElement("password_hash")]
    [System.Text.Json.Serialization.JsonIgnore]
    public string PasswordHash { get; set; } = "changeme";

    [BsonElement("first_name")]
    [JsonProperty("first_name")]
    public string? FirstName { get; set; }

    [BsonElement("last_name")]
    [JsonProperty("last_name")]
    public string? LastName { get; set; }
}

public class AdminUser : User
{
    [BsonElement("api_key")]
    [JsonProperty("api_key")]
    public ApiKey? ApiKey { get; set; }
}

public class ManagedUser : User
{
    [BsonElement("admin_api_key")]
    [JsonProperty("admin_api_key")]
    [System.Text.Json.Serialization.JsonIgnore]
    public string? AdminApiKey { get; set; }
}
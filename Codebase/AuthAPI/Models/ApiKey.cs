using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace cliph.Models;

public class ApiKey
{
    [BsonElement("value")]
    [JsonProperty("value")] 
    public string? Value { get; set; }
    
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement("created_at")]
    [JsonProperty("created_at")] 
    public DateTime CreatedAt { get; set; }
}
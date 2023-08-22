using MongoDB.Bson.Serialization.Attributes;

namespace cliph.Models.User;

public enum UserType
{
    [BsonElement("admin")]
    Admin,
    [BsonElement("regular")]
    Regular
}
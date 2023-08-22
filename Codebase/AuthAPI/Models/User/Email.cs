using System.Text.Json.Serialization;

namespace Cliph.AuthAPI.Models.User
{
    public class Email
    {
        [JsonPropertyName("email")]
        public string EmailAddress { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users;

public class CreateUserJson
{
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }
    [JsonPropertyName("user")]
    public required UserJson User { get; set; }

    public class UserJson
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("surname")]
        public required string Surname { get; set; }
        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }
}
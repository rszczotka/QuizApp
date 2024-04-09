using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users;

public class LoginJson
{
    [JsonPropertyName("login")]
    public required string Login { get; set; }
    [JsonPropertyName("password")]
    public required string Password { get; set; }
}

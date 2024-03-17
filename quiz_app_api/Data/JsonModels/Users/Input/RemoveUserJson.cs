using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users.Input;

public class RemoveUserJson
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }
}

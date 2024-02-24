using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users;

public class GetAllUsersJson
{
    [JsonPropertyName("api_key")]
    public string ApiKey { get; set; }
}

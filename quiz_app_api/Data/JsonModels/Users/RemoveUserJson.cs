using Newtonsoft.Json;

namespace quiz_app_api.Data.JsonModels.Users;

public class RemoveUserJson
{
    [JsonProperty("user_id")]
    public int UserId { get; set; }
    [JsonProperty("api_key")]
    public string? ApiKey { get; set; }
}

using Newtonsoft.Json;

namespace quiz_app_api.Data.JsonModels.Users;

public class GetAllUsersJson
{
    [JsonProperty("api_key")]
    public string? ApiKey { get; set; }
}

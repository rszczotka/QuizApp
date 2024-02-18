using Newtonsoft.Json;

namespace quiz_app_api.Data.JsonModels;

public class SuccessJson
{
    [JsonProperty("success")]
    public bool Success { get; set; }
}

using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels;

public class SuccessJson
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
}

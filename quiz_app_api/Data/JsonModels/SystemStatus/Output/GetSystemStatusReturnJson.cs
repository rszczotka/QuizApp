using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.SystemStatus.Output;

public class GetSystemStatusReturnJson
{
    [JsonPropertyName("status")]
    public int Status { get; set; }
}

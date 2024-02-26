using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.SystemStatus;

public class UpdateSystemStatus
{
	[JsonPropertyName("api_key")]
	public required string ApiKey { get; set; }
	[JsonPropertyName("status")]
	public int Status { get; set; }
}

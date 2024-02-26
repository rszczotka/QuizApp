using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.SystemStatus;

public class GetSystemStatusReturnJson
{
	[JsonPropertyName("status")]
	public int Status { get; set; }
}

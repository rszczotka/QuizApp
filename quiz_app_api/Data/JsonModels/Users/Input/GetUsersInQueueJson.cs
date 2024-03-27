using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Users.Input;

public class GetUsersInQueueJson
{
	[JsonPropertyName("api_key")]
	public string ApiKey { get; set; }
}

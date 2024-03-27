using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.UserAnswers.Input;

public class GetUserAnswersJson
{
	[JsonPropertyName("api_key")]
	public string ApiKey { get; set; }
	[JsonPropertyName("user_id")]
	public int UserId { get; set; }
}

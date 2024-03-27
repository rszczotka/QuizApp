using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.UserAnswers.Input;

public class CreateUserAnswerJson
{
	[JsonPropertyName("question_id")]
	public int QuestionId { get; set; }
	[JsonPropertyName("chosen_option")]
	public int ChosenOption { get; set; }
	[JsonPropertyName("api_key")]
	public string ApiKey { get; set; }
}

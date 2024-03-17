using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Questions.Input;

public class GetAllQuestionsJson
{
	[JsonPropertyName("api_key")]
	public string ApiKey { get; set; }
}

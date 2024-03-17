using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Questions.Output;

public class GetAllQuestionsReturnJson
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("text")]
	public string Text { get; set; }
	[JsonPropertyName("options")]
	public string[] Options { get; set; }
	[JsonPropertyName("correct_answer")]
	public int CorrectAnswer { get; set; }
	[JsonPropertyName("available_time")]
	public int AvailabelTime { get; set; }
}

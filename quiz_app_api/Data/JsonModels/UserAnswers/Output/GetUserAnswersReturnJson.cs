using quiz_app_api.Data.JsonModels.Questions.Output;
using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.UserAnswers.Output;

public class GetUserAnswersReturnJson
{
	[JsonPropertyName("question")]
	public GetAllQuestionsReturnJson Question { get; set; }
	[JsonPropertyName("chosen_option")]
	public int ChosenOption { get; set; }
}

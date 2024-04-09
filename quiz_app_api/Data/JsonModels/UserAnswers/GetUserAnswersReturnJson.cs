using quiz_app_api.Data.JsonModels.Questions;
using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.UserAnswers;

public class GetUserAnswersReturnJson
{
    [JsonPropertyName("question")]
    public required GetAllQuestionsReturnJson Question { get; set; }
    [JsonPropertyName("chosen_option")]
    public required int ChosenOption { get; set; }
}

using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.UserAnswers;

public class CreateUserAnswerJson
{
    [JsonPropertyName("question_id")]
    public required int QuestionId { get; set; }
    [JsonPropertyName("chosen_option")]
    public required int ChosenOption { get; set; }
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }
}

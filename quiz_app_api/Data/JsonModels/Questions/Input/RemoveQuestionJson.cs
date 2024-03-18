using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Questions.Input;

public class RemoveQuestionJson
{

    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }

    [JsonPropertyName("question_id")]
    public required int QuestionId { get; set; }

}

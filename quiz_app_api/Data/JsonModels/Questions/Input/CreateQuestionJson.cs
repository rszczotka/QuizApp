using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Questions.Input;

public class CreateQuestionJson
{
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }
    [JsonPropertyName("question")]
    public required QuestionJson Question { get; set; }
    
    public class QuestionJson
    {
        [JsonPropertyName("text")]
        public required string Text { get; set; }
        [JsonPropertyName("options")]
        public required string[] Options { get; set; }
        [JsonPropertyName("correct_answer")]
        public int CorrectAnswer { get; set; }
        [JsonPropertyName("available_time")]
        public int AvailableTime { get; set; }
    }

}

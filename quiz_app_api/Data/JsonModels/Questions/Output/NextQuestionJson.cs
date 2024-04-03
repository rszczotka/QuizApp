using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Questions.Output;

public class NextQuestionJson
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("options")]
    public required string[] Options { get; set; }

    [JsonPropertyName("available_time")]
    public required int AvailableTime { get; set; }

}

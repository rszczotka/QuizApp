using System.Text.Json.Serialization;

namespace quiz_app_api.Data.JsonModels.Questions.Input;

public class GetNextQuestionJson
{
    
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }

}

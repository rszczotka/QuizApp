namespace quiz_app_api.Data.Entities;

public class QuestionEntity
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required string Opions { get; set; }
    public int CorrectAnswer { get; set; }
    public int AvailableTime { get; set; }
}

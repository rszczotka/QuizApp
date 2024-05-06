namespace quiz_app_api.Data.Entities;

public class UserAnswerEntity
{
    public int Id { get; set; }
    public required UserEntity User { get; set; }
    public required QuestionEntity Question { get; set; }
    public int ChosenOption { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

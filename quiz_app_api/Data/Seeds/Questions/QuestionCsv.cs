using CsvHelper.Configuration.Attributes;

namespace quiz_app_api.Data.Questions;

public class QuestionCsv
{
	[Index(0)]
	public required string Text { get; set; }
	[Index(1)]
	public required string CorrectAnswer { get; set; }
	[Index(2)]
	public required string IncorrectAnswer1 { get; set; }
	[Index(3)]
	public required string IncorrectAnswer2 { get; set; }
	[Index(4)]
	public required string IncorrectAnswer3 { get; set; }
	[Index(5)]
	public required string TimeInSeconds { get; set; }
}

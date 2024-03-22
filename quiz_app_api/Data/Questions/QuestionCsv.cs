using CsvHelper.Configuration.Attributes;

namespace quiz_app_api.Data.Questions;

public class QuestionCsv
{
	[Index(0)]
	public string Text { get; set; }
	[Index(1)]
	public string CorrectAnswer { get; set; }
	[Index(2)]
	public string IncorrectAnswer1 { get; set; }
	[Index(3)]
	public string IncorrectAnswer2 { get; set; }
	[Index(4)]
	public string IncorrectAnswer3 { get; set; }
	[Index(5)]
	public string TimeInSeconds { get; set; }
}

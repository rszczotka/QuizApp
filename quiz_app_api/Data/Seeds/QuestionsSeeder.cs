using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;
using CsvHelper;
using System.Globalization;
using quiz_app_api.Data.Questions;

namespace quiz_app_api.Data.Seeds;

public class QuestionsSeeder
{
	private static readonly string QuestionFile = "Data/Questions/questions.csv";

	public static void Seed(ModelBuilder modelBuilder)
	{
		var questionsCsv = new List<QuestionCsv>();
		
		using(var reader = new StreamReader(QuestionFile))
		using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			questionsCsv = csv.GetRecords<QuestionCsv>().ToList();
		}

		var questions = questionsCsv.Select((x, position) => new QuestionEntity
		{
			Id = position + 1,
			Text = x.Text,
			// the answers are not in a random order yet
			Options = [x.CorrectAnswer, x.IncorrectAnswer1, x.IncorrectAnswer2, x.IncorrectAnswer3],
			CorrectAnswer = 0,
			AvailableTime = x.TimeInSeconds != "" ? int.Parse(x.TimeInSeconds) : 25
		}).ToList();

		// making order of the answers random
		/*var random = new Random();

		foreach(var question in questions)
		{
			var answers = question.Options.ToList();
			var correctAnswer = random.Next(0, 4);

			question.CorrectAnswer = correctAnswer;
			ShuffleAnswers(answers, correctAnswer);
		}*/

		modelBuilder.Entity<QuestionEntity>().HasData(questions);
	}

	// TODO: shuffle the answers
	private static void ShuffleAnswers(List<string> answers, int correctAnswer)
	{
		throw new NotImplementedException();
	}
}

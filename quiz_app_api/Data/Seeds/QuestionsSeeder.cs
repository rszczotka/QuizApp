using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;
using CsvHelper;
using System.Globalization;
using quiz_app_api.Data.Questions;

namespace quiz_app_api.Data.Seeds;

public class QuestionsSeeder
{
	private static readonly string QuestionsFile = "Data/Seeds/Questions/questions.csv";

	public static void Seed(ModelBuilder modelBuilder)
	{
		var questionsCsv = new List<QuestionCsv>();
		
		using(var reader = new StreamReader(QuestionsFile))
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
		var random = new Random();

		foreach(var question in questions)
		{
			var answers = question.Options.ToList();
			var correctAnswerInex = random.Next(0, 4);

			question.CorrectAnswer = correctAnswerInex;
			ShuffleAnswers(answers, correctAnswerInex);

			question.Options = answers.ToArray();
		}

		modelBuilder.Entity<QuestionEntity>().HasData(questions);
	}

	private static void ShuffleAnswers(List<string> answers, int correctAnswerIndex)
	{
		var remainingAnswers = answers.ToList();

		answers[correctAnswerIndex] = remainingAnswers[0];
		remainingAnswers.RemoveAt(0);

		for(int i = 0; i < answers.Count; i++)
		{
			if(i == correctAnswerIndex) continue;

			var random = new Random();
			var randomIndex = random.Next(remainingAnswers.Count);

			answers[i] = remainingAnswers[randomIndex];
			remainingAnswers.RemoveAt(randomIndex);
		}
	}
}

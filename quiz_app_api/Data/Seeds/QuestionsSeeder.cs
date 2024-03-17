using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;

namespace quiz_app_api.Data.Seeds;

public class QuestionsSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<QuestionEntity>().HasData(new List<QuestionEntity>
		{
			new()
			{
				Id = 1,
				Text = "W którym roku wybuchła II Wojna Światowa?",
				Options = ["1945", "1918", "1939", "1980"],
				CorrectAnswer = 2,
				AvailableTime = 60
			},
			new()
			{
				Id = 2,
				Text = "Ile lat żyją bobry",
				Options = ["30", "5", "10", "27"],
				CorrectAnswer = 0,
				AvailableTime = 30
			},
			new()
			{
				Id = 3,
				Text = "W którym roku doszło do masakry na Placu Niebiańskiego Spokoju?",
				Options = ["1939", "2006", "1989", "Nie było żadnej masakry"],
				CorrectAnswer = 3,
				AvailableTime = 10
			}
		});
	}
}

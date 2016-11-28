using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using MealsToday.Data;

namespace MealsToday.Helpers
{
	public static class CSVLoader
	{
		private static User parseUserRow(string row)
		{
			var user = new User();

			string[] dataParts = row.Split(';');

			user.UserId = Convert.ToInt32(dataParts[0]);
			user.Username = dataParts[1];
			user.FirstName = dataParts[2];
			user.LastName = dataParts[3];

#warning Finish user role loading and parsing
			//user.Roles

			return user;
		}

		private static Allergen parseAllergenRow(string row)
		{
			return new Allergen();
		}

		private static Meal parseMealRow(string row)
		{
			return new Meal();
		}

		public static List<User> LoadUserData(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("File not found", filePath);
			}

			string[] allLines = File.ReadAllLines(filePath);

			for (int i = 1; i < allLines.Length; i++)
			{
				var user = parseUserRow(allLines[i]);
			}

			return new List<User>();
		}

		public static List<Allergen> LoadAllergenData(string filePath)
		{
			return new List<Allergen>();
		}

		public static List<Meal> LoadMealData(string filePath)
		{
			return new List<Meal>();
		}
	}

}
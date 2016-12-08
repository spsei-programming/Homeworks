using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealsToday.Data;
using MealsToday.Enums;
using MealsToday.Helpers;

namespace MealsToday
{
	class Program
	{
		static void Main(string[] args)
		{
			Initialize();

			LogIn();

			Console.WriteLine("Current user is:");
			Console.WriteLine(Context.CurrentUser.FullName);
		}

		public static void Initialize()
		{
			var users = CSVLoader.LoadUserData(
					CSVLoader.GetCSVPath(FileTypes.Users));

			var allergens = CSVLoader.LoadAllergenData(
					CSVLoader.GetCSVPath(FileTypes.Allergens));

			Context.AllUsers = users;
			Context.Allergens = allergens;

			var meals = CSVLoader.LoadMealData(
					CSVLoader.GetCSVPath(FileTypes.Meals));

			Context.Meals = meals;

			foreach (KeyValuePair<int, Allergen> a in Context.MapOfAllergens)
			{
				Console.WriteLine($"Allergen with key {a.Key} is {a.Value.Name}");
			}

		}

		public static void LogIn()
		{
			Console.Write("Username:");
			var username = Console.ReadLine();
			Console.Write("Password:");
			var password = Console.ReadLine();

			bool userFound = false;

			foreach (User user in Context.AllUsers)
			{
				if (user.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) 
					&& user.Password == password)
				{
					Context.CurrentUser = user;
					userFound = true;
					break;
				}
			}

			if (!userFound)
			{
				throw new UnauthorizedAccessException("Wrong password or username.");
			}
		}
	}
}

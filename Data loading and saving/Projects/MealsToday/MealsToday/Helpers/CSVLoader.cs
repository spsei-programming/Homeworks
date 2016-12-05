using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using MealsToday.Data;
using MealsToday.Enums;

namespace MealsToday.Helpers
{
	public static class CSVLoader
	{
		#region Private methods

		private static UserRoles mapUserRole(string role)
		{
			//User; MealsAdministrator; OrdersAdministrator
			//User; ReportingAdmin
			//PowerUser
			switch (role.ToLower())
			{
				case "poweruser":
				case "user": 
					return UserRoles.User;
				case "mealsadministrator":
					return UserRoles.MealsAdministrator;
				case "ordersadministrator":
					return UserRoles.OrdersAdministrator;
				case "reportingadmin":
					return UserRoles.ReportingAdministrator;
			}
			throw new ArgumentOutOfRangeException(nameof(role), $"Role {role} out of range.");
		}
		
		private static User parseUserRow(string row)
		{
			var user = new User();

			string[] dataParts = row.Split(';');

			user.UserId = Convert.ToInt32(dataParts[0]);
			user.Username = dataParts[1];
			user.FirstName = dataParts[2];
			user.LastName = dataParts[3];
			user.Password = dataParts[4];

			if (dataParts.Length == 6)
			{
				user.Roles.Add(mapUserRole(dataParts[5]));
			}
			else
			{
				for (int i = 5; i < dataParts.Length; i++)
				{
					user.Roles.Add(mapUserRole(dataParts[i].Trim('"')));
				}
			}
			

			return user;
		}

		private static Allergen parseAllergenRow(string row)
		{
			var dataParts = row.Split(';');
			var name = dataParts[1].Replace('"', ' ').Trim();
			
			return new Allergen(Convert.ToInt32(dataParts[0]), name);
		}

		private static Meal parseMealRow(string row)
		{
			return new Meal();
		}

		public static string GetCSVPath(FileTypes fileType)
		{
			var baseDirectory = ConfigurationManager.AppSettings["CSVFilesFolder"];

			switch (fileType)
			{
				case FileTypes.Meals:
					return Path.Combine(baseDirectory, "Meals.csv");
				case FileTypes.Users:
					return Path.Combine(baseDirectory, "Users.csv");
				case FileTypes.Orders:
					return Path.Combine(baseDirectory, "Orders.csv");
				case FileTypes.Allergens:
					return Path.Combine(baseDirectory, "Allergens.csv");
				default:
					throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
			}


			return (baseDirectory + "\\" + fileType);
		}

		#endregion

		public static List<User> LoadUserData(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("File not found", filePath);
			}
			var users = new List<User>();
			string[] allLines = File.ReadAllLines(filePath, Encoding.UTF8);

			for (int i = 1; i < allLines.Length; i++)
			{
				var user = parseUserRow(allLines[i]);
				users.Add(user);
			}

			return users;
		}

		public static List<Allergen> LoadAllergenData(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("File not found", filePath);
			}
			var allergens = new List<Allergen>();
			string[] allLines = File.ReadAllLines(filePath, Encoding.UTF8);

			for (int i = 1; i < allLines.Length; i++)
			{
				var allergen = parseAllergenRow(allLines[i]);
				allergens.Add(allergen);
			}

			return allergens;
		}

		public static List<Meal> LoadMealData(string filePath)
		{
			return new List<Meal>();
		}
	}
}
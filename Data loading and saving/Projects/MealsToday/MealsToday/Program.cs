using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealsToday.Data;
using MealsToday.Helpers;

namespace MealsToday
{
	class Program
	{
		static void Main(string[] args)
		{
			var users = CSVLoader.LoadUserData(@"Users.csv");

			users.ForEach(x => Console.WriteLine(x.FullName));

			Context.AllUsers = users;

			LogIn();

			Console.WriteLine("Current user is:");
			Console.WriteLine(Context.CurrentUser.FullName);
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

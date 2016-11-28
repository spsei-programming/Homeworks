using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealsToday.Helpers;

namespace MealsToday
{
	class Program
	{
		static void Main(string[] args)
		{
			CSVLoader.LoadUserData(@"C:\Git\spsei-programming\Homeworks\Data loading and saving\CSV files\Users.csv");
		}
	}
}

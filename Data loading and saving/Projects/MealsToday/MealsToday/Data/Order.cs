using System;
using System.Data;

namespace MealsToday.Data
{
	public class Order
	{
		public int OrderId { get; set; }

		public DateTime OrderDate { get; set; }
		
		public User User { get; set; }

		public Meal Meal { get; set; }

		public byte Amount { get; set; } = 1;
	}
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealsToday.MVC.Providers.Assets;
using MealsToday.MVC.Providers.Base;
using MealsToday.MVC.Providers.Data;
using MealsToday.MVC.Providers.DBModels;

namespace MealsToday.MVC.Providers
{
	public class UsersProvider : GenericDatabaseProvider<User>
	{
		protected override User SelectMapFunction(SqlDataReader reader)
		{
			var user = new User();
			user.Id = (int)reader["UserId"];
			user.FirstName = reader["FirstName"].ToString();
			user.LastName = reader["LastName"].ToString();
			user.Email = reader["Email"].ToString();
			user.UserTypeCode = reader["UserTypeCode"].ToString();

			return user;
		}

		public User Get(int id)
		{
			var sql = $"{SQL.User_BaseSelect} where userId = {id}";

			return ExecuteSingleQuery(sql, SelectMapFunction);
		}

		public List<User> GetList(string condition = "")
		{
			var sql = $"{SQL.User_BaseSelect} where {condition}";

			return ExecuteQuery(sql, SelectMapFunction);
		}

		public User Insert(User user)
		{
			var sql = string.Format(SQL.User_Insert, user.UserTypeCode, user.FirstName, user.LastName, user.Email);

			var model = ExecuteSingleQuery(sql, reader => new UserInserted()
			{
				Email = reader["Email"].ToString(),
				UserId = (int)reader["UserId"]
			});

			return Get(model.UserId);
		}

		public User Update(User user)
		{
			var connectionString = ConfigurationManager.ConnectionStrings["MealsDB"].ConnectionString;

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlCommand cmd = conn.CreateCommand();

				cmd.CommandText = "dbo.UpdateUser";
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("firstName", user.FirstName);
				cmd.Parameters.AddWithValue("lastName", user.LastName);
				cmd.Parameters.AddWithValue("email", user.Email);
				cmd.Parameters.AddWithValue("usertypecode", user.UserTypeCode);
				cmd.Parameters.AddWithValue("userid", user.Id);

				conn.Open();

				cmd.ExecuteNonQuery();

				conn.Close();
			}

			return Get(user.Id);
			
		}

		public void Delete(int id)
		{
			var sql = string.Format(SQL.User_Delete, id);

			ExecNonQuery(sql);
		}

		
	}
}

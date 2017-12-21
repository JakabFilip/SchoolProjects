using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace jakabfilip.aspify.com
{
	public class SqlProvider
	{
		/// <summary>
		/// Prepares, opens and executes SqlConnection 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="connectionString">ConnectionString</param>
		/// <param name="query">Actual Query</param>
		/// <param name="cmd">Your Code to handle response from Sql Query</param>
		/// <returns>Returns output of your cmd's code</returns>
		public T ExecuteQuery<T>(
			string connectionString,
			string query,
			Func<SqlCommand, T> cmd
		)
		{
			T result;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection)
				{
					CommandType = CommandType.Text
				};
				connection.Open();

				result = cmd(command);

				connection.Close();
			}

			return result;
		}

		/// <summary>
		/// Executes Sql Query without expecting any results
		/// </summary>
		/// <param name="connectionString">Connection string used for sql connection</param>
		/// <param name="query">Query being executed on sql db</param>
		public void ExecuteNonQuery(
			string connectionString,
			string query
		)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection)
				{
					CommandType = CommandType.Text
				};

				connection.Open();

				command.ExecuteNonQuery();

				connection.Close();
			}
		}
	}
}
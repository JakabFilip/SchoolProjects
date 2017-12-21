using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using jakabfilip.aspify.com.Models;
using jakabfilip.aspify.com;

namespace jakabfilip.aspify.com.Controllers
{
	[CustomFitler]
	public class LoginController : Controller
	{
		// GET: Login
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(FormCollection userLoginCollection)
		{

#warning Implement Authorization 

			

			if (userLoginCollection["UserName"] == admin.UserName
			    && userLoginCollection["Password"] == admin.Password)
			{
				ViewData["CurrentUser"] = admin;
				Response.Cookies.Add(new HttpCookie("currentUser", admin.UserId.ToString()));
				return RedirectToAction("Index", "Home");
			}
			ViewData["AuthenticationErrorMessage"] = "username/password are incorrect";
			return RedirectToAction("Index");
		}

		public ActionResult Register(FormCollection userRegisterCollection)
		{
			// SQL Injection validation
			foreach (object input in userRegisterCollection)
			{
				if (IsSqlInjection((string) input))
				{
					ViewData["ErrorMessage"] = "Atleast one input contains malicous value... Registration fialed";

					return RedirectToAction("Index");
				}
			}

			string query =
				string.Format(App_LocalResources.SqlCommands.Insert,
					"Users", "UserId,Username,Password,Email,UserType",
					$"'{Guid.NewGuid()}','{userRegisterCollection["UserName"]}','{userRegisterCollection["Password"]}','{userRegisterCollection["Email"]}','Guest'");
			string connectionString =
				System.Web.Configuration
				.WebConfigurationManager
				.ConnectionStrings["MainDb"]
				.ConnectionString;

			SqlProvider provider = new SqlProvider();

			provider.ExecuteNonQuery(connectionString,
				query);

			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Used to check, if User Exist
		/// </summary>
		/// <param name="userId">UserId, used as filter</param>
		/// <returns>Returns null if no User was found, otherwise retruns matched User</returns>
		private User Authorize(Guid userId)
		{
			SqlProvider provider = new SqlProvider();

			string connectionString =
				System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;

			string query; // TODO: Add Select statement form resource

			User user = provider.ExecuteQuery<User>(
				connectionString, query, (cmd) =>
			{
				// TODO: Return Matched User by Guid
			});
		}

		private bool IsSqlInjection(string value)
		{
			return Regex.IsMatch(value, @"'\w?;");
		}
	}
}
 
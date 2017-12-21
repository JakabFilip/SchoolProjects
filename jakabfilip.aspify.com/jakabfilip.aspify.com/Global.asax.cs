using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using jakabfilip.aspify.com.Models;

namespace jakabfilip.aspify.com
{
	public class CustomFitlerAttribute : ActionFilterAttribute
	{
		private void CheckLanguage(ActionExecutingContext filterContext)
		{
			if (!(filterContext.RouteData.Values["lang"] is string routeLanguage)) return;

			Thread.CurrentThread.CurrentCulture = new CultureInfo(routeLanguage);
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(routeLanguage);
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			CheckLanguage(filterContext);
		}
	}

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			// GlobalFilters.Filters.Add(new CustomFitlerAttribute());
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			RouteConfig.RegisterRoutes(RouteTable.Routes);

			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}

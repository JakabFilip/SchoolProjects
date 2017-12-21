using System.Web;
using System.Web.Optimization;

namespace jakabfilip.aspify.com
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-*"));

			//bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
			//	"~/Scripts/bootstrap.js",
			//	"~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/SemanticJs").Include(
				"~/Scripts/SemanticJs/semantic.min.js"));

			bundles.Add(new StyleBundle("~/Content/SemanticCss").Include(
				"~/Content/SemanticCss/semantic.min.css"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/Site.css"));
		}
	}
}

using System.Web;
using System.Web.Optimization;

namespace IPS_Web_Final
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts//bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/core-style.css",
                      "~/Content/style.css",
                      "~/Content/responsive.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsplugins").Include(
                   "~/Scripts/popper.min.js",
                   "~/Scripts/plugins.js",
                   "~/Scripts/active.js"
                   ));
        }
    }
}

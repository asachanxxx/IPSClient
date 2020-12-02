using System.Web;
using System.Web.Optimization;

namespace IPS_Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/themescripts").Include(
                     "~/theams/jquery.js",
                     "~/theams/bootstrap.js",
                     "~/theams/imagesloaded.js",
                     "~/theams/masonry.js",
                     "~/theams/script.js"));


            bundles.Add(new StyleBundle("~/themeCSS").Include(
                   "~/theams/bootstrap.css",
                   "~/theams/font-awesome.min.css",
                   "~/theams/responsive.css",
                   "~/theams/style.css"
                   ));
        }
    }
}

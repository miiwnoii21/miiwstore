using System.Web;
using System.Web.Optimization;

namespace MiiwStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Default Bundle

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #endregion

            #region Pluton Bundle

            bundles.Add(new ScriptBundle("~/bundles/pluton/jquery").Include(
                            "~/Scripts/libs/jquery-{version}.js",
                            "~/Scripts/libs/jquery.mixitup.js"));

            bundles.Add(new ScriptBundle("~/bundles/pluton/jquery/plugin").Include(
                            "~/Scripts/libs/jquery.bxslider.js",
                            "~/Scripts/libs/jquery.cslider.js",
                            "~/Scripts/libs/jquery.placeholder.js",
                            "~/Scripts/libs/jquery.inview.js"
                            ));

            bundles.Add(new ScriptBundle("~/bundles/pluton/app").Include(
                            "~/Scripts/apps/pluton-app.js"));

            bundles.Add(new ScriptBundle("~/bundles/pluton/angular").Include(
                             "~/Scripts/libs/angular.*"));

            bundles.Add(new ScriptBundle("~/bundles/pluton/modernizr").Include(
                            "~/Scripts/libs/modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/pluton/bootstrap").Include(
                            "~/Scripts/libs/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/pluton/css").Include(
                            "~/Content/bootstrap.css",
                            "~/Content/bootstrap-responsive.css",
                            "~/Content/Site.css",
                            "~/Content/pluton.css",
                            "~/Content/jquery.cslider.css",
                            "~/Content/jquery.bxslider.css",
                            "~/Content/animate.css"));

            #endregion

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}

using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Contetn/boostrap-reboot.css",
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/jquery.mask.min.js",
                "~/Scripts/jquery-3.2.1.js",
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/knockout.js"));
        }
    }
}

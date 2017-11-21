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
                "~/Content/bootstrap-select.css",
                "~/Content/font-awesome.css",
                "~/Content/jquery.businessHours.css",
                "~/Content/jquery.timepicker.min.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/jquery-3.2.1.js",
                "~/Scripts/jquery.mask.min.js",
                "~/Scripts/jquery.businessHours.min.js",
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap-select.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.timepicker.js",
                "~/Scripts/knockout.js"));
        }
    }
}

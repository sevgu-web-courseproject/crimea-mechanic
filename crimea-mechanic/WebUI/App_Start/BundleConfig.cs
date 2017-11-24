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
                "~/Content/jquery.timepicker.min.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-3.2.1.js",
                "~/Scripts/jquery.mask.min.js",
                "~/Scripts/jquery.timepicker.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-select.js",
                "~/Scripts/knockout.js"));

            bundles.Add(new ScriptBundle("~/Scripts/registrationUserVM").Include(
                "~/Scripts/ViewModels/registrationUserVM.js",
                "~/Scripts/Helpers/ajaxHelper.js"));

            bundles.Add(new ScriptBundle("~/Scripts/registrationCarServiceVM").Include(
                "~/Scripts/ViewModels/registrationCarServiceVM.js",
                "~/Scripts/Helpers/ajaxHelper.js"));

            bundles.Add(new ScriptBundle("~/Scripts/homePage").Include(
                "~/Scripts/ViewModels/homePageVM.js",
                "~/Scripts/Helpers/ajaxHelper.js"));
        }
    }
}

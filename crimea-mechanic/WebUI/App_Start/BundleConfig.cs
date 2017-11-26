﻿using System.Web.Optimization;

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
                "~/Content/iziToast.min.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-3.2.1.js",
                "~/Scripts/jquery.mask.min.js",
                "~/Scripts/jquery.timepicker.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/common-onready.js",
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-select.js",
                "~/Scripts/iziToast.min.js",
                "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/knockout").Include(
                "~/Scripts/knockout.js",
                "~/Scripts/knockout-mapping.js"));

            bundles.Add(new ScriptBundle("~/Scripts/helpers").Include(
                "~/Scripts/Helpers/ajaxHelper.js",
                "~/Scripts/Helpers/notificationHelper.js",
                "~/Scripts/Helpers/timeHelper.js"));

            bundles.Add(new ScriptBundle("~/Scripts/registrationUserVM").Include(
                "~/Scripts/ViewModels/registrationUserVM.js"));

            bundles.Add(new ScriptBundle("~/Scripts/registrationCarServiceVM").Include(
                "~/Scripts/ViewModels/registrationCarServiceVM.js"));

            bundles.Add(new ScriptBundle("~/Scripts/homePage").Include(
                "~/Scripts/ViewModels/homePageVM.js"));

            bundles.Add(new ScriptBundle("~/Scripts/carServiceCard").Include(
                "~/Scripts/ViewModels/carServiceCardVM.js"));

            bundles.Add(new ScriptBundle("~/Scripts/registrationRequestsVM").Include(
                "~/Scripts/ViewModels/registrationRequestsVM.js"));

            bundles.Add(new ScriptBundle("~/Scripts/registrationRequestCardVM").Include(
                "~/Scripts/ViewModels/registrationRequestCardVM.js"));
        }
    }
}

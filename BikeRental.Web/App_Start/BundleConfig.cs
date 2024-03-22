using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BikeRental.Web
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/bootstrap.min.css").Include("~/Vendor/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/bundles/bootstrap-datepicker.css").Include("~/Vendor/css/bootstrap-datepicker.css"));
            bundles.Add(new StyleBundle("~/bundles/jquery.fancybox.min.css").Include("~/Vendor/css/jquery.fancybox.min.css"));
            bundles.Add(new StyleBundle("~/bundles/owl.carousel.min.css").Include("~/Vendor/css/owl.carousel.min.css"));
            bundles.Add(new StyleBundle("~/bundles/owl.theme.default.min.css").Include("~/Vendor/css/owl.theme.default.min.css"));
            bundles.Add(new StyleBundle("~/bundles/flaticon.css").Include("~/Vendor/fonts/flaticon/font/flaticon.css"));
            bundles.Add(new StyleBundle("~/bundles/aos.css").Include("~/Vendor/css/aos.css"));
            bundles.Add(new StyleBundle("~/bundles/style.css").Include("~/Vendor/css/style.css"));
            bundles.Add(new StyleBundle("~/bundles/fonts/icomoon/style.css").Include("~/Vendor/fonts/icomoon/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-3.3.1.min.js").Include("~/Vendor/js/jquery-3.3.1.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/popper.min.js").Include("~/Vendor/js/popper.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap.min.js").Include("~/Vendor/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/owl.carousel.min.js").Include("~/Vendor/js/owl.carousel.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.sticky.js").Include("~/Vendor/js/jquery.sticky.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.waypoints.min.js").Include("~/Vendor/js/jquery.waypoints.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.animateNumber.min.js").Include("~/Vendor/js/jquery.animateNumber.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.fancybox.min.js").Include("~/Vendor/js/jquery.fancybox.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.easing.1.3.js").Include("~/Vendor/js/jquery.easing.1.3.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker.min.js").Include("~/Vendor/js/bootstrap-datepicker.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/aos.js").Include("~/Vendor/js/aos.js"));
            bundles.Add(new ScriptBundle("~/bundles/main.js").Include("~/Vendor/js/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/login.js").Include("~/Vendor/js/login.js"));

        }
    }
}
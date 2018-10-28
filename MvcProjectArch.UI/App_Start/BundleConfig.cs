using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MvcProjectArch.UI.App_Start
{
    public class BundleConfig
    {
       
            // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
            public static void RegisterBundles(BundleCollection bundles)
            {
                //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                //            "~/Scripts/jquery-{version}.js"));

                //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                //            "~/Scripts/jquery.validate*"));

                // Use the development version of Modernizr to develop with and learn from. Then, when you're
                // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
                bundles.Add(new ScriptBundle("~/Content/MinimizeJs/MainScript").Include(
                            "~/Content/js/bootstrap.min.js",
                            "~/Content/js/AdminLTE/app.js"));


                bundles.Add(new StyleBundle("~/Content/MinimizeCss/MainCss").Include(
                          "~/Content/css/bootstrap.min.css",
                          "~/Content/css/font-awesome.min.css",
                          "~/Content/css/ionicons.min.css",
                          "~/Content/css/AdminLTE.css"));
            
        }
    }
}
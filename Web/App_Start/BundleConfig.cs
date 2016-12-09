using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Plugin/jQuery/jquery-2.1.4.js",
                        "~/Scripts/Plugin/jquery.tmpl.min.js",
                        "~/Scripts/Common.js",
                        "~/Scripts/UIHelp.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Plugin/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/Plugin/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/all-skins.1.0.0.css",
                      "~/Content/common.css"));
        }
    }
}
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
                        "~/Scripts/Plugin/jquery.tmpl.min.js",
                        "~/Scripts/Common.js",
                        "~/Scripts/UIHelp.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Plugin/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/AdminLTE").Include(
                        "~/Scripts/Plugin/bootstrap/js/bootstrap.js"
                        , "~/Scripts/Plugin/dist/js/app.js"
                        , "~/Scripts/Plugin/plugins/jQuery/jquery-2.2.3.min.js"
                        , "~/Scripts/Plugin/plugins/iCheck/icheck.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/common.css"
                      , "~/Scripts/Plugin/bootstrap/css/bootstrap.css"
                      , "~/Scripts/Plugin/bootstrap/font-awesome-4.5.0.min.css"
                      , "~/Scripts/Plugin/bootstrap/ionicons-2.0.1.min.css"
                      , "~/Scripts/Plugin/dist/css/AdminLTE.css"
                      , "~/Scripts/Plugin/dist/css/skins/_all-skins.css"
                      , "~/Scripts/Plugin/plugins/iCheck/square/blue.css"));
        }
    }
}
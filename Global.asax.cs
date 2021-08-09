using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Tunierverwaltung.Controller;

namespace Tunierverwaltung
{
    public class Global : HttpApplication
    {
        private static FussballspielerController _fussballspielerController;

        private static TunierController _tunierController;

        private static MannschaftController _mannschaftController;
        public static FussballspielerController FussballspielerController { get => _fussballspielerController; set => _fussballspielerController = value; }
        public static MannschaftController MannschaftController { get => _mannschaftController; set => _mannschaftController = value; }
        public static TunierController TunierController { get => _tunierController; set => _tunierController = value; }

        public Global() : base()
        {
            FussballspielerController = new FussballspielerController();
            MannschaftController = new MannschaftController();
            TunierController = new TunierController();
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }
}
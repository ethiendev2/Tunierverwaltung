﻿using System;
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

        public static FussballspielerController FussballspielerController { get => _fussballspielerController; set => _fussballspielerController = value; }

        public Global() : base()
        {
            FussballspielerController = new FussballspielerController();
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }
}
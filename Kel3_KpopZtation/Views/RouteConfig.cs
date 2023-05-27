using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Kel3_KpopZtation.Views {
    public class RouteConfig {
        public static void RegisterRoute ( RouteCollection routes ) {
            routes.MapPageRoute (
                routeName: "Default",
                routeUrl: "",
                physicalFile: "~/index.aspx"
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation {
    public partial class index : System.Web.UI.Page {
        NavigationController nc = new NavigationController();
        protected void Page_Load ( object sender, EventArgs e ) {
            nc.Redirect("/Views/Home.aspx");
        }
    }
}
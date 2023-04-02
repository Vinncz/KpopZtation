using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation.Views {
    public partial class EditArtist : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockIfNotAdmin(AuthController.ExtractCustomer(), "EditArtist.aspx");
            /* END TEMPLATE */


        }

        protected void BTSubmit_Click(object sender, EventArgs e) {
            Console.WriteLine("");
        }

        protected void BTCancel_Click(object sender, EventArgs e) {
            Response.Redirect("Home.aspx");
        }
    }
}
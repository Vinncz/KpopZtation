using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Views {
    public partial class DeleteAccount : System.Web.UI.Page {
        ElementController ec = new ElementController();
        NavigationController nc = new NavigationController();
        
        protected void Page_Load ( object sender, EventArgs e ) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockWhenNotSignedIn(AuthController.ExtractCustomer());
            /* END TEMPLATE */


        }

        protected void _GBTProceed_Click ( object sender, EventArgs e ) {
            // delete account
            CustomerController.Delete(AuthController.ExtractCustomer().CustomerID);
        }

        protected void _GBTCancel_Click ( object sender, EventArgs e ) {
            // redirect  ke home
            nc.Redirect("./Home.aspx");
        }
    }
}
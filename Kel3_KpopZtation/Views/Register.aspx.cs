using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Factories;

namespace Kel3_KpopZtation.Views {
    public partial class Register : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockWhenSignedIn(AuthController.ExtractCustomer());
            /* END TEMPLATE */

            /* Set ErrorMessage Section ke invisible utk pertama kali */
            ec.Invis(LBMessage);

            /* Quality of Life Improvement :: AutoFocus */
            TBName.Focus();
        }

        protected void BTSubmit_Click(object sender, EventArgs e) {

        }
    }
}
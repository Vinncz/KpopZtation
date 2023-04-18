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
    public partial class UpdateProfile : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();
        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockWhenNotSignedIn(AuthController.ExtractCustomer());
            /* END TEMPLATE */

            ec.Invis(LBMessage);
        }

        protected void BTSubmit_Click(object sender, EventArgs e) {
            string Name = _GTBName.Text;
            string Email = _GTBEmail.Text;
            string Sex = _GDLGender.SelectedValue;
            string Address = _GTBAddress.Text;
            string Password = _GTBPassword.Text;

            // (Customer c, List<string> ErrorMsgs) = AuthController.Register(Name, Email, Sex, Address, Password);
        }

        protected void BTCancel_Click(object sender, EventArgs e) {
            Response.Redirect("Home.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Controllers.PageController;

namespace Kel3_KpopZtation.Views {

    public partial class Login : System.Web.UI.Page {

        /* 
         * [ DONE ] Functional by March 28th, 2023 by Kevin
         * [ DONE ] Refactored in March 29th, 2023 by Kevin
         * Login Page Criteria - Page 6
         * 
         * (X) Only accessible to Guests.
         * (X) Able to respond with appropriate error messages for each violation.
         * (X) Has a [Remember Me] checkbox.
         * (X) The [Remember Me Cookie] has to own an expiry date.
         */
        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        protected void Page_Load(object sender, EventArgs e) {

            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockWhenSignedIn(AuthController.ExtractCustomer());
            /* END TEMPLATE */

            /* Everything else goes here. */
            /* Set ErrorMessage Section ke invisible utk pertama kali */
            ec.Invis(LBMessage);

            /* Quality of Life Improvement :: AutoFocus */
            TBEmail.Focus();
        }

        protected void BTSubmit_Click(object sender, EventArgs e) {
            string email = TBEmail.Text;
            string password = TBPassword.Text;
            bool rememberme = CBRemember.Checked;

            (Object Dump, List<string> ErrorMsgs) = AuthController.Authenticate(email, password, rememberme);

            if (Dump == null && !ErrorMsgs.Contains("Password cannot be empty!")) ErrorMsgs.Add("Incorrect password.");
            if (ErrorMsgs.Count > 0) {
                LBMessage.Visible = true;
                LBMessage.Text = "<svg width='20' height='20' viewBox='0 0 20 20' fill='none' xmlns='http://www.w3.org/2000/svg'><g clip-path='url(#clip0_635_179)'><rect width='20' height='20' rx='10' fill='#7F2835'/><path d='M10 4.16699V11.2503' stroke='#FFBFD1' stroke-width='1.25'stroke-linecap='round'/><rect x='9.16663' y='13.333' width='1.66667' height='1.66667' rx='0.833333'fill='#FFBFD1'/></g><defs><clipPath id='clip0_635_179'><rect width='20' height='20' fill='white'/></clipPath></defs></svg> <br />";
                LBMessage.Text += ErrorMsgs.Aggregate((current, next) => current + "<br />" + next);

            } else if (Dump != null) { 
                nc.BlockWhenSignedIn(Dump);
            }
        }
    }
}
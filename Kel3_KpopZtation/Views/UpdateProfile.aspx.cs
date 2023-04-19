using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views {
    public partial class UpdateProfile : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();
        public static Customer This = null;
        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockWhenNotSignedIn(AuthController.ExtractCustomer());
            /* END TEMPLATE */

            This = AuthController.ExtractCustomer();

            ec.Invis(LBMessage);
            if ( !IsPostBack ) {
                RefreshPage();
            }
        }
        protected void BTSubmit_Click(object sender, EventArgs e) {
            string Name = _GTBName.Text;
            string Email = _GTBEmail.Text;
            string Sex = _GDLGender.SelectedValue;
            string Address = _GTBAddress.Text;
            string Password = _GTBPassword.Text;

            ec.Vis(LBMessage);
            (bool UpdatedSuccessfully, List<string> ErrorMsgs) = CustomerController.Update(This.CustomerID, Name, Email, Sex, Address, Password);
            if ( !UpdatedSuccessfully || ErrorMsgs.Count > 0 ) {
                LBMessage.Text = "<svg width='20' height='20' viewBox='0 0 20 20' fill='none' xmlns='http://www.w3.org/2000/svg'><g clip-path='url(#clip0_635_179)'><rect width='20' height='20' rx='10' fill='#7F2835'/><path d='M10 4.16699V11.2503' stroke='#FFBFD1' stroke-width='1.25'stroke-linecap='round'/><rect x='9.16663' y='13.333' width='1.66667' height='1.66667' rx='0.833333'fill='#FFBFD1'/></g><defs><clipPath id='clip0_635_179'><rect width='20' height='20' fill='white'/></clipPath></defs></svg> <br />";
                LBMessage.Text += ErrorMsgs.Aggregate((current, next) => current + "<br />" + next);

            } else {
                LBMessage.Text = "Updated Profile Successfully!";
                RefreshPage();

            }
        }
        private void RefreshPage() {
            This = CustomerController.Find(AuthController.ExtractCustomer().CustomerID);

            _GTBName.Text = This.CustomerName;
            _GTBEmail.Text = This.CustomerEmail;
            _GDLGender.SelectedValue = This.CustomerGender;
            _GTBAddress.Text = This.CustomerAddress;
            _GTBPassword.Text = This.CustomerPassword;
        }
        protected void BTCancel_Click(object sender, EventArgs e) {
            Response.Redirect("Home.aspx");
        }
    }
}
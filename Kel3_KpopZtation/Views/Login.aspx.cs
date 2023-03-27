using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views
{
    public partial class Login : System.Web.UI.Page
    {
        private static LoginController c = new LoginController();
        protected void Page_Load(object sender, EventArgs e) {
            if (Request.Cookies.Get("AuthInfo").Value != null || Session["AuthInfo"] != null) {
                Response.Redirect("./Home.aspx");
            }
        }

        protected void BTSubmit_Click(object sender, EventArgs e) {
            string email = TBEmail.Text;
            string password = TBPassword.Text;
            bool rememberme = CBRemember.Checked;

            bool EmailAndPasswordValid = c.ValidateEmailAndPassword(email, password);
            if (EmailAndPasswordValid) {
                Customer cus = c.SearchMatchingCustomer(email, password);
                if (cus != null) {
                    if (rememberme) {
                        HttpCookie cookie = new HttpCookie("AuthInfo") {
                            Value = Convert.ToString(cus.CustomerID),
                            Expires = DateTime.UtcNow.AddDays(30)
                        };
                        Request.Cookies.Set(cookie);
                    }

                    Session["AuthInfo"] = cus;
                    Response.Redirect("./Home.aspx");

                } else {
                    LBMessage.Text = "There are no ascosiated account with the credentials you provided.";
                }

            } else {
                LBMessage.Text = "Your input is in incorrect format.";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kel3_KpopZtation.Views {
    public partial class Home : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            MemberOnly.Visible = false;
            AdminOrAbove.Visible = false;
            MemberOrAbove.Visible = false;

            HttpCookie AuthCookie = Request.Cookies["AuthInfo"];

            if (AuthCookie != null && AuthCookie.Values["CustomerRole"] == "Admin") {
                /* Set various element visible for which only an admin can see */
                AdminOrAbove.Visible = true;
                MemberOrAbove.Visible = true;
                GuestOnly.Visible = false;

            } else if (AuthCookie != null && AuthCookie.Values["CustomerRole"] == "Buyer") {
                /* Set various element visible for which a buyer is allowed to access. */
                MemberOrAbove.Visible = true;
                MemberOnly.Visible = true;
                GuestOnly.Visible = false;
            }
        }

        private void SetVisible () {

        }
    }
}
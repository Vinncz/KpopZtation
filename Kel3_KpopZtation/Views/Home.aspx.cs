using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views {
    public partial class Home : System.Web.UI.Page {

        /* 
         * [ IN PROGRESS ] March 31st, 2023 by Kevin
         * Home Page Criteria - Page 7
         * 
         * ( ) Able to show every registered artists.
         * ( ) Able to redirect users to Artist Detail Page when a certain action is done.
         * ( ) Has to have Custom Visibility Button:
         *     ( ) Insert Button, redirect Admin to Insert Artist Page,
         *         & Invisible to non-Admin
         *     ( ) Update Button, redirect Admin to Update Artist Page,
         *         & invisible to non-Admin
         *     ( ) Delete Button, delte the selected artist,
         *         & invisible to non-Admin
         */

        ElementController ec = new ElementController();
        protected void Page_Load(object sender, EventArgs e) {
            AuthController.MakeSessionFromCookie();
            Customer c = (Customer) Session["AuthInfo"];

            /* Make elements, which only reserved for [LOGGED IN] client, invisible */

            ec.PrepareVisibility(Page, c);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views {
    public partial class MasterPage : System.Web.UI.MasterPage {
        const int CookieSetbackValue = -14;

        /* 
         * [ IN PROGRESS ] March 30th, 2023 by Kevin
         * Navigation Bar Criteria - Page 7
         * 
         * If user is NOT LOGGED IN, show:
         * (X) Home
         * (X) Sign In
         * (X) Register
         * 
         * If user is a BUYER, show:
         * (X) Home
         * ( ) Cart
         * ( ) Transaction
         * ( ) Update Profile
         * (X) Log Out
         * 
         * If user is an ADMIN, show:
         * (X) Home
         * ( ) Transaction -> which diverge to [Transaction Report Page]
         * ( ) Update Profile
         * (X) Log Out
         * 
         * NOTE:
         * The [LOG OUT] button DESTROYS all cookie variables.
         */

        ElementController ec = new ElementController();
        protected void Page_Load(object sender, EventArgs e) {
            
            /* 
             * WARNING:
             * ALWAYS sync cookie and session!
             * 
             * Data yang digunakan adalah SELALU data yang terdapat pada Session.
             * Auth Cookie hanya digunakan untuk initialize data kedalam Session, setiap kali session baru dibuat.
             * 
             * Biasakanlah utk memasukkan data yang disimpan session kedalam Variable Holder.
             */ 
            AuthController.MakeSessionFromCookie();
            Customer c = (Customer) Session["AuthInfo"];

            /* Apabila client LOGGED IN kedalam accountnya */
            ec.PrepareVisibility(Page, c);
        }

        protected void BTLogOut_Click(object sender, EventArgs e) {
            AuthController.SignOut();
        }
    }
}
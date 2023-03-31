using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Repositories;
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

        protected void Page_Load(object sender, EventArgs e) {
            // HttpCookie AuthCookie = Request.Cookies["AuthInfo"];
            AuthController.MakeSessionFromCookie();
            Customer c = (Customer)Session["AuthInfo"];

            /* 
             * Cek apakah terdapat Session atau Cookie yang tersimpan.
             * Jika YA, gaperlu login. 
             * 
             * Basically, dibawah ini adalah function untuk refresh Cookie atau Session.
             * Setiap kali lu mau sync Session dengan Cookie, href ke Login.aspx/?FwdTo=<Halaman Yang Lu Mau Refresh>
             * 
             * Jika terdapat Cookie AuthInfo
             */
            if ( c != null ) {
                /* Sync dengan Session */
                // CookieController.SyncCookieWithSession();

                /* 
                * Jika dia minta diforward ke halaman lain setelah login berhasil, gunakan parameter ?FwdTo=... 
                *      
                *      Contoh:
                *      localhost:4039/Views/Login.aspx?FwdTo=Home.apsx
                * 
                * Ini akan selalu forward ke Home.aspx sbg default dan fallback.
                * 
                * Jangan lupa di halaman parameternya harus CEK DULU apakah cookie
                * yg dipegang punya permission thdp data yang diakses.
                */
                if (Request.QueryString["FwdTo"] != null) {
                    Response.Redirect("./" + Request.QueryString["FwdTo"]);
                }

                /* Redirect fallback */
                Response.Redirect("./Home.aspx");
            }

            /* Everything else goes here. */

            /* Set ErrorMessage Section ke invisible utk pertama kali */
            LBMessage.Visible = false;

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
                if (Request.QueryString["FwdTo"] != null) {
                    Response.Redirect("./" + Request.QueryString["FwdTo"]);
                }

                /* Redirect fallback */
                Response.Redirect("./Home.aspx");
            }
        }
    }
}
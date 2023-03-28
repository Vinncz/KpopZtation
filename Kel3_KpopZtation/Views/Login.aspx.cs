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

namespace Kel3_KpopZtation.Views
{
    public partial class Login : System.Web.UI.Page {

        /* Global variable to easily set a cookie's lifetime in days */
        const int CookieLifespan = 7;

        /* 
         * [ DONE ] 28 March 2023 by Kevin
         * Login Page Criteria - Page 6
         * 
         * (X) Only accessible to Guests.
         * (X) Able to respond with appropriate error messages for each violation.
         * (X) Has a [Remember Me] checkbox.
         * (X) The [Remember Me Cookie] has to own an expiry date.
         */
        protected void Page_Load(object sender, EventArgs e) {

            /* 
             * Cek apakah terdapat Session atau Cookie yang tersimpan.
             * Jika YA, gaperlu login. 
             * 
             * Basically, dibawah ini adalah function untuk refresh Cookie atau Session.
             * Setiap kali lu mau sync Session dengan Cookie, href ke Login.aspx/?FwdTo=<Halaman Yang Lu Mau Refresh>
             * 
             * NOTES:
             * Request.Cookies digunakan untuk READ/MODIFY apakah terdapat suatu cookie.
             * Response.Cookies digunakan untuk ASSIGN suatu cookie ke client.
             */
            if (!FormatValidator.IsNull(Request.Cookies["AuthInfo"]) 
                || !FormatValidator.IsNull(Session["AuthInfo"])) {

                /* 
                 * Lengkapin dulu salah satu dari Session atau Cookie yang kosong.
                 * 
                 * Utk assign Cookie ke browser, >> LU GABISA LEMPAR KERJAAN INI KE CONTROLLER.  << 
                 * Makanya kerjain disini.
                 * 
                 * Jika Cookie kosong/expired (Cookie udah pasti null)
                 * Dengan asumsi bahwa data yang disimpan Session adalah BENAR
                 * 
                 * Cookie akan mendapat prioritas lebih dulu untuk dicek.
                 */
                if (FormatValidator.IsNull(Request.Cookies["AuthInfo"])) {
                    Response.Cookies.Add(CookieFactory.MakeCookie("AuthInfo", (Customer) Session["AuthInfo"], CookieLifespan));
                
                /* 
                 * Jika Session kosong, tapi ada Cookie tersimpan --
                 * Dengan asumsi bahwa data yang disimpan Cookie adalah BENAR
                 */
                } else if (FormatValidator.IsNull(Session["AuthInfo"])) {
                    int CustomerID = Convert.ToInt32(Request.Cookies["AuthInfo"].Values["CustomerID"]);
                    Session["AuthInfo"] = CustomerRepo.ExistByID(CustomerID);
                } 

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
                if (!FormatValidator.IsNull(Request.QueryString["FwdTo"])) {
                    Response.Redirect("./" + Request.QueryString["FwdTo"]);
                }

                /* Redirect fallback */
                Response.Redirect("./Home.aspx");
            }

            /* Everything else goes here. */
            
        }

        protected void BTSubmit_Click(object sender, EventArgs e) {
            string email = TBEmail.Text;
            string password = TBPassword.Text;
            bool rememberme = CBRemember.Checked;
            Customer cus = null;

            (bool EmailAndPasswordValid, List<string> ErrorMsgs) = LoginController.ValidateEmailAndPassword(email, password);

            if (EmailAndPasswordValid) {
                cus = CustomerRepo.ExistByEmailAndPassword(email, password);

                /* Jika credentials yang dimasukkan adalah BENAR, dan terdapat account yang terasosiasi dengan email yg diketik */
                if (cus != null) {

                    /* Option [Remember Me] terpilih. Assign Cookie kepada browser client */
                    if (rememberme && FormatValidator.IsNull(Request.Cookies["AuthInfo"])) 
                        Response.Cookies.Add(CookieFactory.MakeCookie("AuthInfo", cus, CookieLifespan));
                    
                    /* Masukin object Customer kedalam Session */
                    Session["AuthInfo"] = cus;

                    /* Redirect client ke tujuan mereka */
                    if (!FormatValidator.IsNull(Request.QueryString["FwdTo"]))
                        Response.Redirect("./" + Request.QueryString["FwdTo"]);
                    
                    /* Redirect fallback */
                    Response.Redirect("./Home.aspx");
                }
            }

            /* Jika sampai sini, maka sudah dipastikan terdapat error pada input. */
            if (cus == null && ErrorMsgs.Count == 0) ErrorMsgs.Add("Incorrect password.");
            LBMessage.Text = ErrorMsgs.Aggregate((current, next) => current + "<br />" + next);
        }
    }
}
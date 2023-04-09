using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Controllers {
    public class NavigationController {

        /* 
         * When a user is logged in, the following method blocks them from accessing the page.
         */
        public void BlockWhenSignedIn (object o) {
            if ( o != null ) {
                /* 
                * Jika dia minta diforward ke halaman lain, gunakan parameter ?FwdTo=... 
                *      
                *      Contoh:
                *      localhost:4039/Views/Login.aspx?FwdTo=Home.apsx
                * 
                * Ini akan selalu forward ke Home.aspx sbg default dan fallback.
                * 
                * Perihal pengecekan dan penghalangan suatu User Role untuk mengakses halaman yang dituju
                * adalah sepenuhnya milik halaman yang bersangkutan.
                */
                if (HttpContext.Current.Request.QueryString["FwdTo"] != null) {
                    HttpContext.Current.Response.Redirect("./" + HttpContext.Current.Request.QueryString["FwdTo"]);
                }

                /* Redirect fallback */
                HttpContext.Current.Response.Redirect("./Home.aspx");
            }
        }

        public void BlockIfNotAdmin (Customer c, string Destination) {
            if ( c == null || c.CustomerRole != "Admin" ) {
                HttpContext.Current.Response.Redirect("Login.aspx?FwdTo=" + Destination);

            }
        }

        public void BlockIfNotBuyer (Customer c, string Destination) {
            if ( c == null || c.CustomerRole != "Buyer" ) {
                HttpContext.Current.Response.Redirect("Login.aspx?FwdTo=" + Destination);

            }
        }
    }
}
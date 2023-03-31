using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public static class CookieController {

        /* Global variable to easily set a cookie's lifetime in days */
        private const int CookieLifespan = 7;
        public const int CookieSetbackValue = -14;

        private static HttpCookie MakeAuthCookie () {
            return CookieFactory.MakeCookie("AuthInfo", (Customer) HttpContext.Current.Session["AuthInfo"], CookieLifespan);
        }

        public static bool CookieAndSessionMatched () {
            Customer c = (Customer) HttpContext.Current.Session["AuthInfo"];

            if (c == null) return false;
            return HttpContext.Current.Request.Cookies["AuthInfo"].Values["CustomerID"] == c.CustomerID.ToString();
        }

        public static bool SyncCookieWithSession () {
            HttpCookie ThisCookie = HttpContext.Current.Request.Cookies["AuthInfo"];

            /* Ketika tidak ada Cookie maupun Session */
            if ( HttpContext.Current.Request.Cookies["AuthInfo"] == null && HttpContext.Current.Session["AuthInfo"] == null ) {
                return false;

            /* Ketika tidak ada salah satu Cookie atau Session, sync. */
            } else if ( HttpContext.Current.Request.Cookies["AuthInfo"] == null || HttpContext.Current.Session["AuthInfo"] == null ) {

                /* 
                 * Lengkapin dulu salah satu dari Session atau Cookie yang kosong.
                 * 
                 * Jika Cookie kosong/expired (Cookie udah pasti null)
                 * Dengan asumsi bahwa data yang disimpan Session adalah BENAR
                 * 
                 * Cookie akan mendapat prioritas lebih dulu untuk dicek.
                 */
                if ( ThisCookie == null && HttpContext.Current.Session["AuthInfo"] != null ) {
                    AssignAuthCookie();

                /* 
                * Jika Session kosong, tapi ada Cookie tersimpan --
                * Dengan asumsi bahwa data yang disimpan Cookie adalah BENAR
                */
                } else if ( HttpContext.Current.Session["AuthInfo"] == null && HttpContext.Current.Request.Cookies["AuthInfo"] != null ) {
                    int CustomerID = Convert.ToInt32(HttpContext.Current.Request.Cookies["AuthInfo"].Values["CustomerID"]);

                    HttpContext.Current.Session["AuthInfo"] = CustomerRepo.ExistByID(CustomerID);

                }

            }

            /* Jika sampai sini, return apakah session dan cookie sama-sama tidak kosong? */
            return HttpContext.Current.Session["AuthInfo"] != null && HttpContext.Current.Request.Cookies["AuthInfo"] != null;
        }

        public static void AssignAuthCookie () {
            HttpContext.Current.Response.Cookies.Add(MakeAuthCookie());
        }

        public static void AssignSession (Customer c) {
            HttpContext.Current.Session["AuthInfo"] = c;
        }
    }
}
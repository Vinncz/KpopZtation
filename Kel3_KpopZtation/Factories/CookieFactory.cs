using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Factories
{
    public static class CookieFactory {
        public static HttpCookie MakeCookie (string CookieName, int ExpiryDateInDays) {
            return new HttpCookie(CookieName) {
                Expires = DateTime.UtcNow.AddDays(ExpiryDateInDays)
            };
        }

        public static HttpCookie MakeCookie (string CookieName, Customer c, int ExpiryDateInDays) {
            HttpCookie cookie =  new HttpCookie(CookieName) {
                Expires = DateTime.UtcNow.AddDays(ExpiryDateInDays)
            };

            cookie.Values["CustomerID"] = c.CustomerID.ToString();
            cookie.Values["CustomerName"] = c.CustomerName.ToString();
            cookie.Values["CustomerEmail"] = c.CustomerEmail.ToString();
            cookie.Values["CustomerGender"] = c.CustomerGender.ToString();

            return cookie;
        }
    }
}
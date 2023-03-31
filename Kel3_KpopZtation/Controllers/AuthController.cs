using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public class AuthController {

        public static CustomerRepo CusRepo = new CustomerRepo();

        /* 
         * Below is a tuple method. It returns multiple values.
         * 
         * Access it by doing:
         * (bool isValid, List<string> ErrorMsgs) = ValidateEmailAndPassword (email, password);
         */
        public static (Customer AssociatedAccount, List<string> ErrorMsgs) Authenticate (string Email, string Password, bool SetCookie) {
            Customer AssociatedAccount = null;
            List<string> ErrorMsgs = new List<string>();

            /* Email validation has been delegated to appropriate method */
            var EmailValidationResult = ValidateEmail(Email); ErrorMsgs.Add(EmailValidationResult.ErrorMsg);

            /* Password validation has been delegated to appropriate method */
            var PasswordValidationResult = ValidatePassword(Password); ErrorMsgs.Add(PasswordValidationResult.ErrorMsg);

            /* Removing empty error msgs */
            RemoveEmptyErrorMsgs(ErrorMsgs);

            /* Determining whether the given parameter can be checked further */
            bool ParameterIsValid = EmailValidationResult.isValid  && PasswordValidationResult.isValid;
            if ( ParameterIsValid ) {

                /* Determining whether there is any account associated with said email and password */
                AssociatedAccount = CustomerRepo.EmailPasswordMatch(Email, Password);

                /* When there is */
                if (AssociatedAccount != null) { 
                    
                    /* Store client's login info on a Session that self-expire after the connection is terminated */
                    CookieController.AssignSession(AssociatedAccount);

                    /* If client opt to auto login, then assign a cookie to client */
                    if (SetCookie) CookieController.AssignAuthCookie();
                }
            }
            
            /* Return the AssociatedAccount (if any), and the error messages found along the way */
            return (AssociatedAccount, ErrorMsgs);
        }

        public static (bool Status, Customer CreatedAccount) Register () {

            /* Jangan lupa harus dikerjain */

            return (false, null);
        }

        /* 
         * If there is an Authentication Cookie stored in the client's browser,
         * then make the session as per Cookie's data dictate. 
         */
        public static void MakeSessionFromCookie () {
            HttpCookie AuthCookie = HttpContext.Current.Request.Cookies["AuthInfo"];

            /* 
             * Sync data yang disimpan pada Cookie kedalam Session.
             * Dengan asumsi bahwa segala informasi yang dipegang oleh sebuah Cookie adalah benar. 
             */
            if (AuthCookie != null) {
                HttpContext.Current.Session["AuthInfo"] = CustomerRepo.ExistByID( Convert.ToInt32(AuthCookie.Values["CustomerID"]) );
            } 
        }

        public static (bool isValid, string ErrorMsg) ValidateEmail (string email) {

            /* Cek apakah string parameter bisa diproses */
            if ( FormatController.NullWhitespacesOrEmpty(email) ) {
                return (false, "Email cannot be empty or all whitespaces!");

            /* Cek apakah itu berformat email */
            } else if ( !FormatController.InEmailFormat(email) ) {
                return (false, "Email is not in a valid format! ");

            } else if ( !EmailExistOnDatabase(email) ) {
                return (false, "There is no account associated with that email!");
            }

            return (true, "");
        }

        public static (bool isValid, string ErrorMsg) ValidatePassword (string password) {

            /* 
             * Cek apakah string parameter bisa diproses.
             * 
             * No, this IS NOT a duplicate code smell. It is simply just a coincidence that 
             * password has partially the same checks as email's.
             * 
             * Furthermore, it is simpler to validate this way in the future, 
             * say what if we require passwords to have specific format (definitely NOT foreshadowing).
             */
            if (string.IsNullOrEmpty(password)) {
                return (false, "Password cannot be empty!");
            }

            return (true, "");
        }

        public static bool EmailExistOnDatabase (string email) {
            if (CustomerRepo.ExistByEmail(email) != null)
                return true;
            
            return false;
        }

        private static void RemoveEmptyErrorMsgs(List<string> ErrorMsgs) {
            ErrorMsgs.RemoveAll(s => s.Length <= 0);
        }
    
        public static void SignOut () {
            foreach (string SavedCookie in HttpContext.Current.Request.Cookies.AllKeys) {
                // System.Diagnostics.Debug.WriteLine(SavedCookie);
                HttpContext.Current.Response.Cookies.Get(SavedCookie).Expires = DateTime.Now.AddDays(CookieController.CookieSetbackValue);
            }

            HttpContext.Current.Response.Redirect("Login.aspx");
        }
    }
}
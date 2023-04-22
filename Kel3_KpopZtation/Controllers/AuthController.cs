using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Handlers;

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

            bool EmailValidationResult = CustomerController.ValidateEmail(Email, ErrorMsgs); 
            bool PasswordValidationResult = CustomerController.ValidatePassword(Password, ErrorMsgs); 

            var EmailExistence = EmailExistOnDatabase(Email, ""); 
            ErrorMsgs.Add(EmailExistence.ErrorMsg);

            FormatController.RemoveEmptyString(ErrorMsgs);

            bool ParameterIsValid = EmailValidationResult  && PasswordValidationResult && EmailExistence.doesExist;
            if ( ParameterIsValid ) {
                AssociatedAccount = CustomerRepo.Find(Email);

                if (AssociatedAccount != null && AssociatedAccount.CustomerPassword == Password) { 
                    CookieController.AssignSession(AssociatedAccount);

                    if (SetCookie) 
                        CookieController.AssignAuthCookie();

                    return (AssociatedAccount, ErrorMsgs);
                }
            }

            return (null, ErrorMsgs);
        }
        public static (Customer CreatedAccount, List<string> ErrorMsgs) Register (string Name, string Email, string Sex, string Address, string Password) {

            Customer CreatedAccount = null;
            List<string> ErrorMsgs = new List<string>();

            bool NameValidationResult = CustomerController.ValidateName(Name, ErrorMsgs);
            bool EmailValidationResult = CustomerController.ValidateEmail(Email, ErrorMsgs);

            var EmailExistenceInDB = EmailExistOnDatabase(Email, "");
            if (EmailExistenceInDB.doesExist == true) ErrorMsgs.Add("There is already an account associated with that email! Try logging in.");

            bool SexValidationResult = CustomerController.ValidateSex(Sex, ErrorMsgs);
            bool AddressValidationResult = CustomerController.ValidateAddress(Address, ErrorMsgs);
            bool PasswordValidationResult = CustomerController.ValidatePassword(Password, ErrorMsgs);

            FormatController.RemoveEmptyString(ErrorMsgs);

            bool ParameterIsValid = NameValidationResult && EmailValidationResult && EmailExistenceInDB.doesExist == false && SexValidationResult && AddressValidationResult && PasswordValidationResult;
            if ( ParameterIsValid ) {
                CreatedAccount = CustomerHandler.MakeCustomer(Name, Email, Sex, Address, Password, "Buyer");
                CustomerRepo.Insert(CreatedAccount);

                if (CreatedAccount != null) {
                    CookieController.AssignSession(CreatedAccount);
                }
            }

            return (CreatedAccount, ErrorMsgs);
        }
        public static (bool doesExist, string ErrorMsg) EmailExistOnDatabase (string email, string customErrorMsg) {
            if (CustomerRepo.Find(email) != null)
                return (true, customErrorMsg);

            if (FormatController.NullWhitespacesOrEmpty(customErrorMsg) || FormatController.TrimLen(customErrorMsg) <= 0)
                return (false, "There is no account associated with that email!");
            else
                return (false, customErrorMsg);
        }
        public static void MakeSessionFromCookie () {
            HttpCookie AuthCookie = HttpContext.Current.Request.Cookies["AuthInfo"];

            /* 
             * Sync data yang disimpan pada Cookie kedalam Session.
             * Dengan asumsi bahwa segala informasi yang dipegang oleh sebuah Cookie adalah benar. 
             */
            if (AuthCookie != null) {
                HttpContext.Current.Session["AuthInfo"] = CustomerRepo.Find( Convert.ToInt32(AuthCookie.Values["CustomerID"]) );
            } 
        }
        public static Customer ExtractCustomer () {
            Customer c = null;
            try {
                c = (Customer) HttpContext.Current.Session["AuthInfo"];
            } catch { }

            return c;
        }
        public static void SignOut () {
            foreach (string SavedCookie in HttpContext.Current.Request.Cookies.AllKeys) {
                // System.Diagnostics.Debug.WriteLine(SavedCookie);
                HttpContext.Current.Response.Cookies.Get(SavedCookie).Expires = DateTime.Now.AddDays(CookieController.CookieSetbackValue);
            }

            HttpContext.Current.Session["AuthInfo"] = (Customer) null;
            HttpContext.Current.Response.Redirect("Login.aspx");
        }
    }
}
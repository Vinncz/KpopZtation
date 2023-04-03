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

            /* Email validation has been delegated to appropriate method */
            var EmailValidationResult = ValidateEmail(Email); ErrorMsgs.Add(EmailValidationResult.ErrorMsg);
            var EmailStatus = EmailExistOnDatabase(Email, ""); ErrorMsgs.Add(EmailStatus.ErrorMsg);

            /* Password validation has been delegated to appropriate method */
            var PasswordValidationResult = ValidatePassword(Password); ErrorMsgs.Add(PasswordValidationResult.ErrorMsg);

            /* Removing empty error msgs */
            FormatController.RemoveEmptyString(ErrorMsgs);

            /* Determining whether the given parameter can be checked further */
            bool ParameterIsValid = EmailValidationResult.isValid  && PasswordValidationResult.isValid && EmailStatus.doesExist;
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

        public static (Customer CreatedAccount, List<string> ErrorMsgs) Register 
            (string name, string email, string sex, string address, string password) {

            Customer CreatedAccount = null;
            List<string> ErrorMsgs = new List<string>();

            var NameValidationResult = ValidateName(name); ErrorMsgs.Add(NameValidationResult.ErrorMsg);

            var EmailValidationResult = ValidateEmail(email); ErrorMsgs.Add(EmailValidationResult.ErrorMsg);
            var EmailExistenceInDB = EmailExistOnDatabase(email, "There are already an account ascosiated with that email! Try logging in.");  ErrorMsgs.Add(EmailExistenceInDB.ErrorMsg);

            var SexValidationResult = ValidateSex(sex); ErrorMsgs.Add(SexValidationResult.ErrorMsg);

            var AddressValidationResult = ValidateAddress(address); ErrorMsgs.Add(AddressValidationResult.ErrorMsg);

            var PasswordValidationResult = ValidatePassword(password); ErrorMsgs.Add(PasswordValidationResult.ErrorMsg);

            FormatController.RemoveEmptyString(ErrorMsgs);

            bool ParameterIsValid = NameValidationResult.isValid && EmailValidationResult.isValid && EmailExistenceInDB.doesExist == false
                                    && SexValidationResult.isValid && AddressValidationResult.isValid && PasswordValidationResult.isValid;
            
            System.Diagnostics.Debug.WriteLine(NameValidationResult.isValid ? "name valid" : "name invalid");
            System.Diagnostics.Debug.WriteLine(EmailValidationResult.isValid ? "email valid" : "email invalid");
            System.Diagnostics.Debug.WriteLine(EmailExistenceInDB.doesExist ? "email exists" : "email does not exist");
            System.Diagnostics.Debug.WriteLine(SexValidationResult.isValid ? "Sex valid" : "Sex invalid");
            System.Diagnostics.Debug.WriteLine(AddressValidationResult.isValid ? "Address valid" : "Address invalid");
            System.Diagnostics.Debug.WriteLine(PasswordValidationResult.isValid ? "Password valid" : "Password invalid");

            if (ParameterIsValid) {
                CreatedAccount = CustomerHandler.MakeCustomer(name, email, sex, address, password, "Buyer");
                CustomerHandler.InsertCustomer(CreatedAccount);

                if (CreatedAccount != null) {
                    CookieController.AssignSession(CreatedAccount);
                }
            }

            return (CreatedAccount, ErrorMsgs);
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

        public static Customer ExtractCustomer () {
            return (Customer) HttpContext.Current.Session["AuthInfo"];
        }

        public static (bool isValid, string ErrorMsg) ValidateName (string name) {
            
            /* Cek apakah string parameter bisa diproses */
            if ( FormatController.NullWhitespacesOrEmpty(name) ) {
                return (false, "Name cannot be empty or all whitespaces!");

            /* Cek apakah panjang string parameter berada diantara 5-50 karakter */
            } else if ( FormatController.TrimLen(name) < 5 || FormatController.TrimLen(name) > 50 ) {
                return (false, "That is such a long name! Try using aliases.");
            
            }

            return (true, "");
        }

        public static (bool isValid, string ErrorMsg) ValidateEmail (string email) {

            /* Cek apakah string parameter bisa diproses */
            if ( FormatController.NullWhitespacesOrEmpty(email) ) {
                return (false, "Email cannot be empty or all whitespaces!");

            /* Cek apakah itu berformat email */
            } else if ( !FormatController.InEmailFormat(email) ) {
                return (false, "Email is not in a correct format!");

            } 

            return (true, "");
        }

        public static (bool isValid, string ErrorMsg) ValidatePassword (string password) {

            if ( FormatController.NullWhitespacesOrEmpty(password) ) {
                return (false, "Password cannot be empty!");

            } else if ( !FormatController.InAlphaNumericFormat(password) ) {
                return (false, "Password must be an alphanumeric!");
            }
            
            if ( FormatController.TrimLen(password) > 50 ) {
                return (false, "Password is too long! Try keeping it under 50 characters.");
            } 

            return (true, "");
        }

        public static (bool isValid, string ErrorMsg) ValidateSex (string sex) {
            System.Diagnostics.Debug.WriteLine(sex);
            /* Cek apakah variabelnya bisa diproses */
            if (FormatController.NullWhitespacesOrEmpty(sex) || FormatController.TrimLen(sex) < 4) {
                return (false, "Gender must be picked!");

            /* Cek apakah memenuhi salah satu dari dua kemungkinan */
            } else if ( !(sex != "Male" || sex != "Female") ) {
                return (false, "Gender must be either Male or Female!");
            }

            if ( sex != "Male" || sex != "Female" )
                return (true, "");
            else
                return (false, "Something went wrong.");
        }

        public static (bool isValid, string ErrorMsg) ValidateAddress (string address) {

            /* Cek apakah string parameter bisa diproses */
            if ( FormatController.NullWhitespacesOrEmpty(address) ) {
                return (false, "Address cannot be empty or all whitespaces!");

            /* Cek apakah itu berakhiran dengan "Street" */
            } else if ( !FormatController.EndsWith(address, "Street") ) {
                return (false, "An address must end with \"Street\"!");

            } 
            
            if ( FormatController.TrimLen(address) > 100 ) {
                return (false, "Address is too long! Try shortening it to 100 characters max.");
            }

            return (true, "");
        }

        public static (bool doesExist, string ErrorMsg) EmailExistOnDatabase (string email, string customErrorMsg) {
            if (CustomerRepo.ExistByEmail(email) != null)
                return (true, customErrorMsg);

            /* Jika gamau custom error msg */
            if (FormatController.NullWhitespacesOrEmpty(customErrorMsg) || FormatController.TrimLen(customErrorMsg) <= 0)
                return (false, "There is no account associated with that email!");
            else
                return (false, customErrorMsg);
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
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
        public static (Customer AssociatedAccount, List<string> ErrorMsgs) Authenticate (string Email, string Password) {
            Customer AssociatedAccount = null;
            List<string> ErrorMsgs = new List<string>();

            var EmailValidationResult = ValidateEmail(Email);
            ErrorMsgs.Add(EmailValidationResult.ErrorMsg);

            var PasswordValidationResult = ValidatePassword(Password);
            ErrorMsgs.Add(PasswordValidationResult.ErrorMsg);

            RemoveEmptyErrorMsgs(ErrorMsgs);

            bool LoginSuccessful = EmailValidationResult.isValid 
                                     && PasswordValidationResult.isValid 
                                     && EmailExistOnDatabase(Email);

            if ( LoginSuccessful ) {
                AssociatedAccount = CustomerRepo.EmailPasswordMatch(Email, Password);

                if (AssociatedAccount != null) { 
                    CookieController.AssignSession(AssociatedAccount);
                    CookieController.AssignAuthCookie();
                    CookieController.SyncCookieWithSession();
                }
            }
            
            return (AssociatedAccount, ErrorMsgs);
        }

        private static void RemoveEmptyErrorMsgs(List<string> ErrorMsgs) {
            ErrorMsgs.RemoveAll(s => s.Length <= 0);
        }

        public static (bool Status, Customer CreatedAccount) Register () {

            /* Jangan lupa harus dikerjain */

            return (false, null);
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
    }
}
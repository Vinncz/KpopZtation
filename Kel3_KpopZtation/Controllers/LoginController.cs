using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public static class LoginController {
        private static CustomerRepo c = new CustomerRepo();

        /* 
         * Below is a tuple method. It returns multiple values.
         * 
         * Access it by doing:
         * (bool isValid, List<string> ErrorMsgs) = ValidateEmailAndPassword(email, password);
         */
        public static (bool isValid, List<string> ErrorMsgs) ValidateEmailAndPassword(string email, string password) {
            List<string> ErrorMsgs = new List<string>();

            var emailValidationResult = ValidateEmail(email);
            ErrorMsgs.Add(emailValidationResult.ErrorMsg);

            var passwordValidationResult = ValidatePassword(password);
            ErrorMsgs.Add(passwordValidationResult.ErrorMsg);

            bool isValid = emailValidationResult.isValid && passwordValidationResult.isValid;

            ErrorMsgs.RemoveAll(s => s.Length <= 0);
            return (isValid, ErrorMsgs);
        }

        public static (bool isValid, string ErrorMsg) ValidateEmail (string email) {

            /* Cek apakah formatnya salah */
            if (string.IsNullOrWhiteSpace(email) || FormatValidator.TrimmedLength(email) <= 0) {
                return (false, "Email cannot be empty or all whitespaces!");

            /* Cek apakah itu berformat email */
            } else if ( !FormatValidator.EmailFormatValidator(email) ) {
                return (false, "Email is not in a valid format! ");

            /* Cek kondisi lainnya */
            } else {

                /* Cek apakah emailnya sudah terasosiasi dengan account */
                if (CustomerRepo.ExistByEmail(email) == null) {
                    return (false, "No account is associated with that email! Try registering.");
                }
            }

            return (true, "");
        }

        public static (bool isValid, string ErrorMsg) ValidatePassword (string password) {

            /* 
             * Cek apakah formatnya salah.
             * 
             * No,this IS NOT a duplicate code smell. It is simply just a coincidence that 
             * a password has partially the same checks as email's
             */
            if (string.IsNullOrEmpty(password)) {
                return (false, "Password cannot be empty!");
            }

            return (true, "");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kel3_KpopZtation.Controllers {
    public class CustomerController {
        public static bool ValidateName (string Name, List<string> ErrorMsgs) {

            string ErrorMsg = "";
            if ( FormatController.NullWhitespacesOrEmpty(Name) ) {
                ErrorMsg = "Name cannot be empty or all whitespaces!";

            } else if ( FormatController.TrimLen(Name) < 5 ) {
                ErrorMsg = "Name must have at least 5 characters!";
            
            }

            if (FormatController.TrimLen(Name) > 50) {
                ErrorMsg = "That is such a long name! Consider using aliases.";
            }

            return CheckErrorMsg(ErrorMsg, ErrorMsgs);
        }
        public static bool ValidateEmail (string Email, List<string> ErrorMsgs) {

            string ErrorMsg = "";
            if ( FormatController.NullWhitespacesOrEmpty(Email) ) {
                ErrorMsg = "Email cannot be empty or all whitespaces!";

            } 
            
            if ( !FormatController.InEmailFormat(Email) ) {
                ErrorMsg = "Email is not in a correct format!";

            }

            return CheckErrorMsg(ErrorMsg, ErrorMsgs);
        }
        public static bool ValidatePassword (string Password, List<string> ErrorMsgs) {

            string ErrorMsg = "";
            if ( FormatController.NullWhitespacesOrEmpty(Password) ) {
                ErrorMsg = "Password cannot be empty!";

            } else if ( !FormatController.InAlphaNumericFormat(Password) ) {
                ErrorMsg = "Password must be an alphanumeric!";

            }
            
            if ( FormatController.TrimLen(Password) > 50 ) {
                ErrorMsg = "Password is too long! Try keeping it under 50 characters.";

            }

            return CheckErrorMsg(ErrorMsg, ErrorMsgs);
        }
        public static bool ValidateSex (string sex, List<string> ErrorMsgs) {

            string ErrorMsg = "";
            if (FormatController.NullWhitespacesOrEmpty(sex) || FormatController.TrimLen(sex) < 4) {
                ErrorMsg = "Gender must be picked!";
                
            } else if ( !(sex != "Male" || sex != "Female") ) {
                ErrorMsg = "Gender must be either Male or Female!";
                
            }

            return CheckErrorMsg(ErrorMsg, ErrorMsgs);
        }
        public static bool ValidateAddress (string address, List<string> ErrorMsgs) {

            string ErrorMsg = "";
            if ( FormatController.NullWhitespacesOrEmpty(address) ) {
                ErrorMsg = "Address cannot be empty or all whitespaces!";

            } else if ( !FormatController.EndsWith(address, "Street") ) {
                ErrorMsg = "An address must end with \"Street\"!";

            } 
            
            if ( FormatController.TrimLen(address) > 100 ) {
                ErrorMsg = "Address is too long! Try shortening it to 100 characters max.";

            }

            return CheckErrorMsg(ErrorMsg, ErrorMsgs);
        }

        private static bool CheckErrorMsg (string ErrorMsg, List<string> ErrorMsgs) {
            if ( FormatController.TrimLen(ErrorMsg) > 0 ) {
                ErrorMsgs.Add(ErrorMsg);
                return false;
            }

            return true;
        } 
    }
}
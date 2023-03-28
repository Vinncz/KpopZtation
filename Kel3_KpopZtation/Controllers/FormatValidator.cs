using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kel3_KpopZtation.Controllers
{
    public static class FormatValidator {
        public static bool EmailFormatValidator (string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }

        public static int TrimmedLength (string s) {
            try {
                return s.Trim().Length;
            } catch {
                return -1;
            }
        }

        public static bool IsNull (object o) {
            return o == null;
        }


    }
}
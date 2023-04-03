using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kel3_KpopZtation.Controllers {
    public static class FormatController {

        /* <Setters> */
            /* Sets the valid file size in MegaBytes */
            private static readonly int ValidFileSize = 2 * (1024 * 1024);

            /* Sets the allowed filetype */
            private static readonly List<string> ValidFileExtension
                = new string[] { ".png", ".jpg", ".jpeg", ".jfif" }.ToList();
        /* </Setters> */

        public static bool HasValidFileSize (int FileSize) {
            double FileSizeInMB = FileSize / (1024 * 1024);

            return FileSizeInMB <= ValidFileSize;
        }

        public static bool HasValidFileExtension (string FileExtension) {
            return ValidFileExtension.Contains(FileExtension);
        }

        public static List<string> GetValidFileExtension () {
            return ValidFileExtension;
        }

        public static bool InEmailFormat (string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }

        public static bool InAlphaNumericFormat (string CheckedString) {
            if ( NullWhitespacesOrEmpty(CheckedString) )
                return false;
            
            foreach (char c in CheckedString)
                if ( !char.IsLetterOrDigit(c) )
                    return false;

            return true;
        }

        public static bool EndsWith (string CheckedString, string Pattern) {
            if (NullWhitespacesOrEmpty(CheckedString) || NullWhitespacesOrEmpty(Pattern))
                return false;

            return CheckedString.EndsWith(Pattern);
        }

        public static bool NullWhitespacesOrEmpty (string s) {
            return string.IsNullOrWhiteSpace(s) || TrimLen(s) <= 0;
        }

        public static int TrimLen (string s) {
            try {
                return s.Trim().Length;
            } catch {
                return -1;
            }
        }
    
        public static void RemoveEmptyString (List<string> ErrorMsgs) {
            ErrorMsgs.RemoveAll(s => s.Length <= 0);
        }
    }
}
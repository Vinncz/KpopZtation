using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Handlers;

namespace Kel3_KpopZtation.Controllers {
    public static class ArtistController {

        public static Artist GetArtistByID (string id) {
            return ArtistRepo.ExistByID( Convert.ToInt32(id) );
        }

        public static (bool updatedSuccessfully, List<string> ErrorMsgs) UpdateArtist (int artistID, string name, string filename, int filesize) {
            List<string> ErrorMsgs = new List<string>();

            var NameValidationResult = ValidateName(name); ErrorMsgs.Add(NameValidationResult.ErrorMsg);
            var FileValidationResult = ValidateProfilePicture(filename, filesize); ErrorMsgs.Add(FileValidationResult.ErrorMsg);

            FormatController.RemoveEmptyString(ErrorMsgs);
            System.Diagnostics.Debug.WriteLine(NameValidationResult.isValid ? "Nama artist valid -> " + name : "Namanya jelek -> " + name);
            System.Diagnostics.Debug.WriteLine(FileValidationResult.isValid ? "File valid -> " + filename : "File jelek -> " + filename);

            if (NameValidationResult.isValid  &&  FileValidationResult.isValid) {
                ArtistRepo.UpdateArtist(artistID, name, filename);
                return (true, ErrorMsgs);
            }

            return (false, ErrorMsgs);
        }

        public static (bool updatedSuccessfully, List<string> ErrorMsgs) MakeArtist (string name, string filename, int filesize) {
            List<string> ErrorMsgs = new List<string>();

            var NameValidationResult = ValidateName(name); ErrorMsgs.Add(NameValidationResult.ErrorMsg);
            var FileValidationResult = ValidateProfilePicture(filename, filesize); ErrorMsgs.Add(FileValidationResult.ErrorMsg);

            FormatController.RemoveEmptyString(ErrorMsgs);
            System.Diagnostics.Debug.WriteLine(NameValidationResult.isValid ? "Nama artist valid -> " + name : "Namanya jelek -> " + name);
            System.Diagnostics.Debug.WriteLine(FileValidationResult.isValid ? "File valid -> " + filename : "File jelek -> " + filename);

            if (NameValidationResult.isValid  &&  FileValidationResult.isValid) {
                ArtistHandler.InsertArtist(ArtistHandler.MakeArtist(name, filename));
                return (true, ErrorMsgs);
            }

            return (false, ErrorMsgs);
        }

        public static (bool isValid, string ErrorMsg) ValidateName (string name) {
            
            /* Cek apakah string parameter bisa diproses */
            if ( FormatController.NullWhitespacesOrEmpty(name) ) {
                return (false, "Name cannot be empty or all whitespaces!");

            /* Cek apakah panjang string parameter berada diantara 5-50 karakter */
            } else if ( FormatController.TrimLen(name) > 50 ) {
                return (false, "That is such a long name! Try using aliases.");
            
            }

            return (true, "");
        }

        public static (bool isValid, string ErrorMsg) ValidateProfilePicture (string filename, int filesize) {

            if ( FormatController.NullWhitespacesOrEmpty(filename) ) {
                return (true, "");

            } else if ( !FormatController.HasValidFileExtension(Path.GetExtension(filename.ToLower())) ) {
                List<string> ListofValidFileExtension = FormatController.GetValidFileExtension();
                string ValidFileExtension = string.Join(", ", ListofValidFileExtension.Take(ListofValidFileExtension.Count - 1)) + " and " + ListofValidFileExtension.Last();
                
                return (false, "The only allowed file extension is limited to " + ValidFileExtension);
            }

            if ( filesize > 2000000) {
                return (false, "The file is too big! Consider compressing it.");
            }

            return (true, "");
        }
    }
}
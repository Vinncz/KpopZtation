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
            return ArtistRepo.Find( Convert.ToInt32(id) );
        }
        public static bool MakeArtist (string name, string filename, int filesize, List<string> ErrorMsgs) {
            bool validationResult = ValidateArtist(name, filename, filesize, ErrorMsgs);

            if (validationResult) {
                if (FormatController.TrimLen(filename) <= 0 || filesize <= 1024) {
                    ErrorMsgs.Add("Picture must be present.");
                    return false;
                }

                bool duplicateName = ArtistRepo.Find(name) != null;
                if (duplicateName) {
                    ErrorMsgs.Add("There is already an artist with the same name as the one you typed");
                    FormatController.RemoveEmptyString(ErrorMsgs);

                    return false;
                }

                ArtistRepo.Insert(ArtistHandler.MakeArtist(name, filename));
                return true;
            }

            return false;
        }
        public static bool UpdateArtist (int artistID, string name, string filename, int filesize, List<string> ErrorMsgs) {
            bool validationResult = ValidateArtist(name, filename, filesize, ErrorMsgs);

            if (validationResult) {
                Artist ArtistWithTheSameName = ArtistRepo.Find(name);

                if ( ArtistWithTheSameName.ArtistID != artistID ) {
                    ErrorMsgs.Add("There is already an artist with the same name as the one you typed.");
                    FormatController.RemoveEmptyString(ErrorMsgs);

                    return false;
                }

                ArtistRepo.Update(artistID, name, filename);
                return true;
            }

            return false;
        }
        
        public static void DeleteArtist (int ArtistID) {
            AlbumRepo.DeleteByArtistID(ArtistID);
            ArtistRepo.Delete(ArtistID);
        }
        
        public static bool ValidateArtist (string name, string filename, int filesize, List<string> ErrorMsgs) {
            bool NameValidationResult = ValidateName(name, ErrorMsgs);
            bool FileValidationResult = ValidateProfilePicture(filename, filesize, ErrorMsgs);

            FormatController.RemoveEmptyString(ErrorMsgs);

            if (NameValidationResult && FileValidationResult) {
                return true;
            }

            return false;
        }
        public static bool ValidateName (string name, List<string> ErrorMsgs) {

            string ErrorMsg = "";
            if ( FormatController.NullWhitespacesOrEmpty(name) ) {
                ErrorMsg = "Name cannot be empty or all whitespaces!";

            } else if ( FormatController.TrimLen(name) > 50 ) {
                ErrorMsg = "That is such a long name! Try using aliases.";
            
            }

            return CheckErrorMsg(ErrorMsg, ErrorMsgs);
        }
        public static bool ValidateProfilePicture (string filename, int filesize, List<string> ErrorMsgs) {

            string ErrorMsg = "";
            if ( FormatController.NullWhitespacesOrEmpty(filename) ) {
                ErrorMsg = "";

            } else if ( !FormatController.HasValidFileExtension(Path.GetExtension(filename.ToLower())) ) {
                List<string> ListofValidFileExtension = FormatController.GetValidFileExtension();
                string ValidFileExtension = string.Join(", ", ListofValidFileExtension.Take(ListofValidFileExtension.Count - 1)) + " and " + ListofValidFileExtension.Last();
                
                ErrorMsg = "The only allowed file extension is limited to " + ValidFileExtension;
            }

            if ( filesize > 2000000) {
                ErrorMsg = "The file is too big! Consider compressing it.";
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
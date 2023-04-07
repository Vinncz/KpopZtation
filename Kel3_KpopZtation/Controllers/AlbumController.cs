﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Handlers;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public static class AlbumController {

        public static (bool CreatedSuccessfully, List<string> ErrorMsgs) MakeAlbum (int ArtistID, string AlbumName, string AlbumDescription, string AlbumPrice, string AlbumStock, string AlbumCoverName, int AlbumCoverSize) {

            var validationResult = ValidateAlbum (AlbumName, AlbumDescription, AlbumPrice, AlbumStock, AlbumCoverName, AlbumCoverSize);

            if (validationResult.isValid) {
                if ( FormatController.TrimLen(AlbumCoverName) <= 0 || AlbumCoverSize <= 1024 ) {
                    validationResult.ErrorMsgs.Add("Picture must be present.");
                    return (false, validationResult.ErrorMsgs);
                }

                AlbumHandler.InsertAlbum(AlbumHandler.MakeAlbum(AlbumName, AlbumDescription, int.Parse(AlbumPrice), int.Parse(AlbumStock), AlbumCoverName, ArtistID));
                return (true, validationResult.ErrorMsgs);
            }

            return (false, validationResult.ErrorMsgs);
        }

        public static (bool isValid, List<string> ErrorMsgs) ValidateAlbum (string AlbumName, string AlbumDescription, string AlbumPrice, string AlbumStock, string AlbumCoverName, int AlbumCoverSize) {
            List<string> ErrorMsgs = new List<string>();

            var NameValidationResult = ValidateName(AlbumName); ErrorMsgs.Add(NameValidationResult.ErrorMsg);
            var DescriptionValidationResult = ValidateDescription(AlbumDescription); ErrorMsgs.Add(DescriptionValidationResult.ErrorMsg);
            var PriceValidationResult = ValidatePrice(AlbumPrice); ErrorMsgs.Add(PriceValidationResult.ErrorMsg);
            var StockValidationResult = ValidateStock(AlbumStock); ErrorMsgs.Add(StockValidationResult.ErrorMsg);
            /* 
             * NOTE:
             * FileValidation hanya validasi KETIKA FILENYA ADA SAJA.
             * Untuk itu, silahkan cek sendiri apa yang terjadi ketika FILE TIDAK ADA. 
             */
            var FileValidationResult = ValidatePicture(AlbumCoverName, AlbumCoverSize); ErrorMsgs.Add(FileValidationResult.ErrorMsg);

            FormatController.RemoveEmptyString(ErrorMsgs);

            if (NameValidationResult.isValid && DescriptionValidationResult.isValid && PriceValidationResult.isValid && StockValidationResult.isValid && FileValidationResult.isValid) {
                return (true, ErrorMsgs);
            }

            return (false, ErrorMsgs);
        }

        /*
        public static (bool updatedSuccessfully, List<string> ErrorMsgs) UpdateAlbum (int artistID, string name, string filename, int filesize) {
            var validationResult = ValidateAlbum(name, filename, filesize);

            if (validationResult.isValid) {
                ArtistRepo.UpdateArtist(artistID, name, filename);
                return (true, validationResult.ErrorMsgs);
            }

            return (false, validationResult.ErrorMsgs);
        }
        */

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

        public static (bool isValid, string ErrorMsg) ValidateDescription (string description) {
            
            /* Cek apakah string parameter bisa diproses */
            if ( FormatController.NullWhitespacesOrEmpty(description) ) {
                return (false, "Description cannot be empty or all whitespaces!");

            /* Cek apakah panjang string parameter berada diantara 5-50 karakter */
            } else if ( FormatController.TrimLen(description) > 255 ) {
                return (false, "That is such a long description! Try to keep it minimal under 255 characters.");
            
            }

            return (true, "");
        }
        public static (bool isValid, string ErrorMsg) ValidatePrice (string price) {
            
            if ( FormatController.NullWhitespacesOrEmpty(price) || FormatController.TrimLen(price) <= 0 ) {
                return (false, "Price must be filled");

            } 
            
            int IntPrice = Convert.ToInt32(price);
            if (IntPrice < 100_000 || IntPrice > 1_000_000 ) {
                return (false, "Price must be between 100k and 1 million!");

            }

            return (true, "");
        }
        public static (bool isValid, string ErrorMsg) ValidateStock (string stock) {
            
            if ( FormatController.NullWhitespacesOrEmpty(stock) || FormatController.TrimLen(stock) <= 0 ) {
                return (false, "Stock must be filled");

            } 
            
            int IntStock = Convert.ToInt32(stock);
            if (IntStock < 0 ) {
                return (false, "Stock must be larger than 0!");

            }

            return (true, "");
        }
        public static (bool isValid, string ErrorMsg) ValidatePicture (string filename, int filesize) {

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
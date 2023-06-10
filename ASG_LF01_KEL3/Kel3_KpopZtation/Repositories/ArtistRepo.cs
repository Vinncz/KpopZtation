using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Handlers;

namespace Kel3_KpopZtation.Repositories {
    public static class ArtistRepo {
        private static KZDBEntities db = ConnectionMaster.CopyInstance();

        /* Miscellaneous/Overloaded Operations */
        public static int GetLatestID () {
            /* O(log n) algorithm */
            Artist data = db.Artists.OrderByDescending(a => a.ArtistID).FirstOrDefault();
            if (data != null)
                return data.ArtistID;

            return 0;
        }
        public static Artist Find (string ArtistName) {
            return ( from Artist in db.Artists
                     where Artist.ArtistName == ArtistName
                     select Artist ).FirstOrDefault();
        }

        /* CRUD Operations */
        public static List<Artist> Select () {
            return ( from Artist in db.Artists 
                     orderby Artist.ArtistName ascending
                     select Artist ).ToList();
        }
        public static Artist Find (int ArtistID) {
            return ( from Artist in db.Artists
                     where Artist.ArtistID == ArtistID
                     select Artist ).FirstOrDefault();
        }
        public static bool Insert (Artist a) {
            db.Artists.Add(a);
            return Save();
        }
        public static bool Update (int TargetArtistID, string ArtistName, string ArtistImageFileName) {
            Artist a = Find(TargetArtistID);
            return RewriteIfChanged(ArtistName, ArtistImageFileName, a);
        }
            private static bool RewriteIfChanged (string ArtistName, string ArtistImageFileName, Artist TargetData) {
                if (TargetData == null) {
                    return false;
                }

                if (TargetData.ArtistName != ArtistName) {
                    TargetData.ArtistName = ArtistName;
                }

                if ( !FormatController.NullWhitespacesOrEmpty(ArtistImageFileName) && FormatController.TrimLen(ArtistImageFileName) > 0 && TargetData.ArtistImage != ArtistImageFileName ) {
                    TargetData.ArtistImage = ArtistImageFileName;
                }

                return Save();
            }
        public static bool Delete (int TargetArtistID) {
            Artist TargetData = Find(TargetArtistID);
            db.Artists.Remove(TargetData);

            return Save();
        }
        public static bool Save () {
            try {
                db.SaveChanges();
                return true;

            } catch ( System.Data.Entity.Validation.DbEntityValidationException ex ) {
                PrintError(ex);
                return false;

            }
        }
            private static void PrintError ( System.Data.Entity.Validation.DbEntityValidationException ex ) {
            foreach ( var entityValidationErrors in ex.EntityValidationErrors ) {
                foreach ( var validationError in entityValidationErrors.ValidationErrors ) {
                    System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                }
            }
        }

    }
}
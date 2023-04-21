using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation.Repositories {
    public static class AlbumRepo {
        private static KZDBEntities db = ConnectionMaster.CopyInstance();

        /* Miscellaneous/Overloaded Operations */
        public static int GetLatestID () {
            /* O(log n) algorithm */
            Album data = db.Albums.OrderByDescending(a => a.AlbumID).FirstOrDefault();
            if (data != null)
                return data.AlbumID;

            return 0;
        }
        public static List<Album> Select (int ArtistID) {
            return (from Album in db.Albums 
                    where Album.ArtistID == ArtistID 
                    orderby Album.AlbumName ascending
                    select Album).ToList();
        }
        public static bool DeleteByArtistID (int ArtistID) {
            List<Album> Albums = Select(ArtistID);
            foreach (Album a in Albums) {
                db.Albums.Remove(a);
            }

            return Save();
        }

        /* CRUD Operations */
        public static List<Album> Select () {
            return (from Album in db.Albums
                    orderby Album.AlbumName ascending
                    select Album).ToList();
        }
        public static Album Find (int AlbumID) {
            return (from Album in db.Albums
                    where Album.AlbumID == AlbumID
                    select Album).FirstOrDefault();
        }
        public static bool Insert (Album a) {
            db.Albums.Add(a);
            return Save();
        }
        public static bool Update (int TargetAlbumID, string AlbumName, string AlbumDescription, string AlbumImage, int AlbumStock, int AlbumPrice) {
            Album TargetData = Find(TargetAlbumID);
            return RewriteIfChanged(AlbumName, AlbumDescription, AlbumImage, AlbumStock, AlbumPrice, TargetData);
        }
            private static bool RewriteIfChanged (string AlbumName, string AlbumDescription, string AlbumImage, int AlbumStock, int AlbumPrice, Album TargetData) {
                if (TargetData == null)
                    return false;
                
                if (TargetData.AlbumName != AlbumName) {
                    TargetData.AlbumName = AlbumName;
                }

                if (TargetData.AlbumDescription != AlbumDescription) {
                    TargetData.AlbumDescription = AlbumDescription;
                }

                if (!FormatController.NullWhitespacesOrEmpty(AlbumImage) && FormatController.TrimLen(AlbumImage) > 0 && TargetData.AlbumImage != AlbumImage) {
                    TargetData.AlbumImage = AlbumImage;
                }

                if (TargetData.AlbumPrice != AlbumPrice) {
                    TargetData.AlbumPrice = AlbumPrice;
                }

                if (TargetData.AlbumStock != AlbumStock) {
                    TargetData.AlbumStock = AlbumStock;
                }

                return Save();
            }
        public static bool Delete (int TargetAlbumID) {
            Album TargetData = Find(TargetAlbumID);
            db.Albums.Remove(TargetData);

            return Save();
        }
        public static bool Save () {
            try {
                db.SaveChanges();
                return true;

            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
                PrintError(ex);
                return false;

            }
        }
            private static void PrintError (System.Data.Entity.Validation.DbEntityValidationException ex) {
                foreach (var entityValidationErrors in ex.EntityValidationErrors) {
                    foreach (var validationError in entityValidationErrors.ValidationErrors) {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public static class AlbumRepo {

        private static KZEntities db = ConnectionMaster.CopyInstance();
        public static void InsertAlbum (Album a) {
            try {
                db.Albums.Add(a);
                db.SaveChanges();
            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
        public static List<Album> Retrieve () {
            return (from Album in db.Albums select Album).ToList();
        }
        public static List<Album> AssociatedAlbum (int ArtistID) {
            return (from Album in db.Albums where Album.ArtistID == ArtistID select Album).ToList();
        }
        public static Album ExistByID (int AlbumID) {
            return (from Album in db.Albums where Album.AlbumID == AlbumID select Album).FirstOrDefault();
        }
        public static int GetLatestID () {
            return (from Album in db.Albums orderby Album.AlbumID descending select Album.AlbumID).FirstOrDefault();
        }
        public static void RemoveByID (int id) {
            Album a = (from Album in db.Albums where Album.AlbumID == id select Album).FirstOrDefault();
            db.Albums.Remove(a);
            save();
        }
        public static void RemoveByArtistID (int ArtistID) {
            List<Album> Albums = AssociatedAlbum(ArtistID);
            foreach (Album a in Albums) {
                db.Albums.Remove(a);
            }

            save();
        }
        public static void save () {
            db.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public static class AlbumRepo {

        private static KZEntities db = ConnectionMaster.CopyInstance();

        public static List<Album> Retrieve () {
            return (from Album in db.Albums select Album).ToList();
        }

        public static List<Album> RetrieveAssociated (int ArtistID) {
            return (from Album in db.Albums where Album.Artist.ArtistID == ArtistID select Album).ToList();
        }

        public static void RemoveByID (int id) {
            Album a = (from Album in db.Albums where Album.AlbumID == id select Album).FirstOrDefault();
            db.Albums.Remove(a);
            db.SaveChanges();
        }

    }
}
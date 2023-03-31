using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public static class ArtistRepo {

        private static KZEntities db = ConnectionMaster.CopyInstance();

        public static Artist ExistByID (int ID) {
            return (from Artist in db.Artists
                    where Artist.ArtistID == ID 
                    select Artist).FirstOrDefault();
        }
        /* There is a restriction about name being unique */
        public static Artist ExistByName (string Email) {
            return (from Artist in db.Artists
                    where Artist.ArtistName == Email
                    select Artist).FirstOrDefault();
        }
        public static List<Artist> Retrieve () {
            return (from Artist in db.Artists orderby Artist.ArtistName ascending select Artist).ToList();
        }

        public static void RemoveByID (int id) {
            Artist a = (from Artist in db.Artists where Artist.ArtistID == id select Artist).FirstOrDefault();
            db.Artists.Remove(a);
            db.SaveChanges();
        }
    }
}
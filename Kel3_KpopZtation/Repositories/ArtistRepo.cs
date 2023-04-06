﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation.Repositories {
    public static class ArtistRepo {

        private static KZEntities db = ConnectionMaster.CopyInstance();

        public static void InsertArtist (Artist a) {
            db.Artists.Add(a);
            db.SaveChanges();
        }
        public static void UpdateArtist (int ID, string name, string filename) {
            Artist a = db.Artists.Find(ID);

            if ( !FormatController.NullWhitespacesOrEmpty(name) && FormatController.TrimLen(name) > 0  &&  a.ArtistName != name) 
                a.ArtistName = name;

            if (!FormatController.NullWhitespacesOrEmpty(filename) && FormatController.TrimLen(filename) > 0 && a.ArtistImage != filename )
                a.ArtistImage = filename;

            db.SaveChanges();
        }
        public static Artist ExistByID (int ID) {
            return (from Artist in db.Artists
                    where Artist.ArtistID == ID 
                    select Artist).FirstOrDefault();
        }
        public static Artist ExistByName (string Email) {
            return (from Artist in db.Artists
                    where Artist.ArtistName == Email
                    select Artist).FirstOrDefault();
        }
        public static List<Artist> Retrieve () {
            return (from Artist in db.Artists orderby Artist.ArtistName ascending select Artist).ToList();
        }
        public static int GetLatestID () {
            return (from Artist in db.Artists orderby Artist.ArtistID descending select Artist.ArtistID).FirstOrDefault();
        }
        public static void RemoveByID (int id) {
            Artist a = ExistByID(id);
            db.Artists.Remove(a);
            db.SaveChanges();
        }
    }
}
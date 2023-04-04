using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Factories;

namespace Kel3_KpopZtation.Handlers {
    public class ArtistHandler {
        public static void InsertArtist (Artist c) {
            ArtistRepo.InsertArtist(c);
        }

        public static Artist MakeArtist (string name, string filename) {
            int id = GetLatestArtistID();

            if (id <= 0) 
                return null;

            Artist a = null;
            try {
                a = ArtistFactory.MakeArtist(++id, name, filename);
            } catch {
                a = null;
            }

            return a;
        }

        public static int GetLatestArtistID () {
            return ArtistRepo.GetLatestID();
        }
    }
}
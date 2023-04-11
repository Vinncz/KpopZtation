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
            int ArtistID = GetLatestArtistID() + 1;

            Artist a = null;
            try {
                a = ArtistFactory.MakeArtist(ArtistID, name, filename);
            } catch {}

            return a;
        }
        public static int GetLatestArtistID () {
            return ArtistRepo.GetLatestID();
        }
    }
}
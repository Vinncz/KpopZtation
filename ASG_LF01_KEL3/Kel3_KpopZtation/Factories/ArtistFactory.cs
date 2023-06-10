using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Factories {
    public class ArtistFactory {
        public static Artist MakeArtist (int ArtistID, string ArtistName, string ArtistImage) {
            return new Artist() {
                ArtistID = ArtistID,
                ArtistName = ArtistName,
                ArtistImage = ArtistImage
            };
        }

        public static Artist MakeArtistModel () {
            return new Artist();
        }
    }
}
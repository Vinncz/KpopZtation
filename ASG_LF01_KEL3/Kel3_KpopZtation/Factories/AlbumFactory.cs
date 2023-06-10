using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Factories
{
    public class AlbumFactory
    {
        public static Album MakeAlbum (int AlbumID, string AlbumName, string AlbumDescription, string AlbumCoverName, int AlbumPrice, int AlbumStock, int ArtistID) {
            return new Album() {
                AlbumID = AlbumID,
                AlbumName = AlbumName,
                AlbumDescription = AlbumDescription,
                AlbumImage = AlbumCoverName,
                AlbumPrice = AlbumPrice,
                AlbumStock = AlbumStock,
                ArtistID = ArtistID
            };
        }

        public static Album MakeAlbumModel () {
            return new Album();
        }
    }
}
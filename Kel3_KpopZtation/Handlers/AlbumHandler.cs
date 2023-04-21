using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Handlers {
    public class AlbumHandler {
        public static Album ExistByID (int AlbumID) {
            return AlbumRepo.Find(AlbumID);
        }
        public static void InsertAlbum (Album a) {
            AlbumRepo.Insert(a);
        }
        public static Album MakeAlbum (string AlbumName, string AlbumDescription, int AlbumPrice, int AlbumStock, string AlbumCoverName, int ArtistID) {
            int LatestID = AlbumRepo.GetLatestID() + 1;

            Album a = null;
            try {
                a = AlbumFactory.MakeAlbum(LatestID, AlbumName, AlbumDescription, AlbumCoverName, AlbumPrice, AlbumStock, ArtistID);
            } catch {}

            return a;
        }
        public static void DeleteAssociatedAlbum (int ArtistID) {
            AlbumRepo.DeleteByArtistID(ArtistID);
        }
    }
}
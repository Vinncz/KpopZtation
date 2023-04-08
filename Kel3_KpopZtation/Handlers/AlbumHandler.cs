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

        public static void InsertAlbum (Album a) {
            AlbumRepo.InsertAlbum(a);
        }
        public static Album MakeAlbum (string AlbumName, string AlbumDescription, int AlbumPrice, int AlbumStock, string AlbumCoverName, int ArtistID) {
            int LatestID = AlbumRepo.GetLatestID() + 1;

            Album a = null;
            try {
                a = AlbumFactory.MakeAlbum(LatestID, AlbumName, AlbumDescription, AlbumCoverName, AlbumPrice, AlbumStock, ArtistID);
            } catch {}

            return a;
        }
        public static bool EditAlbum (int AlbumID, string NewAlbumName, string NewAlbumDescription, int NewAlbumPrice, int NewAlbumStock, string NewAlbumCoverName, bool ImageIsEmpty) {
            Album a = AlbumRepo.ExistByID(AlbumID);
            if (a == null)
                return false;

            if (a.AlbumName != NewAlbumName) 
                a.AlbumName = NewAlbumName;

            if (a.AlbumDescription != NewAlbumDescription)
                a.AlbumName = NewAlbumDescription;

            if (a.AlbumPrice != NewAlbumPrice)
                a.AlbumPrice = NewAlbumPrice;

            if (a.AlbumStock != NewAlbumStock)
                a.AlbumPrice = NewAlbumStock;

            if (ImageIsEmpty == false)
                if (a.AlbumImage != NewAlbumCoverName)
                    a.AlbumImage = NewAlbumCoverName;

            AlbumRepo.save();
            return true;
        }
        public static void DeleteAssociatedAlbum (int ArtistID) {
            AlbumRepo.RemoveByArtistID(ArtistID);
        }
    }
}
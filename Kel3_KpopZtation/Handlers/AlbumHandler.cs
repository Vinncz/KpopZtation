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
        public static Album MakeAlbum (string AlbumName, string AlbumDescription, int AlbumPrice, int AlbumStock, string AlbumCoverName, int ArtistID) {
            int LatestID = AlbumRepo.GetLatestID() + 1;

            Album a = null;
            try {
                a = AlbumFactory.MakeAlbum(LatestID, AlbumName, AlbumDescription, AlbumCoverName, AlbumPrice, AlbumStock, ArtistID);
            } catch {}

            return a;
        }
        public static int CountStock (int AlbumID) {
            Album a = AlbumRepo.Find(AlbumID);
            int AlbumStock = a.AlbumStock;

            Customer c = AuthController.ExtractCustomer();
            List<TransactionHeader> headers = TransactionRepo.SelectHeader(c.CustomerID);

            foreach (TransactionHeader header in headers) {
                foreach (TransactionDetail detail in header.TransactionDetails) {
                    if (detail.AlbumID == a.AlbumID) {
                        AlbumStock -= detail.Quantity;
                    }
                }
            }

            return AlbumStock;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Handlers {
    public class TransactionHandler {
        public static bool MakeTransaction (List<Cart> CartItems) {
            if (CartItems.Count <= 0) return false;

            int TransactionID = TransactionRepo.GetLatestID() + 1;

            TransactionHeader Header = MakeHeader(CartItems, TransactionID);
            List<TransactionDetail> ExtractedDetails = ExtractCorespondingDetails(CartItems, TransactionID);
            
            TransactionRepo.Insert(Header);
            TransactionRepo.Insert(ExtractedDetails);

            return true;
        }
        private static TransactionHeader MakeHeader (List<Cart> CartItems, int TransactionID) {
            TransactionHeader th = new TransactionHeader();
            int CustomerID = CartItems.First().CustomerID;
            HandleHeader(th, TransactionID, CustomerID);

            return th;
        }
        private static List<TransactionDetail> ExtractCorespondingDetails(List<Cart> CartItems, int TransactionID) {
            List<TransactionDetail> Details = new List<TransactionDetail>();

            foreach (Cart c in CartItems) {
                TransactionDetail td = new TransactionDetail();

                int AlbumID = c.AlbumID;
                int Quantity = c.Quantity;

                HandleDetails(td, TransactionID, AlbumID, Quantity);
                Details.Add(td);
            }

            return Details;
        }
        private static void HandleDetails(TransactionDetail td, int TransactionID, int AlbumID, int Quantity) {
            td.TransactionID = TransactionID;
            td.Quantity = Quantity;
            td.AlbumID = AlbumID;
        }
        private static void HandleHeader(TransactionHeader th, int TransactionID, int CustomerID) {
            th.TransactionID = TransactionID;
            th.TransactionDate = DateTime.Now;
            th.CustomerID = CustomerID;
        }
        public static bool Delete ( int TargetTransactionID ) {
            if ( TransactionRepo.DeleteDetail(TargetTransactionID) ) {
                if ( TransactionRepo.DeleteHeader(TargetTransactionID) ) {
                    return true;
                }
            }

            return false;
        }
    }
}
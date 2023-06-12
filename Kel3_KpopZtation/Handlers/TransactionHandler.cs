using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Datasets;
using Kel3_KpopZtation.Factories;

namespace Kel3_KpopZtation.Handlers {
    public class TransactionHandler {

        /* REPORT RELATED */
        public static KpopDataset MakeReportData () {
            List<TransactionHeader> transactions = TransactionRepo.SelectHeader();

            KpopDataset ds = new KpopDataset();
            var tHeader = ds.tHeader;
            var tDetail = ds.tDetail;
            MakeReportHeader(transactions, tHeader, tDetail);

            return ds;
        }
        private static void MakeReportHeader ( List<TransactionHeader> transactions, KpopDataset.tHeaderDataTable tHeader, KpopDataset.tDetailDataTable tDetail ) {
            foreach ( TransactionHeader th in transactions ) {
                var h = tHeader.NewRow();

                h["TransactionID"] = th.TransactionID;
                h["CustomerID"] = th.CustomerID;
                h["TransactionDate"] = th.TransactionDate;

                int grand_total = MakeDetailRows(tDetail, th, 0);
                h["GrandPrice"] = grand_total;

                tHeader.Rows.Add(h);
            }
        }
        private static int MakeDetailRows ( KpopDataset.tDetailDataTable tDetail, TransactionHeader th, int grand_total ) {
            foreach ( TransactionDetail td in th.TransactionDetails ) {
                var d = tDetail.NewRow();

                d["TransactionID"] = td.TransactionID;
                d["AlbumName"] = td.Album.AlbumName;
                d["Quantity"] = td.Quantity;
                d["AlbumPrice"] = td.Album.AlbumPrice;

                int subtotal = td.Quantity * td.Album.AlbumPrice; ;
                d["SubTotalPrice"] = subtotal;

                grand_total += subtotal;

                tDetail.Rows.Add(d);
            }

            return grand_total;
        }
        /* END */

        public static bool MakeTransaction (List<Cart> CartItems) {
            if (CartItems.Count <= 0) return false;

            int TransactionID = TransactionRepo.GetLatestID() + 1;

            TransactionHeader Header = MakeHeader(CartItems, TransactionID);
            List<TransactionDetail> ExtractedDetails = ExtractCorespondingDetails(CartItems, TransactionID);
            
            /* Coba masukkan transaksi ke database */
            TransactionRepo.Insert(Header);
            TransactionRepo.Insert(ExtractedDetails);
            bool result = TransactionRepo.Save();

            /* Jika tidak ada kendala saat melakukan transaksi */
            if (result) {
                /* Update stok album yang di checkout */
                AlbumHandler.RecountStock(TransactionID);
            }

            return result;
        }
        private static TransactionHeader MakeHeader (List<Cart> CartItems, int TransactionID) {
            int CustomerID = CartItems.First().CustomerID;
            TransactionHeader th = TransactionFactory.MakeHeader(TransactionID, CustomerID);

            return th;
        }
        private static List<TransactionDetail> ExtractCorespondingDetails(List<Cart> CartItems, int TransactionID) {
            List<TransactionDetail> Details = new List<TransactionDetail>();

            foreach (Cart c in CartItems) {
                int AlbumID = c.AlbumID;
                int Quantity = c.Quantity;

                TransactionDetail td = TransactionFactory.MakeDetail(TransactionID, Quantity, AlbumID);
                Details.Add(td);
            }

            return Details;
        }

        public static void DeleteTransaction (int CustomerID) {
            List<TransactionHeader> ths = TransactionRepo.SelectHeader(CustomerID);
            foreach (TransactionHeader th in ths) {
                TransactionRepo.DeleteDetail(th.TransactionID);
                TransactionRepo.DeleteHeader(th.TransactionID);
            }

        }
     //   public static bool Delete ( int TargetTransactionID ) {
     //       if ( TransactionRepo.DeleteDetail(TargetTransactionID) ) {
     //           if ( TransactionRepo.DeleteHeader(TargetTransactionID) ) {
     //               return true;
     //           }
     //       }
     //
     //       return false;
     //   }
    }
}
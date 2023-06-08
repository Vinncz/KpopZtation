using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Datasets;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public class ReportController {

        public static class TransactionReport {
            public static KpopDataset GetDataset () {
                List<TransactionHeader> transactions = TransactionRepo.SelectHeader();

                KpopDataset ds = new KpopDataset();
                var tHeader = ds.tHeader;
                var tDetail = ds.tDetail;
                MakeHeaderRows(transactions, tHeader, tDetail);

                return ds;
            }

            private static void MakeHeaderRows ( List<TransactionHeader> transactions, KpopDataset.tHeaderDataTable tHeader, KpopDataset.tDetailDataTable tDetail ) {
                foreach ( TransactionHeader th in transactions ) {
                    var h = tHeader.NewRow();
                    h["TransactionID"] = th.TransactionID;
                    h["CustomerID"] = th.CustomerID;
                    h["TransactionDate"] = th.TransactionDate;

                    int grand_total = 0;
                    grand_total = MakeDetailRows(tDetail, th, grand_total);

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
        }

    }
}
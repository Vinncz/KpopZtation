using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Factories {
    public static class TransactionFactory {

        public static TransactionHeader MakeHeader (int TransactionID, int CustomerID) {
            TransactionHeader th = new TransactionHeader();
            th.TransactionID = TransactionID;
            th.CustomerID = CustomerID;
            th.TransactionDate = DateTime.Now;

            return th;
        }

        public static TransactionDetail MakeDetail (int TransactionID, int Quantity, int AlbumID) {
            TransactionDetail td = new TransactionDetail();
            td.TransactionID = TransactionID;
            td.Quantity = Quantity;
            td.AlbumID = AlbumID;

            return td;
        }

    }
}
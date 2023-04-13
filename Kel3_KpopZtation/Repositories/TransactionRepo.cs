using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public class TransactionRepo {
        private static KZDBEntities db = ConnectionMaster.CopyInstance();

        //Create
        public static void InsertHeader (TransactionHeader th) {
            db.TransactionHeaders.Add(th);

            if ( Save() ) {
                System.Diagnostics.Debug.WriteLine("Successfully added header!");
            }
        }
        public static void InsertDetail (TransactionDetail td) {
            db.TransactionDetails.Add(td);

            if ( Save() ) {
                System.Diagnostics.Debug.WriteLine("Successfully added detail!");
            }
        }
        public static void BulkInsertDetail (List<TransactionDetail> Details) {
            foreach (TransactionDetail Detail in Details) {
                db.TransactionDetails.Add(Detail);
            }

            if ( Save() ) {
                System.Diagnostics.Debug.WriteLine("Successfully added detail!");
            }
        }
        //Read
        public static List<TransactionHeader> RetrieveHeader () {
            return (from TransactionHeader in db.TransactionHeaders select TransactionHeader).ToList();
        }
        public static TransactionHeader HeaderExistByID (int TransactionID) {
            return (from TransactionHeader in db.TransactionHeaders 
                    where TransactionHeader.TransactionID == TransactionID 
                    select TransactionHeader).FirstOrDefault();
        }

        public static List<TransactionDetail> RetrieveDetail () {
            return (from TransactionDetail in db.TransactionDetails select TransactionDetail).ToList();
        }
        public static TransactionDetail DetailExistByID (int TransactionID) {
            return (from TransactionDetail in db.TransactionDetails
                    where TransactionDetail.TransactionID == TransactionID 
                    select TransactionDetail).FirstOrDefault();
        }
        //Update

        //Delete

        //Miscl
        public static int GetLatestID () {
            return (from TransactionHeader in db.TransactionHeaders 
                    orderby TransactionHeader.TransactionID descending 
                    select TransactionHeader.TransactionID).FirstOrDefault();
        }

        public static bool Save () {
            try {
                db.SaveChanges();
            } catch {
                return false;
            }

            return true;
        }
    }
}
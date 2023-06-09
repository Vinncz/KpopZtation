using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public class TransactionRepo {
        private static KZDBEntities db = ConnectionMaster.CopyInstance();

        /* Miscellaneous/Overloaded Operations */
        public static int GetLatestID () {
            /* O(log n) algorithm */
            TransactionHeader data = db.TransactionHeaders.OrderByDescending(a => a.TransactionID).FirstOrDefault();
            if (data != null)
                return data.TransactionID;

            return 0;
        }
        public static List<TransactionHeader> SelectHeader (int CustomerID) {
            return (from TransactionHeader in db.TransactionHeaders 
                    where TransactionHeader.CustomerID == CustomerID
                    orderby TransactionHeader.TransactionDate descending
                    select TransactionHeader).ToList();
        }

        /** Has to manually save */
//        public static bool Insert ( List<TransactionDetail> Details ) {
 //           foreach ( TransactionDetail Detail in Details ) {
 //               db.TransactionDetails.Add(Detail);
 //           }

 //           return Save();
 //       }
        public static void Insert ( List<TransactionDetail> Details ) {
            foreach ( TransactionDetail Detail in Details ) {
                db.TransactionDetails.Add(Detail);
            }
        }

        /* CRUD Operations */
        public static List<TransactionHeader> SelectHeader () {
            return (from TransactionHeader in db.TransactionHeaders
                    orderby TransactionHeader.TransactionDate descending
                    select TransactionHeader).ToList();
        }

        public static TransactionHeader FindHeader (int TransactionID) {
            return (from TransactionHeader in db.TransactionHeaders
                    where TransactionHeader.TransactionID == TransactionID
                    select TransactionHeader).FirstOrDefault();
        }
        public static List<TransactionDetail> FindDetail (int TransactionID) {
            return (from TransactionDetail in db.TransactionDetails
                    where TransactionDetail.TransactionID == TransactionID
                    orderby TransactionDetail.Quantity, TransactionDetail.Album.AlbumName ascending
                    select TransactionDetail).ToList();
        }

        /** Has to manually save */
 //       public static bool Insert (TransactionHeader th) {
 //           db.TransactionHeaders.Add(th);
//            return Save();
//        }
        public static void Insert ( TransactionHeader th ) {
            db.TransactionHeaders.Add(th);
        }
        public static bool Insert (TransactionDetail td) {
            db.TransactionDetails.Add(td);
            return Save();
        }

        public static bool Update (int TargetTransactionID) {
            throw new Exception("Update Transaction is not implemented!");

            List<TransactionDetail> TargetDatas = FindDetail(TargetTransactionID);
            return RewriteIfChanged(TargetDatas);
        }
            private static bool RewriteIfChanged ( List<TransactionDetail> TargetDatas ) {
                throw new Exception("Not Implemented!");

                foreach (TransactionDetail td in TargetDatas) {
                    // TODO ...
                }

                return Save();
            }

        public static bool DeleteHeader ( int TargetTransactionID ) {
            TransactionHeader th = FindHeader(TargetTransactionID);
            db.TransactionHeaders.Remove(th);

            return Save();
        }
        public static bool DeleteDetail ( int TargetTransactionID ) {
            List<TransactionDetail> tds = FindDetail(TargetTransactionID);
            foreach ( TransactionDetail td in tds ) {
                db.TransactionDetails.Remove(td);
            }

            return Save();
        }

        public static bool Save () {
            try {
                db.SaveChanges();
                return true;

            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
                PrintError(ex);
                return false;

            }
        }
            private static void PrintError (System.Data.Entity.Validation.DbEntityValidationException ex) {
                foreach (var entityValidationErrors in ex.EntityValidationErrors) {
                    foreach (var validationError in entityValidationErrors.ValidationErrors) {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
    }
}
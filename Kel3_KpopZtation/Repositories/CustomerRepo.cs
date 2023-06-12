using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation.Repositories {
    public class CustomerRepo {
        private static KZDBEntities db = ConnectionMaster.CopyInstance();

        /* Miscellaneous/Overloaded Operations */
        public static int GetLatestID () {
            /* O(log n) algorithm */
            Customer latestCustomer = db.Customers.OrderByDescending(c => c.CustomerID).FirstOrDefault();
            if (latestCustomer != null)
                return latestCustomer.CustomerID;

            return 0;
        }
        public static List<Customer> Find (string CustomerEmail) {
            return (from Customer in db.Customers
                    where Customer.CustomerEmail == CustomerEmail
                    select Customer).ToList();
        }

        /* CRUD Operations */
        public static List<Customer> Select () {
            return (from Customer in db.Customers 
                    orderby Customer.CustomerName ascending
                    select Customer).ToList();
        }
        public static Customer Find (int CustomerID) {
            return (from Customer in db.Customers
                    where Customer.CustomerID == CustomerID
                    select Customer).FirstOrDefault();
        }
        public static bool Insert (Customer c) {
            db.Customers.Add(c);
            return Save();
        }
        public static bool Update (int TargetCustomerID, string NewName, string NewEmail, string NewGender, string NewAddress, string NewPassword) {
            Customer TargetData = Find(TargetCustomerID);
            RewriteIfChanged (NewName, NewEmail, NewGender, NewAddress, NewPassword, TargetData);

            return Save();
        }
            private static void RewriteIfChanged (string NewName, string NewEmail, string NewGender, string NewAddress, string NewPassword, Customer TargetData) {
                if (TargetData.CustomerName != NewName) {
                    TargetData.CustomerName = NewName;
                }

                if (TargetData.CustomerEmail != NewEmail) {
                    TargetData.CustomerEmail = NewEmail;
                }

                if (TargetData.CustomerAddress != NewAddress) {
                    TargetData.CustomerAddress = NewAddress;
                }

                if (TargetData.CustomerGender != NewGender) {
                    TargetData.CustomerGender = NewGender;
                }

                if (TargetData.CustomerPassword != NewPassword) {
                    TargetData.CustomerPassword = NewPassword;
                }
            }
        public static bool Delete (int TargetCustomerID) {
            Customer TargetData = Find(TargetCustomerID);
            db.Customers.Remove(TargetData);

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
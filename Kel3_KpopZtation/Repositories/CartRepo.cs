using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation.Repositories {
    public class CartRepo {
        private static KZDBEntities db = ConnectionMaster.CopyInstance();

        /* Miscellaneous/Overloaded Operations */
        public static Cart Find ( int CustomerID, int AlbumID ) {
            return ( from Cart in db.Carts
                     where Cart.CustomerID == CustomerID && Cart.AlbumID == AlbumID
                     select Cart ).FirstOrDefault();
        }
        public static bool Delete ( int CustomerID, int AlbumID ) {
            Cart ThisPersonsCart = Find(CustomerID, AlbumID);
            db.Carts.Remove(ThisPersonsCart);

            return Save();
        }

        /* CRUD Operations */
        public static List<Cart> Select () {
            return ( from Cart in db.Carts
                     orderby Cart.Quantity ascending
                     select Cart ).ToList();
        }
        public static List<Cart> Find ( int CustomerID ) {
            return ( from Cart in db.Carts
                     where Cart.CustomerID == CustomerID
                     select Cart ).ToList();
        }
        public static bool Insert ( Cart c ) {
            db.Carts.Add(c);
            return Save();
        }
        public static bool Update ( int TargetCustomerID ) {
            throw new Exception("Update cart has not yet implemented!");
            List<Cart> TargetData = Find(TargetCustomerID);
            return RewriteIfChanged(TargetData);
        }
            private static bool RewriteIfChanged ( List<Cart> TargetData ) {
                throw new Exception("Not Implemented!");

                return Save();
            }
        public static bool Delete ( int CustomerID ) {
            List<Cart> TargetData = Find(CustomerID);
            foreach( Cart CustomerCart in TargetData ) {
                db.Carts.Remove(CustomerCart);
            }

            return Save();
        }
        public static bool Save () {
            try {
                db.SaveChanges();
                return true;

            } catch ( System.Data.Entity.Validation.DbEntityValidationException ex ) {
                PrintError(ex);
                return false;

            }
        }
            private static void PrintError ( System.Data.Entity.Validation.DbEntityValidationException ex ) {
                foreach ( var entityValidationErrors in ex.EntityValidationErrors ) {
                    foreach ( var validationError in entityValidationErrors.ValidationErrors ) {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

    }
}
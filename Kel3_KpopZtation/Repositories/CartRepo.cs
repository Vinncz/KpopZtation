using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public class CartRepo {

        private static KZEntities db = ConnectionMaster.CopyInstance();

        public static List<Cart> GetCart (int CustomerID) {
            return (from Cart in db.Carts
                    where Cart.CustomerID == CustomerID
                    select Cart).ToList();
        }

        public static void RemoveFromCart (Customer c, int AlbumID) {
            Cart ThisPersonsCart = db.Carts.Find(c.CustomerID, AlbumID);
            db.Carts.Remove(ThisPersonsCart);
            db.SaveChanges();
        }

        public static void Save () {
            db.SaveChanges();
        } 
    }
}
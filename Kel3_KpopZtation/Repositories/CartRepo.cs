using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Factories;

namespace Kel3_KpopZtation.Repositories {
    public class CartRepo {

        private static KZDBEntities db = ConnectionMaster.CopyInstance();
        public static Cart GetItemFromCart (int CustomerID, int AlbumID) {
            return (from Cart in db.Carts
                    where Cart.CustomerID == CustomerID && Cart.AlbumID == AlbumID
                    select Cart).FirstOrDefault();
        }
        public static void EmptyCart (int CustomerID) {
            List<Cart> CustomersCart = (from Cart in db.Carts where Cart.CustomerID == CustomerID select Cart).ToList();
            foreach (Cart CartContent in CustomersCart) {
                db.Carts.Remove(CartContent);
            }

            Save();
        }
        public static void AddItem (int CustomerID, int AlbumID, int Amount) {
            Cart c = CartFactory.MakeCart(CustomerID, AlbumID, Amount);
            db.Carts.Add(c);
            Save();
        }
        public static List<Cart> GetCart (int CustomerID) {
            return (from Cart in db.Carts
                    where Cart.CustomerID == CustomerID
                    select Cart).ToList();
        }
        public static void RemoveItemFromCart (int CustomerID, int AlbumID) {
            Cart ThisPersonsCart = db.Carts.Find(CustomerID, AlbumID);
            db.Carts.Remove(ThisPersonsCart);
            Save();
        }
        public static void Save () {
            db.SaveChanges();
        } 
    }
}
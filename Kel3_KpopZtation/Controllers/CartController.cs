using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Handlers;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public class CartController {
        public static void UpdateItemFromCart (int CustomerID, int AlbumID, int AdditionalItem) {
            CartHandler.UpdateItemFromCart(CustomerID, AlbumID, AdditionalItem);
        }
        public static void RemoveItemFromCart (int CustomerID, int AlbumID) {
            CartHandler.RemoveItemFromCart(CustomerID, AlbumID);
        }
        public static bool CheckOut (int CustomerID) {
            bool result = TransactionHandler.MakeTransaction(CartRepo.GetCart(CustomerID));
            if (result) {
                CartHandler.EmptyCart(CustomerID);
            }
            return result;
        }
        public static void AddOrUpdateCart (Customer c, int AlbumID, int Amount) {
            if (AlbumID <= 0 || Amount <= 0 || c == null || c.CustomerRole != "Buyer") return;

            Cart ExistForThisItem = CartRepo.GetItemFromCart(c.CustomerID, AlbumID);

            if (ExistForThisItem == null) AddToCart(c.CustomerID, AlbumID, Amount);
            else UpdateItemFromCart(c.CustomerID, AlbumID, Amount);
        }
        public static void AddToCart (int CustomerID, int AlbumID, int Amount) {
            CartRepo.AddItem(CustomerID, AlbumID, Amount);
        }
        public static List<Cart> GetContent (Customer User)  {
            return CartRepo.GetCart(User.CustomerID);
        }
    }
}
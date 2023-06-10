using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Handlers;
using Kel3_KpopZtation.Factories;
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
            bool result = TransactionHandler.MakeTransaction(CartRepo.Find(CustomerID));
            if (result) {
                CartHandler.EmptyCart(CustomerID);
            }
            return result;
        }
        private static bool ValidateItem (int CustomerID) {
            bool valid = true;
            List<Cart> CustomersCart = CartRepo.Find(CustomerID);

            foreach (Cart CartItem in CustomersCart) {
                Album a = AlbumRepo.Find(CartItem.AlbumID);

                bool IsPresent = CartItem != null;
                bool ValidQuantity = CartItem.Quantity <= a.AlbumStock;

                if ( !( IsPresent && ValidQuantity ) ) {
                    valid = false;
                    break;
                }
            }
            
            return valid;
        }
        private static int CountItem (List<Cart> CartItems, int AlbumID) {
            foreach (Cart c in CartItems) {
                // kalo ketemu albumnya, return amt yang ada di cart
                if (c.AlbumID == AlbumID) {
                    return c.Quantity;
                }
            }

            // kalo ga ketemu itemnya di cart
            return 0;
        }
        public static bool AddOrUpdateCart (Customer c, int AlbumID, int RequestedAmount) {
            if (AlbumID <= 0 || RequestedAmount <= 0 || c == null || c.CustomerRole != "Buyer") return false;

            Album a = AlbumRepo.Find(AlbumID);
            // jika amount yang ditambahkan + amount yang udah ada di cart lebih dari stok album
            int AmountInCart = CountItem(GetContent(c), AlbumID);
            System.Diagnostics.Debug.WriteLine("Amount in cart: " + AmountInCart);
            if ( RequestedAmount + AmountInCart > a.AlbumStock) {
                return false;
            }


            Cart ExistForThisItem = CartRepo.Find(c.CustomerID, AlbumID);

            if (ExistForThisItem == null) AddToCart(c.CustomerID, AlbumID, RequestedAmount);
            else UpdateItemFromCart(c.CustomerID, AlbumID, RequestedAmount);
            return true;
        }
        public static void AddToCart (int CustomerID, int AlbumID, int Amount) {
            Cart c = CartFactory.MakeCart(CustomerID, AlbumID, Amount);
            CartRepo.Insert(c);
        }
        public static List<Cart> GetContent (Customer User)  {
            return CartRepo.Find(User.CustomerID);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public class CartController {
        public static void AddToCart (Customer c, int AlbumID, int Amount) {
            if (Amount <= 0) return;

            CartRepo.AddItem(c.CustomerID, AlbumID, Amount);
        }

        public static List<Cart> GetContent (Customer User)  {
            return CartRepo.GetCart(User.CustomerID);
        }
    }
}
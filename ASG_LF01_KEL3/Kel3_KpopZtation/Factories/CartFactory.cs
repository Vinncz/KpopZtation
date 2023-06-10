using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Factories {
    public class CartFactory {

        public static Cart MakeCart (int CustomerID, int AlbumID, int Amount) {
            Cart c = new Cart();
            c.CustomerID = CustomerID;
            c.AlbumID = AlbumID;
            c.Quantity = Amount;

            return c;
        }

        public static Cart MakeCartModel () {
            return new Cart();
        }
    }
}
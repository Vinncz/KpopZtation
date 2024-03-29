﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Handlers {
    public class CartHandler {
        public static void UpdateItemFromCart (int CustomerID, int AlbumID, int AdditionalItem) {
            Cart c = CartRepo.Find(CustomerID, AlbumID);
            c.Quantity += AdditionalItem;
        }
        public static void RemoveItemFromCart (int CustomerID, int AlbumID) {
            CartRepo.Delete(CustomerID, AlbumID);
        }
        public static void EmptyCart (int CustomerID) {
            CartRepo.Delete(CustomerID);
        }
    }
}
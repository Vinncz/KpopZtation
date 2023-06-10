using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Factories {
    public static class CustomerFactory {
        public static Customer MakeCustomer (int CustomerID, string CustomerName, string CustomerEmail, string CustomerAddress, string CustomerPassword, string CustomerGender, string CustomerRole) {
            return new Customer() {
                CustomerID = CustomerID,
                CustomerName = CustomerName,
                CustomerEmail = CustomerEmail,
                CustomerAddress = CustomerAddress,
                CustomerPassword = CustomerPassword,
                CustomerGender = CustomerGender,
                CustomerRole = CustomerRole
            };
        }

        public static Customer MakeCustomerModel () {
            return new Customer();
        }
    }
}
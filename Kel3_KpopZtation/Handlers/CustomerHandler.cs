using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Factories;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Handlers {
    public class CustomerHandler {

        public static void InsertCustomer (Customer c) {
            CustomerRepo.InsertCustomer(c);
        }

        public static Customer MakeCustomer (string name, string email, string sex, string address, string password, string role) {
            int id = GetLatestCustomerID();

            if (id <= 0) 
                return null;

            Customer c = null;
            try {
                c = CustomerFactory.MakeCustomer(++id, name, email, address, password, sex.ToString(), role);
            } catch {
                c = null;
            }

            return c;
        }

        public static int GetLatestCustomerID () {
            return CustomerRepo.GetLatestCustomerID();
        }

    }
}
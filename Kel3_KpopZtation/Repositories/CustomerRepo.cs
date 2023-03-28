using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories
{
    public class CustomerRepo
    {
        private static KZEntities db = ConnectionMaster.CopyInstance();
        public static Customer ExistByEmailAndPassword(string email, string password) {
            return (from Customer in db.Customers
                    where Customer.CustomerEmail == email && Customer.CustomerPassword == password
                    select Customer).FirstOrDefault();
        }
        public static Customer ExistByID (int ID) {
            return (from Customer in db.Customers 
                    where Customer.CustomerID == ID 
                    select Customer).FirstOrDefault();
        }
        public static Customer ExistByEmail (string Email) {
            return (from Customer in db.Customers
                    where Customer.CustomerEmail == Email
                    select Customer).FirstOrDefault();
        }
        public static List<Customer> Retrieve () {
            return (from Customer in db.Customers
                    select Customer).ToList();
        }
    }
}
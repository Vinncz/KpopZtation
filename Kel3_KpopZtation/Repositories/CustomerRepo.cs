﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public class CustomerRepo {
        private static KZDBEntities db = ConnectionMaster.CopyInstance();
        public static void InsertCustomer (Customer c) {
            try {
                db.Customers.Add(c);
                db.SaveChanges();
            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
        public static Customer EmailPasswordMatch (string email, string password) {
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
        public static int GetLatestCustomerID () {
            return (from Customer in db.Customers 
                    orderby Customer.CustomerID descending 
                    select Customer.CustomerID).FirstOrDefault();
        }
    }
}
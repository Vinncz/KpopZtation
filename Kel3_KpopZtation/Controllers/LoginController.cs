using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers
{
    public class LoginController
    {
        private static CustomerRepo c = new CustomerRepo();
        public bool ValidateEmailAndPassword (string email, string password) {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(email) || email.Trim().Length <= 0)
                isValid = false;
            else {
                if ( CustomerRepo.GetOneByEmail(email) == null ) {
                    isValid = false;
                }
            }

            if (string.IsNullOrWhiteSpace(password) || password.Trim().Length <= 0)
                isValid = false;

            return isValid;
        }

        public Customer SearchMatchingCustomer (string email, string password) {
            return CustomerRepo.GetOneByEmailAndPassword(email, password);
        }
    }
}
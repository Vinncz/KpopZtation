using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers {
    public class TransactionController {
        public static List<TransactionDetail> FindDetail ( int TransactionID ) {
            return TransactionRepo.FindDetail(TransactionID);
        }
    }
}
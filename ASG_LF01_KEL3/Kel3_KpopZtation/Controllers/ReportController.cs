using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Datasets;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Handlers;

namespace Kel3_KpopZtation.Controllers {
    public class ReportController {

        /* Antisipasi jikalau terdapat banyak hal yg memerlukan report */
        public static class TransactionReport {
            public static KpopDataset GetReportData () {
                return TransactionHandler.MakeReportData();
            }
        }

    }
}
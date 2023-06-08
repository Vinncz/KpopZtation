using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Datasets;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views {
    public partial class TransactionReport : System.Web.UI.Page {
        ElementController ec = new ElementController();
        NavigationController nc = new NavigationController();

        protected void Page_Load ( object sender, EventArgs e ) {
            AuthController.MakeSessionFromCookie();
            nc.BlockIfNotAdmin(AuthController.ExtractCustomer(), "TransactionReport.aspx");
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());

            Reports.FINAL_TRANSACTION_REPORT report = new Reports.FINAL_TRANSACTION_REPORT();
            CrystalReportViewer1.ReportSource = report;

            KpopDataset dataset = ReportController.TransactionReport.GetDataset();
            report.SetDataSource(dataset);
        }
    }
}
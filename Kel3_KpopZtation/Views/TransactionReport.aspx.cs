using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Kel3_KpopZtation.Views {
    public partial class TransactionReport : System.Web.UI.Page {
        protected void Page_Load ( object sender, EventArgs e ) {
            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath("~/CrystalReport1.rpt")); // replace with the path to your report file

            // set the data source for the report
            report.SetDataSource(GetMyData()); // replace with your data source method

            // bind the report to the Crystal Report Viewer control
            CrystalReportViewer1.ReportSource = report;
        }

        private DataTable GetMyData () {
            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));
            table.Rows.Add(1, "John", 30);
            table.Rows.Add(2, "Jane", 25);
            table.Rows.Add(3, "Bob", 40);

            if ( table.Columns.Count == 0 )
                System.Diagnostics.Debug.WriteLine("KOSONG ANJING");

            return table;
        }

    }
}
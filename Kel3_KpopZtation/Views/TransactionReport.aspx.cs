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
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views {
    public partial class TransactionReport : System.Web.UI.Page {
        KZDBEntities db = ConnectionMaster.CopyInstance();
        protected void Page_Load ( object sender, EventArgs e ) {
            Reports.FINAL_TRANSACTION_REPORT report = new Reports.FINAL_TRANSACTION_REPORT();
            CrystalReportViewer1.ReportSource = report;

            KpopDataset dataset = GetDataItem(db.TransactionHeaders.ToList());
            report.SetDataSource(dataset);
        }

        private KpopDataset GetDataItem (List<TransactionHeader> transactions) {
            KpopDataset ds = new KpopDataset();
            var tHeader = ds.tHeader;
            var tDetail = ds.tDetail;

            foreach (TransactionHeader th in transactions) {
                var h = tHeader.NewRow();
                h["TransactionID"] = th.TransactionID;
                h["CustomerID"] = th.CustomerID;
                h["TransactionDate"] = th.TransactionDate;

                int grand_total = 0;

                foreach(TransactionDetail td in th.TransactionDetails) {
                    var d = tDetail.NewRow();
                    d["TransactionID"] = td.TransactionID;
                    d["AlbumName"] = td.Album.AlbumName;
                    d["Quantity"] = td.Quantity;
                    d["AlbumPrice"] = td.Album.AlbumPrice;

                    int subtotal = td.Quantity * td.Album.AlbumPrice; ;
                    d["SubTotalPrice"] = subtotal;

                    grand_total += subtotal;

                    tDetail.Rows.Add(d);
                }

                h["GrandPrice"] = grand_total;
                tHeader.Rows.Add(h);
            }

            return ds;
        }

    }
}
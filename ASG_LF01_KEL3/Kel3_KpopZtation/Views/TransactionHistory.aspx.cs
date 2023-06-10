using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Views {
    public partial class TransactionHistory : System.Web.UI.Page {
        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        public static List<TransactionHeader> TransactionHeaders = new List<TransactionHeader>();
        public static List<TransactionDetail> TransactionDetails = new List<TransactionDetail>();

        public static string [] CourierName = {"TUKU", "JENI", "SiLambat", 
                                               "Kirim Aja", "Lempar Aja", "NCS", 
                                               "MoveSend Instant", "MoveSend Sameday", "Pick Express Instant", 
                                               "Pick Express Sameday", "Minim", "LeopardAir Cargo"};
        public static int index = 0;

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockIfNotBuyer(AuthController.ExtractCustomer(), "TransactionHistory.aspx");
            /* END TEMPLATE */

            BindData();
        }

        private void BindData () {
            Customer c = AuthController.ExtractCustomer();
            if ( c == null ) return;

            TransactionHeaders = TransactionRepo.SelectHeader( c.CustomerID );
            BORETransactionList.DataSource = TransactionHeaders;
            BORETransactionList.DataBind();
        }

        protected void OuterRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                Repeater innerRepeater = (Repeater) e.Item.FindControl("BORETransactionListDetail");

                TransactionHeader th = (TransactionHeader) e.Item.DataItem;
                int TransactionID = th.TransactionID;

                TransactionDetails = TransactionController.FindDetail(TransactionID);
                innerRepeater.DataSource = TransactionDetails;
                innerRepeater.DataBind();
            }
        }
    }
}
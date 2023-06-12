using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Controllers;
using System.Web.UI.WebControls;

namespace Kel3_KpopZtation.Views {
    public partial class TransactionDetail : System.Web.UI.Page {
        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        public static TransactionHeader th = null;
        public static List<Models.TransactionDetail> TransactionDetails = new List<Models.TransactionDetail>();

        public static string [] CourierName = {"TUKU", "JENI", "SiLambat",
                                               "Kirim Aja", "Lempar Aja", "NCS",
                                               "MoveSend Instant", "MoveSend Sameday", "Pick Express Instant",
                                               "Pick Express Sameday", "Minim", "LeopardAir Cargo"};
        public static int index = 0;

        public static int TransactionID = 0;
        public static string CustomerName = "Guest";
        public static string TransactionDate = "";
        public static int GrandTotal = 0;


        protected void Page_Load ( object sender, EventArgs e ) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockIfNotBuyer(AuthController.ExtractCustomer(), "TransactionHistory.aspx");
            /* END TEMPLATE */

            TransactionID = Convert.ToInt32(Request.QueryString["id"]);
            if ( TransactionID <= 0 ) {
                nc.Redirect("TransactionHistory.aspx");
                return;
            }

            Initialize();
            BindData(TransactionID);
        }

        private void Initialize() {
            th = TransactionController.FindHeader(TransactionID);

            if ( th != null )
                nc.BlockIfNotTheOwner(AuthController.ExtractCustomer(), th.CustomerID, "TransactionHistory.aspx");
            else
                nc.Redirect("TransactionHistory.aspx");

            CustomerName = th.Customer.CustomerName;
            TransactionDate = th.TransactionDate.ToString("HH:mm:ss - MMMM dd, yyyy");
            GrandTotal = CalculateGrandTotal();
        }

        private static int CalculateGrandTotal () {
            List<Models.TransactionDetail> details = TransactionController.FindDetail(th.TransactionID);
            int GrandTotal = 0;
            foreach (Models.TransactionDetail td in details) {
                GrandTotal += td.Quantity * td.Album.AlbumPrice;
            }

            return GrandTotal;
        }

        protected void BindData ( int TransactionID ) {
            Customer c = AuthController.ExtractCustomer();
            if ( c == null ) return;

            BORETransactionDetail.DataSource = TransactionController.FindDetail(TransactionID);
            BORETransactionDetail.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation.Views {
    public partial class Cart : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        public static List<Models.Cart> CartContent = new List<Models.Cart>();
        public static int TotalPrice = 0;
        public static int ItemCount = 0;

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockIfNotBuyer(AuthController.ExtractCustomer(), "Cart.aspx");
            /* END TEMPLATE */
            
            ec.Invis(LBMessage, DVEmpty, DVno_refresh);

            Reset();
            BindData();

            if ( IsPostBack ) {
                ec.Vis(DVno_refresh);
            }
        }

        protected void DeleteButton_Command(object sender, CommandEventArgs e) {

            if (e.CommandName == "Delete") {
                int ArgAlbumID = Convert.ToInt32(e.CommandArgument);

                Customer c = AuthController.ExtractCustomer();
                if (c == null) return;

                CartController.RemoveItemFromCart(c.CustomerID , ArgAlbumID);

                // Re-bind the data to the Repeater control
                BindData();
                Recount();
            }
        }

        private void BindData () {
            CartContent = CartController.GetContent(AuthController.ExtractCustomer());
            BOREItemsInCart.DataSource = CartContent;
            BOREItemsInCart.DataBind();

            if ( CartContent == null || CartContent.Count() <= 0 ) {
                ec.Vis(DVEmpty);

            } else {
                Recount();
            }
        }

        private void Recount () {
            TotalPrice = 0;
            ItemCount = 0;
            foreach (var item in CartContent) {
                TotalPrice += item.Quantity * item.Album.AlbumPrice;
                ItemCount += item.Quantity;
            }
        } 

        private void Reset () {
            CartContent = new List<Models.Cart>();
            TotalPrice = 0;
            ItemCount = 0;
        }

        protected void BOBTCheckOut_Click(object sender, EventArgs e) {
            Customer c = AuthController.ExtractCustomer();
            if (c == null) Response.Redirect("Home.aspx");
            
            bool CheckedOutSuccessfully = CartController.CheckOut(c.CustomerID);

            ec.Vis(LBMessage);
            if (CheckedOutSuccessfully) {
                LBMessage.Text = "Successfully checked out!";
                Reset();
                BindData();
            } else {
                LBMessage.Text = "Failed to check out!";
            }
        }
    }
}
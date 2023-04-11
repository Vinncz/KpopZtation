using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Repositories;
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

            Reset();
            BindData();

            if (CartContent == null || CartContent.Count() <= 0) {
                ec.Vis();

            } else {
                Recount();
            }
        }

        protected void DeleteButton_Command(object sender, CommandEventArgs e) {

            if (e.CommandName == "Delete") {
                int ArgAlbumID = Convert.ToInt32(e.CommandArgument);
                // Delete the album with the specified ID from your data source
                CartRepo.RemoveFromCart(AuthController.ExtractCustomer(), ArgAlbumID);

                // Re-bind the data to the Repeater control
                BindData();
            }
        }

        private void BindData () {
            CartContent = CartController.GetContent(AuthController.ExtractCustomer());
            BOREItemsInCart.DataSource = CartContent;
            BOREItemsInCart.DataBind();
        }

        private void Recount () {
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Handlers;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views {
    public partial class ViewAlbum : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        public int ArtistID = 0;
        public string ArtistName = "Unknown";
        public int AlbumID = 0;
        public string AlbumImage = "Placeholder.png";
        public string AlbumName = "Album Name";
        public string AlbumDescription = "Album Description";
        public string AlbumPrice = "Rp 0,-";
        public int AlbumStock = 0;
        private Album a = null;

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            /* END TEMPLATE */

            AlbumID = Convert.ToInt32(Request.QueryString["id"]);

            if (AlbumID <= 0)
                Response.Redirect("Home.aspx");

            if ( !IsPostBack ) {
                ec.Invis(LBMessage);
                RefreshPage();
                BOTBAddedAmount.Text = "1";
            }
        }

        protected void BTSubmit_Click(object sender, EventArgs e) {
            System.Diagnostics.Debug.WriteLine("Album id: " + AlbumID);
            if (AlbumID <= 0) return;

            string AddedAmount = BOTBAddedAmount.Text;

            (bool SufficientStock, string ErrorMsg) = AlbumController.CheckStock(AlbumID, AddedAmount);

            ec.Vis(LBMessage);
            if ( SufficientStock == false || FormatController.TrimLen(ErrorMsg) > 0 ) {
                LBMessage.Text = "<svg width='20' height='20' viewBox='0 0 20 20' fill='none' xmlns='http://www.w3.org/2000/svg'><g clip-path='url(#clip0_635_179)'><rect width='20' height='20' rx='10' fill='#7F2835'/><path d='M10 4.16699V11.2503' stroke='#FFBFD1' stroke-width='1.25'stroke-linecap='round'/><rect x='9.16663' y='13.333' width='1.66667' height='1.66667' rx='0.833333'fill='#FFBFD1'/></g><defs><clipPath id='clip0_635_179'><rect width='20' height='20' fill='white'/></clipPath></defs></svg> <br />";
                LBMessage.Text += ErrorMsg;

            } else {
                CartController.AddOrUpdateCart(AuthController.ExtractCustomer(), AlbumID, Convert.ToInt32(AddedAmount));
                LBMessage.Text = "Added " + AddedAmount + " albums to your cart!";

            }
         
            RefreshPage();
        }

        private void RefreshPage () { 
            a = AlbumController.Find( AlbumID );
            if ( a != null ) {
                AlbumName = a.AlbumName;
                AlbumImage = a.AlbumImage;
                AlbumDescription = a.AlbumDescription;
                AlbumPrice = "Rp " + FormatController.FormatToCurrency(a.AlbumPrice.ToString()) + ",-";
                AlbumStock = a.AlbumStock;
                AlbumID = a.AlbumID;

                ArtistID = a.ArtistID;
                ArtistName = a.Artist.ArtistName;

                __LBAlbumDescription.Text = AlbumDescription;
                __LBAlbumPrice.Text = AlbumPrice.ToString();

                int albumstock = AlbumHandler.CountStock(a.AlbumID);
                __LBAlbumStock.Text = albumstock.ToString();
            }
        }

    }
}
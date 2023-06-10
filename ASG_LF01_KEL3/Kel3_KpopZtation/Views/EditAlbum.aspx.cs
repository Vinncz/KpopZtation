using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Controllers;

namespace Kel3_KpopZtation.Views {
    public partial class EditAlbum : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        public int ArtistID = 0;
        public int AlbumID = 0;
        public string AlbumImage = "Placeholder.png";
        public string AlbumName = "Album Name";
        public string AlbumDescription = "Album Description";
        public int AlbumPrice = 0;
        public int AlbumStock = 0;
        private Album a = null;

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockIfNotAdmin(AuthController.ExtractCustomer(), "EditArtist.aspx");
            /* END TEMPLATE */

            AlbumID = Convert.ToInt32(Request.QueryString["id"]);
            if (AlbumID <= 0)
                Response.Redirect("Home.aspx");

            ec.Invis(LBMessage);
            if ( !IsPostBack ) {
                RefreshPage();

            }

            AOTBAlbumName.Focus();
        }
        protected void BTSubmit_Click(object sender, EventArgs e) {
            if (AlbumID <= 0) return;

            string NewAlbumName = AOTBAlbumName.Text;
            string NewAlbumDesc = AOTBAlbumDescription.Text;
            string NewAlbumPrice = AOTBAlbumPrice.Text;
            string NewAlbumStock = AOTBAlbumStock.Text;
            string NewAlbumCover = AOFUAlbumCover.FileName;
            int NewAlbumCoverSize = AOFUAlbumCover.PostedFile.ContentLength;

            int IntNewAlbumStock = Convert.ToInt32(NewAlbumStock);
            if (IntNewAlbumStock <= 0) {
                ec.Vis(LBMessage);
                LBMessage.Text = "<svg width='20' height='20' viewBox='0 0 20 20' fill='none' xmlns='http://www.w3.org/2000/svg'><g clip-path='url(#clip0_635_179)'><rect width='20' height='20' rx='10' fill='#7F2835'/><path d='M10 4.16699V11.2503' stroke='#FFBFD1' stroke-width='1.25'stroke-linecap='round'/><rect x='9.16663' y='13.333' width='1.66667' height='1.66667' rx='0.833333'fill='#FFBFD1'/></g><defs><clipPath id='clip0_635_179'><rect width='20' height='20' fill='white'/></clipPath></defs></svg> <br />";
                LBMessage.Text += "Cannot update stock to 0. Delete album instead.";

                RefreshPage();
                return;
            }

            (bool updatedSuccessfully, List<string> ErrorMsgs) = AlbumController.UpdateAlbum(AlbumID, NewAlbumName, NewAlbumDesc, NewAlbumPrice, NewAlbumStock, NewAlbumCover, NewAlbumCoverSize);

            ec.Vis(LBMessage);
            if ( updatedSuccessfully == false || ErrorMsgs.Count > 0 ) {
                LBMessage.Text = "<svg width='20' height='20' viewBox='0 0 20 20' fill='none' xmlns='http://www.w3.org/2000/svg'><g clip-path='url(#clip0_635_179)'><rect width='20' height='20' rx='10' fill='#7F2835'/><path d='M10 4.16699V11.2503' stroke='#FFBFD1' stroke-width='1.25'stroke-linecap='round'/><rect x='9.16663' y='13.333' width='1.66667' height='1.66667' rx='0.833333'fill='#FFBFD1'/></g><defs><clipPath id='clip0_635_179'><rect width='20' height='20' fill='white'/></clipPath></defs></svg> <br />";
                LBMessage.Text += ErrorMsgs.Aggregate((current, next) => current + "<br />" + next);

            } else {
                LBMessage.Text = "Updated successfully!";

                if (FormatController.TrimLen(NewAlbumCover) > 0)
                    AOFUAlbumCover.SaveAs(Server.MapPath("~/Assets/Albums/" + NewAlbumCover));

            }
         
            RefreshPage();
        }
        private void RefreshPage () { 
            a = AlbumController.Find( AlbumID );
            if ( a != null ) {
                AlbumName = a.AlbumName;
                AlbumImage = a.AlbumImage;
                AlbumDescription = a.AlbumDescription;
                AlbumPrice = a.AlbumPrice;
                AlbumStock = a.AlbumStock;
                AlbumID = a.AlbumID;

                ArtistID = a.ArtistID;

                AOTBAlbumName.Text = AlbumName;
                AOTBAlbumDescription.Text = AlbumDescription;
                AOTBAlbumPrice.Text = AlbumPrice.ToString();
                AOTBAlbumStock.Text = AlbumStock.ToString();
            }
        }
        protected void BTCancel_Click(object sender, EventArgs e) {
            RefreshPage();
            SyncArtistID();
            Response.Redirect("ViewArtist.aspx?id=" + ArtistID);
        }
        private void SyncArtistID () { 
            if (a != null) {
                ArtistID = a.ArtistID;
            }
        }
    }
}
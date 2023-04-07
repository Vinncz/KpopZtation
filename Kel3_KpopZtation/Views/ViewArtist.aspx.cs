using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Controllers.PageController;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Views {
    public partial class ViewArtist : System.Web.UI.Page {

        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        public int ArtistID = 0;
        public string ArtistName = "";
        public string ArtistImage = "";
        public int ArtistHasNAlbums = 0;
        public List<Album> Albums = new List<Album>();

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            /* END TEMPLATE */

            ArtistID = Convert.ToInt32(Request.QueryString["id"]);
            if (ArtistID <= 0) 
                Response.Redirect("Home.aspx");

            Artist a = ArtistController.GetArtistByID( ArtistID.ToString() );
            if ( a != null ) {
                ArtistName = a.ArtistName;
                ArtistImage = a.ArtistImage;
                ArtistHasNAlbums = a.Albums.Count();
            }

            Refresh();
            AOBTAddNewArtist.HRef = "AddAlbum.aspx?artist_id=" + a.ArtistID;
        }

        protected void DeleteButton_Command(object sender, CommandEventArgs e) {

            if (e.CommandName == "Delete") {
                int albumID = Convert.ToInt32(e.CommandArgument);
                // Delete the artist with the specified ID from your data source
                AlbumRepo.RemoveByID(albumID);

                // Re-bind the data to the Repeater control
                Refresh();
            }
        }

        private void Refresh () {
            Albums = ViewArtistPageController.AssociatedAlbum(ArtistID);
            ArtistsRepeater.DataSource = Albums;
            ArtistsRepeater.DataBind();
        }
    }
}
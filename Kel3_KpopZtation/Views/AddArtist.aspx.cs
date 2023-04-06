using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Controllers.PageController;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Handlers;

namespace Kel3_KpopZtation.Views {
    public partial class AddArtist : System.Web.UI.Page {
        
        private static ElementController ec = new ElementController();
        private static NavigationController nc = new NavigationController();

        public int ArtistID = 0;
        public string ArtistName = "Artist Name";
        public string ArtistImage = "Placeholder.png";
        public int ArtistHasNAlbums = 0;

        protected void Page_Load(object sender, EventArgs e) {
            /* BEGIN TEMPLATE */
            AuthController.MakeSessionFromCookie();
            ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
            nc.BlockIfNotAdmin(AuthController.ExtractCustomer(), "EditArtist.aspx");
            /* END TEMPLATE */

            if (!IsPostBack)
                ec.Invis(LBMessage);
            AOTBNewName.Focus();
        }

        protected void BTSubmit_Click(object sender, EventArgs e) {
            string NewArtistName = AOTBNewName.Text;
            string NewArtistImage = AOFUProfilePicture.FileName;
            int NewArtistImageSize = AOFUProfilePicture.PostedFile.ContentLength;

            (bool updatedSuccessfully, List<string> ErrorMsgs) = ArtistController.MakeArtist(NewArtistName, NewArtistImage, NewArtistImageSize);

            if ( updatedSuccessfully == false || ErrorMsgs.Count > 0 ) {
                ec.Vis(LBMessage);
                LBMessage.Text = "<svg width='20' height='20' viewBox='0 0 20 20' fill='none' xmlns='http://www.w3.org/2000/svg'><g clip-path='url(#clip0_635_179)'><rect width='20' height='20' rx='10' fill='#7F2835'/><path d='M10 4.16699V11.2503' stroke='#FFBFD1' stroke-width='1.25'stroke-linecap='round'/><rect x='9.16663' y='13.333' width='1.66667' height='1.66667' rx='0.833333'fill='#FFBFD1'/></g><defs><clipPath id='clip0_635_179'><rect width='20' height='20' fill='white'/></clipPath></defs></svg> <br />";
                LBMessage.Text += ErrorMsgs.Aggregate((current, next) => current + "<br />" + next);

            } else {
                ec.Vis(LBMessage);
                LBMessage.Text = "Updated successfully!";

                if (FormatController.TrimLen(NewArtistImage) > 0)
                    AOFUProfilePicture.SaveAs(Server.MapPath("~/Assets/Artists/" + NewArtistImage));

                Response.Redirect("Home.aspx");
            }
        }

        protected void BTCancel_Click(object sender, EventArgs e) {
            Response.Redirect("Home.aspx");
        }

        protected void RefreshPage () { 
            Artist a = ArtistController.GetArtistByID(ArtistID.ToString());
            if ( a != null ) {
                ArtistName = a.ArtistName;
                ArtistImage = a.ArtistImage;
                ArtistHasNAlbums = a.Albums.Count();

                AOTBNewName.Text = ArtistName;
            }
        }
    }
}
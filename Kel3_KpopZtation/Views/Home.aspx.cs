using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kel3_KpopZtation.Controllers;
using Kel3_KpopZtation.Controllers.PageController;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Views {
    public partial class Home : System.Web.UI.Page {

        /* 
         * [ IN PROGRESS ] March 31st, 2023 by Kevin
         * Home Page Criteria - Page 7
         * 
         * (X) Able to show every registered artists.
         * ( ) Able to redirect users to Artist Detail Page when a certain action is done.
         * (X) Have Custom Visibility Button:
         *     (X) Insert Button, redirect Admin to Insert Artist Page,
         *         & Invisible to non-Admin
         *     (X) Update Button, redirect Admin to Update Artist Page,
         *         & invisible to non-Admin
         *     (X) Delete Button, delte the selected artist,
         *         & invisible to non-Admin
         */

        ElementController ec = new ElementController();
        public static List<Artist> Artists = null;
        public static string CustomerName = "Guest";

        protected void Page_Load(object sender, EventArgs e) {
            AuthController.MakeSessionFromCookie();
            Customer c = AuthController.ExtractCustomer();

            if (c != null) CustomerName = c.CustomerName;
            ec.PrepareVisibility(Page, c);

            Artists = HomePageController.Retrieve();
            ArtistsRepeater.DataSource = Artists;
            ArtistsRepeater.DataBind();
        }

        protected void DeleteButton_Command(object sender, CommandEventArgs e) {

            if (e.CommandName == "Delete") {
                int artistID = Convert.ToInt32(e.CommandArgument);
                // Delete the artist with the specified ID from your data source
                ArtistRepo.RemoveByID(artistID);

                // Re-bind the data to the Repeater control
                Artists = ArtistRepo.Retrieve();
                ArtistsRepeater.DataSource = Artists;
                ArtistsRepeater.DataBind();
            }
        }

        protected override void Render(HtmlTextWriter writer) {
            ClientScript.RegisterForEventValidation(ArtistsRepeater.UniqueID.ToString());
            base.Render(writer);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Repositories;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Controllers.PageController {
    public class ViewArtistPageController {

        public static List<Album> Retrieve (int ArtistID) {
            return AlbumRepo.RetrieveAssociated(ArtistID);
        } 

    }
}
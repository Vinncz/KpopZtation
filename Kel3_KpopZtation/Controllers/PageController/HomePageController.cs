using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;
using Kel3_KpopZtation.Repositories;

namespace Kel3_KpopZtation.Controllers.PageController {
    public static class HomePageController {

        public static List<Artist> Retrieve () {
            return ArtistRepo.Select();
        } 

    }
}
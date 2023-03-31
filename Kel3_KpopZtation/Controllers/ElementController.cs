using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Controllers {
    public class ElementController {

        public List<Control> FindControlsStartingWithPatterns(Control parent, List<string> patterns) {
            List<Control> controls = new List<Control>();
            foreach (Control c in parent.Controls) {
                if (c.ID != null) {
                    foreach (string pattern in patterns) {
                        if (pattern.StartsWith("__") && c.ID.Length >= 4 && c.ID.Substring(2, 2) == pattern.Substring(2)) {
                            controls.Add(c);
                            break;
                        
                        } else if (c.ID.StartsWith(pattern)) {
                            controls.Add(c);
                            break;
                        }
                    }
                
                } controls.AddRange(FindControlsStartingWithPatterns(c, patterns));
            
            } return controls;
        }

        public void PrepareVisibility (Control Page, Customer c) {
            if ( c != null ) {

                /* Make elements that Guests can't see visible */
                Vis(FindControlsStartingWithPatterns(Page, new List<string> { "_G" }));

                /* Make elemets that LOGGED IN user shouldn't see invisible */
                Invis(FindControlsStartingWithPatterns(Page, new List<string> { "GO", "BO", "AO" }));

                /* Obstruct elements that only a specific role are supposed/not supposed to see */
                if (c.CustomerRole == "Buyer") {
                    /* Make element for Buyer visible */
                    Vis(FindControlsStartingWithPatterns(Page, new List<string> { "BO" }));

                    /* Make element NOT for Buyer invisible */
                    Invis(FindControlsStartingWithPatterns(Page, new List<string> { "_B" }));

                } else if ( c.CustomerRole == "Admin") {
                    /* Make element for admins visible */
                    Vis(FindControlsStartingWithPatterns(Page, new List<string> { "AO" }));

                    /* Make element NOT for admin invisible */
                    Invis(FindControlsStartingWithPatterns(Page, new List<string> { "_A" }));

                }

            } else {
                Vis( FindControlsStartingWithPatterns(Page, new List<string> {"GO"}) );
                Invis( FindControlsStartingWithPatterns(Page, new List<string> {"BO", "AO", "_G"}) );
            
            }
        }

        public void Invis (Control c) {
            c.Visible = false;
        }

        public void Invis (List<Control> c) {
            foreach (Control el in c) {
                el.Visible = false;
            }
        }

        public void Invis (params Control [] c) {
            foreach (Control el in c) {
                el.Visible = false;
            }
        }

        public void Vis (Control c) {
            c.Visible = true;
        }

        public void Vis (params Control [] c) {
            foreach (Control el in c) {
                el.Visible = true;
            }
        }

        public void Vis (List<Control> c) {
            foreach (Control el in c) {
                el.Visible = true;
            }
        }

    }
}
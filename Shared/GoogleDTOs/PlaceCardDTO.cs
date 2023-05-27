using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GoogleDTOs
{
    public class PlaceCardDTO
    {
        public string icon { get; set; }
        public string icon_background_color { get; set; }
        public string name { get; set; }
        public string place_id { get; set; }
        public double rating { get; set; }
        public double price_level { get; set; }

    }
}

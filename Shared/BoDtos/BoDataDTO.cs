using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BoDtos
{
    public class BoDataDTO
    {
        public Guid UserId { get; set; }
        public string BusinessName { get; set; }
        public string Type { get; set; }
        public string ContactNumber { get; set; }
        public string Location { get; set; }
        public string AgeLimit { get; set; }
        public string About { get; set; }

    }
}

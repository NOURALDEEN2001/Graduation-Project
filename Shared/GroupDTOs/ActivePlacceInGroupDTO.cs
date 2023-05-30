using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GroupDTOs
{
    public class ActivePlacceInGroupDTO
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string PlaceId { get; set; }
        public DateTime HangOutDate { get; set; }
    }
}

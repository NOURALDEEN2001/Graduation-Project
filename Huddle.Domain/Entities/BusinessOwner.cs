using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class BusinessOwner : EventPlanner
    {
        public string BusinessName { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<FollowedBusinessOwner> FollowedBusinessOwners { get; set; } = new List<FollowedBusinessOwner>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class Consumer: User
    {
        public string DOB { get; set; }
        public string PreferredTimeFrom { get; set; }
        public string PreferredTimeTo { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<FollowedBusinessOwner> FollowedBusinessOwners { get; set; } = new List<FollowedBusinessOwner>();
        public ICollection<Activity> Activities{ get; set; } = new List<Activity>();

    }
}

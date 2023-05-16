using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class EventPlanner : User
    {
        public string ContactNumber { get; set; }
        public string Location { get; set; }
        public string AgeLimit { get; set; }
        public float Rate { get; set; }
        public float PriceRate { get; set; }
        public int TrendingCounter { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<FollowedEventPlanner> FollowedEventPlanners { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string ContactNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public Guid EventPlannerId { get; set; }
        [ForeignKey("EventPlannerId")]
        public EventPlanner EventPlanner { get; set; }
        public ICollection<FollowedEvent> FollowedEvents { get; set; }

    }
}

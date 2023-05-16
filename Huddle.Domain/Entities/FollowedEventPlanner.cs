using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class FollowedEventPlanner
    {
        public Guid Id { get; set; }
        public Guid ConsumerId { get; set; }
        [ForeignKey("ConsumerId")]
        public Consumer Consumer { get; set; }
        public Guid EventPlannerId { get; set; }
        [ForeignKey("EventPlannerId")]
        public EventPlanner EventPlanner { get; set; }

    }
}

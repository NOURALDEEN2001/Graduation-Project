using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class UserContribution
    {
        public Guid ConsumerId { get; set; }
        [ForeignKey("ConsumerId")]
        public Consumer consumer { get; set; }
        public Guid GroupId { get; set; } 
        [ForeignKey("GroupId")]
        public Group group { get; set; }
        public string PlaceId { get; set; }
        public int? Contribution { get; set; }

    }
}

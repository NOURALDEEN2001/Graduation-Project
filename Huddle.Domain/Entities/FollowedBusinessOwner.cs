using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class FollowedBusinessOwner
    {
        public Guid Id { get; set; }
        public Guid BusinessOwnerId { get; set; }
        [ForeignKey("BusinessOwnerId")]
        public BusinessOwner BusinessOwner { get; set; }
        public Guid ConsumerId { get; set; }
        [ForeignKey("ConsumerId")]
        public Consumer Consumer { get; set; }

    }
}

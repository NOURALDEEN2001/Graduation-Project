using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class ConsumerActivity
    {
        public Guid ConsumerId { get; set; }
        [ForeignKey("ConsumerId")]
        public Consumer Consumer { get; set; }
        public Guid ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
      

    }
}

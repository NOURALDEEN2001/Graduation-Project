using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class GroupConsumer
    {
        [Key]
        [Column(Order = 1)]
        public string Id => $"{GroupId}{ConsumerId}";
        public Guid ConsumerId { get; set; }
        [ForeignKey("ConsumerId")]
        public Consumer Consumer { get; set; }
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        

    }
}

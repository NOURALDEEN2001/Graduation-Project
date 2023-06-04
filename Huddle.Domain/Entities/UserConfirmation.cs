using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class UserConfirmation
    {
        public Guid ConsumerId { get; set; }
        [ForeignKey("ConsumerId")]
        public Consumer consumer { get; set; }
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group group { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}

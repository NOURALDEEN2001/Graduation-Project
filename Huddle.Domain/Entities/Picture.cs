using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class Picture
    {
        public Guid Id { get; set; }
        public string Type { get; set; } 
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        
    }
}

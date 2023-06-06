using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GroupAdmin { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HashNumber { get; set; }
        public ICollection<Consumer> Consumers { get; set; } = new List<Consumer>();
        public string? Status { get; set; }
    } 
}

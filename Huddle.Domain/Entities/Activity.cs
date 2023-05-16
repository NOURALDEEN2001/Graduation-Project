using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string ActivityType { get; set; }
        public ICollection<Consumer> Consumers { get; set; } = new List<Consumer>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BoDtos
{
    public class EventDTO
    {
        public Guid eventPlannerId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}

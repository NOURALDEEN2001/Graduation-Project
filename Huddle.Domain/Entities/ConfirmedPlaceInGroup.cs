using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class ConfirmedPlaceInGroup
    {
        [Key]
        public Guid GroupId { get; set; }
        public string PlaceId { get; set; }
        public int? InCount { get; set; }
        public int? OutCount { get; set; }
        public DateTime HangOutDate { get; set; }

    }
}

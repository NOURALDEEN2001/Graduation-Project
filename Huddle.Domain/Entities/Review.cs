using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public string ReviewDescription { get; set; }
        public float ReviewStars { get; set; }
        public float PriceStars { get; set; }
        public Guid ReviewerID { get; set; }
        [ForeignKey("ReviewerID")]
        public Consumer Consumer { get; set; }
        public Guid? EventId { get; set; }
        [ForeignKey("EventId")]
        public Event? Event { get; set; }
        public Guid? BusinessOwnerId { get; set; }
        [ForeignKey("BusinessOwnerId")]
        public BusinessOwner? businessOwner { get; set; }
    }
}

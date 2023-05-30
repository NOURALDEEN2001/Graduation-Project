using GoogleApi.Entities.Places.Search.NearBy.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class ActivePlaceInGroup
    {

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public Consumer Consumer { get; set; }
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        public string PlaceId { get; set; }
        public DateTime HangOutDate { get; set; }

    }
}

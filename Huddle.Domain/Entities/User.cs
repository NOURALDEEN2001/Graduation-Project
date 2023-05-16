using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string About { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}

using Huddle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GroupDTOs
{
    public class GetUserGroupsDTO
    {
        public Group Group { get; set; }
        public bool? IsConfirmed { get; set; }

    }
}

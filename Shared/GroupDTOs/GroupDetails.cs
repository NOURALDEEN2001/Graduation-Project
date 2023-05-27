using Huddle.Domain.Entities;
using Shared.GoogleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GroupDTOs
{
    public class GroupDetails
    {
        public List<UserInfo> UserInfos { get; set; }
        public List<PlaceCardDTO> ActivePlaces { get; set; }
    }
}

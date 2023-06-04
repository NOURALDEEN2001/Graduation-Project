using GoogleApi.Entities.Places.Details.Response;
using Huddle.Domain.Entities;
using Newtonsoft.Json.Linq;
using Shared.GoogleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GroupDTOs
{
    public class GroupDetailsDTO
    {
        public List<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
        public bool? RequesterIsConfirmed { get; set; }
        public List<PlaceInGroupDetails> ActivePlaces { get; set; } = new List<PlaceInGroupDetails>();

    }
}

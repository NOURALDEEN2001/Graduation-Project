using AutoMapper;
using Huddle.Domain.Entities;
using Shared.GroupDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Profiles.GroupProfiles
{
    public class ActivePlaceInGroupProfile : Profile
    {
        public ActivePlaceInGroupProfile()
        {
            CreateMap<ActivePlacceInGroupDTO, ActivePlaceInGroup>()
                .ForMember(dest => dest.Group, opt => opt.Ignore())
                .ForMember(dest => dest.Consumer, opt => opt.Ignore());
        }
    }
}

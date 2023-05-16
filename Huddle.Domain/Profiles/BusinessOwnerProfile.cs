using AutoMapper;
using Huddle.Domain.Entities;
using Shared.RegistrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Profiles
{
    public class BusinessOwnerProfile : Profile
    {
        public BusinessOwnerProfile()
        {
            CreateMap<RegisterBusinessOwnerDTO,BusinessOwner>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Fname + src.Lname))
                .ReverseMap();
        }
    }
}

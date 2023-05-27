using AutoMapper;
using Huddle.Domain.Entities;
using Huddle.Domain.RegistrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Profiles
{
    public class UserProfile: Profile 
    {
        public UserProfile()
        {
            CreateMap<RegisterConsumerDTO, User>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Fname + src.Lname))
                .ReverseMap();
        }
    }
}

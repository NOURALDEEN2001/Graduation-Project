using AutoMapper;
using Huddle.Domain.Entities;
using Shared.RegistrationDTOs;

namespace Shared.Profiles
{
    public class ConsumerProfile : Profile
    {
        public ConsumerProfile()
        {
            CreateMap<RegisterConsumerDTO, Consumer>()
                .ForMember(dest => 
                    dest.UserName,
                    opt => opt.MapFrom(src => src.Fname + src.Lname))
                .ReverseMap();
        }
    }
}

using AutoMapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Huddle.Domain.Entities;
using Huddle.Domain.Context;
using Huddle.Domain.RegistrationDTOs;

namespace Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private UserManager<User> _userManager;
        private readonly HuddleContext _context;
        private readonly IMapper _mapper;
        public UserRepository(UserManager<User> userManager, IMapper mapper, HuddleContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserManagerResponse<string>> RegisterConsumer(RegisterConsumerDTO consumerDTO)
        {
            if (consumerDTO == null)
                throw new NullReferenceException("Consumer Registeration Model is Null");

            var consumer = _mapper.Map<Consumer>(consumerDTO);
            var result = await _userManager.CreateAsync(consumer, consumerDTO.Password);
            if (result.Succeeded)
            {
                return new UserManagerResponse<string>
                {
                    Message = "Consumer created successfully!",
                    IsSuccess = true,
                };
            }

            var userManagerResponse = new UserManagerResponse<string>()
            {
                Message = "Consumer did not creat",
                IsSuccess = false,
            };
            userManagerResponse.Errors.AddRange(result.Errors.Select(e => e.Description));
            return userManagerResponse;
        }

        public async Task<UserManagerResponse<string>> RegisterEventPlanner(RegisterEventPlannerDTO registerEventPlannerDTO)
        {
            if (registerEventPlannerDTO == null)
                throw new NullReferenceException("EventPlanner Registration Model is Null");
            var eventPlanner = _mapper.Map<EventPlanner>(registerEventPlannerDTO);
            var result = await _userManager.CreateAsync(eventPlanner, registerEventPlannerDTO.Password);
            if (result.Succeeded)
            {
                return new UserManagerResponse<string>
                {
                    Message = "EventPlanner created successfully!",
                    IsSuccess = true,
                };
            }
          
            var userManagerResponse = new UserManagerResponse<string>()
            {
                Message = "EventPlanner did not creat",
                IsSuccess = false,
            };
            userManagerResponse.Errors.AddRange(result.Errors.Select(e => e.Description));
            return userManagerResponse;
        }

        public async Task<UserManagerResponse<string>> RegisterBusinessOwner(RegisterBusinessOwnerDTO registerBusinessOwnerDTO)
        {
            if (registerBusinessOwnerDTO == null)
                throw new NullReferenceException("Business Registration Model is Null");
            var businessOwner = _mapper.Map<BusinessOwner>(registerBusinessOwnerDTO);
            var result = await _userManager.CreateAsync(businessOwner, registerBusinessOwnerDTO.Password);
            if (result.Succeeded)
            {
                return new UserManagerResponse<string>
                {
                    Message = "BusinessOwner created successfully!",
                    IsSuccess = true,
                };
            }
           
            var userManagerResponse = new UserManagerResponse<string>()
            {
                Message = "BusinessOwner did not creat",
                IsSuccess = false,
            };
            userManagerResponse.Errors.AddRange(result.Errors.Select(e => e.Description));
            return userManagerResponse;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using Huddle.Domain.Context;
using Huddle.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared;
using Shared.RegistrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Repositories.ConsumerRepo
{
    public class UserRepository : IUserRepository
    {
        private UserManager<User> _userManager;
        private readonly HuddleContext _context;
        private readonly IMapper _mapper;
        public UserRepository(UserManager<User> userManager,IMapper mapper,HuddleContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserManagerResponse> RegisterConsumer(RegisterConsumerDTO consumerDTO)
        {
            if (consumerDTO == null)
                throw new NullReferenceException("Consumer Registeration Model is Null");

            var consumer = _mapper.Map<Consumer>(consumerDTO);
            var result = await _userManager.CreateAsync(consumer, consumerDTO.Password);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Consumer created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "Consumer did not creat",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> RegisterEventPlanner(RegisterEventPlannerDTO registerEventPlannerDTO)
        {
            if (registerEventPlannerDTO == null)
                throw new NullReferenceException("EventPlanner Registration Model is Null");
            var eventPlanner = _mapper.Map<EventPlanner>(registerEventPlannerDTO);
            var result = await _userManager.CreateAsync(eventPlanner, registerEventPlannerDTO.Password);
            if(result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "EventPlanner created successfully!",
                    IsSuccess = true,
                };
            }
            return new UserManagerResponse
            {
                Message = "EventPlanner did not creat",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> RegisterBusinessOwner(RegisterBusinessOwnerDTO registerBusinessOwnerDTO)
        {
            if (registerBusinessOwnerDTO == null)
                throw new NullReferenceException("Business Registration Model is Null");
            var businessOwner = _mapper.Map<BusinessOwner>(registerBusinessOwnerDTO);
            var result = await _userManager.CreateAsync(businessOwner, registerBusinessOwnerDTO.Password);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "BusinessOwner created successfully!",
                    IsSuccess = true,
                };
            }
            return new UserManagerResponse
            {
                Message = "BusinessOwner did not creat",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }

      
    }
}

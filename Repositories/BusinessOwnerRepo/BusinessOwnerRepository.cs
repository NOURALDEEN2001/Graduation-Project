using Huddle.Domain.Context;
using Huddle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.BoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.BusinessOwnerRepo
{
    public class BusinessOwnerRepository : IBusinessOwnerRepository
    {
        private readonly HuddleContext _context;
        public BusinessOwnerRepository(HuddleContext context)
        {
            _context = context;
        }

        public async Task<UserManagerResponse<string>> AddEvent(EventDTO eventDTO)
        {
            if (eventDTO == null)
            {
                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = "null userId"
                };
            }

            try
            {
                Event eventt = new Event()
                {
                    Id = Guid.NewGuid(),
                    About = eventDTO.About,
                    StartDate = eventDTO.StartDate,
                    DueDate = eventDTO.DueDate,
                    Name = eventDTO.Name,
                    EventPlannerId = eventDTO.eventPlannerId,
                };
                var response = _context.Events.Add(eventt);
                _context.SaveChanges();
                return new UserManagerResponse<string> { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<UserManagerResponse<BusinessOwner>> GetBoData(Guid userId)
        {
            if (userId.Equals(Guid.Empty))
            {
                return new UserManagerResponse<BusinessOwner>
                {
                    IsSuccess = false,
                    Message = "null userId"
                };
            }
            try
            {
                var response = await _context.BusinessOwners.FirstOrDefaultAsync(bo => bo.Id == userId);
                if (response == null) return new UserManagerResponse<BusinessOwner> { IsSuccess = false, Message = "null response form the data base" };
                var userManagerResponse = new UserManagerResponse<BusinessOwner>
                {
                    IsSuccess = true,
                    Message = "success",
                };
                userManagerResponse.Obj.Add(response);
                return userManagerResponse;
            }

            catch (Exception ex)
            {

                return new UserManagerResponse<BusinessOwner>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UserManagerResponse<Event>> GetEvent(Guid eventId)
        {
            if (eventId.Equals(Guid.Empty))
            {
                return new UserManagerResponse<Event>
                {
                    IsSuccess = false,
                    Message = "null userId"
                };
            }
            try
            {
                var response = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);

                if (response == null)
                {
                    var userManagerResponse = new UserManagerResponse<Event> { IsSuccess = true };
                    userManagerResponse.Obj.Add(response);
                    return userManagerResponse;
                }
                return new UserManagerResponse<Event> { IsSuccess = false };
                    
            }
            catch (Exception ex)
            {
                return new UserManagerResponse<Event>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UserManagerResponse<Event>> GetEvents()
        {
            try
            {
                var response = _context.Events.ToList();
                if (response == null) return new UserManagerResponse<Event> { IsSuccess = false };
                var userManagerResponse = new UserManagerResponse<Event> { IsSuccess = true };
                userManagerResponse.Obj.AddRange(response);
                return userManagerResponse;
            }
            catch (Exception ex)
            {
                return new UserManagerResponse<Event>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<UserManagerResponse<string>> RemoveEvent(Guid userId, Guid eventID)
        {
            if (userId.Equals(Guid.Empty))
            {
                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = "null userId"
                };
            }
            try
            {
                var response = await _context.Events.Where(e => e.Id == eventID && e.EventPlannerId == userId).FirstOrDefaultAsync();
                if (response == null) return new UserManagerResponse<string> { IsSuccess = false, };
                _context.Events.Remove(response);
                _context.SaveChanges();
                return new UserManagerResponse<string> { IsSuccess = true };
            }
            catch (Exception ex)
            {

                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UserManagerResponse<string>> UpdateBoData(BoDataDTO boDataDTO)
        {
            if (boDataDTO == null)
            {
                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = "null userId"
                };
            }
            try
            {
                var response = await _context.BusinessOwners.FirstOrDefaultAsync(bo => bo.Id == boDataDTO.UserId);

                response.BusinessName = boDataDTO.BusinessName;
                response.Type = boDataDTO.Type;
                response.ContactNumber = boDataDTO.ContactNumber;
                response.AgeLimit = boDataDTO.AgeLimit;
                response.Location = boDataDTO.Location;
                response.About = boDataDTO.About;
                await _context.SaveChangesAsync();
                
                if (response == null) return new UserManagerResponse<string> { IsSuccess = false, Message = "null response form the data base" };

                var userManagerResponse = new UserManagerResponse<string>
                {
                    IsSuccess = true,
                    Message = "success",
                };
                return userManagerResponse;
            }

            catch (Exception ex)
            {

                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


    }
}

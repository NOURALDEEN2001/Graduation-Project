using Huddle.Domain.Entities;
using Shared;
using Shared.BoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.BusinessOwnerRepo
{
    public interface IBusinessOwnerRepository
    {
        public Task<UserManagerResponse<BusinessOwner>> GetBoData(Guid userId);
        public Task<UserManagerResponse<string>> UpdateBoData(BoDataDTO boDataDTO);
        public Task<UserManagerResponse<string>> AddEvent(EventDTO eventDTO);
        public Task<UserManagerResponse<string>> RemoveEvent(Guid userId, Guid eventID);
        public Task<UserManagerResponse<Event>> GetEvent(Guid eventId);
        public Task<UserManagerResponse<Event>> GetEvents();
    }
}

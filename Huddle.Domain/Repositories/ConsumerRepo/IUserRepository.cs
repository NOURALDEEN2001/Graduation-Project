using Huddle.Domain.Entities;
using Shared;
using Shared.RegistrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Repositories.ConsumerRepo
{
    public interface IUserRepository
    {
        Task<UserManagerResponse> RegisterConsumer(RegisterConsumerDTO consumer);
        Task<UserManagerResponse> RegisterEventPlanner(RegisterEventPlannerDTO eventPlanner);
        Task<UserManagerResponse> RegisterBusinessOwner(RegisterBusinessOwnerDTO businessOwner);
    }
}

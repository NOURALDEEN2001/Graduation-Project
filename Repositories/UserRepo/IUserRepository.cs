using Huddle.Domain.RegistrationDTOs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<UserManagerResponse<string>> RegisterConsumer(RegisterConsumerDTO consumer);
        Task<UserManagerResponse<string>> RegisterEventPlanner(RegisterEventPlannerDTO eventPlanner);
        Task<UserManagerResponse<string>> RegisterBusinessOwner(RegisterBusinessOwnerDTO businessOwner);
    }
}

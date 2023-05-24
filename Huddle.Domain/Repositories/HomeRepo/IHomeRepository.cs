using Huddle.Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Repositories.HomeRepo
{
    public interface IHomeRepository
    {
        public List<string> GetUserPreferences(Guid userId);
        public Task<UserManagerResponse> AddPlaceToGroup(ActivePlaceInGroup activePlaceInGroup);

    }
}

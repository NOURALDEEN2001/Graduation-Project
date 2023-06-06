using Huddle.Domain.Entities;
using Shared;
using Shared.GroupDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.HomeRepo
{
    public interface IHomeRepository
    {
        public List<string> GetUserPreferences(Guid userId);
        public Task<UserManagerResponse<string>> AddPlaceToGroup(ActivePlacceInGroupDTO activePlaceInGroup);
        public Task ResetData();
    }
}

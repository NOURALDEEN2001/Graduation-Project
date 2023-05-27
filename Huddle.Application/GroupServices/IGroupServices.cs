using Shared;
using Shared.GroupDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Application.GroupServices
{
    public interface IGroupServices
    {
        public Task<UserManagerResponse<GroupDetails>> GetGroupDetails(Guid groupId);
    }
}

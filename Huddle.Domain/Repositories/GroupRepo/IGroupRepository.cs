using Huddle.Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Repositories.GroupRepo
{
    public interface IGroupRepository
    {
        public Task<List<Group>> GetConsumerGroups(Guid userId);
        public Task<UserManagerResponse> CreateGroup(Guid userId, string groupName);
        public Task<UserManagerResponse> AddConsumerToGroup(Guid userId, int groupHashNum);
    }
}

using Huddle.Domain.Entities;
using Shared;
using Shared.GroupDTOs;
using Shared.Profiles.GroupProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.GroupRepo
{
    public interface IGroupRepository
    {
        public Task<List<Group>> GetConsumerGroups(Guid userId);
        public Task<UserManagerResponse<string>> CreateGroup(Guid userId, string groupName);
        public Task<UserManagerResponse<string>> AddConsumerToGroup(Guid userId, long groupHashNum);
        public Task<UserManagerResponse<Consumer>> GetGroupMembers(Guid groupId);
        public Task<UserManagerResponse<ActivePlaceInGroup>> GetActivePlaces(Guid groupId);
        public Task<UserManagerResponse<UserConfirmation>> GetIfConfirmed(Guid groupId,Guid consumerId);
        public Task<UserManagerResponse<int>> GetContributionCount(Guid groupId, string placeId, int inOrOut);
        public Task<UserManagerResponse<int?>> CheckIsIn(Guid groupId,string placeId,Guid consumerId);
        public Task<UserManagerResponse<FinaldDcisionPlace>> ConfirmUserInGroup(Guid groupId,Guid consumerId);
    }
}

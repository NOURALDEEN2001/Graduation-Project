﻿using Huddle.Domain.Entities;
using Shared;
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
    }
}

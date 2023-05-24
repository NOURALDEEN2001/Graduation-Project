using Huddle.Domain.Context;
using Huddle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Repositories.GroupRepo
{
    public class GroupRepository : IGroupRepository
    {
        readonly HuddleContext _context;
        public GroupRepository(HuddleContext context)
        {
            _context = context;
        }

        public async Task<UserManagerResponse> AddConsumerToGroup(Guid userId, int groupHashNum)
        {
            if (userId == Guid.Empty || groupHashNum == 0)
                return  new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "user id or group hash number is null",
                };
            try
            {
                var response = _context.Groups.Where(g => g.HashNumber == groupHashNum).FirstOrDefault();
                if (response == null)
                    return new UserManagerResponse
                    {
                        IsSuccess = false,
                        Message = $"There is no group with hash number = {groupHashNum}",
                    };
                GroupConsumer groupConsumerToAdd = new GroupConsumer
                {
                    ConsumerId = userId,
                    GroupId = response.Id,
                };
                var response2 = await _context.GroupConsumers.AddAsync(groupConsumerToAdd);
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "User add to group sccessfully"
                };
            }
            catch(Exception ex)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
             
        }

        public async Task<UserManagerResponse> CreateGroup(Guid userId, string groupName)
        {
            if(userId == Guid.Empty || groupName == null)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "user id or group name is null",
                };
            }
            Guid groupId = Guid.NewGuid();
            Group groupToAdd = new Group
            {
                Name = groupName,
                GroupAdmin = userId,
                Id = groupId,
            };
            var response = await _context.Groups.AddAsync(groupToAdd);

            var groupConsumerToAdd = new GroupConsumer
            {
                ConsumerId = userId,
                GroupId = groupId,
            };
            var response2 = await _context.GroupConsumers.AddAsync(groupConsumerToAdd);

            await _context.SaveChangesAsync();
            if (response.Entity != null && response2.Entity != null)
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Group created successfully"
                };
            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "erroe while create group"
            };
        }

        public async Task<List<Group>> GetConsumerGroups(Guid userId)
        {
            var response =  _context.Groups.FromSql($"SELECT * FROM Groups  where Groups.Id in (SELECT GroupId FROM GroupConsumers WHERE ConsumerId = {userId});").ToList();
            return response;
        }


    }
}

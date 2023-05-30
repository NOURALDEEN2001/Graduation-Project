using Huddle.Domain.Context;
using Huddle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.GroupRepo
{
    public class GroupRepository : IGroupRepository
    {
        readonly HuddleContext _context;
        public GroupRepository(HuddleContext context)
        {
            _context = context;
        }

        public async Task<UserManagerResponse<string>> AddConsumerToGroup(Guid userId, long groupHashNum)
        {
            if (userId == Guid.Empty || groupHashNum == 0)
                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = "user id or group hash number is null",
                };
            try
            {
                var response = _context.Groups.Where(g => g.HashNumber == groupHashNum).FirstOrDefault();
                var response3 = _context.Consumers.Find(userId);
                if (response == null)
                    return new UserManagerResponse<string>
                    {
                        IsSuccess = false,
                        Message = $"There is no group with hash number = {groupHashNum}",
                    };
                if (response3 == null)
                    return new UserManagerResponse<string>
                    {
                        IsSuccess = false,
                        Message = $"There is no user with user Id = {userId}",
                    };
                GroupConsumer groupConsumerToAdd = new GroupConsumer
                {
                    ConsumerId = userId,
                    GroupId = response.Id,
                };
                var response2 = await _context.GroupConsumers.AddAsync(groupConsumerToAdd);
                await _context.SaveChangesAsync();
                return new UserManagerResponse<string>
                {
                    IsSuccess = true,
                    Message = "User add to group sccessfully"
                };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }

        public async Task<UserManagerResponse<string>> CreateGroup(Guid userId, string groupName)
        {
            if (userId == Guid.Empty || groupName == null)
            {
                return new UserManagerResponse<string>
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
                return new UserManagerResponse<string>
                {
                    IsSuccess = true,
                    Message = "Group created successfully"
                };
            return new UserManagerResponse<string>
            {
                IsSuccess = false,
                Message = "erroe while create group"
            };
        }

        public async Task<List<Group>> GetConsumerGroups(Guid userId)
        {
            var response = _context.Groups.FromSql($"SELECT * FROM Groups  where Groups.Id in (SELECT GroupId FROM GroupConsumers WHERE ConsumerId = {userId});").ToList();
            return response;
        }

        public async Task<UserManagerResponse<Consumer>> GetGroupMembers(Guid groupId)
        {
            if (groupId.Equals(Guid.Empty))
                return new UserManagerResponse<Consumer>
                {
                    IsSuccess = false,
                    Message = $"There is no group with this Id = {groupId}"
                };
            try
            {
                var consumerIds = _context.GroupConsumers
                    .Where(gc => gc.GroupId == groupId)
                    .Select(gc => gc.ConsumerId)
                    .ToList();

                var response =  _context.Consumers
                    .Where(c => consumerIds.Contains(c.Id))
                    .ToList();

                if (response == null)
                {
                    return new UserManagerResponse<Consumer>
                    {
                        IsSuccess = false,
                        Message = "There is no members in this group."
                    };
                }
                return new UserManagerResponse<Consumer>
                {
                    IsSuccess = true,
                    Obj = response.ToList()
                };
            }
            catch (Exception ex)
            {

                return new UserManagerResponse<Consumer>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
           
        }

        public async Task<UserManagerResponse<ActivePlaceInGroup>> GetActivePlaces(Guid groupId)
        {
            if (groupId.Equals(Guid.Empty))
                return new UserManagerResponse<ActivePlaceInGroup>
                {
                    IsSuccess = false,
                    Message = "There is no group with this Id"
                };
            try
            {
                var response =  _context.ActivePlacesInGroups.Where(g => g.GroupId == groupId).ToList();
                if(response == null)
                {
                    return new UserManagerResponse<ActivePlaceInGroup>
                    {
                        IsSuccess = false,
                        Message = "There is no Active places in this group."
                    };
                }
                return new UserManagerResponse<ActivePlaceInGroup>
                {
                    IsSuccess = true,
                    Message = "success",
                    Obj = response.ToList()
                };
            }
            catch (Exception ex)
            {

                return new UserManagerResponse<ActivePlaceInGroup>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}

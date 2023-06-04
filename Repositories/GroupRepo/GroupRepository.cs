using GoogleApi.Entities.Search.Common;
using Huddle.Domain.Context;
using Huddle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared;
using Shared.GroupDTOs;
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
            var findUser = await _context.Consumers.FindAsync(userId);
            if (findUser == null) return new UserManagerResponse<string> { IsSuccess = false, Message = $"There is no user with Id = {userId}" };
           
            try
            {
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
            catch (Exception ex)
            {

                return new UserManagerResponse<string> { IsSuccess = false };
            }
            
        }

        public async Task<List<Group>> GetConsumerGroups(Guid userId)
        {
            var response = await _context.Groups.FromSql($"SELECT * FROM Groups  where Groups.Id in (SELECT GroupId FROM GroupConsumers WHERE ConsumerId = {userId});").ToListAsync();
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
                var consumerIds = await _context.GroupConsumers
                    .Where(gc => gc.GroupId == groupId)
                    .Select(gc => gc.ConsumerId)
                    .ToListAsync();

                var response = await _context.Consumers
                    .Where(c => consumerIds.Contains(c.Id))
                    .ToListAsync();

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
                var response = await _context.ActivePlacesInGroups.Where(g => g.GroupId == groupId).ToListAsync();
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

        public async Task<UserManagerResponse<UserConfirmation>> GetIfConfirmed(Guid groupId, Guid consumerId)
        {
            if (groupId.Equals(Guid.Empty) && consumerId.Equals(Guid.Empty))
                return new UserManagerResponse<UserConfirmation>
                {
                    IsSuccess = false,
                    Message = "group Id or consumer Id is Wrong"
                };
            try
            {
                var response = await _context.UserConfirmations.Where(g => g.GroupId == groupId && g.ConsumerId == consumerId).FirstOrDefaultAsync();
                if (response == null)
                {
                    return new UserManagerResponse<UserConfirmation>
                    {
                        IsSuccess = false,
                        Message = "There is no confirmation details for the specified query"
                    };
                }
                var userManagerResponse = new UserManagerResponse<UserConfirmation>();
                userManagerResponse.IsSuccess = true;
                userManagerResponse.Message = "success";
                userManagerResponse.Obj.Add(response);
                return userManagerResponse;
            }
            catch (Exception ex )
            {
                return new UserManagerResponse<UserConfirmation> { IsSuccess = false,Message = ex.Message};
            }
           

        }

        public async Task<UserManagerResponse<int>> GetContributionCount(Guid groupId, string placeId,int inOrOut)
        {
            if(groupId.Equals(Guid.Empty) || placeId == null)
            {
                return new UserManagerResponse<int>
                {
                    IsSuccess = false,
                    Message = "Empty group ID or Place Id"
                };
            }
            try
            {
                var response = await _context.UserContributions.CountAsync(u => u.GroupId == groupId && u.PlaceId == placeId && u.Contribution == inOrOut);

                var userManagerResponse = new UserManagerResponse<int>();
                userManagerResponse.IsSuccess = true;
                userManagerResponse.Message = "Success";
                userManagerResponse.Obj.Add(response);
                return userManagerResponse;
            }
            catch (Exception ex)
            {

                return new UserManagerResponse<int> { IsSuccess = false,Message = ex.Message};
            }
        }

        public async Task<UserManagerResponse<int?>> CheckIsIn(Guid groupId, string placeId, Guid consumerId)
        {
            if (consumerId.Equals(Guid.Empty) || groupId.Equals(Guid.Empty) || placeId == null)
                return new UserManagerResponse<int?>
                {
                    IsSuccess = false,
                    Message = "Group Id ,Place Id or user Id is empty."
                };
            try
            {
                var response = await _context.UserContributions.Where(u => u.GroupId == groupId && u.PlaceId == placeId && u.ConsumerId == consumerId).FirstOrDefaultAsync();
                if (response == null)
                    return new UserManagerResponse<int?>
                    {
                        IsSuccess = false,
                        Message = "there is no data for the specified User about this place contribution."
                    };
                var userManagerResponse = new UserManagerResponse<int?>();
                userManagerResponse.IsSuccess = true;
                userManagerResponse.Obj.Add(response.Contribution);
                return userManagerResponse;
            }
            catch (Exception ex)
            {

                return new UserManagerResponse<int?> { IsSuccess = false,Message = ex.Message};
            }
        }

        public async Task<UserManagerResponse<FinaldDcisionPlace>> ConfirmUserInGroup(Guid groupId, Guid consumerId)
        {
            if (consumerId.Equals(Guid.Empty) || groupId.Equals(Guid.Empty)) return new UserManagerResponse<FinaldDcisionPlace>
            {
                IsSuccess = false,
                Message = "group Id or consumer Id is null"
            };
            try
            {
                List<PlaceContributionCount> placeContributionCounts = new List<PlaceContributionCount>();
                var activePlaces = await _context.ActivePlacesInGroups.Where(g => g.GroupId == groupId).ToListAsync();
                if(activePlaces != null)
                {
                    foreach(var place in activePlaces)
                    {
                        var count0 = await _context.UserContributions
                                    .CountAsync(u => u.PlaceId == place.PlaceId && u.GroupId == groupId && u.Contribution == 0);

                        var count1 = await _context.UserContributions
                                    .CountAsync(u => u.PlaceId == place.PlaceId && u.GroupId == groupId && u.Contribution == 1);

                        var score = count1 - count0;
                        placeContributionCounts.Add(new PlaceContributionCount { PlaceId = place.PlaceId, Score = score });
                    }
                    var sortedList = placeContributionCounts.OrderByDescending(c => c.Score).ToList();
                    var confirmedMembers = await _context.UserConfirmations
                                           .CountAsync(u => u.GroupId == groupId);
                    var unConfirmedMembers = await _context.GroupConsumers.CountAsync(gc => gc.GroupId == groupId) - confirmedMembers;

                    var confirmedInFirstPlaceScore = _context.UserConfirmations
                                .Count(uc => _context.UserContributions
                                .Where(uc => uc.PlaceId == sortedList[0].PlaceId)
                                .Select(uc => uc.GroupId)
                                .Contains(groupId));
                    if (sortedList.Count() >= 2)
                    {
                        if (confirmedInFirstPlaceScore - sortedList[2].Score > 2 * unConfirmedMembers)
                        {
                            var userManagerResponse = new UserManagerResponse<FinaldDcisionPlace>()
                            {
                                IsSuccess = true,
                                Message = "The hangout is decided",
                            };
                            userManagerResponse.Obj.Add(new FinaldDcisionPlace { IsDone = true, PlaceId = sortedList[0].PlaceId });
                            //var removeRemainingPlaces = _context.ActivePlacesInGroups.Where()
                        }
                    }
                    else
                    {
                        if (confirmedInFirstPlaceScore > 2 * unConfirmedMembers)
                        {
                            var userManagerResponse = new UserManagerResponse<FinaldDcisionPlace>()
                            {
                                IsSuccess = true,
                                Message = "The hangout is decided",
                            };
                            userManagerResponse.Obj.Add(new FinaldDcisionPlace { IsDone = true, PlaceId = sortedList[0].PlaceId });
                        }
                    }
                }
                return new UserManagerResponse<FinaldDcisionPlace> { IsSuccess = false, Message = "There is no places for this group" };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse<FinaldDcisionPlace> { IsSuccess = false, Message = ex.Message };
            }

        }
    }
}

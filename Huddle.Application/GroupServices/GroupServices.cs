using Azure;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Places.Details.Response;
using Huddle.Application.GoogleMaps;
using Huddle.Domain.Entities;
using Newtonsoft.Json;
using Repositories.GroupRepo;
using Shared;
using Shared.GroupDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Application.GroupServices
{
    public class GroupServices : IGroupServices
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        public GroupServices(IGroupRepository groupRepository,IGoogleMapsApiService googleMapsApiService)
        {
            _groupRepository = groupRepository;
            _googleMapsApiService = googleMapsApiService;
        }
        
       
        public async Task<UserManagerResponse<GroupDetailsDTO>> GetGroupDetails(Guid groupId,Guid consumerId)
        {
            try
            {
                var groupMembers = await _groupRepository.GetGroupMembers(groupId);
                var activePlaces = await _groupRepository.GetActivePlaces(groupId);
                GroupDetailsDTO groupDetails = new GroupDetailsDTO();
                var userManagerResponse = new UserManagerResponse<GroupDetailsDTO>();

                if (groupMembers.IsSuccess)
                {
                    foreach (var consumer in groupMembers.Obj)
                    {
                        var isConfirmedConsumer = await _groupRepository.GetIfConfirmed(groupId, consumer.Id);
                        bool? isConfirmedConsumerResult;
                        if (isConfirmedConsumer.IsSuccess)
                            isConfirmedConsumerResult = isConfirmedConsumer.Obj[0].IsConfirmed;
                        else
                            isConfirmedConsumerResult = null;

                        groupDetails.UserInfos.Add(new UserInfo
                        {
                            UserId = consumer.Id,
                            Fname = consumer.Fname,
                            Label = (consumer.Fname[0].ToString() + consumer.Lname[0].ToString().ToString()),
                            IsConfirmed = isConfirmedConsumerResult,
                        });
                    }
                }
                else return new UserManagerResponse<GroupDetailsDTO>
                {
                    IsSuccess = false,
                    Message = "There is no members in this group",
                };

                if (activePlaces.IsSuccess)
                {
                    foreach (var place in activePlaces.Obj)
                    {
                        var placeDetails = await _googleMapsApiService.GetPlaceDetails(place.PlaceId);
                        var inCount = await _groupRepository.GetContributionCount(groupId, place.PlaceId, 1);
                        var outCount = await _groupRepository.GetContributionCount(groupId, place.PlaceId, 0);
                        var isIn = await _groupRepository.CheckIsIn(groupId, place.PlaceId, consumerId);
                        PlaceInGroupDetails placeInGroupDetails = new PlaceInGroupDetails();
                        if (!placeDetails.IsSuccess)
                        {
                            placeInGroupDetails.PlaceDetails = placeDetails.Message;
                            userManagerResponse.Errors.Add(placeDetails.Message);
                        }
                        else placeInGroupDetails.PlaceDetails = placeDetails.Obj[0];
                        if (!inCount.IsSuccess)
                        {
                            placeInGroupDetails.InCount = null;
                            userManagerResponse.Errors.Add(inCount.Message);
                        }
                        else placeInGroupDetails.InCount = inCount.Obj[0];
                        if (!outCount.IsSuccess)
                        {
                            placeInGroupDetails.OutCount = null;
                            userManagerResponse.Errors.Add(outCount.Message);
                        }
                        else placeInGroupDetails.OutCount = outCount.Obj[0];
                        if (!isIn.IsSuccess)
                        {
                            placeInGroupDetails.IsIn = null;
                            userManagerResponse.Errors.Add(isIn.Message);
                        }
                        else placeInGroupDetails.IsIn = isIn.Obj[0];

                        groupDetails.ActivePlaces.Add(placeInGroupDetails);
                    }

                    var isConfirmedRequester = await _groupRepository.GetIfConfirmed(groupId, consumerId);
                    if(isConfirmedRequester.IsSuccess)
                        groupDetails.RequesterIsConfirmed = isConfirmedRequester.Obj[0].IsConfirmed;
                    else
                        groupDetails.RequesterIsConfirmed = null;
                    
                    
                    userManagerResponse.IsSuccess = true;
                    userManagerResponse.Message = "Success";
                    userManagerResponse.Obj.Add(groupDetails);
                    return userManagerResponse;
                }
                var groupStatus = await _groupRepository.GetGroupStatus(groupId);
                if (groupStatus.IsSuccess)
                {
                    if (groupStatus.Obj[0] == "Confirmed")
                    {
                        var response = await _groupRepository.GetConfirmedGroup(groupId);
                        if (response.IsSuccess)
                        {
                            var placeDetails = await _googleMapsApiService.GetPlaceDetails(response.Obj[0].PlaceId);
                            PlaceInGroupDetails placeInGroupDetails = new PlaceInGroupDetails();
                            placeInGroupDetails.PlaceDetails = placeDetails.Obj[0];
                            placeInGroupDetails.InCount = response.Obj[0].InCount;
                            placeInGroupDetails.OutCount = response.Obj[0].OutCount;
                            placeInGroupDetails.IsIn = null;
                            groupDetails.ActivePlaces.Add(placeInGroupDetails);

                            userManagerResponse.IsSuccess = true;
                            userManagerResponse.Message = "Success";
                            userManagerResponse.Obj.Add(groupDetails);
                            return userManagerResponse;
                        }
                    }
                }
                return new UserManagerResponse<GroupDetailsDTO>
                {
                    IsSuccess = false,
                    Message = "Faild Fetching group details",
                };

            }
            catch (Exception ex)
            {

                return new UserManagerResponse<GroupDetailsDTO> { IsSuccess = false, Message = ex.Message };
            }
           
        }
    }
}

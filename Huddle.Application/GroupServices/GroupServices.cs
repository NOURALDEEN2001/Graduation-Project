using Azure;
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

        public async Task<UserManagerResponse<GroupDetailsDTO>> GetGroupDetails(Guid groupId)
        {
            try
            {
                var groupMembers = await _groupRepository.GetGroupMembers(groupId);
                var activePlaces = await _groupRepository.GetActivePlaces(groupId);
                GroupDetailsDTO groupDetails = new GroupDetailsDTO();
                if (groupMembers.IsSuccess && activePlaces.IsSuccess)
                {
                    foreach (var consumer in groupMembers.Obj)
                    {
                        groupDetails.UserInfos.Add(new UserInfo
                        {
                            Fname = consumer.Fname,
                            Label = (consumer.Fname[0] + consumer.Lname[0]).ToString()
                        });
                    }

                    foreach (var place in activePlaces.Obj)
                    {
                        var placeDetails = await _googleMapsApiService.GetPlaceDetails(place.PlaceId);
                        if (placeDetails.IsSuccess)
                        {
                            groupDetails.ActivePlaces.Add(placeDetails.Obj[0]);
                        }
                    }
                    var userManagerResponse = new UserManagerResponse<GroupDetailsDTO>();
                    userManagerResponse.IsSuccess = true;
                    userManagerResponse.Message = "Success";
                    userManagerResponse.Obj.Add(groupDetails);
                    return userManagerResponse;
                }
                return new UserManagerResponse<GroupDetailsDTO>
                {
                    IsSuccess = false,
                    Message = "Faild",

                };

            }
            catch (Exception ex)
            {

                return new UserManagerResponse<GroupDetailsDTO> { IsSuccess = false, Message = ex.Message };
            }
           
        }
    }
}

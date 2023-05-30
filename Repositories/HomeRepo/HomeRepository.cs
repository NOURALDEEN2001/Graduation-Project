using AutoMapper;
using Huddle.Domain.Context;
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
    public class HomeRepository : IHomeRepository
    {
        private readonly HuddleContext _context;
        private readonly IMapper _mapper;
        public HomeRepository(HuddleContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<string> GetUserPreferences(Guid userId)
        {
            var consumerActivities = _context.ConsumerActivities.Where(c => c.ConsumerId == userId).ToList();
            List<string> preferences = new List<string>();
            foreach (var consumerActivity in consumerActivities)
            {
                var activity = _context.Activities.Where(a => a.Id == consumerActivity.ActivityId).FirstOrDefault();
                if (activity != null)
                    preferences.Add(activity.ActivityType);
            }

            return preferences;
        }

        public async Task<UserManagerResponse<string>> AddPlaceToGroup(ActivePlacceInGroupDTO activePlaceInGroup)
        {
            if (activePlaceInGroup == null)
                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = "data is null"
                };
            try
            {
                //var mapedActivePlaceInGroup = _mapper.Map<ActivePlaceInGroup>(activePlaceInGroup);
                var mapedActivePlaceInGroup = new ActivePlaceInGroup
                {
                    GroupId = activePlaceInGroup.GroupId,
                    UserId = activePlaceInGroup.UserId,
                    PlaceId = activePlaceInGroup.PlaceId,
                    HangOutDate = activePlaceInGroup.HangOutDate,
                };
                var response = await _context.ActivePlacesInGroups.AddAsync(mapedActivePlaceInGroup);
                if (response == null)
                {
                    return new UserManagerResponse<string>
                    {
                        IsSuccess = false,
                        Message = "failed To add to data base"
                    };
                }
                await _context.SaveChangesAsync();
                return new UserManagerResponse<string>
                {
                    IsSuccess = true,
                    Message = "Added Successfully"
                };
            }
            catch (Exception ex)
            {

                return new UserManagerResponse<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

using Huddle.Domain.Context;
using Huddle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.Repositories.HomeRepo
{
    public class HomeRepository : IHomeRepository
    {
        private readonly HuddleContext _context;
        public HomeRepository(HuddleContext context)
        {
            _context = context;
        }

        public List<string> GetUserPreferences(Guid userId)
        {
            var consumerActivities = _context.ConsumerActivities.Where(c => c.ConsumerId == userId).ToList();
            List<string> preferences = new List<string>();
            foreach(var consumerActivity in consumerActivities)
            {
                var activity = _context.Activities.Where(a => a.Id == consumerActivity.ActivityId).FirstOrDefault();
                if (activity != null)
                    preferences.Add(activity.ActivityType);
            }
            
            return preferences;
        }

        public async Task<UserManagerResponse> AddPlaceToGroup(ActivePlaceInGroup activePlaceInGroup)
        {
            var responce = await _context.ActivePlacesInGroups.AddAsync(activePlaceInGroup);
            if(responce != null)
            {
                await _context.SaveChangesAsync();
                return new UserManagerResponse
                {
                    Message = "Place added successfully",
                    IsSuccess = true,
                };
            }
            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Error when adding A place to a grouop"
            };
        }
    }
}

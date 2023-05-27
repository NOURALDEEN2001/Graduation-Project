using Shared;
using Shared.GoogleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Application.GoogleMaps
{
    public interface IGoogleMapsApiService
    {
        public Task GetPlaceBySearch(string place);
        public Task<string> GetNearByBasedOnPerference(double latitude, double longitude, Guid userId);
        public Task<UserManagerResponse<PlaceCardDTO>> GetPlaceDetails(string placeId);
    }
}

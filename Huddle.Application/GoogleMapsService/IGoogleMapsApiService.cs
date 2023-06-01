using GoogleApi.Entities.Places.Details.Response;
using Newtonsoft.Json.Linq;
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
        public Task<UserManagerResponse<string>> GetPlaceDetails(string placeId);
        public Task<UserManagerResponse<string>> FindPlace(string placeToSearch);
    }
}

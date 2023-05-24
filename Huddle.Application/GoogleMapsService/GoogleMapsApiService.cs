using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Places.Search.NearBy.Response;
using Newtonsoft.Json;
using Huddle.Domain.Repositories.HomeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace Huddle.Application.GoogleMaps
{
    public class GoogleMapsApiService: IGoogleMapsApiService
    {
        private GooglePlaces.Search.TextSearchApi _textSearchApi;
        private GooglePlaces.Search.NearBySearchApi _nearBySearchApi;
        private readonly IHomeRepository _homeRepository;
        public GoogleMapsApiService(GooglePlaces.Search.TextSearchApi textSearchApi,GooglePlaces.Search.NearBySearchApi nearBySearchApi,IHomeRepository homeRepository)
        {
            _textSearchApi = textSearchApi;
            _nearBySearchApi = nearBySearchApi;
            _homeRepository = homeRepository;
        }

        public async Task GetPlaceBySearch(string place)
        {
            var response = await _textSearchApi.QueryAsync(new GoogleApi.Entities.Places.Search.Text.Request.PlacesTextSearchRequest
            {
                Query = place,
                Key = "AIzaSyDCZ1dUovZcLYLZvrGIQc7XBCG8ZKxxSK4"
            });

        }

        public async Task<string> GetNearByBasedOnPerference(double latitude,double longitude, Guid userId)
        {
            var Userpreferences = _homeRepository.GetUserPreferences(userId);
            List<NearByResult> PlacesToReturn = new List<NearByResult>();
            for(int i =0; i< Userpreferences.Count ;i++)
            {
                var response = await _nearBySearchApi.QueryAsync(new GoogleApi.Entities.Places.Search.NearBy.Request.PlacesNearBySearchRequest
                {
                    Key = "AIzaSyDCZ1dUovZcLYLZvrGIQc7XBCG8ZKxxSK4",
                    Location = new Coordinate(31.963261, 35.908798),
                    Radius = 30000,
                    Name = Userpreferences[i]
                });

                PlacesToReturn.AddRange(response.Results.Take(5).ToList());
            }

           var JsonPlaces = JsonConvert.SerializeObject(PlacesToReturn);
           return JsonPlaces;
        }
    }
}

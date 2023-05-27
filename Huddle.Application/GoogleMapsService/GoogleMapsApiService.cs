using AutoMapper;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Places.Search.NearBy.Response;
using Newtonsoft.Json;
using Repositories.HomeRepo;
using Shared;
using Shared.GoogleDTOs;
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
        private GooglePlaces.DetailsApi _detailsApi;
        private readonly IHomeRepository _homeRepository;
        private readonly IMapper _mapper;
        public GoogleMapsApiService(GooglePlaces.Search.TextSearchApi textSearchApi,GooglePlaces.Search.NearBySearchApi nearBySearchApi,
                                    IHomeRepository homeRepository, GooglePlaces.DetailsApi detailsApi,
                                    IMapper mapper)
        {
            _textSearchApi = textSearchApi;
            _nearBySearchApi = nearBySearchApi;
            _homeRepository = homeRepository;
            _detailsApi = detailsApi;
            _mapper = mapper;
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
                    Location = new Coordinate(latitude, longitude),
                    Radius = 30000,
                    Name = Userpreferences[i]
                });

                PlacesToReturn.AddRange(response.Results.Take(5).ToList());
            }

           var JsonPlaces = JsonConvert.SerializeObject(PlacesToReturn);
           return JsonPlaces;
        }

        public async Task<UserManagerResponse<PlaceCardDTO>> GetPlaceDetails(string placeId)
        {
            try
            {
                var response = await _detailsApi.QueryAsync(new GoogleApi.Entities.Places.Details.Request.PlacesDetailsRequest
                {
                    PlaceId = placeId,
                });
                if (response == null) return new UserManagerResponse<PlaceCardDTO>
                {
                    IsSuccess = false,
                    Message = "null response from google APIs"
                };
                var mapedResponse = _mapper.Map<PlaceCardDTO>(response.Result);
                var userManagerResponse = new UserManagerResponse<PlaceCardDTO>();
                userManagerResponse.IsSuccess = true;
                userManagerResponse.Message = "success";
                userManagerResponse.Obj[0] = mapedResponse;
                return userManagerResponse;
            }
            catch (Exception ex)
            {
                return new UserManagerResponse<PlaceCardDTO> { IsSuccess = false, Message = ex.Message };
            }
           
        }
    }
}

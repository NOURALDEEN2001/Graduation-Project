using AutoMapper;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Places.Details.Response;
using GoogleApi.Entities.Places.Search.Find.Request.Enums;
using GoogleApi.Entities.Places.Search.NearBy.Response;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private GooglePlaces.Search.FindSearchApi _findSearchApi;
        private GooglePlaces.DetailsApi _detailsApi;
        private readonly IHomeRepository _homeRepository;
        private readonly IMapper _mapper;
        private static readonly HttpClient _httpClient = new HttpClient();
        public GoogleMapsApiService(GooglePlaces.Search.TextSearchApi textSearchApi,GooglePlaces.Search.NearBySearchApi nearBySearchApi,
                                    IHomeRepository homeRepository, GooglePlaces.DetailsApi detailsApi,
                                    IMapper mapper, GooglePlaces.Search.FindSearchApi findSearchApi)
        {
            _textSearchApi = textSearchApi;
            _nearBySearchApi = nearBySearchApi;
            _homeRepository = homeRepository;
            _detailsApi = detailsApi;
            _mapper = mapper;
            _findSearchApi = findSearchApi;
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
                    Radius = 5000,
                    Name = Userpreferences[i]
                });

                PlacesToReturn.AddRange(response.Results.Take(5).ToList());
            }

           var JsonPlaces = JsonConvert.SerializeObject(PlacesToReturn);
           return JsonPlaces;
        }

        public async Task<UserManagerResponse<string>> GetPlaceDetails(string placeId)
        {
            //try
            //{
            //    var response = await _detailsApi.QueryAsync(new GoogleApi.Entities.Places.Details.Request.PlacesDetailsRequest
            //    {
            //        PlaceId = placeId,
            //        Key = "AIzaSyDCZ1dUovZcLYLZvrGIQc7XBCG8ZKxxSK4"
            //    });
            //    if (response == null) return new UserManagerResponse<DetailsResult>
            //    {
            //        IsSuccess = false,
            //        Message = "null response from google APIs"
            //    };
            //    //var mapedResponse = _mapper.Map<DetailsResult>(response.Result);
            //    var userManagerResponse = new UserManagerResponse<DetailsResult>();
            //    userManagerResponse.IsSuccess = true;
            //    userManagerResponse.Message = "success";
            //    userManagerResponse.Obj.Add(response.Result);
            //    return userManagerResponse;
            //}
            //catch (Exception ex)
            //{
            //    return new UserManagerResponse<DetailsResult> { IsSuccess = false, Message = ex.Message };
            //}
            //var apiKey = "AIzaSyDCZ1dUovZcLYLZvrGIQc7XBCG8ZKxxSK4";
            //string url = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={placeId}&key={apiKey}";
            try
            {
                string URL = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&key=AIzaSyDCZ1dUovZcLYLZvrGIQc7XBCG8ZKxxSK4";
                HttpResponseMessage response = await _httpClient.GetAsync(URL);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                dynamic jsonObject = JsonConvert.DeserializeObject(responseBody);

                string readableString = JsonConvert.SerializeObject(jsonObject);

                var userManagerResponse = new UserManagerResponse<string>
                {
                    IsSuccess = true,
                    Message = "success",
                };
                userManagerResponse.Obj.Add(readableString);
                return userManagerResponse;
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

        public async Task<UserManagerResponse<string>> FindPlace(string placeToSearch)
        {
            try
            {
                if (placeToSearch == null)
                    return new UserManagerResponse<string>
                    {
                        IsSuccess = false,
                        Message = "The requested place is null"
                    };
                var response = _findSearchApi.QueryAsync(new GoogleApi.Entities.Places.Search.Find.Request.PlacesFindSearchRequest
                {
                    Key = "AIzaSyDCZ1dUovZcLYLZvrGIQc7XBCG8ZKxxSK4",
                    Input = placeToSearch,
                    Type = InputType.TextQuery
                });
                var jsonPlace = JsonConvert.SerializeObject(response.Result);
                return new UserManagerResponse<string>
                {
                    IsSuccess = true,
                    Message = jsonPlace,
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


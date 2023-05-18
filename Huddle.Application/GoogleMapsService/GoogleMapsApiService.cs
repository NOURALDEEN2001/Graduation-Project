using GoogleApi;
using GoogleApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Huddle.Application.GoogleMaps
{
    public class GoogleMapsApiService: IGoogleMapsApiService
    {
        private GooglePlaces.Search.TextSearchApi _textSearchApi;


        public GoogleMapsApiService(GooglePlaces.Search.TextSearchApi textSearchApi)
        {
            _textSearchApi = textSearchApi;
        }

        public async Task GetPlaceBySearch(string place)
        {
            var response = await _textSearchApi.QueryAsync(new GoogleApi.Entities.Places.Search.Text.Request.PlacesTextSearchRequest
            {
                Query = place,
                Key = "AIzaSyDCZ1dUovZcLYLZvrGIQc7XBCG8ZKxxSK4"
            });

        }
    }
}

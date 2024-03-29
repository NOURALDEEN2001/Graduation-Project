﻿using Huddle.Application.GoogleMaps;
using Huddle.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.HomeRepo;
using Shared.GroupDTOs;
using Shared.HomeDTOs;
using System.Text.Json;

namespace Huddle.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IGoogleMapsApiService _googleMapsApiService;
        private readonly IHomeRepository _homeRepository;
        public HomeController(IGoogleMapsApiService googleMapsApiService,IHomeRepository homeRepository)
        {
            _googleMapsApiService = googleMapsApiService;
            _homeRepository = homeRepository;
        }

        [HttpPost]
        [Route("GetNearBy")]
        public async Task<IActionResult> GetNearByPlaces([FromBody] HomeCardPlaceRequestDTO homeCardPlaceRequestDTO)
        {
            if(homeCardPlaceRequestDTO == null)
                return BadRequest();
            var response = await _googleMapsApiService.GetNearByBasedOnPerference(homeCardPlaceRequestDTO.latitude, homeCardPlaceRequestDTO.longitude, homeCardPlaceRequestDTO.userId);
            if(response != null)
                return Ok(response);
            return NotFound();
        }

        [HttpPost]
        [Route("AddPlaceToGroup")]
        public async Task<IActionResult> AddPlaceToGroup(ActivePlacceInGroupDTO activePlaceInGroup)
        {
            var response = await _homeRepository.AddPlaceToGroup(activePlaceInGroup);
            if (response.IsSuccess)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);

        }

        [HttpGet]
        [Route("ResetData")]
        public async Task<IActionResult> ResetData()
        {
            await _homeRepository.ResetData();
            return Ok();
        }


    }
}

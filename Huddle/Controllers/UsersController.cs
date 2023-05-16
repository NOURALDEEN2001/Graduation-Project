using Huddle.Application.GoogleMaps;
using Huddle.Domain.Repositories.ConsumerRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.RegistrationDTOs;

namespace Huddle.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        public UsersController(IUserRepository consumerRepository, IGoogleMapsApiService googleMapsApiService)
        {
            _UserRepository = consumerRepository;
            _googleMapsApiService = googleMapsApiService;
        }

        [HttpPost]
        [Route("RegisterConsumer")]
        public async Task<IActionResult> RegisterConsumer([FromBody] RegisterConsumerDTO registerConsumerDTO)
        {
            var result = await _UserRepository.RegisterConsumer(registerConsumerDTO);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        [Route("RegisterEventPlanner")]
        public async Task<IActionResult> RegisterEventPlanner([FromBody] RegisterEventPlannerDTO registerEventPlannerDTO)
        {
            var result = await _UserRepository.RegisterEventPlanner(registerEventPlannerDTO);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        [Route("RegisterBusinessOwner")]
        public async Task<IActionResult> RegisterBusinessOwner([FromBody] RegisterBusinessOwnerDTO registerBusinessOwnerDTO)
        {
            var result = await _UserRepository.RegisterBusinessOwner(registerBusinessOwnerDTO);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetPlaces")]
        public async Task<IActionResult> GetPlaces([FromHeader] string place)
        {
            _googleMapsApiService.GetPlaceBySearch(place);
            return Ok();
        }
    }
}

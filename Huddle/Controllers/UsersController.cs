using Huddle.Application.GoogleMaps;
using Huddle.Application.UserServices;
using Huddle.Domain.Repositories.UserRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.RegistrationDTOs;

namespace Huddle.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        private readonly IUserServices _userServices;
        public UsersController(IUserRepository consumerRepository, IGoogleMapsApiService googleMapsApiService,IUserServices userServices)
        {
            _userRepository = consumerRepository;
            _googleMapsApiService = googleMapsApiService;
            _userServices = userServices;
        }

        [HttpPost]
        [Route("RegisterConsumer")]
        public async Task<IActionResult> RegisterConsumer([FromBody] RegisterConsumerDTO registerConsumerDTO)
        {
            var result = await _userRepository.RegisterConsumer(registerConsumerDTO);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        [Route("RegisterEventPlanner")]
        public async Task<IActionResult> RegisterEventPlanner([FromBody] RegisterEventPlannerDTO registerEventPlannerDTO)
        {
            var result = await _userRepository.RegisterEventPlanner(registerEventPlannerDTO);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        [Route("RegisterBusinessOwner")]
        public async Task<IActionResult> RegisterBusinessOwner([FromBody] RegisterBusinessOwnerDTO registerBusinessOwnerDTO)
        {
            var result = await _userRepository.RegisterBusinessOwner(registerBusinessOwnerDTO);
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

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] string email)
        {
            var response = await _userServices.AuthenticateUser(email);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetName")]
        public IActionResult GetName()
        {
            return Ok("Husam is bitch!");
        }

        [HttpPost]
        [Route("SendName")]
        public IActionResult SendNameTest([FromBody] string name)
        {
            return Ok(name);
        }
    }
}

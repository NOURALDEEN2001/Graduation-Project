using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.GroupRepo;
using Huddle.Domain;
using Huddle.Application.GroupServices;
using Huddle.Domain.Entities;
using Huddle.Application.GoogleMaps;

namespace Huddle.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupServices _groupServices;
        private readonly IGoogleMapsApiService _googleMapsApiService;
        public GroupController(IGroupRepository groupRepository,IGroupServices groupServices,
                               IGoogleMapsApiService googleMapsApiService)
        {
            _groupRepository = groupRepository;
            _groupServices = groupServices;
            _googleMapsApiService = googleMapsApiService;
        }

        [HttpPost]
        [Route("GetConsumerGroups")]
        public async Task<IActionResult> GetUserGroups(Guid userId)
        {
            if(userId != Guid.Empty)
            {
                var response = await _groupRepository.GetConsumerGroups(userId);
                if (response.Count == 0)
                    return Ok("There is no Groups for this user");
                return Ok(response);
            }
            return BadRequest("Wrong user Id");
        }

        [HttpPost]
        [Route("CreateGroup")]
        public async Task<IActionResult> CreateGroup(Guid userId, string GroupName)
        {
            var response = await _groupRepository.CreateGroup(userId, GroupName);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpPost]
        [Route("AddConsumerToGroup")]
        public async Task<IActionResult> AddConsumerToGroup(Guid userId, long hashNum)
        {
            var response = await _groupRepository.AddConsumerToGroup(userId, hashNum);
            if(response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost]
        [Route("GetGroupDetails")]
        public async Task<IActionResult> GetGroupDetails(Guid groupId,Guid consumerId)
        {
            var response = await _groupServices.GetGroupDetails(groupId,consumerId);
            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }
            return BadRequest(response.Message);
        }

        [HttpPost]
        [Route("FindPlace")]
        public async Task<IActionResult> FindPlace(string placeToFind)
        {
            var response = await _googleMapsApiService.FindPlace(placeToFind);
            if (response.IsSuccess)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }
    } 
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.GroupRepo;
using Huddle.Domain;
using Huddle.Application.GroupServices;
using Huddle.Domain.Entities;

namespace Huddle.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupServices _groupServices;
        public GroupController(IGroupRepository groupRepository,IGroupServices groupServices)
        {
            _groupRepository = groupRepository;
            _groupServices = groupServices;
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
        public async Task<IActionResult> GetGroupDetails(Guid groupId)
        {
            var response = await _groupServices.GetGroupDetails(groupId);
            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }
            else
                return BadRequest(response.Message);
        }

        
    } 
}

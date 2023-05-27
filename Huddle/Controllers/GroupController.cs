using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.GroupRepo;
using Huddle.Domain;
namespace Huddle.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
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
    }
}

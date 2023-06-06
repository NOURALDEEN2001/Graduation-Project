using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.BusinessOwnerRepo;
using Shared.BoDtos;

namespace Huddle.Controllers
{
    [Route("api/BusinessOwner")]
    [ApiController]
    public class BusinessOwnerController : ControllerBase
    {
        private readonly IBusinessOwnerRepository _businessOwnerRepository;
        public BusinessOwnerController(IBusinessOwnerRepository businessOwnerRepository)
        {
            _businessOwnerRepository = businessOwnerRepository;
        }

        [HttpPost]
        [Route("GetBoData")]
        public async Task<IActionResult> GetBoData(Guid userId)
        {
            var boData = await _businessOwnerRepository.GetBoData(userId);
            if (boData.IsSuccess)
                return Ok(boData.Obj[0]);
            return BadRequest(boData);
        }

        [HttpPost]
        [Route("AddEvent")]
        public async Task<IActionResult> AddEvent(EventDTO eventDTO)
        {
            var boData = await _businessOwnerRepository.AddEvent(eventDTO);
            if (boData.IsSuccess)
                return Ok();
            return BadRequest(boData);
        }

        [HttpPost]
        [Route("RemoveEvent")]
        public async Task<IActionResult> RemoveEvent(Guid userId, Guid eventID)
        {
            var boData = await _businessOwnerRepository.RemoveEvent(userId, eventID);
            if (boData.IsSuccess)
                return Ok();
            return BadRequest(boData);
        }

        [HttpPost]
        [Route("GetEvent")]
        public async Task<IActionResult> GetEvent(Guid eventId)
        {
            var boData = await _businessOwnerRepository.GetEvent(eventId);
            if (boData.IsSuccess)
                return Ok(boData.Obj[0]);
            return BadRequest(boData);
        }

        [HttpPost]
        [Route("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var boData = await _businessOwnerRepository.GetEvents();
            if (boData.IsSuccess)
                return Ok(boData.Obj);
            return BadRequest(boData);
        }

        [HttpPost]
        [Route("UpdateBoData")]
        public async Task<IActionResult> UpdateBoData(BoDataDTO boDataDTO)
        {
            var boData = await _businessOwnerRepository.UpdateBoData(boDataDTO);
            if (boData.IsSuccess)
                return Ok();
            return BadRequest(boData);
        }
    }
}

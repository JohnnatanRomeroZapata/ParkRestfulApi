using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkCore.Interfaces.IServices;
using ParkCore.Models;
using ParkCore.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailService _trailService;
        private readonly IMapper _mapper;

        public TrailController(ITrailService trailService, IMapper mapper)
        {
            _trailService = trailService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of Trail.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrails()
        {
            ICollection<Trail> trailList = _trailService.GetTrails();
            ICollection<TrailDto> trailDtoList = new List<TrailDto>();

            foreach (var trail in trailList)
            {
                trailDtoList.Add(_mapper.Map<TrailDto>(trail));
            }

            return Ok(trailDtoList);
        }


        /// <summary>
        /// Get individual Trail
        /// </summary>
        /// <param name="trailId">The Id of Trail</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetTrail(int trailId)
        {
            Trail trail = await _trailService.GetTrail(trailId);

            if (trail is null)
            {
                return NotFound();
            }

            TrailDto trailDto = _mapper.Map<TrailDto>(trail);

            return Ok(trailDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateTrail([FromBody] TrailCreateDto trailCreateDto)
        {
            if (trailCreateDto is null)
            {
                return BadRequest(ModelState);
            }

            if (_trailService.ExistsTrailByName(trailCreateDto.Name))
            {
                ModelState.AddModelError("", "Trail already exists");
                return StatusCode(404, ModelState);
            }

            Trail trail = _mapper.Map<Trail>(trailCreateDto);

            if (!(await _trailService.CreateTrail(trail)))
            {
                ModelState.AddModelError("", $"{trail.Name} was not created");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTrail", new { trailId = trail.Id }, trail);
        }



        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailUpdateDto)
        {
            if (trailUpdateDto is null || trailUpdateDto.Id != trailId)
            {
                return BadRequest(ModelState);
            }

            Trail trail = _mapper.Map<Trail>(trailUpdateDto);

            if (!(await _trailService.UpdateTrail(trail)))
            {
                ModelState.AddModelError("", $"{trail.Name} was not updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteTrail(int trailId)
        {
            if (!(await _trailService.ExistsTrailById(trailId)))
            {
                return NotFound();
            }

            Trail trail = await _trailService.GetTrail(trailId);

            if (!(await _trailService.RemoveTrail(trail)))
            {
                ModelState.AddModelError("", $"{trail.Name} was not deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkApi.Models;
using ParkApi.Models.Dtos;
using ParkApi.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        public TrailController(ITrailRepository trailRepository, IMapper mapper)
        {
            _trailRepository = trailRepository;
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
            ICollection<Trail> trailList = _trailRepository.GetTrails();
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
        public IActionResult GetTrail(int trailId)
        {
            Trail trail = _trailRepository.GetTrail(trailId);

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
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailCreateDto)
        {
            if (trailCreateDto is null)
            {
                return BadRequest(ModelState);
            }

            if (_trailRepository.TrailExists(trailCreateDto.Name))
            {
                ModelState.AddModelError("", "Trail already exists");
                return StatusCode(404, ModelState);
            }

            Trail trail = _mapper.Map<Trail>(trailCreateDto);

            if (!_trailRepository.CreateTrail(trail))
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
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailUpdateDto)
        {
            if (trailUpdateDto is null || trailUpdateDto.Id != trailId)
            {
                return BadRequest(ModelState);
            }

            Trail trail = _mapper.Map<Trail>(trailUpdateDto);

            if (!_trailRepository.UpdateTrail(trail))
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
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepository.TrailExists(trailId))
            {
                return NotFound();
            }

            Trail trail = _trailRepository.GetTrail(trailId);

            if (!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"{trail.Name} was not deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

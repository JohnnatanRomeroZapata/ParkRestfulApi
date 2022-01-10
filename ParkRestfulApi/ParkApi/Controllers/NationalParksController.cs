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
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkService _npService;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkService npService, IMapper mapper)
        {
            _npService = npService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of National Parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalParks()
        {
            ICollection<NationalPark> nationalParkList = _npService.GetNationalParks();
            ICollection<NationalParkDto> nationalParkDtoList = new List<NationalParkDto>();

            foreach (var np in nationalParkList)
            {
                nationalParkDtoList.Add(_mapper.Map<NationalParkDto>(np));
            }

            return Ok(nationalParkDtoList);
        }


        /// <summary>
        /// Get individual National Park
        /// </summary>
        /// <param name="nationalParkId">The Id of National Park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetNationalPark(int nationalParkId)
        {
            NationalPark nationalPark = await _npService.GetNationalPark(nationalParkId);

            if(nationalPark is null)
            {
                return NotFound();
            }

            NationalParkDto nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);

            return Ok(nationalParkDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateNationalPark([FromBody]NationalParkDto nationalParkDto)
        {
            if(nationalParkDto is null)
            {
                return BadRequest(ModelState);
            }

            if (_npService.ExistsNationalParkByName(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park already exists");
                return StatusCode(404, ModelState);
            }

            NationalPark nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if (!(await _npService.CreateNationalPark(nationalPark)))
            {
                ModelState.AddModelError("", $"{nationalPark.Name} was not created");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new {version = HttpContext.GetRequestedApiVersion().ToString(),  nationalParkId = nationalPark.Id }, nationalPark);
        }



        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto is null || nationalParkDto.Id != nationalParkId)
            {
                return BadRequest(ModelState);
            }

            NationalPark nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if (!(await _npService.UpdateNationalPark(nationalPark)))
            {
                ModelState.AddModelError("", $"{nationalPark.Name} was not updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "RemoveNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveNationalPark(int nationalParkId)
        {
            if (!(await _npService.ExistsNationalParkById(nationalParkId)))
            {
                return NotFound();
            }

            NationalPark nationalPark = await _npService.GetNationalPark(nationalParkId);

            if (!(await _npService.RemoveNationalPark(nationalPark)))
            {
                ModelState.AddModelError("", $"{nationalPark.Name} was not deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

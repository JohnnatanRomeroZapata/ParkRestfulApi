using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkApi.Models;
using ParkApi.Models.Dtos;
using ParkApi.Repository.IRepositories;
using System.Collections.Generic;

namespace ParkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _npRepository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepository, IMapper mapper)
        {
            _npRepository = npRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            ICollection<NationalPark> nationalParkList = _npRepository.GetNationalParks();
            ICollection<NationalParkDto> nationalParkDtoList = new List<NationalParkDto>();

            foreach (var np in nationalParkList)
            {
                nationalParkDtoList.Add(_mapper.Map<NationalParkDto>(np));
            }

            return Ok(nationalParkDtoList);
        }

        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            NationalPark nationalPark = _npRepository.GetNationalPark(nationalParkId);

            if(nationalPark is null)
            {
                return NotFound();
            }

            NationalParkDto nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);

            return Ok(nationalParkDto);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody]NationalParkDto nationalParkDto)
        {
            if(nationalParkDto is null)
            {
                return BadRequest(ModelState);
            }

            if (_npRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park already exists");
                return StatusCode(404, ModelState);
            }

            NationalPark nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_npRepository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"{nationalPark.Name} was not created");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalPark.Id }, nationalPark);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto is null || nationalParkDto.Id != nationalParkId)
            {
                return BadRequest(ModelState);
            }

            NationalPark nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_npRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"{nationalPark.Name} was not updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_npRepository.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }

            NationalPark nationalPark = _npRepository.GetNationalPark(nationalParkId);

            if (!_npRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"{nationalPark.Name} was not deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

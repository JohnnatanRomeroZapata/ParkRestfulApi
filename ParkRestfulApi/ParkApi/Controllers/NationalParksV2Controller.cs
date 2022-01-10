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
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksV2Controller : ControllerBase
    {
        private readonly INationalParkService _npService;
        private readonly IMapper _mapper;

        public NationalParksV2Controller(INationalParkService npService, IMapper mapper)
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
    }
}

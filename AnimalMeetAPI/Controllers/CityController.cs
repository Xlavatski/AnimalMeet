using AnimalMeetAPI.Models;
using AnimalMeetAPI.Models.Dtos;
using AnimalMeetAPI.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/city")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of cities.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CityDto>))]
        public IActionResult GetCities() 
        {
            var objList = _cityRepository.GetCities();

            var vmDto = _mapper.Map<List<CityDto>>(objList);
            return Ok(vmDto);
        }

        /// <summary>
        /// Get individual city.
        /// </summary>
        /// <param name="cityId"> The Id of city </param>
        /// <returns></returns>
        [HttpGet("{cityId:int}", Name = "GetCity")]
        [ProducesResponseType(200, Type = typeof(CityDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetCity(int cityId)
        {
            var obj = _cityRepository.GetCity(cityId);
            if (obj == null)
            {
                return NotFound();
            }

            var vmDto = _mapper.Map<CityDto>(obj);

            return Ok(vmDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CityDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCity([FromBody] CityDto cityDto)
        {
            if (cityDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_cityRepository.CityExist(cityDto.Name))
            {
                ModelState.AddModelError("", "City Exists!");
                return StatusCode(404, ModelState);
            }

            var cityObj = _mapper.Map<City>(cityDto);
            if (!_cityRepository.CreateCity(cityObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {cityObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCity", new { cityId = cityObj.Id }, cityObj);
        }

        [HttpPatch("{cityId:int}", Name = "UpdateCity")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCity(int cityId, [FromBody] CityDto cityDto)
        {
            if (cityDto == null || cityId != cityDto.Id)
            {
                return BadRequest(ModelState);
            }

            var cityObj = _mapper.Map<City>(cityDto);
            if (!_cityRepository.UpdateCity(cityObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updateing the record {cityObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{cityId:int}", Name = "DeletePark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCity(int cityId)
        {
            if (!_cityRepository.CityExist(cityId))
            {
                return NotFound();
            }

            var cityObj = _cityRepository.GetCity(cityId);
            if (!_cityRepository.DeleteCity(cityObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {cityObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

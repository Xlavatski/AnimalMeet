using AnimalMeetAPI.Models;
using AnimalMeetAPI.Models.Dtos;
using AnimalMeetAPI.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AnimalTypeController : ControllerBase
    {
        private readonly IAnimalTypeRepository _animTypeRepo;
        private readonly IMapper _mapper;
        public AnimalTypeController(IAnimalTypeRepository animTypeRepo, IMapper mapper)
        {
            _animTypeRepo = animTypeRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of animal types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AnimalTypeDto>))]
        public IActionResult GetAnimalTypes()
        {
            var objList = _animTypeRepo.GetAnimalTypes();
            var objDto = new List<AnimalTypeDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<AnimalTypeDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual animal type.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetAnimalType")]
        [ProducesResponseType(200, Type = typeof(AnimalTypeDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetAnimalType(int id)
        {
            var obj = _animTypeRepo.GetAnimalType(id);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<AnimalTypeDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AnimalTypeDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAnimalType([FromBody] AnimalTypeDto animTypeDto)
        {
            if (animTypeDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_animTypeRepo.AnimalTypeExist(animTypeDto.Name))
            {
                ModelState.AddModelError("", "Animal Type Exists!");
                return StatusCode(404, ModelState);
            }

            var animTypeObj = _mapper.Map<AnimalType>(animTypeDto);

            if (!_animTypeRepo.CreateAnimalType(animTypeObj))
            {
                ModelState.AddModelError("", $"something went wrong when saving the record {animTypeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAnimalType", new { id = animTypeObj.Id }, animTypeObj);
        }

        [HttpPatch("{id:int}", Name = "UpdateAnimalType")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAnimalType(int id, [FromBody] AnimalTypeDto animTypeDto)
        {
            if (animTypeDto == null || id != animTypeDto.Id)
            {
                return BadRequest(ModelState);
            }

            var animTypeObj = _mapper.Map<AnimalType>(animTypeDto);
            if (!_animTypeRepo.UpdateAnimalType(animTypeObj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {animTypeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteAnimalType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAnimalType(int id)
        {
            if (!_animTypeRepo.AnimalTypeExist(id))
            {
                return NotFound();
            }

            var animTypeObj = _animTypeRepo.GetAnimalType(id);
            if (!_animTypeRepo.DeleteAnimalType(animTypeObj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting the record {animTypeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

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
    [Route("api/animalsubtype")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AnimalSubTypeController : ControllerBase
    {
        private readonly IAnimalSubTypeRepository _subTypeRepo;
        private readonly IMapper _mapper;
        public AnimalSubTypeController(IAnimalSubTypeRepository subTypeRepo, IMapper mapper)
        {
            _subTypeRepo = subTypeRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of animal subtypes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AnimalSubtypeDto>))]
        public IActionResult GetAnimalSubTypes()
        {
            var objList = _subTypeRepo.GetAnimalSubTypes();
            var objDto = new List<AnimalSubtypeDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<AnimalSubtypeDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual animal subtype.
        /// </summary>
        /// <param name="id">The Id od the animal subtype</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetAnimalSubType")]
        [ProducesResponseType(200, Type = typeof(AnimalSubtypeDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetAnimalSubType(int id)
        {
            var obj = _subTypeRepo.GetAnimalSubType(id);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<AnimalSubtypeDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("[action]/{animTypeId:int}")]
        [ProducesResponseType(200, Type = typeof(AnimalSubtypeDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetSubTypesInAnimalType(int animTypeId)
        {
            var objList = _subTypeRepo.GetSubTypesInAnimalType(animTypeId);
            if (objList == null)
            {
                return NotFound();
            }

            var objDto = new List<AnimalSubtypeDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<AnimalSubtypeDto>(obj));
            }
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AnimalSubtypeDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAnimalSubType([FromBody] AnimalSubtypeCreateDto subTypeDto)
        {
            if (subTypeDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_subTypeRepo.AnimalSubTypeExist(subTypeDto.Name))
            {
                ModelState.AddModelError("", "Animal Subtype Exists!");
                return StatusCode(404, ModelState);
            }

            var subTypeObj = _mapper.Map<AnimalSubtype>(subTypeDto);

            if (!_subTypeRepo.CreateAnimalSubType(subTypeObj))
            {
                ModelState.AddModelError("", $"something went wrong when saving the record {subTypeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAnimalSubType", new { id = subTypeObj.Id }, subTypeDto);
        }

        [HttpPatch("{id:int}", Name = "UpdateAnimalSubType")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAnimalSubType(int id, [FromBody] AnimalSubtypeUpdateDto subTypeDto)
        {
            if (subTypeDto == null || id != subTypeDto.Id)
            {
                return BadRequest(ModelState);
            }

            var subTypeObj = _mapper.Map<AnimalSubtype>(subTypeDto);
            if (!_subTypeRepo.UpdateAnimalSubType(subTypeObj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {subTypeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteAnimalSubType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAnimalSubType(int id)
        {
            if (!_subTypeRepo.AnimalSubTypeExist(id))
            {
                return NotFound();
            }

            var subTypeObj = _subTypeRepo.GetAnimalSubType(id);
            if (!_subTypeRepo.DeleteAnimalSubType(subTypeObj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting the record {subTypeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

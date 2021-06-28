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
    [Route("api/pets")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PetsController : ControllerBase
    {
        private readonly IPetsRepository _petsRepo;
        private readonly IMapper _mapper;
        public PetsController(IPetsRepository petsRepo, IMapper mapper)
        {
            _petsRepo = petsRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of pets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PetsDto>))]
        public IActionResult GetPets()
        {
            var objList = _petsRepo.GetPets();
            var objDto = new List<PetsDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<PetsDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual pet.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetPet")]
        [ProducesResponseType(200, Type = typeof(PetsDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPet(int id)
        {
            var obj = _petsRepo.GetPet(id);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<PetsDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("[action]/{animSubTypeId:int}")]
        [ProducesResponseType(200, Type = typeof(PetsDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPetsInAnimalSubType(int animSubTypeId)
        {
            var objList = _petsRepo.GetPetsInAnimalSubType(animSubTypeId);
            if (objList == null)
            {
                return NotFound();
            }

            var objDto = new List<PetsDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<PetsDto>(obj));
            }
            return Ok(objDto);
        }

        [HttpGet("[action]/{userId:int}")]
        [ProducesResponseType(200, Type = typeof(PetsDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPetsInUser(int userId)
        {
            var objList = _petsRepo.GetPetsInUser(userId);
            if (objList == null)
            {
                return NotFound();
            }

            var objDto = new List<PetsDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<PetsDto>(obj));
            }
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PetsDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePets([FromBody] PetsCreateDto petsDto)
        {
            if (petsDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_petsRepo.PetsExist(petsDto.Name))
            {
                ModelState.AddModelError("", "Pet Exists!");
                return StatusCode(404, ModelState);
            }

            var petObj = _mapper.Map<Pets>(petsDto);

            if (!_petsRepo.CreatePets(petObj))
            {
                ModelState.AddModelError("", $"something went wrong when saving the record {petObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPet", new { id = petObj.Id }, petObj);
        }

        [HttpPatch("{id:int}", Name = "UpdatePets")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePets(int id, [FromBody] PetsUpdateDto petsDto)
        {
            if (petsDto == null || id != petsDto.Id)
            {
                return BadRequest(ModelState);
            }

            var petsObj = _mapper.Map<Pets>(petsDto);
            if (!_petsRepo.UpdatePets(petsObj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {petsObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeletePets")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePets(int id)
        {
            if (!_petsRepo.PetsExist(id))
            {
                return NotFound();
            }

            var petsObj = _petsRepo.GetPet(id);
            if (!_petsRepo.DeletePets(petsObj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting the record {petsObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

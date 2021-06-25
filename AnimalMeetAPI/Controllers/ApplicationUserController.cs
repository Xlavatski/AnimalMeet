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
    public class ApplicationUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ApplicationUserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ApplicationUserDto>))]
        public IActionResult GetUsers()
        {
            var objList = _userRepository.GetUsers();
            var objDto = new List<ApplicationUserDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ApplicationUserDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual user.
        /// </summary>
        /// <param name="id">The Id od the user</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(ApplicationUserDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int id)
        {
            var obj = _userRepository.GetUser(id);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ApplicationUserDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("[action]/{cityId:int}")]
        [ProducesResponseType(200, Type = typeof(ApplicationUserDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUsersInCity(int cityId)
        {

            var objList = _userRepository.GetUsersInCity(cityId);
            if (objList == null)
            {
                return NotFound();
            }

            var objDto = new List<ApplicationUserDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ApplicationUserDto>(obj));
            }
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ApplicationUserDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUser([FromBody] ApplicationUserCreateDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.UserExist(userDto.Name))
            {
                ModelState.AddModelError("", "Application User Exists!");
                return StatusCode(404, ModelState);
            }

            var userObj = _mapper.Map<ApplicationUser>(userDto);

            if (!_userRepository.CreateUser(userObj))
            {
                ModelState.AddModelError("", $"something went wrong when saving the record {userObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUser", new { id = userObj.Id }, userDto);
        }

        [HttpPatch("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int id, [FromBody] ApplicationUserUpdateDto userDto)
        {
            if (userDto == null || id != userDto.Id)
            {
                return BadRequest(ModelState);
            }

            var userObj = _mapper.Map<ApplicationUser>(userDto);
            if (!_userRepository.UpdateUser(userObj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {userObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int id)
        {
            if (!_userRepository.UserExist(id))
            {
                return NotFound();
            }

            var userObj = _userRepository.GetUser(id);
            if (!_userRepository.DeleteUser(userObj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting the record {userObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

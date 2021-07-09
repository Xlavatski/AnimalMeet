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
    [Route("api/authuser")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IAuthUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthUserController(IAuthUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = _userRepository.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            bool ifUserNameUnique = _userRepository.IsUniqueUser(model.Username); 
            if (!ifUserNameUnique)
            {
                return BadRequest(new { message = "Username alredy exists" });
            }

            if (model.Username.Length < 3) 
            {
                return BadRequest(new { message = "Username must contain at least three letters" });
            }


            if (model.Password.Length < 7 || model.Password.Any(char.IsDigit) == false) 
            {
                return BadRequest(new { message = "Password must contain a minimum of seven characters and at least one number" });
            }

            var user = _userRepository.Register(model.Username, model.Password, model.Name, model.Surname, model.City);

            if (user == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }

            return Ok();
        }

        /// <summary>
        /// Get individual user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(ApplicationUserDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetAnimalSubType(int id)
        {
            var obj = _userRepository.GetUser(id);
            if (obj == null) 
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ApplicationUserDto>(obj);
            return Ok(objDto);
        }

    }
}

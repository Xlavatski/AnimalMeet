using AnimalMeetAPI.Models;
using AnimalMeetAPI.Repository.IRepository;
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

        public AuthUserController(IAuthUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            var user = _userRepository.Register(model.Username, model.Password, model.Name, model.Surname, model.City);

            if (user == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }

            return Ok();
        }
    }
}

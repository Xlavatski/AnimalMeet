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
        /// Get list of city.
        /// </summary>
        /// <returns></returns>
    }
}

using AnimalMeetWeb.Models;
using AnimalMeetWeb.Models.ViewModel;
using AnimalMeetWeb.Repository.IRepository;
using AnimalMeetWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Areas.User.Controllers
{
    [Area("User")]
    public class PetsController : Controller
    {
        private readonly IPetsRepository _petsRepo;
        private readonly IUserService _userService;

        public PetsController(IPetsRepository petsRepo, IUserService userService)
        {
            _petsRepo = petsRepo;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(new Pets() { });
        }


        public async Task<IActionResult> GetAllPets() 
        {
            int idUser = _userService.Id;
            return Json(new { data = await _petsRepo.GetAllPetsOfUserAsync(SD.PetsAPIPath + "GetPetsInUser/", idUser, HttpContext.Session.GetString("JWToken")) });
        }

    }
}

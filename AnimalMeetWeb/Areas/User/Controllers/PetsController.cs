using AnimalMeetWeb.Models;
using AnimalMeetWeb.Models.ViewModel;
using AnimalMeetWeb.Repository.IRepository;
using AnimalMeetWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private readonly IAnimalTypeRepository _animalTypeRepo;
        private readonly IAnimalSubTypeRepository _animalSubTypeRepo;

        public PetsController(IPetsRepository petsRepo, IUserService userService, IAnimalTypeRepository animalTypeRepo, IAnimalSubTypeRepository animalSubTypeRepo)
        {
            _petsRepo = petsRepo;
            _userService = userService;

            _animalTypeRepo = animalTypeRepo;
            _animalSubTypeRepo = animalSubTypeRepo;
        }

        public IActionResult Index()
        {
            return View(new Pets() { });
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
            if (id != null) 
            {
                IEnumerable<Pets> PetsList = await _petsRepo.GetAllPetsOfUserAsync(SD.PetsAPIPath + "GetPetsInUser/", id, HttpContext.Session.GetString("JWToken"));
            }

            IEnumerable<AnimalType> AnimTypeList = await _animalTypeRepo.GetAllAsync(SD.AnimalTypeAPIPath, HttpContext.Session.GetString("JWToken"));

            ViewBag.AnimalTypLis = new SelectList(AnimTypeList, "Id", "Name");

            IEnumerable<AnimalSubType> AnimaSubtypeList = await _animalSubTypeRepo.GetAllAsync(SD.AnimalSubTypeAPIPath, HttpContext.Session.GetString("JWToken"));

            ViewBag.AnimalSubTypLis = new SelectList(AnimaSubtypeList, "Id", "Name");

            Pets objPets = new Pets();

            if (id == null) 
            {
                return View(objPets);
            }

            objPets = await _petsRepo.GetAsync(SD.PetsAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (objPets == null) 
            {
                return NotFound();
            }

            return View(objPets);
        }


        public async Task<IActionResult> GetAllPets() 
        {
            int idUser = _userService.Id;
            return Json(new { data = await _petsRepo.GetAllPetsOfUserAsync(SD.PetsAPIPath + "GetPetsInUser/", idUser, HttpContext.Session.GetString("JWToken")) });
        }

    }
}

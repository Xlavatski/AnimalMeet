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
            return View(new PetsIndex() { });
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
            Pets objPets = new Pets();
            int? idUser = _userService.Id;

            IEnumerable<PetsIndex> PetsList = await _petsRepo.GetAllPetsOfUserAsync(SD.PetsAPIPath + "GetPetsInUser/", idUser, HttpContext.Session.GetString("JWToken"));
            //if (PetsList == )
            //{
            //    return View(objPets);
            //}


            IEnumerable<AnimalType> AnimTypeList = await _animalTypeRepo.GetAllAsync(SD.AnimalTypeAPIPath, HttpContext.Session.GetString("JWToken"));

            ViewBag.AnimalTypLis = new SelectList(AnimTypeList, "Id", "Name");

            IEnumerable<AnimalSubType> AnimaSubtypeList = await _animalSubTypeRepo.GetAllAsync(SD.AnimalSubTypeAPIPath, HttpContext.Session.GetString("JWToken"));

            ViewBag.AnimalSubTypLis = new SelectList(AnimaSubtypeList, "Id", "Name");


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

        #region API region
        public async Task<IActionResult> GetAllPets() 
        {
            int idUser = _userService.Id;
            return Json(new { data = await _petsRepo.GetAllPetsOfUserAsync(SD.PetsAPIPath + "GetPetsInUser/", idUser, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpPost]
        public async Task<IActionResult> GetTypeOfSubtype(int? idType)
        {
            IEnumerable<AnimalSubType> objType = await _animalSubTypeRepo.GetAllSubTypesOfTypeAsync(SD.AnimalSubTypeAPIPath + "GetSubTypesInAnimalType/", idType, HttpContext.Session.GetString("JWToken"));
            
            SelectList obgtype = new SelectList(objType, "Id", "Name", 0);
            ViewBag.obgtype = new SelectList(objType, "Id", "Name", 0);
            return Json(obgtype);
        }


        //public async Task<IActionResult> GetTypeOfSubtype(int? idType)
        //{

        //    return Json(new { data = await _animalSubTypeRepo.GetAllSubTypesOfTypeAsync(SD.AnimalSubTypeAPIPath + "GetSubTypesInAnimalType/", idType, HttpContext.Session.GetString("JWToken")) });
        //}

        #endregion
    }
}

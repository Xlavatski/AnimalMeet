using AnimalMeetWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Areas.User.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetsRepository _petsRepo;
        private readonly IAnimalSubTypeRepository _animalSubRepo;
        //ovaj ne bude dobar 
        private readonly IAccountRepository _accoRepo;

        public PetsController(IPetsRepository petsRepo, IAnimalSubTypeRepository animalSubRepo, IAccountRepository accoRepo)
        {
            _petsRepo = petsRepo;
            _animalSubRepo = animalSubRepo;
            _accoRepo = accoRepo;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}

using AnimalMeetWeb.Models;
using AnimalMeetWeb.Models.ViewModel;
using AnimalMeetWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AnimalSubTypeController : Controller
    {
        private readonly IAnimalTypeRepository _animTypeRepo;
        private readonly IAnimalSubTypeRepository _animSubTypeRepo;

        public AnimalSubTypeController(IAnimalTypeRepository animTypeRepo, IAnimalSubTypeRepository animSubTypeRepo)
        {
            _animTypeRepo = animTypeRepo;
            _animSubTypeRepo = animSubTypeRepo;
        }

        public IActionResult Index()
        {
            return View(new AnimalSubType() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<AnimalType> animTypeList = await _animTypeRepo.GetAllAsync(SD.AnimalTypeAPIPath, HttpContext.Session.GetString("JWToken"));

            AnimalSubTypeVM objVM = new AnimalSubTypeVM()
            {
                AnimalTypeList = animTypeList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                AnimalSubType = new AnimalSubType()
            };

            if (id == null)
            {
                return View(objVM);
            }

            objVM.AnimalSubType = await _animSubTypeRepo.GetAsync(SD.AnimalSubTypeAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (objVM.AnimalSubType == null)
            {
                return NotFound();
            }

            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AnimalSubTypeVM obj)
        {
            if (ModelState.IsValid)
            {

                if (obj.AnimalSubType.Id == 0)
                {
                    await _animSubTypeRepo.CreateAsync(SD.AnimalSubTypeAPIPath, obj.AnimalSubType, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _animSubTypeRepo.UpdateAsync(SD.AnimalSubTypeAPIPath + obj.AnimalSubType.Id, obj.AnimalSubType, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<AnimalType> animTypeList = await _animTypeRepo.GetAllAsync(SD.AnimalTypeAPIPath, HttpContext.Session.GetString("JWToken"));

                AnimalSubTypeVM objVM = new AnimalSubTypeVM()
                {
                    AnimalTypeList = animTypeList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    AnimalSubType = obj.AnimalSubType
                };
                return View(objVM);
            }
        }

        public async Task<IActionResult> GetAllAnimalSubType()
        {
            return Json(new { data = await _animSubTypeRepo.GetAllAsync(SD.AnimalSubTypeAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _animSubTypeRepo.DeleteAsync(SD.AnimalSubTypeAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}

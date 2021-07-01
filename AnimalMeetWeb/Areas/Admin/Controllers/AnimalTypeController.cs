using AnimalMeetWeb.Models;
using AnimalMeetWeb.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;

namespace AnimalMeetWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AnimalTypeController : Controller
    {
        private readonly IAnimalTypeRepository _animTypeRepo;
        public AnimalTypeController(IAnimalTypeRepository animTypeRepo)
        {
            _animTypeRepo = animTypeRepo;
        }

        public IActionResult Index()
        {
            return View(new AnimalType() { });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            AnimalType obj = new AnimalType();

            if (id == null)
            {
                return View(obj);
            }

            obj = await _animTypeRepo.GetAsync(SD.AnimalTypeAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AnimalType obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _animTypeRepo.CreateAsync(SD.AnimalTypeAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _animTypeRepo.UpdateAsync(SD.AnimalTypeAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllAnimalType()
        {
            return Json(new { data = await _animTypeRepo.GetAllAsync(SD.AnimalTypeAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _animTypeRepo.DeleteAsync(SD.AnimalTypeAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}

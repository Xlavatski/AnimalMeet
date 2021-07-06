using AnimalMeetWeb.Models;
using AnimalMeetWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Areas.Admin
{
    [Area("Admin")]
    //[Authorize]
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public IActionResult Index()
        {
            return View(new City() { });
        }


        public async Task<IActionResult> Upsert(int? id)
        {
            City obj = new City();

            if (id == null)
            {
                return View(obj);
            }

            obj = await _cityRepository.GetAsync(SD.CityAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (obj == null) 
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(City objC) 
        {
            if (ModelState.IsValid) 
            {
                if (objC.Id == 0)
                {
                    await _cityRepository.CreateAsync(SD.CityAPIPath, objC, HttpContext.Session.GetString("JWToken"));
                }
                else 
                {
                    await _cityRepository.UpdateAsync(SD.CityAPIPath+objC.Id, objC, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }

            return View(objC);
        }

        public async Task<IActionResult> GetAllCity() 
        {
            return Json(new { data = await _cityRepository.GetAllAsync(SD.CityAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) 
        {
            var status = await _cityRepository.DeleteAsync(SD.CityAPIPath, id, HttpContext.Session.GetString("JWToken"));

            if (status) 
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }

    }
}

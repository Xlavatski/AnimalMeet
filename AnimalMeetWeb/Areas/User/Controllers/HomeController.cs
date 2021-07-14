using AnimalMeetWeb.Models;
using AnimalMeetWeb.Models.ViewModel;
using AnimalMeetWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Areas.User
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository _repoAcco;
        private readonly ICityRepository _cityRepository;

        public HomeController(ILogger<HomeController> logger, IAccountRepository repoAcco, ICityRepository cityRepository)
        {
            _logger = logger;
            _repoAcco = repoAcco;
            _cityRepository = cityRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login() 
        {
            UserLogin obj = new UserLogin();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin obj) 
        {
            UserLogin objUser = await _repoAcco.LoginAsync(SD.AuthUserAPIPath + "authenticate/", obj);
            if (objUser.Token == null)
            {
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, objUser.Id.ToString()));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", objUser.Token);
            //TempData["alert"] = "Welcom " + objUser.Username;


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Register() 
        {
            IEnumerable<City> cList = await _cityRepository.GetAllAsync(SD.CityAPIPath, HttpContext.Session.GetString("JWToken"));

            ViewBag.CityList = new SelectList(cList, "Id", "Name");

            UserRegisterVM objVM = new UserRegisterVM()
            { 
                UserRegister = new UserRegister()
            };

            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM obj) 
        {
            bool result = await _repoAcco.RegisterAsync(SD.AuthUserAPIPath + "register/", obj);

            if (result == false) 
            {
                IEnumerable<City> cList = await _cityRepository.GetAllAsync(SD.CityAPIPath, HttpContext.Session.GetString("JWToken"));

                ViewBag.CityList = new SelectList(cList, "Id", "Name", obj.UserRegister.City);

                return View();
            }

            TempData["alert"] = "Registration Seccesfull ";
            return RedirectToAction(nameof(Login));
        }
    }
}

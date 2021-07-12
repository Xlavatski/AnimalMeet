using AnimalMeetWeb.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalMeetWeb.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly IAccountRepository _accoRepository;
        public UserNameViewComponent(IAccountRepository accoRepository)
        {
            _accoRepository = accoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userFromDb = _accoRepository.GetAsync(SD.AuthUserAPIPath, id.GetValueOrDefault() , HttpContext.Session.GetString("JWToken"));


            return View(userFromDb);
        }
    }
}

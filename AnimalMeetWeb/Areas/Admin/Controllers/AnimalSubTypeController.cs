﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Areas.Admin.Controllers
{
    public class AnimalSubTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
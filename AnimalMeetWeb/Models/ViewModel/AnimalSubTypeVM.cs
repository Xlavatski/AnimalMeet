using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Models.ViewModel
{
    public class AnimalSubTypeVM
    {
        public IEnumerable<SelectListItem> AnimalTypeList { get; set; }
        public AnimalSubType AnimalSubType { get; set; }
    }
}

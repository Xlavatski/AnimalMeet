using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<AnimalType> AnimalTypeList { get; set; }
        public IEnumerable<AnimalSubType> AnimalSubTypeLsit { get; set; }
    }
}

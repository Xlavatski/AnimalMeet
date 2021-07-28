using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Models.ViewModel
{
    public class PetsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int UserId { get; set; }
        public int? AnimalTypeId { get; set; }
        public int? AnimalSubtypeId { get; set; }
    }
}

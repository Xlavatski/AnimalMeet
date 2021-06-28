using AnimalMeetAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Models
{
    public class AnimalSubType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}

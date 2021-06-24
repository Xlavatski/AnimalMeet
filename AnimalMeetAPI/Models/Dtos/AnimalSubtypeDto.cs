using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Models.Dtos
{
    public class AnimalSubtypeDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}

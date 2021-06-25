using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Models.Dtos
{
    public class ApplicationUserUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public int? CityId { get; set; }
        public string Role { get; set; }
    }
}

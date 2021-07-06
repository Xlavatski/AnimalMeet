using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Models
{
    public class ApplicationUser 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public string Role { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}

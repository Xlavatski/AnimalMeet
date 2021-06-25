using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static AnimalMeetAPI.Models.Pets;

namespace AnimalMeetAPI.Models.Dtos
{
    public class PetsDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public SexType Sex { get; set; }

        public int UserId { get; set; }
        //hidden
        public ApplicationUser User { get; set; }

        [Required]
        public int? AnimalSubtypeId { get; set; }
        public AnimalSubtype AnimalSubtype { get; set; }
    }
}

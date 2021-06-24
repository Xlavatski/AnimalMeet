using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public enum SexType { Male, Female }
        [Required]
        public SexType Sex { get; set; }

        public string UserId { get; set; }
        //hidden
        public ApplicationUser User { get; set; }

        [Required]
        public int? AnimalSubtypeId { get; set; }
        public AnimalSubtype AnimalSubtype { get; set; }
    }
}

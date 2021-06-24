using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Models
{
    public class Pets
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public int Age { get; set; }
        public enum SexType { Male, Female }
        [Required]
        public SexType Sex { get; set; }

        public int UserId { get; set; }

        //hidden
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public int? AnimalSubtypeId { get; set; }

        [ForeignKey("AnimalSubtypeId")]
        public AnimalSubtype AnimalSubtype { get; set; }
    }
}

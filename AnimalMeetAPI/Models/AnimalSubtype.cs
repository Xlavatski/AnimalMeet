using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Models
{
    public class AnimalSubtype
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? AnimalTypeId { get; set; }

        [ForeignKey("AnimalTypeId")]
        public AnimalType AnimalType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Animat.Dtos
{
    public class PetDto
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

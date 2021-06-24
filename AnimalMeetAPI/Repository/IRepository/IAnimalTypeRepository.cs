using AnimalMeetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository.IRepository
{
    public interface IAnimalTypeRepository
    {
        ICollection<AnimalType> GetAnimalTypes();
        AnimalType GetAnimalType(int id);
        bool AnimalTypeExist(string name);
        bool AnimalTypeExist(int id);
        bool CreateAnimalType(AnimalType animalType);
        bool UpdateAnimalType(AnimalType animalType);
        bool DeleteAnimalType(AnimalType animalType);
        bool Save();
    }
}

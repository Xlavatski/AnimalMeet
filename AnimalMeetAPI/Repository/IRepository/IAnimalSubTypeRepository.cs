using AnimalMeetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository.IRepository
{
    public interface IAnimalSubTypeRepository
    {
        ICollection<AnimalSubtype> GetAnimalSubTypes();
        ICollection<AnimalSubtype> GetSubTypesInAnimalType(int animTypeId);
        AnimalSubtype GetAnimalSubType(int id);
        bool AnimalSubTypeExist(string name);
        bool AnimalSubTypeExist(int id);
        bool CreateAnimalSubType(AnimalSubtype animalSubType);
        bool UpdateAnimalSubType(AnimalSubtype animalSubType);
        bool DeleteAnimalSubType(AnimalSubtype animalSubType);
        bool Save();
    }
}

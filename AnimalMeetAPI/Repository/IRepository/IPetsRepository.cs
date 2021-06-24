using AnimalMeetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository.IRepository
{
    public interface IPetsRepository
    {
        ICollection<Pets> GetPets();
        ICollection<Pets> GetPetsInAnimalSubType(int animSubTypeId);
        ICollection<Pets> GetPetsInUser(string userId);
        Pets GetPets(int id);
        bool PetsExist(string name);
        bool PetsExist(int id);
        bool CreatePets(Pets pets);
        bool UpdatePets(Pets pets);
        bool DeletePets(Pets pets);
        bool Save();
    }
}

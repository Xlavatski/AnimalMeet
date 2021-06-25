using AnimalMeetAPI.Data;
using AnimalMeetAPI.Models;
using AnimalMeetAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository
{
    public class PetsRepository : IPetsRepository
    {
        private readonly ApplicationDbContext _db;

        public PetsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreatePets(Pets pets)
        {
            _db.Pets.Add(pets);
            return Save();
        }

        public bool DeletePets(Pets pets)
        {
            _db.Pets.Remove(pets);
            return Save();
        }

        public ICollection<Pets> GetPets()
        {
            return _db.Pets.OrderBy(a => a.Name).ToList();
        }

        public Pets GetPet(int id)
        {
            return _db.Pets.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Pets> GetPetsInAnimalSubType(int animSubTypeId)
        {
            return _db.Pets.Include(c => c.AnimalSubtype).Where(c => c.AnimalSubtypeId == animSubTypeId).ToList();
        }

        public ICollection<Pets> GetPetsInUser(int userId)
        {
            return _db.Pets.Include(c => c.User).Where(c => c.UserId == userId).ToList();
        }

        public bool PetsExist(string name)
        {
            bool value = _db.Pets.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool PetsExist(int id)
        {
            return _db.Pets.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePets(Pets pets)
        {
            _db.Pets.Update(pets);
            return Save();
        }
    }
}

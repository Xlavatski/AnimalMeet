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
    public class AnimalSubTypeRepository : IAnimalSubTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public AnimalSubTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AnimalSubTypeExist(string name)
        {
            bool value = _db.AnimalSubtypes.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool AnimalSubTypeExist(int id)
        {
            return _db.AnimalSubtypes.Any(a => a.Id == id);
        }

        public bool CreateAnimalSubType(AnimalSubtype animalSubType)
        {
            _db.AnimalSubtypes.Add(animalSubType);
            return Save();
        }

        public bool DeleteAnimalSubType(AnimalSubtype animalSubType)
        {
            _db.AnimalSubtypes.Remove(animalSubType);
            return Save();
        }

        public AnimalSubtype GetAnimalSubType(int id)
        {
            return _db.AnimalSubtypes.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<AnimalSubtype> GetAnimalSubTypes()
        {
            return _db.AnimalSubtypes.OrderBy(a => a.Name).ToList();
        }

        public bool Save()
        {
            var value = _db.SaveChanges() > 0 ? true : false;
            return value;
        }

        public bool UpdateAnimalSubType(AnimalSubtype animalSubType)
        {
            _db.AnimalSubtypes.Update(animalSubType);
            return Save();
        }

        public ICollection<AnimalSubtype> GetSubTypesInAnimalType(int animTypeId)
        {
            return _db.AnimalSubtypes.Include(c => c.AnimalType).Where(c => c.AnimalTypeId == animTypeId).ToList();
        }
    }
}

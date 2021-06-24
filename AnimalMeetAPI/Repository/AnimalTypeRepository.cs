using AnimalMeetAPI.Data;
using AnimalMeetAPI.Models;
using AnimalMeetAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository
{
    public class AnimalTypeRepository : IAnimalTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public AnimalTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AnimalTypeExist(string name)
        {
            bool value = _db.Animals.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool AnimalTypeExist(int id)
        {
            return _db.Animals.Any(a => a.Id == id);
        }

        public bool CreateAnimalType(AnimalType animalType)
        {
            _db.Animals.Add(animalType);
            return Save();
        }

        public bool DeleteAnimalType(AnimalType animalType)
        {
            _db.Animals.Remove(animalType);
            return Save();
        }

        public AnimalType GetAnimalType(int id)
        {
            return _db.Animals.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<AnimalType> GetAnimalTypes()
        {
            return _db.Animals.OrderBy(a => a.Name).ToList();
        }

        public bool Save()
        {
            var value = _db.SaveChanges() > 0 ? true : false;
            return value;
        }

        public bool UpdateAnimalType(AnimalType animalType)
        {
            _db.Animals.Update(animalType);
            return Save();
        }
    }
}

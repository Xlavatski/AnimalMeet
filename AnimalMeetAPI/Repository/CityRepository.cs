using AnimalMeetAPI.Data;
using AnimalMeetAPI.Models;
using AnimalMeetAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CityExist(string name)
        {
            bool value = _db.Cities.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool CityExist(int id)
        {
            return _db.Cities.Any(a => a.Id == id);
        }

        public bool CreateCity(City city)
        {
            _db.Cities.Add(city);
            return Save();
        }

        public bool DeleteCity(City city)
        {
            _db.Cities.Remove(city);
            return Save();
        }

        public ICollection<City> GetCities()
        {
            return _db.Cities.OrderBy(a => a.Name).ToList();
        }

        public City GetCity(int id)
        {
            return _db.Cities.FirstOrDefault(a => a.Id == id);
        }

        public bool Save()
        {
            var value = _db.SaveChanges() > 0 ? true : false;
            return value;

        }

        public bool UpdateCity(City city)
        {
            _db.Cities.Update(city);
            return Save();
        }
    }
}

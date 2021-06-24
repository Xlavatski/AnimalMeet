using AnimalMeetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository.IRepository
{
    public interface ICityRepository
    {
        ICollection<City> GetCities();
        City GetCity(int id);
        bool CityExist(string name);
        bool CityExist(int id);
        bool CreateCity(City city);
        bool UpdateCity(City city);
        bool DeleteCity(City city);
        bool Save();

    }
}

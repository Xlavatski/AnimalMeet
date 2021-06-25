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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateUser(ApplicationUser applicationUser)
        {
            _db.ApplicationUsers.Add(applicationUser);
            return Save();
        }

        public bool DeleteUser(ApplicationUser applicationUser)
        {
            _db.ApplicationUsers.Remove(applicationUser);
            return Save();
        }

        public ApplicationUser GetUser(int id)
        {
            return _db.ApplicationUsers.Include(c => c.City).FirstOrDefault(a => a.Id == id);
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _db.ApplicationUsers.Include(c => c.City).OrderBy(a => a.Name).ToList();
        }

        public bool UserExist(string name)
        {
            bool value = _db.ApplicationUsers.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool UserExist(int id)
        {
            return _db.ApplicationUsers.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateUser(ApplicationUser applicationUser)
        {
            _db.ApplicationUsers.Update(applicationUser);
            return Save();
        }

        public ICollection<ApplicationUser> GetUsersInCity(int cityId)
        {
            return _db.ApplicationUsers.Include(c => c.City).Where(c => c.CityId == cityId).ToList();
        }

    }
}

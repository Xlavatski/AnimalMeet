using AnimalMeetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();
        ICollection<ApplicationUser> GetUsersInCity(int cityId);
        ApplicationUser GetUser(int id);
        bool UserExist(string name);
        bool UserExist(int id);
        bool CreateUser(ApplicationUser applicationUser);
        bool UpdateUser(ApplicationUser applicationUser);
        bool DeleteUser(ApplicationUser applicationUser);
        bool Save();
    }
}

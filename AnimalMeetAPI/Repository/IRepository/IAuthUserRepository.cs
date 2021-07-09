using AnimalMeetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Repository.IRepository
{
    public interface IAuthUserRepository
    {
        bool IsUniqueUser(string username);
        ApplicationUser Authenticate(string username, string password); 
        ApplicationUser Register(string username, string password, string name, string surname, int? cityId);
        ApplicationUser GetUser(int id);
    }
}

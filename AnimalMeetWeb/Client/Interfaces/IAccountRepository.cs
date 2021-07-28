using AnimalMeetWeb.Models;
using AnimalMeetWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Repository.IRepository
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<UserLogin> LoginAsync(string url, UserLogin objToCreate);
        Task<bool> RegisterAsync(string url, UserRegisterVM objToCreate);
    }
}

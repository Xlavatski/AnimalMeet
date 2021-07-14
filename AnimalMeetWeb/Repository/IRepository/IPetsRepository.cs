using AnimalMeetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Repository.IRepository
{
    public interface IPetsRepository : IRepository<Pets>
    {
        Task<IEnumerable<Pets>> GetAllPetsOfUserAsync(string url, int Id, string token);
    }
}

using AnimalMeetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Repository.IRepository
{
    public interface IAnimalSubTypeRepository : IRepository<AnimalSubType>
    {
        Task<IEnumerable<AnimalSubType>> GetAllSubTypesOfTypeAsync(string url, int? Id, string token = "");
    }
}

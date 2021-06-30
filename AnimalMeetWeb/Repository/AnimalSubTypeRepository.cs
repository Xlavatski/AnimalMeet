using AnimalMeetWeb.Models;
using AnimalMeetWeb.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Repository
{
    public class AnimalSubTypeRepository : Repository<AnimalSubType>, IAnimalSubTypeRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public AnimalSubTypeRepository(IHttpClientFactory clientFactory): base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}

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
    public class PetsRepository : Repository<Pets>, IPetsRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public PetsRepository(IHttpClientFactory clientFactory): base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}

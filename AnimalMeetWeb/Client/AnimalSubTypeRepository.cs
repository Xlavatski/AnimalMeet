using AnimalMeetWeb.Models;
using AnimalMeetWeb.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Repository
{
    public class AnimalSubTypeRepository : HttpBaseClient<AnimalSubType>, IAnimalSubTypeRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public AnimalSubTypeRepository(IHttpClientFactory clientFactory): base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<AnimalSubType>> GetAllSubTypesOfTypeAsync(string url, int? Id, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + Id);

            var client = _clientFactory.CreateClient();
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<AnimalSubType>>(jsonString);
            }
            return null;
        }
    }
}

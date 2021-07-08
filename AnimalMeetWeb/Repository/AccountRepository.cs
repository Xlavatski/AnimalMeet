using AnimalMeetWeb.Models;
using AnimalMeetWeb.Models.ViewModel;
using AnimalMeetWeb.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountRepository(IHttpClientFactory clientFactory) /*: base(clientFactory)*/
        {
            _clientFactory = clientFactory;
        }

        public async Task<UserLogin> LoginAsync(string url, UserLogin objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");

            }
            else 
            {
                return new UserLogin();
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserLogin>(jsonString);
            }
            else
            {
                return new UserLogin();
            }

        }

        public async Task<bool> RegisterAsync(string url, UserRegisterVM objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate.UserRegister), Encoding.UTF8, "application/json");
            }
            else 
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

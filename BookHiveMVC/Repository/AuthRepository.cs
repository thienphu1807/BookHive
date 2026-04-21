using AutoMapper;
using BookHiveMVC.Models.Dtos;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;
using Newtonsoft.Json;
using System.Text;

namespace BookHiveMVC.Repository
{
    public class AuthRepository : IAuthRepository
    {
        protected readonly IHttpClientFactory _clientFactory;
        protected readonly IMapper _mapper;
        public AuthRepository(IHttpClientFactory clientFactory, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
        }

        public async Task<bool> LogOutAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RegisterAsync(string url, RegisterDto registerDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (registerDto == null)
            {
                return false;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<ResponseAuth?> SignInAsync(string url, LoginDto loginDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (loginDto == null)
            {
                return null;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseAuth>(json);

            return result;
        }
    }
}

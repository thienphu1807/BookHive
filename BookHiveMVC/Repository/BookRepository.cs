using AutoMapper;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace BookHiveMVC.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(IHttpClientFactory httpClientFactory, IMapper mapper) : base(httpClientFactory, mapper)
        {
        }
        public async Task<ICollection<GetBook>> GetAllBookAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<GetBook>>(jsonString);
               
            }
            return null;
        }
        public async Task<ICollection<GetBook>> GetBookByTitleAsync(string url, string tilte)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + tilte);

            var client  = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<GetBook>>(jsonString);
            }
            return null;
        }

        public async Task<ICollection<GetBook>> GetBookFromAuthorAsync(string url, int AuthorId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + AuthorId);

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<GetBook>>(jsonString);
            }
            return null;
        }

        public async Task<ICollection<GetBook>> GetBookFromCategoryAsync(string url, int CategoryId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + CategoryId);

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<GetBook>>(jsonString);
            }
            return null;
        }
    }
}

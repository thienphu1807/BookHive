using AutoMapper;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Models.Dtos;
using BookHiveMVC.Repository.IRepository;
using Newtonsoft.Json;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Security.Claims;

namespace BookHiveMVC.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookRepository(IHttpClientFactory httpClientFactory, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CreateBookAsync(string url, CreateBook createBook)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (createBook == null)
            {
                return false;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
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
        public async Task<ICollection<GetBookReview>> GetBookReview(string url, int bookId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + bookId + "/review/");

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<GetBookReview>>(jsonString);
            }
            return null;
        }
        public async Task<bool> AddBookReviewAsync(string url, int bookId, AddBookReview reviewDto)
        {
            var client = _clientFactory.CreateClient();

            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            var request = new HttpRequestMessage(HttpMethod.Post, $"{url}{bookId}/review");
            request.Content = JsonContent.Create(reviewDto);

            if (!string.IsNullOrEmpty(userName))
            {
                request.Headers.Add("X-UserName", userName);
            }

            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}

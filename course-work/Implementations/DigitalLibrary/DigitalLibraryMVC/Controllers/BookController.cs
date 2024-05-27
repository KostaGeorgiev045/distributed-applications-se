using DigitalLibrary.Helpers;
using DigitalLibrary.Interfaces;
using DigitalLibrary.Models;
using DigitalLibraryMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Linq.Dynamic.Core;
using System.Net.Http.Headers;

namespace DigitalLibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiSettings _apiSettings;

        public BookController(IHttpContextAccessor httpContextAccessor, IOptions<ApiSettings> apiSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiSettings = apiSettings.Value;
            _client = new HttpClient
            {
                // Set the base address to the running API URL (adjust this as necessary)
                BaseAddress = new Uri("https://localhost:7061/api/") // You can change to the appropriate URL
            };
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<BookViewModel> bookList = new List<BookViewModel>();
            HttpResponseMessage response;

            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            Console.WriteLine("Token: " + token); // Log the token
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                response = await _client.GetAsync("Book/GetAll");
            }
            catch (HttpRequestException ex)
            {
                // Fallback to HTTP if HTTPS is not available (for development purposes only)
                _client.BaseAddress = new Uri("http://localhost:5214/api/");
                response = await _client.GetAsync("Book/GetAll");
            }

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                // Log the response data
                Console.WriteLine(data); // You can use a proper logging framework instead
                bookList = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            }
            else
            {
                // Log the error response
                Console.WriteLine($"API call failed with status code: {response.StatusCode}");
            }

            return View(bookList);
        }
    }
}

using DigitalLibrary.Helpers;
using DigitalLibrary.Interfaces;
using DigitalLibrary.Models;
using DigitalLibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;

namespace DigitalLibraryMVC.Controllers
{
    public class BookController : Controller
    {
        //Uri baseAddress = new Uri("http://localhost:44397/api");
        //private readonly HttpClient _client;

        //public BookController()
        //{
        //    _client = new HttpClient();
        //    _client.BaseAddress = baseAddress;
        //}
        private static readonly HttpClient _client;

        static BookController()
        {
            _client = new HttpClient
            {
                // Set the base address to the running API URL (adjust this as necessary)
                BaseAddress = new Uri("https://localhost:7061/api") // You can change to the appropriate URL
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BookViewModel> bookList = new List<BookViewModel>();
            HttpResponseMessage response;

            try
            {
                response = await _client.GetAsync("Book/GetAll");
            }
            catch (HttpRequestException)
            {
                // Fallback to HTTP if HTTPS is not available (for development purposes only)
                _client.BaseAddress = new Uri("http://localhost:5214/api");
                response = await _client.GetAsync("Book/GetAll");
            }

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            }

            return View(bookList);
        }
    }
}

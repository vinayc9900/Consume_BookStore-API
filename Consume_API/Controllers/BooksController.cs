using Consume_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Consume_API.Controllers
{
    public class BooksController : Controller
    {
        // GET: BooksController
        public async Task<IActionResult> Index()
        {
            List<BookModel> books = new List<BookModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:43589/");
            HttpResponseMessage resp = await client.GetAsync("api/books");
            if(resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookModel>>(results);
            }
            return View(books);
        }

        // GET: BooksController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            BookModel book = new BookModel();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:43589/");
            HttpResponseMessage resp = await client.GetAsync($"api/books/{id}");
            if (resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<BookModel>(results);
            }
            return View(book);

            
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public async Task <ActionResult> Create(BookModel bookModel)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:43589/");
            var  resp = await client.PostAsJsonAsync<BookModel>("api/books", bookModel);
            if(resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: BooksController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            BookModel book = new BookModel();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:43589/");
            HttpResponseMessage resp = await client.GetAsync($"api/books/{id}");
            if (resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<BookModel>(results);
            }
            return View(book);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookModel bookModel)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:43589/");
            var resp = await client.PutAsJsonAsync<BookModel>($"api/books/{bookModel.Id}", bookModel);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: BooksController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:43589/");
            HttpResponseMessage resp = await client.DeleteAsync($"api/books/{id}");
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: BooksController/Delete/5
       
    }
}

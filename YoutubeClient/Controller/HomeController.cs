using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YoutubeClient.Models;

namespace YoutubeClient.Controllers
{
    public class HomeController : Controller
    {
        public  IActionResult Index()
        {
            HttpClient client = new HttpClient();

            var responseMessage = client.GetAsync("http://localhost:1575/api/Products").Result;
            List<Product> products=null;
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                products = JsonConvert.DeserializeObject<List<Product>> (responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(products);
        }
        public IActionResult Add()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("http://localhost:1575/api/Products", content).Result;

            if(responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Ekleme işlemi başarısız");
            return View();
        }

        public IActionResult Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.GetAsync($"http://localhost:1575/api/Products/{id}").Result;
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
               var product = JsonConvert.DeserializeObject<Product>(responseMessage.Content.ReadAsStringAsync().Result);

                return View(product);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult Edit(Product product)
        {
            HttpClient httpClient = new HttpClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var responseMessage= httpClient.PutAsync($"http://localhost:1575/api/Products/{product.Id}", content).Result;
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"http://localhost:1575/api/Products/{id}").Result;

            return RedirectToAction("Index");
        }

    }
}

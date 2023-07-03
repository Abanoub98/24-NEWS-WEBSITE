using _24NEWSMVC.Models;
using _24NEWSMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace _24NEWSMVC.Controllers
{
    public class HomeController : Controller
    {
    

        Uri baseAddress = new Uri("https://localhost:7072/api");
        HttpClient client;

        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }



        public async Task<IActionResult> Index()
        {         
            List<ArticleViewModel> modelList = new List<ArticleViewModel>();
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Articles");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                modelList = JsonConvert.DeserializeObject<List<ArticleViewModel>>(data);

            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View(modelList);
        }


        //[HttpGet("results")]
        //public async Task<IActionResult> SearchResuls(string query)
        //{
        //    List<ArticleViewModel> modelList = new List<ArticleViewModel>();
        //    HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Articles/results" + ViewData["CurrentFiler"]);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = await response.Content.ReadAsStringAsync();
        //        modelList = JsonConvert.DeserializeObject<List<ArticleViewModel>>(data);

        //    }
        //    else
        //    {
        //        Console.WriteLine("Error Calling Web Api");
        //    }
        //    return View(modelList);
        //}



        [HttpGet("articleinfo/{id}")]
        public async Task<IActionResult> ArticleInfo(int id)
        {
            ArticleViewModel model = new ArticleViewModel();
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Articles/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ArticleViewModel>(data);
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View(model);
        }

        public ActionResult About()
        {
            return View();  
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
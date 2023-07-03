using _24NEWSMVC.Models;
using _24NEWSMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace _24NEWSMVC.Controllers
{
    public class AdminController : Controller
    {

        public string tokenString = "";

        Uri baseAddress = new Uri("https://localhost:7072/api");
        HttpClient client;

        public AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {

            if(HttpContext.Session.GetString("token") ==null )
            {
                return RedirectToAction("Login");
            }
            return View();
        }






        //############### Login and logout sub-controller actions ############################

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "/Auth/token", content);

              
            if (response.IsSuccessStatusCode)
            {
                string getData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetToken>(getData);
                HttpContext.Session.SetString("token",value.Token);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("LoginState", "Invalid username or password \t try again or regiester first!");
                Console.WriteLine("Error Calling Web Api");
            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Login");
        }

        //############### Login and logout sub-controller actions ############################







        //############### Author sub-controller actions ############################

        public async Task<IActionResult> Author()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

            List<AuthorViewModel> modelList = new List<AuthorViewModel>();
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Authors");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                modelList = JsonConvert.DeserializeObject<List<AuthorViewModel>>(data);
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View(modelList);
        }





        [HttpGet("viewauthor/{id}")]
        public async Task<IActionResult> ViewAuthor(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

            AuthorViewModel model = new AuthorViewModel();
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Authors/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<AuthorViewModel>(data);
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View(model);
        }

   


        public ActionResult CreateAuthor()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

     
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorViewModel model)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "/Authors", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Author");
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View();
        }


        [HttpGet("editauthor/{id}")]
        public async Task<IActionResult> EditAuthor(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

            AuthorViewModel model = new AuthorViewModel();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Authors/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<AuthorViewModel>(data);
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View(model);
        }

        [HttpPost("editauthor/{id}")]
        public async Task<IActionResult> EditAuthor(AuthorViewModel model , int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = await client.PutAsync(client.BaseAddress + "/Authors/"+id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Author");
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View("EditAuthor",model);
        }


        

        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + "/Authors/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Author");
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View("Author");
        }


        //############### Author sub-controller actions ############################







        //############### Article sub-controller actions ############################

        [HttpGet("viewarticle/{id}")]
        public async Task<IActionResult> ViewArticle(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

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




        public async Task<IActionResult> Articles()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

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

        [HttpGet("getarticleimgbyid/{id}")]
        public async Task<IActionResult> GetArticleImgById(int id)
        {
           

            ArticleViewModel model = new ArticleViewModel();
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Articles/"+id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ArticleViewModel>(data);
                return File(model.img,"image/jpeg");
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
         return NotFound();
        }

        public async Task<IActionResult> CreateArticle()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }

            ArticleViewModel model = new ArticleViewModel();
            List<AuthorViewModel> modelList = new List<AuthorViewModel>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/Authors");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                modelList = JsonConvert.DeserializeObject<List<AuthorViewModel>>(data);
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            model.Authors = modelList.ToDictionary(x => x.Id, x => x.Name);
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleViewModel model)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }
            var authorResponse = await client.GetAsync(client.BaseAddress + "/Authors/" + model.AuthorID);
            var authorJsonString = await authorResponse.Content.ReadAsStringAsync();
            var author = JsonConvert.DeserializeObject<AuthorViewModel>(authorJsonString);
            model.AuthorName = author!.Name;
            using var dataStream = new MemoryStream();
            await model.imgFile.CopyToAsync(dataStream);

            model.img = dataStream.ToArray();

            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "/Articles", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine(responseBody);
            System.Diagnostics.Debug.WriteLine(model.PublicationDate);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Articles");
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View();
        }











        [HttpGet("editarticle/{id}")]
        public async Task<IActionResult> EditArticle(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }
            ArticleViewModel model = new ArticleViewModel();
            List<AuthorViewModel> modelList = new List<AuthorViewModel>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage responseAuthor = await client.GetAsync(client.BaseAddress + "/Authors");

            HttpResponseMessage responseArticle = await client.GetAsync(client.BaseAddress + "/Articles/" + id);
            if (responseArticle.IsSuccessStatusCode && responseAuthor.IsSuccessStatusCode)
            {
                string dataArticle = await responseArticle.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ArticleViewModel>(dataArticle);
       
                string dataAuthor = await responseAuthor.Content.ReadAsStringAsync();
                modelList = JsonConvert.DeserializeObject<List<AuthorViewModel>>(dataAuthor);
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            model.Authors=modelList.ToDictionary(x => x.Id, x => x.Name);   
            return View(model);
        }

        [HttpPost("editarticle/{id}")]
        public async Task<IActionResult> EditArticle(ArticleViewModel model, int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            var authorResponse = await client.GetAsync(client.BaseAddress + "/Authors/" + model.AuthorID);
            var authorJsonString = await authorResponse.Content.ReadAsStringAsync();
            var author = JsonConvert.DeserializeObject<AuthorViewModel>(authorJsonString);
            model.AuthorName = author!.Name;
            using var dataStream = new MemoryStream();

            await model.imgFile.CopyToAsync(dataStream);
            model.img = dataStream.ToArray();



            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(client.BaseAddress + "/Articles/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Articles");
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View("EditArticle", model);
        }





        [HttpGet]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + "/Articles/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Articles");
            }
            else
            {
                Console.WriteLine("Error Calling Web Api");
            }
            return View("Articles");
        }


        //############### Article sub-controller actions ############################
    }
}

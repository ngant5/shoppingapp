using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FE_MVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category/Index
        public async Task<ActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost/Shopping/"); // Điều chỉnh đường dẫn cơ sở
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/categories");
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsAsync<List<Category>>();
                    return View(data);
                }
            }
            catch (HttpRequestException ex)
            {
                // Logging error
                System.Diagnostics.Trace.TraceError("HTTP Request Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred. Please check the log for details.";
            }
            catch (Exception ex)
            {
                // Logging error
                System.Diagnostics.Trace.TraceError("General Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred. Please check the log for details.";
            }

            return View();
        }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
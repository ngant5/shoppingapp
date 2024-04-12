using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Collections;

namespace FE_MVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {

        // GET: Admin/Category/Index
        public async Task<ActionResult> GetAllCategories()
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.UseDefaultCredentials = true;
                handler.PreAuthenticate = true;
                handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
                handler.Credentials = new NetworkCredential("test01", "test01");

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("http://localhost/Shopping");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("api/categories");
                    response.EnsureSuccessStatusCode();

                    //var data = await response.Content.ReadAsAsync<ActionResult>();
                    //return View(data);
                }
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                TempData["ErrorMessage"] = jEx.Message;
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

            }

            return View();
        }
    }

    
}
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
        private readonly string baseApiUrl = "http://localhost/Shopping/api/categories";

        public async Task<ActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost/Shopping/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/categories");
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsAsync<List<Category>>();
                    return View(data);
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.TraceError("HTTP Request Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred. Please check the log for details.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("General Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred. Please check the log for details.";
            }

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                if (category == null)
                {
                    TempData["ErrorMessage"] = "Invalid category data. Please provide valid category information.";
                    return View(category);
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("", category);
                    response.EnsureSuccessStatusCode();

                    if (!response.IsSuccessStatusCode)
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Trace.TraceError($"Error response: {errorResponse}");
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to create category. Please try again.";
                        return View(category);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred. Please check the log for details.";
                return View(category);
            }
        }
        
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"{id}");
                    response.EnsureSuccessStatusCode();

                    var category = await response.Content.ReadAsAsync<Category>();
                    return View("Edit",category);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred. Please check the log for details.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Category category)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PutAsJsonAsync($"{category.CategoryID}", category);
                    response.EnsureSuccessStatusCode();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred. Please check the log for details.";
                return View(category);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"{id}");
                    response.EnsureSuccessStatusCode();

                    return RedirectToAction("Index");
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.TraceError("HTTP Request Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please check the log for details.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("General Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please check the log for details.";
                return RedirectToAction("Index");
            }
        }
        
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
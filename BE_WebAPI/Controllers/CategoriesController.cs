using BE_WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BE_WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private readonly shoppingEntities db = new shoppingEntities();

        readonly List<Models.Categories> listCategory = new List<Models.Categories>();
        public CategoriesController()
        {
            listCategory = db.Categories.ToList();
        }

        // GET api/categories
        public IEnumerable<Models.Categories> Get()
        {
            
            return listCategory;
        }

        // GET api/categories/{id}
        public IHttpActionResult Get(int id)
        {
            var category = listCategory.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/categories
        public IHttpActionResult Post([FromBody] Models.Categories newCategory)
        {
            
            if (newCategory == null)
            {
                return BadRequest("Invalid data. New category object is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Categories.Add(newCategory);
                db.SaveChanges();
                listCategory.Add(newCategory);
                return CreatedAtRoute("DefaultApi", new { id = newCategory.CategoryID }, newCategory);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError(); 
            }

        }

        // PUT api/categories/{id}
        public IHttpActionResult Put(int id, [FromBody] Models.Categories updatedCategory)
        {
            var existingCategory = listCategory.FirstOrDefault(c => c.CategoryID == id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                existingCategory.CategoryName = updatedCategory.CategoryName;
                db.SaveChanges();
                int index = listCategory.FindIndex(c => c.CategoryID == id);
                if (index != -1)
                {
                    listCategory[index].CategoryName = updatedCategory.CategoryName;
                }

                return Ok(existingCategory);
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError(); // Trả về lỗi 500 - Internal Server Error
            }

        }

        // DELETE api/categories/{id}
        public IHttpActionResult Delete(int id)
        {
            var category = listCategory.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                listCategory.Remove(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError(); }
        }
    }
        
}
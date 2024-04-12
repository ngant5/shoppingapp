using BE_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BE_WebAPI.Controllers
{
    
    public class CategoriesController : ApiController
    {
        private shoppingEntities db = new shoppingEntities();

        List<Models.Categories> listCategory = new List<Models.Categories>();
        // GET api/categories
        public IEnumerable<Models.Categories> Get()
        {
            listCategory = db.Categories.ToList();
            return listCategory;
        }

        // GET api/categories/5
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
            listCategory.Add(newCategory);
            return CreatedAtRoute("DefaultApi", new { id = newCategory.CategoryID }, newCategory);
        }

        // PUT api/categories/5
        public IHttpActionResult Put(int id, [FromBody] Models.Categories updatedCategory)
        {
            var existingCategory = listCategory.FirstOrDefault(c => c.CategoryID == id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.CategoryName = updatedCategory.CategoryName;
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/categories/5
        public IHttpActionResult Delete(int id)
        {
            var category = listCategory.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            listCategory.Remove(category);
            return Ok(category);
        }
    }

}
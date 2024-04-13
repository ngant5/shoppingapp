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

            db.Categories.Add(newCategory);
            db.SaveChanges();
            listCategory.Add(newCategory);
            return CreatedAtRoute("DefaultApi", new { id = newCategory.CategoryID }, newCategory);
        }

        // PUT api/categories/{id}
        public IHttpActionResult Put(int id, [FromBody] Models.Categories updatedCategory)
        {
            var existingCategory = listCategory.FirstOrDefault(c => c.CategoryID == id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.CategoryName = updatedCategory.CategoryName;
            db.SaveChanges();
            int index = listCategory.FindIndex(c => c.CategoryID == id);
            if (index != -1)
            {
                listCategory[index].CategoryName = updatedCategory.CategoryName;
            }

            return Ok(existingCategory);

        }

        // DELETE api/categories/{id}
        public IHttpActionResult Delete(int id)
        {
            var category = listCategory.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            listCategory.Remove(category); 
            return Ok(category);
        }
    }

}
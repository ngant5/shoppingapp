using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BE_WebAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Models.Category> Get()
        {
            List<Models.Category> listCategory = new List<Models.Category>();
            listCategory.Add(new Models.Category { CategoryID = 1, CategoryName = "Category 1" });
            listCategory.Add(new Models.Category { CategoryID = 2, CategoryName = "Category 2" });
            listCategory.Add(new Models.Category { CategoryID = 3, CategoryName = "Category 3" });


            return listCategory;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
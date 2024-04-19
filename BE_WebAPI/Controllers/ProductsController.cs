using BE_WebAPI.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BE_WebAPI.Controllers
{
    //[EnableCors(origins: "http://localhost", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        private readonly shoppingEntities db = new shoppingEntities();

        readonly List<Controllers.Products> listProduct = new List<Controllers.Products>();
        public ProductsController()
        {
            listProduct = db.Products.ToList();
        }

        // GET api/products
        public IEnumerable<Controllers.Products> Get()
        {
            return listProduct;
        }

        // GET api/products/{id}
        public IHttpActionResult Get(int id)
        {
            var product = listProduct.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/products
        public IHttpActionResult Post([FromBody] Controllers.Products newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Invalid data. New product object is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Products.Add(newProduct);
                db.SaveChanges();
                listProduct.Add(newProduct);
                return CreatedAtRoute("DefaultApi", new { id = newProduct.ProductID }, newProduct);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // PUT api/products/{id}
        public IHttpActionResult Put(int id, [FromBody] Controllers.Products updatedProduct)
        {
            if (updatedProduct == null || updatedProduct.ProductID != id)
            {
                return BadRequest("Invalid data. Updated product object is null or has incorrect ID.");
            }
            var existingProduct = listProduct.FirstOrDefault(p => p.ProductID == id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            if (db.Products.Any(p => p.Code == updatedProduct.Code && p.ProductID != id))
            {
                return BadRequest("Product with the same code already exists.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                existingProduct.ProductName = updatedProduct.ProductName;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Code = updatedProduct.Code;
                existingProduct.CategoryID = updatedProduct.CategoryID;
                db.SaveChanges();
                int index = listProduct.FindIndex(p => p.ProductID == id);
                if (index != -1)
                {
                    listProduct[index].ProductName = updatedProduct.ProductName;
                    listProduct[index].Description = updatedProduct.Description;
                    listProduct[index].Price = updatedProduct.Price;
                    listProduct[index].Code = updatedProduct.Code;
                    listProduct[index].CategoryID = updatedProduct.CategoryID;
                }

                return Ok(existingProduct);
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // DELETE api/products/{id}
        public IHttpActionResult Delete(int id)
        {
            var product = listProduct.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            try
            {
                db.Products.Remove(product);
                db.SaveChanges();
                listProduct.Remove(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }
    }
}
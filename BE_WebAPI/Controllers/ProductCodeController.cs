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
    public class ProductCodeController : ApiController
    {
        private readonly shoppingEntities db = new shoppingEntities();

        readonly List<Controllers.ProductCode> listProductCode = new List<Controllers.ProductCode>();
        public ProductCodeController()
        {
            listProductCode = db.ProductCode.ToList();
        }

        // GET api/productcodes
        public IEnumerable<Controllers.ProductCode> Get()
        {
            return listProductCode;
        }

        // GET api/productcodes/{id}
        public IHttpActionResult Get(int id)
        {
            var productCode = listProductCode.FirstOrDefault(c => c.CodeID == id);
            if (productCode == null)
            {
                return NotFound();
            }
            return Ok(productCode);
        }

        // POST api/productcodes
        public IHttpActionResult Post([FromBody] Controllers.ProductCode newProductCode)
        {
            if (newProductCode == null)
            {
                return BadRequest("Invalid data. New product code object is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.ProductCode.Add(newProductCode);
                db.SaveChanges();
                listProductCode.Add(newProductCode);
                return CreatedAtRoute("DefaultApi", new { id = newProductCode.CodeID }, newProductCode);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // PUT api/productcodes/{id}
        public IHttpActionResult Put(int id, [FromBody] Controllers.ProductCode updatedProductCode)
        {
            var existingProductCode = listProductCode.FirstOrDefault(c => c.CodeID == id);
            if (existingProductCode == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                existingProductCode.Title = updatedProductCode.Title;
                db.SaveChanges();
                int index = listProductCode.FindIndex(c => c.CodeID == id);
                if (index != -1)
                {
                    listProductCode[index].Title = updatedProductCode.Title;
                }

                return Ok(existingProductCode);
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // DELETE api/productcodes/{id}
        public IHttpActionResult Delete(int id)
        {
            var productCode = listProductCode.FirstOrDefault(c => c.CodeID == id);
            if (productCode == null)
            {
                return NotFound();
            }
            try
            {
                db.ProductCode.Remove(productCode);
                db.SaveChanges();
                listProductCode.Remove(productCode);
                return Ok(productCode);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }
    }
}
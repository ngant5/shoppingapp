using BE_WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BE_WebAPI.Controllers
{
    //[EnableCors(origins: "http://localhost", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        private readonly shoppingEntities db = new shoppingEntities();

        readonly List<Controllers.Customers> listCustomers = new List<Controllers.Customers>();
        public CustomersController()
        {
            listCustomers = db.Customers.ToList();
        }

        
        public IEnumerable<Controllers.Customers> Get()
        {
            return listCustomers;
        }

        
        public IHttpActionResult Get(int id)
        {
            var customer = listCustomers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        
        public IHttpActionResult Post([FromBody] Controllers.Customers newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("Invalid data. New customer object is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                listCustomers.Add(newCustomer);
                return CreatedAtRoute("DefaultApi", new { id = newCustomer.CustomerID }, newCustomer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        
        public IHttpActionResult Put(int id, [FromBody] Controllers.Customers updatedCustomer)
        {
            var existingCustomer = listCustomers.FirstOrDefault(c => c.CustomerID == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                existingCustomer.Username = updatedCustomer.Username;
                existingCustomer.Password = updatedCustomer.Password;
                existingCustomer.FullName = updatedCustomer.FullName;
                existingCustomer.Address = updatedCustomer.Address;
                existingCustomer.Email = updatedCustomer.Email;

                db.SaveChanges();

                int index = listCustomers.FindIndex(c => c.CustomerID == id);
                if (index != -1)
                {
                    listCustomers[index] = updatedCustomer;
                }

                return Ok(existingCustomer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        
        public IHttpActionResult Delete(int id)
        {
            var customer = listCustomers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                listCustomers.Remove(customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }
    }
}
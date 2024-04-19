using BE_WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BE_WebAPI.Controllers
{
    //[EnableCors(origins: "http://localhost", headers: "*", methods: "*")]
    public class DeliveryTypeController : ApiController
    {
        private readonly shoppingEntities db = new shoppingEntities();
        private readonly List<DeliveryType> deliveryTypes = new List<DeliveryType>();

        public DeliveryTypeController()
        {
            deliveryTypes = db.DeliveryType.ToList();
        }

        // GET api/deliverytype
        public IEnumerable<DeliveryType> Get()
        {
            return deliveryTypes;
        }

        // GET api/deliverytype/{id}
        public IHttpActionResult Get(int id)
        {
            var deliveryType = deliveryTypes.FirstOrDefault(dt => dt.DeliveryTypeID == id);
            if (deliveryType == null)
            {
                return NotFound();
            }
            return Ok(deliveryType);
        }

        // POST api/deliverytype
        public IHttpActionResult Post([FromBody] DeliveryType newDeliveryType)
        {
            if (newDeliveryType == null)
            {
                return BadRequest("Invalid data. New delivery type object is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.DeliveryType.Add(newDeliveryType);
                db.SaveChanges();
                deliveryTypes.Add(newDeliveryType);
                return CreatedAtRoute("DefaultApi", new { id = newDeliveryType.DeliveryTypeID }, newDeliveryType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // PUT api/deliverytype/{id}
        public IHttpActionResult Put(int id, [FromBody] DeliveryType updatedDeliveryType)
        {
            var existingDeliveryType = deliveryTypes.FirstOrDefault(dt => dt.DeliveryTypeID == id);
            if (existingDeliveryType == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                existingDeliveryType.Title = updatedDeliveryType.Title;
                db.SaveChanges();
                int index = deliveryTypes.FindIndex(dt => dt.DeliveryTypeID == id);
                if (index != -1)
                {
                    deliveryTypes[index].Title = updatedDeliveryType.Title;
                }

                return Ok(existingDeliveryType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // DELETE api/deliverytype/{id}
        public IHttpActionResult Delete(int id)
        {
            var deliveryType = deliveryTypes.FirstOrDefault(dt => dt.DeliveryTypeID == id);
            if (deliveryType == null)
            {
                return NotFound();
            }
            try
            {
                db.DeliveryType.Remove(deliveryType);
                db.SaveChanges();
                deliveryTypes.Remove(deliveryType);
                return Ok(deliveryType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }
    }
}
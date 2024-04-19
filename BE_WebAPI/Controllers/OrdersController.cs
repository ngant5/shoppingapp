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
    public class OrdersController : ApiController
{
    private readonly shoppingEntities db = new shoppingEntities();
    
    readonly List<Controllers.Orders> listOrders = new List<Controllers.Orders>();
    
    public OrdersController()
    {
        listOrders = db.Orders.ToList();
    }

    // GET api/orders
    public IEnumerable<Controllers.Orders> Get()
    {
        return listOrders;
    }

    // GET api/orders/{id}
    public IHttpActionResult Get(int id)
    {
        var order = listOrders.FirstOrDefault(o => o.OrderID == id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    // POST api/orders
    public IHttpActionResult Post([FromBody] Controllers.Orders newOrder)
    {
        if (newOrder == null)
        {
            return BadRequest("Invalid data. New order object is null.");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            db.Orders.Add(newOrder);
            db.SaveChanges();
            listOrders.Add(newOrder);
            return CreatedAtRoute("DefaultApi", new { id = newOrder.OrderID }, newOrder);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
            return InternalServerError();
        }
    }

    // PUT api/orders/{id}
    public IHttpActionResult Put(int id, [FromBody] Controllers.Orders updatedOrder)
    {
        var existingOrder = listOrders.FirstOrDefault(o => o.OrderID == id);
        if (existingOrder == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Update the existing order fields
            existingOrder.OrderNumber = updatedOrder.OrderNumber;
            existingOrder.CustomerID = updatedOrder.CustomerID;
            existingOrder.EmployeeID = updatedOrder.EmployeeID;
            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.ShippingAddress = updatedOrder.ShippingAddress;
            existingOrder.Notes = updatedOrder.Notes;

            db.SaveChanges();

            // Update the order in the list
            int index = listOrders.FindIndex(o => o.OrderID == id);
            if (index != -1)
            {
                listOrders[index] = existingOrder;
            }

            return Ok(existingOrder);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
            return InternalServerError();
        }
    }

    // DELETE api/orders/{id}
    public IHttpActionResult Delete(int id)
    {
        var order = listOrders.FirstOrDefault(o => o.OrderID == id);
        if (order == null)
        {
            return NotFound();
        }
        try
        {
            db.Orders.Remove(order);
            db.SaveChanges();
            listOrders.Remove(order);
            return Ok(order);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
            return InternalServerError();
        }
    }
}
}
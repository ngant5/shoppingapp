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
    [EnableCors(origins: "http://localhost", headers: "*", methods: "*")]

    public class EmployeesController : ApiController
    {
        private readonly shoppingEntities db = new shoppingEntities();

        readonly List<Employees> listEmployees = new List<Employees>();
        public EmployeesController()
        {
            listEmployees = db.Employees.ToList();
        }


        // GET api/employees
        public IEnumerable<Employees> Get()
        {
            return listEmployees;
        }

        // GET api/employees/5
        public IHttpActionResult Get(int id)
        {
            var employee = listEmployees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST api/employees
        public IHttpActionResult Post([FromBody] Employees newEmployee)
        {
            if (newEmployee == null)
            {
                return BadRequest("Invalid data. New employee object is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Employees.Add(newEmployee);
                db.SaveChanges();

                listEmployees.Add(newEmployee);
                return CreatedAtRoute("DefaultApi", new { id = newEmployee.EmployeeID }, newEmployee);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // PUT api/employees/5
        public IHttpActionResult Put(int id, [FromBody] Employees updatedEmployee)
        {
            var existingEmployee = listEmployees.FirstOrDefault(e => e.EmployeeID == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                existingEmployee.Username = updatedEmployee.Username;
                existingEmployee.Password = updatedEmployee.Password;
                existingEmployee.FullName = updatedEmployee.FullName;
                existingEmployee.Role = updatedEmployee.Role;
                existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
                existingEmployee.Email = updatedEmployee.Email;
                db.SaveChanges();
                int index = listEmployees.FindIndex(c => c.EmployeeID == id);
                if (index != -1)
                {
                    listEmployees[index].Username = updatedEmployee.Username;
                    listEmployees[index].Password = updatedEmployee.Password;
                    listEmployees[index].FullName = updatedEmployee.FullName;
                    listEmployees[index].Role = updatedEmployee.Role;
                    listEmployees[index].PhoneNumber = updatedEmployee.PhoneNumber;
                    listEmployees[index].Email = updatedEmployee.Email;
                }

                return Ok(existingEmployee);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }

        // DELETE api/employees/5
        public IHttpActionResult Delete(int id)
        {
            var employee = listEmployees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            try
            {
                db.Employees.Remove(employee);

                db.SaveChanges();
                listEmployees.Remove(employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error: " + ex.Message);
                return InternalServerError();
            }
        }
    }
}
using EmployeeAPI.DataLayer;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private EmpDbContext _context;
        public CustomerController(EmpDbContext context)
        {
            _context = context;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Customers> Get()
        {
            return _context.tblCustomers.ToList<Customers>();
        }

        // POST api/<CustomerController>
        [Authorize(Roles ="admin")]
        [HttpPost]
        public IActionResult Post([FromBody] Customers customer)
        {
            customer.dateOfJoining = DateTime.Today+"";
            Customers cust = _context.tblCustomers.FirstOrDefault<Customers>(c => c.username == customer.username);
            if(cust==null)
            {
                _context.tblCustomers.Add(customer);
                Credentials credentials = new Credentials()
                {
                    password = customer.username,
                    username = customer.username,
                    role = "customer"
                };
                _context.tblCredentials.Add(credentials);
                _context.SaveChanges();
                return Ok(customer);
            }
            return BadRequest("Customer is already added");

        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using EmployeeAPI.DataLayer;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly EmpDbContext _context;
        public RegisterController(EmpDbContext context)
        {
            _context = context;
        }
        // POST <RegisterController>
        [Route("Register")]
        [HttpPost]
        public IActionResult Post([FromBody] RegisterCredentials cred)
        {
            cred.dateOfJoining = DateTime.Today+"";
            if(cred!=null)
            {
                if (cred.username.Trim() != "" && cred.password.Trim() != "" && cred.role.Trim() != "")
                {
                    if (_context.tblCredentials.FirstOrDefault<Credentials>(c => c.username == cred.username) != null)
                    {
                        return BadRequest("Username is already registered");
                    }
                    else
                    {
                        if (cred.role.Trim().ToLower() == "customer")
                        {
                            Customers cs = new Customers();
                            cs.fullName = cred.fullName;
                            cs.username = cred.username;
                            cs.dateOfJoining = cred.dateOfJoining;
                            cs.phoneNumber = cred.phoneNumber;
                            _context.tblRegisterCredentials.Add(cred);
                            _context.tblCustomers.Add(cs);
                            Credentials credentials = new Credentials();
                            credentials.username = cred.username;
                            credentials.password = cred.password;
                            credentials.role = cred.role;
                            _context.tblCredentials.Add(credentials);
                            _context.SaveChanges();
                            return Ok("Customer added");
                        }
                        else if (cred.role.Trim().ToLower() == "admin")
                        {
                            Credentials credentials = new Credentials();
                            credentials.username = cred.username;
                            credentials.password = cred.password;
                            credentials.role = cred.role;
                            _context.tblCredentials.Add(credentials);
                            _context.tblRegisterCredentials.Add(cred);
                            _context.SaveChanges();
                            return Ok("Admin added");
                        }
                    }
                    return BadRequest("Invalid role credentials");
                }
            }
            return BadRequest("Invalid data sent");
        }
    }
}

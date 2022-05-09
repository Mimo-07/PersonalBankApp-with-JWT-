using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.DataLayer
{
    public class EmpDbContext:DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options):base(options)
        {
                
        }

        public DbSet<Credentials> tblCredentials { get; set; }
        public DbSet<Customers> tblCustomers { get; set; }

        public DbSet<RegisterCredentials> tblRegisterCredentials { get; set; }
    }
}

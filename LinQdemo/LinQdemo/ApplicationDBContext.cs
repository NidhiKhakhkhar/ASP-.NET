using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQdemo
{
    internal class ApplicationDBContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder
       optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=NISHIT1208\SQLEXPRESS;Database=LinQ;Trusted_Connection=True");
        }

    }
}

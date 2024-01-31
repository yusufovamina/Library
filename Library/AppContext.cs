using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class AppContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        public AppContext()
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = STHQ012E-09; Database=LibraryProject; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;");

        }
    }
}

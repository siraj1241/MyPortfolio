using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyPortfolio.Models.Data
{
    public class PortfolioDbContext:DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options): base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }



    }
}

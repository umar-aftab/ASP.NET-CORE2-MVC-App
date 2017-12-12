using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShBazmool.Models
{
    public class ShBazmoolDbContext : DbContext
    {
        public DbSet<Lecture> Lectures{ get; set; }
        public DbSet<Writing> Writings{ get; set; }
        public DbSet<Article> Articles{ get; set; }
        public DbSet<Explanation> Explanations { get; set; }
        public DbSet<User> Users { get; set; }

        public ShBazmoolDbContext(DbContextOptions<ShBazmoolDbContext> options) : base(options)
        {
            //   Database.EnsureCreated();
            
        }
    }
}

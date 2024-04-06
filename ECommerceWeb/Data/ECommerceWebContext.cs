using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerceWeb.Models;

namespace ECommerceWeb.Data
{
    public class ECommerceWebContext : DbContext
    {
        public ECommerceWebContext (DbContextOptions<ECommerceWebContext> options)
            : base(options)
        {
        }

        public DbSet<ECommerceWeb.Models.Category> Category { get; set; } = default!;

        public DbSet<ECommerceWeb.Models.Product>? Product { get; set; }

        public DbSet<ECommerceWeb.Models.User>? User { get; set; }
    }
}

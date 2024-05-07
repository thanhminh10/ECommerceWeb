using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerceWeb.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ECommerceWeb.Data
{
    public class ECommerceWebContext : IdentityDbContext<ApplicationUser> // Update inheritance
    {
        public ECommerceWebContext (DbContextOptions<ECommerceWebContext> options)
            : base(options)
        {
        }

        public DbSet<ECommerceWeb.Models.Category> Category { get; set; } = default!;

        public DbSet<ECommerceWeb.Models.Product>? Product { get; set; }

        public DbSet<ECommerceWeb.Models.Brand>? Brand { get; set; }


        public DbSet<ECommerceWeb.Models.Color>? Color { get; set; }
       
    }


}

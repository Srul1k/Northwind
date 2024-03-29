﻿using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models;

namespace Northwind.Infrastructure.Data
{
    public class NorthwindDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
            : base(options) { }
    }
}

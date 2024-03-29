﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Northwind.Application.Interfaces;
using Northwind.Application.Models;

namespace Northwind.Infrastructure.Repositories
{
    public class ProductRepository : EntityRepository<Product>, IProductAsyncRepository
    {
        public ProductRepository(DbContext dbContext) : base(dbContext) { }

        public override async Task<Product> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await Set
                .Include(s => s.Supplier)
                .Include(c => c.Category)
                .AsNoTracking()
                .FirstAsync(predicate);
        }

        public override async Task<IEnumerable<Product>> FindAllAsync()
        {
            return await Set
               .Include(s => s.Supplier)
               .Include(c => c.Category)
               .OrderByDescending(p => p.ProductId)
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<IEnumerable<Product>> TakeLast(int count)
        {
            return await Set
                .Include(s => s.Supplier)
                .Include(c => c.Category)
                .OrderByDescending(p => p.ProductId)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
            {

            }

            public DbSet<ProductInfo> ProductInfo {get; set;}

    }
}
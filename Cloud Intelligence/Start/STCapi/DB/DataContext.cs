using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCapi.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Links> Links { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}

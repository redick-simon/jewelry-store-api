using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreApi.Data
{
    public class JewelryDbContext : DbContext
    {
        public JewelryDbContext(DbContextOptions<JewelryDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}

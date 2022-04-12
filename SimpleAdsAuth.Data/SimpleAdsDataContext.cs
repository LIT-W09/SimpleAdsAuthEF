using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdsAuth.Data
{
    public class SimpleAdsDataContext : DbContext
    {
        private readonly string _connectionString;

        public SimpleAdsDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<SimpleAd> Ads { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

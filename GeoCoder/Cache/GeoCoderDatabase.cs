using GeoCoder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCoder.Cache
{
    internal class GeoCoderDatabase : DbContext
    {
        public DbSet<GeoLocation> Locations => Set<GeoLocation>();
        public GeoCoderDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=geocoder.db");
        }
    }
}

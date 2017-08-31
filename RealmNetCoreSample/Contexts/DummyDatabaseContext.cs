using System;
using Microsoft.EntityFrameworkCore;
using RealmNetCoreSample.Models;

namespace RealmNetCoreSample.Contexts
{
    public class DummyDatabaseContext : DbContext
    {
        public DummyDatabaseContext()
        {
        }

        public DummyDatabaseContext(DbContextOptions<DummyDatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("dummy");
        }

        public DbSet<RealmNetCoreSample.Models.Announcement> Announcement { get; set; }

        public DbSet<RealmNetCoreSample.Models.User> User { get; set; }
    }
}

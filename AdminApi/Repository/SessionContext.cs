using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SessionContext : DbContext
    {
        public SessionContext(DbContextOptions<SessionContext> options) : base(options)
        { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<AccessCard> AccessCards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<User>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Role>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<IdentityRole>().Metadata.SetIsTableExcludedFromMigrations(true);

        }
    }
}

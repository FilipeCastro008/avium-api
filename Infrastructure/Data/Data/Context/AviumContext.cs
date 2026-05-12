using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Context {
    public class AviumContext : DbContext {

        public AviumContext(DbContextOptions<AviumContext> options) : base (options) {}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AviumContext).Assembly); 
            
            base.OnModelCreating(modelBuilder);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Urna.Entity.Entity;

namespace Urna.Entity.Context
{
    public partial class UrnaContext : DbContext
    {
        
        public UrnaContext(DbContextOptions<UrnaContext> options)
            : base(options)
        {
        }
        public DbSet<Voto> votos{ get; set; }
        public DbSet<Candidato> candidatos{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidato>()
             .HasKey(x => x.Id);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}

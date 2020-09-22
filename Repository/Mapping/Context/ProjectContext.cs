using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping.Context {
    public class ProjectContext : DbContext {
        public DbSet<Amigo> Amigos { get; set; }
        public DbSet <Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Parceiro> Parceiros { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(b => { b.AddConsole(); });

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseLoggerFactory(loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AmigoMap());
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new ParceiroMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

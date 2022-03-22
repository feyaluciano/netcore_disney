using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{



    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Personaje>? Personajes { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<VideoFilm>? VideosFilm { get; set; }
        public DbSet<Genero>? Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personaje>(
        eb =>
        {
            eb.Property(b => b.Peso).HasColumnType("decimal(3, 2)");
        });            
        }

    }
}
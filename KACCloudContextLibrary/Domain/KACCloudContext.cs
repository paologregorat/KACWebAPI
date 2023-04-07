using KACCloudContextLibrary.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KACCloudContextLibrary.Domain
{
    public class KACCloudContext : DbContext
    {
        private static string _connectionString;
        public static KACCloudContext GetInstance()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(_connectionString);
            return new KACCloudContext(optionsBuilder.Options, _connectionString);

        }

        public KACCloudContext(DbContextOptions<KACCloudContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public KACCloudContext(DbContextOptions options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        public virtual DbSet<AnagraficaCloud> Anagrafiche { get; set; } = null;
        public virtual DbSet<Utente> Utenti { get; set; } = null;
        public virtual DbSet<Ruolo> Ruoli { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ANAGRAFICHE
            modelBuilder.Entity<AnagraficaCloud>(entity =>
            {
                entity.ToTable("Anagrafiche");

                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            //UTENTI
            modelBuilder.Entity<Utente>(entity =>
            {
                entity.ToTable("Utenti");

                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.HasOne(d => d.Ruolo)
                   .WithMany(p => p!.Utenti)
                   .HasForeignKey(d => d.RuoloID)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.NoAction);
            });

            //RUOLI
            modelBuilder.Entity<Ruolo>(entity =>
            {
                entity.ToTable("Ruoli");

                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

        }
    }
}

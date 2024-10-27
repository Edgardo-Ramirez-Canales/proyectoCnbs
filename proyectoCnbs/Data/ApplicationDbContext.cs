using Microsoft.EntityFrameworkCore;
using proyectoCnbs.Models;

namespace proyectoCnbs.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
        
        }

        //AGREGAR MODELO (Cada modelo corresponde a una tabla en la BD)
        public DbSet<ApiData> ApiData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "ConexionSql",
                    sqlOptions => sqlOptions.CommandTimeout(120) // Timeout en segundos
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApiData>()
                .HasIndex(a => a.Id)
                .HasDatabaseName("IX_Id");

            modelBuilder.Entity<ApiData>()
                .HasIndex(a => a.FechaAConsultar)
                .HasDatabaseName("IX_FechaAConsultar");

        }
    }
}

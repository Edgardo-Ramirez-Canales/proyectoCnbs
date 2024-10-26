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
    }
}

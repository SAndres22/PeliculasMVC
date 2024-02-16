using Microsoft.EntityFrameworkCore;
using PeliculasMVC.Models;

namespace PeliculasMVC.Data
{
    public class PeliculasMVCContext : DbContext
    {
        //este constructor se utiliza para crear una instancia de PeliculasMVCContext,
        //que es un contexto de base de datos, y se le pasa un conjunto de opciones de configuración
        //que se utilizarán para configurar ese contexto de la base de datos.
        public PeliculasMVCContext (DbContextOptions<PeliculasMVCContext> options): base(options)
        {
        }

        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<PeliculaGenero> PeliculaGenero { get; set; }
    }
}

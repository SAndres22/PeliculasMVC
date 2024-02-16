using System.ComponentModel.DataAnnotations;

namespace PeliculasMVC.Models
{
    public class PeliculaGenero
    {
        [Key]
        public int Id_PeliculaGenero { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }

        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
    }
}

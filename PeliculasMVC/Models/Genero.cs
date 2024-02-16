using System.ComponentModel.DataAnnotations;

namespace PeliculasMVC.Models
{
    public class Genero
    {
        [Key]
        public int Id_Genero { get; set; }
        public string? Nombre { get; set; }

        // Navegación a través de la relación muchos a muchos
        public List<PeliculaGenero> PeliculaGeneros { get; set; }
    }
}

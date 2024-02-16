using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasMVC.Models;

public class Pelicula
{
    [Key]
    public int Id_Pelicula { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required(ErrorMessage ="El campo Titulo no puede estar vacio")]
    public string? Titulo { get; set; }


    [Display(Name = "Fecha De Lanzamiento")]
    [Required(ErrorMessage = "El campo Fecha de Lanzamiento no puede estar vacio")]
    [DataType(DataType.Date)]
    public DateTime Fecha_Lanzamiento { get; set; }


    [Range(1, 100000 , ErrorMessage ="El rango va hasta $100.000")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    [Required(ErrorMessage = "El campo Precio no puede estar vacio")]
    public decimal Precio { get; set; }


    // Navegación a través de la relación muchos a muchos
    public List<PeliculaGenero>? PeliculaGenero { get; set; }


}

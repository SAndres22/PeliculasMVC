using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace PeliculasMVC.Controllers
{
    public class HolaMundoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Url que se encia
        //https://localhost:7093/HolaMundo/Welcome?nombre=Andres&edad=20
        public string Welcome(string nombre, int edad = 22)
        {
            return HtmlEncoder.Default.Encode($"Bienvenid@ {nombre} , ud tiene {edad} años");

        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PeliculasMVC.Data;
using PeliculasMVC.Models;

namespace PeliculasMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly PeliculasMVCContext _context;

        public MoviesController(PeliculasMVCContext context)
        {
            _context = context;
        }

        // GET: Movies

        public async Task<IActionResult> Index()
        {
            ViewBag.AllGeneros = new SelectList(_context.Genero, "Id_Genero", "Nombre");

            ViewBag.Generos = _context.Pelicula
        .Include(p => p.PeliculaGenero)
        .ThenInclude(pg => pg.Genero)
        .ToList();

            return View(await _context.Pelicula.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {

                return View("_NotFound");
            }

            var movie = await _context.Pelicula
        .Include(p => p.PeliculaGenero)
        .ThenInclude(pg => pg.Genero)
        .FirstOrDefaultAsync(p => p.Id_Pelicula == id);

            if (movie == null)
            {

                return View("_NotFound");
            }

            return View(movie);
        }

        // GET: Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Generos = new SelectList(_context.Genero, "Id_Genero", "Nombre");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula, int[] generosSeleccionados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();

                if (generosSeleccionados != null)
                {
                    foreach (var generoId in generosSeleccionados)
                    {
                        _context.PeliculaGenero.Add(new PeliculaGenero { PeliculaId = pelicula.Id_Pelicula, GeneroId = generoId });
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Generos = new SelectList(_context.Genero, "Id_Genero", "Nombre");
            return View(pelicula);
        }



        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

           var movie = await _context.Pelicula
        .Include(p => p.PeliculaGenero)
        .ThenInclude(pg => pg.Genero)
        .FirstOrDefaultAsync(p => p.Id_Pelicula == id);

            if (movie == null)
            {
                return View("_NotFound");
            }

            var allGeneros = await _context.Genero.ToListAsync(); // Obtener todos los géneros disponibles
            ViewBag.AllGeneros = allGeneros;
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pelicula pelicula, int[] generosSeleccionados)
        {
            if (id != pelicula.Id_Pelicula)
            {
                return View("_NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Eliminar todos los géneros asociados a la película
                    _context.PeliculaGenero.RemoveRange(_context.PeliculaGenero.Where(pg => pg.PeliculaId == pelicula.Id_Pelicula));

                    // Agregar los nuevos géneros seleccionados
                    if (generosSeleccionados != null)
                    {
                        foreach (var generoId in generosSeleccionados)
                        {
                            _context.PeliculaGenero.Add(new PeliculaGenero { PeliculaId = pelicula.Id_Pelicula, GeneroId = generoId });
                        }
                    }

                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(pelicula.Id_Pelicula))
                    {
                        return View("_NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(pelicula);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("_NotFound"); 
            }

            var movie = await _context.Pelicula
        .Include(p => p.PeliculaGenero)
        .ThenInclude(pg => pg.Genero)
        .FirstOrDefaultAsync(p => p.Id_Pelicula == id);
            if (movie == null)
            {
                return View("_NotFound"); 
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Pelicula.FindAsync(id);
            if (movie != null)
            {
                _context.Pelicula.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool MovieExists(int id)
        {
            return _context.Pelicula.Any(e => e.Id_Pelicula == id);
        }


        /* Metodo para buscar  peliculas por nombre*/
        public async Task<IActionResult> BuscarPelicula(string cadenabuscarPelicula)
        {
            if (_context.Pelicula == null)
            {
                return Problem("El conjunto de entidades 'MvcMovieContext.Movie' es nulo.");
            }

            ViewBag.AllGeneros = new SelectList(_context.Genero, "Id_Genero", "Nombre");

            var movies = from m in _context.Pelicula
                         select m;

            if (!String.IsNullOrEmpty(cadenabuscarPelicula))
            {
                movies = movies.Where(s => s.Titulo!.Contains(cadenabuscarPelicula));
            }

            ViewBag.Generos = _context.Pelicula
        .Include(p => p.PeliculaGenero)
        .ThenInclude(pg => pg.Genero)
        .ToList();

            var  moviesList =  await movies.ToListAsync();

            return View("Index", moviesList);
        }


        public async Task<IActionResult> BuscarPorGenero(string genero)
        {

            ViewBag.AllGeneros = new SelectList(_context.Genero, "Id_Genero", "Nombre");

            // Realiza la búsqueda de películas por género
            var peliculas = await _context.Pelicula
                .Include(p => p.PeliculaGenero)
                .ThenInclude(pg => pg.Genero)
                .Where(p => p.PeliculaGenero.Any(pg => pg.Genero.Nombre == genero))
                .ToListAsync();

            // Devuelve la vista "Index" con las películas encontradas
            return View("Index", peliculas);
        }
    }


}

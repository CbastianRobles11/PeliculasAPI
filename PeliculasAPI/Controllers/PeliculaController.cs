
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController:ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PeliculaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeliculas()
        {
            // es include para que incluya el nombre de la pelicula
            var lista = await _db.Peliculas.OrderBy(p => p.NombrePelicula).Include(p =>
            p.Categoria).ToListAsync();

            //enviamos con ok a la lista
            return Ok(lista);

        }

        [HttpGet("{id}",Name = "GetPelicula")]
        public async Task<IActionResult> GetPelicula(int id)
        {
            // es include para que incluya el nombre de la pelicula
            var lista = await _db.Peliculas.Include(p => p.Categoria).FirstOrDefaultAsync(p=>p.Id==id);

            if (lista==null)
            {
                return NotFound();
            }

            //enviamos con ok a la lista
            return Ok(lista);

        }

        [HttpPost]
        public async Task<IActionResult> CreaPelicula([FromBody]Pelicula pelicula)
        {
            if (pelicula==null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _db.AddAsync(pelicula);
            // guardo cambios
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetPelicula", new {id=pelicula.Id},pelicula);

        }

    }
}

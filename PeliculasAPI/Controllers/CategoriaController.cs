using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        //usaremos el db ]Conctext 
        private readonly ApplicationDbContext _db;

        public CategoriaController(ApplicationDbContext db)
        {
            _db = db;
        }

        //todo lo que retorna de categorias
        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<Categoria>))]
        [ProducesResponseType(400)]//Bad Reqest
        public async Task<IActionResult> GetCategorias()
        {
            var lista = await _db.Categorias.OrderBy(c=>c.Nombre).ToListAsync();

            return  Ok(lista);
        }

        [HttpGet("{id}",Name = "GetCategoria")]
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(400)]//Bad Reqest
        [ProducesResponseType(404)]//Not Found
        public async Task<IActionResult> GetCategoria(int id)
        {
            // var categoria=await _db.Categorias.FindAsync(id);
            var categoria = await _db.Categorias.FirstOrDefaultAsync(c=>c.Id==id);

            if (categoria==null)
            {
                return NotFound();
            }

            return Ok(categoria);

        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]//Bad Reqest
        [ProducesResponseType(500)]//Not Found
        public async Task<IActionResult> CreaCategoria([FromBody] Categoria categoria)
        {

            if (categoria==null)
            {
                // el modalState devuelve todos los erroes
                return BadRequest(ModelState);

            }

            // si no mandamos los valores que son 
            if (!ModelState.IsValid)
            {
                // el modalState devuelve todos los erroes
                return BadRequest(ModelState);

            }

            await _db.AddAsync(categoria);
            //graba los cambios
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetCategoria", new { id = categoria.Id }, categoria);  


        }


    }
}

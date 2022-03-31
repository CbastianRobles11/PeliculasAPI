

using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Models;

namespace PeliculasAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) :base(option)
        {
                    
        }


        //agregamos modelos para crear tablas
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }

    }
}

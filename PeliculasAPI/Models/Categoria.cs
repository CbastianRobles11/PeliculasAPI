using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public bool Estado { get; set; }


    }
}

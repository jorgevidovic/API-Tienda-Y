using System.ComponentModel.DataAnnotations;

namespace API_Tienda_Y.Models
{
    public class Fabricante
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}

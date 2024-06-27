using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Tienda_Y.Models
{
    public class Producto
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Codigo_Fabricante { get; set; }

        [JsonIgnore]
        public Fabricante Fabricante { get; set; }
    }
}

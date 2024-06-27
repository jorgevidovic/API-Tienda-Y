namespace API_Tienda_Y.Models.DTO.Producto
{
    public class ProductoDTO
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Codigo_Fabricante { get; set; }
        public string Nombre_Fabricante { get; set; }
    }
}

﻿namespace API_Tienda_Y.Models.DTO.Producto
{
    public class ProductoCreateDTO
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Codigo_Fabricante { get; set; }
    }
}

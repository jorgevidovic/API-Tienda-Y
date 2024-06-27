using API_Tienda_Y.Models;
using API_Tienda_Y.Models.DTO.Producto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Tienda_Y.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly TiendaContext _context;

        public ProductoController(TiendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            return await _context.Producto
                .Include(p => p.Fabricante)
                .Select(p => new ProductoDTO
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Nombre_Fabricante = p.Fabricante.Nombre
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await _context.Producto
                .Include(p => p.Fabricante)
                .Select(p => new ProductoDTO
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Codigo_Fabricante = p.Codigo_Fabricante,
                    Nombre_Fabricante = p.Fabricante.Nombre
                }).FirstOrDefaultAsync(p => p.Codigo == id);
            if (producto == null)
            {
                return NotFound();
            }

            return producto;

        }

        [HttpGet("Fabricante/{fabricanteName}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductsByFabricante(string fabricanteName)
        {
            var productos = await _context.Producto
                .Include(p => p.Fabricante)
                .Where(p => p.Fabricante.Nombre.Contains(fabricanteName))
                .Select(p => new ProductoDTO
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Codigo_Fabricante = p.Codigo_Fabricante,
                    Nombre_Fabricante = p.Fabricante.Nombre

                })
                .ToListAsync();

            if (productos == null)
            {
                return NotFound();
            }

            return productos;
        }




        [HttpPost]
        public async Task<ActionResult<Producto>> AddProducto(ProductoCreateDTO productoCreateDto)
        {
            var producto = new Producto
            {
                Nombre = productoCreateDto.Nombre,
                Precio = productoCreateDto.Precio,
                Codigo_Fabricante = productoCreateDto.Codigo_Fabricante
            };


            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Codigo }, producto);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, ProductoCreateDTO productoCreateDto)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = productoCreateDto.Nombre;
            producto.Precio = productoCreateDto.Precio;
            producto.Codigo_Fabricante = productoCreateDto.Codigo_Fabricante;

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }

            }

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }   

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Codigo == id);
        }

    }
}


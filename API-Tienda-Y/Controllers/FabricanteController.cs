using API_Tienda_Y.Models;
using API_Tienda_Y.Models.DTO.Fabricante;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Tienda_Y.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricanteController : ControllerBase
    {
        private readonly TiendaContext _context;

        public FabricanteController(TiendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FabricanteDTO>>> GetFabricante()
        {
            return await _context.Fabricante
                .Include(f => f.Productos)
                .Select(f => new FabricanteDTO
                {
                    Codigo = f.Codigo,
                    Nombre = f.Nombre
                })
            .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FabricanteDTO>> GetFabricante(int id)
        {
            var fabricante = await _context.Fabricante
                .Include(f => f.Productos)
                .Select(f => new FabricanteDTO
                {
                    Codigo = f.Codigo,
                    Nombre = f.Nombre
                })
                .FirstOrDefaultAsync(f => f.Codigo == id);

            if (fabricante == null)
            {
                return NotFound();
            }

            return fabricante;

        }



        [HttpGet("Producto/{productName}")]
        public async Task<ActionResult<IEnumerable<FabricanteDTO>>> GetFabricantesByProduct(string productName)
        {
            var fabricantes = await _context.Fabricante
                .Include(f => f.Productos)
                .Where(f => f.Productos.Any(p => p.Nombre.Contains(productName)))
                .Select(f => new FabricanteDTO
                {
                    Codigo = f.Codigo,
                    Nombre = f.Nombre
                })
                .ToListAsync();


            if (fabricantes == null || fabricantes.Count == 0)
            {
                return NotFound();
            }

            return fabricantes;
        }


        [HttpPost]
        public async Task<ActionResult<Fabricante>> CreateFabricante(FabricanteCreateDTO fabricanteDto)
        {
            var fabricante = new Fabricante
            {
                Nombre = fabricanteDto.Nombre
            };

            _context.Fabricante.Add(fabricante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFabricante), new { id = fabricante.Codigo }, fabricante);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFabricante(int id, FabricanteCreateDTO fabricanteDto)
        {
            var fabricante = await _context.Fabricante.FindAsync(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            fabricante.Nombre = fabricanteDto.Nombre;

            _context.Entry(fabricante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricanteExists(id))
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
        public async Task<IActionResult> DeleteFabricante(int id)
        {
            var fabricante = await _context.Fabricante.FindAsync(id);
            
            if (fabricante == null)
            {
                return NotFound();
            }

            _context.Fabricante.Remove(fabricante);
            await _context.SaveChangesAsync();

            return NoContent();
        
        }



        private bool FabricanteExists(int id)
        {
            return _context.Fabricante.Any(e => e.Codigo == id);
        }


    }
}

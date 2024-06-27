using Microsoft.EntityFrameworkCore;

namespace API_Tienda_Y.Models
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options) { }

        public DbSet<Fabricante> Fabricante { get; set; }

        public DbSet<Producto> Producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Fabricante)
                .WithMany(f => f.Productos)
                .HasForeignKey(p => p.Codigo_Fabricante);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Codigo)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Fabricante>()
                .Property(f => f.Codigo)
                .ValueGeneratedOnAdd();
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }  // clave foranea
        public int FacturaId { get; set; }   // clave foranea
        public Producto Producto { get; set; } = new Producto();
        public int Cantidad { get; set; }

        [NotMapped]
        public decimal Subtotal => Producto.Precio * Cantidad;
    }
}
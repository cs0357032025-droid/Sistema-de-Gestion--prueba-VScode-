namespace Core
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public Producto Producto { get; set; } = new Producto();
        public int Cantidad { get; set; }
        public decimal Subtotal => Producto.Precio * Cantidad;
    }
}
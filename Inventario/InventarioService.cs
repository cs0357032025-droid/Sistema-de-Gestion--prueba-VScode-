using Core;
using System.Collections.Generic;
using System.Linq;

namespace Inventario
{
    public class InventarioService
    {
        private readonly AppDbContext _context;

        public InventarioService()
        {
            _context = new AppDbContext();
        }

        // agregar producto nuevo
        public void AgregarProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

        // obtener todos los productos
        public List<Producto> ObtenerProductos()
        {
            return _context.Productos.ToList();
        }

        // buscar producto por nombre
        public Producto? BuscarProducto(string nombre)
        {
            return _context.Productos.FirstOrDefault(p => p.Nombre.ToLower() == nombre.ToLower());
        }

        // descontar stock cuando se hace una venta
        public void DescontarStock(Producto producto, int cantidad)
        {
            producto.Stock -= cantidad;
            _context.SaveChanges();
        }

        // agregar stock cuando llega mercancia
        public void AgregarStock(Producto producto, int cantidad)
        {
            producto.Stock += cantidad;
            _context.SaveChanges();
        }

        // verificar productos con stock bajo
        public List<Producto> ProductosStockBajo()
        {
            return _context.Productos.Where(p => p.Stock <= p.StockMinimo).ToList();
        }
    }
}
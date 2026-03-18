using Core;
using System.Collections.Generic;
using System.Linq;

namespace Inventario
{
    public class InventarioService
    {
        private List<Producto> _productos = new List<Producto>();

        // agregar producto nuevo
        public void AgregarProducto(Producto producto)
        {
            _productos.Add(producto); // tenias _products
        }

        // obtener todos los productos
        public List<Producto> ObtenerProductos()
        {
            return _productos;
        }

        // buscar producto por su nombre :D
        public Producto? BuscarProducto(string nombre)
        {
            return _productos.FirstOrDefault(p => p.Nombre.ToLower() == nombre.ToLower());
        }

        // actualizar stock cuando se hace una venta
        public void DescontarStock(Producto producto, int cantidad)
        {
            producto.Stock -= cantidad;
        }

        // agregar stock cuando llega mercancia
        public void AgregarStock(Producto producto, int cantidad)
        {
            producto.Stock += cantidad;
        }

        // verificar productos con stock bajo
        public List<Producto> ProductosStockBajo()
        {
            return _productos.Where(p => p.Stock <= p.StockMinimo).ToList(); // tenias _prodctos
        }
    }
}
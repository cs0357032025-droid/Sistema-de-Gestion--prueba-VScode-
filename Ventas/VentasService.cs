using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ventas
{
    public class VentasService
    {
        private List<Factura> _facturas = new List<Factura>();
        private Inventario.InventarioService _inventario; // se corrigio el error de que tenia InventarioServices con s al final

        public VentasService(Inventario.InventarioService inventario) // 
        {
            _inventario = inventario;
        }

        // crear una nueva venta
        public Factura CrearVenta(List<DetalleFactura> detalles)
        {
            foreach (var detalle in detalles)
            {
                if (detalle.Producto.Stock < detalle.Cantidad)
                    throw new Exception($"El Stock es insuficiente para {detalle.Producto.Nombre}");
            }

            foreach (var detalle in detalles)
            {
                _inventario.DescontarStock(detalle.Producto, detalle.Cantidad);
            }

            var factura = new Factura
            {
                Id = _facturas.Count + 1,
                Fecha = DateTime.Now,
                Detalles = detalles
            };

            _facturas.Add(factura);
            return factura;
        }

        // obtener todas las ventas
        public List<Factura> ObtenerVentas()
        {
            return _facturas;
        }

        // ventas del dia
        public List<Factura> VentasDelDia()
        {
            return _facturas.Where(f => f.Fecha.Date == DateTime.Today).ToList();
        }

        // total vendido en el dia
        public decimal TotalDelDia()
        {
            return VentasDelDia().Sum(f => f.Total);
        }
    }
}
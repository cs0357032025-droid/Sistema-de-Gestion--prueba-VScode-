using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Inventario;
using Microsoft.EntityFrameworkCore;

namespace Ventas
{
    public class VentasService
    {
        private readonly AppDbContext _context;
        private readonly InventarioService _inventario;

        public VentasService(InventarioService inventario)
        {
            _context = new AppDbContext();
            _inventario = inventario;
        }

        // crear una nueva venta
        public Factura CrearVenta(List<DetalleFactura> detalles)
        {
            // verificar stock antes de vender
            foreach (var detalle in detalles)
            {
                if (detalle.Producto.Stock < detalle.Cantidad)
                    throw new Exception($"Stock insuficiente para {detalle.Producto.Nombre}");
            }

            // crear la factura
            var factura = new Factura
            {
                Fecha = DateTime.Now,
                Detalles = detalles
            };

            // guardar factura en la base de datos
            _context.Facturas.Add(factura);

            // descontar stock de cada producto
            foreach (var detalle in detalles)
            {
                _inventario.DescontarStock(detalle.Producto, detalle.Cantidad);
            }

            _context.SaveChanges();
            return factura;
        }

        // obtener todas las ventas
        public List<Factura> ObtenerVentas()
        {
            return _context.Facturas
                .Include(f => f.Detalles)
                .ToList();
        }

        // ventas del dia
        public List<Factura> VentasDelDia()
        {
            return _context.Facturas
                .Include(f => f.Detalles)
                .Where(f => f.Fecha.Date == DateTime.Today)
                .ToList();
        }

        // total vendido en el dia
        public decimal TotalDelDia()
        {
            return VentasDelDia().Sum(f => f.Total);
        }
    }
}
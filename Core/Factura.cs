using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Core
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();

        [NotMapped]
        public decimal Total => Detalles.Sum(d => d.Subtotal);
    }
}
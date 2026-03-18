using Core;
using System;

namespace Inventario
{
    public class MovimientoStock
    {
        public int Id { get; set; }
        public Producto Producto { get; set; } = new Producto(); // faltaba inicializar
        public int Cantidad { get; set; }
        public string Tipo { get; set; } = string.Empty; // faltaba inicializar
        public DateTime Fecha { get; set; }
    }
}
using System;

namespace Core
{
    //creacion de la clase producto
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; } // alerta de cuando se agote
    }
}
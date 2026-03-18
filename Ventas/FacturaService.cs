using Core;

namespace Ventas
{
    public class FacturaService
    {
        // Calcular el cambio que se le da al cliente
        public decimal CalcularCambio(decimal totalVenta, decimal montoPagado)
        {
            if (montoPagado < totalVenta)
                throw new Exception("El monto pagado es insuficiente");

            return montoPagado - totalVenta;
        }

        // Generar resumen de la factura
        public string GenerarResumen(Factura factura)
        {
            var resumen = $"Factura #{factura.Id}\n";
            resumen += $"Fecha: {factura.Fecha}\n";
            resumen += "------------------------\n";

            foreach (var detalle in factura.Detalles)
            {
                resumen += $"{detalle.Producto.Nombre} x{detalle.Cantidad} = {detalle.Subtotal:C}\n";
            }

            resumen += "------------------------\n";
            resumen += $"TOTAL: {factura.Total:C}";

            return resumen;
        }
    }
}
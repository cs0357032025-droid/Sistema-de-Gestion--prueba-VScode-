using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Core;
using Inventario;
using Ventas;

namespace UI
{
    public class FormVentas : Form
    {
        private DataGridView dgvDetalles = null!;
        private TextBox txtProducto = null!;
        private TextBox txtCantidad = null!;
        private TextBox txtMontoPagado = null!;
        private Label lblTitulo = null!;
        private Label lblProducto = null!;
        private Label lblCantidad = null!;
        private Label lblTotal = null!;
        private Label lblMontoPagado = null!;
        private Button btnAgregar = null!;
        private Button btnCobrar = null!;
        private Button btnLimpiar = null!;

        private readonly InventarioService _inventarioService;
        private readonly VentasService _ventasService;
        private readonly FacturaService _facturaService;
        private List<DetalleFactura> _detalles = new List<DetalleFactura>();

        public FormVentas(InventarioService inventarioService, VentasService ventasService)
        {
            _inventarioService = inventarioService;
            _ventasService = ventasService;
            _facturaService = new FacturaService();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Ventas";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitulo = new Label
            {
                Text = "Realizar Venta",
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(550, 35),
                Location = new System.Drawing.Point(25, 15),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            lblProducto = new Label
            {
                Text = "Producto:",
                Location = new System.Drawing.Point(25, 65),
                Size = new System.Drawing.Size(80, 25)
            };

            txtProducto = new TextBox
            {
                Location = new System.Drawing.Point(110, 62),
                Size = new System.Drawing.Size(200, 25)
            };

            lblCantidad = new Label
            {
                Text = "Cantidad:",
                Location = new System.Drawing.Point(25, 100),
                Size = new System.Drawing.Size(80, 25)
            };

            txtCantidad = new TextBox
            {
                Location = new System.Drawing.Point(110, 97),
                Size = new System.Drawing.Size(200, 25)
            };

            btnAgregar = new Button
            {
                Text = "Agregar",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(330, 62)
            };
            btnAgregar.Click += BtnAgregar_Click;

            dgvDetalles = new DataGridView
            {
                Size = new System.Drawing.Size(550, 220),
                Location = new System.Drawing.Point(25, 140),
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            lblTotal = new Label
            {
                Text = "Total: $0.00",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(25, 375),
                Size = new System.Drawing.Size(200, 30)
            };

            lblMontoPagado = new Label
            {
                Text = "Monto pagado:",
                Location = new System.Drawing.Point(25, 415),
                Size = new System.Drawing.Size(100, 25)
            };

            txtMontoPagado = new TextBox
            {
                Location = new System.Drawing.Point(130, 412),
                Size = new System.Drawing.Size(150, 25)
            };

            btnCobrar = new Button
            {
                Text = "Cobrar",
                Size = new System.Drawing.Size(100, 35),
                Location = new System.Drawing.Point(350, 410)
            };
            btnCobrar.Click += BtnCobrar_Click;

            btnLimpiar = new Button
            {
                Text = "Limpiar",
                Size = new System.Drawing.Size(100, 35),
                Location = new System.Drawing.Point(460, 410)
            };
            btnLimpiar.Click += BtnLimpiar_Click;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblProducto);
            this.Controls.Add(txtProducto);
            this.Controls.Add(lblCantidad);
            this.Controls.Add(txtCantidad);
            this.Controls.Add(btnAgregar);
            this.Controls.Add(dgvDetalles);
            this.Controls.Add(lblTotal);
            this.Controls.Add(lblMontoPagado);
            this.Controls.Add(txtMontoPagado);
            this.Controls.Add(btnCobrar);
            this.Controls.Add(btnLimpiar);
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            var producto = _inventarioService.BuscarProducto(txtProducto.Text);
            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad valida");
                return;
            }

            if (producto.Stock < cantidad)
            {
                MessageBox.Show($"Stock insuficiente. Stock disponible: {producto.Stock}");
                return;
            }

            var detalle = new DetalleFactura
            {
                Producto = producto,
                Cantidad = cantidad
            };

            _detalles.Add(detalle);
            ActualizarTabla();
            txtProducto.Clear();
            txtCantidad.Clear();
        }

        private void ActualizarTabla()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = _detalles;

            decimal total = 0;
            foreach (var detalle in _detalles)
                total += detalle.Subtotal;

            lblTotal.Text = $"Total: {total:C}";
        }

        private void BtnCobrar_Click(object? sender, EventArgs e)
        {
            if (_detalles.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta");
                return;
            }

            if (!decimal.TryParse(txtMontoPagado.Text, out decimal montoPagado))
            {
                MessageBox.Show("Ingrese un monto valido");
                return;
            }

            try
            {
                var factura = _ventasService.CrearVenta(_detalles);
                var cambio = _facturaService.CalcularCambio(factura.Total, montoPagado);
                var resumen = _facturaService.GenerarResumen(factura);

                MessageBox.Show($"{resumen}\n\nMonto pagado: {montoPagado:C}\nCambio: {cambio:C}");
                BtnLimpiar_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            _detalles.Clear();
            ActualizarTabla();
            txtMontoPagado.Clear();
        }
    }
}
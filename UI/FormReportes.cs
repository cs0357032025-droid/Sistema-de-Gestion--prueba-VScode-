using System;
using System.Windows.Forms;
using Inventario;
using Ventas;

namespace UI
{
    public class FormReportes : Form
    {
        private DataGridView dgvVentas = null!;
        private DataGridView dgvStockBajo = null!;
        private Label lblTitulo = null!;
        private Label lblVentas = null!;
        private Label lblStockBajo = null!;
        private Label lblTotalDia = null!;
        private Button btnActualizar = null!;

        private readonly VentasService _ventasService;
        private readonly InventarioService _inventarioService;

        public FormReportes(VentasService ventasService, InventarioService inventarioService)
        {
            _ventasService = ventasService;
            _inventarioService = inventarioService;
            InitializeComponents();
            CargarReportes();
        }

        private void InitializeComponents()
        {
            this.Text = "Reportes";
            this.Size = new System.Drawing.Size(650, 580);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitulo = new Label
            {
                Text = "Reportes del Sistema",
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(600, 35),
                Location = new System.Drawing.Point(25, 15),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            lblVentas = new Label
            {
                Text = "Ventas del dia:",
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(25, 60),
                Size = new System.Drawing.Size(200, 25)
            };

            dgvVentas = new DataGridView
            {
                Size = new System.Drawing.Size(600, 180),
                Location = new System.Drawing.Point(25, 90),
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            lblTotalDia = new Label
            {
                Text = "Total del dia: $0.00",
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(25, 280),
                Size = new System.Drawing.Size(300, 25)
            };

            lblStockBajo = new Label
            {
                Text = "Productos con stock bajo:",
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(25, 320),
                Size = new System.Drawing.Size(300, 25)
            };

            dgvStockBajo = new DataGridView
            {
                Size = new System.Drawing.Size(600, 160),
                Location = new System.Drawing.Point(25, 350),
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            btnActualizar = new Button
            {
                Text = "Actualizar",
                Size = new System.Drawing.Size(150, 35),
                Location = new System.Drawing.Point(250, 520)
            };
            btnActualizar.Click += BtnActualizar_Click;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblVentas);
            this.Controls.Add(dgvVentas);
            this.Controls.Add(lblTotalDia);
            this.Controls.Add(lblStockBajo);
            this.Controls.Add(dgvStockBajo);
            this.Controls.Add(btnActualizar);
        }

        private void CargarReportes()
        {
            dgvVentas.DataSource = _ventasService.VentasDelDia();
            lblTotalDia.Text = $"Total del dia: {_ventasService.TotalDelDia():C}";
            dgvStockBajo.DataSource = _inventarioService.ProductosStockBajo();
        }

        private void BtnActualizar_Click(object? sender, EventArgs e)
        {
            CargarReportes();
            MessageBox.Show("Reportes actualizados");
        }
    }
}
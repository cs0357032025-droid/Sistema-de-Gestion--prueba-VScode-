using System;
using System.Windows.Forms;
using Inventario;
using Ventas;

namespace UI
{
    public class FormMenu : Form
    {
        private Button btnVentas = null!;
        private Button btnInventario = null!;
        private Button btnReportes = null!;
        private Label lblTitulo = null!;
        private readonly VentasService _ventasService;
        private readonly InventarioService _inventarioService;

        public FormMenu()
        {
            _inventarioService = new InventarioService();
            _ventasService = new VentasService(_inventarioService);
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Sistema de Ventas";
            Size = new System.Drawing.Size(400, 350);
            StartPosition = FormStartPosition.CenterScreen;

            lblTitulo = new Label
            {
                Text = "Sistema de Ventas",
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(350, 40),
                Location = new System.Drawing.Point(25, 30),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            btnVentas = new Button
            {
                Text = "Ventas",
                Size = new System.Drawing.Size(200, 50),
                Location = new System.Drawing.Point(100, 100)
            };
            btnVentas.Click += BtnVentas_Click;

            btnInventario = new Button
            {
                Text = "Inventario",
                Size = new System.Drawing.Size(200, 50),
                Location = new System.Drawing.Point(100, 170)
            };
            btnInventario.Click += BtnInventario_Click;

            btnReportes = new Button
            {
                Text = "Reportes",
                Size = new System.Drawing.Size(200, 50),
                Location = new System.Drawing.Point(100, 240)
            };
            btnReportes.Click += BtnReportes_Click;

            Controls.Add(lblTitulo);
            Controls.Add(btnVentas);
            Controls.Add(btnInventario);
            Controls.Add(btnReportes);
        }

        private void BtnVentas_Click(object? sender, EventArgs e)
        {
            var formVentas = new FormVentas(_inventarioService, _ventasService);
            formVentas.Show();
        }

        private void BtnInventario_Click(object? sender, EventArgs e)
        {
            var formInventario = new FormInventario(_inventarioService);
            formInventario.Show();
        }

        private void BtnReportes_Click(object? sender, EventArgs e)
        {
            var formReportes = new FormReportes(_ventasService, _inventarioService);
            formReportes.Show();
        }
    }
}
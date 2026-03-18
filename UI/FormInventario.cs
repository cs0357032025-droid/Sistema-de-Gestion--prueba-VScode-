using System;
using System.Windows.Forms;
using Core;
using Inventario;

namespace UI
{
    public class FormInventario : Form
    {
        private DataGridView dgvProductos = null!;
        private Button btnAgregar = null!;
        private Button btnActualizarStock = null!;  // faltaba declarar esta variable
        private Label lblTitulo = null!;

        private readonly InventarioService _inventarioService;

        public FormInventario(InventarioService inventarioService)
        {
            _inventarioService = inventarioService;
            InitializeComponents();  // cambiado a InitializeComponents con S
            CargarProductos();
        }

        private void InitializeComponents()  // con S al final
        {
            this.Text = "Inventario";  // tenia Invetario
            this.Size = new System.Drawing.Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;  // tenia StartPositon sin i

            lblTitulo = new Label
            {
                Text = "Gestion de Inventario",
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(550, 35),
                Location = new System.Drawing.Point(25, 15),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            dgvProductos = new DataGridView
            {
                Size = new System.Drawing.Size(550, 280),
                Location = new System.Drawing.Point(25, 60),
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            btnAgregar = new Button
            {
                Text = "Agregar Producto",
                Size = new System.Drawing.Size(180, 40),
                Location = new System.Drawing.Point(25, 360)  // tenia Drawig en lugar de Drawing
            };
            btnAgregar.Click += BtnAgregar_Click;

            btnActualizarStock = new Button
            {
                Text = "Actualizar Stock",
                Size = new System.Drawing.Size(180, 40),
                Location = new System.Drawing.Point(220, 360)
            };
            btnActualizarStock.Click += BtnActualizarStock_Click;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(dgvProductos);
            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnActualizarStock);
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = _inventarioService.ObtenerProductos();
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            var nombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre del producto:", "Agregar Producto");
            var precioStr = Microsoft.VisualBasic.Interaction.InputBox("Precio del producto:", "Agregar Producto");
            var stockStr = Microsoft.VisualBasic.Interaction.InputBox("Stock inicial:", "Agregar Producto");
            var stockMinStr = Microsoft.VisualBasic.Interaction.InputBox("Stock minimo:", "Agregar Producto");

            if (string.IsNullOrEmpty(nombre)) return;

            var producto = new Producto
            {
                Nombre = nombre,
                Precio = decimal.Parse(precioStr),
                Stock = int.Parse(stockStr),
                StockMinimo = int.Parse(stockMinStr)
            };

            _inventarioService.AgregarProducto(producto);
            CargarProductos();
            MessageBox.Show("Producto agregado exitosamente.");
        }

        private void BtnActualizarStock_Click(object? sender, EventArgs e)
        {
            var nombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre del producto:", "Actualizar Stock");
            var cantidadStr = Microsoft.VisualBasic.Interaction.InputBox("Cantidad a agregar:", "Actualizar Stock");

            var producto = _inventarioService.BuscarProducto(nombre);
            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            _inventarioService.AgregarStock(producto, int.Parse(cantidadStr)); // pasamos el objeto producto no el nombre
            CargarProductos();
            MessageBox.Show("Stock actualizado exitosamente.");
        }
    }
}
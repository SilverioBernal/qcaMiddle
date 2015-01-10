namespace BP
{
    partial class frmEstadoPedidos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstadoPedidos));
            this.lblNumPedido = new System.Windows.Forms.Label();
            this.lblCodCliente = new System.Windows.Forms.Label();
            this.lblRepVentas = new System.Windows.Forms.Label();
            this.lblNomCliente = new System.Windows.Forms.Label();
            this.btnNuevaConsulta = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.Nom_Cliente = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Numero_Cliente = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Numero_Orden = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Fecha_Orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hora_Orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha_Comprometida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num_Rep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TbxNumPedido = new System.Windows.Forms.MaskedTextBox();
            this.cbxRepVentas = new System.Windows.Forms.ComboBox();
            this.cbxNomCliente = new System.Windows.Forms.ComboBox();
            this.cbxCodCliente = new System.Windows.Forms.ComboBox();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.Num_Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc_Productos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnResultados = new System.Windows.Forms.Button();
            this.tbxDetalle = new System.Windows.Forms.TextBox();
            this.txtCotizacion = new System.Windows.Forms.TextBox();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.lblDatosCotiza = new System.Windows.Forms.Label();
            this.lblDatosEntre = new System.Windows.Forms.Label();
            this.lblDetOrden = new System.Windows.Forms.Label();
            this.lblDetalleEntrega = new System.Windows.Forms.Label();
            this.txtDetalleEntrega = new System.Windows.Forms.TextBox();
            this.mskTFechaGeneracion = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDocTransp = new System.Windows.Forms.TextBox();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.lblFactura = new System.Windows.Forms.Label();
            this.txtRecibido = new System.Windows.Forms.TextBox();
            this.groupBoxDetalle = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.groupBoxDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumPedido
            // 
            this.lblNumPedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumPedido.AutoSize = true;
            this.lblNumPedido.Location = new System.Drawing.Point(655, 10);
            this.lblNumPedido.Name = "lblNumPedido";
            this.lblNumPedido.Size = new System.Drawing.Size(80, 13);
            this.lblNumPedido.TabIndex = 0;
            this.lblNumPedido.Text = "Número Pedido";
            // 
            // lblCodCliente
            // 
            this.lblCodCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCodCliente.AutoSize = true;
            this.lblCodCliente.Location = new System.Drawing.Point(36, 65);
            this.lblCodCliente.Name = "lblCodCliente";
            this.lblCodCliente.Size = new System.Drawing.Size(78, 13);
            this.lblCodCliente.TabIndex = 2;
            this.lblCodCliente.Text = "Codigo Cliente:";
            // 
            // lblRepVentas
            // 
            this.lblRepVentas.AutoSize = true;
            this.lblRepVentas.Location = new System.Drawing.Point(36, 7);
            this.lblRepVentas.Name = "lblRepVentas";
            this.lblRepVentas.Size = new System.Drawing.Size(83, 13);
            this.lblRepVentas.TabIndex = 6;
            this.lblRepVentas.Text = "Representante :";
            // 
            // lblNomCliente
            // 
            this.lblNomCliente.AutoSize = true;
            this.lblNomCliente.Location = new System.Drawing.Point(36, 37);
            this.lblNomCliente.Name = "lblNomCliente";
            this.lblNomCliente.Size = new System.Drawing.Size(82, 13);
            this.lblNomCliente.TabIndex = 4;
            this.lblNomCliente.Text = "Nombre Cliente:";
            // 
            // btnNuevaConsulta
            // 
            this.btnNuevaConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNuevaConsulta.Location = new System.Drawing.Point(17, 472);
            this.btnNuevaConsulta.Name = "btnNuevaConsulta";
            this.btnNuevaConsulta.Size = new System.Drawing.Size(99, 23);
            this.btnNuevaConsulta.TabIndex = 6;
            this.btnNuevaConsulta.Text = "Nueva Consulta";
            this.btnNuevaConsulta.UseVisualStyleBackColor = true;
            this.btnNuevaConsulta.Click += new System.EventHandler(this.btnNuevaConsulta_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nom_Cliente,
            this.Numero_Cliente,
            this.Numero_Orden,
            this.Fecha_Orden,
            this.Hora_Orden,
            this.Fecha_Comprometida,
            this.Num_Rep});
            this.dgvResultados.Location = new System.Drawing.Point(12, 89);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowHeadersVisible = false;
            this.dgvResultados.Size = new System.Drawing.Size(947, 362);
            this.dgvResultados.TabIndex = 5;
            this.dgvResultados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellClick);
            // 
            // Nom_Cliente
            // 
            this.Nom_Cliente.HeaderText = "Cliente";
            this.Nom_Cliente.Name = "Nom_Cliente";
            this.Nom_Cliente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Nom_Cliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Nom_Cliente.Width = 220;
            // 
            // Numero_Cliente
            // 
            this.Numero_Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Numero_Cliente.HeaderText = "Numero_Cliente";
            this.Numero_Cliente.Name = "Numero_Cliente";
            this.Numero_Cliente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Numero_Cliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Numero_Orden
            // 
            this.Numero_Orden.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Numero_Orden.HeaderText = "Numero_Orden";
            this.Numero_Orden.Name = "Numero_Orden";
            this.Numero_Orden.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Numero_Orden.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Fecha_Orden
            // 
            this.Fecha_Orden.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fecha_Orden.HeaderText = "Fecha_Orden";
            this.Fecha_Orden.Name = "Fecha_Orden";
            // 
            // Hora_Orden
            // 
            this.Hora_Orden.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Hora_Orden.HeaderText = "Hora_Orden";
            this.Hora_Orden.Name = "Hora_Orden";
            // 
            // Fecha_Comprometida
            // 
            this.Fecha_Comprometida.HeaderText = "Fecha_Comprometida";
            this.Fecha_Comprometida.Name = "Fecha_Comprometida";
            // 
            // Num_Rep
            // 
            this.Num_Rep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Num_Rep.HeaderText = "Numero_Representante";
            this.Num_Rep.Name = "Num_Rep";
            // 
            // TbxNumPedido
            // 
            this.TbxNumPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxNumPedido.BeepOnError = true;
            this.TbxNumPedido.Location = new System.Drawing.Point(824, 7);
            this.TbxNumPedido.Mask = "9999999999999999";
            this.TbxNumPedido.Name = "TbxNumPedido";
            this.TbxNumPedido.Size = new System.Drawing.Size(100, 20);
            this.TbxNumPedido.TabIndex = 0;
            this.TbxNumPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbxNumPedido_KeyPress);
            // 
            // cbxRepVentas
            // 
            this.cbxRepVentas.FormattingEnabled = true;
            this.cbxRepVentas.Location = new System.Drawing.Point(134, 7);
            this.cbxRepVentas.Name = "cbxRepVentas";
            this.cbxRepVentas.Size = new System.Drawing.Size(276, 21);
            this.cbxRepVentas.TabIndex = 1;
            this.cbxRepVentas.Text = "--Seleccione Representante--";
            this.cbxRepVentas.SelectedIndexChanged += new System.EventHandler(this.cbxRepVentas_SelectedIndexChanged);
            // 
            // cbxNomCliente
            // 
            this.cbxNomCliente.FormattingEnabled = true;
            this.cbxNomCliente.Location = new System.Drawing.Point(134, 34);
            this.cbxNomCliente.Name = "cbxNomCliente";
            this.cbxNomCliente.Size = new System.Drawing.Size(276, 21);
            this.cbxNomCliente.TabIndex = 3;
            this.cbxNomCliente.Text = "--Seleccione Un Cliente--";
            this.cbxNomCliente.SelectedIndexChanged += new System.EventHandler(this.cbxNomCliente_SelectedIndexChanged);
            // 
            // cbxCodCliente
            // 
            this.cbxCodCliente.FormattingEnabled = true;
            this.cbxCodCliente.Location = new System.Drawing.Point(134, 62);
            this.cbxCodCliente.Name = "cbxCodCliente";
            this.cbxCodCliente.Size = new System.Drawing.Size(100, 21);
            this.cbxCodCliente.TabIndex = 2;
            this.cbxCodCliente.Text = "--Codigo Cliente--";
            this.cbxCodCliente.SelectedIndexChanged += new System.EventHandler(this.cbxCodCliente_SelectedIndexChanged);
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num_Articulo,
            this.Cantidad,
            this.Desc_Productos,
            this.Precio});
            this.dgvDetalles.Location = new System.Drawing.Point(149, 328);
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.Size = new System.Drawing.Size(553, 104);
            this.dgvDetalles.TabIndex = 7;
            this.dgvDetalles.Visible = false;
            // 
            // Num_Articulo
            // 
            this.Num_Articulo.HeaderText = "Numero Articulo";
            this.Num_Articulo.Name = "Num_Articulo";
            // 
            // Cantidad
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            // 
            // Desc_Productos
            // 
            this.Desc_Productos.HeaderText = "Desc_Productos";
            this.Desc_Productos.Name = "Desc_Productos";
            this.Desc_Productos.Width = 250;
            // 
            // Precio
            // 
            dataGridViewCellStyle2.Format = "C";
            dataGridViewCellStyle2.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle2;
            this.Precio.HeaderText = "Precio Unitario";
            this.Precio.Name = "Precio";
            // 
            // btnResultados
            // 
            this.btnResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResultados.Location = new System.Drawing.Point(122, 472);
            this.btnResultados.Name = "btnResultados";
            this.btnResultados.Size = new System.Drawing.Size(101, 23);
            this.btnResultados.TabIndex = 8;
            this.btnResultados.UseVisualStyleBackColor = true;
            this.btnResultados.Visible = false;
            this.btnResultados.Click += new System.EventHandler(this.btnResultados_Click);
            // 
            // tbxDetalle
            // 
            this.tbxDetalle.Location = new System.Drawing.Point(289, 209);
            this.tbxDetalle.Multiline = true;
            this.tbxDetalle.Name = "tbxDetalle";
            this.tbxDetalle.Size = new System.Drawing.Size(691, 97);
            this.tbxDetalle.TabIndex = 9;
            this.tbxDetalle.Text = "Datos Seleccionados: ";
            this.tbxDetalle.Visible = false;
            // 
            // txtCotizacion
            // 
            this.txtCotizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCotizacion.Location = new System.Drawing.Point(80, 19);
            this.txtCotizacion.Name = "txtCotizacion";
            this.txtCotizacion.ReadOnly = true;
            this.txtCotizacion.Size = new System.Drawing.Size(213, 20);
            this.txtCotizacion.TabIndex = 10;
            this.txtCotizacion.Visible = false;
            // 
            // txtEntrega
            // 
            this.txtEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrega.Location = new System.Drawing.Point(80, 45);
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.ReadOnly = true;
            this.txtEntrega.Size = new System.Drawing.Size(282, 20);
            this.txtEntrega.TabIndex = 11;
            this.txtEntrega.Visible = false;
            // 
            // lblDatosCotiza
            // 
            this.lblDatosCotiza.AutoSize = true;
            this.lblDatosCotiza.Location = new System.Drawing.Point(9, 16);
            this.lblDatosCotiza.Name = "lblDatosCotiza";
            this.lblDatosCotiza.Size = new System.Drawing.Size(65, 13);
            this.lblDatosCotiza.TabIndex = 12;
            this.lblDatosCotiza.Text = "Cotizacion:  ";
            this.lblDatosCotiza.Visible = false;
            // 
            // lblDatosEntre
            // 
            this.lblDatosEntre.AutoSize = true;
            this.lblDatosEntre.Location = new System.Drawing.Point(10, 45);
            this.lblDatosEntre.Name = "lblDatosEntre";
            this.lblDatosEntre.Size = new System.Drawing.Size(53, 13);
            this.lblDatosEntre.TabIndex = 13;
            this.lblDatosEntre.Text = "Entrega:  ";
            this.lblDatosEntre.Visible = false;
            // 
            // lblDetOrden
            // 
            this.lblDetOrden.AutoSize = true;
            this.lblDetOrden.Location = new System.Drawing.Point(22, 342);
            this.lblDetOrden.Name = "lblDetOrden";
            this.lblDetOrden.Size = new System.Drawing.Size(104, 13);
            this.lblDetOrden.TabIndex = 14;
            this.lblDetOrden.Text = "Detalle de la Orden: ";
            this.lblDetOrden.Visible = false;
            // 
            // lblDetalleEntrega
            // 
            this.lblDetalleEntrega.AutoSize = true;
            this.lblDetalleEntrega.Location = new System.Drawing.Point(19, 427);
            this.lblDetalleEntrega.Name = "lblDetalleEntrega";
            this.lblDetalleEntrega.Size = new System.Drawing.Size(112, 13);
            this.lblDetalleEntrega.TabIndex = 16;
            this.lblDetalleEntrega.Text = "Detalle de la Entrega: ";
            this.lblDetalleEntrega.Visible = false;
            // 
            // txtDetalleEntrega
            // 
            this.txtDetalleEntrega.Location = new System.Drawing.Point(147, 438);
            this.txtDetalleEntrega.Multiline = true;
            this.txtDetalleEntrega.Name = "txtDetalleEntrega";
            this.txtDetalleEntrega.Size = new System.Drawing.Size(424, 28);
            this.txtDetalleEntrega.TabIndex = 15;
            this.txtDetalleEntrega.Visible = false;
            // 
            // mskTFechaGeneracion
            // 
            this.mskTFechaGeneracion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mskTFechaGeneracion.Location = new System.Drawing.Point(824, 35);
            this.mskTFechaGeneracion.Mask = "0000/00/00";
            this.mskTFechaGeneracion.Name = "mskTFechaGeneracion";
            this.mskTFechaGeneracion.Size = new System.Drawing.Size(100, 20);
            this.mskTFechaGeneracion.TabIndex = 4;
            this.mskTFechaGeneracion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskTFechaGeneracion_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(643, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Fecha de Entrega";
            // 
            // txtDocTransp
            // 
            this.txtDocTransp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocTransp.Location = new System.Drawing.Point(107, 106);
            this.txtDocTransp.Multiline = true;
            this.txtDocTransp.Name = "txtDocTransp";
            this.txtDocTransp.ReadOnly = true;
            this.txtDocTransp.Size = new System.Drawing.Size(583, 34);
            this.txtDocTransp.TabIndex = 20;
            this.txtDocTransp.Visible = false;
            // 
            // txtFactura
            // 
            this.txtFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactura.Location = new System.Drawing.Point(80, 76);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.ReadOnly = true;
            this.txtFactura.Size = new System.Drawing.Size(333, 20);
            this.txtFactura.TabIndex = 22;
            this.txtFactura.Visible = false;
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Location = new System.Drawing.Point(10, 76);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(43, 13);
            this.lblFactura.TabIndex = 23;
            this.lblFactura.Text = "Factura";
            this.lblFactura.Visible = false;
            // 
            // txtRecibido
            // 
            this.txtRecibido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecibido.Location = new System.Drawing.Point(107, 155);
            this.txtRecibido.Name = "txtRecibido";
            this.txtRecibido.ReadOnly = true;
            this.txtRecibido.Size = new System.Drawing.Size(583, 20);
            this.txtRecibido.TabIndex = 24;
            this.txtRecibido.Visible = false;
            // 
            // groupBoxDetalle
            // 
            this.groupBoxDetalle.Controls.Add(this.pictureBox2);
            this.groupBoxDetalle.Controls.Add(this.pictureBox1);
            this.groupBoxDetalle.Controls.Add(this.lblDatosCotiza);
            this.groupBoxDetalle.Controls.Add(this.txtRecibido);
            this.groupBoxDetalle.Controls.Add(this.txtCotizacion);
            this.groupBoxDetalle.Controls.Add(this.lblDatosEntre);
            this.groupBoxDetalle.Controls.Add(this.txtDocTransp);
            this.groupBoxDetalle.Controls.Add(this.txtFactura);
            this.groupBoxDetalle.Controls.Add(this.lblFactura);
            this.groupBoxDetalle.Controls.Add(this.txtEntrega);
            this.groupBoxDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxDetalle.Location = new System.Drawing.Point(12, 110);
            this.groupBoxDetalle.Name = "groupBoxDetalle";
            this.groupBoxDetalle.Size = new System.Drawing.Size(714, 186);
            this.groupBoxDetalle.TabIndex = 26;
            this.groupBoxDetalle.TabStop = false;
            this.groupBoxDetalle.Text = "Detalle del Proceso";
            this.groupBoxDetalle.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BP.Properties.Resources.vcm_s_kf_repr_40x40;
            this.pictureBox2.Location = new System.Drawing.Point(58, 144);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 27;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BP.Properties.Resources.vcm_s_kf_repr_64x64;
            this.pictureBox1.Location = new System.Drawing.Point(58, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // frmEstadoPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 498);
            this.Controls.Add(this.groupBoxDetalle);
            this.Controls.Add(this.mskTFechaGeneracion);
            this.Controls.Add(this.lblDetalleEntrega);
            this.Controls.Add(this.txtDetalleEntrega);
            this.Controls.Add(this.lblDetOrden);
            this.Controls.Add(this.tbxDetalle);
            this.Controls.Add(this.btnResultados);
            this.Controls.Add(this.dgvDetalles);
            this.Controls.Add(this.cbxCodCliente);
            this.Controls.Add(this.cbxNomCliente);
            this.Controls.Add(this.cbxRepVentas);
            this.Controls.Add(this.TbxNumPedido);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnNuevaConsulta);
            this.Controls.Add(this.lblRepVentas);
            this.Controls.Add(this.lblNomCliente);
            this.Controls.Add(this.lblCodCliente);
            this.Controls.Add(this.lblNumPedido);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEstadoPedidos";
            this.Text = "Estado de Pedidos";
            this.Load += new System.EventHandler(this.frmEstadoPedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.groupBoxDetalle.ResumeLayout(false);
            this.groupBoxDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumPedido;
        private System.Windows.Forms.Label lblCodCliente;
        private System.Windows.Forms.Label lblRepVentas;
        private System.Windows.Forms.Label lblNomCliente;
        private System.Windows.Forms.Button btnNuevaConsulta;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.MaskedTextBox TbxNumPedido;
        private System.Windows.Forms.ComboBox cbxRepVentas;
        private System.Windows.Forms.ComboBox cbxNomCliente;
        private System.Windows.Forms.ComboBox cbxCodCliente;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.Button btnResultados;
        private System.Windows.Forms.TextBox tbxDetalle;
        private System.Windows.Forms.TextBox txtCotizacion;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label lblDatosCotiza;
        private System.Windows.Forms.Label lblDatosEntre;
        private System.Windows.Forms.Label lblDetOrden;
        private System.Windows.Forms.Label lblDetalleEntrega;
        private System.Windows.Forms.TextBox txtDetalleEntrega;
        private System.Windows.Forms.MaskedTextBox mskTFechaGeneracion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDocTransp;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.TextBox txtRecibido;
        private System.Windows.Forms.GroupBox groupBoxDetalle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridViewButtonColumn Nom_Cliente;
        private System.Windows.Forms.DataGridViewButtonColumn Numero_Cliente;
        private System.Windows.Forms.DataGridViewButtonColumn Numero_Orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha_Orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hora_Orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha_Comprometida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num_Rep;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num_Articulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc_Productos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;

    }
}
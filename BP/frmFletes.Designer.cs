namespace BP
{
    partial class frmFletes
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFletes));
            this.cmbBodega = new System.Windows.Forms.ComboBox();
            this.cmbZonaDestinoInicial = new System.Windows.Forms.ComboBox();
            this.cmbZonaDestinoFinal = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.lblBodega = new System.Windows.Forms.Label();
            this.lblZonaDestinoInicial = new System.Windows.Forms.Label();
            this.lblZonaDestinoFinal = new System.Windows.Forms.Label();
            this.lblTransportadora = new System.Windows.Forms.Label();
            this.lblConductor = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.cmdTransportadora = new System.Windows.Forms.ComboBox();
            this.cmbConductor = new System.Windows.Forms.ComboBox();
            this.cmbPlaca = new System.Windows.Forms.ComboBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.txtMacroguia = new System.Windows.Forms.TextBox();
            this.lblMacroguia = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.Zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DocEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTotales = new System.Windows.Forms.DataGridView();
            this.Titulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTipoVehiculo = new System.Windows.Forms.Label();
            this.cmbTipoVehiculo = new System.Windows.Forms.ComboBox();
            this.txt_Comentario = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotales)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBodega
            // 
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(132, 39);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(121, 21);
            this.cmbBodega.TabIndex = 1;
            this.cmbBodega.TextChanged += new System.EventHandler(this.cmbBodega_TextChanged);
            // 
            // cmbZonaDestinoInicial
            // 
            this.cmbZonaDestinoInicial.Enabled = false;
            this.cmbZonaDestinoInicial.FormattingEnabled = true;
            this.cmbZonaDestinoInicial.Location = new System.Drawing.Point(132, 66);
            this.cmbZonaDestinoInicial.Name = "cmbZonaDestinoInicial";
            this.cmbZonaDestinoInicial.Size = new System.Drawing.Size(121, 21);
            this.cmbZonaDestinoInicial.TabIndex = 2;
            this.cmbZonaDestinoInicial.TextChanged += new System.EventHandler(this.cmbZonaDestinoInicial_TextChanged);
            // 
            // cmbZonaDestinoFinal
            // 
            this.cmbZonaDestinoFinal.Enabled = false;
            this.cmbZonaDestinoFinal.FormattingEnabled = true;
            this.cmbZonaDestinoFinal.Location = new System.Drawing.Point(132, 93);
            this.cmbZonaDestinoFinal.Name = "cmbZonaDestinoFinal";
            this.cmbZonaDestinoFinal.Size = new System.Drawing.Size(121, 21);
            this.cmbZonaDestinoFinal.TabIndex = 3;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(28, 13);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(92, 13);
            this.lblFecha.TabIndex = 4;
            this.lblFecha.Text = "Fecha de Entrega";
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(132, 13);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(121, 20);
            this.dtpFechaEntrega.TabIndex = 5;
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Location = new System.Drawing.Point(28, 42);
            this.lblBodega.Name = "lblBodega";
            this.lblBodega.Size = new System.Drawing.Size(78, 13);
            this.lblBodega.TabIndex = 6;
            this.lblBodega.Text = "Bodega Origen";
            // 
            // lblZonaDestinoInicial
            // 
            this.lblZonaDestinoInicial.AutoSize = true;
            this.lblZonaDestinoInicial.Location = new System.Drawing.Point(28, 69);
            this.lblZonaDestinoInicial.Name = "lblZonaDestinoInicial";
            this.lblZonaDestinoInicial.Size = new System.Drawing.Size(101, 13);
            this.lblZonaDestinoInicial.TabIndex = 7;
            this.lblZonaDestinoInicial.Text = "Zona Destino Inicial";
            // 
            // lblZonaDestinoFinal
            // 
            this.lblZonaDestinoFinal.AutoSize = true;
            this.lblZonaDestinoFinal.Location = new System.Drawing.Point(28, 96);
            this.lblZonaDestinoFinal.Name = "lblZonaDestinoFinal";
            this.lblZonaDestinoFinal.Size = new System.Drawing.Size(96, 13);
            this.lblZonaDestinoFinal.TabIndex = 8;
            this.lblZonaDestinoFinal.Text = "Zona Destino Final";
            // 
            // lblTransportadora
            // 
            this.lblTransportadora.AutoSize = true;
            this.lblTransportadora.Location = new System.Drawing.Point(291, 15);
            this.lblTransportadora.Name = "lblTransportadora";
            this.lblTransportadora.Size = new System.Drawing.Size(79, 13);
            this.lblTransportadora.TabIndex = 9;
            this.lblTransportadora.Text = "Transportadora";
            // 
            // lblConductor
            // 
            this.lblConductor.AutoSize = true;
            this.lblConductor.Location = new System.Drawing.Point(291, 42);
            this.lblConductor.Name = "lblConductor";
            this.lblConductor.Size = new System.Drawing.Size(56, 13);
            this.lblConductor.TabIndex = 10;
            this.lblConductor.Text = "Conductor";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Location = new System.Drawing.Point(291, 68);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(34, 13);
            this.lblPlaca.TabIndex = 11;
            this.lblPlaca.Text = "Placa";
            // 
            // cmdTransportadora
            // 
            this.cmdTransportadora.Enabled = false;
            this.cmdTransportadora.FormattingEnabled = true;
            this.cmdTransportadora.Location = new System.Drawing.Point(395, 12);
            this.cmdTransportadora.Name = "cmdTransportadora";
            this.cmdTransportadora.Size = new System.Drawing.Size(121, 21);
            this.cmdTransportadora.TabIndex = 12;
            // 
            // cmbConductor
            // 
            this.cmbConductor.Enabled = false;
            this.cmbConductor.FormattingEnabled = true;
            this.cmbConductor.Location = new System.Drawing.Point(395, 39);
            this.cmbConductor.Name = "cmbConductor";
            this.cmbConductor.Size = new System.Drawing.Size(121, 21);
            this.cmbConductor.TabIndex = 13;
            // 
            // cmbPlaca
            // 
            this.cmbPlaca.Enabled = false;
            this.cmbPlaca.FormattingEnabled = true;
            this.cmbPlaca.Location = new System.Drawing.Point(395, 65);
            this.cmbPlaca.Name = "cmbPlaca";
            this.cmbPlaca.Size = new System.Drawing.Size(121, 21);
            this.cmbPlaca.TabIndex = 14;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGrabar.Location = new System.Drawing.Point(99, 430);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 16;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImprimir.Location = new System.Drawing.Point(181, 430);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 17;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAnular.Location = new System.Drawing.Point(263, 430);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(75, 23);
            this.btnAnular.TabIndex = 18;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // txtMacroguia
            // 
            this.txtMacroguia.Location = new System.Drawing.Point(395, 93);
            this.txtMacroguia.Name = "txtMacroguia";
            this.txtMacroguia.Size = new System.Drawing.Size(121, 20);
            this.txtMacroguia.TabIndex = 19;
            this.toolTip1.SetToolTip(this.txtMacroguia, "Digite el número de Macroguía");
            this.txtMacroguia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMacroguia_KeyPress);
            // 
            // lblMacroguia
            // 
            this.lblMacroguia.AutoSize = true;
            this.lblMacroguia.Location = new System.Drawing.Point(291, 96);
            this.lblMacroguia.Name = "lblMacroguia";
            this.lblMacroguia.Size = new System.Drawing.Size(78, 13);
            this.lblMacroguia.TabIndex = 20;
            this.lblMacroguia.Text = "No. Transporte";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuscar.Location = new System.Drawing.Point(18, 430);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 21;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Zona,
            this.Tipo,
            this.Cliente,
            this.Rep,
            this.Orden,
            this.Serie,
            this.Kilos,
            this.Valor,
            this.Sel,
            this.DocEntry});
            this.dgvResultados.Location = new System.Drawing.Point(13, 150);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowHeadersVisible = false;
            this.dgvResultados.Size = new System.Drawing.Size(681, 241);
            this.dgvResultados.TabIndex = 22;
            this.dgvResultados.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellValueChanged);
            this.dgvResultados.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvResultados_CurrentCellDirtyStateChanged);
            // 
            // Zona
            // 
            this.Zona.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Zona.FillWeight = 95.70858F;
            this.Zona.HeaderText = "Zona Destino";
            this.Zona.Name = "Zona";
            this.Zona.ReadOnly = true;
            // 
            // Tipo
            // 
            this.Tipo.FillWeight = 30F;
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 30;
            // 
            // Cliente
            // 
            this.Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cliente.FillWeight = 95.70858F;
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            // 
            // Rep
            // 
            this.Rep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rep.FillWeight = 95.70858F;
            this.Rep.HeaderText = "Rep. Ventas";
            this.Rep.Name = "Rep";
            this.Rep.ReadOnly = true;
            // 
            // Orden
            // 
            this.Orden.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Orden.FillWeight = 95.70858F;
            this.Orden.HeaderText = "No. Orden";
            this.Orden.Name = "Orden";
            this.Orden.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.FillWeight = 45F;
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            this.Serie.Width = 45;
            // 
            // Kilos
            // 
            this.Kilos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Kilos.FillWeight = 95.70858F;
            this.Kilos.HeaderText = "Can.Kilos";
            this.Kilos.Name = "Kilos";
            this.Kilos.ReadOnly = true;
            // 
            // Valor
            // 
            this.Valor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Valor.FillWeight = 95.70858F;
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            // 
            // Sel
            // 
            this.Sel.FillWeight = 30F;
            this.Sel.HeaderText = "Sel.";
            this.Sel.Name = "Sel";
            this.Sel.Width = 30;
            // 
            // DocEntry
            // 
            this.DocEntry.HeaderText = "DocEntry";
            this.DocEntry.Name = "DocEntry";
            this.DocEntry.Visible = false;
            // 
            // dgvTotales
            // 
            this.dgvTotales.AllowUserToAddRows = false;
            this.dgvTotales.AllowUserToDeleteRows = false;
            this.dgvTotales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotales.ColumnHeadersVisible = false;
            this.dgvTotales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Titulo,
            this.ColValor});
            this.dgvTotales.Location = new System.Drawing.Point(492, 397);
            this.dgvTotales.Name = "dgvTotales";
            this.dgvTotales.ReadOnly = true;
            this.dgvTotales.RowHeadersVisible = false;
            this.dgvTotales.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvTotales.ShowCellErrors = false;
            this.dgvTotales.ShowCellToolTips = false;
            this.dgvTotales.ShowEditingIcon = false;
            this.dgvTotales.Size = new System.Drawing.Size(202, 66);
            this.dgvTotales.TabIndex = 23;
            // 
            // Titulo
            // 
            this.Titulo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Titulo.HeaderText = "Titulo";
            this.Titulo.Name = "Titulo";
            this.Titulo.ReadOnly = true;
            // 
            // ColValor
            // 
            this.ColValor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColValor.HeaderText = "Valor";
            this.ColValor.Name = "ColValor";
            this.ColValor.ReadOnly = true;
            // 
            // lblTipoVehiculo
            // 
            this.lblTipoVehiculo.AutoSize = true;
            this.lblTipoVehiculo.Location = new System.Drawing.Point(28, 124);
            this.lblTipoVehiculo.Name = "lblTipoVehiculo";
            this.lblTipoVehiculo.Size = new System.Drawing.Size(74, 13);
            this.lblTipoVehiculo.TabIndex = 24;
            this.lblTipoVehiculo.Text = "Tipo Vehículo";
            // 
            // cmbTipoVehiculo
            // 
            this.cmbTipoVehiculo.FormattingEnabled = true;
            this.cmbTipoVehiculo.Location = new System.Drawing.Point(132, 121);
            this.cmbTipoVehiculo.Name = "cmbTipoVehiculo";
            this.cmbTipoVehiculo.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoVehiculo.TabIndex = 25;
            // 
            // txt_Comentario
            // 
            this.txt_Comentario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_Comentario.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_Comentario.Location = new System.Drawing.Point(13, 398);
            this.txt_Comentario.Multiline = true;
            this.txt_Comentario.Name = "txt_Comentario";
            this.txt_Comentario.Size = new System.Drawing.Size(325, 26);
            this.txt_Comentario.TabIndex = 28;
            this.txt_Comentario.Text = "Digite las Observaciones aqui..";
            this.txt_Comentario.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_Comentario_MouseClick);
            this.txt_Comentario.TextChanged += new System.EventHandler(this.txt_Comentario_TextChanged);
            // 
            // frmFletes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 465);
            this.Controls.Add(this.txt_Comentario);
            this.Controls.Add(this.cmbTipoVehiculo);
            this.Controls.Add(this.lblTipoVehiculo);
            this.Controls.Add(this.dgvTotales);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblMacroguia);
            this.Controls.Add(this.txtMacroguia);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.cmbPlaca);
            this.Controls.Add(this.cmbConductor);
            this.Controls.Add(this.cmdTransportadora);
            this.Controls.Add(this.lblPlaca);
            this.Controls.Add(this.lblConductor);
            this.Controls.Add(this.lblTransportadora);
            this.Controls.Add(this.lblZonaDestinoFinal);
            this.Controls.Add(this.lblZonaDestinoInicial);
            this.Controls.Add(this.lblBodega);
            this.Controls.Add(this.dtpFechaEntrega);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.cmbZonaDestinoFinal);
            this.Controls.Add(this.cmbZonaDestinoInicial);
            this.Controls.Add(this.cmbBodega);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFletes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Valor Flete";
            this.Load += new System.EventHandler(this.frmFletes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBodega;
        private System.Windows.Forms.ComboBox cmbZonaDestinoInicial;
        private System.Windows.Forms.ComboBox cmbZonaDestinoFinal;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.Label lblBodega;
        private System.Windows.Forms.Label lblZonaDestinoInicial;
        private System.Windows.Forms.Label lblZonaDestinoFinal;
        private System.Windows.Forms.Label lblTransportadora;
        private System.Windows.Forms.Label lblConductor;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.ComboBox cmdTransportadora;
        private System.Windows.Forms.ComboBox cmbConductor;
        private System.Windows.Forms.ComboBox cmbPlaca;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.TextBox txtMacroguia;
        private System.Windows.Forms.Label lblMacroguia;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zona;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rep;
        private System.Windows.Forms.DataGridViewTextBoxColumn Orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocEntry;
        private System.Windows.Forms.DataGridView dgvTotales;
        private System.Windows.Forms.DataGridViewTextBoxColumn Titulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColValor;
        private System.Windows.Forms.Label lblTipoVehiculo;
        private System.Windows.Forms.ComboBox cmbTipoVehiculo;
        private System.Windows.Forms.TextBox txt_Comentario;
    }
}
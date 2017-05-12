namespace BP
{
    partial class frmContratosConsignacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnCodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNombreP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnElimina = new System.Windows.Forms.Button();
            this.btnActualiza = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.txtNumContrato = new System.Windows.Forms.TextBox();
            this.cbxEmpleado = new System.Windows.Forms.ComboBox();
            this.cbxBodega = new System.Windows.Forms.ComboBox();
            this.cbxNomCliente = new System.Windows.Forms.ComboBox();
            this.cbxCodCliente = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpVigenciaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpVigenciaDesde = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxTipoTercero = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnArchivo = new System.Windows.Forms.Button();
            this.linkArchivo = new System.Windows.Forms.LinkLabel();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtComentarios);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(8, 195);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 247);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles";
            // 
            // txtComentarios
            // 
            this.txtComentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComentarios.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txtComentarios.Location = new System.Drawing.Point(7, 182);
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(566, 59);
            this.txtComentarios.TabIndex = 10;
            this.txtComentarios.Text = "COMENTARIOS";
            this.txtComentarios.Click += new System.EventHandler(this.txtComentarios_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(7, 74);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(200, 102);
            this.dataGridView2.TabIndex = 9;
            this.dataGridView2.Visible = false;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCodProducto,
            this.ColumnNombreP,
            this.ColumnKilos,
            this.ColumnPrecio});
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(545, 98);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // ColumnCodProducto
            // 
            this.ColumnCodProducto.HeaderText = "Código de producto ";
            this.ColumnCodProducto.Name = "ColumnCodProducto";
            this.ColumnCodProducto.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnNombreP
            // 
            this.ColumnNombreP.HeaderText = "Nombre ";
            this.ColumnNombreP.Name = "ColumnNombreP";
            this.ColumnNombreP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnNombreP.Width = 200;
            // 
            // ColumnKilos
            // 
            dataGridViewCellStyle1.Format = "N2";
            this.ColumnKilos.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnKilos.HeaderText = "Kilos";
            this.ColumnKilos.Name = "ColumnKilos";
            // 
            // ColumnPrecio
            // 
            dataGridViewCellStyle2.Format = "C2";
            this.ColumnPrecio.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnPrecio.HeaderText = "Precio";
            this.ColumnPrecio.Name = "ColumnPrecio";
            // 
            // btnElimina
            // 
            this.btnElimina.Enabled = false;
            this.btnElimina.Location = new System.Drawing.Point(255, 479);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(75, 31);
            this.btnElimina.TabIndex = 12;
            this.btnElimina.Text = "Eliminar";
            this.btnElimina.UseVisualStyleBackColor = true;
            this.btnElimina.Click += new System.EventHandler(this.btnElimina_Click);
            // 
            // btnActualiza
            // 
            this.btnActualiza.Enabled = false;
            this.btnActualiza.Location = new System.Drawing.Point(174, 479);
            this.btnActualiza.Name = "btnActualiza";
            this.btnActualiza.Size = new System.Drawing.Size(75, 31);
            this.btnActualiza.TabIndex = 11;
            this.btnActualiza.Text = "Actualizar";
            this.btnActualiza.UseVisualStyleBackColor = true;
            this.btnActualiza.Click += new System.EventHandler(this.btnActualiza_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(91, 479);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 31);
            this.btnBuscar.TabIndex = 10;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maskedTextBox2);
            this.groupBox1.Controls.Add(this.maskedTextBox1);
            this.groupBox1.Controls.Add(this.txtNumContrato);
            this.groupBox1.Controls.Add(this.cbxEmpleado);
            this.groupBox1.Controls.Add(this.cbxBodega);
            this.groupBox1.Controls.Add(this.cbxNomCliente);
            this.groupBox1.Controls.Add(this.cbxCodCliente);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxTipoTercero);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 184);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Contrato";
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(414, 41);
            this.maskedTextBox2.Mask = "99";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(20, 20);
            this.maskedTextBox2.TabIndex = 29;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(364, 42);
            this.maskedTextBox1.Mask = "99";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(24, 20);
            this.maskedTextBox1.TabIndex = 28;
            // 
            // txtNumContrato
            // 
            this.txtNumContrato.Location = new System.Drawing.Point(364, 14);
            this.txtNumContrato.Name = "txtNumContrato";
            this.txtNumContrato.Size = new System.Drawing.Size(70, 20);
            this.txtNumContrato.TabIndex = 27;
            this.txtNumContrato.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumContrato_KeyPress);
            // 
            // cbxEmpleado
            // 
            this.cbxEmpleado.FormattingEnabled = true;
            this.cbxEmpleado.Location = new System.Drawing.Point(349, 91);
            this.cbxEmpleado.Name = "cbxEmpleado";
            this.cbxEmpleado.Size = new System.Drawing.Size(226, 21);
            this.cbxEmpleado.TabIndex = 5;
            this.cbxEmpleado.DropDown += new System.EventHandler(this.cbxEmpleado_DropDown);
            // 
            // cbxBodega
            // 
            this.cbxBodega.FormattingEnabled = true;
            this.cbxBodega.Location = new System.Drawing.Point(349, 66);
            this.cbxBodega.Name = "cbxBodega";
            this.cbxBodega.Size = new System.Drawing.Size(226, 21);
            this.cbxBodega.TabIndex = 4;
            this.cbxBodega.SelectedIndexChanged += new System.EventHandler(this.cbxBodega_SelectedIndexChanged);
            this.cbxBodega.DropDown += new System.EventHandler(this.cbxBodega_DropDown);
            // 
            // cbxNomCliente
            // 
            this.cbxNomCliente.FormattingEnabled = true;
            this.cbxNomCliente.Location = new System.Drawing.Point(108, 61);
            this.cbxNomCliente.Name = "cbxNomCliente";
            this.cbxNomCliente.Size = new System.Drawing.Size(139, 21);
            this.cbxNomCliente.TabIndex = 2;
            this.cbxNomCliente.DropDown += new System.EventHandler(this.cbxNomCliente_DropDown);
            // 
            // cbxCodCliente
            // 
            this.cbxCodCliente.FormattingEnabled = true;
            this.cbxCodCliente.Location = new System.Drawing.Point(108, 41);
            this.cbxCodCliente.Name = "cbxCodCliente";
            this.cbxCodCliente.Size = new System.Drawing.Size(139, 21);
            this.cbxCodCliente.TabIndex = 1;
            this.cbxCodCliente.DropDown += new System.EventHandler(this.cbxCodCliente_DropDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.dtpVigenciaHasta);
            this.groupBox3.Controls.Add(this.dtpVigenciaDesde);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(63, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(434, 57);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vigencia";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Desde";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(213, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Hasta";
            // 
            // dtpVigenciaHasta
            // 
            this.dtpVigenciaHasta.CustomFormat = "yyyyMMdd";
            this.dtpVigenciaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVigenciaHasta.Location = new System.Drawing.Point(216, 35);
            this.dtpVigenciaHasta.Name = "dtpVigenciaHasta";
            this.dtpVigenciaHasta.Size = new System.Drawing.Size(109, 20);
            this.dtpVigenciaHasta.TabIndex = 7;
            // 
            // dtpVigenciaDesde
            // 
            this.dtpVigenciaDesde.CustomFormat = "yyyyMMdd";
            this.dtpVigenciaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVigenciaDesde.Location = new System.Drawing.Point(6, 31);
            this.dtpVigenciaDesde.Name = "dtpVigenciaDesde";
            this.dtpVigenciaDesde.Size = new System.Drawing.Size(110, 20);
            this.dtpVigenciaDesde.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(177, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(157, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Empleado de Ventas / Compras";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(253, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Dias de Corte";
            // 
            // cbxTipoTercero
            // 
            this.cbxTipoTercero.FormattingEnabled = true;
            this.cbxTipoTercero.Items.AddRange(new object[] {
            "Cliente",
            "Proveedor"});
            this.cbxTipoTercero.Location = new System.Drawing.Point(108, 22);
            this.cbxTipoTercero.Name = "cbxTipoTercero";
            this.cbxTipoTercero.Size = new System.Drawing.Size(121, 21);
            this.cbxTipoTercero.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Bodega Asociada";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo de Tercero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre de Tercero";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Codigo Tercero";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Numero de Contrato";
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(10, 479);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(75, 31);
            this.btnCrear.TabIndex = 9;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Contrato";
            this.openFileDialog1.Filter = "Word|*.doc*|Acrobat|*.pdf";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnArchivo
            // 
            this.btnArchivo.Location = new System.Drawing.Point(476, 445);
            this.btnArchivo.Name = "btnArchivo";
            this.btnArchivo.Size = new System.Drawing.Size(55, 23);
            this.btnArchivo.TabIndex = 19;
            this.btnArchivo.Text = "Archivo";
            this.btnArchivo.UseVisualStyleBackColor = true;
            this.btnArchivo.Click += new System.EventHandler(this.btnArchivo_Click);
            // 
            // linkArchivo
            // 
            this.linkArchivo.AutoSize = true;
            this.linkArchivo.Location = new System.Drawing.Point(12, 445);
            this.linkArchivo.Name = "linkArchivo";
            this.linkArchivo.Size = new System.Drawing.Size(10, 13);
            this.linkArchivo.TabIndex = 20;
            this.linkArchivo.TabStop = true;
            this.linkArchivo.Text = ".";
            this.linkArchivo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkArchivo_LinkClicked);
            // 
            // frmContratosConsignacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 522);
            this.Controls.Add(this.linkArchivo);
            this.Controls.Add(this.btnArchivo);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnElimina);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnActualiza);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBuscar);
            this.Name = "frmContratosConsignacion";
            this.Text = "Contratos Consignacion";
            this.Load += new System.EventHandler(this.frmContratosConsignacion_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnElimina;
        private System.Windows.Forms.Button btnActualiza;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxBodega;
        private System.Windows.Forms.ComboBox cbxNomCliente;
        private System.Windows.Forms.ComboBox cbxCodCliente;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpVigenciaHasta;
        private System.Windows.Forms.DateTimePicker dtpVigenciaDesde;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxTipoTercero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.ComboBox cbxEmpleado;
        private System.Windows.Forms.TextBox txtNumContrato;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnArchivo;
        private System.Windows.Forms.LinkLabel linkArchivo;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCodProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNombreP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrecio;
    }
}
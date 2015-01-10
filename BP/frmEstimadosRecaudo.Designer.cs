namespace BP
{
    partial class frmEstimadosRecaudo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstimadosRecaudo));
            this.label1 = new System.Windows.Forms.Label();
            this.txtRepVentas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxRepVentas = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tbxfechCambios = new System.Windows.Forms.TextBox();
            this.cbxClienteInicial = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalZona = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCumplimiento = new System.Windows.Forms.TextBox();
            this.txtEstimadoXCliente = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtTotalRecIn = new System.Windows.Forms.TextBox();
            this.txtDeudaXCliente = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verCalculadoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.chek = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Representante de Ventas";
            // 
            // txtRepVentas
            // 
            this.txtRepVentas.Location = new System.Drawing.Point(149, 9);
            this.txtRepVentas.Name = "txtRepVentas";
            this.txtRepVentas.ReadOnly = true;
            this.txtRepVentas.Size = new System.Drawing.Size(247, 20);
            this.txtRepVentas.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cliente Inicial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha de Vencimiento";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxRepVentas);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnIniciar);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.tbxfechCambios);
            this.groupBox1.Controls.Add(this.cbxClienteInicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRepVentas);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 127);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "*";
            // 
            // cbxRepVentas
            // 
            this.cbxRepVentas.FormattingEnabled = true;
            this.cbxRepVentas.Location = new System.Drawing.Point(149, 8);
            this.cbxRepVentas.Name = "cbxRepVentas";
            this.cbxRepVentas.Size = new System.Drawing.Size(242, 21);
            this.cbxRepVentas.TabIndex = 22;
            this.cbxRepVentas.Visible = false;
            this.cbxRepVentas.SelectedIndexChanged += new System.EventHandler(this.cbxRepVentas_SelectedIndexChanged);
            this.cbxRepVentas.DropDown += new System.EventHandler(this.cbxRepVentas_DropDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(403, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(154, 17);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "Lineas de Negocio Multiple";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cambios permitidos hasta el Proximo:";
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(500, 78);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(33, 23);
            this.btnIniciar.TabIndex = 23;
            this.btnIniciar.Text = ">>>";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(149, 44);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(98, 20);
            this.dateTimePicker1.TabIndex = 10;
    
            // 
            // tbxfechCambios
            // 
            this.tbxfechCambios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxfechCambios.ForeColor = System.Drawing.Color.Red;
            this.tbxfechCambios.Location = new System.Drawing.Point(211, 76);
            this.tbxfechCambios.Name = "tbxfechCambios";
            this.tbxfechCambios.ReadOnly = true;
            this.tbxfechCambios.Size = new System.Drawing.Size(89, 20);
            this.tbxfechCambios.TabIndex = 8;
            // 
            // cbxClienteInicial
            // 
            this.cbxClienteInicial.FormattingEnabled = true;
            this.cbxClienteInicial.Location = new System.Drawing.Point(331, 43);
            this.cbxClienteInicial.Name = "cbxClienteInicial";
            this.cbxClienteInicial.Size = new System.Drawing.Size(174, 21);
            this.cbxClienteInicial.TabIndex = 5;
            this.cbxClienteInicial.SelectedIndexChanged += new System.EventHandler(this.cbxClienteInicial_SelectedIndexChanged);
            this.cbxClienteInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxClienteInicial_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtTotalZona);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCumplimiento);
            this.groupBox2.Controls.Add(this.txtEstimadoXCliente);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.txtTotalRecIn);
            this.groupBox2.Controls.Add(this.txtDeudaXCliente);
            this.groupBox2.Location = new System.Drawing.Point(12, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(659, 337);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Documentos  Abiertos";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 19);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(634, 207);
            this.dataGridView2.TabIndex = 22;
            this.dataGridView2.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 307);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Total Estimado X Ingeniero";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Presupuesto Tecnico ";
            // 
            // txtTotalZona
            // 
            this.txtTotalZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalZona.Location = new System.Drawing.Point(177, 246);
            this.txtTotalZona.Name = "txtTotalZona";
            this.txtTotalZona.ReadOnly = true;
            this.txtTotalZona.Size = new System.Drawing.Size(123, 20);
            this.txtTotalZona.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(60, 275);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "% Vs Tecnico";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Total Estimado X Cliente";
            this.label4.Visible = false;
            // 
            // txtCumplimiento
            // 
            this.txtCumplimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtCumplimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCumplimiento.Location = new System.Drawing.Point(177, 272);
            this.txtCumplimiento.Name = "txtCumplimiento";
            this.txtCumplimiento.ReadOnly = true;
            this.txtCumplimiento.Size = new System.Drawing.Size(100, 20);
            this.txtCumplimiento.TabIndex = 20;
            this.txtCumplimiento.Text = "%";
            // 
            // txtEstimadoXCliente
            // 
            this.txtEstimadoXCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstimadoXCliente.Location = new System.Drawing.Point(451, 282);
            this.txtEstimadoXCliente.Name = "txtEstimadoXCliente";
            this.txtEstimadoXCliente.ReadOnly = true;
            this.txtEstimadoXCliente.Size = new System.Drawing.Size(124, 20);
            this.txtEstimadoXCliente.TabIndex = 14;
            this.txtEstimadoXCliente.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Total deuda X Cliente";
            this.label7.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(634, 207);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // txtTotalRecIn
            // 
            this.txtTotalRecIn.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalRecIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRecIn.Location = new System.Drawing.Point(177, 300);
            this.txtTotalRecIn.Name = "txtTotalRecIn";
            this.txtTotalRecIn.ReadOnly = true;
            this.txtTotalRecIn.Size = new System.Drawing.Size(123, 20);
            this.txtTotalRecIn.TabIndex = 7;
            // 
            // txtDeudaXCliente
            // 
            this.txtDeudaXCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaXCliente.Location = new System.Drawing.Point(452, 246);
            this.txtDeudaXCliente.Name = "txtDeudaXCliente";
            this.txtDeudaXCliente.ReadOnly = true;
            this.txtDeudaXCliente.Size = new System.Drawing.Size(123, 20);
            this.txtDeudaXCliente.TabIndex = 16;
            this.txtDeudaXCliente.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verCalculadoraToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 26);
            // 
            // verCalculadoraToolStripMenuItem
            // 
            this.verCalculadoraToolStripMenuItem.Name = "verCalculadoraToolStripMenuItem";
            this.verCalculadoraToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.verCalculadoraToolStripMenuItem.Text = "Ver Calculadora";
            this.verCalculadoraToolStripMenuItem.Click += new System.EventHandler(this.verCalculadoraToolStripMenuItem_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Enabled = false;
            this.btnActualizar.Location = new System.Drawing.Point(12, 479);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(95, 29);
            this.btnActualizar.TabIndex = 9;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // chek
            // 
            this.chek.FalseValue = 0;
            this.chek.Name = "chek";
            this.chek.TrueValue = 1;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(113, 479);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(95, 29);
            this.btnLimpiar.TabIndex = 22;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // frmEstimadosRecaudo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 529);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEstimadosRecaudo";
            this.Text = "Estimado de Recaudo";
            this.Load += new System.EventHandler(this.frmEstimadosRecaudo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmEstimadosRecaudo_KeyPress);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEstimadosRecaudo_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRepVentas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxClienteInicial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxfechCambios;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtTotalRecIn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEstimadoXCliente;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDeudaXCliente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCumplimiento;
        private System.Windows.Forms.ToolStripMenuItem verCalculadoraToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chek;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalZona;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxRepVentas;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}
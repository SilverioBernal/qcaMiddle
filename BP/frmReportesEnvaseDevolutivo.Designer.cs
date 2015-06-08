namespace BP
{
    partial class frmReportesEnvaseDevolutivo
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdCarteraClientes = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportaRepCartera = new System.Windows.Forms.Button();
            this.btnGeneraRepCartera = new System.Windows.Forms.Button();
            this.filtraProveedor = new System.Windows.Forms.CheckBox();
            this.cboProveedor = new System.Windows.Forms.ComboBox();
            this.filtraFechaRecibo = new System.Windows.Forms.CheckBox();
            this.dpReciboHasta = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dpReciboDesde = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.filtraFechaRemision = new System.Windows.Forms.CheckBox();
            this.dpRemisionHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dpRemisionDesde = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExportarKC = new System.Windows.Forms.Button();
            this.btnGenerarKC = new System.Windows.Forms.Button();
            this.dpKCHasta = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dpKCDesde = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.grdKardexClientes = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnExportarKP = new System.Windows.Forms.Button();
            this.btnGenerarKP = new System.Windows.Forms.Button();
            this.dpKPHasta = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dpKPDesde = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.grdKardexProveedores = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCarteraClientes)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKardexClientes)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKardexProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(927, 516);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(919, 490);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Reporte de cartera";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdCarteraClientes);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(913, 398);
            this.panel2.TabIndex = 1;
            // 
            // grdCarteraClientes
            // 
            this.grdCarteraClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCarteraClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCarteraClientes.Location = new System.Drawing.Point(0, 0);
            this.grdCarteraClientes.Name = "grdCarteraClientes";
            this.grdCarteraClientes.Size = new System.Drawing.Size(913, 398);
            this.grdCarteraClientes.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportaRepCartera);
            this.panel1.Controls.Add(this.btnGeneraRepCartera);
            this.panel1.Controls.Add(this.filtraProveedor);
            this.panel1.Controls.Add(this.cboProveedor);
            this.panel1.Controls.Add(this.filtraFechaRecibo);
            this.panel1.Controls.Add(this.dpReciboHasta);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dpReciboDesde);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.filtraFechaRemision);
            this.panel1.Controls.Add(this.dpRemisionHasta);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dpRemisionDesde);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 86);
            this.panel1.TabIndex = 0;
            // 
            // btnExportaRepCartera
            // 
            this.btnExportaRepCartera.Location = new System.Drawing.Point(726, 32);
            this.btnExportaRepCartera.Name = "btnExportaRepCartera";
            this.btnExportaRepCartera.Size = new System.Drawing.Size(75, 23);
            this.btnExportaRepCartera.TabIndex = 44;
            this.btnExportaRepCartera.Text = "Exportar";
            this.btnExportaRepCartera.UseVisualStyleBackColor = true;
            this.btnExportaRepCartera.Click += new System.EventHandler(this.btnExportaRepCartera_Click);
            // 
            // btnGeneraRepCartera
            // 
            this.btnGeneraRepCartera.Location = new System.Drawing.Point(645, 32);
            this.btnGeneraRepCartera.Name = "btnGeneraRepCartera";
            this.btnGeneraRepCartera.Size = new System.Drawing.Size(75, 23);
            this.btnGeneraRepCartera.TabIndex = 43;
            this.btnGeneraRepCartera.Text = "Generar";
            this.btnGeneraRepCartera.UseVisualStyleBackColor = true;
            this.btnGeneraRepCartera.Click += new System.EventHandler(this.btnGeneraRepCartera_Click);
            // 
            // filtraProveedor
            // 
            this.filtraProveedor.AutoSize = true;
            this.filtraProveedor.Location = new System.Drawing.Point(5, 54);
            this.filtraProveedor.Name = "filtraProveedor";
            this.filtraProveedor.Size = new System.Drawing.Size(120, 17);
            this.filtraProveedor.TabIndex = 42;
            this.filtraProveedor.Text = "Filtrar por proveedor";
            this.filtraProveedor.UseVisualStyleBackColor = true;
            this.filtraProveedor.CheckedChanged += new System.EventHandler(this.filtraProveedor_CheckedChanged);
            // 
            // cboProveedor
            // 
            this.cboProveedor.Enabled = false;
            this.cboProveedor.FormattingEnabled = true;
            this.cboProveedor.Location = new System.Drawing.Point(169, 54);
            this.cboProveedor.Name = "cboProveedor";
            this.cboProveedor.Size = new System.Drawing.Size(250, 21);
            this.cboProveedor.TabIndex = 41;
            // 
            // filtraFechaRecibo
            // 
            this.filtraFechaRecibo.AutoSize = true;
            this.filtraFechaRecibo.Location = new System.Drawing.Point(5, 31);
            this.filtraFechaRecibo.Name = "filtraFechaRecibo";
            this.filtraFechaRecibo.Size = new System.Drawing.Size(146, 17);
            this.filtraFechaRecibo.TabIndex = 17;
            this.filtraFechaRecibo.Text = "Filtrar por fecha de recibo";
            this.filtraFechaRecibo.UseVisualStyleBackColor = true;
            this.filtraFechaRecibo.CheckedChanged += new System.EventHandler(this.filtraFechaRecibo_CheckedChanged);
            // 
            // dpReciboHasta
            // 
            this.dpReciboHasta.Enabled = false;
            this.dpReciboHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpReciboHasta.Location = new System.Drawing.Point(338, 28);
            this.dpReciboHasta.Name = "dpReciboHasta";
            this.dpReciboHasta.Size = new System.Drawing.Size(81, 20);
            this.dpReciboHasta.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Hasta";
            // 
            // dpReciboDesde
            // 
            this.dpReciboDesde.Enabled = false;
            this.dpReciboDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpReciboDesde.Location = new System.Drawing.Point(210, 28);
            this.dpReciboDesde.Name = "dpReciboDesde";
            this.dpReciboDesde.Size = new System.Drawing.Size(81, 20);
            this.dpReciboDesde.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Desde";
            // 
            // filtraFechaRemision
            // 
            this.filtraFechaRemision.AutoSize = true;
            this.filtraFechaRemision.Location = new System.Drawing.Point(5, 8);
            this.filtraFechaRemision.Name = "filtraFechaRemision";
            this.filtraFechaRemision.Size = new System.Drawing.Size(155, 17);
            this.filtraFechaRemision.TabIndex = 12;
            this.filtraFechaRemision.Text = "Filtrar por fecha de remisión";
            this.filtraFechaRemision.UseVisualStyleBackColor = true;
            this.filtraFechaRemision.CheckedChanged += new System.EventHandler(this.filtraFechaRemision_CheckedChanged);
            // 
            // dpRemisionHasta
            // 
            this.dpRemisionHasta.Enabled = false;
            this.dpRemisionHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpRemisionHasta.Location = new System.Drawing.Point(338, 5);
            this.dpRemisionHasta.Name = "dpRemisionHasta";
            this.dpRemisionHasta.Size = new System.Drawing.Size(81, 20);
            this.dpRemisionHasta.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Hasta";
            // 
            // dpRemisionDesde
            // 
            this.dpRemisionDesde.Enabled = false;
            this.dpRemisionDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpRemisionDesde.Location = new System.Drawing.Point(210, 5);
            this.dpRemisionDesde.Name = "dpRemisionDesde";
            this.dpRemisionDesde.Size = new System.Drawing.Size(81, 20);
            this.dpRemisionDesde.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desde";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(919, 490);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Reporte kardex cliente";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel6);
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(919, 490);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Reporte kardex proveedor";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExportarKC);
            this.panel3.Controls.Add(this.btnGenerarKC);
            this.panel3.Controls.Add(this.dpKCHasta);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.dpKCDesde);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(913, 86);
            this.panel3.TabIndex = 1;
            // 
            // btnExportarKC
            // 
            this.btnExportarKC.Location = new System.Drawing.Point(345, 31);
            this.btnExportarKC.Name = "btnExportarKC";
            this.btnExportarKC.Size = new System.Drawing.Size(75, 23);
            this.btnExportarKC.TabIndex = 44;
            this.btnExportarKC.Text = "Exportar";
            this.btnExportarKC.UseVisualStyleBackColor = true;
            this.btnExportarKC.Click += new System.EventHandler(this.btnExportarKC_Click);
            // 
            // btnGenerarKC
            // 
            this.btnGenerarKC.Location = new System.Drawing.Point(264, 31);
            this.btnGenerarKC.Name = "btnGenerarKC";
            this.btnGenerarKC.Size = new System.Drawing.Size(75, 23);
            this.btnGenerarKC.TabIndex = 43;
            this.btnGenerarKC.Text = "Generar";
            this.btnGenerarKC.UseVisualStyleBackColor = true;
            this.btnGenerarKC.Click += new System.EventHandler(this.btnGenerarKC_Click);
            // 
            // dpKCHasta
            // 
            this.dpKCHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpKCHasta.Location = new System.Drawing.Point(177, 32);
            this.dpKCHasta.Name = "dpKCHasta";
            this.dpKCHasta.Size = new System.Drawing.Size(81, 20);
            this.dpKCHasta.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(136, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Hasta";
            // 
            // dpKCDesde
            // 
            this.dpKCDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpKCDesde.Location = new System.Drawing.Point(49, 32);
            this.dpKCDesde.Name = "dpKCDesde";
            this.dpKCDesde.Size = new System.Drawing.Size(81, 20);
            this.dpKCDesde.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Desde";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.grdKardexClientes);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 89);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(913, 398);
            this.panel4.TabIndex = 2;
            // 
            // grdKardexClientes
            // 
            this.grdKardexClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKardexClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKardexClientes.Location = new System.Drawing.Point(0, 0);
            this.grdKardexClientes.Name = "grdKardexClientes";
            this.grdKardexClientes.Size = new System.Drawing.Size(913, 398);
            this.grdKardexClientes.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnExportarKP);
            this.panel5.Controls.Add(this.btnGenerarKP);
            this.panel5.Controls.Add(this.dpKPHasta);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.dpKPDesde);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(919, 86);
            this.panel5.TabIndex = 2;
            // 
            // btnExportarKP
            // 
            this.btnExportarKP.Location = new System.Drawing.Point(345, 31);
            this.btnExportarKP.Name = "btnExportarKP";
            this.btnExportarKP.Size = new System.Drawing.Size(75, 23);
            this.btnExportarKP.TabIndex = 44;
            this.btnExportarKP.Text = "Exportar";
            this.btnExportarKP.UseVisualStyleBackColor = true;
            this.btnExportarKP.Click += new System.EventHandler(this.btnExportarKP_Click);
            // 
            // btnGenerarKP
            // 
            this.btnGenerarKP.Location = new System.Drawing.Point(264, 31);
            this.btnGenerarKP.Name = "btnGenerarKP";
            this.btnGenerarKP.Size = new System.Drawing.Size(75, 23);
            this.btnGenerarKP.TabIndex = 43;
            this.btnGenerarKP.Text = "Generar";
            this.btnGenerarKP.UseVisualStyleBackColor = true;
            this.btnGenerarKP.Click += new System.EventHandler(this.btnGenerarKP_Click);
            // 
            // dpKPHasta
            // 
            this.dpKPHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpKPHasta.Location = new System.Drawing.Point(177, 32);
            this.dpKPHasta.Name = "dpKPHasta";
            this.dpKPHasta.Size = new System.Drawing.Size(81, 20);
            this.dpKPHasta.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Hasta";
            // 
            // dpKPDesde
            // 
            this.dpKPDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpKPDesde.Location = new System.Drawing.Point(49, 32);
            this.dpKPDesde.Name = "dpKPDesde";
            this.dpKPDesde.Size = new System.Drawing.Size(81, 20);
            this.dpKPDesde.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Desde";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.grdKardexProveedores);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 86);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(919, 404);
            this.panel6.TabIndex = 3;
            // 
            // grdKardexProveedores
            // 
            this.grdKardexProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKardexProveedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKardexProveedores.Location = new System.Drawing.Point(0, 0);
            this.grdKardexProveedores.Name = "grdKardexProveedores";
            this.grdKardexProveedores.Size = new System.Drawing.Size(919, 404);
            this.grdKardexProveedores.TabIndex = 0;
            // 
            // frmReportesEnvaseDevolutivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 516);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmReportesEnvaseDevolutivo";
            this.Text = "Reportes Envase Devolutivo";
            this.Load += new System.EventHandler(this.frmReportesEnvaseDevolutivo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCarteraClientes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKardexClientes)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKardexProveedores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox filtraFechaRecibo;
        private System.Windows.Forms.DateTimePicker dpReciboHasta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dpReciboDesde;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox filtraFechaRemision;
        private System.Windows.Forms.DateTimePicker dpRemisionHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpRemisionDesde;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdCarteraClientes;
        private System.Windows.Forms.Button btnExportaRepCartera;
        private System.Windows.Forms.Button btnGeneraRepCartera;
        private System.Windows.Forms.CheckBox filtraProveedor;
        private System.Windows.Forms.ComboBox cboProveedor;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExportarKC;
        private System.Windows.Forms.Button btnGenerarKC;
        private System.Windows.Forms.DateTimePicker dpKCHasta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dpKCDesde;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView grdKardexClientes;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView grdKardexProveedores;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExportarKP;
        private System.Windows.Forms.Button btnGenerarKP;
        private System.Windows.Forms.DateTimePicker dpKPHasta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dpKPDesde;
        private System.Windows.Forms.Label label6;

    }
}
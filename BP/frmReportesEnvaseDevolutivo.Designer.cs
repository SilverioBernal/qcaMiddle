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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.dpDesde = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbReporte = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdReporte = new System.Windows.Forms.DataGridView();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entregado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Retornado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mantenimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Listo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.dpHasta);
            this.panel1.Controls.Add(this.dpDesde);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbReporte);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(927, 76);
            this.panel1.TabIndex = 0;
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(769, 25);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 23);
            this.btnExportar.TabIndex = 14;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(674, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 13;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(549, 28);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.Size = new System.Drawing.Size(104, 20);
            this.dpHasta.TabIndex = 12;
            // 
            // dpDesde
            // 
            this.dpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesde.Location = new System.Drawing.Point(397, 28);
            this.dpDesde.Name = "dpDesde";
            this.dpDesde.Size = new System.Drawing.Size(104, 20);
            this.dpDesde.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(507, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Desde";
            // 
            // cbReporte
            // 
            this.cbReporte.FormattingEnabled = true;
            this.cbReporte.Items.AddRange(new object[] {
            "Tabla kardex EKA",
            "Tabla Kardex Genéricos",
            "Reporte de reacondicionamiento",
            "Reporte de cartera clientes",
            "Reporte de cartera proveedores"});
            this.cbReporte.Location = new System.Drawing.Point(63, 27);
            this.cbReporte.Name = "cbReporte";
            this.cbReporte.Size = new System.Drawing.Size(284, 21);
            this.cbReporte.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Reporte";
            // 
            // grdReporte
            // 
            this.grdReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReporte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tipo,
            this.DocNum,
            this.DocEntry,
            this.CardCode,
            this.CardName,
            this.ItemCode,
            this.ItemName,
            this.Entregado,
            this.Retornado,
            this.Mantenimiento,
            this.Listo});
            this.grdReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReporte.Location = new System.Drawing.Point(0, 76);
            this.grdReporte.Name = "grdReporte";
            this.grdReporte.Size = new System.Drawing.Size(927, 440);
            this.grdReporte.TabIndex = 1;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            // 
            // DocNum
            // 
            this.DocNum.HeaderText = "DocNum";
            this.DocNum.Name = "DocNum";
            // 
            // DocEntry
            // 
            this.DocEntry.HeaderText = "DocEntry";
            this.DocEntry.Name = "DocEntry";
            // 
            // CardCode
            // 
            this.CardCode.HeaderText = "CardCode";
            this.CardCode.Name = "CardCode";
            // 
            // CardName
            // 
            this.CardName.HeaderText = "CardName";
            this.CardName.Name = "CardName";
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            // 
            // Entregado
            // 
            this.Entregado.HeaderText = "Entregado";
            this.Entregado.Name = "Entregado";
            // 
            // Retornado
            // 
            this.Retornado.HeaderText = "Retornado";
            this.Retornado.Name = "Retornado";
            // 
            // Mantenimiento
            // 
            this.Mantenimiento.HeaderText = "Mantenimiento";
            this.Mantenimiento.Name = "Mantenimiento";
            // 
            // Listo
            // 
            this.Listo.HeaderText = "Listo";
            this.Listo.Name = "Listo";
            // 
            // frmReportesEnvaseDevolutivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 516);
            this.Controls.Add(this.grdReporte);
            this.Controls.Add(this.panel1);
            this.Name = "frmReportesEnvaseDevolutivo";
            this.Text = "frmReportesEnvaseDevolutivo";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dpHasta;
        private System.Windows.Forms.DateTimePicker dpDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbReporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView grdReporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Entregado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Retornado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mantenimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Listo;
    }
}
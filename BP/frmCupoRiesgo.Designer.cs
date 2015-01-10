namespace BP
{
    partial class frmCupoRiesgo
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
            this.cbxIngVentas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxProd = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvResultado = new System.Windows.Forms.DataGridView();
            this.btActualiza = new System.Windows.Forms.Button();
            this.chekColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mskPorcentaje = new System.Windows.Forms.MaskedTextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxIngVentas
            // 
            this.cbxIngVentas.FormattingEnabled = true;
            this.cbxIngVentas.Location = new System.Drawing.Point(134, 19);
            this.cbxIngVentas.Name = "cbxIngVentas";
            this.cbxIngVentas.Size = new System.Drawing.Size(121, 21);
            this.cbxIngVentas.TabIndex = 0;
            this.cbxIngVentas.DropDown += new System.EventHandler(this.cbxIngVentas_DropDown);
            this.cbxIngVentas.SelectedValueChanged += new System.EventHandler(this.cbxIngVentas_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ingeniero de Ventas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Unidad de Producto";
            // 
            // cbxProd
            // 
            this.cbxProd.FormattingEnabled = true;
            this.cbxProd.Location = new System.Drawing.Point(134, 46);
            this.cbxProd.Name = "cbxProd";
            this.cbxProd.Size = new System.Drawing.Size(121, 21);
            this.cbxProd.TabIndex = 2;
            this.cbxProd.DropDown += new System.EventHandler(this.cbxProd_DropDown);
            this.cbxProd.SelectedValueChanged += new System.EventHandler(this.cbxProd_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "% Actualizar (+/-)";
            // 
            // dgvResultado
            // 
            this.dgvResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultado.Location = new System.Drawing.Point(12, 123);
            this.dgvResultado.Name = "dgvResultado";
            this.dgvResultado.RowHeadersVisible = false;
            this.dgvResultado.Size = new System.Drawing.Size(634, 253);
            this.dgvResultado.TabIndex = 6;
            // 
            // btActualiza
            // 
            this.btActualiza.Enabled = false;
            this.btActualiza.Location = new System.Drawing.Point(93, 422);
            this.btActualiza.Name = "btActualiza";
            this.btActualiza.Size = new System.Drawing.Size(118, 23);
            this.btActualiza.TabIndex = 7;
            this.btActualiza.Text = "Actualizar";
            this.btActualiza.UseVisualStyleBackColor = true;
            this.btActualiza.Click += new System.EventHandler(this.btActualiza_Click);
            // 
            // chekColumn
            // 
            this.chekColumn.Name = "chekColumn";
            // 
            // mskPorcentaje
            // 
            this.mskPorcentaje.Location = new System.Drawing.Point(168, 69);
            this.mskPorcentaje.Mask = "99";
            this.mskPorcentaje.Name = "mskPorcentaje";
            this.mskPorcentaje.Size = new System.Drawing.Size(22, 20);
            this.mskPorcentaje.TabIndex = 8;
            this.mskPorcentaje.TextChanged += new System.EventHandler(this.mskPorcentaje_TextChanged);
            this.mskPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskPorcentaje_KeyPress);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(134, 70);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(28, 17);
            this.radioButton1.TabIndex = 9;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "-";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(242, 422);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 10;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // frmCupoRiesgo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 497);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.mskPorcentaje);
            this.Controls.Add(this.btActualiza);
            this.Controls.Add(this.dgvResultado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxProd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxIngVentas);
            this.Name = "frmCupoRiesgo";
            this.Text = "Cupo Riesgo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxIngVentas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxProd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvResultado;
        private System.Windows.Forms.Button btActualiza;
        private System.Windows.Forms.MaskedTextBox mskPorcentaje;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chekColumn;
        private System.Windows.Forms.Button btnLimpiar; 
    }
}
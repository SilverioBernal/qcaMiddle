namespace BP
{
    partial class frmCircularizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCircularizacion));
            this.cmbRepresentante = new System.Windows.Forms.ComboBox();
            this.cmbUnidad = new System.Windows.Forms.ComboBox();
            this.lblRepresentante = new System.Windows.Forms.Label();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbRepresentante
            // 
            this.cmbRepresentante.FormattingEnabled = true;
            this.cmbRepresentante.Location = new System.Drawing.Point(132, 52);
            this.cmbRepresentante.Name = "cmbRepresentante";
            this.cmbRepresentante.Size = new System.Drawing.Size(177, 21);
            this.cmbRepresentante.TabIndex = 0;
            this.cmbRepresentante.TextChanged += new System.EventHandler(this.cmbRepresentante_TextChanged);
            // 
            // cmbUnidad
            // 
            this.cmbUnidad.FormattingEnabled = true;
            this.cmbUnidad.Location = new System.Drawing.Point(132, 79);
            this.cmbUnidad.Name = "cmbUnidad";
            this.cmbUnidad.Size = new System.Drawing.Size(177, 21);
            this.cmbUnidad.TabIndex = 1;
            this.cmbUnidad.TextChanged += new System.EventHandler(this.cmbUnidad_TextChanged);
            // 
            // lblRepresentante
            // 
            this.lblRepresentante.AutoSize = true;
            this.lblRepresentante.Location = new System.Drawing.Point(13, 55);
            this.lblRepresentante.Name = "lblRepresentante";
            this.lblRepresentante.Size = new System.Drawing.Size(113, 13);
            this.lblRepresentante.TabIndex = 2;
            this.lblRepresentante.Text = "Representante Ventas";
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Location = new System.Drawing.Point(13, 82);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(99, 13);
            this.lblUnidad.TabIndex = 3;
            this.lblUnidad.Text = "Unidad de Negocio";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(132, 106);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Procesar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(41, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(299, 13);
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "Seleccione el Representante de Ventas o Unidad de Negocio";
            // 
            // frmCircularizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 152);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.lblUnidad);
            this.Controls.Add(this.lblRepresentante);
            this.Controls.Add(this.cmbUnidad);
            this.Controls.Add(this.cmbRepresentante);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCircularizacion";
            this.Text = "frmCircularizacion";
            this.Load += new System.EventHandler(this.frmCircularizacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRepresentante;
        private System.Windows.Forms.ComboBox cmbUnidad;
        private System.Windows.Forms.Label lblRepresentante;
        private System.Windows.Forms.Label lblUnidad;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblTitulo;
    }
}
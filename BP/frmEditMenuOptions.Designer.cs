namespace BP
{
    partial class frmEditMenuOptions
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdMenu = new System.Windows.Forms.DataGridView();
            this.pnlDevPass = new System.Windows.Forms.Panel();
            this.btnDesbloquear = new System.Windows.Forms.Button();
            this.txtDevPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEditMenu = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.saveData = new System.Windows.Forms.Button();
            this.newItem = new System.Windows.Forms.Button();
            this.clearMenu = new System.Windows.Forms.Button();
            this.cboIcono = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtForm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTextoMenu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txthijo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPadre = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdMenu)).BeginInit();
            this.pnlDevPass.SuspendLayout();
            this.pnlEditMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMenu
            // 
            this.grdMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMenu.Location = new System.Drawing.Point(12, 120);
            this.grdMenu.Name = "grdMenu";
            this.grdMenu.Size = new System.Drawing.Size(931, 398);
            this.grdMenu.TabIndex = 0;
            // 
            // pnlDevPass
            // 
            this.pnlDevPass.Controls.Add(this.btnDesbloquear);
            this.pnlDevPass.Controls.Add(this.txtDevPass);
            this.pnlDevPass.Controls.Add(this.label1);
            this.pnlDevPass.Location = new System.Drawing.Point(12, 12);
            this.pnlDevPass.Name = "pnlDevPass";
            this.pnlDevPass.Size = new System.Drawing.Size(931, 100);
            this.pnlDevPass.TabIndex = 1;
            // 
            // btnDesbloquear
            // 
            this.btnDesbloquear.Location = new System.Drawing.Point(480, 40);
            this.btnDesbloquear.Name = "btnDesbloquear";
            this.btnDesbloquear.Size = new System.Drawing.Size(75, 23);
            this.btnDesbloquear.TabIndex = 2;
            this.btnDesbloquear.Text = "Aceptar";
            this.btnDesbloquear.UseVisualStyleBackColor = true;
            this.btnDesbloquear.Click += new System.EventHandler(this.btnDesbloquear_Click);
            // 
            // txtDevPass
            // 
            this.txtDevPass.Location = new System.Drawing.Point(352, 42);
            this.txtDevPass.Name = "txtDevPass";
            this.txtDevPass.PasswordChar = '*';
            this.txtDevPass.Size = new System.Drawing.Size(122, 20);
            this.txtDevPass.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clave Desarrollo";
            // 
            // pnlEditMenu
            // 
            this.pnlEditMenu.Controls.Add(this.btnExportar);
            this.pnlEditMenu.Controls.Add(this.saveData);
            this.pnlEditMenu.Controls.Add(this.newItem);
            this.pnlEditMenu.Controls.Add(this.clearMenu);
            this.pnlEditMenu.Controls.Add(this.cboIcono);
            this.pnlEditMenu.Controls.Add(this.label6);
            this.pnlEditMenu.Controls.Add(this.txtForm);
            this.pnlEditMenu.Controls.Add(this.label5);
            this.pnlEditMenu.Controls.Add(this.txtTextoMenu);
            this.pnlEditMenu.Controls.Add(this.label4);
            this.pnlEditMenu.Controls.Add(this.txthijo);
            this.pnlEditMenu.Controls.Add(this.label3);
            this.pnlEditMenu.Controls.Add(this.cboPadre);
            this.pnlEditMenu.Controls.Add(this.label2);
            this.pnlEditMenu.Location = new System.Drawing.Point(12, 12);
            this.pnlEditMenu.Name = "pnlEditMenu";
            this.pnlEditMenu.Size = new System.Drawing.Size(931, 100);
            this.pnlEditMenu.TabIndex = 3;
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(577, 69);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(87, 23);
            this.btnExportar.TabIndex = 13;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // saveData
            // 
            this.saveData.Location = new System.Drawing.Point(484, 69);
            this.saveData.Name = "saveData";
            this.saveData.Size = new System.Drawing.Size(87, 23);
            this.saveData.TabIndex = 12;
            this.saveData.Text = "Guardar";
            this.saveData.UseVisualStyleBackColor = true;
            this.saveData.Click += new System.EventHandler(this.saveData_Click);
            // 
            // newItem
            // 
            this.newItem.Location = new System.Drawing.Point(391, 69);
            this.newItem.Name = "newItem";
            this.newItem.Size = new System.Drawing.Size(87, 23);
            this.newItem.TabIndex = 11;
            this.newItem.Text = "Nuevo Item";
            this.newItem.UseVisualStyleBackColor = true;
            this.newItem.Click += new System.EventHandler(this.newItem_Click);
            // 
            // clearMenu
            // 
            this.clearMenu.Location = new System.Drawing.Point(298, 69);
            this.clearMenu.Name = "clearMenu";
            this.clearMenu.Size = new System.Drawing.Size(87, 23);
            this.clearMenu.TabIndex = 10;
            this.clearMenu.Text = "Limpiar Menu";
            this.clearMenu.UseVisualStyleBackColor = true;
            this.clearMenu.Click += new System.EventHandler(this.clearMenu_Click);
            // 
            // cboIcono
            // 
            this.cboIcono.FormattingEnabled = true;
            this.cboIcono.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cboIcono.Location = new System.Drawing.Point(682, 13);
            this.cboIcono.Name = "cboIcono";
            this.cboIcono.Size = new System.Drawing.Size(121, 21);
            this.cboIcono.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(642, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Icono";
            // 
            // txtForm
            // 
            this.txtForm.Location = new System.Drawing.Point(298, 40);
            this.txtForm.Name = "txtForm";
            this.txtForm.Size = new System.Drawing.Size(330, 20);
            this.txtForm.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nombre Form";
            // 
            // txtTextoMenu
            // 
            this.txtTextoMenu.Location = new System.Drawing.Point(298, 13);
            this.txtTextoMenu.Name = "txtTextoMenu";
            this.txtTextoMenu.Size = new System.Drawing.Size(330, 20);
            this.txtTextoMenu.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Texto Menu";
            // 
            // txthijo
            // 
            this.txthijo.Location = new System.Drawing.Point(91, 41);
            this.txthijo.Name = "txthijo";
            this.txthijo.Size = new System.Drawing.Size(121, 20);
            this.txthijo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nodo Hijo";
            // 
            // cboPadre
            // 
            this.cboPadre.FormattingEnabled = true;
            this.cboPadre.Location = new System.Drawing.Point(91, 13);
            this.cboPadre.Name = "cboPadre";
            this.cboPadre.Size = new System.Drawing.Size(121, 21);
            this.cboPadre.TabIndex = 1;
            this.cboPadre.SelectedIndexChanged += new System.EventHandler(this.cboPadre_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nodo Padre";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 508);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(955, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // frmEditMenuOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 530);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlEditMenu);
            this.Controls.Add(this.pnlDevPass);
            this.Controls.Add(this.grdMenu);
            this.Name = "frmEditMenuOptions";
            this.ShowIcon = false;
            this.Text = "frmEditMenuOptions";
            this.Load += new System.EventHandler(this.frmEditMenuOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMenu)).EndInit();
            this.pnlDevPass.ResumeLayout(false);
            this.pnlDevPass.PerformLayout();
            this.pnlEditMenu.ResumeLayout(false);
            this.pnlEditMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdMenu;
        private System.Windows.Forms.Panel pnlDevPass;
        private System.Windows.Forms.Button btnDesbloquear;
        private System.Windows.Forms.TextBox txtDevPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlEditMenu;
        private System.Windows.Forms.TextBox txtForm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTextoMenu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txthijo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPadre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboIcono;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button saveData;
        private System.Windows.Forms.Button newItem;
        private System.Windows.Forms.Button clearMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnExportar;
    }
}
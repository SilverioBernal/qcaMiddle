namespace BP
{
    partial class frmCtrlUsrMiddle
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
            this.grdusr = new System.Windows.Forms.DataGridView();
            this.cboVldUsr = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdUsrMod = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdusr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsrMod)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdusr
            // 
            this.grdusr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdusr.Location = new System.Drawing.Point(12, 30);
            this.grdusr.Name = "grdusr";
            this.grdusr.Size = new System.Drawing.Size(194, 500);
            this.grdusr.TabIndex = 0;
            this.grdusr.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdusr_CellEndEdit);
            // 
            // cboVldUsr
            // 
            this.cboVldUsr.FormattingEnabled = true;
            this.cboVldUsr.Location = new System.Drawing.Point(293, 13);
            this.cboVldUsr.Name = "cboVldUsr";
            this.cboVldUsr.Size = new System.Drawing.Size(204, 21);
            this.cboVldUsr.TabIndex = 1;
            this.cboVldUsr.SelectedIndexChanged += new System.EventHandler(this.cboVldUsr_SelectedIndexChanged);
            this.cboVldUsr.Click += new System.EventHandler(this.cboVldUsr_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Usuario Actual";
            // 
            // grdUsrMod
            // 
            this.grdUsrMod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUsrMod.Location = new System.Drawing.Point(215, 60);
            this.grdUsrMod.Name = "grdUsrMod";
            this.grdUsrMod.Size = new System.Drawing.Size(495, 470);
            this.grdUsrMod.TabIndex = 3;
            this.grdUsrMod.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdUsrMod_CellMouseUp);
            this.grdUsrMod.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdUsrMod_CellEndEdit);
            this.grdUsrMod.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdUsrMod_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Usuarios Activos";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(722, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // frmCtrlUsrMiddle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 542);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grdUsrMod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboVldUsr);
            this.Controls.Add(this.grdusr);
            this.Name = "frmCtrlUsrMiddle";
            this.Text = "frmCtrlUsrMiddle";
            this.Load += new System.EventHandler(this.frmCtrlUsrMiddle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdusr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsrMod)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdusr;
        private System.Windows.Forms.ComboBox cboVldUsr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdUsrMod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
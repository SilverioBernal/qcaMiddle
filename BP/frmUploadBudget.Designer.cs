namespace BP
{
    partial class frmUploadBudget
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtpath = new System.Windows.Forms.TextBox();
            this.btn = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.btnCargarEstos = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ruta origen";
            // 
            // txtpath
            // 
            this.txtpath.Location = new System.Drawing.Point(80, 22);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(480, 20);
            this.txtpath.TabIndex = 1;
            // 
            // btn
            // 
            this.btn.AutoSize = true;
            this.btn.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btn.Location = new System.Drawing.Point(566, 20);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(26, 23);
            this.btn.TabIndex = 2;
            this.btn.Text = "...";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.AutoSize = true;
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnUpload.Location = new System.Drawing.Point(598, 19);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(55, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Validar";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.SystemColors.Window;
            this.txtlog.Location = new System.Drawing.Point(15, 80);
            this.txtlog.Name = "txtlog";
            this.txtlog.Size = new System.Drawing.Size(641, 336);
            this.txtlog.TabIndex = 4;
            this.txtlog.Text = "";
            // 
            // btnCargarEstos
            // 
            this.btnCargarEstos.Location = new System.Drawing.Point(253, 432);
            this.btnCargarEstos.Name = "btnCargarEstos";
            this.btnCargarEstos.Size = new System.Drawing.Size(109, 33);
            this.btnCargarEstos.TabIndex = 5;
            this.btnCargarEstos.Text = "Cargar";
            this.btnCargarEstos.UseVisualStyleBackColor = true;
            this.btnCargarEstos.Click += new System.EventHandler(this.btnCargarEstos_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(129, 471);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(353, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(80, 48);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(480, 12);
            this.progressBar2.TabIndex = 7;
            // 
            // frmUploadBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 506);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCargarEstos);
            this.Controls.Add(this.txtlog);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.txtpath);
            this.Controls.Add(this.label1);
            this.Name = "frmUploadBudget";
            this.Text = "Importacion de Presupuesto";
            this.Load += new System.EventHandler(this.frmUploadBudget_Load);
            this.VisibleChanged += new System.EventHandler(this.frmUploadBudget_VisibleChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmUploadBudget_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtpath;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.RichTextBox txtlog;
        private System.Windows.Forms.Button btnCargarEstos;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}
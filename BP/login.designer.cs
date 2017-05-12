namespace BP
{
    partial class loging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loging));
            this.CntConnect = new System.Windows.Forms.GroupBox();
            this.cbxTipoSver = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.txtPassSap = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsrSap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CntConnect.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CntConnect
            // 
            this.CntConnect.Controls.Add(this.cbxTipoSver);
            this.CntConnect.Controls.Add(this.button1);
            this.CntConnect.Controls.Add(this.btnConectar);
            this.CntConnect.Controls.Add(this.label3);
            this.CntConnect.Controls.Add(this.cboCompany);
            this.CntConnect.Controls.Add(this.txtPassSap);
            this.CntConnect.Controls.Add(this.label2);
            this.CntConnect.Controls.Add(this.txtUsrSap);
            this.CntConnect.Controls.Add(this.label1);
            this.CntConnect.Location = new System.Drawing.Point(0, 42);
            this.CntConnect.Name = "CntConnect";
            this.CntConnect.Size = new System.Drawing.Size(478, 117);
            this.CntConnect.TabIndex = 0;
            this.CntConnect.TabStop = false;
            this.CntConnect.Text = "Conectar con SAP";
            // 
            // cbxTipoSver
            // 
            this.cbxTipoSver.FormattingEnabled = true;
            this.cbxTipoSver.Items.AddRange(new object[] {
            "MSSQL 2008",
            "MSSQL 2012"});
            this.cbxTipoSver.Location = new System.Drawing.Point(364, 56);
            this.cbxTipoSver.Name = "cbxTipoSver";
            this.cbxTipoSver.Size = new System.Drawing.Size(97, 21);
            this.cbxTipoSver.TabIndex = 3;
            this.cbxTipoSver.Text = "MSSQL 2008";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(380, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Terminar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(293, 83);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(81, 23);
            this.btnConectar.TabIndex = 4;
            this.btnConectar.Text = "Iniciar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Compañia";
            // 
            // cboCompany
            // 
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(69, 30);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(392, 21);
            this.cboCompany.TabIndex = 0;
            // 
            // txtPassSap
            // 
            this.txtPassSap.Location = new System.Drawing.Point(236, 57);
            this.txtPassSap.Name = "txtPassSap";
            this.txtPassSap.PasswordChar = '*';
            this.txtPassSap.Size = new System.Drawing.Size(100, 20);
            this.txtPassSap.TabIndex = 2;
            this.txtPassSap.Text = "12345";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pass SAP";
            // 
            // txtUsrSap
            // 
            this.txtUsrSap.AutoCompleteCustomSource.AddRange(new string[] {
            "C_INGE",
            "B_INGE",
            "QCA",
            "C_SERCL",
            "B_SERCL",
            "M_SERCL",
            "M_INGE",
            "G_INGE",
            "G_SERCL"});
            this.txtUsrSap.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUsrSap.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUsrSap.Location = new System.Drawing.Point(69, 57);
            this.txtUsrSap.Name = "txtUsrSap";
            this.txtUsrSap.Size = new System.Drawing.Size(100, 20);
            this.txtUsrSap.TabIndex = 1;
            this.txtUsrSap.Text = "B_JEFTIC";
            this.txtUsrSap.TextChanged += new System.EventHandler(this.txtUsrSap_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User SAP";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 162);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(479, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "...";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BP.Properties.Resources.iconSAP;
            this.pictureBox1.Location = new System.Drawing.Point(439, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 38);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Inicio de Sesion";
            // 
            // loging
            // 
            this.AcceptButton = this.btnConectar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 184);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CntConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "loging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FromsSDK_SBD_Load);
            this.CntConnect.ResumeLayout(false);
            this.CntConnect.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox CntConnect;
        private System.Windows.Forms.TextBox txtPassSap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsrSap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxTipoSver;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
    }
}
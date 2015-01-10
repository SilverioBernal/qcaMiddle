namespace BP
{
    partial class FromsSDK_SBD
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
            this.CntConnect = new System.Windows.Forms.GroupBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.txtPassSap = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsrSap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CntWork = new System.Windows.Forms.GroupBox();
            this.cntPayedInvoices = new System.Windows.Forms.GroupBox();
            this.grdInvoice = new System.Windows.Forms.DataGridView();
            this.btnGetInvoices = new System.Windows.Forms.Button();
            this.txtSnName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSnCod = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CntSN = new System.Windows.Forms.GroupBox();
            this.btnDelBP = new System.Windows.Forms.Button();
            this.btnSaveBP = new System.Windows.Forms.Button();
            this.btnAddBP = new System.Windows.Forms.Button();
            this.btnGetBP = new System.Windows.Forms.Button();
            this.lstBP = new System.Windows.Forms.ListBox();
            this.txtNameBP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodeBP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CntConnect.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.CntWork.SuspendLayout();
            this.cntPayedInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoice)).BeginInit();
            this.CntSN.SuspendLayout();
            this.SuspendLayout();
            // 
            // CntConnect
            // 
            this.CntConnect.Controls.Add(this.btnConectar);
            this.CntConnect.Controls.Add(this.label3);
            this.CntConnect.Controls.Add(this.cboCompany);
            this.CntConnect.Controls.Add(this.txtPassSap);
            this.CntConnect.Controls.Add(this.label2);
            this.CntConnect.Controls.Add(this.txtUsrSap);
            this.CntConnect.Controls.Add(this.label1);
            this.CntConnect.Location = new System.Drawing.Point(2, 2);
            this.CntConnect.Name = "CntConnect";
            this.CntConnect.Size = new System.Drawing.Size(451, 88);
            this.CntConnect.TabIndex = 0;
            this.CntConnect.TabStop = false;
            this.CntConnect.Text = "Conectar con SAP";
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(342, 55);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(103, 23);
            this.btnConectar.TabIndex = 3;
            this.btnConectar.Text = "Conectar con SAP";
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
            this.cboCompany.Size = new System.Drawing.Size(376, 21);
            this.cboCompany.TabIndex = 0;
            // 
            // txtPassSap
            // 
            this.txtPassSap.Location = new System.Drawing.Point(236, 57);
            this.txtPassSap.Name = "txtPassSap";
            this.txtPassSap.PasswordChar = '*';
            this.txtPassSap.Size = new System.Drawing.Size(100, 20);
            this.txtPassSap.TabIndex = 2;
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
            this.txtUsrSap.Location = new System.Drawing.Point(69, 57);
            this.txtUsrSap.Name = "txtUsrSap";
            this.txtUsrSap.Size = new System.Drawing.Size(100, 20);
            this.txtUsrSap.TabIndex = 1;
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
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 627);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1179, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // CntWork
            // 
            this.CntWork.Controls.Add(this.cntPayedInvoices);
            this.CntWork.Controls.Add(this.CntSN);
            this.CntWork.Location = new System.Drawing.Point(2, 96);
            this.CntWork.Name = "CntWork";
            this.CntWork.Size = new System.Drawing.Size(1177, 528);
            this.CntWork.TabIndex = 2;
            this.CntWork.TabStop = false;
            this.CntWork.Text = "Trabajar con SAP";
            // 
            // cntPayedInvoices
            // 
            this.cntPayedInvoices.Controls.Add(this.grdInvoice);
            this.cntPayedInvoices.Controls.Add(this.btnGetInvoices);
            this.cntPayedInvoices.Controls.Add(this.txtSnName);
            this.cntPayedInvoices.Controls.Add(this.label6);
            this.cntPayedInvoices.Controls.Add(this.txtSnCod);
            this.cntPayedInvoices.Controls.Add(this.label7);
            this.cntPayedInvoices.Location = new System.Drawing.Point(6, 216);
            this.cntPayedInvoices.Name = "cntPayedInvoices";
            this.cntPayedInvoices.Size = new System.Drawing.Size(445, 306);
            this.cntPayedInvoices.TabIndex = 16;
            this.cntPayedInvoices.TabStop = false;
            this.cntPayedInvoices.Text = "Consulta de facturas pagadas";
            // 
            // grdInvoice
            // 
            this.grdInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInvoice.Location = new System.Drawing.Point(7, 71);
            this.grdInvoice.Name = "grdInvoice";
            this.grdInvoice.Size = new System.Drawing.Size(323, 229);
            this.grdInvoice.TabIndex = 13;
            // 
            // btnGetInvoices
            // 
            this.btnGetInvoices.Location = new System.Drawing.Point(336, 71);
            this.btnGetInvoices.Name = "btnGetInvoices";
            this.btnGetInvoices.Size = new System.Drawing.Size(103, 23);
            this.btnGetInvoices.TabIndex = 12;
            this.btnGetInvoices.Text = "Facturas Pagadas";
            this.btnGetInvoices.UseVisualStyleBackColor = true;
            this.btnGetInvoices.Click += new System.EventHandler(this.btnGetInvoices_Click);
            // 
            // txtSnName
            // 
            this.txtSnName.Location = new System.Drawing.Point(63, 45);
            this.txtSnName.Name = "txtSnName";
            this.txtSnName.Size = new System.Drawing.Size(376, 20);
            this.txtSnName.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Nom. SN";
            // 
            // txtSnCod
            // 
            this.txtSnCod.Location = new System.Drawing.Point(63, 19);
            this.txtSnCod.Name = "txtSnCod";
            this.txtSnCod.Size = new System.Drawing.Size(100, 20);
            this.txtSnCod.TabIndex = 7;
            this.txtSnCod.TextChanged += new System.EventHandler(this.txtSnCod_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Cod. SN";
            // 
            // CntSN
            // 
            this.CntSN.Controls.Add(this.btnDelBP);
            this.CntSN.Controls.Add(this.btnSaveBP);
            this.CntSN.Controls.Add(this.btnAddBP);
            this.CntSN.Controls.Add(this.btnGetBP);
            this.CntSN.Controls.Add(this.lstBP);
            this.CntSN.Controls.Add(this.txtNameBP);
            this.CntSN.Controls.Add(this.label5);
            this.CntSN.Controls.Add(this.txtCodeBP);
            this.CntSN.Controls.Add(this.label4);
            this.CntSN.Location = new System.Drawing.Point(6, 19);
            this.CntSN.Name = "CntSN";
            this.CntSN.Size = new System.Drawing.Size(445, 191);
            this.CntSN.TabIndex = 0;
            this.CntSN.TabStop = false;
            this.CntSN.Text = "Gestion de socios de negocio";
            // 
            // btnDelBP
            // 
            this.btnDelBP.Location = new System.Drawing.Point(336, 158);
            this.btnDelBP.Name = "btnDelBP";
            this.btnDelBP.Size = new System.Drawing.Size(103, 23);
            this.btnDelBP.TabIndex = 15;
            this.btnDelBP.Text = "Eliminar";
            this.btnDelBP.UseVisualStyleBackColor = true;
            this.btnDelBP.Click += new System.EventHandler(this.btnDelBP_Click);
            // 
            // btnSaveBP
            // 
            this.btnSaveBP.Location = new System.Drawing.Point(336, 129);
            this.btnSaveBP.Name = "btnSaveBP";
            this.btnSaveBP.Size = new System.Drawing.Size(103, 23);
            this.btnSaveBP.TabIndex = 14;
            this.btnSaveBP.Text = "Guardar";
            this.btnSaveBP.UseVisualStyleBackColor = true;
            this.btnSaveBP.Click += new System.EventHandler(this.btnSaveBP_Click);
            // 
            // btnAddBP
            // 
            this.btnAddBP.Location = new System.Drawing.Point(336, 100);
            this.btnAddBP.Name = "btnAddBP";
            this.btnAddBP.Size = new System.Drawing.Size(103, 23);
            this.btnAddBP.TabIndex = 13;
            this.btnAddBP.Text = "Nuevo";
            this.btnAddBP.UseVisualStyleBackColor = true;
            this.btnAddBP.Click += new System.EventHandler(this.btnAddBP_Click);
            // 
            // btnGetBP
            // 
            this.btnGetBP.Location = new System.Drawing.Point(336, 71);
            this.btnGetBP.Name = "btnGetBP";
            this.btnGetBP.Size = new System.Drawing.Size(103, 23);
            this.btnGetBP.TabIndex = 12;
            this.btnGetBP.Text = "Traer SN\'s";
            this.btnGetBP.UseVisualStyleBackColor = true;
            this.btnGetBP.Click += new System.EventHandler(this.btnGetBP_Click);
            // 
            // lstBP
            // 
            this.lstBP.FormattingEnabled = true;
            this.lstBP.Location = new System.Drawing.Point(6, 71);
            this.lstBP.Name = "lstBP";
            this.lstBP.Size = new System.Drawing.Size(324, 108);
            this.lstBP.TabIndex = 11;
            this.lstBP.DoubleClick += new System.EventHandler(this.lstBP_DoubleClick);
            // 
            // txtNameBP
            // 
            this.txtNameBP.Location = new System.Drawing.Point(63, 45);
            this.txtNameBP.Name = "txtNameBP";
            this.txtNameBP.Size = new System.Drawing.Size(376, 20);
            this.txtNameBP.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Nom. SN";
            // 
            // txtCodeBP
            // 
            this.txtCodeBP.Location = new System.Drawing.Point(63, 19);
            this.txtCodeBP.Name = "txtCodeBP";
            this.txtCodeBP.Size = new System.Drawing.Size(100, 20);
            this.txtCodeBP.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cod. SN";
            // 
            // FromsSDK_SBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 649);
            this.Controls.Add(this.CntWork);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CntConnect);
            this.Name = "FromsSDK_SBD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FromsSDK_SBD";
            this.Load += new System.EventHandler(this.FromsSDK_SBD_Load);
            this.CntConnect.ResumeLayout(false);
            this.CntConnect.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.CntWork.ResumeLayout(false);
            this.cntPayedInvoices.ResumeLayout(false);
            this.cntPayedInvoices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoice)).EndInit();
            this.CntSN.ResumeLayout(false);
            this.CntSN.PerformLayout();
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
        private System.Windows.Forms.GroupBox CntWork;
        private System.Windows.Forms.GroupBox CntSN;
        private System.Windows.Forms.Button btnSaveBP;
        private System.Windows.Forms.Button btnAddBP;
        private System.Windows.Forms.Button btnGetBP;
        private System.Windows.Forms.ListBox lstBP;
        private System.Windows.Forms.TextBox txtNameBP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodeBP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelBP;
        private System.Windows.Forms.GroupBox cntPayedInvoices;
        private System.Windows.Forms.Button btnGetInvoices;
        private System.Windows.Forms.TextBox txtSnName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSnCod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView grdInvoice;
    }
}
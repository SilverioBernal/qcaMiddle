namespace BP
{
    partial class frmBatchDistribution
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboWhFrom = new System.Windows.Forms.ComboBox();
            this.lblBodegaOrigen = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdItem = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.grdBatch = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdTrn = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtIq = new System.Windows.Forms.TextBox();
            this.lblQ = new System.Windows.Forms.Label();
            this.txtRealBatch = new System.Windows.Forms.TextBox();
            this.lblLote = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboWhTo = new System.Windows.Forms.ComboBox();
            this.lblBodegaDestino = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItem)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBatch)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTrn)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 673);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1079, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cboWhFrom);
            this.panel1.Controls.Add(this.lblBodegaOrigen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1079, 48);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(995, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(393, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 21);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboWhFrom
            // 
            this.cboWhFrom.FormattingEnabled = true;
            this.cboWhFrom.Location = new System.Drawing.Point(96, 12);
            this.cboWhFrom.Name = "cboWhFrom";
            this.cboWhFrom.Size = new System.Drawing.Size(284, 21);
            this.cboWhFrom.TabIndex = 1;
            // 
            // lblBodegaOrigen
            // 
            this.lblBodegaOrigen.AutoSize = true;
            this.lblBodegaOrigen.Location = new System.Drawing.Point(12, 15);
            this.lblBodegaOrigen.Name = "lblBodegaOrigen";
            this.lblBodegaOrigen.Size = new System.Drawing.Size(76, 13);
            this.lblBodegaOrigen.TabIndex = 0;
            this.lblBodegaOrigen.Text = "Bodega origen";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1079, 291);
            this.panel2.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdItem);
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdBatch);
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Size = new System.Drawing.Size(1079, 291);
            this.splitContainer1.SplitterDistance = 594;
            this.splitContainer1.TabIndex = 0;
            // 
            // grdItem
            // 
            this.grdItem.AllowUserToAddRows = false;
            this.grdItem.AllowUserToDeleteRows = false;
            this.grdItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdItem.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdItem.Location = new System.Drawing.Point(0, 22);
            this.grdItem.MultiSelect = false;
            this.grdItem.Name = "grdItem";
            this.grdItem.ReadOnly = true;
            this.grdItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdItem.Size = new System.Drawing.Size(594, 269);
            this.grdItem.TabIndex = 1;
            this.grdItem.SelectionChanged += new System.EventHandler(this.grdItem_SelectionChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(594, 22);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Artículos disponibles";
            // 
            // grdBatch
            // 
            this.grdBatch.AllowUserToAddRows = false;
            this.grdBatch.AllowUserToDeleteRows = false;
            this.grdBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBatch.Location = new System.Drawing.Point(0, 22);
            this.grdBatch.Name = "grdBatch";
            this.grdBatch.ReadOnly = true;
            this.grdBatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBatch.Size = new System.Drawing.Size(481, 269);
            this.grdBatch.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(481, 22);
            this.panel6.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lotes";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdTrn);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 339);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1079, 334);
            this.panel3.TabIndex = 4;
            // 
            // grdTrn
            // 
            this.grdTrn.AllowUserToAddRows = false;
            this.grdTrn.AllowUserToDeleteRows = false;
            this.grdTrn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTrn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTrn.Location = new System.Drawing.Point(0, 47);
            this.grdTrn.Name = "grdTrn";
            this.grdTrn.ReadOnly = true;
            this.grdTrn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTrn.Size = new System.Drawing.Size(1079, 287);
            this.grdTrn.TabIndex = 3;
            this.grdTrn.SelectionChanged += new System.EventHandler(this.grdTrn_SelectionChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnDeleteRow);
            this.panel4.Controls.Add(this.btnFinish);
            this.panel4.Controls.Add(this.txtQty);
            this.panel4.Controls.Add(this.lblQty);
            this.panel4.Controls.Add(this.txtIq);
            this.panel4.Controls.Add(this.lblQ);
            this.panel4.Controls.Add(this.txtRealBatch);
            this.panel4.Controls.Add(this.lblLote);
            this.panel4.Controls.Add(this.btnAdd);
            this.panel4.Controls.Add(this.cboWhTo);
            this.panel4.Controls.Add(this.lblBodegaDestino);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1079, 47);
            this.panel4.TabIndex = 2;
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Enabled = false;
            this.btnDeleteRow.Location = new System.Drawing.Point(916, 14);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(75, 21);
            this.btnDeleteRow.TabIndex = 10;
            this.btnDeleteRow.Text = "Eliminar";
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.Location = new System.Drawing.Point(995, 14);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 21);
            this.btnFinish.TabIndex = 9;
            this.btnFinish.Text = "Terminar";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(752, 14);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(77, 20);
            this.txtQty.TabIndex = 8;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(698, 16);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(49, 13);
            this.lblQty.TabIndex = 7;
            this.lblQty.Text = "Cantidad";
            // 
            // txtIq
            // 
            this.txtIq.Location = new System.Drawing.Point(590, 13);
            this.txtIq.Name = "txtIq";
            this.txtIq.Size = new System.Drawing.Size(102, 20);
            this.txtIq.TabIndex = 6;
            // 
            // lblQ
            // 
            this.lblQ.AutoSize = true;
            this.lblQ.Location = new System.Drawing.Point(566, 15);
            this.lblQ.Name = "lblQ";
            this.lblQ.Size = new System.Drawing.Size(18, 13);
            this.lblQ.TabIndex = 5;
            this.lblQ.Text = "IQ";
            // 
            // txtRealBatch
            // 
            this.txtRealBatch.Location = new System.Drawing.Point(444, 13);
            this.txtRealBatch.Name = "txtRealBatch";
            this.txtRealBatch.Size = new System.Drawing.Size(116, 20);
            this.txtRealBatch.TabIndex = 4;
            // 
            // lblLote
            // 
            this.lblLote.AutoSize = true;
            this.lblLote.Location = new System.Drawing.Point(390, 15);
            this.lblLote.Name = "lblLote";
            this.lblLote.Size = new System.Drawing.Size(48, 13);
            this.lblLote.TabIndex = 3;
            this.lblLote.Text = "Lote real";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(835, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 21);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Añadir";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cboWhTo
            // 
            this.cboWhTo.FormattingEnabled = true;
            this.cboWhTo.Location = new System.Drawing.Point(96, 12);
            this.cboWhTo.Name = "cboWhTo";
            this.cboWhTo.Size = new System.Drawing.Size(284, 21);
            this.cboWhTo.TabIndex = 1;
            // 
            // lblBodegaDestino
            // 
            this.lblBodegaDestino.AutoSize = true;
            this.lblBodegaDestino.Location = new System.Drawing.Point(12, 15);
            this.lblBodegaDestino.Name = "lblBodegaDestino";
            this.lblBodegaDestino.Size = new System.Drawing.Size(81, 13);
            this.lblBodegaDestino.TabIndex = 0;
            this.lblBodegaDestino.Text = "Bodega destino";
            // 
            // frmBatchDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 695);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmBatchDistribution";
            this.Text = "frmBatchDistribution";
            this.Load += new System.EventHandler(this.frmBatchDistribution_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItem)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBatch)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTrn)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboWhFrom;
        private System.Windows.Forms.Label lblBodegaOrigen;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView grdItem;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtIq;
        private System.Windows.Forms.Label lblQ;
        private System.Windows.Forms.TextBox txtRealBatch;
        private System.Windows.Forms.Label lblLote;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cboWhTo;
        private System.Windows.Forms.Label lblBodegaDestino;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.DataGridView grdBatch;
        private System.Windows.Forms.DataGridView grdTrn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDeleteRow;
    }
}
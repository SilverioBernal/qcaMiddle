namespace BP
{
    partial class frmMenuPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuPrincipal));
            this.strBarraMensajes = new System.Windows.Forms.StatusStrip();
            this.tslLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarOpciones = new System.Windows.Forms.ToolStripMenuItem();
            this.importarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.trwMenu = new System.Windows.Forms.TreeView();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.mstMenuPrincipal = new System.Windows.Forms.MenuStrip();
            this.tsmVentanas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCascada = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.splSeparador = new System.Windows.Forms.Splitter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cambiarDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.strBarraMensajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.mstMenuPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // strBarraMensajes
            // 
            this.strBarraMensajes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslLabel});
            this.strBarraMensajes.Location = new System.Drawing.Point(0, 504);
            this.strBarraMensajes.Name = "strBarraMensajes";
            this.strBarraMensajes.Size = new System.Drawing.Size(944, 22);
            this.strBarraMensajes.TabIndex = 2;
            this.strBarraMensajes.Text = "statusStrip1";
            // 
            // tslLabel
            // 
            this.tslLabel.Name = "tslLabel";
            this.tslLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "PIN01D.ICO");
            this.imageList1.Images.SetKeyName(1, "PINUP01A.ICO");
            this.imageList1.Images.SetKeyName(2, "PINUP01B.ICO");
            this.imageList1.Images.SetKeyName(3, "PINUP02A.ICO");
            this.imageList1.Images.SetKeyName(4, "public.ico");
            // 
            // picLogo
            // 
            this.picLogo.ContextMenuStrip = this.ctxMenu;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(0, 417);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(260, 63);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarOpciones,
            this.importarMenu});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(211, 48);
            // 
            // editarOpciones
            // 
            this.editarOpciones.Name = "editarOpciones";
            this.editarOpciones.Size = new System.Drawing.Size(210, 22);
            this.editarOpciones.Text = "Editar Opciones del Menu";
            this.editarOpciones.Click += new System.EventHandler(this.editarOpciones_Click);
            // 
            // importarMenu
            // 
            this.importarMenu.Name = "importarMenu";
            this.importarMenu.Size = new System.Drawing.Size(210, 22);
            this.importarMenu.Text = "Importar Menu";
            this.importarMenu.Click += new System.EventHandler(this.importarMenu_Click);
            // 
            // trwMenu
            // 
            this.trwMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trwMenu.ImageIndex = 0;
            this.trwMenu.ImageList = this.imageList1;
            this.trwMenu.Location = new System.Drawing.Point(0, 0);
            this.trwMenu.Name = "trwMenu";
            this.trwMenu.SelectedImageIndex = 0;
            this.trwMenu.Size = new System.Drawing.Size(260, 421);
            this.trwMenu.TabIndex = 6;
            this.trwMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.picLogo);
            this.pnlMenu.Controls.Add(this.trwMenu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 24);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(260, 480);
            this.pnlMenu.TabIndex = 7;
            // 
            // mstMenuPrincipal
            // 
            this.mstMenuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmVentanas,
            this.tsmSalir});
            this.mstMenuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mstMenuPrincipal.MdiWindowListItem = this.tsmVentanas;
            this.mstMenuPrincipal.Name = "mstMenuPrincipal";
            this.mstMenuPrincipal.Size = new System.Drawing.Size(944, 24);
            this.mstMenuPrincipal.TabIndex = 10;
            this.mstMenuPrincipal.Text = "menuStrip1";
            // 
            // tsmVentanas
            // 
            this.tsmVentanas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCascada,
            this.tsmiVertical,
            this.tsmiHorizontal,
            this.tsmiCerrar});
            this.tsmVentanas.Name = "tsmVentanas";
            this.tsmVentanas.Size = new System.Drawing.Size(67, 20);
            this.tsmVentanas.Text = "Ventanas";
            // 
            // tsmiCascada
            // 
            this.tsmiCascada.Name = "tsmiCascada";
            this.tsmiCascada.Size = new System.Drawing.Size(129, 22);
            this.tsmiCascada.Text = "Cascada";
            this.tsmiCascada.Click += new System.EventHandler(this.tsmiCascada_Click);
            // 
            // tsmiVertical
            // 
            this.tsmiVertical.Name = "tsmiVertical";
            this.tsmiVertical.Size = new System.Drawing.Size(129, 22);
            this.tsmiVertical.Text = "Vertical";
            this.tsmiVertical.Click += new System.EventHandler(this.tsmiVertical_Click);
            // 
            // tsmiHorizontal
            // 
            this.tsmiHorizontal.Name = "tsmiHorizontal";
            this.tsmiHorizontal.Size = new System.Drawing.Size(129, 22);
            this.tsmiHorizontal.Text = "Horizontal";
            this.tsmiHorizontal.Click += new System.EventHandler(this.tsmiHorizontal_Click);
            // 
            // tsmiCerrar
            // 
            this.tsmiCerrar.Name = "tsmiCerrar";
            this.tsmiCerrar.Size = new System.Drawing.Size(129, 22);
            this.tsmiCerrar.Text = "Cerrar";
            this.tsmiCerrar.Click += new System.EventHandler(this.tsmiCerrar_Click);
            // 
            // tsmSalir
            // 
            this.tsmSalir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSalir,
            this.cambiarDeUsuarioToolStripMenuItem});
            this.tsmSalir.Name = "tsmSalir";
            this.tsmSalir.Size = new System.Drawing.Size(41, 20);
            this.tsmSalir.Text = "Salir";
            // 
            // tsmiSalir
            // 
            this.tsmiSalir.Name = "tsmiSalir";
            this.tsmiSalir.Size = new System.Drawing.Size(96, 22);
            this.tsmiSalir.Text = "Salir";
            this.tsmiSalir.Click += new System.EventHandler(this.salirToolStripMenuItem1_Click);
            // 
            // splSeparador
            // 
            this.splSeparador.Location = new System.Drawing.Point(260, 24);
            this.splSeparador.Name = "splSeparador";
            this.splSeparador.Size = new System.Drawing.Size(3, 480);
            this.splSeparador.TabIndex = 11;
            this.splSeparador.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::BP.Properties.Resources.iconSAP;
            this.pictureBox1.Location = new System.Drawing.Point(890, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // cambiarDeUsuarioToolStripMenuItem
            // 
            this.cambiarDeUsuarioToolStripMenuItem.Name = "cambiarDeUsuarioToolStripMenuItem";
            this.cambiarDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.cambiarDeUsuarioToolStripMenuItem.Text = "Cambiar de Usuario";
            this.cambiarDeUsuarioToolStripMenuItem.Click += new System.EventHandler(this.cambiarDeUsuarioToolStripMenuItem_Click);
            // 
            // frmMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 526);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.splSeparador);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.strBarraMensajes);
            this.Controls.Add(this.mstMenuPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mstMenuPrincipal;
            this.Name = "frmMenuPrincipal";
            this.Text = "Formulario Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMenuPrincipal_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMenuPrincipal_FormClosed);
            this.strBarraMensajes.ResumeLayout(false);
            this.strBarraMensajes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            this.mstMenuPrincipal.ResumeLayout(false);
            this.mstMenuPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip strBarraMensajes;
        private System.Windows.Forms.ToolStripStatusLabel tslLabel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem editarOpciones;
        private System.Windows.Forms.ToolStripMenuItem importarMenu;
        private System.Windows.Forms.TreeView trwMenu;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.MenuStrip mstMenuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem tsmSalir;
        private System.Windows.Forms.ToolStripMenuItem tsmiSalir;
        private System.Windows.Forms.Splitter splSeparador;
        private System.Windows.Forms.ToolStripMenuItem tsmVentanas;
        private System.Windows.Forms.ToolStripMenuItem tsmiCascada;
        private System.Windows.Forms.ToolStripMenuItem tsmiVertical;
        private System.Windows.Forms.ToolStripMenuItem tsmiHorizontal;
        private System.Windows.Forms.ToolStripMenuItem tsmiCerrar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem cambiarDeUsuarioToolStripMenuItem;
    }
}
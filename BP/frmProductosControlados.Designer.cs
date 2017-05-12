namespace BP
{
    partial class frmProductosControlados
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
            this.lblReferencia = new System.Windows.Forms.Label();
            this.lblDescipcionArticulo = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.grbGrupoArticulo = new System.Windows.Forms.GroupBox();
            this.cmbDescripcionArticulo = new System.Windows.Forms.ComboBox();
            this.grbGrupoEntidad = new System.Windows.Forms.GroupBox();
            this.cmbTercero = new System.Windows.Forms.ComboBox();
            this.cmbNombreTercero = new System.Windows.Forms.ComboBox();
            this.lblNombreTercero = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbGrupoCertificado = new System.Windows.Forms.GroupBox();
            this.cmbDestino = new System.Windows.Forms.ComboBox();
            this.cmbCalidad = new System.Windows.Forms.ComboBox();
            this.mtbCupo = new System.Windows.Forms.MaskedTextBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.txtCertificado = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.cmbMunicipio = new System.Windows.Forms.ComboBox();
            this.lblMunicipio = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.grbGrupoArticulo.SuspendLayout();
            this.grbGrupoEntidad.SuspendLayout();
            this.grbGrupoCertificado.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(10, 17);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(59, 13);
            this.lblReferencia.TabIndex = 0;
            this.lblReferencia.Text = "Referencia";
            // 
            // lblDescipcionArticulo
            // 
            this.lblDescipcionArticulo.AutoSize = true;
            this.lblDescipcionArticulo.Location = new System.Drawing.Point(10, 44);
            this.lblDescipcionArticulo.Name = "lblDescipcionArticulo";
            this.lblDescipcionArticulo.Size = new System.Drawing.Size(103, 13);
            this.lblDescipcionArticulo.TabIndex = 1;
            this.lblDescipcionArticulo.Text = "Descripción Artículo";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(10, 20);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(114, 13);
            this.lblTipo.TabIndex = 2;
            this.lblTipo.Text = "(C)liente/(P)rov./(Q)CA";
            // 
            // cmbItem
            // 
            this.cmbItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(132, 14);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(157, 21);
            this.cmbItem.TabIndex = 3;
            this.cmbItem.TextChanged += new System.EventHandler(this.cmbItem_TextChanged);
            // 
            // cmbTipo
            // 
            this.cmbTipo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            " --Seleccione Tipo--",
            "C",
            "P",
            "Q"});
            this.cmbTipo.Location = new System.Drawing.Point(132, 17);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(157, 21);
            this.cmbTipo.TabIndex = 5;
            this.cmbTipo.TextChanged += new System.EventHandler(this.cmbTipo_TextChanged);
            // 
            // grbGrupoArticulo
            // 
            this.grbGrupoArticulo.Controls.Add(this.cmbDescripcionArticulo);
            this.grbGrupoArticulo.Controls.Add(this.cmbItem);
            this.grbGrupoArticulo.Controls.Add(this.lblReferencia);
            this.grbGrupoArticulo.Controls.Add(this.lblDescipcionArticulo);
            this.grbGrupoArticulo.Location = new System.Drawing.Point(12, 29);
            this.grbGrupoArticulo.Name = "grbGrupoArticulo";
            this.grbGrupoArticulo.Size = new System.Drawing.Size(441, 68);
            this.grbGrupoArticulo.TabIndex = 6;
            this.grbGrupoArticulo.TabStop = false;
            this.grbGrupoArticulo.Text = "Artículo";
            // 
            // cmbDescripcionArticulo
            // 
            this.cmbDescripcionArticulo.FormattingEnabled = true;
            this.cmbDescripcionArticulo.Location = new System.Drawing.Point(132, 41);
            this.cmbDescripcionArticulo.Name = "cmbDescripcionArticulo";
            this.cmbDescripcionArticulo.Size = new System.Drawing.Size(303, 21);
            this.cmbDescripcionArticulo.TabIndex = 4;
            // 
            // grbGrupoEntidad
            // 
            this.grbGrupoEntidad.Controls.Add(this.cmbTercero);
            this.grbGrupoEntidad.Controls.Add(this.cmbNombreTercero);
            this.grbGrupoEntidad.Controls.Add(this.lblNombreTercero);
            this.grbGrupoEntidad.Controls.Add(this.label1);
            this.grbGrupoEntidad.Controls.Add(this.cmbTipo);
            this.grbGrupoEntidad.Controls.Add(this.lblTipo);
            this.grbGrupoEntidad.Location = new System.Drawing.Point(12, 114);
            this.grbGrupoEntidad.Name = "grbGrupoEntidad";
            this.grbGrupoEntidad.Size = new System.Drawing.Size(441, 98);
            this.grbGrupoEntidad.TabIndex = 7;
            this.grbGrupoEntidad.TabStop = false;
            this.grbGrupoEntidad.Text = "Entidad";
            // 
            // cmbTercero
            // 
            this.cmbTercero.FormattingEnabled = true;
            this.cmbTercero.Location = new System.Drawing.Point(132, 46);
            this.cmbTercero.Name = "cmbTercero";
            this.cmbTercero.Size = new System.Drawing.Size(157, 21);
            this.cmbTercero.TabIndex = 10;
            // 
            // cmbNombreTercero
            // 
            this.cmbNombreTercero.FormattingEnabled = true;
            this.cmbNombreTercero.Location = new System.Drawing.Point(132, 73);
            this.cmbNombreTercero.Name = "cmbNombreTercero";
            this.cmbNombreTercero.Size = new System.Drawing.Size(303, 21);
            this.cmbNombreTercero.TabIndex = 9;
            // 
            // lblNombreTercero
            // 
            this.lblNombreTercero.AutoSize = true;
            this.lblNombreTercero.Location = new System.Drawing.Point(10, 73);
            this.lblNombreTercero.Name = "lblNombreTercero";
            this.lblNombreTercero.Size = new System.Drawing.Size(84, 13);
            this.lblNombreTercero.TabIndex = 8;
            this.lblNombreTercero.Text = "Nombre Tercero";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Código Tercero";
            // 
            // grbGrupoCertificado
            // 
            this.grbGrupoCertificado.Controls.Add(this.txtDescripcion);
            this.grbGrupoCertificado.Controls.Add(this.lblDescripcion);
            this.grbGrupoCertificado.Controls.Add(this.cmbMunicipio);
            this.grbGrupoCertificado.Controls.Add(this.lblMunicipio);
            this.grbGrupoCertificado.Controls.Add(this.cmbDestino);
            this.grbGrupoCertificado.Controls.Add(this.cmbCalidad);
            this.grbGrupoCertificado.Controls.Add(this.mtbCupo);
            this.grbGrupoCertificado.Controls.Add(this.dtpFechaFin);
            this.grbGrupoCertificado.Controls.Add(this.dtpFechaInicio);
            this.grbGrupoCertificado.Controls.Add(this.label5);
            this.grbGrupoCertificado.Controls.Add(this.label4);
            this.grbGrupoCertificado.Controls.Add(this.label3);
            this.grbGrupoCertificado.Controls.Add(this.lblFechaFin);
            this.grbGrupoCertificado.Controls.Add(this.lblFechaInicio);
            this.grbGrupoCertificado.Controls.Add(this.txtCertificado);
            this.grbGrupoCertificado.Controls.Add(this.label2);
            this.grbGrupoCertificado.Location = new System.Drawing.Point(13, 233);
            this.grbGrupoCertificado.Name = "grbGrupoCertificado";
            this.grbGrupoCertificado.Size = new System.Drawing.Size(440, 264);
            this.grbGrupoCertificado.TabIndex = 8;
            this.grbGrupoCertificado.TabStop = false;
            this.grbGrupoCertificado.Text = "Certificado";
            // 
            // cmbDestino
            // 
            this.cmbDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDestino.FormattingEnabled = true;
            this.cmbDestino.Location = new System.Drawing.Point(131, 147);
            this.cmbDestino.Name = "cmbDestino";
            this.cmbDestino.Size = new System.Drawing.Size(303, 21);
            this.cmbDestino.TabIndex = 11;
            // 
            // cmbCalidad
            // 
            this.cmbCalidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCalidad.FormattingEnabled = true;
            this.cmbCalidad.Location = new System.Drawing.Point(131, 118);
            this.cmbCalidad.Name = "cmbCalidad";
            this.cmbCalidad.Size = new System.Drawing.Size(303, 21);
            this.cmbCalidad.TabIndex = 10;
            // 
            // mtbCupo
            // 
            this.mtbCupo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mtbCupo.Location = new System.Drawing.Point(131, 92);
            this.mtbCupo.Mask = "9999999999";
            this.mtbCupo.Name = "mtbCupo";
            this.mtbCupo.Size = new System.Drawing.Size(121, 20);
            this.mtbCupo.TabIndex = 9;
            this.mtbCupo.ValidatingType = typeof(int);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(131, 66);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(121, 20);
            this.dtpFechaFin.TabIndex = 8;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(131, 39);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(121, 20);
            this.dtpFechaInicio.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Destino";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Calidad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cupo";
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Location = new System.Drawing.Point(9, 70);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(98, 13);
            this.lblFechaFin.TabIndex = 3;
            this.lblFechaFin.Text = "Fecha Fin Vigencia";
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Location = new System.Drawing.Point(9, 43);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(109, 13);
            this.lblFechaInicio.TabIndex = 2;
            this.lblFechaInicio.Text = "Fecha Inicio Vigencia";
            // 
            // txtCertificado
            // 
            this.txtCertificado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCertificado.Location = new System.Drawing.Point(131, 13);
            this.txtCertificado.MaxLength = 20;
            this.txtCertificado.Name = "txtCertificado";
            this.txtCertificado.Size = new System.Drawing.Size(121, 20);
            this.txtCertificado.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Certificado";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdicionar.Location = new System.Drawing.Point(25, 503);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 9;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminar.Location = new System.Drawing.Point(187, 503);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConsultar.Location = new System.Drawing.Point(268, 503);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 11;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActualizar.Location = new System.Drawing.Point(106, 503);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 12;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // cmbMunicipio
            // 
            this.cmbMunicipio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMunicipio.FormattingEnabled = true;
            this.cmbMunicipio.Location = new System.Drawing.Point(132, 174);
            this.cmbMunicipio.Name = "cmbMunicipio";
            this.cmbMunicipio.Size = new System.Drawing.Size(303, 21);
            this.cmbMunicipio.TabIndex = 13;
            // 
            // lblMunicipio
            // 
            this.lblMunicipio.AutoSize = true;
            this.lblMunicipio.Location = new System.Drawing.Point(9, 177);
            this.lblMunicipio.Name = "lblMunicipio";
            this.lblMunicipio.Size = new System.Drawing.Size(52, 13);
            this.lblMunicipio.TabIndex = 12;
            this.lblMunicipio.Text = "Municipio";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(9, 207);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 14;
            this.lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(131, 207);
            this.txtDescripcion.MaxLength = 150;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(303, 51);
            this.txtDescripcion.TabIndex = 15;
            // 
            // frmProductosControlados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 541);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.grbGrupoCertificado);
            this.Controls.Add(this.grbGrupoEntidad);
            this.Controls.Add(this.grbGrupoArticulo);
            this.Name = "frmProductosControlados";
            this.Text = "Mantenimiento Productos Controlados";
            this.Load += new System.EventHandler(this.frmProductosControlados_Load);
            this.grbGrupoArticulo.ResumeLayout(false);
            this.grbGrupoArticulo.PerformLayout();
            this.grbGrupoEntidad.ResumeLayout(false);
            this.grbGrupoEntidad.PerformLayout();
            this.grbGrupoCertificado.ResumeLayout(false);
            this.grbGrupoCertificado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.Label lblDescipcionArticulo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.GroupBox grbGrupoArticulo;
        private System.Windows.Forms.GroupBox grbGrupoEntidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombreTercero;
        private System.Windows.Forms.GroupBox grbGrupoCertificado;
        private System.Windows.Forms.TextBox txtCertificado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.MaskedTextBox mtbCupo;
        private System.Windows.Forms.ComboBox cmbDestino;
        private System.Windows.Forms.ComboBox cmbCalidad;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.ComboBox cmbDescripcionArticulo;
        private System.Windows.Forms.ComboBox cmbNombreTercero;
        private System.Windows.Forms.ComboBox cmbTercero;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.ComboBox cmbMunicipio;
        private System.Windows.Forms.Label lblMunicipio;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAPbobsCOM;

namespace BP
{
    public partial class frmReporteVentasAseguradora : Form
    {
        public DataSet miDataSet;
        public frmReporteVentasAseguradora()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(this.dtpFechaFinal.Text) > Convert.ToDateTime(this.dtpFechaInicial.Text))
                {
                    if (mtbMonto.Text.Length > 0 && !mtbMonto.Text.Equals("0"))
                    {
                        if (!this.cmbAseguradora.Text.Equals(" --Seleccione Aseguradora--"))
                        {
                            string miSql = "SELECT DISTINCT T0.CardCode AS [Código SAP],T0.CardName AS [Nombre Socio Negocios],T0.LicTradNum AS Nit,CAST(U_CSS_CupoAprobado AS decimal(18,2)) [Cupo Aprobado],U_CSS_Fecha_Ini_Vige [Inicio Vigencia],U_CSS_Fecha_Fin_Vige [Fin Vigencia],T0.U_CSS_Cliente_Asegur [Código Cliente Aseguradora],CAST (SUM(DocTotal - PaidToDate) AS decimal(18,2))  Saldo,CAST (SUM(DocTotal - PaidToDate) AS decimal(18,2))  SaldoTotal,CAST (SUM(DocTotal - PaidToDate) AS decimal(18,2)) - CAST(U_CSS_CupoAprobado as decimal (18,2)) Diferencia   " +
                                            "FROM OCRD T0 " +
                                            "INNER JOIN OINV T1 " +
                                            "ON T0.CardCode=T1.CardCode " +
                                            "AND T1.DocDate BETWEEN '" + Convert.ToDateTime(this.dtpFechaInicial.Text).ToString("yyyyMMdd") + "' AND '" + Convert.ToDateTime(this.dtpFechaFinal.Text).ToString("yyyyMMdd") + "' " +
                                            "AND T1.DocDate >= T0.U_CSS_Fecha_Ini_Vige " +
                                            "AND T1.DocDate <= T0.U_CSS_Fecha_Fin_Vige " +
                                            "AND T0.U_CSS_Codigo_Asegura='" + this.cmbAseguradora.SelectedValue + "' " +
                                            "  where (U_CSS_CupoAprobado>0)     GROUP BY T0.CardCode,T0.CardName,T0.LicTradNum,U_CSS_CupoAprobado,U_CSS_Fecha_Ini_Vige,U_CSS_Fecha_Fin_Vige,T0.U_CSS_Cliente_Asegur " +
                                            "HAVING SUM(DocTotal - PaidToDate) >= '" + this.mtbMonto.Text + "' " +
                                            "ORDER BY Diferencia desc " +

                                            " SELECT [Número Documento],CardCode,[Código SAP],Saldo,[Fecha Documento],[Fecha-Vence Documento] FROM(    SELECT * FROM (SELECT T0.CardCode " +
                                            "FROM OCRD T0 " +
                                            "INNER JOIN OINV T1 " +
                                            "ON T0.CardCode=T1.CardCode " +
                                            "AND T1.DocDate BETWEEN '" + Convert.ToDateTime(this.dtpFechaInicial.Text).ToString("yyyyMMdd") + "' AND '" + Convert.ToDateTime(this.dtpFechaFinal.Text).ToString("yyyyMMdd") + "' " +
                                            "AND T1.DocDate >= T0.U_CSS_Fecha_Ini_Vige " +
                                            "AND T1.DocDate <= T0.U_CSS_Fecha_Fin_Vige " +
                                            "AND T0.U_CSS_Codigo_Asegura='" + this.cmbAseguradora.SelectedValue + "' " +
                                            " where  (U_CSS_CupoAprobado>0)  GROUP BY T0.CardCode,T0.CardName,T0.LicTradNum,U_CSS_CupoAprobado,U_CSS_Fecha_Ini_Vige,U_CSS_Fecha_Fin_Vige,T0.U_CSS_Cliente_Asegur " +
                                            "  HAVING    SUM(DocTotal - PaidToDate) >= '" + this.mtbMonto.Text + "') T1 " +
                                            "INNER JOIN " +
                                            "(SELECT T0.CardCode [Código SAP], CAST((DocTotal - PaidToDate) AS DECIMAL(18,2)) AS Saldo, T1.DocNum [Número Documento],T1.DocDate [Fecha Documento],T1.DocDueDate [Fecha-Vence Documento] " +
                                            "FROM OCRD T0 " +
                                            "INNER JOIN OINV T1 " +
                                            "ON T0.CardCode=T1.CardCode " +
                                            "AND T1.DocDate BETWEEN '" + Convert.ToDateTime(this.dtpFechaInicial.Text).ToString("yyyyMMdd") + "' AND '" + Convert.ToDateTime(this.dtpFechaFinal.Text).ToString("yyyyMMdd") + "' " +
                                            "AND T1.DocDate >= T0.U_CSS_Fecha_Ini_Vige " +
                                            "AND T1.DocDate <= T0.U_CSS_Fecha_Fin_Vige " +
                                            "AND T0.U_CSS_Codigo_Asegura='" + this.cmbAseguradora.SelectedValue + "' " +
                                            "WHERE (DocTotal - PaidToDate) > 0) T2 " +
                                            "ON T1.CardCode=T2.[Código SAP] )TTR";
                            miDataSet = ClaseDatos.procesaDataSet(miSql);
                            miDataSet.Tables[0].TableName = "Clientes";
                            miDataSet.Tables[1].TableName = "Ventas";
                            Type miVariable = Type.GetType("System.Boolean");
                            miDataSet.Tables[1].Columns.Add("Seleccione", miVariable);                            
                            miDataSet.Relations.Add("miRelacion", miDataSet.Tables["Clientes"].Columns["Código SAP"], miDataSet.Tables["Ventas"].Columns["Código SAP"]);
                            for (int miContador = 0; miContador < miDataSet.Tables[1].Rows.Count; miContador++)
                            {
                                miDataSet.Tables[1].Rows[miContador]["Seleccione"] = true;
                            }
                            this.dgvEncabezado.DataSource = miDataSet;
                            this.dgvEncabezado.DataMember = "Clientes";
                            this.dgvEncabezado.Columns[3].HeaderText = "Cupo Asegurado";
                            this.dgvEncabezado.Columns[3].DefaultCellStyle.Format = "C2";
                            this.dgvEncabezado.Columns[7].DefaultCellStyle.Format = "C2";
                            this.dgvEncabezado.Columns[8].DefaultCellStyle.Format = "C2";
                            this.dgvEncabezado.Columns[9].DefaultCellStyle.Format = "C2";
                            this.dgvDetalle.DataSource = miDataSet;
                            this.dgvDetalle.DataMember = "Clientes.miRelacion";
                            this.dgvDetalle.Columns[1].Visible = false;
                            this.dgvDetalle.Columns[3].DefaultCellStyle.Format = "C2";
                            this.dgvDetalle.Columns[4].ReadOnly = true;
                            this.dgvDetalle.Columns[5].ReadOnly = true;
                            this.dgvDetalle.Columns[0].ReadOnly = true;
                            this.dgvDetalle.Columns[2].ReadOnly=true;
                            this.btnImportar.Enabled = true;
                            this.dgvEncabezado.ReadOnly = true;

                            double sum = 0.0, suma = 0.0, suma1 = 0.0;

                            for (int a=0; a < dgvEncabezado.Rows.Count;a++ ) {

                               
                                sum += Convert.ToDouble(dgvEncabezado.Rows[a].Cells[7].Value.ToString());
                                suma1 += Convert.ToDouble(dgvEncabezado.Rows[a].Cells[8].Value.ToString());
                                suma += Convert.ToDouble(dgvEncabezado.Rows[a].Cells[9].Value.ToString());

                                txtTotal.Text = sum.ToString("C2");
                                txtTotal1.Text = suma.ToString("C2");
                                textBox1.Text = suma1.ToString("C2");


                                //cODIGO DE FREDISITO


                                this.dgvEncabezado.Rows[a].Cells[4].Value = this.dtpFechaInicial.Text;
                                this.dgvEncabezado.Rows[a].Cells[5].Value = this.dtpFechaFinal.Text;



                                //
                            
                            
                            }



                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar una aseguradora", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);   
                        }                       
                    }
                    else
                    {
                        MessageBox.Show("Debe digitar un monto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);  
                    }
                }
                else
                {
                    MessageBox.Show("Le fecha final debe ser mayor que la fecha inicial", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None); 
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al realizar la consulta: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnImportar_Click(object sender, EventArgs e)
        {
            UserTable miTablaEncabezado = (UserTable)ClaseDatos.objCompany.UserTables.Item("CSS_REPORTE_ENCABEZ");
            UserTable miTablaDetalle = (UserTable)ClaseDatos.objCompany.UserTables.Item("CSS_REPORTE_DETALLE");
            try
            {
                string miSql = "SELECT Code,U_CSS_Fecha_Inicio,U_CSS_Fecha_Fin " +
                               "FROM [@CSS_REPORTE_ENCABEZ] " +
                               "WHERE U_CSS_Codigo_Asegura='" + this.cmbAseguradora.Text + "' " +
                               "AND ( '" + Convert.ToDateTime(this.dtpFechaInicial.Text).ToString("yyyyMMdd") + "' BETWEEN U_CSS_Fecha_Inicio AND U_CSS_Fecha_Fin OR " +
                               "'" + Convert.ToDateTime(this.dtpFechaFinal.Text).ToString("yyyyMMdd") + "' BETWEEN U_CSS_Fecha_Inicio AND U_CSS_Fecha_Fin)";
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                IDataReader miLector=ClaseDatos.procesaDataReader(miSql);
                bool miEstadoBusqueda = true;
                if (miLector.Read())
                {
                    DialogResult miDialogo = MessageBox.Show("Existe un reporte de ventas generado entre las fechas: " + miLector.GetValue(1).ToString().Substring(0, 10) + " y " + miLector.GetValue(2).ToString().Substring(0, 10) + Environment.NewLine +
                        "Desea Continuar", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (miDialogo != DialogResult.Yes)
                    {
                        miEstadoBusqueda = false;
                        MessageBox.Show("Ha cancelado la operación", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        this.EliminarDatos(miLector.GetValue(0).ToString()); 
                        while (miLector.Read())
                        {
                           this.EliminarDatos(miLector.GetValue(0).ToString());                             
                        }
                    }
                }
                ClaseDatos.SqlUnConnex();
                if (miEstadoBusqueda)
                {
                    for (int miContador = 0; miContador < miDataSet.Tables[0].Rows.Count; miContador++)
                    {
                        //Si se cumple la condición
                        DataRow[] misFilas = miDataSet.Tables[1].Select("Seleccione='True' AND CardCode='" + this.miDataSet.Tables[0].Rows[miContador][0].ToString() + "'");
                        if (misFilas.Length > 0)
                        {
                            //Almacenar valores del Encabezado                                        
                            miSql = "SELECT isnull(MAX(convert(int,Code)),0) as Contador FROM [@CSS_REPORTE_ENCABEZ]";
                            string miCodigoEncabezado = ClaseDatos.scalarStringSql(miSql);
                            miTablaEncabezado.Code = (Convert.ToInt32(miCodigoEncabezado) + 1).ToString();
                            miTablaEncabezado.Name = (Convert.ToInt32(miCodigoEncabezado) + 1).ToString();
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Codigo_SAP").Value = this.miDataSet.Tables[0].Rows[miContador][0].ToString();
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Nombre_Cliente").Value = this.miDataSet.Tables[0].Rows[miContador][1].ToString();
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Nit").Value = this.miDataSet.Tables[0].Rows[miContador][2].ToString();
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Cupo_Aprobado").Value = Convert.ToDouble(this.miDataSet.Tables[0].Rows[miContador][3].ToString());
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Fecha_Inicio").Value = Convert.ToDateTime(this.miDataSet.Tables[0].Rows[miContador][4].ToString());
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Fecha_Fin").Value = Convert.ToDateTime(this.miDataSet.Tables[0].Rows[miContador][5].ToString());
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Cliente_Asegur").Value = this.miDataSet.Tables[0].Rows[miContador][6].ToString();
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Codigo_Asegura").Value = this.cmbAseguradora.Text;
                            miTablaEncabezado.UserFields.Fields.Item("U_CSS_Saldo").Value = Convert.ToDouble(this.miDataSet.Tables[0].Rows[miContador][7].ToString());
                            miTablaEncabezado.Add();
                            //Almacenando los resultados asociados a los documentos
                            for (int miContadorInterno = 0; miContadorInterno < misFilas.Length; miContadorInterno++)
                            {
                                miSql = "SELECT isnull(MAX(convert(int,Code)),0) as Contador FROM [@CSS_REPORTE_DETALLE]";
                                string miCodigoDetalle = ClaseDatos.scalarStringSql(miSql);
                                miTablaDetalle.Code = (Convert.ToInt32(miCodigoDetalle) + 1).ToString();
                                miTablaDetalle.Name = (Convert.ToInt32(miCodigoDetalle) + 1).ToString();
                                miTablaDetalle.UserFields.Fields.Item("U_CSS_Codigo_SAP").Value = misFilas[miContadorInterno][1].ToString();
                                miTablaDetalle.UserFields.Fields.Item("U_CSS_Codigo_Asegura").Value = this.cmbAseguradora.Text.ToString();
                                miTablaDetalle.UserFields.Fields.Item("U_CSS_Numero_Documen").Value = misFilas[miContadorInterno][0].ToString();
                                miTablaDetalle.UserFields.Fields.Item("U_CSS_Saldo").Value = Convert.ToDouble(misFilas[miContadorInterno][3].ToString());
                                miTablaDetalle.UserFields.Fields.Item("U_CSS_Codigo_Encabez").Value = (Convert.ToInt32(miCodigoEncabezado) + 1).ToString();
                                miTablaDetalle.Add();
                            }
                        }
                    }
                    MessageBox.Show("Operación Realizada con Éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al almacenar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmReporteVentasAseguradora_Load(object sender, EventArgs e)
        {

            string miSql = "SELECT ' --Seleccione Aseguradora--' AS U_CSS_Codigo_Asegura,' --Seleccione Aseguradora--' AS  U_CSS_Nombre_Asegura "+
                           "UNION ALL " +
                           "SELECT DISTINCT(U_CSS_Codigo_Asegura),U_CSS_Nombre_Asegura " +
                           "FROM OCRD "+
                           "WHERE U_CSS_Codigo_Asegura<>'' "+
                           "ORDER BY 1";
            try
            {
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                DataSet miLector = ClaseDatos.procesaDataSet(miSql);               
                this.cmbAseguradora.DataSource = miLector.Tables[0];
                this.cmbAseguradora.ValueMember = "U_CSS_Codigo_Asegura";
                this.cmbAseguradora.DisplayMember = "U_CSS_Nombre_Asegura";
                this.btnImportar.Enabled = false;
                this.cmbAseguradora.SelectedIndex=this.cmbAseguradora.FindString(" --Seleccione Aseguradora--");
                ClaseDatos.SqlUnConnex();
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar las aseguradoras: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        
        {
            double miTotal = 0;
            string miSocio = "";
            string miSocio1 = e.ToString();
            for (int miContador = 0; miContador < this.dgvDetalle.Rows.Count; miContador++)
            {
                string a=this.dgvDetalle.Rows[miContador].Cells[6].Value.ToString();
                if (this.dgvDetalle.Rows[miContador].Cells[6].Value.ToString().Equals("True"))
                {
                    miTotal += Convert.ToDouble(this.dgvDetalle.Rows[miContador].Cells[3].Value);
                    miSocio = this.dgvDetalle.Rows[miContador].Cells[2].Value.ToString();
                   
                }
            }            
            for (int miContador = 0; miContador < this.dgvEncabezado.Rows.Count; miContador++)
            {
                if (miSocio.Equals(this.dgvEncabezado.Rows[miContador].Cells[0].Value.ToString()))
                {
                    this.dgvEncabezado.Rows[miContador].Cells[7].Value = miTotal;

                    this.dgvEncabezado.Rows[miContador].Cells[9].Value = (Convert.ToDouble(this.dgvEncabezado.Rows[miContador].Cells[7].Value) - Convert.ToDouble(this.dgvEncabezado.Rows[miContador].Cells[3].Value));
                  


                }
            }

            textBox2.Text = miTotal.ToString("C2");
            double sum = 0.0, suma = 0.0, suma1 = 0.0;
            for (int a = 0; a < dgvEncabezado.Rows.Count; a++)
            {


                sum += Convert.ToDouble(dgvEncabezado.Rows[a].Cells[7].Value.ToString());
                suma1 += Convert.ToDouble(dgvEncabezado.Rows[a].Cells[8].Value.ToString());
                suma += Convert.ToDouble(dgvEncabezado.Rows[a].Cells[9].Value.ToString());

                txtTotal.Text = sum.ToString("C2");
                txtTotal1.Text = suma.ToString("C2");
                textBox1.Text = suma1.ToString("C2");



            }




        }

        private void dgvDetalle_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvDetalle.IsCurrentCellDirty)
            {
                this.dgvDetalle.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void EliminarDatos(string unCodigo)
        {
            UserTable miTablaEncabezado = (UserTable)ClaseDatos.objCompany.UserTables.Item("CSS_REPORTE_ENCABEZ");
            UserTable miTablaDetalle = (UserTable)ClaseDatos.objCompany.UserTables.Item("CSS_REPORTE_DETALLE");            
            string miSql = "SELECT Code FROM [@CSS_REPORTE_DETALLE] " +
                         "WHERE U_CSS_Codigo_Encabez='" + unCodigo + "'";
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
            IDataReader miLector = ClaseDatos.procesaDataReader(miSql);
            while (miLector.Read())
            {
                miTablaDetalle.GetByKey(miLector.GetValue(0).ToString());
                miTablaDetalle.Remove();
            }
            ClaseDatos.SqlUnConnex();
            miTablaEncabezado.GetByKey(unCodigo);
            miTablaEncabezado.Remove();
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            double miTotal = 0;
          
            for (int miContador = 0; miContador < this.dgvDetalle.Rows.Count; miContador++)
            {
                string a = this.dgvDetalle.Rows[miContador].Cells[6].Value.ToString();
                if (this.dgvDetalle.Rows[miContador].Cells[6].Value.ToString().Equals("True"))
                {
                    miTotal += Convert.ToDouble(this.dgvDetalle.Rows[miContador].Cells[3].Value);
                   

                }
            }

            textBox2.Text = miTotal.ToString("C2");

        }
    }
}

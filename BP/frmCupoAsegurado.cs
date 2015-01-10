using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAPbobsCOM;
using System.IO;

namespace BP
{
    public partial class frmCupoAsegurado : Form
    {
        public frmCupoAsegurado()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void txtArchivo_MouseDown(object sender, MouseEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (!Directory.Exists(@"c:\Datos"))
            {
                Directory.CreateDirectory(@"c:\Datos");
            }
            openFileDialog1.InitialDirectory = @"c:\Datos";
            openFileDialog1.Filter = "Archivos de texto separados por tabulador (*.txt)|*.TXT";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.ToString() != "")
                {
                    this.txtArchivo.Text = openFileDialog1.FileName.ToString();
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtArchivo.Text.Length > 0)
                {
                    if (this.IsFileOpen(this.txtArchivo.Text))
                    {
                        //DataSet miDataSet = ClaseDatos.ImportaExcel("SELECT * FROM [Cupo Asegurado$]", txtArchivo.Text);
                        DataSet miDataSet = ClaseDatos.importTabFile(txtArchivo.Text);
                        this.dtgMatriz.DataSource = miDataSet.Tables[0];

                    }
                    else
                    {
                        MessageBox.Show("Cierre el archivo: " + this.txtArchivo.Text + " antes de intentar realizar la carga", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un archivo", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar el archivo: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

            long retval;
            string miSql = "";
            BusinessPartners miSocio = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            UserTable miTabla = (UserTable)ClaseDatos.objCompany.UserTables.Item("CSS_CUPO_ASEGURADOR");

            int filaActual = 0;
            try
            {
                if (ValidarExistenciaSocioNegocios())
                {
                    progressBar1.Maximum = this.dtgMatriz.Rows.Count;
                    for (int miContador = 0; miContador < this.dtgMatriz.Rows.Count; miContador++)
                    {
                        filaActual = miContador;
                        string feCA = "";


                        /*2014-08-26 sbernald - importacion de cupos con nit y no con codigo cliente*/
                        ClaseDatos.SqlUnConnex();
                        string socioSql = string.Format("Select CardCode from OCRD where LicTradNum = '{0}'", this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString());
                        string codigoCliente = ClaseDatos.scalarStringSql(socioSql);

                        //miSocio.GetByKey(this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString());
                        miSocio.GetByKey(codigoCliente);
                        miSocio.UserFields.Fields.Item("U_CSS_Cliente_Asegur").Value = this.dtgMatriz.Rows[miContador].Cells[1].Value.ToString();
                        miSocio.UserFields.Fields.Item("U_CSS_Fecha_Solicitu").Value = Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[3].Value.ToString());//.ToString("yyyyMMdd");
                        miSocio.UserFields.Fields.Item("U_CSS_Codigo_Asegura").Value = this.dtgMatriz.Rows[miContador].Cells[4].Value.ToString();
                        miSocio.UserFields.Fields.Item("U_CSS_Nombre_Asegura").Value = this.dtgMatriz.Rows[miContador].Cells[5].Value.ToString();
                        miSocio.UserFields.Fields.Item("U_CSS_CupoSolicitado").Value = Convert.ToDouble(this.dtgMatriz.Rows[miContador].Cells[6].Value.ToString());
                        miSocio.UserFields.Fields.Item("U_CSS_Estado_Solicit").Value = this.dtgMatriz.Rows[miContador].Cells[7].Value.ToString();
                        miSocio.UserFields.Fields.Item("U_CSS_CupoAprobado").Value = Convert.ToDouble(this.dtgMatriz.Rows[miContador].Cells[8].Value.ToString());
                        miSocio.UserFields.Fields.Item("U_CSS_Fecha_Ini_Vige").Value = Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[9].Value.ToString());//.ToString("yyyyMMdd");
                        miSocio.UserFields.Fields.Item("U_CSS_Fecha_Fin_Vige").Value = Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[10].Value.ToString());//.ToString("yyyyMMdd");
                        miSocio.UserFields.Fields.Item("U_CSS_Causal_Rechazo").Value = this.dtgMatriz.Rows[miContador].Cells[11].Value.ToString();
                        feCA = Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[9].Value.ToString()).ToString("yyyyMMdd");

                        retval = miSocio.Update();

                        if (retval < 0 || retval > 0)
                        {
                            Int32 ErrCode = 0;
                            string ErrMensaje = "";

                            ClaseDatos.objCompany.GetLastError(out ErrCode, out ErrMensaje);
                            //MessageBox.Show("SAP- El Socio: " + this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString() + " presenta el siguiente error: " + ErrMensaje, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string errorSistema = string.Format("el socio {0} presenta el siguiente error: {1} ", this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString(), ErrMensaje);
                            MessageBox.Show(errorSistema, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            File.WriteAllText("C:\\LogCupoAsegurado\\log.csv", errorSistema);
                        }

                        //#region update_confirma fECHAS
                        ////Update Jose walter Santamaria Para re confirmar fechas en la tabla
                        //string card_update = "update OCRD set U_CSS_Fecha_Fin_Vige ='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[10].Value.ToString()).ToString("yyyyMMdd") +
                        //    "',U_CSS_Fecha_Ini_Vige ='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[9].Value.ToString()).ToString("yyyy-MM-dd") +
                        //    "',U_CSS_Fecha_Solicitu ='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[3].Value.ToString()).ToString("yyyy-MM-dd") +
                        //    "',u_css_fechasolicitud='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[3].Value.ToString()).ToString("yyyy-MM-dd") +
                        //    "' where CARDCODE ='" + this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString() + "'";

                        //ClaseDatos.nonQuery(card_update);
                        //#endregion

                        miSql = "SELECT Code FROM [@CSS_CUPO_ASEGURADOR] " +
                            //"WHERE U_CSS_Cliente='" + this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString() + "' " +
                            "WHERE U_CSS_Cliente='" + codigoCliente + "' " +
                            "AND U_CSS_Codigo_Asegura='" + this.dtgMatriz.Rows[miContador].Cells[4].Value.ToString() + "' " +
                            "AND U_CSS_Fecha_Solicitu='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[3].Value.ToString()).ToString("yyyy-MM-dd") + "'";
                        ClaseDatos.SqlUnConnex();
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);

                        IDataReader miCantidadEncontrada = ClaseDatos.procesaDataReader(miSql);
                        bool miEstadoEncontrado = false;
                        if (miCantidadEncontrada.Read())
                        {
                            miTabla.GetByKey(miCantidadEncontrada[0].ToString());
                            ClaseDatos.SqlUnConnex();
                            miEstadoEncontrado = true;
                        }
                        else
                        {
                            ClaseDatos.SqlUnConnex();
                            miSql = "SELECT isnull(MAX(convert(int,Code)),0) as Contador FROM [@CSS_CUPO_ASEGURADOR]";
                            string miCodigo = ClaseDatos.scalarStringSql(miSql);
                            miTabla.Code = (Convert.ToInt32(miCodigo) + 1).ToString();
                            miTabla.Name = (Convert.ToInt32(miCodigo) + 1).ToString();
                        }
                        miTabla.UserFields.Fields.Item("U_CSS_CupoSolicitado").Value = Convert.ToDouble(this.dtgMatriz.Rows[miContador].Cells[6].Value.ToString());
                        miTabla.UserFields.Fields.Item("U_CSS_CupoAprobado").Value = Convert.ToDouble(this.dtgMatriz.Rows[miContador].Cells[8].Value.ToString());
                        miTabla.UserFields.Fields.Item("U_CSS_Estado_Solicit").Value = this.dtgMatriz.Rows[miContador].Cells[7].Value.ToString();
                        miTabla.UserFields.Fields.Item("U_CSS_Fecha_Solicitu").Value = Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[3].Value.ToString());//.ToString("yyyyMMdd");
                        miTabla.UserFields.Fields.Item("U_CSS_Fecha_Ini_Vige").Value = Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[9].Value.ToString());//.ToString("yyyyMMdd");
                        miTabla.UserFields.Fields.Item("U_CSS_Fecha_Fin_Vige").Value = Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[10].Value.ToString());//.ToString("yyyyMMdd");
                        miTabla.UserFields.Fields.Item("U_CSS_Codigo_Asegura").Value = this.dtgMatriz.Rows[miContador].Cells[4].Value.ToString();
                        miTabla.UserFields.Fields.Item("U_CSS_Nombre_Asegura").Value = this.dtgMatriz.Rows[miContador].Cells[5].Value.ToString();
                        miTabla.UserFields.Fields.Item("U_CSS_Causal_Rechazo").Value = this.dtgMatriz.Rows[miContador].Cells[11].Value.ToString();
                        miTabla.UserFields.Fields.Item("U_CSS_Cliente").Value = codigoCliente;
                        //miTabla.UserFields.Fields.Item("U_CSS_Cliente").Value = this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString();
                        //miTabla.UserFields.Fields.Item("CSS_Nit_Cliente").Value = this.dtgMatriz.Rows[miContador].Cells[12].Value.ToString();


                        if (miEstadoEncontrado)
                        {
                            miTabla.Update();
                        }
                        else
                        {
                            miTabla.Add();
                        }

                        #region update_confirma fECHAS
                        ////Update Jose walter Santamaria Para re confirmar fechas en la tabla
                        //string w_update = "update [@CSS_CUPO_ASEGURADOR] set U_CSS_Fecha_Fin_Vige ='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[10].Value.ToString()).ToString("yyyyMMdd") +
                        //    "',U_CSS_Fecha_Ini_Vige ='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[9].Value.ToString()).ToString("yyyyMMdd") + "',U_CSS_Fecha_Solicitu ='" + Convert.ToDateTime(this.dtgMatriz.Rows[miContador].Cells[3].Value.ToString()).ToString("yyyyMMdd") +
                        //    "' where CODE =" + miTabla.Code;

                        //ClaseDatos.nonQuery(w_update);
                        #endregion
                        progressBar1.Value = miContador;

                    }
                    MessageBox.Show("Operación Realizada con Éxito. ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBox.Show("Se han encontrado Socios de Negocios que no existen en SAP " + Environment.NewLine + "Por favor revise el archivo C:\\LogCupoAsegurado\\log.csv", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al importar el archivo en la linea " + filaActual + ": " + miExcepcion.Message.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(miSocio);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(miTabla);
            }
        }

        private bool ValidarExistenciaSocioNegocios()
        {
            BusinessPartners miSocio = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            bool miEstadoValidacion = true;
            try
            {
                File.WriteAllText("C:\\LogCupoAsegurado\\log.csv", "CLIENTE,MENSAJE");
            }
            catch (IOException miExcepcion)
            {
                throw new Exception(miExcepcion.Message + Environment.NewLine + "Cierre el archivo antes de importar datos");
            }
            for (int miContador = 0; miContador < this.dtgMatriz.Rows.Count; miContador++)
            {

                if (this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString().Length > 0)
                {
                    /*2014-08-26 sbernald - importacion de cupos con nit y no con codigo cliente*/
                    ClaseDatos.SqlUnConnex();
                    string socioSql = string.Format("Select CardCode from OCRD where LicTradNum = '{0}'", this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString());
                    string codigoCliente = ClaseDatos.scalarStringSql(socioSql);

                    //if (!miSocio.GetByKey(this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString()))
                    if (!miSocio.GetByKey(codigoCliente))
                    {
                        if (!Directory.Exists("C:\\LogCupoAsegurado\\"))
                        {
                            Directory.CreateDirectory("C:\\LogCupoAsegurado\\");
                        }
                        if (!File.Exists("C:\\LogCupoAsegurado\\log.csv"))
                        {
                            File.AppendAllText("C:\\LogCupoAsegurado\\log.csv", "CLIENTE,MENSAJE,LÍNEA");
                        }
                        File.AppendAllText("C:\\LogCupoAsegurado\\log.csv", Environment.NewLine + this.dtgMatriz.Rows[miContador].Cells[0].Value.ToString() + "," + "No existe en SAP , " + miContador.ToString());
                        miEstadoValidacion = false;
                    }
                }
            }
            return miEstadoValidacion;
        }

        private bool IsFileOpen(string unPath)
        {
            bool miResultado = true;
            try
            {
                FileStream miArchivo;
                miArchivo = File.Open(unPath, System.IO.FileMode.Open,
                System.IO.FileAccess.Read, System.IO.FileShare.None);
                miArchivo.Close();

            }
            catch (Exception miExcepcion)
            {
                miResultado = false;
            }
            return miResultado;
        }
    }
}

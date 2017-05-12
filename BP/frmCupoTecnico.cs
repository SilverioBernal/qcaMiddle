using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using SAPbobsCOM;

namespace BP
{
    public partial class frmCupoTecnico : Form
    {
        public frmCupoTecnico()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "XLS files (*.xls)|*.XLS";
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
            if (this.txtArchivo.Text.Length > 0)
            {
              ApplicationClass miexcelApp = new ApplicationClass();
                try
                {
                    this.dtgOtrosValores.Rows.Clear();
                    //Crea el objeto workbook abriendo el archivo de excel.
                    Workbook workBook = miexcelApp.Workbooks.Open(this.txtArchivo.Text, 0, true, 5, "", "", true,
                        XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);
                    //Obtiene el worksheet activo usando el nombre o el sheet activo
                    Worksheet miWorkSheet = (Worksheet)workBook.Sheets[1];
                    //Campos encabezado
                    this.txtEmpresa.Text = this.ValidarDatoExcel(((Range)miWorkSheet.Cells[2, 3]),"String");
                    this.txtCliente.Text = this.ValidarDatoExcel(((Range)miWorkSheet.Cells[3, 3]), "String");
                    TimeSpan miFechaExcel = new TimeSpan(Convert.ToInt32(this.ValidarDatoExcel((Range)miWorkSheet.Cells[4, 3],"Numerico")) - 2, 0, 0, 0);
                    DateTime miFechaReal = new DateTime(1900, 1, 1).Add(miFechaExcel);
                    this.txtFecha.Text = miFechaReal.ToString("yyyy-MM-dd");
                    miFechaExcel = new TimeSpan(Convert.ToInt32(this.ValidarDatoExcel(((Range)miWorkSheet.Cells[5, 3]),"Numerico")) - 2, 0, 0, 0);
                    miFechaReal = new DateTime(1900, 1, 1).Add(miFechaExcel);
                    this.txtBalance.Text = miFechaReal.ToString("yyyy-MM-dd");
                    //Campos                     
                    this.txtCuentasXCobrar.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[9, 4],"Numerico")).ToString("0,0.00");
                    this.txtActivoCorriente.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[10, 4],"Numerico")).ToString("0,0.00");
                    this.txtActivoTotal.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[11, 4],"Numerico")).ToString("0,0.00");
                    this.txtCuentasXPagar.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[12, 4], "Numerico")).ToString("0,0.00");
                    this.txtPasivoCorriente.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[13, 4], "Numerico")).ToString("0,0.00");
                    this.txtPasivoTotal.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[14, 4], "Numerico")).ToString("0,0.00");
                    this.txtVentas.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[15, 4], "Numerico")).ToString("0,0.00");
                    this.txtCostoVentas.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[16, 4], "Numerico")).ToString("0,0.00");
                    this.txtUtilidad.Text = Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[17, 4], "Numerico")).ToString("0,0.00");
                    //Campos Trayectoria
                    this.txtTipoCliente.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[22, 3],"String");
                    this.txtAntiguedadTrayectoria.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[24, 3],"String");
                    this.txtPuntosTipoCliente.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[22, 5],"String");
                    this.txtPuntosAntiguedad.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[24, 5],"String");
                    //Campos Referencias Comerciales
                    this.txtCuposOtorgados.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[28, 3],"String");
                    this.txtPuntosCupos.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[28, 5],"String");
                    this.txtAntiguedadReferencia.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[30, 3],"String");
                    this.txtPuntosAntiguedadReferencia.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[30, 5],"String");
                    //Campos Indices Económicos
                    this.txtLiquidez.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[34, 3],"String");
                    this.txtEndeudamiento.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[35, 3],"String");
                    this.txtRotacionCartera.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[36, 3],"String");
                    this.txtRotacionProveedores.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[37, 3],"String");
                    this.txtEficienciaGeneral.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[38, 3],"String");
                    this.txtPuntosLiquidez.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[34, 5],"String");
                    this.txtPuntosEndeudamiento.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[35, 5],"String");
                    this.txtPuntosRotacionCartera.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[36, 5],"String");
                    this.txtPuntosRotacionProvedores.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[37, 5],"String");
                    this.txtPuntosEficiencia.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[38, 5],"String");
                    //Campos Totales
                    this.txtTotalPuntos.Text = this.ValidarDatoExcel((Range)miWorkSheet.Cells[40, 5],"String");
                    this.txtCapitalTrabajo.Text = "$ " + Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[41, 5],"Numerico")).ToString("0,0.00");
                    this.txtCupoTecnico.Text = "$ " + Convert.ToDouble(this.ValidarDatoExcel((Range)miWorkSheet.Cells[42, 5],"Numerico")).ToString("0,0.00");
                    //Campos Adicionales
                    this.dtgOtrosValores.Rows.Add(this.ValidarDatoExcel((Range)miWorkSheet.Cells[55, 1],"String"), this.ValidarDatoExcel((Range)miWorkSheet.Cells[56, 1],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[57, 1],"String"), 
                        this.ValidarDatoExcel((Range)miWorkSheet.Cells[55, 2],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[56, 2],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[57, 2],"String"),
                        this.ValidarDatoExcel((Range)miWorkSheet.Cells[55, 3],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[56, 3],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[57, 3],"String"),
                        this.ValidarDatoExcel((Range)miWorkSheet.Cells[55, 4],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[56, 4],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[57, 4],"String"),
                        this.ValidarDatoExcel((Range)miWorkSheet.Cells[55, 5],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[56, 5],"String"),this.ValidarDatoExcel((Range)miWorkSheet.Cells[57, 5],"String"));
                    workBook.Close(false, Missing.Value, Missing.Value);
                    miexcelApp.Quit();
                    workBook = null;
                    miWorkSheet = null;             
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(miexcelApp);                    
                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar el archivo: " + miExcepcion.Message+" LA PILA ES ESTA: "+miExcepcion.StackTrace, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                finally
                {
                    System.GC.Collect();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un archivo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            BusinessPartners miSocioNegocios = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            UserTable miTabla = ClaseDatos.objCompany.UserTables.Item("CSS_CUPO_TECNICO");
            string miSQL = "";
            try
            {
                if (this.txtEmpresa.Text.Length > 0)
                {
                    if (miSocioNegocios.GetByKey(this.txtCliente.Text))
                    {
                        bool miEstadoValidacion = true;
                        if (Convert.ToDateTime(miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaElaboraci").Value) > Convert.ToDateTime(this.txtFecha.Text))
                        {
                            DialogResult miMensaje = MessageBox.Show("El valor almacenado para el socio de negocios tiene una fecha posterior a la que esta cargando " +
                                Environment.NewLine +
                                "Esta seguro de realizar el importe de datos", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (!miMensaje.Equals(DialogResult.OK))
                            {
                                miEstadoValidacion = false;
                                MessageBox.Show("Ha cancelado la importación de datos", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                        }
                        else
                        {
                            if (miEstadoValidacion)
                            {
                                miSQL="SELECT Count(*) "+
                                      "FROM [@CSS_CUPO_TECNICO] "+
                                      "WHERE U_CSS_FechaElaboraci='"+ this.txtFecha.Text+"' " +
                                      "AND U_CSS_Cliente='" + this.txtCliente.Text+"'";
                                string existe = "";
                                existe = ClaseDatos.scalarStringSql(miSQL);
                                if(Convert.ToInt32(existe)>0)
                                {
                                    miSQL = "SELECT Code " +
                                            "FROM [@CSS_CUPO_TECNICO] " +
                                            "WHERE U_CSS_FechaElaboraci='" + this.txtFecha.Text + "' " +
                                            "AND U_CSS_Cliente='" + this.txtCliente.Text+"'";
                                    string miCode = ClaseDatos.scalarStringSql(miSQL);
                                    miTabla.GetByKey(miCode);
                                }
                                else
                                {                                
                                    miSQL = "SELECT isnull(MAX(convert(int,Code)),0) as Contador FROM [@CSS_CUPO_TECNICO]";
                                    string miCodigo = ClaseDatos.scalarStringSql(miSQL);
                                    miTabla.Code = (Convert.ToInt32(miCodigo) + 1).ToString();
                                    miTabla.Name = (Convert.ToInt32(miCodigo) + 1).ToString();
                                }
                                //Encabezado
                                miTabla.UserFields.Fields.Item("U_CSS_Empresa").Value = this.txtEmpresa.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_Cliente").Value = this.txtCliente.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_FechaElaboraci").Value = Convert.ToDateTime(this.txtFecha.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_FechaBalance").Value = Convert.ToDateTime(this.txtBalance.Text);
                                //Valores                                              
                                miTabla.UserFields.Fields.Item("U_CSS_CXC").Value = Convert.ToDouble(this.txtCuentasXCobrar.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_ActivoCorrient").Value = Convert.ToDouble(this.txtActivoCorriente.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_ActivoTotal").Value = Convert.ToDouble(this.txtActivoTotal.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_CXP").Value = Convert.ToDouble(this.txtCuentasXPagar.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_PasivoCorrient").Value = Convert.ToDouble(this.txtPasivoCorriente.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_PasivoTotal").Value = Convert.ToDouble(this.txtPasivoTotal.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_Ventas").Value = Convert.ToDouble(this.txtVentas.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_CostoVentas").Value = Convert.ToDouble(this.txtCostoVentas.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_Utilidad").Value = Convert.ToDouble(this.txtUtilidad.Text);
                                //Trayectoria y Tipo
                                miTabla.UserFields.Fields.Item("U_CSS_TipoCliente").Value = this.txtTipoCliente.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_AntiguedadTray").Value = this.txtAntiguedadTrayectoria.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosTC").Value = this.txtPuntosTipoCliente.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosAntTraye").Value = this.txtPuntosAntiguedad.Text;
                                //Referencias Comerciales
                                miTabla.UserFields.Fields.Item("U_CSS_CuposOtorgados").Value = this.txtCuposOtorgados.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosCuposOto").Value = this.txtPuntosCupos.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_AntiguedadRefe").Value = this.txtAntiguedadReferencia.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosAntRC").Value = this.txtPuntosAntiguedadReferencia.Text;
                                //Indices Económicos
                                miTabla.UserFields.Fields.Item("U_CSS_Liquidez").Value = Convert.ToDouble(this.txtLiquidez.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_Endeudamiento").Value = Convert.ToDouble(this.txtEndeudamiento.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_RotacionCarter").Value = this.txtRotacionCartera.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_RotacionProvee").Value = this.txtRotacionProveedores.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_Eficiencia").Value = Convert.ToDouble(this.txtEficienciaGeneral.Text);
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosLiquidez").Value = this.txtPuntosLiquidez.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosEndeudam").Value = this.txtPuntosEndeudamiento.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosRotacion").Value = this.txtPuntosRotacionCartera.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosRotProve").Value = this.txtPuntosRotacionProvedores.Text;
                                miTabla.UserFields.Fields.Item("U_CSS_PuntosEficienc").Value = this.txtPuntosEficiencia.Text;
                                //Totales
                                miTabla.UserFields.Fields.Item("U_CSS_TotalPuntos").Value = this.txtTotalPuntos.Text;                                
                                miTabla.UserFields.Fields.Item("U_CSS_CapitalTrabajo").Value = Convert.ToDouble(this.txtCapitalTrabajo.Text.Substring(2, this.txtCapitalTrabajo.Text.Length - 2));
                                miTabla.UserFields.Fields.Item("U_CSS_CupoTecnico").Value = Convert.ToDouble(this.txtCupoTecnico.Text.Substring(2, this.txtCupoTecnico.Text.Length - 2));
                                //Campos finales del formato A55 a E57
                                miTabla.UserFields.Fields.Item("U_CSS_A55").Value = this.dtgOtrosValores.Rows[0].Cells[0].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_A56").Value = this.dtgOtrosValores.Rows[0].Cells[1].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_A57").Value = this.dtgOtrosValores.Rows[0].Cells[2].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_B55").Value = this.dtgOtrosValores.Rows[0].Cells[3].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_B56").Value = this.dtgOtrosValores.Rows[0].Cells[4].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_B57").Value = this.dtgOtrosValores.Rows[0].Cells[5].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_C55").Value = this.dtgOtrosValores.Rows[0].Cells[6].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_C56").Value = this.dtgOtrosValores.Rows[0].Cells[7].Value;
                                miTabla.UserFields.Fields.Item("U_CSS_C57").Value = Convert.ToDouble(this.dtgOtrosValores.Rows[0].Cells[8].Value);
                                miTabla.UserFields.Fields.Item("U_CSS_D55").Value = Convert.ToDouble(this.dtgOtrosValores.Rows[0].Cells[9].Value);
                                miTabla.UserFields.Fields.Item("U_CSS_D56").Value = Convert.ToDouble(this.dtgOtrosValores.Rows[0].Cells[10].Value);
                                miTabla.UserFields.Fields.Item("U_CSS_D57").Value = Convert.ToDouble(this.dtgOtrosValores.Rows[0].Cells[11].Value);
                                miTabla.UserFields.Fields.Item("U_CSS_E55").Value = Convert.ToDouble(this.dtgOtrosValores.Rows[0].Cells[12].Value);
                                miTabla.UserFields.Fields.Item("U_CSS_E56").Value = Convert.ToDouble(this.dtgOtrosValores.Rows[0].Cells[13].Value);
                                miTabla.UserFields.Fields.Item("U_CSS_E57").Value = Convert.ToDouble(this.dtgOtrosValores.Rows[0].Cells[14].Value);
                                if (Convert.ToInt32(existe) == 0)
                                {
                                    miTabla.Add();
                                }
                                else
                                {
                                    miTabla.Update(); 
                                }                                

                                miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaElaboraci").Value = Convert.ToDateTime(this.txtFecha.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaBalance").Value = Convert.ToDateTime(this.txtBalance.Text);
                                //Valores
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_CXC").Value = Convert.ToDouble(this.txtCuentasXCobrar.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_ActivoCorrient").Value = Convert.ToDouble(this.txtActivoCorriente.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_ActivoTotal").Value = Convert.ToDouble(this.txtActivoTotal.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_CXP").Value = Convert.ToDouble(this.txtCuentasXPagar.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PasivoCorrient").Value = Convert.ToDouble(this.txtPasivoCorriente.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PasivoTotal").Value = Convert.ToDouble(this.txtPasivoTotal.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_Ventas").Value = Convert.ToDouble(this.txtVentas.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_CostoVentas").Value = Convert.ToDouble(this.txtCostoVentas.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_Utilidad").Value = Convert.ToDouble(this.txtUtilidad.Text);
                                //Trayectoria y Tipo
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_TipoCliente").Value = this.txtTipoCliente.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_AntiguedadT").Value = this.txtAntiguedadTrayectoria.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosTC").Value = this.txtPuntosTipoCliente.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosAntTray").Value = this.txtPuntosAntiguedad.Text;
                                //Referencias Comerciales
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_CuposOtorgados").Value = this.txtCuposOtorgados.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosCuposOto").Value = this.txtPuntosCupos.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_AntiguedadRef").Value = this.txtAntiguedadReferencia.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosAntRC").Value = this.txtPuntosAntiguedadReferencia.Text;
                                //Indices Económicos
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_Liquidez").Value = Convert.ToDouble(this.txtLiquidez.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_Endeudamiento").Value = Convert.ToDouble(this.txtEndeudamiento.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_RotacionCarter").Value = this.txtRotacionCartera.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_RotacionProvee").Value = this.txtRotacionProveedores.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_Eficiencia").Value = Convert.ToDouble(this.txtEficienciaGeneral.Text);
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosLiquidez").Value = this.txtPuntosLiquidez.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosEndeudam").Value = this.txtPuntosEndeudamiento.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosRotacion").Value = this.txtPuntosRotacionCartera.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosRotProve").Value = this.txtPuntosRotacionProvedores.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_PuntosEficienc").Value = this.txtPuntosEficiencia.Text;
                                //Totales
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_TotalPuntos").Value = this.txtTotalPuntos.Text;
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_CapitalTrabajo").Value = Convert.ToDouble(this.txtCapitalTrabajo.Text.Substring(2, this.txtCapitalTrabajo.Text.Length - 2));
                                miSocioNegocios.UserFields.Fields.Item("U_CSS_CupoTecnico").Value = Convert.ToDouble(this.txtCupoTecnico.Text.Substring(2, this.txtCupoTecnico.Text.Length - 2));
                                miSocioNegocios.Update();
                                MessageBox.Show("Operación Finalizada con Éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                            }
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("El socio de negocios no esta registrado en SAP", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);  
                    }
                }
                else
                {
                    MessageBox.Show("Debe cargar un archivo primero", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None); 
                }

            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al importar valores: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ValidarDatoExcel(Range unRangoExcel, string unTipo)
        {
            string miResultado = "";
            switch (unTipo)
            {
                case "String": miResultado = "";
                    break;
                case "Numerico": miResultado = "0";
                    break;                
                default: miResultado="";
                    break;
            }           
            if (unRangoExcel.Value2 != null)
            {                
                miResultado = unRangoExcel.Value2.ToString(); 
            }
            return miResultado;
        }              
    }
}

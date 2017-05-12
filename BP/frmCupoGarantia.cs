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
    public partial class frmCupoGarantia : Form
    {
        public frmCupoGarantia()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmCupoGarantia_Load(object sender, EventArgs e)
        {
            CargaFormularioInicial();           
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (!this.cmbTercero.Text.Equals(" --Seleccione Codigo--"))
            {                  
                if (!this.cmbTipoGarantia.Text.Equals(" --Seleccione Tipo--"))
                {
                    if ((this.txtDescripcion.Text.Length != 0))
                    {
                        if (this.txNumeroDocumento.Text.Length > 0 || (!this.chkPagare.Checked))
                        {
                            //UserTable miTabla = ClaseDatos.objCompany.UserTables.Item("CSS_CUPO_GARANTIA");
                            try
                            {
                                if (this.dtpFechaFin.Value > this.dtpFechaInicio.Value || (this.chkPagare.Checked))
                                {
                                    string miSQL = "SELECT COUNT(*) " +
                                                   "FROM [@CSS_CUPO_GARANTIA] " +
                                                   "WHERE U_CSS_Numero_Documen='" + this.txNumeroDocumento.Text + "' " +
                                                   "AND U_CSS_Cliente='" + this.cmbTercero.Text + "' " +
                                                   "AND U_CSS_Estado='Activo'";
                                    int miEncontrado = ClaseDatos.scalarIntSql(miSQL);
                                    if (miEncontrado == 0)
                                    {
                                        if (this.cmbTipoGarantia.Text.Equals("Real") && this.chkPagare.Checked)
                                        {
                                            MessageBox.Show("No se puede ingresar documentos de real que sean pagarés", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            if ((this.mtbCupo.Text.Length == 0 || this.mtbCupo.Text.Equals("0")) && !this.chkPagare.Checked)
                                            {
                                                MessageBox.Show("Debe digitar un valor cuando el documento no es un pagaré", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                miSQL = "SELECT isnull(MAX(convert(int,Code)),0) as Contador FROM [@CSS_CUPO_GARANTIA]";
                                                string miCodigo = ClaseDatos.scalarStringSql(miSQL);
                                                string avalista = "";
                                                //miTabla.Code = (Convert.ToInt32(miCodigo) + 1).ToString();
                                                //miTabla.Name = (Convert.ToInt32(miCodigo) + 1).ToString();
                                                bool miValidacionPagare = true;
                                                if (!this.chkPagare.Checked)
                                                {
                                                    miSQL = "SELECT isnull(MAX(convert(int,U_CSS_Numero_Documen)),0) as Numero FROM [@CSS_CUPO_GARANTIA] WHERE U_CSS_Pagare='NO'";
                                                    miCodigo = ClaseDatos.scalarStringSql(miSQL);
                                                    //miTabla.UserFields.Fields.Item("U_CSS_Numero_Documen").Value = (Convert.ToInt32(miCodigo) + 1).ToString();
                                                    //miTabla.UserFields.Fields.Item("U_CSS_Pagare").Value = "NO";
                                                    //miTabla.UserFields.Fields.Item("U_CSS_Tiene_Avalista").Value = "NO";
                                               
                                                }
                                                else
                                                {
                                                   // miTabla.UserFields.Fields.Item("U_CSS_Pagare").Value = "SI";
                                                   // miTabla.UserFields.Fields.Item("U_CSS_Numero_Documen").Value = this.txNumeroDocumento.Text;

                                                    if (this.cmbAvalista.Text.Equals("-- Avalista--"))
                                                    {
                                                        miValidacionPagare = false;
                                                        MessageBox.Show("Seleccione si tiene Avalista", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
 
                                                    }
                                                    else if (this.cmbAvalista.Text.Equals("SI") && (this.txtAvalista.Text.Length > 0))
                                                    {
                                                      //  miTabla.UserFields.Fields.Item("U_CSS_Tiene_Avalista").Value = this.cmbAvalista.Text;  
                                                        avalista = "SI";
                                                    }
                                                    else if (!this.cmbAvalista.Text.Equals("NO"))
                                                    {
                                                        miValidacionPagare = false;
                                                        MessageBox.Show("Cuando selecciona que tiene avalista debe escribir el avalista correspondiente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None); 
                                                    }
                                                   // miTabla.UserFields.Fields.Item("U_CSS_Tiene_Avalista").Value = this.cmbAvalista.Text;                                                        
                                                }
                                                if (miValidacionPagare)
                                                {
                                                    double _valorTotal = 0.0;
                                                   
                                                    //miTabla.UserFields.Fields.Item("U_CSS_Fecha_Emision").Value = Convert.ToDateTime(this.dtpFechaInicio.Text);
                                                    //miTabla.UserFields.Fields.Item("U_CSS_Fecha_Vencimie").Value = Convert.ToDateTime(this.dtpFechaFin.Text);
                                                    if (this.mtbCupo.Text.Length == 0)
                                                    {
                                                      //  miTabla.UserFields.Fields.Item("U_CSS_Valor_Total").Value = Convert.ToDouble(0);
                                                        _valorTotal = 0.0;
                                                    }
                                                    else
                                                    {
                                                      //  miTabla.UserFields.Fields.Item("U_CSS_Valor_Total").Value = Convert.ToDouble(this.mtbCupo.Text);
                                                        _valorTotal = Convert.ToDouble(this.mtbCupo.Text);
                                                    }

                                                //    miTabla.UserFields.Fields.Item("U_CSS_Descripcion").Value = this.txtDescripcion.Text;
                                                //    miTabla.UserFields.Fields.Item("U_CSS_Cliente").Value = this.cmbTercero.Text;
                                                //    miTabla.UserFields.Fields.Item("U_CSS_Avalista").Value = this.txtAvalista.Text;
                                                //    miTabla.UserFields.Fields.Item("U_CSS_Estado").Value = "Activo";
                                                //    miTabla.UserFields.Fields.Item("U_CSS_Tipo_Garantia").Value = this.cmbTipoGarantia.Text; 
                                                   
                                                

                                                    string insert = "insert into  [@CSS_CUPO_GARANTIA]( Code,Name ,U_CSS_Avalista ,U_CSS_Cliente,U_CSS_Descripcion ,U_CSS_Estado ,U_CSS_Fecha_Emision , "
                                                   + " U_CSS_Fecha_Vencimie , U_CSS_Numero_Documen ,U_CSS_Pagare ,U_CSS_Tiene_Avalista,U_CSS_Tipo_Garantia ,U_CSS_Valor_Total, U_CSS_Nit_Cliente ) "
                                                   + " values(" + (Convert.ToInt32(miCodigo) + 1).ToString() + ",'" + (Convert.ToInt32(miCodigo) + 1).ToString() +"','" + this.txtAvalista.Text + "','" + this.cmbTercero.Text + "','" + this.txtDescripcion.Text
                                                   +" ','Activo','" + this.dtpFechaInicio.Text + "','" + this.dtpFechaFin.Text + 
                                                   "','"+this.txNumeroDocumento.Text+"','NO','"+this.cmbAvalista.Text +"','"+this.cmbTipoGarantia.Text+"'," + _valorTotal + ", '" + txtNitTercero.Text +"')";
                                                   
                                                    ClaseDatos.nonQuery(insert);

                                                    //


                                                    //int miResultado = miTabla.Add();
                                                    //if (miResultado != 0)
                                                    //{
                                                    //    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                                                    //    throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                                                    //}
                                                    this.ActualizarSocioNegocios();
                                                    if (this.chkPagare.Checked)
                                                    {
                                                        MessageBox.Show("Operación realizada con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Operación realizada con éxito " + Environment.NewLine + "Se ha creado el documento con el número: " + (Convert.ToInt32(miCodigo) + 1).ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("El documento ya existe para el cliente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("La fecha final debe ser mayor a la fecha inicial", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception miExcepcion)
                            {
                                MessageBox.Show("Error al almacenar el certificado: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtDescripcion.Text = miExcepcion.StackTrace;
                            }
                            finally
                            {
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(miTabla);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe digitar un número de documento cuando el la garantía es de tipo pagaré", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor escriba una descripción", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un tipo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                    
            }
            else
            {
                MessageBox.Show("Por favor seleccione un cliente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                   
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (this.txNumeroDocumento.Text.Length > 0)
            {
                if (!this.cmbTercero.Text.Equals(" -- Seleccione Codigo--"))
                {
                    string miSql = "SELECT U_CSS_Avalista,U_CSS_Tipo_Garantia,U_CSS_Fecha_Emision,U_CSS_Fecha_Vencimie,U_CSS_Valor_Total,U_CSS_Descripcion,U_CSS_Pagare,U_CSS_Tiene_Avalista, U_CSS_Nit_Cliente " +
                                   "FROM [@CSS_CUPO_GARANTIA] " +
                                   "WHERE U_CSS_Cliente='" + this.cmbTercero.Text + "' " +
                                   "AND U_CSS_Numero_Documen='" + this.txNumeroDocumento.Text + "' "+
                                   "AND U_CSS_Estado='Activo'";                                   
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                    IDataReader miLector = ClaseDatos.procesaDataReader(miSql);
                    bool miRegistroEncontrado = false;
                    while (miLector.Read())
                    {
                        miRegistroEncontrado = true;
                        this.txtAvalista.Text = miLector.GetValue(0).ToString();
                        this.cmbTipoGarantia.Text = miLector.GetValue(1).ToString();
                        this.dtpFechaInicio.Value = Convert.ToDateTime(miLector.GetValue(2));
                        this.dtpFechaFin.Value = Convert.ToDateTime(miLector.GetValue(3));
                        this.mtbCupo.Text = Convert.ToDouble(miLector.GetValue(4).ToString()).ToString();
                        this.txtDescripcion.Text = miLector.GetValue(5).ToString();
                        this.txtNitTercero.Text = miLector.GetValue(8).ToString();
                        if (miLector.GetValue(6).ToString().Equals("SI"))
                        {
                            this.chkPagare.Checked = true;
                        }
                        else
                        {
                            this.chkPagare.Checked = false; 
                        }                        
                        this.cmbAvalista.Text = miLector.GetValue(7).ToString();
                    }
                    ClaseDatos.SqlUnConnex();
                    if (!miRegistroEncontrado)
                    {
                        MessageBox.Show("No existe el número del documento para el cliente o se encuentra anulado", "Mensaje número del documento para el cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.btnEliminar.Enabled = true;
                        this.btnActualizar.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un cliente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
            }
            else
            {
                MessageBox.Show("Por favor digite el Número del documento", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.txNumeroDocumento.Text.Length > 0)
            {
                UserTable miTabla = ClaseDatos.objCompany.UserTables.Item("CSS_CUPO_GARANTIA");
                try
                {
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                    string miSQL = "SELECT Code FROM [@CSS_CUPO_GARANTIA] "+
                                   "WHERE U_CSS_Numero_Documen='" + this.txNumeroDocumento.Text + "' "+
                                   "AND U_CSS_Cliente='"+this.cmbTercero.Text+"' "+
                                   "AND U_CSS_Estado='Activo'";
                    IDataReader miLector = ClaseDatos.procesaDataReader(miSQL);
                    string miCodigo = "";
                    if (miLector.Read())
                    {
                        miCodigo = miLector.GetValue(0).ToString();
                    }                    
                    if (miCodigo.Length > 0)
                    {
                        miTabla.GetByKey(miCodigo);
                        ClaseDatos.SqlUnConnex();
                        miTabla.UserFields.Fields.Item("U_CSS_Estado").Value = "Anulado";
                        miTabla.Update();
                        BusinessPartners miSocioNegocios = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                        miSocioNegocios.GetByKey(this.cmbTercero.Text);
                        this.ActualizarSocioNegociosEliminar();                           
                        MessageBox.Show("El documento fue anulado con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El documento no existe o esta anulado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ClaseDatos.SqlUnConnex();
                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al borrar el documento: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(miTabla);
                }
            }
            else
            {
                MessageBox.Show("Por favor digite un certificado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
            }            
        }

        private double CalcularTotal()
        {
            string miSQL = "";
            miSQL = "SELECT SUM(U_CSS_Valor_Total) " +
                    "FROM dbo.[@CSS_CUPO_GARANTIA] " +
                    "WHERE '" + DateTime.Now.ToString("yyyyMMdd") + "' BETWEEN U_CSS_Fecha_Emision AND U_CSS_Fecha_Vencimie " +
                    "AND U_CSS_Cliente='" + this.cmbTercero.Text + "' " +
                    "AND U_CSS_Tipo_Garantia='"+this.cmbTipoGarantia.Text+"' "+
                    "AND U_CSS_Estado='Activo'";
            string miTotal = ClaseDatos.scalarStringSql(miSQL);
            if (miTotal.Length == 0)
            {
                miTotal = "0";
            }
            return Convert.ToDouble(miTotal);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!this.cmbTercero.Text.Equals(" --Seleccione Codigo--"))
            {
                if (!this.cmbAvalista.Text.Equals(" --Seleccione Codigo--"))
                {
                    if (!this.cmbTipoGarantia.Text.Equals(" --Seleccione Tipo--"))
                    {
                        if ((this.txtDescripcion.Text.Length != 0))
                        {
                            if (this.txNumeroDocumento.Text.Length > 0 && (this.chkPagare.Checked))
                            {
                                UserTable miTabla = ClaseDatos.objCompany.UserTables.Item("CSS_CUPO_GARANTIA");
                                try
                                {
                                    if (this.dtpFechaFin.Value > this.dtpFechaInicio.Value || (this.chkPagare.Checked))
                                    {
                                        if (this.cmbTipoGarantia.Text.Equals("Real") && this.chkPagare.Checked)
                                        {
                                            MessageBox.Show("No se puede ingresar documentos de real que sean pagarés", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            if ((this.mtbCupo.Text.Length == 0 || this.mtbCupo.Text.Equals("0")) && !this.chkPagare.Checked)
                                            {
                                                MessageBox.Show("Debe digitar un valor cuando el documento no es un pagaré", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                                                string miSQL = "SELECT Code,U_CSS_Tipo_Garantia FROM [@CSS_CUPO_GARANTIA] " +
                                                               "WHERE U_CSS_Numero_Documen='" + this.txNumeroDocumento.Text + "' " +
                                                               "AND U_CSS_Cliente='" + this.cmbTercero.Text + "'";
                                                IDataReader miLector = ClaseDatos.procesaDataReader(miSQL);
                                                string miCodigo = "";
                                                string miTipo = "";
                                                if (miLector.Read())
                                                {
                                                    miCodigo = miLector.GetValue(0).ToString();
                                                    miTipo = miLector.GetValue(1).ToString();
                                                }
                                                if (miCodigo.Length > 0)
                                                {
                                                    miTabla.GetByKey(miCodigo);
                                                    ClaseDatos.SqlUnConnex();
                                                    bool miValidacionPagare = true;
                                                    if (!this.chkPagare.Checked)
                                                    {
                                                        miTabla.UserFields.Fields.Item("U_CSS_Pagare").Value = "NO";
                                                        miTabla.UserFields.Fields.Item("U_CSS_Tiene_Avalista").Value = "NO";
                                                    }
                                                    else
                                                    {
                                                        miTabla.UserFields.Fields.Item("U_CSS_Pagare").Value = "SI";                                                        
                                                        if (this.cmbAvalista.Text.Equals("-- Avalista--"))
                                                        {
                                                            miValidacionPagare = false;
                                                            MessageBox.Show("Seleccione si tiene Avalista", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                        }
                                                        else if (this.cmbAvalista.Text.Equals("SI") && (this.txtAvalista.Text.Length > 0))
                                                        {
                                                            miTabla.UserFields.Fields.Item("U_CSS_Tiene_Avalista").Value = this.cmbAvalista.Text;
                                                        }
                                                        else if (!this.cmbAvalista.Text.Equals("NO"))
                                                        {
                                                            miValidacionPagare = false;
                                                            MessageBox.Show("Cuando selecciona que tiene avalista debe escribir el avalista correspondiente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                                                        }
                                                    }
                                                    if (miValidacionPagare)
                                                    {
                                                        miTabla.UserFields.Fields.Item("U_CSS_Fecha_Emision").Value = Convert.ToDateTime(this.dtpFechaInicio.Value);
                                                        miTabla.UserFields.Fields.Item("U_CSS_Fecha_Vencimie").Value = Convert.ToDateTime(this.dtpFechaFin.Value);
                                                        if (this.mtbCupo.Text.Length == 0)
                                                        {
                                                            miTabla.UserFields.Fields.Item("U_CSS_Valor_Total").Value = Convert.ToDouble(0);
                                                        }
                                                        else
                                                        {
                                                            miTabla.UserFields.Fields.Item("U_CSS_Valor_Total").Value = Convert.ToDouble(this.mtbCupo.Text);
                                                        }
                                                        miTabla.UserFields.Fields.Item("U_CSS_Avalista").Value = this.txtAvalista.Text;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Descripcion").Value = this.txtDescripcion.Text;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Cliente").Value = this.cmbTercero.Text;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Estado").Value = "Activo";
                                                        miTabla.UserFields.Fields.Item("U_CSS_Tipo_Garantia").Value = this.cmbTipoGarantia.Text;
                                                        //miTabla.UserFields.Fields.Item("U_CSS_Nit_Cliente").Value = this.txtNitTercero.Text;
                                                        int miResultado = miTabla.Update();
                                                        if (miResultado != 0)
                                                        {
                                                            ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                                                            throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                                                        }
                                                        this.ActualizarSocioNegociosActualizar(miTipo, this.cmbTipoGarantia.Text);
                                                        MessageBox.Show("Operación realizada con Éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);

                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("El documento no existe", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                ClaseDatos.SqlUnConnex();                                                    
                                            }
                                        }                                    
                                    }
                                    else
                                    {
                                        MessageBox.Show("La fecha final debe ser mayor a la fecha inicial", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                catch (Exception miExcepcion)
                                {
                                    MessageBox.Show("Error al almacenar el certificado: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(miTabla);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe digitar un número de documento", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor escriba una descripción", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione un tipo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }                
            }
            else
            {
                MessageBox.Show("Por favor seleccione un cliente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }             
        }

        private bool ActualizarSocioNegocios()
        {
            //BusinessPartners miSocioNegocios = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            //miSocioNegocios.GetByKey(this.cmbTercero.Text);

            string miSQL = "SELECT isnull(CARDCode,0) as Contador FROM OCRD WHERE CardCode ='" + this.cmbTercero.Text+"'";
            string miCodigo = ClaseDatos.scalarStringSql(miSQL);
            if (miCodigo.Length > 1)
            {
                double cupo = 0.0;


                if (this.cmbTipoGarantia.Text.Equals("Real"))
                {
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaEmiReal").Value = Convert.ToDateTime(this.dtpFechaInicio.Text);
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaVenReal").Value = Convert.ToDateTime(this.dtpFechaFin.Text);
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoReal").Value = this.txNumeroDocumento.Text;
                    if (this.mtbCupo.Text.Length == 0)
                    {
                        //miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorReal").Value = Convert.ToDouble(0);
                        cupo = Convert.ToDouble(0); ;
                    }
                    else
                    {
                        //miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorReal").Value = Convert.ToDouble(this.mtbCupo.Text);
                        cupo = Convert.ToDouble(this.mtbCupo.Text);
                    }
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_AvalistaReal").Value = this.txtAvalista.Text;
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_DescripcionRea").Value = this.txtDescripcion.Text;
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_EsPagare").Value = "NO";
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorGarReales").Value = this.CalcularTotal();


                    string updateReal = "update OCRD set U_CSS_FechaEmiReal='"+this.dtpFechaInicio.Text+"',U_CSS_FechaVenReal='"+this.dtpFechaFin.Text+
                        "',U_CSS_DocumentoReal='"+this.txNumeroDocumento.Text+"',U_CSS_ValorReal='"+cupo +"',U_CSS_AvalistaReal='"+this.txtAvalista.Text+
                        "',U_CSS_DescripcionRea='"+this.txtDescripcion.Text+"',U_CSS_EsPagare='NO',U_CSS_ValorGarReales='"+this.CalcularTotal()+"' where CardCode ='" + this.cmbTercero.Text + "'";
                                                

                    ClaseDatos.nonQuery(updateReal);

                }
                else
                {
                    string esPagare = "";

                //    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaEmiPers").Value = Convert.ToDateTime(this.dtpFechaInicio.Text);
                //    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaVenPers").Value = Convert.ToDateTime(this.dtpFechaFin.Text);
                //    miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoPers").Value = this.txNumeroDocumento.Text;
                    if (this.mtbCupo.Text.Length == 0)
                    {
                        //miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorPers").Value = Convert.ToDouble(0);
                        cupo =Convert.ToDouble (0);
                    }
                    else
                    {
                        //miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorPers").Value = Convert.ToDouble(this.mtbCupo.Text);
                        cupo = Convert.ToDouble(this.mtbCupo.Text);
                    }
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_AvalistaPers").Value = this.txtAvalista.Text;
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_DescripcionPer").Value = this.txtDescripcion.Text;
                    if (this.chkPagare.Checked)
                    {
                        //miSocioNegocios.UserFields.Fields.Item("U_CSS_EsPagare").Value = "SI";
                        esPagare = "SI";
                    }
                    else
                    {
                        //miSocioNegocios.UserFields.Fields.Item("U_CSS_EsPagare").Value = "NO";
                        esPagare = "NO";
                    }
                    //miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorGarPers").Value = this.CalcularTotal();

                    string updatePers = "update OCRD set U_CSS_FechaEmiPers='" + this.dtpFechaInicio.Text + "',U_CSS_FechaVenPers='" + this.dtpFechaFin.Text +
                        "',U_CSS_DocumentoPers='" + this.txNumeroDocumento.Text + "',U_CSS_ValorPers='" + cupo + "',U_CSS_AvalistaPers='" + this.txtAvalista.Text +
                        "',U_CSS_DescripcionPer='" + this.txtDescripcion.Text + "',U_CSS_EsPagare='" + esPagare + "',U_CSS_ValorGarPers='" + this.CalcularTotal() + "' where CardCode ='" + this.cmbTercero.Text + "'";


                    ClaseDatos.nonQuery(updatePers);

                }

                //int miResultado = miSocioNegocios.Update();
                //if (miResultado != 0)
                //{
                //    throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                //}
                return true;
            }
            else {

                throw new Exception("El socio de Negocios no existe");
            
            }
        }

        private void ActualizarSocioNegociosEliminar()
        {
            BusinessPartners miSocioNegocios = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            miSocioNegocios.GetByKey(this.cmbTercero.Text);
            if (this.cmbTipoGarantia.Text.Equals("Real"))
            {
                string a = miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoReal").Value.ToString();
                if (miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoReal").Value.ToString().Equals(this.txNumeroDocumento.Text))
                {
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaEmiReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaVenReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorReal").Value = Convert.ToDouble(0);
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_AvalistaReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DescripcionRea").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_EsPagare").Value = "NO";                        
                }
                miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorGarReales").Value = this.CalcularTotal();
                miSocioNegocios.Update();
            }
            else
            {
                if (miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoPers").Value.ToString().Equals(this.txNumeroDocumento.Text))
                {
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaEmiPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaVenPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorPers").Value = Convert.ToDouble(0);
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_AvalistaPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DescripcionPer").Value = "";
                    if (this.chkPagare.Checked)
                    {
                        miSocioNegocios.UserFields.Fields.Item("U_CSS_EsPagare").Value = "SI";
                    }
                    else
                    {
                        miSocioNegocios.UserFields.Fields.Item("U_CSS_EsPagare").Value = "NO";
                    }                        
                }
                miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorGarPers").Value = this.CalcularTotal();
                miSocioNegocios.Update();
            }        
        }

        private void ActualizarSocioNegociosActualizar(string unTipoAnterior,string unTipoNuevo)
        {
            BusinessPartners miSocioNegocios = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            miSocioNegocios.GetByKey(this.cmbTercero.Text);
            if (unTipoAnterior.Equals(unTipoNuevo))
            {
                if (this.cmbTipoGarantia.Text.Equals("Real"))
                {
                    if (miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoReal").Value.ToString().Equals(this.txNumeroDocumento.Text))
                    {
                        ActualizarSocioNegocios();
                    }
                }
                else
                {
                    if (miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoPers").Value.ToString().Equals(this.txNumeroDocumento.Text))
                    {
                        ActualizarSocioNegocios();
                    } 
                }
            }
            else if (unTipoAnterior.Equals("Real"))
            {
                if (miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoReal").Value.ToString().Equals(this.txNumeroDocumento.Text))
                {
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaEmiReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaVenReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorReal").Value = Convert.ToDouble(0);
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_AvalistaReal").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DescripcionRea").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_EsPagare").Value = "NO";
                    miSocioNegocios.Update();
                    ActualizarSocioNegocios();
                }
            }
            else            
            {
                if (miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoPers").Value.ToString().Equals(this.txNumeroDocumento.Text))
                {
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaEmiPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_FechaVenPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DocumentoPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_ValorPers").Value = Convert.ToDouble(0);
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_AvalistaPers").Value = "";
                    miSocioNegocios.UserFields.Fields.Item("U_CSS_DescripcionPer").Value = "";
                    miSocioNegocios.Update();
                    ActualizarSocioNegocios();
                }
            }
            
        }

        public void CargaFormularioInicial()
        {
            string miSql = "SELECT ' --Seleccione Codigo--' AS CardCode, ' --Seleccione Nombre--' AS CardName  UNION ALL " +
                                      "SELECT CardCode, CardName " +
                                      "FROM OCRD " +
                                      "ORDER BY 1";
                                      //"WHERE CardType='C' ORDER BY 1";
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
            DataSet miDataSet;
            miDataSet = ClaseDatos.procesaDataSet(miSql);
            this.cmbNombreTercero.DataSource = miDataSet.Tables[0];
            this.cmbNombreTercero.DisplayMember = "CardName";
            this.cmbNombreTercero.ValueMember = "CardCode";
            this.cmbNombreTercero.SelectedIndex = this.cmbNombreTercero.FindString(" --Seleccione Nombre--");
            this.cmbTercero.DataSource = miDataSet.Tables[0];
            this.cmbTercero.DisplayMember = "CardCode";
            this.cmbTercero.ValueMember = "CardCode";
            this.cmbTercero.SelectedIndex = this.cmbTercero.FindString(" --Seleccione Codigo--");
            this.cmbTipoGarantia.SelectedIndex = 0;
            this.cmbAvalista.SelectedIndex = this.cmbAvalista.FindString("-- Avalista--");
            this.btnActualizar.Enabled = false;
            this.btnEliminar.Enabled = false;
            this.txtAvalista.Text = "";
            this.mtbCupo.Text = "";
            this.txNumeroDocumento.Text = "";
            this.txtDescripcion.Text = "";
            this.cmbAvalista.Enabled = false;
            this.txtAvalista.Enabled = false;            
            this.chkPagare.Checked = false;
            this.dtpFechaInicio.Text = DateTime.Now.ToString();
            this.dtpFechaFin.Text = DateTime.Now.ToString();
            txtNitTercero.Text = "";
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            CargaFormularioInicial();            
        }

        private void chkPagare_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkPagare.Checked)
            {
                this.cmbAvalista.Enabled = false;
                this.txtAvalista.Enabled = false;
                this.txNumeroDocumento.Enabled = false;
            }
            else
            {
                this.cmbAvalista.Enabled = true;
                this.txtAvalista.Enabled = true;
                this.txNumeroDocumento.Enabled = true;

            }            
        }

        private void cmbAvalista_TextChanged(object sender, EventArgs e)
        {
            if(!this.cmbAvalista.Text.Equals("SI"))
            {
                this.txtAvalista.Text = "";
            }
        }
    }
}


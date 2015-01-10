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
    public partial class frmProductosControlados : Form
    {
        Dictionary<string, string> misItems = new Dictionary<string, string>();
        public frmProductosControlados()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;           
        }
        private void frmProductosControlados_Load(object sender, EventArgs e)
        {
            try
            {
                string miSql = "SELECT ' --Seleccione Calidad--' AS Code,' --Seleccione Calidad--' AS Name UNION ALL SELECT Code, Name FROM [@CSS_CALIDAD] ORDER BY 2" +
                               "SELECT ' --Seleccione Destino--' AS Code,' --Seleccione Destino--' AS Name UNION ALL SELECT Code, Name FROM [@CSS_DESTINO] ORDER BY 2" +
                               "SELECT ' --Seleccione Código--' AS ItemCode,' --Seleccione Artículo--' AS ItemName UNION ALL SELECT ItemCode,ItemName FROM OITM ORDER BY 1"+
                               "SELECT ' --Seleccione Municipio--' AS Code,' --Seleccione Municipio--' AS Name UNION ALL SELECT Code,Name FROM [@BPCO_MU] ORDER BY 2";
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                DataSet miDataSet = ClaseDatos.procesaDataSet(miSql);                
                this.cmbDestino.DataSource = miDataSet.Tables[1];
                this.cmbDestino.ValueMember = "Code";
                this.cmbDestino.DisplayMember = "Name";
                this.cmbCalidad.DataSource = miDataSet.Tables[0];
                this.cmbCalidad.ValueMember = "Code";
                this.cmbCalidad.DisplayMember = "Name";
                this.cmbItem.DataSource = miDataSet.Tables[2];
                this.cmbItem.DisplayMember = "ItemCode";
                this.cmbItem.ValueMember = "ItemCode";
                this.cmbDescripcionArticulo.DataSource = miDataSet.Tables[2];
                this.cmbDescripcionArticulo.DisplayMember = "ItemName";
                this.cmbDescripcionArticulo.ValueMember = "ItemCode";
                this.cmbItem.SelectedIndex = this.cmbItem.FindString(" --Seleccione Item--");
                this.cmbTipo.SelectedIndex = this.cmbTipo.FindString(" --Seleccione Tipo--");
                this.cmbCalidad.SelectedIndex = this.cmbCalidad.FindString(" --Seleccione Calidad--");
                this.cmbDestino.SelectedIndex = this.cmbDestino.FindString(" --Seleccione Destino--");
                this.cmbDescripcionArticulo.SelectedIndex = this.cmbDescripcionArticulo.FindString(" --Seleccione Artículo--");
                this.cmbItem.SelectedIndex = this.cmbItem.FindString(" --Seleccione Código--");
                this.cmbMunicipio.DataSource = miDataSet.Tables[3];
                this.cmbMunicipio.DisplayMember = "Name";
                this.cmbMunicipio.ValueMember = "Code";
                this.cmbMunicipio.SelectedIndex = this.cmbMunicipio.FindString(" --Seleccione Municipio--");
                this.btnEliminar.Enabled = false;
                this.btnActualizar.Enabled = false;
                ClaseDatos.SqlUnConnex();
            }
            catch(Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar el listaod de Items: "+miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }       

        private void cmbItem_TextChanged(object sender, EventArgs e)
        {
            if (!this.cmbItem.Text.Equals(" --Seleccione Item--"))
            {
                string miDescripcion = "";
                misItems.TryGetValue(this.cmbItem.Text, out miDescripcion);                
            }
        }

        private void cmbTipo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.cmbTercero.Enabled = true;                        
                if (!cmbTipo.Text.ToString().Equals(" --Seleccione Tipo--"))
                {
                    if (cmbTipo.Text.ToString().Equals("Q"))
                    {
                        string miSql = "SELECT CompnyName, TaxIdNum " +
                                       "FROM OADM ";                                       
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                        DataSet miDataSet;
                        miDataSet = ClaseDatos.procesaDataSet(miSql);
                        this.cmbNombreTercero.DataSource = miDataSet.Tables[0];
                        this.cmbNombreTercero.DataSource = miDataSet.Tables[0];
                        this.cmbNombreTercero.DisplayMember = "CompnyName";
                        this.cmbNombreTercero.ValueMember = "TaxIdNum";
                        this.cmbNombreTercero.SelectedIndex = 0;
                        this.cmbTercero.DataSource = miDataSet.Tables[0];
                        this.cmbTercero.DisplayMember = "TaxIdNum";
                        this.cmbTercero.ValueMember = "TaxIdNum";
                        this.cmbTercero.SelectedIndex = 0;
                        this.cmbTercero.Enabled = false;
                        this.cmbNombreTercero.Enabled = false;
                        ClaseDatos.SqlUnConnex();
                    }
                    else if (cmbTipo.Text.ToString().Equals("C"))
                    {                        
                        string miSql = "SELECT ' --Seleccione Codigo--' AS CardCode, ' --Seleccione Nombre--' AS CardName  UNION ALL "+
                                       "SELECT CardCode, CardName " +
                                       "FROM OCRD " +
                                       "WHERE CardType='C' ORDER BY 1";
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                        DataSet miDataSet;
                        miDataSet=ClaseDatos.procesaDataSet(miSql);                                                                       
                        this.cmbNombreTercero.DataSource = miDataSet.Tables[0];
                        this.cmbNombreTercero.DisplayMember = "CardName";
                        this.cmbNombreTercero.ValueMember = "CardCode";
                        this.cmbNombreTercero.SelectedIndex = this.cmbNombreTercero.FindString(" --Seleccione Nombre--");
                        this.cmbTercero.DataSource = miDataSet.Tables[0];
                        this.cmbTercero.DisplayMember = "CardCode";
                        this.cmbTercero.ValueMember = "CardCode";
                        this.cmbTercero.SelectedIndex = this.cmbTercero.FindString(" --Seleccione Codigo--");
                        
                        ClaseDatos.SqlUnConnex();
                    }
                    else
                    {
                        string miSql = "SELECT ' --Seleccione Codigo--' AS CardCode, ' --Seleccione Nombre--' AS CardName  UNION ALL " +
                                       "SELECT CardCode, CardName " +
                                       "FROM OCRD " +
                                       "WHERE CardType='S' ORDER BY 1";
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
                        ClaseDatos.SqlUnConnex();
                        
                    }
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los socios de negocios: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void cmbTercero_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                if (!this.cmbTercero.Text.Equals(" --Seleccione Tercero--"))
                {
                    string miSql = "SELECT CardName "+
                                   "FROM OCRD "+
                                   "WHERE CardCode='" + this.cmbTercero.Text + "'";

                    string miNombre=ClaseDatos.scalarStringSql(miSql);
                    ClaseDatos.SqlUnConnex();                    
                    //this.txtNombreTercero.Text = miNombre;
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar el nombre del socio de negocios: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }           
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {            
            if (!this.cmbTipo.Text.Equals(" --Seleccione Tipo--"))
            {
                if (!this.cmbTercero.Text.Equals(" --Seleccione Codigo--"))
                {
                    if (!this.cmbItem.Text.Equals(" --Seleccione Código--"))
                    {
                        if (!this.cmbDestino.Text.Equals(" --Seleccione Destino--"))
                        {
                            if (this.cmbCalidad.Text.Equals(" --Seleccione Calidad--"))
                            {
                                MessageBox.Show("Por favor seleccione calidad", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (this.mtbCupo.Text.Length > 0 && !this.mtbCupo.Text.Equals("0"))
                            {
                                if (this.txtCertificado.Text.Length > 0)
                                {
                                    if (!this.cmbMunicipio.Text.Equals(" --Seleccione Municipio--"))
                                    {
                                        UserTable miTabla = ClaseDatos.objCompany.UserTables.Item("CSS_CERTIFICADO");
                                        try
                                        {
                                            if (this.dtpFechaFin.Value > this.dtpFechaInicio.Value)
                                            {
                                                string miSQL = "SELECT COUNT(*) " +
                                                               "FROM [@CSS_CERTIFICADO] " +
                                                               "WHERE U_CSS_Certificado='" + this.txtCertificado.Text + "'";
                                                int miEncontrado = ClaseDatos.scalarIntSql(miSQL);
                                                if (miEncontrado == 0)
                                                {
                                                    miSQL = "SELECT COUNT(*) " +
                                                            "FROM [@CSS_CERTIFICADO] " +
                                                            "WHERE U_CSS_Entidad='" + this.cmbTercero.Text + "' " +
                                                            "AND U_CSS_Articulo='" + this.cmbItem.Text + "' " +
                                                            "AND ('" + this.dtpFechaInicio.Value.ToString("yyyyMMdd") + "' BETWEEN U_CSS_Fecha_Inicio AND U_CSS_Fecha_Fin OR " +
                                                            "'" + this.dtpFechaFin.Value.ToString("yyyyMMdd") + "' BETWEEN U_CSS_Fecha_Inicio AND U_CSS_Fecha_Fin OR " +
                                                            "U_CSS_Fecha_Inicio BETWEEN '" + this.dtpFechaInicio.Value.ToString("yyyyMMdd") + "' AND '" + this.dtpFechaFin.Value.ToString("yyyyMMdd") + "' OR " +
                                                            "U_CSS_Fecha_Fin BETWEEN '" + this.dtpFechaInicio.Value.ToString("yyyyMMdd") + "' AND '" + this.dtpFechaFin.Value.ToString("yyyyMMdd") + "')";
                                                    miEncontrado = ClaseDatos.scalarIntSql(miSQL);
                                                    if (miEncontrado == 0)
                                                    {
                                                        miSQL = "SELECT isnull(MAX(convert(int,Code)),0) as Contador FROM [@CSS_CERTIFICADO]";
                                                        string miCodigo = ClaseDatos.scalarStringSql(miSQL);
                                                        miTabla.Code = (Convert.ToInt32(miCodigo) + 1).ToString();
                                                        miTabla.Name = (Convert.ToInt32(miCodigo) + 1).ToString();
                                                        miTabla.UserFields.Fields.Item("U_CSS_Articulo").Value = this.cmbItem.Text;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Entidad").Value = this.cmbTercero.Text;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Calidad").Value = this.cmbCalidad.SelectedValue;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Destino").Value = this.cmbDestino.SelectedValue;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Certificado").Value = this.txtCertificado.Text;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Cupo").Value = Convert.ToInt32(this.mtbCupo.Text);
                                                        miTabla.UserFields.Fields.Item("U_CSS_Fecha_Inicio").Value = this.dtpFechaInicio.Value;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Fecha_Fin").Value = this.dtpFechaFin.Value;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Tipo_Entidad").Value = this.cmbTipo.Text;
                                                        miTabla.UserFields.Fields.Item("U_CSS_Municipio").Value = this.cmbMunicipio.SelectedValue.ToString();
                                                        miTabla.UserFields.Fields.Item("U_CSS_Descripcion").Value = this.txtDescripcion.Text.ToString();
                                                        miTabla.Add();
                                                        MessageBox.Show("Operación realizada con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Existe un certificado de carencia asignado en este rango de fechas", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("El certificado ya existe", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                        MessageBox.Show("Debe seleccionar un Municipio", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Debe digitar un certificado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe digitar un cupo mayor a 0", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor seleccione un destino", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione un artículo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione una entidad", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un tipo de entidad", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                   
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCertificado.Text.Length > 0)
                {
                    string miSql = "SELECT U_CSS_Articulo,U_CSS_Entidad,U_CSS_Fecha_Inicio,U_CSS_Fecha_Fin,U_CSS_Cupo,U_CSS_Calidad,U_CSS_Destino,U_CSS_Tipo_Entidad,U_CSS_Descripcion,U_CSS_Municipio " +
                                 "FROM [@CSS_CERTIFICADO] " +
                                 "WHERE U_CSS_Certificado='" + this.txtCertificado.Text + "'";
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                    IDataReader miLector = ClaseDatos.procesaDataReader(miSql);
                    bool miRegistroEncontrado = false;
                    while (miLector.Read())
                    {
                        miRegistroEncontrado = true;
                        this.cmbItem.Text = miLector.GetValue(0).ToString();
                        this.cmbTipo.Text = miLector.GetValue(7).ToString();
                        this.cmbTercero.SelectedValue = miLector.GetValue(1).ToString();
                        this.dtpFechaInicio.Value = Convert.ToDateTime(miLector.GetValue(2));
                        this.dtpFechaFin.Value = Convert.ToDateTime(miLector.GetValue(3));
                        this.mtbCupo.Text = miLector.GetValue(4).ToString();
                        this.cmbCalidad.SelectedValue = miLector.GetValue(5).ToString();
                        this.cmbDestino.SelectedValue = miLector.GetValue(6).ToString();
                        this.cmbMunicipio.SelectedValue = miLector.GetValue(9).ToString();
                        this.txtDescripcion.Text = miLector.GetValue(8).ToString();
                    }
                    ClaseDatos.SqlUnConnex();
                    if (!miRegistroEncontrado)
                    {
                        MessageBox.Show("No existe el certificado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.btnEliminar.Enabled = true;
                        this.btnActualizar.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor digite un Certificado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al consultar el certificado: "+miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (this.txtCertificado.Text.Length > 0)
            {
                if (!this.cmbTipo.Text.Equals(" --Seleccione Tipo--"))
                {
                    if (!this.cmbTercero.Text.Equals(" --Seleccione Codigo--"))
                    {
                        if (!this.cmbItem.Text.Equals(" --Seleccione Código--"))
                        {
                            if (!this.cmbDestino.Text.Equals(" --Seleccione Destino--"))
                            {
                                if (this.cmbCalidad.Text.Equals(" --Seleccione Calidad--"))
                                {
                                    MessageBox.Show("Por favor seleccione calidad", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (this.mtbCupo.Text.Length > 0 && !this.mtbCupo.Text.Equals("0"))
                                {
                                    UserTable miTabla = ClaseDatos.objCompany.UserTables.Item("CSS_CERTIFICADO");
                                    try
                                    {
                                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                                        string miSQL = "SELECT Code FROM [@CSS_CERTIFICADO] WHERE U_CSS_Certificado='" + this.txtCertificado.Text + "'";
                                        IDataReader miLector = ClaseDatos.procesaDataReader(miSQL);
                                        string miCodigo = "";                                        
                                        if (miLector.Read())
                                        {
                                            miCodigo = miLector.GetValue(0).ToString();
                                        }
                                        ClaseDatos.SqlUnConnex();
                                        if (miCodigo.Length > 0)
                                        {
                                            miSQL = "SELECT COUNT(*) " +
                                                        "FROM [@CSS_CERTIFICADO] " +
                                                        "WHERE U_CSS_Entidad='" + this.cmbTercero.Text + "' " +
                                                        "AND U_CSS_Articulo='" + this.cmbItem.Text + "' " +
                                                        "AND ('" + this.dtpFechaInicio.Value.ToString("yyyyMMdd") + "' BETWEEN U_CSS_Fecha_Inicio AND U_CSS_Fecha_Fin OR " +
                                                        "'" + this.dtpFechaFin.Value.ToString("yyyyMMdd") + "' BETWEEN U_CSS_Fecha_Inicio AND U_CSS_Fecha_Fin OR " +
                                                        "U_CSS_Fecha_Inicio BETWEEN '" + this.dtpFechaInicio.Value.ToString("yyyyMMdd") + "' AND '" + this.dtpFechaFin.Value.ToString("yyyyMMdd") + "' OR " +
                                                        "U_CSS_Fecha_Fin BETWEEN '" + this.dtpFechaInicio.Value.ToString("yyyyMMdd") + "' AND '" + this.dtpFechaFin.Value.ToString("yyyyMMdd") + "') "+ 
                                                        "AND U_CSS_Certificado <> '"+this.txtCertificado.Text+"'";
                                            int miEncontrado = ClaseDatos.scalarIntSql(miSQL);
                                            if (miEncontrado == 0)
                                            {
                                                miTabla.GetByKey(miCodigo);
                                                miTabla.Name = miCodigo;
                                                miTabla.UserFields.Fields.Item("U_CSS_Articulo").Value = this.cmbItem.Text;
                                                miTabla.UserFields.Fields.Item("U_CSS_Entidad").Value = this.cmbTercero.Text;
                                                miTabla.UserFields.Fields.Item("U_CSS_Calidad").Value = this.cmbCalidad.SelectedValue;
                                                miTabla.UserFields.Fields.Item("U_CSS_Destino").Value = this.cmbDestino.SelectedValue;
                                                miTabla.UserFields.Fields.Item("U_CSS_Certificado").Value = this.txtCertificado.Text;
                                                miTabla.UserFields.Fields.Item("U_CSS_Cupo").Value = this.mtbCupo.Text;
                                                miTabla.UserFields.Fields.Item("U_CSS_Fecha_Inicio").Value = this.dtpFechaInicio.Value;
                                                miTabla.UserFields.Fields.Item("U_CSS_Fecha_Fin").Value = this.dtpFechaFin.Value;
                                                miTabla.UserFields.Fields.Item("U_CSS_Tipo_Entidad").Value = this.cmbTipo.Text;
                                                miTabla.UserFields.Fields.Item("U_CSS_Municipio").Value = this.cmbMunicipio.SelectedValue;
                                                miTabla.UserFields.Fields.Item("U_CSS_Descripcion").Value = this.txtDescripcion.Text;

                                                miTabla.Update();
                                                MessageBox.Show("Operación realizada con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Existe un certificado de carencia asignado en este rango de fechas", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("El certificado no existe", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        
                                    }
                                    catch (Exception miExcepcion)
                                    {
                                        MessageBox.Show("Error al actualizar el certificado: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    finally
                                    {
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(miTabla);
                                    }                                    
                                }
                                else
                                {
                                    MessageBox.Show("Debe digitar un cupo mayor a 0", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Por favor seleccione un destino", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor seleccione un artículo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione una entidad", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un tipo de entidad", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe digitar un certificado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.txtCertificado.Text.Length > 0)
            {
                UserTable miTabla = ClaseDatos.objCompany.UserTables.Item("CSS_CERTIFICADO");
                try
                {
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                    string miSQL = "SELECT Code FROM [@CSS_CERTIFICADO] WHERE U_CSS_Certificado='" + this.txtCertificado.Text + "'";
                    IDataReader miLector = ClaseDatos.procesaDataReader(miSQL);
                    string miCodigo = "";
                    if (miLector.Read())
                    {
                        miCodigo = miLector.GetValue(0).ToString();
                    }                    
                    if (miCodigo.Length > 0)
                    {
                        miTabla.GetByKey(miCodigo);
                        miTabla.Remove();
                        MessageBox.Show("El certificado fue eliminado con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El certificado no existe", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ClaseDatos.SqlUnConnex();
                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al borrar el certificado: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}

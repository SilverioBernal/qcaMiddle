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
    public partial class frmCircularizacion : Form
    {
        public frmCircularizacion()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string miSql;
            BusinessPartners miSocioNegocios = (BusinessPartners)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            ClaseDatos.objCompany.StartTransaction();
            try
            {
                if (this.cmbRepresentante.Text.Equals(" --Seleccione Representante--") && this.cmbUnidad.Text.Equals(" --Seleccione Unidad--"))
                {
                    MessageBox.Show("Por favor seleccione un representante o la unidad de negocio","Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    if (!this.cmbRepresentante.Text.Equals(" --Seleccione Representante--"))
                    {
                        miSql = "SELECT CardCode "+
                                "FROM OCRD "+
                                "WHERE SlpCode='"+this.cmbRepresentante.SelectedValue.ToString()+"'";                        
                    }
                    else
                    {
                        miSql = "SELECT CardCode FROM OCRD T0 "+
                                "INNER JOIN OSLP T1 "+
                                "ON T0.SlpCode=T1.SlpCode "+
                                "AND T1.U_QCA_Unidad='"+this.cmbUnidad.Text.ToString()+"'";
                    }
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                    IDataReader miLector = ClaseDatos.procesaDataReader(miSql);
                    while (miLector.Read())
                    {
                        miSocioNegocios.GetByKey(miLector.GetString(0));
                        miSocioNegocios.set_Properties(28, BoYesNoEnum.tYES);
                        int miResultado = miSocioNegocios.Update();
                        if (miResultado != 0)
                        {
                            throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                        }
                    }
                    ClaseDatos.SqlUnConnex();
                }
                ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                MessageBox.Show("Operación realizada con Éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
            }                
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al actualizar los socios de Negocios: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }

        private void frmCircularizacion_Load(object sender, EventArgs e)
        {
            string miSql = "SELECT '0' AS SlpCode, ' --Seleccione Representante--' AS SlpName " +
                           "UNION ALL " +
                           "SELECT SlpCode, SlpName " +
                           "FROM OSLP " +
                           "WHERE SlpName<>'-Ningún empleado del departament' "+
                           "ORDER BY 2";
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
            DataSet miDataSet;
            miDataSet = ClaseDatos.procesaDataSet(miSql);
            this.cmbRepresentante.DataSource = miDataSet.Tables[0];
            this.cmbRepresentante.DisplayMember = "SlpName";
            this.cmbRepresentante.ValueMember = "SlpCode";
            this.cmbRepresentante.SelectedIndex = this.cmbRepresentante.FindString(" --Seleccione Representante--");
            miSql = "SELECT ' --Seleccione Unidad--' AS U_QCA_UNIDAD UNION ALL " +
                    "SELECT DISTINCT(U_QCA_UNIDAD) " +
                    "FROM OSLP " +
                    "WHERE SlpName<>'-Ningún empleado del departament' " +
                    "ORDER BY 1";             
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
            miDataSet = ClaseDatos.procesaDataSet(miSql);
            this.cmbUnidad.DataSource = miDataSet.Tables[0];
            this.cmbUnidad.DisplayMember = "U_QCA_UNIDAD";
            this.cmbUnidad.ValueMember = "U_QCA_UNIDAD";
            this.cmbUnidad.SelectedIndex = this.cmbUnidad.FindString(" --Seleccione Unidad--");           
        }

        private void cmbRepresentante_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbRepresentante.SelectedIndex != 0)
            {
                this.cmbUnidad.Enabled = false;
            }
            else
            {
                this.cmbUnidad.Enabled = true; 
            }
        }

        private void cmbUnidad_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbUnidad.SelectedIndex != 0)
            {
                this.cmbRepresentante.Enabled = false;
            }
            else
            {
                this.cmbRepresentante.Enabled = true; 
            }
        }       
    }
}

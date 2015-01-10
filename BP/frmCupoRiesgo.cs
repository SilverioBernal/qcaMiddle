using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BP
{
    public partial class frmCupoRiesgo : Form
    {
        public frmCupoRiesgo()
        {
            InitializeComponent();
        }
     
      
        private void cbxProd_DropDown(object sender, EventArgs e)
        {
            try
            {

                string miConsulta = "select distinct U_QCA_UNIDAD from OSLP";
                DataSet dato;

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                dato = ClaseDatos.procesaDataSet(miConsulta);



                cbxProd.DisplayMember = "U_QCA_UNIDAD";
                cbxProd.ValueMember = "U_QCA_UNIDAD";
                cbxProd.DataSource = dato.Tables[0];

                cbxIngVentas.Enabled = false;



            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar la familia del producto: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();
        }

        private void cbxIngVentas_DropDown(object sender, EventArgs e)
        {

                        try
            {

                string miConsulta = "select distinct Memo,slpcode from OSLP ";
                DataSet dato;

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                dato = ClaseDatos.procesaDataSet(miConsulta);



                cbxIngVentas.DisplayMember = "Memo";
                cbxIngVentas.ValueMember = "slpcode";
                cbxIngVentas.DataSource = dato.Tables[0];

                cbxProd.Enabled = false;



            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar el Ingeniero de Ventas: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();
        }

        private void cbxIngVentas_SelectedValueChanged(object sender, EventArgs e)
        {

            if (cbxIngVentas.Text == "") { }

            else
            {


                cbxProd.Enabled = false;
                try
                {

                    string miConsulta = "select CardCode as Cliene,CardName as Nombre,CreditLine as Cupo,UpdateDate as Fecha from OCRD where Slpcode=" + cbxIngVentas.SelectedValue;
                    DataSet dato;

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    dato = ClaseDatos.procesaDataSet(miConsulta);


                    dgvResultado.Columns.Add(chekColumn);
                    dgvResultado.DataSource = dato.Tables[0];
                    dgvResultado.Columns[0].Width = 20; //check
                    dgvResultado.Columns[1].ReadOnly = true; //cliente
                    dgvResultado.Columns[2].ReadOnly = true;//Nombre
                    dgvResultado.Columns[2].Width = 200;
                    dgvResultado.Columns[3].ReadOnly = true;//Cupo Credito
                    dgvResultado.Columns[4].ReadOnly = true; //Fecha actualiza
                    dgvResultado.Columns.Add("ColumnProcentaje", "%");
                    dgvResultado.Columns[5].ReadOnly = true; // %
                    dgvResultado.Columns.Add("ColumnVariacion", "Variacion");
                    dgvResultado.Columns[6].ReadOnly = true; //Variacion
                    dgvResultado.Columns.Add("ColumnNuevoCupo", "Nuevo Cupo");
                    dgvResultado.Columns[7].ReadOnly = true; // NUevo CUpo



                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar los datos del producto en la grilla: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
            }
        }

     

        private void cbxProd_SelectedValueChanged(object sender, EventArgs e)
        {


            if (cbxProd.Text == "") { }

            else
            {

                cbxIngVentas.Enabled = false;
                try
                {

                    string miConsulta = "select CardCode as Cliene,CardName as Nombre,CreditLine as Cupo,UpdateDate as Fecha from OCRD where Slpcode in (select distinct slpcode from OSLP   where U_QCA_UNIDAD='" + cbxProd.SelectedValue + "')";
                    DataSet dato;

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    dato = ClaseDatos.procesaDataSet(miConsulta);

                    
                    dgvResultado.Columns.Add(chekColumn);
                    dgvResultado.DataSource = dato.Tables[0];
                    dgvResultado.Columns[0].Width = 20; //check
                    dgvResultado.Columns[1].ReadOnly = true; //cliente
                    dgvResultado.Columns[2].ReadOnly = true;//Nombre
                    dgvResultado.Columns[2].Width = 200;
                    dgvResultado.Columns[3].ReadOnly = true;//Cupo Credito
                    dgvResultado.Columns[4].ReadOnly = true; //Fecha actualiza
                    dgvResultado.Columns.Add("ColumnProcentaje", "%");
                    dgvResultado.Columns[5].ReadOnly = true; // %
                    dgvResultado.Columns.Add("ColumnVariacion", "Variacion");
                    dgvResultado.Columns[6].ReadOnly = true; //Variacion
                    dgvResultado.Columns.Add("ColumnNuevoCupo", "Nuevo Cupo");
                    dgvResultado.Columns[7].ReadOnly = true; // NUevo CUpo


                   


                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar los datos del producto en la grilla: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
            }
        }

        private void btActualiza_Click(object sender, EventArgs e)
        {
            try
            {
                for (int a = 0; a + 1 < dgvResultado.Rows.Count; a++)
                {
                   string valor="",code="",cSql="";
                   valor = "" + dgvResultado.Rows[a].Cells[0].Value;
                    
                    
                    
                    if (valor=="True")
                    {

                        cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_CUPORIESGO]";
                        code = ClaseDatos.scalarIntSql(cSql).ToString();
                        string miUpdate = "SET DATEFORMAT DMY INSERT INTO [@CSS_CUPORIESGO] (code,name,U_CSS_ID_CLIENTE,U_CSS_NOMCLIENTE,U_CSS_CUPORIESGO,U_CSS_FECHAACTUALIZA,U_CSS_PORCENTAJEVAR,U_CSS_NUEVOPORCENTA)"+
                         "VALUES(" + code + ",'" + code + "','" + dgvResultado.Rows[a].Cells[1].Value + "','" + dgvResultado.Rows[a].Cells[2].Value + "'," +Convert.ToDouble( dgvResultado.Rows[a].Cells[3].Value.ToString()) + ",'" + DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year + "'," +Convert.ToDouble(mskPorcentaje.Text) + "," +Convert.ToDouble(dgvResultado.Rows[a].Cells[7].Value.ToString()) + ")";
                        ClaseDatos.SqlConn.Close();

                        ClaseDatos.nonQuery(miUpdate);

                        miUpdate = "SET DATEFORMAT DMY UPDATE OCRD SET UpdateDate='" + DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year + "',U_CSS_CupoRiesgo=" + Convert.ToDouble(dgvResultado.Rows[a].Cells[7].Value.ToString()) + " WHERE CardCode='" + dgvResultado.Rows[a].Cells[1].Value + "'";

                        ClaseDatos.nonQuery(miUpdate);

                        
                    }
                    else { 
                    
                    
                    }

                    


                }



            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los Cupos Riesgo: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();
            MessageBox.Show("Cupo Actualizado Satisfactoriamente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void mskPorcentaje_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void mskPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {

                try
                {
                    double valorPorcentaje;
                    valorPorcentaje = 0.0;
                    if (mskPorcentaje.Text == "") { }
                    else
                    {

                        if (radioButton1.Checked == true)
                        {
                            string negativo = "-";
                            negativo += "0," + mskPorcentaje.Text;

                            valorPorcentaje = Convert.ToDouble(negativo);
                        }
                        else
                        {
                            valorPorcentaje = Convert.ToDouble("0," + mskPorcentaje.Text);
                        }

                        for (int a = 0; a + 1 < dgvResultado.Rows.Count; a++)
                        {
                            dgvResultado.Rows[a].Cells[5].Value = "" + valorPorcentaje;
                            dgvResultado.Rows[a].Cells[6].Value = "" + Convert.ToDouble(valorPorcentaje * Convert.ToDouble(dgvResultado.Rows[a].Cells[3].Value.ToString()));
                            dgvResultado.Rows[a].Cells[7].Value = "" + (Convert.ToDouble(dgvResultado.Rows[a].Cells[3].Value.ToString()) + (valorPorcentaje * Convert.ToDouble(dgvResultado.Rows[a].Cells[3].Value.ToString())));



                        }





                    }
                    btActualiza.Enabled = true;

                }

                catch (Exception ex) {
                    MessageBox.Show("Ocurrio un error "+ex.Message, "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            
            
            
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvResultado.DataSource = null;
            dgvResultado.Rows.Clear();
            dgvResultado.Columns.Remove(chekColumn);
            dgvResultado.Columns.Remove("ColumnProcentaje");
            dgvResultado.Columns.Remove("ColumnVariacion");
            dgvResultado.Columns.Remove("ColumnNuevoCupo");
            cbxIngVentas.DataSource = null;
            cbxIngVentas.Text = "";
            cbxProd.DataSource = null;
            cbxProd.Text = "";
            mskPorcentaje.Text = "";
            radioButton1.Checked = false;

            cbxIngVentas.Enabled = true;
            cbxProd.Enabled = true;

        }

      

        }
    }


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
    public partial class frmPrevisiones : Form
    {
        public frmPrevisiones()
        {
            InitializeComponent();
        }

        private void frmPrevisiones_Load(object sender, EventArgs e)
        {
            try
            {

                cbxMeses.Items.Add("1");
                cbxMeses.Items.Add("2");
                cbxMeses.Items.Add("3");
                cbxMeses.Items.Add("4");
                cbxMeses.Items.Add("5");
                cbxMeses.Items.Add("6");
                cbxMeses.Items.Add("7");
                cbxMeses.Items.Add("8");
                cbxMeses.Items.Add("9");
                cbxMeses.Items.Add("10");
                cbxMeses.Items.Add("11");
                cbxMeses.Items.Add("12");


            }

            catch (Exception ex)
            {


                MessageBox.Show("Upsss " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Se van a generar las previsiones para el mes: " + cbxMeses.Text, "Mensaje del Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                          
                
                string[] insertAR = new string[50];
                string cSql = "", idPrevision = "";


                try {
                    string Miconsulta = "SELECT DISTINCT T2.U_CSS_SUCURSAL from [@CSS_PREVENTAS_HEAD] T0 INNER JOIN [@CSS_PREVENTAS_LINES] T1  " +
                        " ON T0.U_id_presupuesto=T1.U_id_presupuesto INNER JOIN [@CSS_PREVENTAS_LINE2] T2 ON T1.U_id_presupuesto=T2.U_CSS_ID_PRESUPUESTO" +
                        " where  U_mes=" + cbxMeses.Text + " and  U_estimadokg>0  and T2.U_CSS_SUCURSAL <> '' ";                 
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    DataSet misDatos = ClaseDatos.procesaDataSet(Miconsulta);
                     ClaseDatos.SqlConn.Close();

                    
                    
                    if (misDatos.Tables[0].Rows.Count > 0)
                    {
                        for (int c = 0; c < misDatos.Tables[0].Rows.Count; c++)
                        {

                            string Select = "",Sucursal="";

                            if (misDatos.Tables[0].Rows[c][0]+"" == "*" || misDatos.Tables[0].Rows[c][0] == DBNull.Value || misDatos.Tables[0].Rows[c][0].ToString()==null)
                            {
                                Sucursal = "BOGOTA";

                            }
                            else {
                                Sucursal = "" + misDatos.Tables[0].Rows[c][0];
                                
                            }


                            cSql = "select ISNULL(MAX(CAST(AbsId AS INT)) ,0)+1 AS nextId from OFCT";
                            idPrevision = ClaseDatos.scalarIntSql(cSql).ToString();

                        
                            int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32( cbxMeses.Text));
                            string name = Sucursal + "-QCA-" + Convert.ToDateTime("1-"+cbxMeses.Text+"-"+DateTime.Now.Year).ToString("MMMM");



                            string insert = "set dateformat dmy    insert into OFCT(AbsID,Code,Name,StartDate,EndDate,UserSign,FormView)" +
                           " values(" + idPrevision + "," + idPrevision + ",'" + name + "','" + 1 + "/" + Convert.ToInt32(cbxMeses.Text) + "/" + DateTime.Today.Year +
                           "','" + DiaUltimoCorte + "/" + Convert.ToInt32(cbxMeses.Text) + "/" + DateTime.Today.Year + "','7','M') ";

                            ClaseDatos.nonQuery(insert);



                            Select = " SELECT  T1.U_item,T1.U_estimadokg  from [@CSS_PREVENTAS_HEAD] T0 INNER JOIN [@CSS_PREVENTAS_LINES] T1 " +
                               " ON T0.U_id_presupuesto=T1.U_id_presupuesto INNER JOIN [@CSS_PREVENTAS_LINE2] T2 ON T1.U_id_presupuesto=T2.U_CSS_ID_PRESUPUESTO " +
                                " where T1.U_mes=3 and T1. U_estimadokg>0 and  T2.U_CSS_SUCURSAL='" + Sucursal + "'";

                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            DataSet misDatos1 = ClaseDatos.procesaDataSet(Select);
                            ClaseDatos.SqlConn.Close();
                            for (int d = 0; d < misDatos1.Tables[0].Rows.Count; d++)
                            {
                                string insert2 = "";
                                if (misDatos1.Tables[0].Rows[d][1].ToString().Length > 1)
                                {

                                    insert2 = "SET DATEFORMAT dmy  insert into FCT1(AbsID,Date,ItemCode,LineID,Quantity) values(" + idPrevision + ",'" + DiaUltimoCorte + "/" + Convert.ToInt32(cbxMeses.Text) + "/" + DateTime.Today.Year + "','" + misDatos1.Tables[0].Rows[d][0] + "', " + (d + 1) + ",'" + misDatos1.Tables[0].Rows[d][1].ToString().Replace(",", ".") + "')";
                                }

                                ClaseDatos.nonQuery(insert2);

                            
                            }

                        }


                    }
                
                
                
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos en de las previsiones " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlConn.Close();




                MessageBox.Show("Se generaron las previsiones Satisfactoriamente ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }


    }     
   }

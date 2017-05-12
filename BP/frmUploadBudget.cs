using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BP
{
    public partial class frmUploadBudget : Form
    {


       
       

        public frmUploadBudget()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btn_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Excel files (*.xls)|*.XLS";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.ToString() != "")
                {
                    txtpath.Text = openFileDialog1.FileName.ToString();
                    btnUpload.Enabled = true;
                }
                else
                {
                    btnUpload.Enabled = false;
                }
            }
        }

        private void frmUploadBudget_Load(object sender, EventArgs e)
        {
            btnUpload.Enabled = false;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

            try
            {
                txtlog.Text = "";
                txtlog.BackColor = System.Drawing.Color.White;


                string cIng, cAño, cCliente, cItem, cLog, cQuery;
                double dCosto, dMargen, dPorVar1, dPorVar2, dPorVar3;
                int iMes, iMesVar1, iMesVar2, iMesVar3, nIngVal, nCliVal, nItemVal, nRegVal, Conta=1;


                DataSet oDs = ClaseDatos.ImportaExcel("SELECT * FROM [Plantilla Cargue v1.0$]", txtpath.Text);

                cLog = "";

                progressBar2.Maximum = oDs.Tables[0].Rows.Count ;

                foreach (DataRow renglon in oDs.Tables[0].Rows)
                {
                    progressBar2.Value = Conta;

                    try
                    {


                        #region asigno variables a cada columna de la hoja de excel

                        if ((renglon[0]).ToString() != "")
                        {

                            if ("" + renglon[1] == "")
                            {
                                cIng = "NULL";
                            }
                            else
                            {
                                cIng = "" + renglon[1];
                            }


                            if ("" + renglon[2] == "")
                            {
                                cAño = "NULL";
                            }
                            else
                            {
                                cAño = "" + renglon[2];
                            }

                            if ("" + renglon[3] == "")
                            {
                                cCliente = "0";
                            }
                            else
                            {
                                cCliente = "" + renglon[3];
                            }




                            if ("" + renglon[5] == "")
                            {
                                cItem = "0";
                            }
                            else
                            {
                                cItem = "" + renglon[5];
                            }



                            if (renglon[10].ToString() == "")
                            {
                                dCosto = 0;
                            }
                            else
                            {
                                dCosto = Convert.ToDouble(renglon[10].ToString());
                            }



                            if (renglon[11].ToString() == "")
                            {
                                dMargen = 0;
                            }
                            else
                            {
                                dMargen = Convert.ToDouble(renglon[11].ToString());
                            }

                            if (renglon[8].ToString() == "")
                            {
                                iMes = 0;
                            }
                            else
                            {
                                iMes = Convert.ToInt32(renglon[8].ToString());
                            }

                            if (renglon[12].ToString() == "")
                            {
                                iMesVar1 = 0;
                            }
                            else
                            {
                                iMesVar1 = Convert.ToInt32(renglon[12].ToString());
                            }

                            if (renglon[13].ToString() == "")
                            {
                                dPorVar1 = 0;
                            }
                            else
                            {

                                dPorVar1 = Convert.ToDouble((renglon[13].ToString()).Replace(",", "."));
                            }

                            if (renglon[14].ToString() == "")
                            {
                                iMesVar2 = 0;
                            }
                            else
                            {
                                iMesVar2 = Convert.ToInt32("" + renglon[14]);
                            }

                            if (renglon[15].ToString() == "")
                            {
                                dPorVar2 = 0;
                            }
                            else
                            {
                                dPorVar2 = Convert.ToDouble("" + renglon[15]);
                            }

                            if (renglon[16].ToString() == "")
                            {
                                iMesVar3 = 0;
                            }
                            else
                            {
                                iMesVar3 = Convert.ToInt32("" + renglon[16]);
                            }

                            if (renglon[17].ToString() == "")
                            {
                                dPorVar3 = 0;
                            }
                            else
                            {
                                dPorVar3 = Convert.ToDouble("" + renglon[17]);
                            }


                            #endregion

                            #region valido que los datos de ingeniero, cliente y item sea validos

                            //valido que los datos de ingeniero, cliente y item sea validos



                            cQuery = "select  count(*) from OSLP where SlpCode ='" + cIng + "'";
                            nIngVal = ClaseDatos.scalarIntSql(cQuery);

                            if (nIngVal == 0)
                            {
                                txtlog.Text = txtlog.Text + "\n|El Ingeniero " + cIng + " no existe| Linea: " + Conta +
                                              " XXXXX \n";
                                txtlog.BackColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                cQuery = "select count(*) from ocrd where cardcode =  '" + cCliente + "' ";
                                nCliVal = ClaseDatos.scalarIntSql(cQuery);

                                if (nCliVal == 0)
                                {
                                    txtlog.Text = txtlog.Text + "\nEl Cliente " + cCliente + " no existe|| Linea: " +
                                                  Conta + " XXXXX; \n";
                                    txtlog.BackColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    cQuery = "select count(*) from oitm where itemcode =  '" + cItem + "' ";
                                    nItemVal = ClaseDatos.scalarIntSql(cQuery);

                                    if (nItemVal == 0)
                                    {
                                        txtlog.Text = txtlog.Text + "\nEl Item " + cItem + " no existe| | Linea: " +
                                                      Conta + " XXXXX;\n ";
                                        txtlog.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {


                                        //valido que exista la combinacion ingeniero, cliente, año, item
                                        cQuery =
                                            "select " +
                                            "top 1 ISNULL( SUM(  a.U_id_estimado),0) " +
                                            "from  " +
                                            "	 [@CSS_PRESUPUESTOHEAD]  a  " +
                                            "		inner join [@CSS_PRESUPUESTOLINE] b on  " +
                                            "			a.U_id_estimado = b.U_id_estima_venta " +
                                            "where  " +
                                            "a.U_id_ing_slpcode =  '" + cIng + "' " +
                                            "and a.U_ano_estim =  " + cAño +
                                            " and u_cliente =  '" + cCliente + "' " +
                                            " and u_item =  '" + cItem + "' ";

                                        nRegVal = ClaseDatos.scalarIntSql(cQuery);


                                        if (nRegVal == 0)
                                        {
                                            txtlog.Text = txtlog.Text + "\nla combinacion ingeniero " + cIng +
                                                          ", cliente " + cCliente + ", año " + cAño + ", item" + cItem +
                                                          " no existe en el presupuesto | | Linea: " + Conta + " XXXXXX \n";
                                            txtlog.BackColor = System.Drawing.Color.Red;

                                        }
                                        else
                                        {

                                            #endregion valido que los datos de ingeniero, cliente y item sea validos


                                            txtlog.Text = txtlog.Text + "\nPresupuesto para Ingeniero " + cIng +
                                                          ", Item " + cItem + ", para el mes " + iMes.ToString() +
                                                          ", Verificado";





                                        }

                                    }
                                }
                                //cLog = cLog + "\n\r";
                            }

                            //txtlog.Text = cLog;


                        }
                    }
                    catch (SqlException nullRef)
                    {
                        txtlog.Text = txtlog.Text + "\nHay un valor erroneo en la linea: " + Conta + " XXXXXX; \n";
                        ClaseDatos.SqlConn.Close();


                    }
                    catch (Exception eRR)
                    {
                        txtlog.Text = txtlog.Text + "\n Exepcion : " + eRR + " Linea: " + Conta + " XXXXXX; \n";
                        ClaseDatos.SqlConn.Close();

                    }
                    finally
                    {

                    }


                    Conta++;
                  

                }




            }
            catch (Exception ex)
            {

                MessageBox.Show("Verifique la extencion del archivo,la ruta y el nombre de la hoja que debe ser:  Plantilla Cargue v1  ." + ex.Message, "Mensjae del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
               ClaseDatos.SqlConn.Close();
            }
            finally
            {
               
            }


        }

        private void btnCargarEstos_Click(object sender, EventArgs e)
        {

            try
            {


                string cIng, cAño, cCliente, cItem, cLog, cQuery;
                double dCosto, dMargen, dPorVar1, dPorVar2, dPorVar3;
                int iMes, iMesVar1, iMesVar2, iMesVar3, nCont=1, nCliVal, nItemVal, nRegVal;


                DataSet oDs = ClaseDatos.ImportaExcel("SELECT * FROM [Plantilla Cargue v1.0$]", txtpath.Text);

                cLog = "";

                progressBar1.Maximum = oDs.Tables[0].Rows.Count;

                foreach (DataRow renglon in oDs.Tables[0].Rows)
                {

                    progressBar1.Value = nCont;
                    
                    #region asigno variables a cada columna de la hoja de excel

                    cIng = renglon[1].ToString();
                    cAño = renglon[2].ToString();
                    cCliente = renglon[3].ToString();
                    cItem = renglon[5].ToString();

                    if (renglon[10].ToString() == "")
                    {
                        dCosto = 0;
                    }
                    else
                    {
                        dCosto = Convert.ToDouble(renglon[10].ToString().Replace(",","."));
                    }



                    if (renglon[11].ToString() == "")
                    {
                        dMargen = 0;
                    }
                    else
                    {
                        dMargen = Convert.ToDouble(renglon[11].ToString().Replace(",", "."));
                    }

                    if (renglon[8].ToString() == "")
                    {
                        iMes = 0;
                    }
                    else
                    {
                        iMes = Convert.ToInt32(renglon[8].ToString());
                    }

                    if (renglon[12].ToString() == "")
                    {
                        iMesVar1 = 0;
                    }
                    else
                    {
                        iMesVar1 = Convert.ToInt32(renglon[12].ToString());
                    }

                    if (renglon[13].ToString() == "")
                    {
                        dPorVar1 = 0;
                    }
                    else
                    {
                        dPorVar1 = Convert.ToDouble(renglon[13].ToString().Replace(",", "."));
                    }

                    if (renglon[14].ToString() == "")
                    {
                        iMesVar2 = 0;
                    }
                    else
                    {
                        iMesVar2 = Convert.ToInt32("" + renglon[14]);
                    }

                    if (renglon[15].ToString() == "")
                    {
                        dPorVar2 = 0;
                    }
                    else
                    {
                        dPorVar2 = Convert.ToDouble(("" + renglon[15]).Replace(",", "."));
                    }

                    if (renglon[16].ToString() == "")
                    {
                        iMesVar3 = 0;
                    }
                    else
                    {
                        iMesVar3 = Convert.ToInt32("" + renglon[16]);
                    }

                    if (renglon[17].ToString() == "")
                    {
                        dPorVar3 = 0;
                    }
                    else
                    {
                        dPorVar3 = Convert.ToDouble(("" + renglon[17]).Replace(",", "."));
                    }


                    #endregion


                    //valido que exista la combinacion ingeniero, cliente, año, item
                    cQuery =
                        "select " +
                        "top 1 a.U_id_estimado " +
                        "from  " +
                        "	[@CSS_PRESUPUESTOHEAD] a  " +
                        "		inner join [@CSS_PRESUPUESTOLINE] b on  " +
                        "			a.U_id_estimado = b.U_id_estima_venta  " +
                        "where  " +
                        " a.U_id_ing_slpcode =  '" + cIng + "' " +
                        "and a.U_ano_estim =  " + cAño +
                        " and u_cliente =  '" + cCliente + "' " +
                        " and u_item =  '" + cItem + "' ";
                    nRegVal = ClaseDatos.scalarIntSql(cQuery);

                    if (nRegVal == 0)
                    {
                        txtlog.Text = txtlog.Text + "\nla combinacion ingeniero " + cIng + ", cliente " + cCliente +
                                      ", año " + cAño + ", item" + cItem + " no existe en el presupuesto ;";
                    }
                    else
                    {


                        cQuery =
                            "update [@CSS_PRESUPUESTOLINE] set " +
                            "u_costo = " + dCosto.ToString() + ", " +
                            "u_margen = " + dMargen.ToString() + ", " +
                            "u_mesvar1 = " + iMesVar1.ToString() + ", " +
                            "u_porvar1 = " + dPorVar1.ToString() + ", " +
                            "u_mesvar2 = " + iMesVar2.ToString() + ", " +
                            "u_porvar2 = " + dPorVar2.ToString() + ", " +
                            "u_mesvar3 = " + iMesVar3.ToString() + ", " +
                            "u_porvar3 = " + dPorVar3.ToString() + " " +
                            "where " +
                            " U_id_estima_venta = " + nRegVal.ToString() +
                            " and u_cliente =  '" + cCliente + "' " +
                            " and u_item =  '" + cItem + "' " +
                            " and u_mes = " + iMes.ToString();

                        txtlog.Text = txtlog.Text + "\nPresupuesto para Ingeniero " + cIng + ", Item " + cItem +
                                      ", para el mes " + iMes.ToString() + ",  " + ClaseDatos.nonQuery(cQuery + " ;");
                     


                    }



                    nCont++;


                }

                MessageBox.Show(
                       "Los datos de presupuestos de Ventas secargaron Satisfactoriamente.",
                       "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("... " + ex.Message, "Mensjae del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }




        }

        private void frmUploadBudget_VisibleChanged(object sender, EventArgs e)
        {
          
        }

        private void frmUploadBudget_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                {

                   

                }
            }
        }
    }
}

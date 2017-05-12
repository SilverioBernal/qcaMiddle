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
    public partial class frmProyectaCosto : Form
    {
        public frmProyectaCosto()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            toolStripStatusLabel1.Text = "";
        }

        DataSet dsPres = null;

        private void btnProyecta_Click(object sender, EventArgs e)
        {

            
            try
            {
                string cQuery;
                double ncosto, ncostoVar, porVar1, porVar2, porVar3, precio, margen, TRM;
                int mesVar1, mesVar2, mesVar3, mes;
                double deval;

                dsPres = datos(txtYear.Text);

                foreach (DataRow renglon in dsPres.Tables[0].Rows)
                {
                    if (renglon["U_costo"].ToString() == "")
                    {
                        ncosto = 0;
                    }
                    else {
                        ncosto = double.Parse(renglon["U_costo"].ToString());
                    }

                    if ( renglon["U_mes"].ToString()== "")
                    {
                        mes =   0;
                    }
                    else
                    {
                        mes = int.Parse(renglon["U_mes"].ToString());//Asigna el mes
                    }

                    if ( renglon["U_mesVar1"].ToString()== "")
                    {
                         mesVar1 = 0;
                    }
                    else
                    {
                        mesVar1 = int.Parse(renglon["U_mesVar1"].ToString());
                    }
                    if ( renglon["U_mesVar2"].ToString()== "")
                    {
                        mesVar2 = 0;
                    }
                    else
                    {
                        mesVar2 = int.Parse(renglon["U_mesVar2"].ToString());
                    }
                    if ( renglon["U_mesVar3"].ToString()== "")
                    {
                       mesVar3 = 0;
                    }
                    else
                    {
                         mesVar3 = int.Parse(renglon["U_mesVar3"].ToString());
                    }
                    if (renglon["U_porVar1"].ToString() == "")
                    {
                       porVar1 = 0;
                    }
                    else
                    {
                        porVar1 = double.Parse(renglon["U_porVar1"].ToString());
                    }

                    if ( renglon["U_porVar2"].ToString()== "")
                    {
                         porVar2 =  0;
                    }
                    else
                    {
                         porVar2 = double.Parse(renglon["U_porVar2"].ToString());
                    }
                    if (renglon["U_porVar3"].ToString() == "")
                    {
                         porVar3 =  0;
                    }
                    else
                    {
                        porVar3 = double.Parse(renglon["U_porVar3"].ToString());
                    }
                    if ( renglon["U_margen"].ToString()== "")
                    {
                        margen = 0;
                    }
                    else
                    {
                        margen = double.Parse(renglon["U_margen"].ToString());
                    }
                    
                    

                    #region calculo costo  
                    if (renglon["u_tipomoneda"].ToString() == "0") //COP
                    {
                        ncostoVar = ncosto;
                    }
                    else //USD
                    {
                        deval = double.Parse(txtDeval.Text);
                        TRM = double.Parse( txtTrm.Text);


                        ncostoVar = (ncosto/TRM) * trmDeval(TRM, deval, mes);


                    }

                    if (ncostoVar > 0)
                    {
                        if (mesVar1 != 0)
                        {
                            if (mes >= mesVar1)
                            {
                                ncostoVar = ncostoVar + (ncostoVar * (porVar1 / 100));

                                //Calcula la primer variacion de precio en el mes

                                if (mesVar2 != 0)
                                {
                                    if (mes >= mesVar2)
                                    {
                                        ncostoVar = ncostoVar + (ncostoVar * (porVar2 / 100));
                                        if (mesVar3 != 0)
                                        {
                                            if (mes >= mesVar3)
                                            {
                                                ncostoVar = ncostoVar + (ncostoVar * (porVar3 / 100));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        precio = ncostoVar/((100-margen)/ 100);
                    }
                    else
                    {
                        precio = 0;
                    }
                    cQuery =
                        "Update [@CSS_PRESUPUESTOLINE] set u_costoProyectado =  " + ncostoVar.ToString().Replace(",", ".") + ", u_precio = " + precio.ToString().Replace(",", ".") + 
                        " where code = " + renglon["code"].ToString();

                   

                    toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cQuery);
               
                    
                    #endregion
                                    }
                dsPres = datos(txtYear.Text);


                
            }
            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
            }
            dsPres = datos(txtYear.Text);
            grdPres.DataSource = dsPres.Tables[0];

            //grdPres.Columns[17].Visible = false;
            grdPres.Columns[18].Visible = false;
            grdPres.Columns[19].Visible = false;
            grdPres.Columns[20].Visible = false;
        


        }

        private double trmDeval(double trm, double factor, int mes)
        {
            double factorDev, trmReal;
            trmReal = 0;
            try
            {
                factorDev = 1;

                
                for (int i = 1; i <= mes; i++)
                {
                    trmReal = trm * (1 + ((mes * (factor / 12)) / 100));
                }
                
            }
            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
                trmReal = 0;
            }
            return trmReal;
        }

        private DataSet datos(string year)
        {
            string cQuery;

            cQuery =
           "select isnull(u_tipomoneda, 0) u_tipomoneda, b.* " +
           "from " +
           " [@CSS_PRESUPUESTOHEAD] a " +
           "inner join [@CSS_PRESUPUESTOLINE] b on  " +
           "	a.U_id_estimado = b.U_id_estima_venta    " +
           "       inner join oitm c on " +
           "           b .u_item = c.itemcode " +
           "Where A.U_ano_estim = " + txtYear.Text + 
           " ORDER BY B.U_ITEM, B.U_MES";

            return ClaseDatos.procesaDataSet(cQuery);
        }
    }
}

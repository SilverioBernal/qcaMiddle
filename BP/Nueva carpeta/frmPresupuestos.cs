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
    public partial class frmPresupuestos : Form
    {

        #region Variables Globales

        DataSet dsItems = null;
        DataSet dsPr = null;
        DataSet dsPr2 = null;
        int idPres = 0,indice=0,total=0,posi=0,slpcode=0;
        double total1 = 0.0;
       
         object [,] arregloCarga=new object[8,2];
         object[,] arregloMuestra = new object[8, 2];
         object [,] arreglo0=new object [8,2];
         object [,] arreglo1=new object [8,2];
         object [,] arreglo2=new object [8,2];
         object [,] arreglo3=new object [8,2];
         object [,] arreglo4=new object [8,2];
         object [,] arreglo5=new object [8,2];
         object [,] arreglo6=new object [8,2];
         object [,] arreglo7=new object [8,2];
         object [,] arreglo8=new object [8,2];
         object [,] arreglo9=new object [8,2];
         object [,] arreglo10=new object [8,2];
         object [,] arreglo11=new object [8,2];

        #endregion

         public frmPresupuestos()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guardarDatos();
            buscarDatos();
        }

        private void frmPresupuestos_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Month >= 11 && DateTime.Now.Month <= 12) {

                radioButton1.Enabled = true;
            }

            #region Valida el usuario de Ventas

            try
            {
                ClaseDatos.validSalesEmploye(loging.usrCode);
               
                if (ClaseDatos.salesEmpl == true)
                {

                    try {

                        string miConsulta = "select salesPrson,firstName,middleName,lastName from ohem where userid = (select internal_k from ousr where user_code='"+loging.usrCode+"')";
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet empleado = ClaseDatos.procesaDataSet(miConsulta);

                        this.Text = empleado.Tables[0].Rows[0].ItemArray[0] + "                     " + empleado.Tables[0].Rows[0].ItemArray[1] + "  " + empleado.Tables[0].Rows[0].ItemArray[3]+" - Presupuestos y Estimados ";
                        txtSalesName.Text = " "+empleado.Tables[0].Rows[0].ItemArray[1] + "  " + empleado.Tables[0].Rows[0].ItemArray[3];
                        slpCustomers();
                        slpcode=Convert.ToInt32(empleado.Tables[0].Rows[0].ItemArray[0]);
                        txtItems.ContextMenuStrip = contextMenuStrip1;

                        dataGridView1.Visible = false;
                        txtYear.Text = (DateTime.Today.Year).ToString();
                        dataGridView2.Visible = false;
                     
                    
                    
                    
                    }

                    catch(Exception ex){
                        MessageBox.Show(" " + ex.Message);
                        ClaseDatos.SqlConn.Close();
                    
                    }
                    ClaseDatos.SqlConn.Close();
                    
                    
                    
                }
                else
                {
                    MessageBox.Show("Este usuario no es un empleado de ventas");
                    this.DestroyHandle();
                }
            }
            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
            }

            #endregion

            #region Bloquea Formulario

            string bloquea = "";
            

            try
            {

                bloquea = "select U_CSS_FECHA_CORTE from [@CSS_PRESUPUESTOS_PA] where U_CSS_MES=" + DateTime.Today.Month;

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet ultimo = ClaseDatos.procesaDataSet(bloquea);

                bloquea = "" + ultimo.Tables[0].Rows[0][0];


            }

            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
                ClaseDatos.SqlConn.Close();
            }

            ClaseDatos.SqlConn.Close();


            if (DateTime.Now.Date > Convert.ToDateTime(bloquea).Date)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                dataGridView2.Enabled = true;
                cboCustomers.Enabled = true;
                txtYear.Enabled = true;
                txtItems.Enabled = true;
                button1.Enabled = true;
                txtItemName.Enabled = true;



            }

            #endregion


        }


        #region Carga Dataset
        private void slpCustomers()
        {

            //Carga los clientes de el empleado en el cbxCostumers

            DataSet dsCostumer = null;
            try
            {
                string consulta = "";
                dsCostumer = ClaseDatos.procesaDataSet(
               consulta= "select distinct " +
                "a.cardcode, a.cardname " +
                "from " +
                "	ocrd a " +
                "	inner join crd1 b on " +
                "		a.cardcode = b.cardcode " +
                "WHERE b.BLOCK = " +
                "	(" +
                "		SELECT slpname " +
                "		FROM OSLP " +
                "		WHERE SLPCODE =		" +
                "			(" +
                "				select isnull(salesPrson ,'-1') " +
                "				from ohem " +
                "				where userid = " +
                "					(" +
                "						select internal_k from ousr where user_code = '" +
                                loging.usrCode + "'" +
                "					)" +
                "			)" +
                "	)" +
                "union " +
                "select cardcode, cardname " +
                "from ocrd " +
                "where slpcode = " +
                "	(" +
                "		select isnull(salesPrson ,'-1') from ohem where userid = (select internal_k from ousr where user_code = '" +
                                loging.usrCode + "')" +
                "	) ORDER BY CARDNAME"
                );
                cboCustomers.DataSource = dsCostumer.Tables[0];
                cboCustomers.DisplayMember = "cardname";
                cboCustomers.ValueMember = "cardcode";
            }
            catch (Exception er)
            {
               
                toolStripStatusLabel1.Text = er.Message;
            }
        }

#endregion

        #region Lista los clientes y los productos

        private void showAll_Click(object sender, EventArgs e)
        {

            //muestra todos los productos del menu emergente

            dsItems = ClaseDatos.procesaDataSet("select itemname, itemcode from oitm order by itemname");
            dataGridView1.DataSource = dsItems.Tables[0];
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[0].Width = dataGridView1.Width - 10;            
            dataGridView1.Visible = true;
            

        }

        private void showAllByBp_Click(object sender, EventArgs e)
        {
            dsItems = ClaseDatos.procesaDataSet("SELECT B.ITEMNAME, A.ITEMCODE FROM OSCN A INNER JOIN OITM B ON A.ITEMCODE = B.ITEMCODE WHERE A.CARDCODE = '" + cboCustomers.SelectedValue.ToString() + "'");
            dataGridView1.DataSource = dsItems.Tables[0];
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[0].Width = dataGridView1.Width - 10;
            dataGridView1.Visible = true;
        }

        
        
        #endregion

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Para seleccionar un articulo
            
            txtItems.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtItemName.Visible = true;
            txtItemName.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dataGridView1.Visible = false;
            txtItems.Focus();
            radioButton1.Checked = false;
           
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {   //Presupuesto
            if (txtItems.Text != null)
            {

                buscarDatos();
               
            }
            else {

                MessageBox.Show("Seleccione un Producto");
                radioButton1.Checked = false;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {   //Estimado

            try
            {

                if (txtItems.Text != null)
                {
                    cbxDistribuir.Enabled = true;
                    buscarDatos();
                    radioButton1.Enabled = false;
                    cbxDistribuir.Checked = true;
                }
                else
                {
                    MessageBox.Show("Seleccione un Producto");
                    radioButton2.Checked = false;

                }
            }
            catch(Exception ex){

                MessageBox.Show("   " + ex.Message);
            }

        }      




        private void buscarDatos()
        {
            string cSql;
            idPres = 0;

            try
            {
                #region validacion de la existencia de los datos de presupuesto para el ing. y año indicados
                //validacion de la existencia de los datos de presupuesto para el ing. y año indicados
                cSql =
                    "select COUNT(A.u_ID_PRESUPUESTO) " +
                    "from  " +
                    "	[@CSS_PREVENTAS_head] a  " +
                    "where  " +
                    "a.U_ingeniero =  '" + loging.usrCode + "'" +
                    " and a.u_ano_presup = " + txtYear.Text;

                if (ClaseDatos.scalarIntSql(cSql) > 0)
                {
                    cSql =
                        "select A.u_ID_PRESUPUESTO " +
                        "from  " +
                        "	[@CSS_PREVENTAS_head] a  " +
                        "where  " +
                        "a.U_ingeniero =  '" + loging.usrCode + "'" +
                        " and a.u_ano_presup = " + txtYear.Text;

                    idPres = ClaseDatos.scalarIntSql(cSql);

                }

                #endregion

                //Busqueda de los datos de presupuesto segun parametros ya ingresados (solo lectura)
                #region datos actuales y del año anterior

                //registros del año actual

                if (radioButton2.Checked == true) {

                    cSql = "select U_ID_PRESUPUESTO,U_ANO_PRESUP,U_CLIENTE,U_ITEM,ES_ENERO,ES_FEBRERO,ES_MARZO,ES_ABRIL,ES_MAYO,ES_JUNIO,ES_JULIO,ES_AGOSTO,ES_SEPTIEMBRE,ES_OCTUBRE,ES_NOVIEMBRE,ES_DICIEMBRE from CSS_PRESUPUESTO_KG where u_id_presupuesto=" + idPres.ToString();
                    dataGridView3.DataSource = ClaseDatos.procesaDataSet(cSql).Tables[0];
                    dataGridView3.Columns[3].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[4].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[5].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[6].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[7].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[8].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[9].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[10].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[11].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[12].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[13].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[14].DefaultCellStyle.Format = "0.00";
                
                }
                else {
                    cSql = "select U_ID_PRESUPUESTO,U_ANO_PRESUP,U_CLIENTE,U_ITEM,PR_ENERO,PR_FEBRERO,PR_MARZO,PR_ABRIL,PR_MAYO,PR_JUNIO,PR_JULIO,PR_AGOSTO,PR_SEPTIEMBRE,PR_OCTUBRE,PR_NOVIEMBRE,PR_DICIEMBRE from CSS_PRESUPUESTO_KG where u_id_presupuesto = " + idPres.ToString();
                    dataGridView3.DataSource = ClaseDatos.procesaDataSet(cSql).Tables[0];
                    dataGridView3.Columns[3].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[4].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[5].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[6].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[7].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[8].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[9].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[10].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[11].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[12].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[13].DefaultCellStyle.Format = "0.00";
                    dataGridView3.Columns[14].DefaultCellStyle.Format = "0.00";


                }

               

                //registros del año anterior
                cSql = "select * from CSS_PRESUPUESTO_KG where u_id_presupuesto in( " +
                "select  A.u_ID_PRESUPUESTO  from  [@CSS_PREVENTAS_head] a  where a.U_ingeniero = '" + loging.usrCode + "'" +
                "and a.u_ano_presup = " + (Convert.ToInt32(txtYear.Text) - 1).ToString() + ")";

                dataGridView4.DataSource = ClaseDatos.procesaDataSet(cSql).Tables[0];
                #endregion

                //dependiendo de si es estimado o presupuesto muestro los datos en el grid de trabajo

                //si ya existe el registro de presupuesto para el año e ing. 
                if (idPres > 0)
                {
                    #region existen datos depresupuesto año/ing
                    #region si exsten de datos para el producto actual en el presupuesto se traen a la grilla de trabajo
                    //especificacion de queris dependiendo del checkButton que se halla elegido
                    if (radioButton1.Checked == true && radioButton2.Checked == false)
                    {
                        //si exsten de datos para el producto actual en el presupuesto se traen a la grilla de trabajo
                        cSql =
                            "select  u_mes MES, u_presupkg PRESUPUESTO, A.u_ID_PRESUPUESTO " +
                            "from  " +
                            "	[@CSS_PREVENTAS_head] a  " +
                            "	inner join [@CSS_PREVENTAS_LINES] b on  " +
                            "		a.u_id_presupuesto = b.u_id_presupuesto " +
                            "where  " +
                            "a.U_ingeniero =  '" + loging.usrCode + "'" +
                            "and B.U_cliente =  '" + cboCustomers.SelectedValue + "' " +
                            "and B.U_item =  '" + txtItems.Text + "'" +
                            "and a.u_ano_presup = " + txtYear.Text + " ORDER BY U_MES";
                    }

                    //AQUI

                    if (radioButton1.Checked == false && radioButton2.Checked == true)
                    {

                        cSql =
                            "select  u_mes MES, u_estimadokg ESTIMADO " +
                            "from  " +
                            "	[@CSS_PREVENTAS_head] a  " +
                            "	inner join [@CSS_PREVENTAS_LINES] b on  " +
                            "		a.u_id_presupuesto = b.u_id_presupuesto " +
                            "where  " +
                            "a.U_ingeniero =  '" + loging.usrCode + "'" +
                            "and B.U_cliente =  '" + cboCustomers.SelectedValue + "' " +
                            "and B.U_item =  '" + txtItems.Text + "'" +
                            "and a.u_ano_presup = " + txtYear.Text + " ORDER BY U_MES";
                    }

                    #endregion

                    dsPr = ClaseDatos.procesaDataSet(cSql);

                    if (dsPr.Tables[0].Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dsPr.Tables[0];
                        dataGridView2.Columns[0].ReadOnly = true;
                        dataGridView2.Columns[1].ReadOnly = true;
                        dataGridView2.Columns[1].DefaultCellStyle.Format = "0.00";



                    }
                    // si no se traen datos en blanco para ser llenados
                    else
                    {
                        //especificacion de queris dependiendo del checkButton que se halla elegido
                        #region si no existen datos se llena la grilla de trabajo con datos vacios
                        if (radioButton1.Checked == true && radioButton2.Checked == false)
                        {

                            cSql =
                                "select  1  MES, cast('0.0' as float)  PRESUPUESTO union " +
                                "select  2 u_mes,cast('0.0' as float) u_presupkg union " +
                                "select  3 u_mes, cast('0.0' as float) u_presupkg union " +
                                "select  4 u_mes,cast('0.0' as float) u_presupkg union " +
                                "select  5 u_mes, cast('0.0' as float) u_presupkg union " +
                                "select  6 u_mes, cast('0.0' as float) u_presupkg union " +
                                "select  7 u_mes, cast('0.0' as float) u_presupkg union " +
                                "select  8 u_mes, cast('0.0' as float) u_presupkg union " +
                                "select  9 u_mes, cast('0.0' as float) u_presupkg union " +
                                "select  10 u_mes,cast('0.0' as float) u_presupkg union " +
                                "select  11 u_mes, cast('0.0' as float) u_presupkg union " +
                                "select  12 u_mes, cast('0.0' as float) u_presupkg ";


                        }

                        if (radioButton1.Checked == false && radioButton2.Checked == true)
                        {
                            cSql =
                                "select  1  MES , cast('0.0' as float)  ESTIMADO union " +
                                "select  2 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  3 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  4 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  5 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  6 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  7 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  8 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  9 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  10 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  11 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  12 u_mes, cast('0.0' as float) u_estimadokg ";
                        }
                        #endregion
                        
                        dsPr2 = ClaseDatos.procesaDataSet(cSql);
                        
                        dataGridView2.DataSource = dsPr2.Tables[0];
                        dataGridView2.Columns[0].ReadOnly = true;
                        dataGridView2.Columns[1].ReadOnly = true;
                   
                     

                    }
                    //se muestran los para su edicion
                    dataGridView2.Visible = true;
                    dataGridView2.Columns[0].ReadOnly = false;
                    dataGridView2.Columns[1].ReadOnly = false;
                    
                 

                    #endregion
                }
                else
                {
                    //especificacion de queris dependiendo del checkButton que se halla elegido
                    #region si no existen datos se llena la grilla de trabajo con datos vacios
                    if (radioButton1.Checked == true && radioButton2.Checked == false)
                    {

                        cSql =
                            "select  1  MES, 0  PRESUPUESTO union " +
                            "select  2 u_mes, 0 u_presupkg union " +
                            "select  3 u_mes, 0 u_presupkg union " +
                            "select  4 u_mes, 0 u_presupkg union " +
                            "select  5 u_mes, 0 u_presupkg union " +
                            "select  6 u_mes, 0 u_presupkg union " +
                            "select  7 u_mes, 0 u_presupkg union " +
                            "select  8 u_mes, 0 u_presupkg union " +
                            "select  9 u_mes, 0 u_presupkg union " +
                            "select  10 u_mes, 0 u_presupkg union " +
                            "select  11 u_mes, 0 u_presupkg union " +
                            "select  12 u_mes, 0 u_presupkg ";
                    }

                    if (radioButton1.Checked == false && radioButton2.Checked == true)
                    {
                        cSql =
                                "select  1  MES , cast('0.0' as float)  ESTIMADO union " +
                                "select  2 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  3 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  4 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  5 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  6 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  7 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  8 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  9 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  10 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  11 u_mes, cast('0.0' as float) u_estimadokg union " +
                                "select  12 u_mes, cast('0.0' as float) u_estimadokg ";
                    }
                    #endregion

                    dsPr2 = ClaseDatos.procesaDataSet(cSql);
                    dataGridView2.DataSource = dsPr2.Tables[0];
                    dataGridView2.Columns[0].ReadOnly = true;
                    dataGridView2.Columns[1].ValueType = typeof(double);
               
                    dataGridView2.Columns[1].ReadOnly = true;
                  
                                   }
            }
            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
            }



           //verificacion de habilitacion o no

            

                if (radioButton2.Checked == true)
                {
                    string bloquea = "";
                            
                  for (int a=0;a<dataGridView2.Rows.Count;a++) {
                    
                     dataGridView2.Rows[a].ReadOnly = true;

                    }

                  try {

                      bloquea = "select U_CSS_FECHA_CORTE from [@CSS_PRESUPUESTOS_PA] where U_CSS_MES="+DateTime.Today.Month;

                      ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                      IDataReader ultimo = ClaseDatos.procesaDataReader(bloquea);

                      while (ultimo.Read())
                      {

                          bloquea =""+ ultimo.GetValue(0).ToString();



                      }
                  
                  
                  }

                  catch (Exception er)
                  {
                      toolStripStatusLabel1.Text = er.Message;
                      ClaseDatos.SqlConn.Close();
                  }

                  ClaseDatos.SqlConn.Close();



                  

                        switch (Convert.ToDateTime(bloquea).Month)
                        {

                            case 1:
                                dataGridView2.Rows[0].ReadOnly = false;
                                dataGridView2.Rows[1].ReadOnly = false;
                                dataGridView2.Rows[2].ReadOnly = false;
                                dataGridView2.Rows[3].ReadOnly = false;
                                break;
                            case 2: 
                                dataGridView2.Rows[1].ReadOnly = false;
                                dataGridView2.Rows[2].ReadOnly = false;
                                dataGridView2.Rows[3].ReadOnly = false;
                               // dataGridView2.Rows[6].ReadOnly = false;
                                break;
                            case 3: 
                                dataGridView2.Rows[4].ReadOnly = false;
                                dataGridView2.Rows[5].ReadOnly = false;
                                dataGridView2.Rows[6].ReadOnly = false;
                                dataGridView2.Rows[7].ReadOnly = false;

                                break;
                            case 4:
                                dataGridView2.Rows[5].ReadOnly = false;
                                dataGridView2.Rows[6].ReadOnly = false;
                                dataGridView2.Rows[7].ReadOnly = false;
                                dataGridView2.Rows[8].ReadOnly = false;
                                break;
                            case 5: 
                                dataGridView2.Rows[6].ReadOnly = false;
                                dataGridView2.Rows[7].ReadOnly = false;
                                dataGridView2.Rows[8].ReadOnly = false;
                                dataGridView2.Rows[9].ReadOnly = false;
                                break;
                            case 6: 
                                dataGridView2.Rows[7].ReadOnly = false;
                                dataGridView2.Rows[8].ReadOnly = false;
                                dataGridView2.Rows[9].ReadOnly = false;
                                dataGridView2.Rows[10].ReadOnly = false;
                                break;
                            case 7: 
                                dataGridView2.Rows[8].ReadOnly = false;
                                dataGridView2.Rows[9].ReadOnly = false;
                                dataGridView2.Rows[10].ReadOnly = false;
                                dataGridView2.Rows[11].ReadOnly = false;
                                break;
                            case 8: 
                                dataGridView2.Rows[9].ReadOnly = false;
                                dataGridView2.Rows[10].ReadOnly = false;
                                dataGridView2.Rows[11].ReadOnly = false;
                                dataGridView2.Rows[12].ReadOnly = false;
                                break;
                            case 9:
                                dataGridView2.Rows[10].ReadOnly = false;
                                dataGridView2.Rows[11].ReadOnly = false;
                                dataGridView2.Rows[12].ReadOnly = false;
                                break;
                            case 10: 
                               dataGridView2.Rows[11].ReadOnly = false;
                                dataGridView2.Rows[12].ReadOnly = false;
                               
                                break;
                            case 11:
                                dataGridView2.Rows[12].ReadOnly = false;
                               
                                break;

                            case 12:
                                MessageBox.Show("Segun el sistema estamos en el mes de Diciembre del año "+DateTime.Today.Year+" por lo cual los estimados para este año ya estan desabilitados"); 
                                break;
                            default:
                                MessageBox.Show("Se debe verificar la fecha del sistema");                                ;
                                break;
                            

                        }
                  
              
            
            }

            dataGridView2.Columns[0].Width = 35;
        }



        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        private void guardarDatos()
        {
            string cSql;
            try
            {
                //si ya existe el Id de presupuesto 
                if (idPres > 0)
                {
                    #region manejo de datos cuando existe el escenario de presupuesto
                    //si el articulo ya existe se hace update de los datos
                    if (dsPr.Tables[0].Rows.Count > 0)
                    {
                        string idPresupLines="", idPresupLi2;
                        #region actualizacion de datos cuando ya existe el articulo en el escenario de presupuesto
                        if (radioButton1.Checked == true && radioButton2.Checked == false)
                        {

                            foreach (DataRow renglon in dsPr.Tables[0].Rows)
                            {
                                //PARA PRESUPUESTO

                                try {

                                    idPresupLines = "Select code from [@CSS_PREVENTAS_LINES]  "+
                                         "where  " +
                                    "u_id_presupuesto =  " + idPres.ToString() + " " +
                                    "and U_item =  '" + txtItems.Text + "' " +
                                     "and u_mes = " + renglon[0].ToString();
                                  
                                     IDataReader datos;

                                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                                    datos = ClaseDatos.procesaDataReader(idPresupLines);


                                    while (datos.Read())
                                    {

                                        idPresupLines = "" + datos.GetValue(0);
                                    
                                    
                                    }

                                    ClaseDatos.SqlConn.Close();
                                }

                                catch (Exception ex) {

                                    MessageBox.Show("Upssss  " + ex.Message);
                                    ClaseDatos.SqlConn.Close();
                                
                                }



                                cSql =
                                    "update [@CSS_PREVENTAS_LINES] set u_presupkg = " + renglon[1].ToString() +
                                    "where  " +
                                    "u_id_presupuesto =  " + idPres.ToString() + " " +
                                    "and U_item =  '" + txtItems.Text + "' " +
                                    "and u_mes = " + renglon[0].ToString();

                                toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                                #region Distribuir

                                if (cbxDistribuir.Checked == true)
                                {

                                    Organizador(Convert.ToInt16(renglon[0].ToString()) - 1, 1, arregloCarga);    //Recide Mes,proceso,arreglo y trae los datos para cargar

                                    for (int a = 0; a <= 9; a++)
                                    {

                                        cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINE2]";
                                        idPresupLi2 = ClaseDatos.scalarIntSql(cSql).ToString();

                                        cSql = "insert into [@CSS_PREVENTAS_LINE2](code, name, U_CSS_ID_PRESUPUESTO, U_CSS_MES_PRESUPU, U_CSS_SUCURSAL, U_CSS_CANTIDAD,U_CSS_ID_PRE_LINES) " +
                                  "values (" + idPresupLi2 + ", '" + idPresupLi2 + "', " + idPres.ToString() + "," + renglon[0].ToString() + ",' " + arregloCarga[a, 0] + "', " + arregloCarga[a, 1] + "," + idPresupLines + ")";

                                        ClaseDatos.nonQuery(cSql);

                                    }






                                }

                                #endregion


                            }
                        }
                        // Cuando es Estimado
                        if (radioButton1.Checked == false && radioButton2.Checked == true)
                        {
                            //PARA ESTIMADO

                            foreach (DataRow renglon in dsPr.Tables[0].Rows)
                            {

                                try
                                {

                                    idPresupLines = "Select code from [@CSS_PREVENTAS_LINES]  " +
                                         "where  " +
                                    "u_id_presupuesto =  " + idPres.ToString() + " " +
                                    "and U_item =  '" + txtItems.Text + "' " +
                                     "and u_mes = " + renglon[0].ToString()+
                                      " and u_cliente = '" + cboCustomers.SelectedValue+"'";

                                    IDataReader datos;

                                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                                    datos = ClaseDatos.procesaDataReader(idPresupLines);


                                    while (datos.Read())
                                    {

                                        idPresupLines = "" + datos.GetValue(0);


                                    }

                                    ClaseDatos.SqlConn.Close();
                                }

                                catch (Exception ex)
                                {

                                    MessageBox.Show("Upssss  " + ex.Message);
                                    ClaseDatos.SqlConn.Close();

                                }


                                cSql =
                                    "update [@CSS_PREVENTAS_LINES] set u_estimadokg = '" + renglon[1].ToString().Replace(",",".") +
                                    "' where  " +
                                    "u_id_presupuesto =  " + idPres.ToString() + " " +
                                    "and U_item =  '" + txtItems.Text + "' " +
                                    "and u_mes = " + renglon[0].ToString()+
                                     " and u_cliente = '" + cboCustomers.SelectedValue + "'";

                                toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                                #region Distribuir

                                if (cbxDistribuir.Checked == true)
                                {

                                    Organizador(Convert.ToInt16(renglon[0].ToString()) - 1, 1, arregloCarga);    //Recide Mes,proceso,arreglo y trae los datos para cargar

                                    if (idPresupLines.Length > 1)
                                    {

                                        for (int a = 0; a <= 7; a++)
                                        {
                                            if (arregloCarga[a, 0] + "" == "null" || arregloCarga[a, 0] + "" == "")
                                            {


                                                arregloCarga[a, 0] = "0,0"; ////////////////////////////////////////

                                            }
                                            if (arregloCarga[a, 1] + "" == "null" || arregloCarga[a, 1] + "" == "")
                                            {


                                                arregloCarga[a, 1] = "0,0"; ////////////////////////////////////////

                                            }
                                            
                                            if (arregloCarga[a, 1].ToString().Length > 1)
                                            {
                                                cSql = "Update [@CSS_PREVENTAS_LINE2] set U_CSS_CANTIDAD='" + arregloCarga[a, 1].ToString().Replace(",", ".") +
                                          "' " + " where U_CSS_MES_PRESUPU= " + renglon[0].ToString() + " and u_CSS_ID_PRESUPUESTO= " + idPres.ToString() + " and U_css_id_pre_lines=" + idPresupLines +
                                          " and U_CSS_SUCURSAL='" + arregloCarga[a, 0] + "'";
                                            }
                                            else {

                                                cSql = "Update [@CSS_PREVENTAS_LINE2] set U_CSS_CANTIDAD='" + arregloCarga[a, 1] +
                                              "' " + " where U_CSS_MES_PRESUPU= " + renglon[0].ToString() + " and u_CSS_ID_PRESUPUESTO= " + idPres.ToString() + " and U_css_id_pre_lines=" + idPresupLines +
                                              " and U_CSS_SUCURSAL='" + arregloCarga[a, 0] + "'";
                                            
                                            }

                                        //  U_CSS_MES_PRESUPU,   U_CSS_ID_PRE_LINES

                                            ClaseDatos.nonQuery(cSql);

                                        }

                                    }
                                    else {

                                        for (int a = 0; a <= 7; a++)
                                        {

                                            cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINE2]";
                                            idPresupLi2 = ClaseDatos.scalarIntSql(cSql).ToString();

                                            cSql = "insert into [@CSS_PREVENTAS_LINE2](code, name, U_CSS_ID_PRESUPUESTO, U_CSS_MES_PRESUPU, U_CSS_SUCURSAL, U_CSS_CANTIDAD,U_CSS_ID_PRE_LINES) " +
                                      "values (" + idPresupLi2 + ", '" + idPresupLi2 + "', "
                                      + idPres.ToString() + "," + renglon[0].ToString() +
                                      ",' " + arregloCarga[a, 0] + "','"
                                      + arregloCarga[a, 1] +
                                      "'," + idPresupLines + ")";

                                            ClaseDatos.nonQuery(cSql);

                                        }
                                    
                                    
                                    }


                                }

                                #endregion
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region insercion de datos que no existian en el escenario de presupuesto
                        string idPresupLines,idPresupLi2;
                        if (radioButton1.Checked == true && radioButton2.Checked == false)
                        {
                           foreach (DataRow renglon in dsPr2.Tables[0].Rows)
                            {
                                cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINES]";
                                idPresupLines = ClaseDatos.scalarIntSql(cSql).ToString();

                                cSql = "insert into [@CSS_PREVENTAS_LINES](code, name, U_ID_PRESUPUESTO, u_cliente, u_item, u_mes, u_presupkg, u_estimadokg) " +
                                       "values (" + idPresupLines + ", " + idPresupLines + ", " + idPres.ToString() + ", '" + cboCustomers.SelectedValue + "', '" + txtItems.Text + "', " + renglon[0].ToString() + ", " + renglon[1].ToString() + ", 0)";
                                                                
                                toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                                #region Distribuir

                                if (cbxDistribuir.Checked == true)
                                {

                                    Organizador(Convert.ToInt16(renglon[0].ToString())-1, 1, arregloCarga);    //Recide Mes,proceso,arreglo y trae los datos para cargar

                                    for (int a = 0; a <=7; a++)
                                    {

                                        cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINE2]";
                                        idPresupLi2 = ClaseDatos.scalarIntSql(cSql).ToString();

                                            cSql = "insert into [@CSS_PREVENTAS_LINE2](code, name, U_CSS_ID_PRESUPUESTO, U_CSS_MES_PRESUPU, U_CSS_SUCURSAL, U_CSS_CANTIDAD,U_CSS_ID_PRE_LINES) " +
                                      "values (" + idPresupLi2 + ", '" + idPresupLi2 + "', " + idPres.ToString() + "," + renglon[0].ToString() + ",' " + arregloCarga[a, 0] + "', " + arregloCarga[a, 1] + "," + idPresupLines + ")";

                                           ClaseDatos.nonQuery(cSql);

                                    }






                                }

                                #endregion


                            }
                        }

                        if (radioButton1.Checked == false && radioButton2.Checked == true)
                        {
                            foreach (DataRow renglon in dsPr2.Tables[0].Rows)
                            {
                                cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINES]";
                                idPresupLines = ClaseDatos.scalarIntSql(cSql).ToString();

                                cSql = "insert into [@CSS_PREVENTAS_LINES] (code, name, U_ID_PRESUPUESTO, u_cliente, u_item, u_mes, u_presupkg, u_estimadokg) " +
                                       "values (" + idPresupLines + ", " + idPresupLines + ", " + idPres.ToString() + ", '" + cboCustomers.SelectedValue + "', '" + txtItems.Text + "', " + renglon[0].ToString() + ", 0,'" + renglon[1].ToString().Replace(",",".").ToString() + "')";



                                toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);


                                #region Distribuir

                                if (cbxDistribuir.Checked == true)
                                {

                                    Organizador(Convert.ToInt16(renglon[0].ToString()) - 1, 1, arregloCarga);    //Recide Mes,proceso,arreglo y trae los datos para cargar

                                    for (int a = 0; a <= 7; a++)
                                    {
                                        if (arregloCarga[a, 0] + "" == "null" || arregloCarga[a, 0] + "" == "")
                                        {


                                            arregloCarga[a, 0] = "0,0"; ////////////////////////////////////////

                                        }
                                        if (arregloCarga[a, 1] + "" == "null" || arregloCarga[a, 1] + "" == "")
                                        {


                                            arregloCarga[a, 1] = "0,0"; ////////////////////////////////////////

                                        }
                                       


                                        cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINE2]";
                                        idPresupLi2 = ClaseDatos.scalarIntSql(cSql).ToString();

                                        cSql = "insert into [@CSS_PREVENTAS_LINE2](code, name, U_CSS_ID_PRESUPUESTO, U_CSS_MES_PRESUPU, U_CSS_SUCURSAL, U_CSS_CANTIDAD,U_CSS_ID_PRE_LINES) " +
                                  "values (" + idPresupLi2 + ", '" + idPresupLi2 + "', " + idPres.ToString() + "," + renglon[0].ToString() + ",' " + arregloCarga[a, 0] + "', '" + arregloCarga[a, 1].ToString().Replace(",",".") + "'," + idPresupLines + ")";

                                        ClaseDatos.nonQuery(cSql);

                                    }






                                }

                                #endregion


                            }
                        }
                        #endregion
                    }
                    #endregion
                    MessageBox.Show("Datos Actualizados Correctamente ");
                }
                else
                {
                    #region manejo de datos cuando no existe escenario de presupuesto

                    #region creacion de nuevos escenarios de presupuesto
                    string idPresup;

                    cSql = "select ISNULL(MAX(U_ID_PRESUPUESTO) ,0)+1 AS nextId from [@CSS_PREVENTAS_HEAD]";
                    idPresup = ClaseDatos.scalarIntSql(cSql).ToString();

                    cSql = "insert into [@CSS_PREVENTAS_HEAD] (code, name, U_ID_PRESUPUESTO, u_ingeniero, u_ano_presup) " +
                           "values (" + idPresup + ", " + idPresup + ", " + idPresup + ", '" + loging.usrCode + "', " + txtYear.Text + ")";

                    toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                    #endregion

                    #region insercion de datos que no existian en el escenario de presupuesto
                    string idPresupLines;
                    if (radioButton1.Checked == true && radioButton2.Checked == false)
                    {
                        string idPresupLi2 = "";
                        foreach (DataRow renglon in dsPr2.Tables[0].Rows)
                        {
                            cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINES]";
                            idPresupLines = ClaseDatos.scalarIntSql(cSql).ToString();

                            cSql = "insert into [@CSS_PREVENTAS_LINES](code, name, U_ID_PRESUPUESTO, u_cliente, u_item, u_mes, u_presupkg, u_estimadokg) " +
                                   "values (" + idPresupLines + ", " + idPresupLines + ", " + idPresup + ", '" + cboCustomers.SelectedValue + "', '" + txtItems.Text + "', " + renglon[0].ToString() + ", " + renglon[1].ToString() + ", 0)";

                            toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                            #region Distribuir

                            if (cbxDistribuir.Checked == true)
                            {

                                Organizador(Convert.ToInt16(renglon[0].ToString()) - 1, 1, arregloCarga);    //Recide Mes,proceso,arreglo y trae los datos para cargar

                                for (int a = 0; a <= 7; a++)
                                {

                                    cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINE2]";
                                    idPresupLi2 = ClaseDatos.scalarIntSql(cSql).ToString();

                                    cSql = "insert into [@CSS_PREVENTAS_LINE2](code, name, U_CSS_ID_PRESUPUESTO, U_CSS_MES_PRESUPU, U_CSS_SUCURSAL, U_CSS_CANTIDAD,U_CSS_ID_PRE_LINES) " +
                              "values (" + idPresupLi2 + ", '" + idPresupLi2 + "', " + idPres.ToString() + "," + renglon[0].ToString() + ",' " + arregloCarga[a, 0] + "', " + arregloCarga[a, 1] + "," + idPresupLines + ")";

                                    ClaseDatos.nonQuery(cSql);

                                }






                            }

                            #endregion
                        }
                    }

                    if (radioButton1.Checked == false && radioButton2.Checked == true)
                    {

                        string idPresupLi2 = "";
                        foreach (DataRow renglon in dsPr2.Tables[0].Rows)
                        {
                            cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINES]";
                            idPresupLines = ClaseDatos.scalarIntSql(cSql).ToString();

                            cSql = "insert into [@CSS_PREVENTAS_LINES] (code, name, U_ID_PRESUPUESTO, u_cliente, u_item, u_mes, u_presupkg, u_estimadokg) " +
                                   "values (" + idPresupLines + ", " + idPresupLines + ", " + idPresup + ", '" + cboCustomers.SelectedValue + "', '" + txtItems.Text + "', " + renglon[0].ToString() + ", 0, " + renglon[1].ToString() + ")";

                            toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                            #region Distribuir

                            if (cbxDistribuir.Checked == true)
                            {

                                Organizador(Convert.ToInt16(renglon[0].ToString()) - 1, 1, arregloCarga);    //Recide Mes,proceso,arreglo y trae los datos para cargar

                                for (int a = 0; a <= 9; a++)
                                {

                                    cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PREVENTAS_LINE2]";
                                    idPresupLi2 = ClaseDatos.scalarIntSql(cSql).ToString();

                                    cSql = "insert into [@CSS_PREVENTAS_LINE2](code, name, U_CSS_ID_PRESUPUESTO, U_CSS_MES_PRESUPU, U_CSS_SUCURSAL, U_CSS_CANTIDAD,U_CSS_ID_PRE_LINES) " +
                              "values (" + idPresupLi2 + ", '" + idPresupLi2 + "', " + idPres.ToString() + "," + renglon[0].ToString() + ",' " + arregloCarga[a, 0] + "', " + arregloCarga[a, 1] + "," + idPresupLines + ")";

                                    ClaseDatos.nonQuery(cSql);

                                }






                            }

                            #endregion




                        }
                    }
                    #endregion

                    #endregion
                    MessageBox.Show("Datos Creados Correctamente ");
                }
            }
            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
            }

           
        }

        

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.ColumnIndex==0){
                  
            try
            {
                string sucursales = "Select name from [@CSS_SUCURSALES]";
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet misDatos = ClaseDatos.procesaDataSet(sucursales);


                dgvSucursales.DataSource = misDatos.Tables[0];
                dgvSucursales.Visible = true;
                
            }
            catch (Exception er)
            {
                MessageBox.Show("Error cargando la tabla de sucursales " + er.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();
           }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            try{

            if (cbxDistribuir.Checked == true && radioButton1.Checked == true)
            {
                indice = dataGridView2.CurrentRow.Index;

                if (dataGridView5.Rows[indice].Cells[0].Value != null && dataGridView5.Rows[indice].Cells[0].Value != DBNull.Value)
                {
                    dgvDetalle.Visible = true;
                    dgvDetalle.Rows.Add(8);
                    button1.Enabled = false;

                    Organizador(indice, 1, arregloMuestra);
                    arregloMuestra = arregloCarga;

                    for (int a = 0; a < 8; a++)
                    {

                        dgvDetalle.Rows[a].Cells[0].Value = "" + arregloCarga[a, 0];
                        dgvDetalle.Rows[a].Cells[1].Value = "" + arregloCarga[a, 1];

                    }

                }
                else
                {
                    
                    dgvDetalle.Visible = true;
                    dgvDetalle.Rows.Add(8);
                    button1.Enabled = false;
                  
                }


            }
            else {
                if (cbxDistribuir.Checked == true && radioButton2.Checked == true)
                {
                    indice = dataGridView2.CurrentRow.Index;

                    dgvDetalle.Visible = true;
                    dgvDetalle.Rows.Add(8);
                    button1.Enabled = false;
                    dataGridView2.Enabled = false;
                    
                                      
                    
                    if (Convert.ToInt16(txtYear.Text) > DateTime.Now.Date.Year)
                    {
                        if (dataGridView2.CurrentRow.Index > 1)
                        {

                            if (DateTime.Now.Date.Day <= 28)
                            {
                                dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].ReadOnly = false;
                            }
                        }


                    }
                    else
                    {

                        if (dataGridView2.CurrentRow.Index > DateTime.Now.Date.Month + 1)
                        {
                            if (DateTime.Now.Date.Day < 28)
                            {
                                dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].ReadOnly = false;
                            }

                        }


                    }

                }
            }


        }
        catch(Exception ex){

            MessageBox.Show("Upss "+ex.Message,"Mensaje de Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        }

        private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {

                dgvSucursales.Visible = false;
                object[,] arreglo = new object[8, 2];
                double total;

                for (int a = 0; a+1 <dgvDetalle.Rows.Count; a++) {

                    for (int b = 0; b <= 1; b++) {

                        if (dgvDetalle.Rows[a].Cells[b].Value == null || dgvDetalle.Rows[a].Cells[b].Value.ToString() == "") 
                        {

                            arreglo[a, b] = "0,0";

                        }

                         else
                           {
                                 arreglo[a, b] = dgvDetalle.Rows[a].Cells[b].Value;

                                        if (b == 1)
                                        {
                                            total = Convert.ToDouble(arreglo[a, b].ToString());

                                                total1 = total1 + (Convert.ToDouble(arreglo[a, b].ToString()));
                                            
                                        }
                             }
                        
                    
                    }

                    int gaby = 0;

                }

                Organizador(0,0,arreglo);

                dataGridView2.Rows[indice].Cells[1].Value = total1;
                dataGridView5.Rows[indice].Cells[0].Value = 1; // Activa la columna chek

                dgvDetalle.Visible = false;
                dgvDetalle.Rows.Clear();
                button1.Enabled = true;
                dataGridView2.Enabled = true;
                total1 = 0.0;

            }
        }

        private void dgvSucursales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

                dgvDetalle.Rows[dgvDetalle.CurrentRow.Index].Cells[0].Value = dgvSucursales.Rows[dgvSucursales.CurrentRow.Index].Cells[0].Value;
                dgvSucursales.Visible = false;
            

           
        }

        public void Organizador(int dato, int proceso, object[,] arreglo)
        {


            //Metodo para cargar los datos distribuidos de un presupuesto. Este metodo carga y descarga matrices.


            if (proceso == 0 && dato == 0)
            {
                             

                switch (indice)
                {

                    case 0:
                        arreglo0 = arreglo;
                        break;
                    case 1:
                        arreglo1 = arreglo;
                        break;
                    case 2:
                        arreglo2 = arreglo;
                        break;
                    case 3:
                        arreglo3 = arreglo;
                        break;
                    case 4:
                        arreglo4 = arreglo;
                        break;
                    case 5:
                        arreglo5 = arreglo;
                        break;
                    case 6:
                        arreglo6 = arreglo;
                        break;
                    case 7:
                        arreglo7 = arreglo;
                        break;
                    case 8:
                        arreglo8 = arreglo;
                        break;
                    case 9:
                        arreglo9 = arreglo;
                        break;
                    case 10:
                        arreglo10 = arreglo;
                        break;
                    case 11:
                        arreglo11 = arreglo;
                        break;

                    default: MessageBox.Show("Fallo el arreglo", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;


                }




            }
            else
            {
                switch (dato)
                {

                    case 0:
                        arregloCarga = arreglo0;
                        break;
                    case 1:
                        arregloCarga = arreglo1;
                        break;
                    case 2:
                        arregloCarga = arreglo2;
                        break;
                    case 3:
                        arregloCarga = arreglo3;
                        break;
                    case 4:
                        arregloCarga = arreglo4;
                        break;
                    case 5:
                        arregloCarga = arreglo5;
                        break;
                    case 6:
                        arregloCarga = arreglo6;
                        break;
                    case 7:
                        arregloCarga = arreglo7;
                        break;
                    case 8:
                        arregloCarga = arreglo8;
                        break;
                    case 9:
                        arregloCarga = arreglo9;
                        break;
                    case 10:
                        arregloCarga = arreglo10;
                        break;
                    case 11:
                        arregloCarga = arreglo11;
                        break;

                    default: MessageBox.Show("Fallo la carga del arreglo", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;


                }





            }



        }

        private void cbxDistribuir_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView5.Visible = true;
            DataGridViewCheckBoxColumn chek = new DataGridViewCheckBoxColumn();
            chek.Width = 20;
            chek.HeaderText = " * ";
            chek.TrueValue = 1;
            chek.FalseValue = null;
            dataGridView5.Columns.Add(chek);
            dataGridView5.Rows.Add(12);
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2) {

                dataGridView3.ContextMenuStrip = contextMenuStrip2;
            
            
            
            }

        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {

                dataGridView3.ContextMenuStrip = contextMenuStrip2;



            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0) {

               

                    try {


                        for (int a = 0; a < dataGridView3.Rows.Count; a++)
                        {

                            dataGridView3.Rows[a].HeaderCell.Value = (a+1)+" "; 

                            string miConsulta = "select CardName from OCRD where CardCode='" + dataGridView3.Rows[a].Cells[2].Value.ToString() + "'";
                            IDataReader datos;

                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            datos = ClaseDatos.procesaDataReader(miConsulta);

                            while (datos.Read())
                            {

                                dataGridView3.Rows[a].Cells[2].Value =""+ datos.GetValue(0);

                                
                            }

                            

                            

                        }

                        ClaseDatos.SqlConn.Close();
                    
                    }

                    catch (Exception ex) {

                        MessageBox.Show("Upps " + ex.Message, "Mensjane de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClaseDatos.SqlConn.Close();
                    }
                



                    try
                    {


                        for (int a = 0; a < dataGridView3.Rows.Count; a++)
                        {

                            string miConsulta = "select ItemName from OITM where ItemCode='" + dataGridView3.Rows[a].Cells[3].Value.ToString() + "'";
                            IDataReader datos;

                            
                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            datos = ClaseDatos.procesaDataReader(miConsulta);

                            while (datos.Read())
                            {

                                dataGridView3.Rows[a].Cells[3].Value = "" + datos.GetValue(0);


                            }
                           

                          }

                        ClaseDatos.SqlConn.Close();

                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show("Upps " + ex.Message, "Mensjane de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClaseDatos.SqlConn.Close();
                    }
                
                
                
            
            }

            dataGridView3.Columns[2].HeaderText = "Cliente";
            dataGridView3.Columns[3].HeaderText = "Producto";
            dataGridView4.Columns[2].HeaderText = "Cliente";
            dataGridView4.Columns[3].HeaderText = "Producto";


            if (dataGridView4.Rows.Count > 0) { 
            
            }
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
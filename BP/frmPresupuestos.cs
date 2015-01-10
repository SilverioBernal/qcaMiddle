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
        int idPres = 0,indice=0,total=0,posi=0,slpcode=0,_Click=0,_t=0,w=0;
        double total1 = 0.0;
       
        

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
            //if (DateTime.Now.Month >= 8 && DateTime.Now.Month <= 12) {

            //    radioButton1.Enabled = true;
            //}

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

                        this.Text += " " + empleado.Tables[0].Rows[0].ItemArray[0] + " " + empleado.Tables[0].Rows[0].ItemArray[1] + "  " + empleado.Tables[0].Rows[0].ItemArray[3] + " - Presupuestos y Estimados ";
                        txtSalesName.Text = " "+empleado.Tables[0].Rows[0].ItemArray[1] + "  " + empleado.Tables[0].Rows[0].ItemArray[3];
                        //Carga el Combobox con el listado de clientes
                        slpCustomers();
                        slpcode=Convert.ToInt32(empleado.Tables[0].Rows[0].ItemArray[0]);
                        txtItems.ContextMenuStrip = contextMenuStrip1;

                        dataGridView1.Visible = false;
                        txtYear.Text = (DateTime.Today.Year+1).ToString();
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

                if (ultimo.Tables[0].Rows.Count > 0)
                {


                    bloquea = "" + ultimo.Tables[0].Rows[0][0];
                }
                else {

                    bloquea = "";
                    MessageBox.Show("La fecha de corte del formulario impide que se pueda habilitar.","Mensaje del Sistema",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                
                }


            }

            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
                ClaseDatos.SqlConn.Close();
            }

            ClaseDatos.SqlConn.Close();

             if (bloquea.Length >1)


            if (DateTime.Now.Date > Convert.ToDateTime(bloquea).Date)
            {
                radioButton1.Enabled = true;
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
                cboCustomers.DataSource = null;
                cboCustomers.DataSource = dsCostumer.Tables[0];
                cboCustomers.DisplayMember = "cardname";
                cboCustomers.ValueMember = "cardcode";
            }
            catch (Exception er)
            {
               
                toolStripStatusLabel1.Text = er.Message;
            }
        }
        private void slpCustomers2()
        {

            //Carga los clientes de el empleado en el cbxCostumers

            DataSet dsCostumer = null;
            try
            {
                string consulta = "";

                consulta = "select distinct " +
                 "a.cardcode, a.cardname " +
                 "from " +
                 "	ocrd a " +
                 "	inner join crd1 b on " +
                 "		a.cardcode = b.cardcode " +
                 "WHERE b.BLOCK = " +
                 "	(" +
                 "		SELECT slpname " +
                 "		FROM OSLP " +
                 "		WHERE SLPCODE =		" + cbxSalesName.SelectedValue +

                 " ) union " +
                 "select cardcode, cardname " +
                 "from ocrd " +
                 "where slpcode = " + cbxSalesName.SelectedValue +
                 "	 ORDER BY CARDNAME";
                dsCostumer = ClaseDatos.procesaDataSet(consulta);
                cboCustomers.DataSource = null;
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
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[0].Width = dataGridView1.Width - 10;            
            dataGridView1.Visible = true;
            

        }

        private void showAllByBp_Click(object sender, EventArgs e)
        {
            dsItems = ClaseDatos.procesaDataSet("SELECT B.ITEMNAME, A.ITEMCODE FROM OSCN A INNER JOIN OITM B ON A.ITEMCODE = B.ITEMCODE WHERE A.CARDCODE = '" + cboCustomers.SelectedValue.ToString() + "'");
            dataGridView1.DataSource = dsItems.Tables[0];
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[1].Visible = true;
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
            dataGridView2.Visible = true;
            
            radioButton1_Click(sender, e);

           
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

      




        private void buscarDatos()
        {
            string cSql="";
            idPres = 0;

            try
            {
                #region validacion de la existencia de los datos de presupuesto para el ing. y año indicados
                //validacion de la existencia de los datos de presupuesto para el ing. y año indicados

                if (cbxSalesName.Visible == true && rdButonLineasMultiples.Checked == true) {

                    cSql =
                           " select COUNT(cast (a.code as integer)) " +
                           " from  " +
                           "	[@CSS_PRESUPUESTOHEAD] a  " +
                           "where  " +
                           " a.U_id_ing_slpcode =  '" + slpcode + "'" +
                           " and a.U_ano_estim = " + txtYear.Text;
                
                }
                else
                {

                    cSql =
                        " select COUNT(cast (a.code as integer)) " +
                           "from  " +
                           "	[@CSS_PRESUPUESTOHEAD] a  " +
                           "where  " +
                           " a.U_id_ing_slpcode =  '" + slpcode + "'" +
                           " and a.U_ano_estim = " + txtYear.Text;
                }

                if (ClaseDatos.scalarIntSql(cSql) > 0)
                {
                    cSql =
                        "select cast(a.code as integer)  " +
                           "from  " +
                           "	[@CSS_PRESUPUESTOHEAD] a  " +
                           "where  " +
                           "a.U_id_ing_slpcode =  '" + slpcode + "'" +
                           " and a.U_ano_estim = " + txtYear.Text;
                    idPres = ClaseDatos.scalarIntSql(cSql);

                }
                else { 
                
                //crea el id de presupuesto para el caso en que no exista

                    cSql = " select ISNULL( max(cast (code as integer))+1,0) as nextId from [@CSS_PRESUPUESTOHEAD]  ";
                    idPres = ClaseDatos.scalarIntSql(cSql);

                    cSql = "insert into [@CSS_PRESUPUESTOHEAD] (code, name, u_ano_estim,U_id_estimado, U_id_ing_slpcode) " +
                          "values ('" + idPres  + "', '" + idPres  + "', " + txtYear.Text + ", '" + idPres  + "', '" + slpcode + "')";

                    toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);
                    
                   
                
                }

                #endregion

                //Busqueda de los datos de presupuesto segun parametros ya ingresados (solo lectura)
                #region datos actuales y del año anterior

                
                    //SIN DATOS DE PRESUPUESTO

                cSql = "select U_id_estimado '# Presupuesto',U_ano_estim 'Año',U_CLIENTE 'Cliente',U_ITEM 'Cod Producto', U_nombre_item 'Nombre Producto',ES_ENERO 'ENERO',ES_FEBRERO 'FEBRERO',ES_MARZO 'MARZO',ES_ABRIL 'ABRIL',ES_MAYO 'MAYO',ES_JUNIO 'JUNIO',ES_JULIO 'JULIO',ES_AGOSTO 'AGOSTO',ES_SEPTIEMBRE 'SEPTIEMBRE',ES_OCTUBRE 'OCTUBRE',ES_NOVIEMBRE 'NOVIEMBRE',ES_DICIEMBRE 'DICIEMBRE' from CSS_PRESUPUESTO_KG where U_id_estimado=" + idPres.ToString();
                    dataGridView3.DataSource = ClaseDatos.procesaDataSet(cSql).Tables[0];
                   
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
                
               
                //registros del año anterior
                    cSql = "select U_id_estimado '# Presupuesto',U_ano_estim 'Año',U_CLIENTE 'Cliente',U_ITEM 'Cod Producto', U_nombre_item 'Nombre Producto',ES_ENERO 'ENERO',ES_FEBRERO 'FEBRERO',ES_MARZO 'MARZO',ES_ABRIL 'ABRIL',ES_MAYO 'MAYO',ES_JUNIO 'JUNIO',ES_JULIO 'JULIO',ES_AGOSTO 'AGOSTO',ES_SEPTIEMBRE 'SEPTIEMBRE',ES_OCTUBRE 'OCTUBRE',ES_NOVIEMBRE 'NOVIEMBRE',ES_DICIEMBRE 'DICIEMBRE' from  CSS_PRESUPUESTO_KG where u_id_estimado in(  " +
                "select  A.u_ID_estimado  from  [@CSS_PRESUPUESTOHEAD] a  where a.U_id_ing_slpcode = '" + slpcode + "'" +
                "and a.U_ano_estim = " + (Convert.ToInt32(txtYear.Text) - 1).ToString() + ")";

                dataGridView4.DataSource = ClaseDatos.procesaDataSet(cSql).Tables[0];
                dataGridView4.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                
                #endregion

               

                //si ya existe el registro de presupuesto para el año e ing. 
                if (idPres > 0)
                {
                    #region existen datos depresupuesto año/ing
                    #region si exsten de datos para el producto actual en el presupuesto se traen a la grilla de trabajo
                    //especificacion de queris dependiendo del checkButton que se halla elegido
                   
                        //si exsten de datos para el producto actual en el presupuesto se traen a la grilla de trabajo
                        cSql =
                            "select  u_mes MES, U_estimadokg PRESUPUESTO " +
                            "from  " +
                            "	[@CSS_PRESUPUESTOHEAD] a  " +
                            "	inner join [@CSS_PRESUPUESTOLINE] b on  " +
                            "		a.U_id_estimado = b.U_id_estima_venta " +
                            "where  " +
                            "a.U_id_ing_slpcode =  '" + slpcode  + "'" +
                            "and B.U_cliente =  '" + cboCustomers.SelectedValue + "' " +
                            "and B.U_item =  '" + txtItems.Text + "'" +
                            "and a.U_ano_estim = " + txtYear.Text + " ORDER BY U_MES";
                    

                                    

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
                    //SIN DATOS DE PRESUPUESTO ANTERIOR
                    #region si no existen datos se llena la grilla de trabajo con datos vacios
              

                        cSql =
                            "select  1  MES, 0.0  PRESUPUESTO union " +
                            "select  2 u_mes, 0.0 u_presupkg union " +
                            "select  3 u_mes, 0.0 u_presupkg union " +
                            "select  4 u_mes, 0.0 u_presupkg union " +
                            "select  5 u_mes, 0.0 u_presupkg union " +
                            "select  6 u_mes, 0.0 u_presupkg union " +
                            "select  7 u_mes, 0.0 u_presupkg union " +
                            "select  8 u_mes, 0.0 u_presupkg union " +
                            "select  9 u_mes, 0.0 u_presupkg union " +
                            "select  10 u_mes, 0.0 u_presupkg union " +
                            "select  11 u_mes, 0.0 u_presupkg union " +
                            "select  12 u_mes, 0.0 u_presupkg ";
                

                    
                    #endregion

                    dsPr2 = ClaseDatos.procesaDataSet(cSql);
                    dataGridView2.DataSource = dsPr2.Tables[0];
                    dataGridView2.Columns[0].ReadOnly = true;
                    dataGridView2.Columns[1].ValueType = typeof(double);
                  
                                   }
            }
            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
            }



            dataGridView2.Columns[0].Width = 35;
            dataGridView2.Columns[0].ReadOnly  = true ;
        }





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
                        string idPresupLines="";
                        #region actualizacion de datos cuando ya existe el articulo en el escenario de presupuesto
                       
                            foreach (DataRow renglon in dsPr.Tables[0].Rows)
                            {
                                //PARA PRESUPUESTO

                                try {

                                    idPresupLines = "Select code from [@CSS_PRESUPUESTOLINE]  " +
                                         "where  " +
                                    "U_id_estima_venta =  " + idPres.ToString() + " " +
                                    "and U_item =  '" + txtItems.Text + "' " +
                                     "and u_mes = " + renglon[0].ToString() +
                                     "and u_cliente = '" + cboCustomers.SelectedValue + "'";
                                  
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
                                    "update [@CSS_PRESUPUESTOLINE] set U_estimadokg = " + renglon[1].ToString().Replace(",",".") +
                                    " where  " +
                                    " code =  " + idPresupLines  ;

                                toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                               

                            }

                       
                        #endregion
                    }
                    else
                    {
                        #region insercion de datos que no existian en el escenario de presupuesto
                        string idPresupLines,idPresupLi2;
                       
                           foreach (DataRow renglon in dsPr2.Tables[0].Rows)
                            {
                                cSql = " select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PRESUPUESTOLINE]";
                                idPresupLines = ClaseDatos.scalarIntSql(cSql).ToString();

                                cSql = "insert into [@CSS_PRESUPUESTOLINE](code, name, U_id_estima_venta, u_cliente, u_item, U_nombre_item, u_mes,  u_estimadokg) " +
                                       "values (" + idPresupLines + ", " + idPresupLines + ", " + idPres.ToString() + ", '" + cboCustomers.SelectedValue + "', '" + txtItems.Text + "', '"  + txtItemName.Text + "',"+ renglon[0].ToString() + ", " + renglon[1].ToString() + ")";
                                                                
                                toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

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

                    cSql = " select ISNULL( max(cast (code as integer))+1,0) as nextId from [@CSS_PRESUPUESTOHEAD]  ";
                    idPresup = ClaseDatos.scalarIntSql(cSql).ToString();

                    cSql = "insert into [@CSS_PRESUPUESTOHEAD] (code, name, u_ano_estim,U_id_estimado, U_id_ing_slpcode) " +
                           "values ('" + idPresup + "', '" + idPresup + "', " + txtYear.Text + ", '" +idPresup+ "', '" + slpcode + "')";

                    toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                    #endregion

                    #region insercion de datos que no existian en el escenario de presupuesto
                    string idPresupLines;
                    
                      
                        foreach (DataRow renglon in dsPr2.Tables[0].Rows)
                        {
                            cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_PRESUPUESTOLINE] ";
                            idPresupLines = ClaseDatos.scalarIntSql(cSql).ToString();

                            cSql = "insert into [@CSS_PRESUPUESTOLINE](code, name, U_id_estima_venta, u_cliente, u_item,U_nombre_item, u_mes, u_estimadokg) " +
                                   "values (" + idPresupLines + ", " + idPresupLines + ", " + idPresup + ", '" + cboCustomers.SelectedValue + "', '" + txtItems.Text + "', '" + txtItemName.Text + "', " + renglon[0].ToString() + ", " + renglon[1].ToString() + ")";

                            toolStripStatusLabel1.Text = ClaseDatos.nonQuery(cSql);

                            
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

        private void rdButonLineasMultiples_CheckedChanged(object sender, EventArgs e)
        {
            if (rdButonLineasMultiples.Checked == true) { cbxSalesName_DropDown(sender, e); }
            else
            {

                cbxSalesName.Enabled = false;
            }

        }
        private void cbxSalesName_DropDown(object sender, EventArgs e)
        {
            _Click = 1;
            if (_t == 0)
            {

                try
                {
                    string miConsulta = "";

                    if (rdButonLineasMultiples.Checked == false)
                    {

                        miConsulta = "SELECT  salesPrson,lastName FROM OHEM WHERE manager=(SELECT empID from OHEM where userId =(select internal_k from ousr where user_code = '" + cbxSalesName.Text + "'))";

                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                        cbxSalesName.Enabled = true;
                        cbxSalesName.Visible = true;
                        cbxSalesName.DataSource = null;
                        cbxSalesName.ResetText();

                        cbxSalesName.DataSource = misDatos.Tables[0];
                        cbxSalesName.ValueMember = "salesPrson";
                        cbxSalesName.DisplayMember = "lastName";

                    }
                    else
                    {

                        //consulta con parametro especial

                        miConsulta = "SELECT SlpCode,SlpName FROM OSLP WHERE U_CSS_IDUSER='" + loging.usrCode + "'";

                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                        cbxSalesName.Enabled = true;
                        cbxSalesName.Visible = true;
                        cbxSalesName.DataSource = null;
                        cbxSalesName.ResetText();

                        cbxSalesName.DataSource = misDatos.Tables[0];
                        cbxSalesName.ValueMember = "slpcode";
                        cbxSalesName.DisplayMember = "SlpName";

                    }



                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("CSS:L598 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
                _t = 1;
                _Click = 2;
            }
            else
            {

                if (rdButonLineasMultiples.Checked == true)
                {
                    try
                    {
                        //consulta con parametro especial
                        cbxSalesName.Enabled = true;
                        cbxSalesName.Visible = true;

                        string miConsulta = "";

                        miConsulta = "SELECT slpcode,SlpName FROM OSLP WHERE U_CSS_ESPECIAL=" + slpcode;


                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);


                        cbxSalesName.DataSource = null;
                        cbxSalesName.ResetText();

                        cbxSalesName.DataSource = misDatos.Tables[0];
                        cbxSalesName.ValueMember = "slpcode";
                        cbxSalesName.DisplayMember = "SlpName";
                    }
                    catch (Exception miExcepcion)
                    {
                        MessageBox.Show("CSS:L633 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ClaseDatos.SqlUnConnex();
                    _t = 2;
                    _Click = 2;

                }

            }
        }

        private void cbxSalesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_t >= 2)
            {
                w = 2;
               // slpcode = Convert.ToInt16(cbxSalesName.SelectedValue.ToString());
                slpCustomers2();

                this.Text = "el id de la Liena de Negocio seleccionada es:" + cbxSalesName.SelectedValue ;
                slpcode =  Convert.ToInt16(cbxSalesName.SelectedValue.ToString() );


            }
        }

        private void toolStripMenuItem2CopyTodas_Click(object sender, EventArgs e)
        {
            string copy="";
            copy = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString() ;
            for (int a = 0; a < dataGridView2.Rows.Count;a++ )
            {
                dataGridView2.Rows[a].Cells[1].Value = copy;

            }
        }

        private void toolStripMenuItem3BorrarTodas_Click(object sender, EventArgs e)
        {
            for (int b = 0; b < dataGridView2.Rows.Count; b++)
            {
                dataGridView2.Rows[b].Cells[1].Value = "0.0";

            }

        }

       

        

    }
}
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
    public partial class frmEstimadosRecaudo : Form

    {
       
        
        public IDataReader datosProyectados;
        double sumaTotalInge,acumulado;
        int vengode,Click, contadorFilas, t,rep;
        DateTime Fecha;
        DateTime FechaCorte,ultimodiaMes;
      
        string valecelda,ValorPorcentaje,Mes;
         public string empID; // Identificacion del usuario
         public string slPPcode;// Identificacion del Empleado Usuario
         public int Ultimonumero;

        public frmEstimadosRecaudo()
        {
           
            InitializeComponent();


        }
     
        public void ESTIM_REC_PARAM_Parametros() {

            try
            {

                string miConsulta = "set dateformat ymd select code,U_CSS_Fecha_Corte,U_CSS_Mes,U_CSS_ultimodiames from [@CSS_ESTIM_REC_PARAM]"+
                    " where U_CSS_Mes=" + DateTime.Today.Month;

                IDataReader miIreader;

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                miIreader = ClaseDatos.procesaDataReader(miConsulta);

                while (miIreader.Read())
                {



                    FechaCorte = Convert.ToDateTime(miIreader.GetValue(1).ToString()); //Fecha de corte hasta la que se permiten cambios
                    this.tbxfechCambios.Text = miIreader.GetValue(1).ToString();
                    Mes =""+ miIreader.GetValue(2).ToString(); //Indica el mes al que se va a verificar
                    ultimodiaMes = Convert.ToDateTime(miIreader.GetValue(3).ToString());  //indica cual es el ultimo dia del mes


                }





            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los datos en EstimadosRecaudo_Load, --Favor verificar la fecha bloqueo y parametrizacion en ESTIM_REC_PARAM--. Error : " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();

        
        
        
        }
        private void frmEstimadosRecaudo_Load(object sender, EventArgs e)
        {

            #region variables
            rep = 5000;
            t = 0;
            Click = 0;
            acumulado = 0.0;
            ValorPorcentaje = "";
            txtRepVentas.Text = loging.usrNombreRepventas;
            contadorFilas=0;
            Fecha = System.DateTime.Now; 
            empID = "0";
            #endregion
            
            #region  verifica si el usuario logeado es jefe o no de unidad
            // 

            try {

                string miConsulta = "SELECT  U_CSS_JEFEUNIDAD from OHEM  where userid =(select internal_k from ousr where user_code = '" + txtRepVentas.Text + "')";
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                IDataReader misDatos = ClaseDatos.procesaDataReader(miConsulta);

                while (misDatos.Read())
                {

                    if ( misDatos.GetValue(0).ToString() == "SI") {

                        cbxRepVentas.Visible = true;
                        cbxRepVentas.Text = txtRepVentas.Text;  
                    }

                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error al cargar los datos en EstimadosRecaudo_Load, try1: " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ClaseDatos.SqlUnConnex();

            #endregion
            
            #region try1 valida usuario ventas

            try
            {

                string miConsulta = "SELECT    T0.salesprson, T0.firstname,T0.middlename,T0.lastname from ohem T0 where userid =(select internal_k from ousr where user_code = '" + txtRepVentas.Text + "')";
                               

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    IDataReader misDatos = ClaseDatos.procesaDataReader(miConsulta);
                    
                    

                        while (misDatos.Read())
                    {

                        empID = misDatos.GetValue(0).ToString() + " ";
                       txtRepVentas.Text = misDatos.GetValue(1).ToString() +" ";
                       txtRepVentas.Text += misDatos.GetValue(2).ToString() + " ";
                       txtRepVentas.Text += misDatos.GetValue(3).ToString() + " ";
                      
                        
                    }

                
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los datos en EstimadosRecaudo_Load, try1:L189 " + miExcepcion.Message +" & "+ miExcepcion.StackTrace, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();

            #endregion

            #region Try2 Inserta los registros en la factura


            try
            {
                string docentry = "", slpcode = "", cardcode = "", cardname = "", saldovencido = "", DocDueDate = "", IdFactura = "", stringUpdate = "", stringInsert = "";

                DateTime fechaMas15;
               
                if (DateTime.Now.Month == 1)
                {
                    int mes = DateTime.Today.AddMonths(-1).Month;
                    int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);
                    fechaMas15 = Convert.ToDateTime(DateTime.Now.AddYears(-1).Year + "/" + mes + "/" + DiaUltimoCorte);
                }
                else
                {
                    int mes = DateTime.Today.AddMonths(-1).Month;
                    int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);
                    fechaMas15 = Convert.ToDateTime(DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-") + DiaUltimoCorte);
                }


                string miConsulta = "UPDATE [@CSS_ESTIMADO_REC] SET U_CSS_PAGADA ='Y' WHERE U_Css_SlpCode ="+empID+"    select t2.DocEntry , t1.SlpCode ,t0.U_CSS_CodCliente ,t0.U_CSS_NombreCliente ,t0.U_CSS_TotalDeuda,SUBSTRING (U_CSS_Vencimiento,8,4)" +
" +'-'+ CASE WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JAN' THEN '01'"+ 
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'FEB' THEN '02' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'MAR' THEN '03'"+
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'APR' THEN '04' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'MAY' THEN '05' "+
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JUN' THEN '06' "+
"  WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JUL' THEN '07' "+
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'AGO' THEN '08' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'SEP' THEN '09' "+
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'OCT' THEN '10' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'NOV' THEN '11' "+
" ELSE '12' END +'-'+ SUBSTRING (U_CSS_Vencimiento,5,2),t0.U_CSS_NumeroDoc " + 
                "from [@CSS_CARTERACORTE] t0 inner join [OSLP] t1 on t0.U_CSS_EmpVentas =t1.SlpName inner join OINV t2 "+
                "on t0.U_CSS_NumeroDoc =t2.DocNum "+
                " where t1.SlpCode =" + empID + "  AND U_CSS_FechaCorte='" + fechaMas15.ToString("yyyyMMdd") + "'";

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
               DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);
                
                ///xxxxxxxxxxxxx

               for (int r = 0; r < misDatos.Tables[0].Rows.Count; r++)
               {

                   docentry = misDatos.Tables[0].Rows[r].ItemArray[0].ToString();
                   slpcode = misDatos.Tables[0].Rows[r].ItemArray[1].ToString();
                   cardcode = misDatos.Tables[0].Rows[r].ItemArray[2].ToString();
                   cardname = misDatos.Tables[0].Rows[r].ItemArray[3].ToString();
                   saldovencido = misDatos.Tables[0].Rows[r].ItemArray[4].ToString();
                   DocDueDate = misDatos.Tables[0].Rows[r].ItemArray[5].ToString();
                   IdFactura = misDatos.Tables[0].Rows[r].ItemArray[6].ToString();

                   if(Convert.ToDouble(saldovencido)>0){
                   
                   

                   string miSql = "";
                   miSql = "SELECT isnull(MAX(convert(INT,u_CSS_IDFATCURA)),0) as Contador FROM [@CSS_ESTIMADO_REC] WHERE u_CSS_IDFATCURA=" + IdFactura;
                   string miCodigo = ClaseDatos.scalarStringSql(miSql);
                   if (miCodigo == "0" || miCodigo.Length < 2)
                   {
                       string IDnuM;
                       stringUpdate = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_ESTIMADO_REC]";
                      IDnuM = ClaseDatos.scalarStringSql(stringUpdate);


                       stringInsert = "INSERT INTO [@CSS_ESTIMADO_REC](Code, Name, U_CSS_CardCode,U_CSS_CardName, U_Css_SlpCode, U_CSS_ValorFactura, U_CSS_DocUpdate,U_CSS_DocDueDate,U_CSS_IDFatcura )" +
                        "VALUES (" + IDnuM + "," + IDnuM + ",'" + cardcode + "','" + cardname + "', " + slpcode + "," + Convert.ToDouble(saldovencido) +
                        " ,'" + DateTime.Now.Date.ToString("yyyyMMdd")+ "','" +Convert.ToDateTime(DocDueDate).ToString("yyyyMMdd") + "'," + IdFactura + ")";

                       ClaseDatos.nonQuery(stringInsert);

                   }
                   else
                   {


                       stringUpdate = "UPDATE  [@CSS_ESTIMADO_REC] SET  U_CSS_PAGADA=NULL , U_Css_SlpCode=" + slpcode + " , U_CSS_ValorFactura=" + Convert.ToDouble(saldovencido).ToString().Replace(",", ".") + " , U_CSS_DocUpdate= '" +
                     DateTime.Now.Date.ToString("yyyyMMdd") + "', U_CSS_DocDueDate= '" + Convert.ToDateTime(DocDueDate).ToString("yyyyMMdd") + "' WHERE  U_CSS_IDFatcura=" + IdFactura;
                         
                           ClaseDatos.nonQuery(stringUpdate);

                    }
                   }

               
                   slPPcode = slpcode ;

               }
               slPPcode = empID;
               
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("CSS: L253 " + miExcepcion.Message+" &  "+miExcepcion.StackTrace , "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();

          

             #endregion

            #region cbxClienteInicial



            try
            {


                string miConsulta = " SELECT   distinct U_CSS_CardCode,U_CSS_CardName  " +
                    " FROM       [@CSS_ESTIMADO_REC]   " +
                   "  WHERE   U_Css_SlpCode = '" + slPPcode + "' ORDER BY U_CSS_CardName";


                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                cbxClienteInicial.DisplayMember = "U_CSS_CardName";
                cbxClienteInicial.ValueMember = "U_CSS_CardCode";
                cbxClienteInicial.DataSource = misDatos.Tables[0];


            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("CSS: L285" + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();


            #endregion

            #region valida el Bloqueo del formulario

            ESTIM_REC_PARAM_Parametros();

            if (FechaCorte < DateTime.Today.Date)
            {

                MessageBox.Show("Ha superado la fecha de modificacion", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Enabled = false;
                dataGridView1.Visible = false;

                cbxClienteInicial.Enabled = false;
                cbxRepVentas.Enabled = false;
                txtRepVentas.Enabled = false;
                btnActualizar.Enabled = false;
                btnLimpiar.Enabled = false;
                btnIniciar.Visible = false;

            }

            #endregion
            calcular_Tecnico();
         
            Click = 2;

           

        }

        public void calcular_Tecnico() {

            #region presupuesto tecnico


            try
            {

                DateTime fechaMas15;

                if (DateTime.Now.Month == 1)
                {
                    int mes = DateTime.Today.AddMonths(-1).Month;
                    int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);

                    fechaMas15 = Convert.ToDateTime(DateTime.Now.AddYears(-1).Year + "-" + mes + "-" + DiaUltimoCorte);
                }
                else
                {
                    int mes = DateTime.Today.AddMonths(-1).Month;
                    int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);
                    fechaMas15 = Convert.ToDateTime(DateTime.Now.Date.AddMonths (-1).ToString ("yyyy-MM-")  + DiaUltimoCorte);
                }

                string select =
                          "SELECT ISNULL( SUM( U_CSS_ValorFactura),0)  FROM [@CSS_ESTIMADO_REC] WHERE U_Css_SlpCode =" + slPPcode
                          + " AND U_CSS_DocDueDate  <='" + fechaMas15.AddDays(15).Date.ToString("yyyyMMdd") + "' AND U_CSS_PAGADA IS NULL ";


                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet misDatos1 = ClaseDatos.procesaDataSet(select);


                txtTotalZona.Text = Convert.ToDouble("" + misDatos1.Tables[0].Rows[0][0].ToString()).ToString("C2");


                ValorPorcentaje = Convert.ToDouble("" + misDatos1.Tables[0].Rows[0][0].ToString()).ToString();



            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("CSS:L559 " + miExcepcion.Message + " & " + miExcepcion.StackTrace, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtTotalZona .Text = miExcepcion.StackTrace;
                MessageBox.Show(" " + miExcepcion.StackTrace, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();

            #endregion

        
        }

        public void cargarGrilla(int cadena)
        {

            try
            {
                string docentry = "", slpcode = "", cardcode = "", cardname = "", saldovencido = "", DocDueDate = "", IdFactura = "", stringUpdate = "", stringInsert = "";
                string miConsulta = "";
                if (cbxRepVentas.Visible == true && cbxRepVentas.Items.Count>1)
                {


                    DateTime fechaMas15;
                    
                    if (DateTime.Now.Month == 1)
                    {
                        int mes = DateTime.Today.AddMonths(-1).Month;
                        int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);
                        fechaMas15 = Convert.ToDateTime(DateTime.Now.AddYears(-1).Year + "/" + mes + "/" + DiaUltimoCorte);
                    }
                    else
                    {
                        int mes = DateTime.Today.AddMonths(-1).Month;
                        int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);
                        fechaMas15 = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.AddMonths(-1).Month + "/" + DiaUltimoCorte);
                    }


                    ///XXX
                


                    //XXXXX



                    miConsulta = "UPDATE [@CSS_ESTIMADO_REC] SET U_CSS_PAGADA ='Y' WHERE U_Css_SlpCode =" + cbxRepVentas.SelectedValue + " select t2.DocEntry , t1.SlpCode ,t0.U_CSS_CodCliente ,t0.U_CSS_NombreCliente ,t0.U_CSS_TotalDeuda,SUBSTRING (U_CSS_Vencimiento,8,4)" +
" +'-'+ CASE WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JAN' THEN '01'" +
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'FEB' THEN '02' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'MAR' THEN '03'" +
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'APR' THEN '04' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'MAY' THEN '05' " +
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JUN' THEN '06' " +
"  WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JUL' THEN '07' " +
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'AGO' THEN '08' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'SEP' THEN '09' " +
" WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'OCT' THEN '10' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'NOV' THEN '11' " +
" ELSE '12' END +'-'+ SUBSTRING (U_CSS_Vencimiento,5,2),t0.U_CSS_NumeroDoc " +
                "from [@CSS_CARTERACORTE] t0 inner join [OSLP] t1 on t0.U_CSS_EmpVentas =t1.SlpName inner join OINV t2 " +
                "on t0.U_CSS_NumeroDoc =t2.DocNum " +
                " where t1.SlpCode =" + cbxRepVentas.SelectedValue + "  AND U_CSS_FechaCorte='" + fechaMas15.ToString("yyyyMMdd") + "'";

                     slPPcode = cbxRepVentas.SelectedValue.ToString ();

                }
                else
                {



                    DateTime fechaMas15;
                   
                    if (DateTime.Now.Month == 1)
                    {

                        int mes = DateTime.Today.AddMonths(-1).Month;
                        int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);
                        fechaMas15 = Convert.ToDateTime(DateTime.Now.AddYears(-1).Year + "/" + mes + "/" + DiaUltimoCorte);
                    }
                    else
                    {
                        int mes = DateTime.Now.AddMonths(-1).Month;
                        int DiaUltimoCorte = DateTime.DaysInMonth(DateTime.Now.Year, mes);
                        fechaMas15 = Convert.ToDateTime(DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-") + DiaUltimoCorte);
                    }


                    miConsulta = "select t2.DocEntry , t1.SlpCode ,t0.U_CSS_CodCliente ,t0.U_CSS_NombreCliente ,t0.U_CSS_TotalDeuda,SUBSTRING (U_CSS_Vencimiento,8,4)" +
 " +'-'+ CASE WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JAN' THEN '01'" +
 " WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'FEB' THEN '02' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'MAR' THEN '03'" +
 " WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'APR' THEN '04' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'MAY' THEN '05' " +
 " WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JUN' THEN '06' " +
 "  WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'JUL' THEN '07' " +
 " WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'AGO' THEN '08' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'SEP' THEN '09' " +
 " WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'OCT' THEN '10' WHEN SUBSTRING (U_CSS_Vencimiento,1,3) = 'NOV' THEN '11' " +
 " ELSE '12' END +'-'+ SUBSTRING (U_CSS_Vencimiento,5,2),t0.U_CSS_NumeroDoc " +
                 "from [@CSS_CARTERACORTE] t0 inner join [OSLP] t1 on t0.U_CSS_EmpVentas =t1.SlpName inner join OINV t2 " +
                 "on t0.U_CSS_NumeroDoc =t2.DocNum " +
                 " where t1.SlpCode =" + slPPcode + " AND U_CSS_FechaCorte='"+fechaMas15.ToString ("yyyyMMdd")+"'";



                }
       
               ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
               DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);
                
              

               for (int r = 0; r < misDatos.Tables[0].Rows.Count; r++)
               {

                   docentry = misDatos.Tables[0].Rows[r].ItemArray[0].ToString();
                   slpcode = misDatos.Tables[0].Rows[r].ItemArray[1].ToString();
                   cardcode = misDatos.Tables[0].Rows[r].ItemArray[2].ToString();
                   cardname = misDatos.Tables[0].Rows[r].ItemArray[3].ToString();
                   saldovencido = misDatos.Tables[0].Rows[r].ItemArray[4].ToString();
                   DocDueDate = misDatos.Tables[0].Rows[r].ItemArray[5].ToString();
                   IdFactura = misDatos.Tables[0].Rows[r].ItemArray[6].ToString();



                   string miSql = "";
                   miSql = "SELECT isnull(MAX(convert(INT,u_CSS_IDFATCURA)),0) as Contador FROM [@CSS_ESTIMADO_REC] WHERE u_CSS_IDFATCURA=" + IdFactura;
                   string miCodigo = ClaseDatos.scalarStringSql(miSql);
                   if (miCodigo == "0" || miCodigo.Length < 2)
                   {
                       string IDnuM;
                       stringUpdate = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_ESTIMADO_REC]";
                       IDnuM = ClaseDatos.scalarStringSql(stringUpdate);



                       stringInsert = "INSERT INTO [@CSS_ESTIMADO_REC](Code, Name, U_CSS_CardCode,U_CSS_CardName, U_Css_SlpCode, U_CSS_ValorFactura, U_CSS_DocUpdate,U_CSS_DocDueDate,U_CSS_IDFatcura )" +
                        "VALUES (" + IDnuM + ",'" + IDnuM + "','" + cardcode + "','" + cardname + "', " + slpcode + "," + Convert.ToDouble(saldovencido).ToString ().Replace (",",".") +
                        " ,'" + DateTime.Now.Date.ToString("yyyyMMdd") + "','" + Convert.ToDateTime(DocDueDate).Date.ToString("yyyyMMdd") + "'," + IdFactura + ")";

                       ClaseDatos.nonQuery(stringInsert);
                 

                   }
                   else
                   {



                           stringUpdate = "UPDATE  [@CSS_ESTIMADO_REC] SET  U_CSS_PAGADA=NULL , U_Css_SlpCode=" + slpcode + " , U_CSS_ValorFactura=" + Convert.ToDouble(saldovencido).ToString ().Replace (",",".") + " , U_CSS_DocUpdate= '" +
                         DateTime.Now.Date.ToString("yyyyMMdd") + "', U_CSS_DocDueDate= '" +Convert.ToDateTime ( DocDueDate).ToString ("yyyyMMdd")+ "' WHERE U_CSS_IDFatcura=" + IdFactura ;
                           ClaseDatos.nonQuery(stringUpdate);


                    

                   }


                   slPPcode = slpcode;
               }

        

                
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("CSS: L472 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();

            if (cadena == 4)
            {

                #region cbxClienteInicial



                try
                {


                    string miConsulta = " SELECT   distinct U_CSS_CardCode,U_CSS_CardName  " +
                        " FROM       [@CSS_ESTIMADO_REC]   " +
                       "  WHERE   U_Css_SlpCode = '" + cbxRepVentas.SelectedValue + "' ORDER BY U_CSS_CardName ";


                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                    cbxClienteInicial.DisplayMember = "U_CSS_CardName";
                    cbxClienteInicial.ValueMember = "U_CSS_CardCode";
                    cbxClienteInicial.DataSource = misDatos.Tables[0];


                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("CSS: L316" + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();


                #endregion

            }


            #region inserta los datos capturados en datagridview1

            System.Windows.Forms.DataGridViewCellStyle miestiloCelda = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle otroEstiloCelda = new System.Windows.Forms.DataGridViewCellStyle();

            try
            {

                string miConsulta = "";

                switch (cadena)
                {

                        
                    case 1:
                        miConsulta = "SELECT    Code, U_CSS_IDFatcura, U_CSS_CardCode, U_CSS_CardName , " +
                      "   U_Css_SlpCode,U_CSS_ValorP, U_CSS_ValorFactura, U_CSS_DocUpdate, U_CSS_DocDueDate" +
                      " FROM       [@CSS_ESTIMADO_REC]   " +
                    "  WHERE  U_Css_SlpCode = '" + slPPcode + "'  and U_CSS_DocDueDate <='" + Convert.ToDateTime(dateTimePicker1.Text).
                    Date.ToString("yyyyMMdd") + "' AND U_CSS_PAGADA IS NULL  order by U_CSS_CardName ";
                        break;

                    case 2:
                        miConsulta = "SELECT    Code, U_CSS_IDFatcura, U_CSS_CardCode, U_CSS_CardName , " +
                    "   U_Css_SlpCode,U_CSS_ValorP, U_CSS_ValorFactura, U_CSS_DocUpdate, U_CSS_DocDueDate " +
                    " FROM       [@CSS_ESTIMADO_REC]   " +
                  "  WHERE  and  U_Css_SlpCode =" + slPPcode + " and U_CSS_DocDueDate <='" +
                  Convert.ToDateTime(dateTimePicker1.Text).Date.ToString("yyyyMMdd") + "'" + " AND U_CSS_PAGADA IS NULL  ORDER BY U_CSS_DocDueDate";
                        break;


                    case 3: miConsulta = "SELECT    Code, U_CSS_IDFatcura, U_CSS_CardCode, U_CSS_CardName , " +
                   "   U_Css_SlpCode,U_CSS_ValorP, U_CSS_ValorFactura, U_CSS_DocUpdate, U_CSS_DocDueDate " +
                   " FROM       [@CSS_ESTIMADO_REC]   " +
                 "  WHERE   U_CSS_CardCode = '" + cbxClienteInicial.SelectedValue + "' and U_Css_SlpCode <=" + slPPcode
                 + " and U_CSS_DocDueDate <='" + Convert.ToDateTime(dateTimePicker1.Text).Date.ToString("yyyyMMdd") + "' AND U_CSS_PAGADA IS NULL ORDER BY U_CSS_DocDueDate";
                        break;


                    case 4: miConsulta = "SELECT    Code, U_CSS_IDFatcura, U_CSS_CardCode, U_CSS_CardName , " +
               "   U_Css_SlpCode,U_CSS_ValorP, U_CSS_ValorFactura, U_CSS_DocUpdate, U_CSS_DocDueDate " +
               " FROM       [@CSS_ESTIMADO_REC]   " +
             "  WHERE    U_Css_SlpCode =" + cbxRepVentas.SelectedValue + " and U_CSS_DocDueDate <='" + Convert.ToDateTime(dateTimePicker1.Text).Date.ToString("yyyyMMdd") + "' AND U_CSS_PAGADA IS NULL  order by U_CSS_CardName";
                        break;

                    default:
                        miConsulta = "SELECT Code, U_CSS_IDFatcura, U_CSS_CardCode, U_CSS_CardName , " +
                        "   U_Css_SlpCode,U_CSS_ValorP, U_CSS_ValorFactura, U_CSS_DocUpdate, U_CSS_DocDueDate" +
                        " FROM       [@CSS_ESTIMADO_REC]   " +
                      "  WHERE     U_Css_SlpCode = '" + slPPcode + "'  and U_CSS_DocDueDate <='" + Convert.ToDateTime(dateTimePicker1.Text).Date.ToString("yyyy/MM/dd") + "' AND U_CSS_PAGADA IS NULL  order by U_CSS_CardName ";
                        break;

                }


                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                dataGridView1.Columns.Add("valor", "***ESTIMADO***");
                dataGridView1.Columns[0].ReadOnly = false;
                dataGridView1.Columns[0].ToolTipText = "Para editar presione 0.000.";
                miestiloCelda.BackColor = System.Drawing.Color.Aquamarine;
                miestiloCelda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridView1.Columns[0].DefaultCellStyle = miestiloCelda;
                dataGridView1.Columns[0].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns[0].Width = 150;
                dataGridView1.DataSource = misDatos.Tables[0];
                dataGridView1.Visible = true;
                dataGridView1.Columns[1].Visible = false;  //code
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[2].HeaderText = "# Nota ó Factura";
                dataGridView1.Columns[2].ToolTipText = "Numero de Nota o Factura ";
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[3].HeaderText = "# Cliente";
                dataGridView1.Columns[3].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[4].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[4].HeaderText = "Nombre";
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[5].Visible = false; //slpcode
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[6].HeaderText = "$ Estimado";
                dataGridView1.Columns[6].Width = 120;
                dataGridView1.Columns[6].ToolTipText = "Valor presupuestado anteriormente por el Ingeniero";
                dataGridView1.Columns[6].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                miestiloCelda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridView1.Columns[6].DefaultCellStyle = miestiloCelda;
                dataGridView1.Columns[7].ReadOnly = true;
                dataGridView1.Columns[7].HeaderText = "Valor en Doc";
                dataGridView1.Columns[7].Width = 120;
                dataGridView1.Columns[7].ToolTipText = "Valor parcial o total de la Factura o de la nota generada. Click para Copiar";
                dataGridView1.Columns[7].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[7].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns[8].Visible = false; //fecha actualizacion
                dataGridView1.Columns[9].ReadOnly = true;
                dataGridView1.Columns[9].HeaderText = "Fecha Vencimiento";
                dataGridView1.Columns[9].ToolTipText = "Fecha en la que se hace el corte de la factura";
                dataGridView1.Columns[9].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;  //fecha corte

                


            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("CSS:L547" + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();


            #endregion


            #region sumatoria general de resultados


            double suma = 0.0, total = 0.0, suma1 = 0.0, total1 = 0.0,_contado=0.0;


            for(int w=0;w<dataGridView1.Rows.Count;w++){

               
               

                if (dataGridView1.Rows[w].Cells[6].Value == null || dataGridView1.Rows[w].Cells[6].Value == DBNull.Value)
                {

                }
                else
                {


                    
                    suma = suma + Convert.ToDouble(dataGridView1.Rows[w].Cells[6].Value.ToString());

                    total1 = 0.01;
                    double nueva = 0.0;


                  nueva = ((suma) / Convert.ToDouble(ValorPorcentaje) )* 100;
                  
                    txtTotalRecIn.Text = (suma).ToString("C2");

                    txtCumplimiento.Text = nueva.ToString("#.00") + "%";
                    acumulado = suma;


                    
                }
                
            
            }


            #endregion

            calcular_Tecnico();

        }
        private void cbxClienteInicial_SelectedIndexChanged(object sender, EventArgs e)
        {
           btnLimpiar_Click(sender, e);

          
            if (txtTotalZona.Text.Length>2)
            {
                txtEstimadoXCliente.Visible = true;
                txtEstimadoXCliente.Text = "";
                txtDeudaXCliente.Visible = true;
                label7.Visible = true;
                label4.Visible = true;
                label9.Visible = false;
                label10.Visible = false;
                btnActualizar.Enabled = true;
                txtTotalRecIn.Visible = false;
                txtCumplimiento.Visible = false;
                cargarGrilla(3);

                double suma = 0.0, suma1 = 0.0, sumaEst = 0.0, sumaEst1 = 0.0;
                for (int a = 0; a < dataGridView1.Rows.Count; a++)
                {

                    if (dataGridView1.Rows[a].Cells[7].Value == null || dataGridView1.Rows[a].Cells[7].Value == DBNull.Value)
                    {

                        suma1 = 0;
                        suma = suma + suma1;

                    }

                    else
                    {
                        suma1 = Convert.ToDouble(dataGridView1.Rows[a].Cells[7].Value);
                        suma = suma + suma1;

                    }



                    if (dataGridView1.Rows[a].Cells[6].Value == null || dataGridView1.Rows[a].Cells[6].Value == DBNull.Value)
                    {

                        sumaEst1 = 0;
                        sumaEst = sumaEst + sumaEst1;

                    }

                    else
                    {
                        sumaEst1 = Convert.ToDouble(dataGridView1.Rows[a].Cells[6].Value);
                        sumaEst = sumaEst + sumaEst1;


                    }


                    txtEstimadoXCliente.Text = sumaEst.ToString("C2");
                                        
                    txtDeudaXCliente.Text = suma.ToString("C2");
                
                }


            }

        }
        private void cbxRepVentas_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnLimpiar_Click(sender, e);

            if (Click > 1)
            {

                if (checkBox1.Checked == true)
                {
                    cargarGrilla(4);
                }
                else { cargarGrilla(1); }



            }



        }
        private void cbxRepVentas_DropDown(object sender, EventArgs e)
        {
            Click = 1;

            if (t == 0)
            {

                try
                {
                    string miConsulta = "";

                    if (checkBox1.Checked == false)
                    {

                        miConsulta = "SELECT  salesPrson,lastName FROM OHEM WHERE manager=(SELECT empID from OHEM where userId =(select internal_k from ousr where user_code = '" + cbxRepVentas.Text + "'))";

                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                        cbxRepVentas.Enabled = true;
                        cbxRepVentas.Visible = true;
                        cbxRepVentas.DataSource = null;
                        cbxRepVentas.ResetText();

                        cbxRepVentas.DataSource = misDatos.Tables[0];
                        cbxRepVentas.ValueMember = "salesPrson";
                        cbxRepVentas.DisplayMember = "lastName";

                    }
                    else
                    {

                        //consulta con parametro especial

                        miConsulta = "SELECT SlpCode,SlpName FROM OSLP WHERE U_CSS_IDUSER='" + loging.usrNombreRepventas+"'";

                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                        cbxRepVentas.Enabled = true;
                        cbxRepVentas.Visible = true;
                        cbxRepVentas.DataSource = null;
                        cbxRepVentas.ResetText();

                        cbxRepVentas.DataSource = misDatos.Tables[0];
                        cbxRepVentas.ValueMember = "slpcode";
                        cbxRepVentas.DisplayMember = "SlpName";

                    }



                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("CSS:L598 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
                t = 1;
                Click = 2;
            }
            else
            {

                if (checkBox1.Checked == true)
                {
                    try
                    {
                        //consulta con parametro especial
                        cbxRepVentas.Enabled = true;
                        cbxRepVentas.Visible = true;

                        string miConsulta = "";

                        miConsulta = "SELECT SlpCode,SlpName FROM OSLP WHERE U_CSS_IDUSER='" + loging.usrNombreRepventas + "'";


                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);


                        cbxRepVentas.DataSource = null;
                        cbxRepVentas.ResetText();

                        cbxRepVentas.DataSource = misDatos.Tables[0];
                        cbxRepVentas.ValueMember = "slpcode";
                        cbxRepVentas.DisplayMember = "SlpName";
                    }
                    catch (Exception miExcepcion)
                    {
                        MessageBox.Show("CSS:L633 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ClaseDatos.SqlUnConnex();
                    t = 1;
                    Click = 2;

                }

            }


        }
   
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {

                if ((e.ColumnIndex == 0|| e.ColumnIndex==7) && btnActualizar.Enabled==true)
                {
                    string XIP="";
                    XIP =""+ dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                    double suma = 0.0, total = 0.0, suma1 = 0.0, total1 = 0.0;

                    if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null || dataGridView1.Rows[e.RowIndex].Cells[0].Value == DBNull.Value || XIP =="0,000000")
                    {

                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = dataGridView1.Rows[e.RowIndex].Cells[7].Value;

                        sumaTotalInge=sumaTotalInge + Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                        suma = acumulado + sumaTotalInge;
                       
                        total1 = 0.01;
                        double nueva = 0.0;


                        nueva = ((suma) / Convert.ToDouble(ValorPorcentaje)) * 100;

                        txtEstimadoXCliente.Text = "" + sumaTotalInge;

                        txtTotalRecIn.Text = (suma).ToString("C2");
              
                        txtCumplimiento.Text =nueva.ToString("#.00")+"%";




                    }
                    else
                    {

                        sumaTotalInge=sumaTotalInge - Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                        suma = acumulado+sumaTotalInge;

                        total1 = 0.01;
                        double nueva = 0.0;


                        nueva = ((suma) / Convert.ToDouble(ValorPorcentaje)) * 100;

                        txtEstimadoXCliente.Visible=false;

                        txtTotalRecIn.Text = (suma).ToString("C2");

                        txtCumplimiento.Text = nueva.ToString("#.00") + "%";

                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = null;



                    }
                }


            }

            catch (Exception ex)
            {

                MessageBox.Show("CSS:L893." + ex.Message, "Mensaje de Sistema", MessageBoxButtons.OK);
            }

        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string stringUpdate, ValorP="", docentry="";
            stringUpdate = "";
            

            try
            {


                for (int a = 0; a < dataGridView1.Rows.Count; a++)
                {
                    if (dataGridView1.Rows[a].Cells[1].Value == null || dataGridView1.Rows[a].Cells[1].Value==DBNull.Value)
                    {
                        
                    }
                    else
                    {



                        if (dataGridView1.Rows[a].Cells[1].Value == null || dataGridView1.Rows[a].Cells[1].Value == DBNull.Value)
                        {
                            docentry = "0.0";
                        }
                        else
                        {
                            docentry = dataGridView1.Rows[a].Cells[1].Value.ToString();

                        }

                        if (dataGridView1.Rows[a].Cells[0].Value == null|| dataGridView1.Rows[a].Cells[0].Value == DBNull.Value)
                        {

                           // ValorP = "0.0";

                        }

                        else
                        {

                            ValorP = dataGridView1.Rows[a].Cells[0].Value.ToString();
                            stringUpdate = "UPDATE  [@CSS_ESTIMADO_REC] SET   U_CSS_DocUpdate= '" +
                               DateTime.Now.Date.ToString("yyyy/MM/dd") + "',U_CSS_FECHA_ESTIMA= '" +
                               DateTime.Now.Date.ToString("yyyy/MM/dd") + "',U_CSS_ValorP =" + Convert.ToDouble(ValorP) + " where Code=" + docentry;

                            ClaseDatos.nonQuery(stringUpdate);


                            stringUpdate = "select CODE from [@CSS_ESTIMADO_REC_H] WHERE U_CSS_IDFACTURA=" + docentry + " AND U_CSS_MES_ESTIMADO='" + DateTime.Now.Date.ToString("yyyy/MM")+"/01'";
                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            IDataReader  misDatos = ClaseDatos.procesaDataReader (stringUpdate);
                            string idCodeHist = "";
                            
                            while (misDatos.Read ()){

                                 idCodeHist =""+ misDatos.GetValue(0); 
                            }
                            ClaseDatos.SqlConn.Close();
                            

                            if(idCodeHist==null ||idCodeHist =="" ){

                                stringUpdate = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_ESTIMADO_REC_H]";
                                idCodeHist = ClaseDatos.scalarStringSql(stringUpdate);
                                string insertHist = "insert into [@CSS_ESTIMADO_REC_H](code,Name,U_CSS_IDFACTURA,U_CSS_MES_ESTIMADO,U_CSS_VALOR_ESTIM) VALUES(" + idCodeHist + ",'"
                                   + idCodeHist + "'," + docentry + ",'" + DateTime.Now.Date.ToString("yyyy/MM") + "/01','" + Convert.ToDouble(ValorP) + "')";

                                ClaseDatos.nonQuery(insertHist);
                            
                            }
                            else {


                                string updateHist = "UPDATE [@CSS_ESTIMADO_REC_H] SET U_CSS_VALOR_ESTIM='" + Convert.ToDouble(ValorP) 
                                    + "' WHERE code= " + idCodeHist + " AND U_CSS_IDFACTURA=" + docentry + " AND U_CSS_MES_ESTIMADO='" 
                                    + DateTime.Now.Date.ToString("yyyy/MM") + "/01'";

                                ClaseDatos.nonQuery(updateHist);


                            }

                           



                        }

                    }

                   

                }

                acumulado =acumulado+ sumaTotalInge;
                sumaTotalInge = 0;


             MessageBox.Show("Datos Actualizados Correctamente ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
             btnIniciar_Click(sender, e);



            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Algun dato no se pudo enlazar CSS:L1187 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

           
        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            btnActualizar.Enabled = false;
            txtEstimadoXCliente.Visible = false;
            txtDeudaXCliente.Visible = false;
            label4.Visible = false;
            label7.Visible = false;
            label9.Visible = true;
            label10.Visible = true;

            txtTotalRecIn.Visible = true;
            txtCumplimiento.Visible = true;
            
            Click = 2;

            if (Click > 1)
            {


                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Hide();
                dataGridView1.Show();

                cargarGrilla(1);


            }
            

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) { cbxRepVentas_DropDown(sender, e); }
            else {

                cbxRepVentas.Enabled = false;
            }
            
        }
        private void verCalculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void frmEstimadosRecaudo_KeyPress(object sender, KeyPressEventArgs e)
        {



        }

        private void frmEstimadosRecaudo_KeyDown(object sender, KeyEventArgs e)
        {

  



        }



        private void cbxClienteInicial_KeyPress(object sender, KeyPressEventArgs e)
        {



                if (e.KeyChar == 161) // ¡
                {

                    //proceso para actualizar facturas pagas contra las adeudadas

                    try
                    {

                        string miConsulta = "SELECT docnum from oinv where ((DocTotal-paidtodate)<=0)";

                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);

                        for (int a = 0; a < misDatos.Tables[0].Rows.Count; a++)
                        {
                            string update = "update   [@CSS_ESTIMADO_REC] set U_CSS_PAGADA='Y' where U_CSS_IDFatcura= " + misDatos.Tables[0].Rows[a][0];
                            ClaseDatos.nonQuery(update);






                        }

                        MessageBox.Show("Datos Actualizados correctamente. Facturas Pagadas");

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(" Error actualizando las facturas pagadas en estimados de recaudo " + ex.Message);


                    }





                




            }



        }

      


    }

    }

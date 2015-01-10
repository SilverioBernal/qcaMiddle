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
    public partial class frmEstadoPedidos : Form
    {

      public  int x = 0, cont = 0; // la variable CONT Activa los controles select  y la variable X evalua los casos de la consulta
      int activado = 0;

        public frmEstadoPedidos()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }
       
        private void frmEstadoPedidos_Load(object sender, EventArgs e)
        {

            cont = 0;

            try
            {
                if (cont<=0){

                    string miConsulta = "SELECT '0' AS Code,'--Seleccione Representante--' AS Memo UNION SELECT slpcode,MEMO AS Empleado FROM OSLP ORDER BY MEMO " +
                                    "SELECT '0' AS Code,'--Seleccione un Cliente--' AS CardName UNION SELECT CardCode,CardName  FROM OCRD where cardtype='c' ORDER BY CardName ";

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);
                ClaseDatos.SqlUnConnex();
                this.cbxRepVentas.DisplayMember = "Memo";
                this.cbxRepVentas.ValueMember = "Code";
                this.cbxRepVentas.DataSource = misDatos.Tables[0];
                this.cbxRepVentas.SelectedIndex = this.cbxRepVentas.FindString("--Seleccione Representante--");
                this.cbxNomCliente.DisplayMember = "CardName";
                this.cbxNomCliente.ValueMember = "CarCode";
                this.cbxNomCliente.DataSource = misDatos.Tables[1];
                this.cbxNomCliente.SelectedIndex = this.cbxNomCliente.FindString("--Seleccione un Cliente--");
                this.cbxCodCliente.DisplayMember = "Code";
                this.cbxCodCliente.ValueMember = "Code";
                this.cbxCodCliente.DataSource = misDatos.Tables[1];
                this.cbxCodCliente.SelectedIndex = this.cbxNomCliente.FindString("0");

                cont = 1;
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        // Llena los datosde la grilla
        private void cbxRepVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cont >= 1)
            {
                
                string miConsulta;

                dgvResultados.Rows.Clear();

                /// Selecciona en la ORDEN el nombre del cliente,Numero de Orden,Fecha de generacion,Hora de generacion,Fecha de compromiso,Codigo de
                /// la empresa,Codigo del empleado. Teniendo en cuenta el nombre del representante de ventas y llena la grilla principal.


                switch (x)
                {
                    case 0:

                        if (mskTFechaGeneracion.Text == "    /  /")
                        {
                            miConsulta = "select Distinct T0.cardname,T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.SlpCode from ORDR T0" +
                                " inner join RDR1 T1 on T0.DocEntry=T1.DocEntry where  T0.CANCELED='N' AND T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "')";
                        }
                        else {
                            miConsulta = "select Distinct T0.cardname,T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.SlpCode from ORDR T0" +
                           " inner join RDR1 T1 on T0.DocEntry=T1.DocEntry where T0.DocDate='"+mskTFechaGeneracion.Text+"' and  T0.CANCELED='N' AND T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "')"; 
                        
                        };

                       
                        break;
                    case 1:

                        if (mskTFechaGeneracion.Text == "    /  /")
                        {
                            miConsulta = "select Distinct T0.cardname,T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.SlpCode from ORDR T0 " +
                                "inner join RDR1 T1 on T0.DocEntry=T1.DocEntry where T0.CANCELED='N' AND T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "') and T0.cardcode='" + cbxCodCliente.Text + "'";
                        }
                        else
                        {
                            miConsulta = "select Distinct T0.cardname,T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.SlpCode from ORDR T0 " +
                                "inner join RDR1 T1 on T0.DocEntry=T1.DocEntry where T0.DocDate='" + mskTFechaGeneracion.Text + "' and  T0.CANCELED='N' AND T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "') and T0.cardcode='" + cbxCodCliente.Text + "'";
                        }

                       
                        break;
                    default:
                        miConsulta = "select Distinct T0.cardname,T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.SlpCode from ORDR T0" +
                         " inner join RDR1 T1 on T0.DocEntry=T1.DocEntry where T0.CANCELED='N' AND  T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "')"; 

                        MessageBox.Show("Si estos datos no se aplican a lo que usted busca por favor haga Click en:   Nueva Consulta ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      
                        break;
                }
                x = x + 1;





                                                    try
                                                    {
                                                        string Hora1 = "", horap = "";
                                                        int lee = 0;
                                                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                                                        IDataReader misDatos = ClaseDatos.procesaDataReader(miConsulta);

                                                        while (misDatos.Read())
                                                        {
                                                            lee = 1;
                                                            object[] matriz = new object[7];
                                                            matriz[0] = misDatos.GetValue(0).ToString(); //nombre de cliente
                                                            matriz[1] = misDatos.GetValue(1).ToString();//numero cliente
                                                            matriz[2] = misDatos.GetValue(2).ToString();//numero pedido
                                                            matriz[3] = misDatos.GetValue(3).ToString().Substring(0,10);//fecha
                                                            Hora1 = misDatos.GetValue(4).ToString();//hora
                                                            if (Hora1.Length > 3) { horap = Hora1.Substring(0, 2) + ":" + Hora1.Substring(2, 2); }
                                                            else { horap = Hora1.Substring(0, 1) + ":" + Hora1.Substring(1, 2); }
                                                            
                                                            matriz[4] = horap;
                                                            matriz[5] = misDatos.GetValue(5).ToString();//fecha compromiso
                                                           
                                                            matriz[6] = misDatos.GetValue(6).ToString();//numero representantes
                                                            dgvResultados.Rows.Add(matriz);
                                                            activado = 1;

                                                        }

                                                        if (lee != 1) {


                                                            MessageBox.Show("El filtro no arrojo datos ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        
                                                        }

                                                        ClaseDatos.SqlUnConnex();

                                                    }
                                                    catch (Exception miExcepcion)
                                                    {
                                                        MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }

           

            }
        }

        private void cbxNomCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

           /// Activa le control codigo cliente para que busque el empleado.Teniendo en cuenta que los dos controles estan enlazados
           /// en la consulta del Evento frmEstadoPedidos_Load
           
            this.cbxCodCliente_SelectedIndexChanged(sender,e);


        }

        private void cbxCodCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cont >= 1)
            {

                /// Selecciona en la ORDEN el nombre del cliente,Numero de Orden,Fecha de generacion,Hora de generacion,Fecha de compromiso,Codigo de
                /// la empresa,Codigo del empleado. Teniendo en cuenta el codigo del Cliente y llena la grilla principal.

               
                string miConsulta;

                dgvResultados.Rows.Clear();


                switch (x)
                {
                    
                   
                    
                    case 0:
                        miConsulta = "select Distinct T0.cardname,T0.cardcode, T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.Slpcode FROM ORDR T0 inner join RDR1 T1 on T0.DocEntry=T1.DocEntry" +
                        " Where T0.cardcode='" + cbxCodCliente.Text + "' and T0.CANCELED='N' ORDER BY T0.cardname ";
                        break;
                    case 1:
                        miConsulta = "select Distinct T0.cardname,T0.cardcode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.Slpcode FROM ORDR T0 inner join RDR1 T1 on T0.DocEntry=T1.DocEntry" +
                        " Where T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "') and T0.cardcode='" + cbxCodCliente.Text + "' and T0.CANCELED='N' ORDER BY T0.cardname";
                        break;
                    case 2:
                        miConsulta = "select Distinct T0.cardname,T0.cardcode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.Slpcode FROM ORDR T0 inner join RDR1 T1 on T0.DocEntry=T1.DocEntry" +
                        " Where T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "') and T0.cardcode='" + cbxCodCliente.Text + "' and T0.CANCELED='N' ORDER BY T0.cardname";
                        break;
                    default:
                        miConsulta = "select Distinct T0.cardname,T0.cardcode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.Slpcode FROM ORDR T0 inner join RDR1 T1 on T0.DocEntry=T1.DocEntry " +
                         " Where T0.cardcode='" + cbxCodCliente.Text + "' and T0.CANCELED='N' ORDER BY T0.cardname";

                        MessageBox.Show("Si estos datos no se aplican a lo que usted busca por favor haga Click en:   Nueva Consulta ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }

                x = x + 1;

                                                try
                                                {
                                                    string Hora1 = "", horap = "";
                                                    int lee = 0;
                                                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                                                    IDataReader misDatos = ClaseDatos.procesaDataReader(miConsulta);

                                                    while (misDatos.Read())
                                                    {
                                                        lee = 1;
                                                        object[] matriz = new object[7];
                                                        matriz[0] = misDatos.GetValue(0).ToString(); //nombre de cliente
                                                        matriz[1] = misDatos.GetValue(1).ToString();//numero cliente
                                                        matriz[2] = misDatos.GetValue(2).ToString();//numero pedido
                                                        matriz[3] = misDatos.GetValue(3).ToString().Substring(0,10);//fecha
                                                        Hora1 = misDatos.GetValue(4).ToString();//hora
                                                        if (Hora1.Length > 3) { horap = Hora1.Substring(0, 2) + ":" + Hora1.Substring(2, 2); }
                                                        else { horap = Hora1.Substring(0, 1) + ":" + Hora1.Substring(1, 2); }
                                                        matriz[4] = horap;
                                                        matriz[5] = misDatos.GetValue(5).ToString();//fecha compromiso

                                                        matriz[6] = misDatos.GetValue(6).ToString();//numero representantes
                                                        dgvResultados.Rows.Add(matriz);
                                                        activado = 1;

                                                    }

                                                    if (lee != 1)
                                                    {


                                                        MessageBox.Show("El filtro no arrojo datos ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                    }

                                                    ClaseDatos.SqlUnConnex();

                                                }
                                                catch (Exception miExcepcion)
                                                {
                                                    MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }



            }
        }

        private void TbxNumPedido_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {

                if (cont >= 1)
                {
                                     try
                                        {
                                            dgvResultados.Rows.Clear();


                                            string miConsulta = "select Distinct T0.cardname,T0.cardcode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.Slpcode FROM ORDR T0 inner join RDR1 T1 on T0.DocEntry=T1.DocEntry Where T0.DocNum='" + TbxNumPedido.Text + "' and CANCELED='N'";

                                            string Hora1 = "", horap = "";
                                            int lee = 0;
                                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                                            IDataReader misDatos = ClaseDatos.procesaDataReader(miConsulta);

                                            while (misDatos.Read())
                                            {
                                                lee = 1;
                                                object[] matriz = new object[7];
                                                matriz[0] = misDatos.GetValue(0).ToString(); //nombre de cliente
                                                matriz[1] = misDatos.GetValue(1).ToString();//numero cliente
                                                matriz[2] = misDatos.GetValue(2).ToString();//numero pedido
                                                matriz[3] = misDatos.GetValue(3).ToString().Substring(0,9);//fecha
                                                Hora1 = misDatos.GetValue(4).ToString();//hora
                                                if (Hora1.Length > 3) { horap = Hora1.Substring(0, 2) + ":" + Hora1.Substring(2, 2); }
                                                else { horap = Hora1.Substring(0, 1)+":" + Hora1.Substring(1, 2); }

                                                matriz[4] = Convert.ToDateTime(horap).ToString("t");
                                                matriz[5] = misDatos.GetValue(5).ToString().Substring(0, 9);//fecha compromiso
                                                matriz[6] = misDatos.GetValue(6).ToString();//numero representantes
                                                dgvResultados.Rows.Add(matriz);
                                                activado = 1;

                                            }





                                            ClaseDatos.SqlUnConnex();
                                            if (lee != 1)
                                            {


                                                MessageBox.Show(" Verifique que el numero de Orden existe o que no esta cancelada ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            }

                                        }
                                        catch (Exception miExcepcion)
                                        {
                                            MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
               
                   }


                cbxCodCliente.Enabled = false;
                cbxRepVentas.Enabled = false;
                cbxNomCliente.Enabled = false;





            }
        }

        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

            ///Este codigo es el mas largo, pues organiza los datos de detalle que se van a presentar.

            if (activado == 1)
            {

                if (dgvResultados.Rows[e.RowIndex].Cells[0].Value == null || dgvResultados.Rows[e.RowIndex].Cells[0].Value == DBNull.Value) { }
                
                else{

                this.tbxDetalle.Location = new System.Drawing.Point(12, 4);

                this.groupBoxDetalle.Location = new System.Drawing.Point(12, 110);
                groupBoxDetalle.Visible = true;


                DataGridViewRow dtRFIla;
                string NomCLiente, NumEntrega, NumOrden, FechaOrden, HoraOrden, FechaCOmpCLiente, NumCliente, NumRep,CodRep,NomRep;
                dtRFIla = dgvResultados.CurrentRow; // Captura la Fila seleccionada por el cliente
                NumOrden = "0";
                NumCliente = "0";
                NomCLiente = "";
                NumEntrega = "";
                NumRep = "0";
                CodRep = "";
                NomRep = "";

               

                #region Valida y asigna los valores que han sido seleccionados
                foreach (DataGridViewCell cell in dtRFIla.Cells)
                {
                    if (cell.OwningColumn.Name == "Numero_Orden")
                    { NumOrden = cell.Value.ToString(); }
                    else
                    {
                        if (cell.OwningColumn.Name == "Fecha_Orden")
                        { FechaOrden = cell.Value.ToString(); }
                        else
                        {
                            if (cell.OwningColumn.Name == "Hora_Orden")
                            { HoraOrden = cell.Value.ToString(); }
                            else
                            {
                                if (cell.OwningColumn.Name == "Fecha_Comprometida")
                                { FechaCOmpCLiente = cell.Value.ToString(); }
                                else
                                {

                                    if (cell.OwningColumn.Name == "Numero_Cliente")
                                    { NumCliente = cell.Value.ToString(); }
                                    else
                                    {
                                        if (cell.OwningColumn.Name == "Nom_Cliente")
                                        { NomCLiente = cell.Value.ToString(); }
                                        else
                                        {

                                            if (cell.OwningColumn.Name == "Num_Rep")
                                            { NumRep = cell.Value.ToString(); }

                                        }
                                    }
                                }

                            }

                        }
                    }
                }

                #endregion

                if (NumOrden != "0")
                {

                    #region tries

                    //Carga los datos de la Cotizacion si existe

                    try
                    {
                        string Hora1,horap;
                        string miConsultaCOT = "SELECT DISTINCT T1.DocNum,T1.DocDate,T1.DocTime FROM OQUT T1 inner join QUT1 T1_Lines ON T1.DocEntry=T1_Lines.DocEntry" +
                                            " WHERE  T1_Lines.TrgetEntry='" + NumOrden + "'";
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        IDataReader misDatos = ClaseDatos.procesaDataReader(miConsultaCOT);

                        while (misDatos.Read())
                        {


                            txtCotizacion.Text += "" + misDatos.GetValue(0).ToString();
                            txtCotizacion.Text += " generada el: " + misDatos.GetValue(1).ToString().Substring(0, 10);
                            Hora1 = misDatos.GetValue(4).ToString();//hora
                            if (Hora1.Length > 3) { horap = Hora1.Substring(0, 2) + ":" + Hora1.Substring(2, 2); }
                            else { horap = Hora1.Substring(0, 1) + ":" + Hora1.Substring(1, 2); }
                            txtCotizacion.Text += "a las: " + Convert.ToDateTime(horap).ToString("t");
                            


                        }

                        ClaseDatos.SqlUnConnex();

                    }
                    catch (Exception miExcepcion)
                    {
                        MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    //// Datos de la Entrega. Aqui tambien los datos del recibido pues esta atado a la entrega.

                    try
                    {
                        int verifica = 0;
                        string Hora1="",horap="", miConsultaENT = "SELECT DISTINCT T2.DocNum,T2.DocDate,T2.DocTime,U_CSS_RECIBIDO,U_CSS_RECIBIDO_DOC,U_CSS_FECHA_RECI FROM ODLN T2 inner join DLN1 T2_Lines ON T2.DocEntry=T2_Lines.DocEntry" +
                                            " WHERE  T2_Lines.BaseRef='" + NumOrden + "'";

                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        IDataReader misDatos = ClaseDatos.procesaDataReader(miConsultaENT);

                        while (misDatos.Read())
                        {

                            verifica = 2;

                            txtEntrega.Text = " " + misDatos.GetValue(0).ToString();
                            NumEntrega = misDatos.GetValue(0).ToString();
                            txtEntrega.Text += "  generada el: " + misDatos.GetValue(1).ToString().Substring(0, 10);
                            Hora1 = misDatos.GetValue(2).ToString();
                            if (Hora1.Length > 3) { horap = Hora1.Substring(0, 2) + ":" + Hora1.Substring(2, 2); }
                            else { horap = Hora1.Substring(0, 1) + ":" + Hora1.Substring(1, 2); }
                            txtEntrega.Text += "a las: " + Convert.ToDateTime(horap).ToString("hh:mm:ss tt");


                            //Datos de Recibido
                            if (misDatos.GetValue(3).ToString() == "" || misDatos.GetValue(3).ToString() == null)
                            {
                                txtRecibido.Text = "No se ha recibido por el Cliente";


                            }
                            else
                            {
                                txtRecibido.Text = "Entregado a : " + misDatos.GetValue(3).ToString();
                                txtRecibido.Text += " Identificado con: " + misDatos.GetValue(4).ToString();
                                txtRecibido.Text += " el: " + misDatos.GetValue(5).ToString();

                            }

                        }

                        if (verifica<1) {

                            MessageBox.Show("No se han generado datos para esta Orden");
                        
                        }

                        ClaseDatos.SqlUnConnex();

                    }
                    catch (Exception miExcepcion)
                    {
                        MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    ///datos de factura
                    ///
                    string numFactura = "";

                    if (NumEntrega.Length > 1)
                    {

                        try
                        {
                            string Hora1 = "", horap = "";
                            string miConsultaFAC = "SELECT DISTINCT T1.DocNum,T1.DocDate,T1.DocTime FROM OINV T1 inner join INV1 T1_Lines ON T1.DocEntry=T1_Lines.DocEntry" +
                                                " WHERE  T1_Lines.BaseRef='" + NumEntrega + "'";
                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            IDataReader misDatos = ClaseDatos.procesaDataReader(miConsultaFAC);

                            while (misDatos.Read())
                            {


                                txtFactura.Text = " " + misDatos.GetValue(0).ToString();
                                numFactura = misDatos.GetValue(0).ToString();
                                txtFactura.Text += " generada el: " + misDatos.GetValue(1).ToString().Substring(0, 10);
                                Hora1 = misDatos.GetValue(2).ToString();
                                if (Hora1.Length > 3) { horap = Hora1.Substring(0, 2) + ":" + Hora1.Substring(2, 2); }
                                else { horap = Hora1.Substring(0, 1) + ":" + Hora1.Substring(1, 2); }
                                txtFactura.Text += "a las: " + Convert.ToDateTime(horap).ToString("hh:mm:ss tt"); ;



                            }

                            ClaseDatos.SqlUnConnex();

                        }
                        catch (Exception miExcepcion)
                        {
                            MessageBox.Show("Error al cargar los datos en la factura: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        ///Datos del documento de transporte

                        try
                        {

                            string miConsultaENT = "select T2.U_CSS_Nombre,T2.U_CSS_Cedula,T4.Code,T3.U_CSS_Razon_Social,T1.U_CSS_Fecha_Impresio from [@CSS_MOVIMIENTO] T1 inner join " +
                             "   [@CSS_CONDUCTOR] T2 on T1.U_CSS_Conductor=T2.Code inner join [@CSS_TRANSPORTADORA] T3 on T1.U_CSS_Transportadora=T3.Code inner join" +
                             " [@CSS_VEHICULO] T4 on T1.U_CSS_Vehiculo=T4.Code where T1.U_CSS_Documento_SAP=" + NumEntrega + " and T1.U_CSS_Estado='Activo'";


                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            IDataReader misDatos = ClaseDatos.procesaDataReader(miConsultaENT);

                            while (misDatos.Read())
                            {


                                txtDocTransp.Text = "Transportado por: " + misDatos.GetValue(0).ToString();
                                txtDocTransp.Text += " CC: " + misDatos.GetValue(1).ToString();
                                txtDocTransp.Text += "  Placa: " + misDatos.GetValue(2).ToString();
                                txtDocTransp.Text += "  Empresa: " + misDatos.GetValue(3).ToString();
                                txtDocTransp.Text += " Impreso el:  " + misDatos.GetValue(4).ToString().Substring(0, 4) + "/" + misDatos.GetValue(4).ToString().Substring(4, 2) + "/" + misDatos.GetValue(4).ToString().Substring(6, 2) + "";


                            }

                            ClaseDatos.SqlUnConnex();

                        }
                        catch (Exception miExcepcion)
                        {
                            MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        ////





                        ///Datos del representante

                        try
                        {

                            string miConsultaEMP = "SELECT Memo,SlpName from OSLP where SlpCode=" + NumRep + "";
                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            IDataReader misDatos = ClaseDatos.procesaDataReader(miConsultaEMP);

                            while (misDatos.Read())
                            {
                                NomRep = misDatos.GetValue(0).ToString();
                                CodRep = " Codigo: " + misDatos.GetValue(1).ToString();

                            }

                            ClaseDatos.SqlUnConnex();

                        }
                        catch (Exception miExcepcion)
                        {
                            MessageBox.Show("Error al cargar los datos en la factura: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }



                    ///
                    try
                    {

                        string miConsultaDetalle = "SELECT T0_Lines.itemcode,T0_Lines.quantity,T0_Lines.dscription,T0_Lines.price, T0.Address2,T0.CardName from ordr T0 inner join rdr1 T0_Lines on T0_Lines.DocEntry=T0.DocEntry  " +
                                            " where T0.DocNum=" + NumOrden + "";
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        IDataReader misDatos = ClaseDatos.procesaDataReader(miConsultaDetalle);

                        while (misDatos.Read())
                        {


                            object[] matriz = new object[4];
                            matriz[0] = misDatos.GetValue(0).ToString();//ItemCOde
                            matriz[1] = misDatos.GetValue(1).ToString();//Cantidad
                            matriz[2] = misDatos.GetValue(2).ToString();//Descripcion
                            matriz[3] = misDatos.GetValue(3).ToString();//Precio
                            txtDetalleEntrega.Text = "Entregar en: " + misDatos.GetValue(4).ToString();// Direccion entrega
                            dgvDetalles.Rows.Add(matriz);

                            dgvDetalles.Columns[1].DefaultCellStyle.Format = "N2";
                            dgvDetalles.Columns[3].DefaultCellStyle.Format = "C2";

                        }
                        dgvDetalles.Columns[1].DefaultCellStyle.Format = "N2";
                        dgvDetalles.Columns[3].DefaultCellStyle.Format = "C2";
                        

                        ClaseDatos.SqlUnConnex();

                    }
                    catch (Exception miExcepcion)
                    {
                        MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }




                    #endregion tries
                }
                else
                {

                    MessageBox.Show("Sin Datos Para Mostrar: ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }

                //Mas Datos
                tbxDetalle.Text = "Numero de Orden : " + NumOrden + "\tCliente :" + NomCLiente 
                    + "\t  Empleado:  "+NomRep;
                               

                btnNuevaConsulta.Visible = false;
                dgvDetalles.Visible = true;
                dgvResultados.Visible = false;
                TbxNumPedido.Visible = false;
                cbxCodCliente.Visible = false;
                cbxNomCliente.Visible = false;
                cbxRepVentas.Visible = false;
                btnResultados.Visible = true;
                btnResultados.Text = "<< Volver";
                tbxDetalle.Visible = true; //Cuadro de texto que permite ver los detalles seleccionados en la grilla
                tbxDetalle.ReadOnly = true;
                txtCotizacion.Visible = true;
                txtCotizacion.ReadOnly = true;
                txtEntrega.Visible = true;
                txtEntrega.ReadOnly = true;
                txtFactura.Visible = true;
                txtFactura.ReadOnly = true;
                txtRecibido.Visible = true;
                txtRecibido.ReadOnly = true;
                lblDatosCotiza.Visible = true;
                lblDatosEntre.Visible = true;
                lblDetalleEntrega.Visible = true;
                lblDetOrden.Visible = true;
                lblFactura.Visible = true;
                pictureBox1.Visible = true;
                txtDetalleEntrega.Visible = true;
                mskTFechaGeneracion.Visible = false;
                txtDetalleEntrega.ReadOnly = true;
                pictureBox2.Visible = true;
                txtDocTransp.Visible = true;
                lblNumPedido.Visible = false;
                label1.Visible = false;

             }
            }
            else {


                btnNuevaConsulta_Click(sender, e);
            
            }
        }

       /// 
       /// Otros
       /// 


        private void btnNuevaConsulta_Click(object sender, EventArgs e)
        {
            TbxNumPedido.Clear();
            TbxNumPedido.Enabled = true;
            cbxNomCliente.Enabled = true;
            cbxRepVentas.Enabled = true;
            cbxCodCliente.Enabled = true;

            cont = 0;
            x = 0;

            try
            {
                if (cont <= 0)
                {

                    //string miConsulta = "SELECT '0' AS Code,'--Seleccione Representante--' AS Razon UNION SELECT slpcode,MEMO AS Empleado FROM OSLP " +
                    //                    "SELECT '0' AS Code,'--Seleccione un Cliente--' AS NombreCliente UNION SELECT CardCode,CardName  FROM OCRD where cardtype='c' ";

                    //ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    //DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);
                    //ClaseDatos.SqlUnConnex();
                    //this.cbxRepVentas.DisplayMember = "Razon";
                    //this.cbxRepVentas.ValueMember = "Code";
                    //this.cbxRepVentas.DataSource = misDatos.Tables[0];
                    this.cbxRepVentas.SelectedIndex = this.cbxRepVentas.FindString("--Seleccione Representante--");
                    //this.cbxNomCliente.DisplayMember = "CardName";
                    //this.cbxNomCliente.ValueMember = "CardCode";
                    //this.cbxNomCliente.DataSource = misDatos.Tables[1];
                    this.cbxNomCliente.SelectedIndex = this.cbxNomCliente.FindString("--Seleccione un Cliente--");
                    //this.cbxCodCliente.DisplayMember = "Code";
                    //this.cbxCodCliente.ValueMember = "Code";
                    //this.cbxCodCliente.DataSource = misDatos.Tables[1];
                    this.cbxCodCliente.SelectedIndex = this.cbxNomCliente.FindString("0");

                    cont = 1;
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            dgvResultados.Rows.Clear();
            dgvDetalles.Rows.Clear();
            mskTFechaGeneracion.Clear();
            label1.Visible = true;
            lblNumPedido.Visible = true;
        }
        private void btnResultados_Click(object sender, EventArgs e)
        {
            groupBoxDetalle.Visible = false;
            btnNuevaConsulta.Visible = true;
            dgvDetalles.Visible = false;
            dgvResultados.Visible = true;
            TbxNumPedido.Visible = true;
            cbxCodCliente.Visible = true;
            cbxNomCliente.Visible = true;
            cbxRepVentas.Visible = true;
            btnResultados.Visible = false;
            tbxDetalle.Visible = false;
            txtCotizacion.Visible = false;
            txtEntrega.Visible = false;
            lblDatosCotiza.Visible = false;
            lblDatosEntre.Visible = false;
            lblDetOrden.Visible = false;
            lblDetalleEntrega.Visible = false;
            txtDetalleEntrega.Visible = false;
            mskTFechaGeneracion.Visible = true;
          
            pictureBox1.Visible = false;
            txtDocTransp.Visible = false;
            lblFactura.Visible = false;
            txtFactura.Visible = false;
            pictureBox2.Visible = false;
            txtRecibido.Visible = false;

            txtFactura.Clear();
            txtRecibido.Clear();
            txtEntrega.Clear();
            txtCotizacion.Clear();
            txtDocTransp.Clear();
            tbxDetalle.Clear();
            dgvDetalles.Rows.Clear();
            x = 0;

        }

        private void mskTFechaGeneracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {



                if (cont >= 1)
                {
                  
                    string miConsulta;

                    if (mskTFechaGeneracion.Text == "  /  /    ")
                    {

                        mskTFechaGeneracion.Text = DateTime.Today.ToString();
                    }

                    switch (x)
                    {
                        case 1:
                            miConsulta = "set dateformat dmy select Distinct T0.cardname,T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.SlpCode from ORDR T0" +
                            " inner join RDR1 T1 on T0.DocEntry=T1.DocEntry Where T0.DocDueDate='" + mskTFechaGeneracion.Text.Substring(0, 10) + "'";
                          
                            break;
                        case 2:
                            miConsulta = "SET DATEFORMAT dmy  SELECT Distinct T0.cardname, T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.Slpcode FROM ORDR T0" +
                            " inner join RDR1 T1 on T0.DocEntry=T1.DocEntry Where T0.Slpcode in(SELECT slpcode from OSLP where memo='" + cbxRepVentas.Text + "') and T0.DocDueDate ='" + mskTFechaGeneracion.Text.Substring(0, 10) + "'";
                             break;
                         case 3:
                            miConsulta = "SET DATEFORMAT dmy  SELECT Distinct T0.cardname, T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.Slpcode FROM ORDR T0" +
                            " inner join RDR1 T1 on T0.DocEntry=T1.DocEntry Where T0.CardCode='"+cbxCodCliente.Text+"' and T0.DocDueDate ='" + mskTFechaGeneracion.Text.Substring(0, 10) + "'";
                           break;

                        default:
                            miConsulta = "select Distinct T0.cardname,T0.CardCode,T0.DocNum,T0.DocDate,T0.DocTime,T0.DocDueDate,T0.SlpCode from ORDR T0" +
                           " inner join RDR1 T1 on T0.DocEntry=T1.DocEntry Where T0.DocDueDate='" + mskTFechaGeneracion.Text.Substring(0, 10) + "'";

                            MessageBox.Show("Si estos datos no se aplican a lo que usted busca por favor haga Click en:   Nueva Consulta ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                    }

                      x = x + 1;

#region a


                    try
                    {

                        string Hora1 = "", horap = "";
                        int lee = 0;
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        IDataReader misDatos = ClaseDatos.procesaDataReader(miConsulta);

                        while (misDatos.Read())
                        {
                            lee = 1;
                            object[] matriz = new object[7];
                            matriz[0] = misDatos.GetValue(0).ToString(); //nombre de cliente
                            matriz[1] = misDatos.GetValue(1).ToString();//numero cliente
                            matriz[2] = misDatos.GetValue(2).ToString();//numero pedido
                            matriz[3] = misDatos.GetValue(3).ToString().Substring(0, 10);//fecha
                            Hora1 = misDatos.GetValue(4).ToString();//hora
                            if (Hora1.Length > 3) { horap = Hora1.Substring(0, 2) + ":" + Hora1.Substring(2, 2); }
                            else { horap = Hora1.Substring(0, 1) + ":" + Hora1.Substring(1, 2); }
                            matriz[4] = horap;
                            matriz[5] = misDatos.GetValue(5).ToString();//fecha compromiso

                            matriz[6] = misDatos.GetValue(6).ToString();//numero representantes
                            dgvResultados.Rows.Add(matriz);
                            activado = 1;
                          

                        }

                        

                        if (lee != 1)
                        {


                            MessageBox.Show("El filtro no arrojo datos ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        ClaseDatos.SqlUnConnex();

                    }
                    catch (Exception miExcepcion)
                    {
                        MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


#endregion


                }
            
            
            
            
            }

        

     

        
               

        
     
        
        }

    

        

        
   }
        
    
}
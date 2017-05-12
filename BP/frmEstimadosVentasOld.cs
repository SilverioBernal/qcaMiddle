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
    public partial class frmEstimadosVentasOld : Form
    {
        #region Variables Globales

        DataSet dsItems = null;
        DataSet dsPr = null;
        DataSet dsPr2 = null;
        int idPres = 0,idPresNewYear=0, indice = 0, total = 0, posi = 0, slpcode = 0,_Click=0,_t=0,w=0;
        double total1 = 0.0;
        System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<BP.CityAndValue>> _dicAnualRetorna = new Dictionary<int, List<BP.CityAndValue>>();
        
        #endregion

        #region Variables del control Especial BP

        DataTable dtCitySource; // Datable que contiene las ciudades
        DataTable dtProducts;// Datatable que contiene los productos
        ArrayControlQCA m_popedContainerForButton;
        PoperContainer m_poperContainerForButton;
        YearArray ArregloAnual;
        List<YearArray> ArrResult;
        Dictionary<int, List<CityAndValue>> dicAnual = new Dictionary<int, List<CityAndValue>>();
        //  Dictionary<int, List<CityAndValue>> _dicAnualRetorna = new Dictionary<int, List<CityAndValue>>();
        DataTable _dicAnualSetTable = new DataTable();
        Dictionary<string, Dictionary<int, List<CityAndValue>>> dicAnualByProduct = new Dictionary<string, Dictionary<int, List<CityAndValue>>>();
        int counter;
        int _mesLockDesde, _mesLockHasta;
        int[] mesesActivos;
        List<CityAndValue> copyCityAndValue;
        double _totalCopyCityAndValue;
        string _productoID, _valor = "";

        #endregion

        public frmEstimadosVentasOld()
        {
            InitializeComponent();

            m_popedContainerForButton = new ArrayControlQCA();
            m_poperContainerForButton = new PoperContainer(m_popedContainerForButton);
            m_popedContainerForButton.btnOk.Click += new System.EventHandler(this.buttonListo_Click);
            counter = 0;
            _productoID = null;

            this.WindowState = FormWindowState.Maximized;
           
        }

        private void frmEstimadosVentas_Load(object sender, EventArgs e)
        {
            bloqueaFormulario();
            validaIngVentas();

        }

        #region Bloquea el Formulario,Valida el Ingeniero de ventas, Lista los clientes y los productos

        private void bloqueaFormulario() {


            #region  BLOQUEA


            string bloquea = "";

            
            try
            {

                bloquea = "select U_CSS_ESTIM_FECHA from [@CSS_PRESUPUESTOS_PA] where U_CSS_MES=" + DateTime.Today.Month;

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet ultimo = ClaseDatos.procesaDataSet(bloquea);

                bloquea = "" + ultimo.Tables[0].Rows[0][0];


            }

            catch (Exception er)
            {
                MessageBox.Show(" La fecha de corte del formulario impide que se pueda habilitar ", "Mensaje del Sistema");
                toolStripStatusLabel1.Text = er.Message;
                ClaseDatos.SqlConn.Close();
            }

            ClaseDatos.SqlConn.Close();


            if (DateTime.Now.Date > Convert.ToDateTime(bloquea).Date)
            {
                cboCustomers.Enabled = false;
                txtYear.Enabled = false;
                txtItems.Enabled = false;
                btnSave.Enabled = false;
                txtItemName.Enabled = false;


            }
            #endregion
        
        }

        private void validaIngVentas() {

            #region Valida que el usuario sea Ingeniero de Ventas

            try
            {
                ClaseDatos.validSalesEmploye(loging.usrCode);

                if (ClaseDatos.salesEmpl == true)
                {

                    try
                    {

                        string miConsulta = "select salesPrson from ohem where userid = (select internal_k from ousr where user_code='" + loging.usrCode + "')";
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        DataSet empleado = ClaseDatos.procesaDataSet(miConsulta);

                        miConsulta = "select SlpCode,SlpName from OSLP where SlpCode=" + empleado.Tables[0].Rows[0][0];
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        empleado.Reset();
                        empleado = ClaseDatos.procesaDataSet(miConsulta);

                        cbxSalesName.Text = " " + empleado.Tables[0].Rows[0].ItemArray[1];
                        slpCustomers();
                        slpcode = Convert.ToInt32(empleado.Tables[0].Rows[0].ItemArray[0]);



                        if (DateTime.Now.Month == 12)
                        { txtYear.Text = DateTime.Now.AddYears(1).Year.ToString(); }
                        else { txtYear.Text = (DateTime.Today.Year).ToString(); }


                        showAll_Click(new object(), new EventArgs());

                    }

                    catch (Exception ex)
                    {
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
        
        }


       

        private void slpCustomers()
        {

            //Carga los clientes de el empleado en el cbxCostumers

            DataSet dsCostumer = null;
            try
            {
                if (w == 0) {

                    string consulta = "";
                    dsCostumer = ClaseDatos.procesaDataSet(
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
                
                }

                else {

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
                    "		WHERE SLPCODE =		" + slpcode +
                    "	)" +
                    "union " +
                    "select cardcode, cardname " +
                    "from ocrd " +
                    "where slpcode = " + slpcode
                   +
                    "	 ORDER BY CARDNAME";
                    dsCostumer = ClaseDatos.procesaDataSet(consulta 
                   
                    );
                
                }
                cboCustomers.DataSource = dsCostumer.Tables[0];
                cboCustomers.DisplayMember = "cardname";
                cboCustomers.ValueMember = "cardcode";
                cbxCostumersCode.DataSource = dsCostumer.Tables[0];
                cbxCostumersCode.DisplayMember = "cardcode";


            }
            catch (Exception er)
            {

                toolStripStatusLabel1.Text = er.Message;
            }
        }
        private void showAll_Click(object sender, EventArgs e)
        {
            try
            {
                //Integracion del control Productos
               dsItems = ClaseDatos.procesaDataSet("select itemname, itemcode from oitm order by itemname");
               string sucursales = "Select Code,Name from [@CSS_SUCURSALES] order by Name";
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet misDatos = ClaseDatos.procesaDataSet(sucursales);

                cbxProducts.DataSource = dsItems.Tables[0];
                cbxProducts.ValueMember = "itemName";
                cbxProducts.ValueMember = "itemCode";

                if (misDatos != null)
                {
                    m_popedContainerForButton.CbSource = misDatos.Tables[0];
                }



              
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Mesnaje del Sistema");
            
            }
           
        }
        private void showAllByBp_Click(object sender, EventArgs e)
        {
            //Integracion del control Productos
            string sucursales = "Select Code,Name from [@CSS_SUCURSALES] order by Name";
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
            DataSet misDatos = ClaseDatos.procesaDataSet(sucursales);
            dsItems = ClaseDatos.procesaDataSet("select itemname, itemcode from oitm order by itemname");
            

        }
       
        #endregion
    
        #region Mostrar Estadisticas,mostrarDatos En Control
            

     
        private void mostrarEstadisticas() {

            #region datos actuales y del año anterior

            // Muestra en la parte inferior de la pantalla los registros grabados del año actual a partir de una Vista guardada en la BD.
            string cSql = " SET DATEFORMAT YMD  " +
            " declare  @fromSLP as CHAR(32)  set @fromSLP =" + slpcode +
           " declare  @Item as CHAR(32)	set @Item ='" + txtItemCode.Text +"'"+
           " declare  @Cliente as CHAR(32) 	set @Cliente = '" + cbxCostumersCode.Text +"'"+
           " declare  @todate as DATETIME 	set @todate = '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day +"'"+
           "   "; // +BP.Properties.Settings.Default.QueryEstadistica + " ";/// '2011-05-31' "

            dataGridView3.DataSource = ClaseDatos.procesaDataSet(cSql).Tables[0];

            cSql = null;


            //muestra en la parte inferior los registros estimados del año anterior para ese tipo de producto y con el mismo ingeniero.

            cSql = " SET DATEFORMAT YMD  " +
           " declare  @fromSLP as CHAR(32)  set @fromSLP =" + slpcode +
          " declare  @Item as CHAR(32)	set @Item ='" + txtItemCode.Text + "'" +
          " declare  @Cliente as CHAR(32) 	set @Cliente = '" + cbxCostumersCode.Text + "'" +
          " declare  @todate as DATETIME 	set @todate = '" + DateTime.Now.AddYears(-1).Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'" +
          "   ";// +BP.Properties.Settings.Default.QueryEstadistica + " ";/// '2011-05-31' "


            dataGridView4.DataSource = ClaseDatos.procesaDataSet(cSql).Tables[0];

            #endregion
        
        
        }

        private void mostrarDatosEnControl(Dictionary<int,List<CityAndValue>> DicAnualPublica)
        {

            int conta = 0;
            foreach( var s in DicAnualPublica){
                
               
               
                for (int d = 0; d < s.Value.Count; d++) {

                    double total = 0;
                    total = total + s.Value[d].Valor;
                    dataGridView1C.Rows[0].Cells[conta].Value = total;
                    conta++;
                
                }
                                
                
            
            
            }
            
           
        }
               
        private DataTable LoadTable(System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<BP.CityAndValue>> datos ){

            DataTable tbRetono=null;

            


            return tbRetono;
        
        }
              

        #endregion
     
        #region Datagridviews

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {

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

        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                #region Nombre cliente

                try
                {
                    for (int a = 0; a < dataGridView3.Rows.Count; a++)
                    {   
                        dataGridView3.Rows[a].HeaderCell.Value = (a + 1) + " ";
                        string miConsulta = "select CardName from OCRD where CardCode='" + dataGridView3.Rows[a].Cells[2].Value.ToString() + "'";
                        IDataReader datos;
                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        datos = ClaseDatos.procesaDataReader(miConsulta);

                        while (datos.Read())
                        {
                            dataGridView3.Rows[a].Cells[2].Value = "" + datos.GetValue(0);
                        }
                    }

                    ClaseDatos.SqlConn.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Upps " + ex.Message, "Mensjane de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClaseDatos.SqlConn.Close();
                }
                #endregion

                #region Productos

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

#endregion 
            }

            dataGridView3.Columns[2].HeaderText = "Cliente";
            dataGridView3.Columns[3].HeaderText = "Producto";
            dataGridView4.Columns[2].HeaderText = "Cliente";
            dataGridView4.Columns[3].HeaderText = "Producto";

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

                        miConsulta = "SELECT SlpCode,SlpName FROM OSLP WHERE U_CSS_IDUSER='" + loging.usrCode+"'";

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
                    MessageBox.Show("CSS:2873 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("CSS:L2908 " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ClaseDatos.SqlUnConnex();
                    _t = 2;
                   _Click = 2;

                }

            }
        }

        private void cbxSalesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_t >= 2) {
                w = 2;
                slpcode = Convert.ToInt16(cbxSalesName.SelectedValue.ToString());
                slpCustomers();
                
                this.Text = "el id de la Liena de Negocio seleccionada es:" + slpcode;
            
            
            }
        }
      
        private   Dictionary<int, List<CityAndValue>> CreaAnoProd(){


            Dictionary<int, List<CityAndValue>> _dicAnualRetorna = null;



            return _dicAnualRetorna;

        }
     
        #region Metodos del Control BPgrid



        private void btnValidar_Click(object sender, EventArgs e)
        {
            buscarDatos();

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (copyCityAndValue != null)
            {

                copiarArreglos(copyCityAndValue);
            }
        }

        private void copiarArreglos(List<CityAndValue> CiudadValorCopia)
        {

            try
            {
                foreach (int s in mesesActivos)
                {

                    if (dicAnual.ContainsKey(s))
                    {
                        dicAnual.Remove(s);

                        dicAnual.Add(s, CiudadValorCopia);

                    }
                    else
                    {
                        dicAnual.Add(s, CiudadValorCopia);
                    }

                    dataGridView1C.Rows[0].Cells[s - 1].Value = _totalCopyCityAndValue;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Copy:" + ex.Message, "Mensaje de Sistema");

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {

                if (dicAnual != null && dicAnual.Count > 0)
                {
                    dicAnual.Clear();

                    for (int m = 0; m < 12; m++)
                    {

                        dataGridView1C.Rows[0].Cells[m].Value = null;
                    }


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Clear:" + ex.Message, "Mensaje de Sistema");

            }

        }

        private void btnRevert_Click(object sender, EventArgs e)
        {

            dicAnualByProduct.Remove(cbxProducts.SelectedValue.ToString());
            counter = counter - 1;
            RevertasignGrid();

        }

        private void RevertasignGrid()
        {

            try
            {

                switch (counter)
                {

                    case 0: gbxProducto1.Text = null;

                        for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                        {
                            dataGridView2C.Rows[0].Cells[index].Value = null;
                        }

                        break;
                    case 1: gbxProduct2.Text = null;

                        for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                        {
                            dataGridView3C.Rows[0].Cells[index].Value = null;
                        }

                        break;
                    case 2: gbxProduct3.Text = null;

                        for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                        {
                            dataGridView4C.Rows[0].Cells[index].Value = null;
                        }

                        break;
                    case 3: gbxProduct4.Text = null;

                        for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                        {
                            dataGridView5C.Rows[0].Cells[index].Value = null;
                        }

                        break;
                    case 4: gbxProduct5.Text = null;

                        for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                        {
                            dataGridView6C.Rows[0].Cells[index].Value = null;
                        }

                        break;
                    case 5: gbxProduct6.Text = null;

                        for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                        {
                            dataGridView7C.Rows[0].Cells[index].Value = null;
                        }

                        break;

                    default:

                        MessageBox.Show("Debe iniciar de nuevo el proceso", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;



                }
            }
            catch (Exception ex) {

                MessageBox.Show("Revert: "+ex.Message);
            
            }


        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {

                // Recibe dicanual

                if (dicAnual != null)
                {

                    publicaPila();



                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Control-BtnAdicionar: " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void publicaPila()
        {

            try
            {


                dicAnualByProduct.Add(cbxProducts.SelectedValue.ToString(), dicAnual);
                asignGrid();


            }
            catch (Exception ex)
            {

                MessageBox.Show("pubPila: " + ex.Message);


            }


        }

        private void asignGrid()
        {

            switch (counter)
            {

                case 0: gbxProducto1.Text = cbxProducts.Text + " - " + cbxProducts.SelectedValue.ToString();

                    for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                    {
                        dataGridView2C.Rows[0].Cells[index].Value = dataGridView1C.Rows[0].Cells[index].Value;
                    }
                    counter = counter + 1;
                    break;
                case 1: gbxProduct2.Text = cbxProducts.Text + " - " + cbxProducts.SelectedValue.ToString();

                    for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                    {
                        dataGridView3C.Rows[0].Cells[index].Value = dataGridView1C.Rows[0].Cells[index].Value;
                    }
                    counter = counter + 1;
                    break;
                case 2: gbxProduct3.Text = cbxProducts.Text + " - " + cbxProducts.SelectedValue.ToString();

                    for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                    {
                        dataGridView4C.Rows[0].Cells[index].Value = dataGridView1C.Rows[0].Cells[index].Value;
                    }
                    counter = counter + 1;
                    break;
                case 3: gbxProduct4.Text = cbxProducts.Text + " - " + cbxProducts.SelectedValue.ToString();

                    for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                    {
                        dataGridView5C.Rows[0].Cells[index].Value = dataGridView1C.Rows[0].Cells[index].Value;
                    }
                    counter = counter + 1;
                    break;
                case 4: gbxProduct5.Text = cbxProducts.Text + " - " + cbxProducts.SelectedValue.ToString();

                    for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                    {
                        dataGridView6C.Rows[0].Cells[index].Value = dataGridView1C.Rows[0].Cells[index].Value;
                    }
                    counter = counter + 1;
                    break;
                case 5: gbxProduct6.Text = cbxProducts.Text + " - " + cbxProducts.SelectedValue.ToString();

                    for (Int32 index = 0; index < dataGridView1C.Rows[0].Cells.Count; index++)
                    {
                        dataGridView7C.Rows[0].Cells[index].Value = dataGridView1C.Rows[0].Cells[index].Value;
                    }
                    counter = counter + 1;
                    break;

                default:

                    MessageBox.Show("Debe hacer click en Salvar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;

            }


        }


        private void dataGridView1C_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (validaMes(e.ColumnIndex + 1))
                {

                    m_popedContainerForButton.resetControls();
                    m_poperContainerForButton.Show(dataGridView1C, e.ColumnIndex);
                    m_popedContainerForButton.Mes = e.ColumnIndex;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Control-CellEnter: " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        
        private bool validaMes(int mesEvaluado)
        {

            bool retshow = false;


            #region valida Mes nuevo año

            switch (DateTime.Now.Month)
            {

                case 1:
                    groupBox1.Text = DateTime.Now.Year.ToString();
                    if (mesEvaluado == 2 || mesEvaluado == 3 || mesEvaluado == 4 || mesEvaluado == 5 || mesEvaluado == 6 || mesEvaluado == 7)
                    {
                        mesesActivos = new int[6] { 2, 3, 4, 5, 6, 7 };

                        aplicaEstilo(mesesActivos);

                        retshow = true;

                    }
                    break;
                case 2:
                    groupBox1.Text = DateTime.Now.Year.ToString();
                    if (mesEvaluado == 3 || mesEvaluado == 4 || mesEvaluado == 5 || mesEvaluado == 6 || mesEvaluado == 7 || mesEvaluado == 8)
                    {
                        mesesActivos = new int[6] { 3, 4, 5, 6, 7, 8 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                case 3:
                    groupBox1.Text = DateTime.Now.Year.ToString();
                    if (mesEvaluado == 4 || mesEvaluado == 5 || mesEvaluado == 6 || mesEvaluado == 7 || mesEvaluado == 8 || mesEvaluado == 9)
                    {
                        mesesActivos = new int[6] { 4, 5, 6, 7, 8, 9 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                case 4:
                    groupBox1.Text = DateTime.Now.Year.ToString();
                    if (mesEvaluado == 5 || mesEvaluado == 6 || mesEvaluado == 7 || mesEvaluado == 8 || mesEvaluado == 9 || mesEvaluado == 10)
                    {
                        mesesActivos = new int[6] { 5, 6, 7, 8, 9, 10 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;

                case 5:
                    groupBox1.Text = DateTime.Now.Year.ToString();
                    if (mesEvaluado == 6 || mesEvaluado == 7 || mesEvaluado == 8 || mesEvaluado == 9 || mesEvaluado == 10 || mesEvaluado == 11)
                    {
                        mesesActivos = new int[6] { 6, 7, 8, 9, 10, 11 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                case 6:
                    groupBox1.Text = DateTime.Now.Year.ToString();
                    if (mesEvaluado == 7 || mesEvaluado == 8 || mesEvaluado == 9 || mesEvaluado == 10 || mesEvaluado == 11 || mesEvaluado == 12)
                    {
                        mesesActivos = new int[6] { 7, 8, 9, 10, 11, 12 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                case 7:
                    progressBar1.Value = 17;
                    groupBox1.Text = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
                    if (mesEvaluado == 8 || mesEvaluado == 9 || mesEvaluado == 10 || mesEvaluado == 11 || mesEvaluado == 12 || mesEvaluado == 1)
                    {
                        mesesActivos = new int[6] { 8, 9, 10, 11, 12, 1 };
                        aplicaEstilo(mesesActivos);

                        retshow = true;

                    }
                    break;
                case 8:
                    progressBar1.Value = 27;
                    groupBox1.Text = DateTime.Now.Year.ToString();
                    if (mesEvaluado == 9 || mesEvaluado == 10 || mesEvaluado == 11 || mesEvaluado == 12 || mesEvaluado == 1 || mesEvaluado == 2)
                    {
                        mesesActivos = new int[6] { 9, 10, 11, 12, 1, 2 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;

                case 9: progressBar1.Value = 38;
                    groupBox1.Text = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
                    if (mesEvaluado == 1 || mesEvaluado == 10 || mesEvaluado == 11 || mesEvaluado == 12 || mesEvaluado == 1 || mesEvaluado == 2 || mesEvaluado == 3)
                    {

                        mesesActivos = new int[6] { 10, 11, 12, 1, 2, 3 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                case 10: progressBar1.Value = 52;
                    groupBox1.Text = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
                    if (mesEvaluado == 1 || mesEvaluado == 2 || mesEvaluado == 11 || mesEvaluado == 12 || mesEvaluado == 3 || mesEvaluado == 4)
                    {
                        mesesActivos = new int[6] { 11, 12, 1, 2, 3, 4 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                case 11: progressBar1.Value = 60;
                    groupBox1.Text = DateTime.Now.Year + "-" + DateTime.Now.AddYears(1).Year;
                    if (mesEvaluado == 1 || mesEvaluado == 2 || mesEvaluado == 3 || mesEvaluado == 12 || mesEvaluado == 4 || mesEvaluado == 5)
                    {
                        mesesActivos = new int[6] { 1, 2, 3, 4, 5, 12 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                case 12: progressBar1.Value = 100;
                    groupBox1.Text = DateTime.Now.AddYears(1).Year.ToString();
                    if (mesEvaluado == 1 || mesEvaluado == 2 || mesEvaluado == 3 || mesEvaluado == 4 || mesEvaluado == 5 || mesEvaluado == 6)
                    {
                        mesesActivos = new int[6] { 1, 2, 3, 4, 5, 6 };
                        aplicaEstilo(mesesActivos);
                        retshow = true;

                    }
                    break;
                default:

                    break;

            }
            #endregion


            return retshow;


        }

        private void aplicaEstilo(int[] idcell)
        {
            try
            {


                System.Windows.Forms.DataGridViewCellStyle estiloCelda = new System.Windows.Forms.DataGridViewCellStyle();
                estiloCelda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                estiloCelda.ForeColor = System.Drawing.Color.Black;
                dataGridView1C.Rows[0].Cells[idcell[0] - 1].Style = estiloCelda;
                dataGridView1C.Rows[0].Cells[idcell[1] - 1].Style = estiloCelda;
                dataGridView1C.Rows[0].Cells[idcell[2] - 1].Style = estiloCelda;
                dataGridView1C.Rows[0].Cells[idcell[3] - 1].Style = estiloCelda;
                dataGridView1C.Rows[0].Cells[idcell[4] - 1].Style = estiloCelda;
                dataGridView1C.Rows[0].Cells[idcell[5] - 1].Style = estiloCelda;

            }
            catch (Exception ex) {

                MessageBox.Show("Ex-Formato: "+ex.Message);
            
            
            }

        }

        private void cbxProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cbxProducts.SelectedValue.ToString().Contains("System.Data"))
            {
                _productoID = cbxProducts.SelectedValue.ToString();
                txtItemCode.Text = _productoID;
            }

        }

        private void buttonListo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dicAnual.ContainsKey(m_popedContainerForButton.Mes))
                {

                    dicAnual.Remove(m_popedContainerForButton.Mes);

                    dicAnual.Add(m_popedContainerForButton.Mes, m_popedContainerForButton.CiudadValor);


                }
                else
                {

                    dicAnual.Add(m_popedContainerForButton.Mes, m_popedContainerForButton.CiudadValor);

                }



                ArregloAnual = new YearArray(m_popedContainerForButton.Datos, m_popedContainerForButton.Mes);


                ArrResult = new List<YearArray>();
                ArrResult.Add(ArregloAnual);

                dataGridView1C.Rows[0].Cells[m_popedContainerForButton.Mes].Value = m_popedContainerForButton.Total.ToString();

                copyCityAndValue = m_popedContainerForButton.CiudadValor;
                _totalCopyCityAndValue = m_popedContainerForButton.Total;

                m_popedContainerForButton.Datos = null;
                m_popedContainerForButton.Total = 0;
                m_poperContainerForButton.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Control-buttonListo: " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }


        #endregion
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            guardarDatos();
        }

        private void guardarDatos()
        {
           int idEstimado = 0;
           string[] ArregloIDLines2;
            try
            {
                string [] itemsForSave=cargaPresupuesto(cbxCostumersCode.Text, DateTime.Now.Year);

                for(int l=0;l<itemsForSave.Length;l++) {
                    if(itemsForSave[l]!=null && itemsForSave[l].ToString()!=""&&itemsForSave[l].ToString().Length>0)
                    {

                  idEstimado=validaIDEstimado( itemsForSave[l].ToString(),cbxCostumersCode.Text,DateTime.Now.Year,slpcode); // valida el estimado para un item


                    if (idEstimado > 0)
                    {
                        #region manejo de datos cuando existe el escenario de presupuesto


                            string idPresupLines = "", idPresupLi2;

                            foreach (var m in dicAnualByProduct)
                            {
                                string codigoProducto = null;
                                codigoProducto = m.Key;
                                foreach (var t in m.Value)
                                {
                                    int mes = 0;
                                    Double estimadoTotalKg=0;
                                    mes = t.Key;

                                      foreach(var s in t.Value){
                                    
                                          estimadoTotalKg=estimadoTotalKg+s.Valor;
                                         idPresupLines=retornaIdlines(itemsForSave[l].ToString(), cbxCostumersCode.Text, DateTime.Now.Year, idEstimado, mes);
                                         ArregloIDLines2 = (retornaIdlines2(itemsForSave[l].ToString(), cbxCostumersCode.Text, DateTime.Now.Year, idEstimado, mes)).Split(',');

                                         #region Se eliminan los registros anteriores y se Insertan los nuevos en EstimadosLines2
                                         if (DeleteEstimadosLines2(ArregloIDLines2)){
                                          
                                             
                                            if(!insertEstimadosLines2(idEstimado.ToString(),idPresupLines,s.Ciudad,mes,s.Valor)){
                                            
                                                 MessageBox.Show("No inserto el valor para el estimado: "+idEstimado+",estimadoLineas: "+idPresupLines+",ciudad:"+s.Ciudad+", valor: "+s.Valor);
                                            
                                            }
                                          
                                          
                                             

                                               }
                                          else{
                                          
                                          MessageBox.Show("No se eliminaron los registros anterioes en Lines2");

                                          }
                                         #endregion

                                      }
                                      //Se actualiza Estimados Lines
                                      UpdateEstimadosLines(idEstimado.ToString(), idPresupLines, cbxCostumersCode.Text, codigoProducto, mes, estimadoTotalKg);

                                }
                            }
                        


                        #endregion
                    }
                    else
                    {

                        #region manejo de datos cuando NO existe el escenario de presupuesto


                        // Se crea un ID nuevo en la cabecera de Estimados(ESTIMADOS_HEAD)
                        int New_IdPresupuestoHead = insertEstimadosHead(cbxCostumersCode.Text, itemsForSave[l].ToString(), DateTime.Now.Year, slpcode);

                           string idPresupLi2;

                            foreach (var m in dicAnualByProduct)
                            {
                                string codigoProducto = null;
                                codigoProducto = m.Key;
                                foreach (var t in m.Value)
                                {
                                    int mes = 0,idEstimadoLine=0;
                                    Double estimadoTotalKg = 0;
                                    mes = t.Key;

                                    #region Se Insertan los nuevos valores en EstimadosLines
                                    idEstimadoLine = insertEstimadosLines(New_IdPresupuestoHead.ToString(), cbxCostumersCode.Text, codigoProducto, "--", mes, 0);
                                   
                                    #endregion

                                    foreach (var s in t.Value)
                                    {
                                        estimadoTotalKg = estimadoTotalKg + s.Valor;
                                         #region Se Insertan los nuevos valores en EstimadosLines2
                                       
                                            if (!insertEstimadosLines2(New_IdPresupuestoHead.ToString(), idEstimadoLine.ToString(), s.Ciudad, mes, s.Valor))
                                            {

                                                MessageBox.Show("No inserto el valor para el estimado: " + New_IdPresupuestoHead + ",estimadoLineas: " + idEstimadoLine + ",ciudad:" + s.Ciudad + ", valor: " + s.Valor);

                                            }
                                        
                                        #endregion
                                    }

                                    //Se actualiza Estimados Lines Para que el valor total de estimadoskg no quede en Cero
                                    UpdateEstimadosLines(New_IdPresupuestoHead.ToString(), idEstimadoLine.ToString(), cbxCostumersCode.Text, codigoProducto, mes, estimadoTotalKg);
                                         

                                }
                            }
                       


                        #endregion

                    }
                  }
                
                }

                MessageBox.Show("Datos Creados Correctamente ");

            }
            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
            }


        }

        private void buscarDatos()
        {
          
            idPres = 0;  // Almacena el Id del Estimado Head
            
            try
            {
               
                #region captura el ID del estimado si existe

                idPres = validaIDEstimado(cbxProducts.SelectedValue.ToString(), cbxCostumersCode.Text, DateTime.Now.Year, slpcode);

                #endregion


                if (idPres > 0)
                {
                    #region existen datos depresupuesto año/ing

                    #region Trae los datos de EstimadoLines en una Tabla
                   
                    int mes = 0;

                    Dictionary<int, List<CityAndValue>> _dicAnualFind = new Dictionary<int, List<CityAndValue>>();
                    List<BP.CityAndValue> _CiuValor = new List<BP.CityAndValue>();
                    CityAndValue cd_val = null;


                    DataTable idEstimadosLinesDt = retornaIdDatosEstimadoslines(txtItemCode.Text, cbxCostumersCode.Text, DateTime.Now.Year, idPres);
                    if (idEstimadosLinesDt != null && idEstimadosLinesDt.Rows.Count > 0)
                    {
                        for (int r = 0; r < idEstimadosLinesDt.Rows.Count; r++)
                        {
                            mes = Convert.ToInt32(idEstimadosLinesDt.Rows[r]["Mes"].ToString());
                            int _IdEstimadoLine=Convert.ToInt32(idEstimadosLinesDt.Rows[r]["IDEstimadoLine"].ToString());

                            // Trae los datos de estimados lines 2
                            DataTable idEstimadosLines2DT = retornaIdDatosEstimadoslines2(txtItemCode.Text, cbxCostumersCode.Text, DateTime.Now.Year, idPres, mes, _IdEstimadoLine);

                          

                                                    if (idEstimadosLines2DT != null && idEstimadosLines2DT.Rows.Count > 0)
                                                    {
                                                       
                                                        for (int s = 0; s < idEstimadosLines2DT.Rows.Count; s++)
                                                        {
                                                            cd_val = new CityAndValue(idEstimadosLines2DT.Rows[s]["Sucursal"].ToString(), Convert.ToDouble(idEstimadosLines2DT.Rows[s]["Cantidad"].ToString()));
                                                            _CiuValor.Add(cd_val);

                                                        }
                                                    
                                                    }
                                                    else {

                                                        cd_val = new CityAndValue(null,0);
                                                        _CiuValor.Add(cd_val);
                                                    
                                                            }

                           

                        }
                        _dicAnualFind.Add(mes, _CiuValor);
                        

                    }
                    else
                    {

                        MessageBox.Show("Sin datos Previos del estimado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    #endregion

                    mostrarDatosEnControl(_dicAnualFind);
                    dicAnual = _dicAnualFind;

                    #endregion
                }


                /// esrte metodo muestra las estadisticas de los productos en el año anterior
                mostrarEstadisticas();

            }

            catch (Exception er)
            {
                toolStripStatusLabel1.Text = er.Message;
            }


        }

        
        #region bases de Datos

        private string [] cargaPresupuesto(string cardCode,int year) {
            string[] valorR =new string[10] ;

            try
            {
                //cargando los itemcode para buscar el id presupuesto

                int contadorAnual=0;
                foreach (var m in dicAnualByProduct)
                {                   
                   
                   valorR[contadorAnual]= m.Key; // contiene el itemcode

                   contadorAnual=contadorAnual+1;
                  }
               
            }

            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return valorR;
            }

            return valorR;
        
        
        }

        private int insertEstimadosHead( string customerID, string itemCode,int ano,int slpCode)
        {

            int InsertadoInt = 0; //Retorna el ID de Estimado que se crea en la tabla de cabecera (ESTIMAVENT_HEAD)
            string cSql = null, idPresupHead = null;
            ClaseDatos.SqlConn.Close();

            try
            {

                cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_ESTIMAVENT_HEAD]";
                idPresupHead = ClaseDatos.scalarIntSql(cSql).ToString();

                cSql =
                    "INSERT INTO [@CSS_ESTIMAVENT_HEAD] (CODE,NAME, U_ano_estim, U_id_estimado, U_id_ing_slpcode) " +
                    "values (" + idPresupHead + ", " + idPresupHead + ", " + ano + ", " + idPresupHead + ", '" + slpcode + "')";

                string resp = ClaseDatos.nonQuery(cSql);

                if (resp.Contains("exito"))
                {

                    InsertadoInt = Convert.ToInt32(idPresupHead);

                }
                else
                {

                    InsertadoInt = 0;

                }

            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
                InsertadoInt = 0;
                ClaseDatos.SqlConn.Close();


            }
            finally {

                ClaseDatos.SqlConn.Close();
            }

            return InsertadoInt;
        }

        private int insertEstimadosLines(string idPressupEst,string customerID,string itemCode,string itemName, int mes,double estimadoKg) {

            int InsertadoInt = 0; //Retorna true cuando la insercion es correcta
            string cSql = null,idPresupLines=null;

            // Adiciono un numero mas al mes pra ser insertado
            mes = mes + 1;

            try
            {
               
                cSql =  "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@ESTIMADO_VENTA_LINE]";
                idPresupLines = ClaseDatos.scalarIntSql(cSql).ToString();

                cSql =
                    "insert into [@ESTIMADO_VENTA_LINE] (code, name, U_id_estima_venta, u_cliente, u_item, u_mes, U_estimadokg,U_nombre_item) " +
                    "values (" + idPresupLines + ", " + idPresupLines + ", " + idPressupEst+
                    ", '" + customerID + "', '" + itemCode + "', " +
                   mes + ",'" +
                   estimadoKg.ToString().Replace(",", ".").ToString() + "','" + itemName +
                    "')";

                string resp = ClaseDatos.nonQuery(cSql);

                if (resp.Contains("exito"))
                {

                    InsertadoInt = Convert.ToInt32(idPresupLines);

                }
                else
                {

                    InsertadoInt = 0;

                }

            }
            catch (Exception ex) {


                MessageBox.Show(ex.Message);
                InsertadoInt = 0;

                ClaseDatos.SqlConn.Close();


            }
            finally
            {

                ClaseDatos.SqlConn.Close();
            }
            return InsertadoInt;        
        }

        private bool UpdateEstimadosLines(string idPressupEst,string idPresuplines, string customerID, string itemCode, int mes, double estimadoKg)
        {

            bool InsertadoBol = false; //Retorna true cuando la insercion es correcta
            string cSql = null;
            mes = mes + 1;


            try
            {
                cSql =
                    "update [@ESTIMADO_VENTA_LINE] set u_estimadokg = '" +
                    estimadoKg.ToString().Replace(",", ".") +
                    "' where  " +
                    "U_id_estima_venta =  " + idPressupEst.ToString() + " " +
                    "and U_item =  '" + itemCode + "' " +
                    "and u_mes = " + mes +
                    " and u_cliente = '" + customerID + "'";

               string resp= ClaseDatos.nonQuery(cSql);

               if (resp.Contains("exito"))
               {

                   InsertadoBol = true;
               }
               else { 
               
                 
               
               }
                               

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
               InsertadoBol = false;

               ClaseDatos.SqlConn.Close();


            }
            finally
            {

                ClaseDatos.SqlConn.Close();
            }

            return InsertadoBol;
        }

        private bool insertEstimadosLines2(string idPressupEst, string idPresuplines, string sucursal, int mes, double cantidadEskg)
        {

            bool InsertadoBol = false; //Retorna true cuando la insercion es correcta
            string cSql = null, idPresupLines2 = null;

            try
            {

                cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_ESTVENTAS_LINE2]";
                idPresupLines2 = ClaseDatos.scalarIntSql(cSql).ToString();

                cSql =
                    "insert into [@CSS_ESTVENTAS_LINE2] (Code,Name,U_CSS_ID_PRESUPUESTO,U_CSS_ID_PRE_LINES,U_CSS_MES_PRESUPU,U_CSS_SUCURSAL,U_CSS_CANTIDAD) " +
                    "values (" + idPresupLines2 + ", " + idPresupLines2 + ", " + idPressupEst +
                    ", '" + idPresuplines + "', '" + mes + "', '" +
                   sucursal + "','" +
                  cantidadEskg.ToString().Replace(",", ".").ToString() + "')";

              string resp=  ClaseDatos.nonQuery(cSql);

              if (resp.Contains("exito"))
                {

                    InsertadoBol = true;

                }
                else
                {

                    InsertadoBol = false;

                }

            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
                InsertadoBol = false;

                ClaseDatos.SqlConn.Close();


            }
            finally
            {

                ClaseDatos.SqlConn.Close();
            }

            return InsertadoBol;
        }

        private bool DeleteEstimadosLines2(string [] idPresuplines2)
        {

            bool DeleteBol = false; //Retorna true cuando la insercion es correcta
            string cSql = null;

            try
            {
                //Elimina el registro en EstimadosLines2

                for (int d = 0; d < idPresuplines2.Length; d++)
                {

                    cSql = "DELETE FROM [@CSS_ESTVENTAS_LINE2] WHERE CODE=" + idPresuplines2[d].ToString();
                 string RESP=   ClaseDatos.nonQuery(cSql);

                }

                DeleteBol = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DeleteBol = false;
                ClaseDatos.SqlConn.Close();

            }
            finally {

                ClaseDatos.SqlConn.Close();
            
            }

            return DeleteBol;
        }

        private int validaIDEstimado(string itemCode,string customerCode, int ano,int slpcode) {


            int InsertadoInt = 0; //Retorna el idestimado
            string cSql = null;
            ClaseDatos.SqlConn.Close();
            ClaseDatos.SqlUnConnex();

            try
            {
                cSql =  "  SELECT CASE WHEN " +
			"	(SELECT  COUNT(T0.U_id_estimado) "+
			" FROM [@CSS_ESTIMAVENT_HEAD] T0 INNER JOIN [@ESTIMADO_VENTA_LINE]  T1 ON T0.U_id_estimado= T1.U_id_estima_venta " +
			" WHERE T0.U_ano_estim="+DateTime.Now.Year+
            " AND T1.U_item='"+itemCode+"'"+
			"	 AND T1.U_cliente='"+ customerCode+"'"+
            "  AND T0.U_id_ing_slpcode='" + slpcode + "'" +
            ")=0 THEN 0 "+
			" ELSE  	(SELECT  TOP 1 T0.U_id_estimado "+
			" FROM [@CSS_ESTIMAVENT_HEAD] T0 INNER JOIN [@ESTIMADO_VENTA_LINE]  T1 ON T0.U_id_estimado= T1.U_id_estima_venta  "+
            " WHERE T0.U_ano_estim=" + DateTime.Now.Year +
            " AND T1.U_item='" + itemCode + "'" +
            "	 AND T1.U_cliente='" + customerCode + "'" +
            "  AND T0.U_id_ing_slpcode='" + slpcode + "'" +
			" ) END  ";




               
                InsertadoInt = ClaseDatos.scalarIntSql(cSql);
                ClaseDatos.SqlConn.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InsertadoInt = 0;
                ClaseDatos.SqlConn.Close();
                ClaseDatos.SqlUnConnex();

            }
            finally {

                ClaseDatos.SqlConn.Close();
            
            }

            return InsertadoInt;
        
        
        }
               
        private string retornaIdlines2(string itemcode, string cardcode, int year, int idEstimado, int mes)
        {
            //OK REVISADO
            // Este metodo detalla hasta el mes en el que se realizo el estimado
            string  retCodeLines2 = null, queryCode = null;

            try
            {

                queryCode =
                            " SELECT T2.Code FROM [@CSS_ESTIMAVENT_HEAD] T0 " +
                            " INNER JOIN [@ESTIMADO_VENTA_LINE] T1 ON T0.U_id_estimado = T1.U_id_estima_venta " +
                            " INNER JOIN [@CSS_ESTVENTAS_LINE2] T2 ON T1.Code= T2.U_CSS_ID_PRE_LINES " +
                            " WHERE T0.U_id_estimado=" + idEstimado + " " +
                            " AND T0.U_ano_estim=" + year + " " +
                            " AND   T1.U_item =  '" + itemcode + "' " +
                            " AND T1.U_cliente='" + cardcode + "' " +
                            " AND T2.U_CSS_SUCURSAL <> ' ' " +
                            " AND T2.U_CSS_MES_PRESUPU=" + mes;

                IDataReader datos;
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                datos = ClaseDatos.procesaDataReader(queryCode);


                while (datos.Read())
                {
                    retCodeLines2 += "" + datos.GetValue(0) + ",";


                }

                ClaseDatos.SqlConn.Close();

                if (retCodeLines2.EndsWith(","))
                {

                    retCodeLines2 = retCodeLines2.Substring(0, retCodeLines2.Length - 1);

                }

                return retCodeLines2;
            }

            catch (Exception ex)
            {
                return retCodeLines2;
                MessageBox.Show("Upssss Linea: 3736 " + ex.Message);
                ClaseDatos.SqlConn.Close();

            }
            finally {

                ClaseDatos.SqlConn.Close();
            }





        }

        private string retornaIdlines(string itemcode, string cardcode, int year, int idEstimado, int mes)
        {

            // Este metodo detalla hasta el mes en el que se realizo el estimado
            string retCodeLines = null, queryCode = null;

            try
            {

                queryCode =
                            " SELECT  TOP 1 T1.Code FROM [@CSS_ESTIMAVENT_HEAD] T0 " +
                            " INNER JOIN [@ESTIMADO_VENTA_LINE] T1 ON T0.U_id_estimado = T1.U_id_estima_venta " +
                            " INNER JOIN [@CSS_ESTVENTAS_LINE2] T2 ON T1.Code= T2.U_CSS_ID_PRE_LINES " +
                            " WHERE T0.U_id_estimado=" + idEstimado + " " +
                            " AND T0.U_ano_estim=" + year + " " +
                            " AND   T1.U_item =  '" + itemcode + "' " +
                            " AND T1.U_cliente='" + cardcode + "' " +
                            " AND T2.U_CSS_SUCURSAL <> ' ' " +
                            " AND T2.U_CSS_MES_PRESUPU=" + mes;

                IDataReader datos;
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                datos = ClaseDatos.procesaDataReader(queryCode);


                while (datos.Read())
                {
                    retCodeLines = "" + datos.GetValue(0);


                }

                ClaseDatos.SqlConn.Close();
                return retCodeLines;
            }

            catch (Exception ex)
            {
                return retCodeLines;
                MessageBox.Show("Upssss Linea: 3736 " + ex.Message);
                ClaseDatos.SqlConn.Close();

            }
            finally {

                ClaseDatos.SqlConn.Close();
            
            }





        }

        private DataTable retornaIdDatosEstimadoslines(string itemcode, string cardcode, int year, int idEstimado)
        {

            // Este metodo Retorna una table con los campos IDEstimadoLine, CardCode,ItemCode,Mes,Estimado
            DataTable retCodeLinesDt= new DataTable();
            string    queryCode = null;

            try
            {

                queryCode =
                            " SELECT T1.Code IDEstimadoLine,T1.U_cliente CardCode,T1.U_item ItemCode,T1.U_mes Mes,T1.U_estimadokg Estimado FROM [@CSS_ESTIMAVENT_HEAD] T0 " +
                            " INNER JOIN [@ESTIMADO_VENTA_LINE] T1 ON T0.U_id_estimado = T1.U_id_estima_venta " +
                            " WHERE T0.U_id_estimado=" + idEstimado + " " +
                            " AND T0.U_ano_estim=" + year + " " +
                            " AND T1.U_item =  '" + itemcode + "' " +
                            " AND T1.U_cliente='" + cardcode + "' " +
                            " AND T1.U_mes IN (1,2,3,4,5,6,7,8,9,10,11,12)    ORDER BY T1.U_mes";


                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());


                retCodeLinesDt.Load(ClaseDatos.procesaDataReader(queryCode));

                ClaseDatos.SqlConn.Close();

                return retCodeLinesDt;
            }

            catch (Exception ex)
            {
                return retCodeLinesDt;
                MessageBox.Show("Upssss Linea: 3736 " + ex.Message);
                ClaseDatos.SqlConn.Close();

            }

            finally {

                ClaseDatos.SqlConn.Close();
            
            
            }




        }

        private DataTable retornaIdDatosEstimadoslines2(string itemcode, string cardcode, int year, int idEstimado,int Mes,int idPreLines)
        {

            // Este metodo Retorna una table con los campos IDEstimados2, Cantidad,Sucursal
            DataTable retCodeLines2Dt = new DataTable();
            string queryCode = null;

            try
            {

                queryCode =    " SELECT T2.Code idEstimados2, T2.U_CSS_CANTIDAD Cantidad,T2.U_CSS_SUCURSAL Sucursal FROM [@CSS_ESTIMAVENT_HEAD] T0 " +
                               " INNER JOIN [@ESTIMADO_VENTA_LINE] T1 ON T0.U_id_estimado = T1.U_id_estima_venta " +
                               " INNER JOIN [@CSS_ESTVENTAS_LINE2] T2 ON T1.Code= T2.U_CSS_ID_PRE_LINES " +
                               " WHERE T0.U_id_estimado=" + idEstimado + " " +
                               " AND T0.U_ano_estim=" + year + " " +
                               " AND T1.U_item =  '" + itemcode + "' " +
                               " AND T1.U_cliente='" + cardcode + "' " +
                               " AND T2.U_CSS_SUCURSAL <> ' '  " +
                               " AND T2.U_CSS_CANTIDAD >0 " +
                               " AND T2.U_CSS_MES_PRESUPU=" +Mes+
                               " AND T2.U_CSS_ID_PRE_LINES=" +idPreLines;

                IDataReader datos;
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                datos = ClaseDatos.procesaDataReader(queryCode);
                retCodeLines2Dt.Load(datos);

                ClaseDatos.SqlConn.Close();

                return retCodeLines2Dt;
            }

            catch (Exception ex)
            {
                return retCodeLines2Dt;
                MessageBox.Show("Upssss Linea: 1827 Retorna DtLines2 " + ex.Message);
                ClaseDatos.SqlConn.Close();

                ClaseDatos.SqlConn.Close();


            }
            finally
            {

                ClaseDatos.SqlConn.Close();
            }





        }
        
        #endregion





    }
}

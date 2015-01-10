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
    public partial class frmContratosConsignacion : Form
    {
         public string Ultimonumero,Ultimonumero1,code;
         public int indice,ultimo;

        public frmContratosConsignacion()
        {
            InitializeComponent();
        }

        private void cbxCodCliente_DropDown(object sender, EventArgs e)
        {
            string cardtype;
            

            if (cbxTipoTercero.Text == "")
            {


            }
            else {

                try
                {

                    if (cbxTipoTercero.Text == "Cliente")
                    {
                        cardtype = "C";
                    }
                    else {
                        cardtype = "S";
                    }



                    string miConsulta = "select CardCode,CardName from OCRD where Cardtype='" +cardtype + "'";
                    DataSet dato;

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    dato = ClaseDatos.procesaDataSet(miConsulta);

                    cbxCodCliente.DisplayMember = "CardCode";
                    cbxCodCliente.ValueMember = "CardCode";
                    cbxCodCliente.DataSource = dato.Tables[0];
                    cbxNomCliente.DisplayMember = "CardName";
                    cbxNomCliente.ValueMember = "CardName";
                    cbxNomCliente.DataSource = dato.Tables[0];



                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar el codigo del cliente: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
            
            
            }
        }

        private void cbxNomCliente_DropDown(object sender, EventArgs e)
        {
            cbxCodCliente_DropDown(sender, e);

        }

        private void cbxBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void cbxBodega_DropDown(object sender, EventArgs e)
        {
            if (cbxCodCliente.Text == "" && cbxNomCliente.Text == "")
            {
            }
            else
            {



                try
                {

                    string miConsulta = "select WhsCode,WhsName from OWHS where U_QCA_SOCIONEGOCIOS='"+cbxCodCliente.Text+"'";
                    DataSet dato;

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    dato = ClaseDatos.procesaDataSet(miConsulta);



                    cbxBodega.DisplayMember = "WhsName";
                    cbxBodega.ValueMember = "WhsCode";
                    cbxBodega.DataSource = dato.Tables[0];



                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar la Bodega: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {


            if (cbxCodCliente.Text == "" || cbxNomCliente.Text == "" || cbxBodega.Text == "" || cbxTipoTercero.Text == "" || txtNumContrato.Text == "" || cbxEmpleado.Text == "" || maskedTextBox1.Text == "")
            {
                MessageBox.Show("Para crear un Contrato es obligatorio diligenciar todos los datos", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {

              

                try
                {
                    if (ultimo == 0)
                    {
                        validaElUltimo("[@CSS_CONTRATOSCONSIG]", 1);
                        ultimo = Convert.ToInt16(Ultimonumero);
                        Ultimonumero = ultimo.ToString();

                    }
                    else
                    {
                       
                        ultimo = ultimo + 1;
                    }


                    if (maskedTextBox2.Text == "") { maskedTextBox2.Text = "0"; } 



                    string miInsert = "INSERT INTO [@CSS_CONTRATOSCONSIG](Code,Name,U_CSS_CODCLIENTE,U_CSS_NOMCLIENTE,U_CSS_DIACORTE1,U_CSS_DIACORTE2,U_CSS_FECHAINI,U_CSS_FECHAFIN,U_CSS_IDBODEGA,U_CSS_NOMBODEGA, U_CSS_IDEMPLEADO,U_CSS_NOMEMPLEADO,U_CSS_TIPOSOCIO,U_CSS_NUMCONTRATO,U_CSS_COMENTARIOS,U_CSS_RUTA) "
                    + "	VALUES(" + ultimo + ",'" + ultimo + "','" + cbxCodCliente.Text + "','" + cbxNomCliente.Text + "'," + maskedTextBox1.Text + ","
                    + maskedTextBox2.Text + ",'" + dtpVigenciaDesde.Text + "','" + dtpVigenciaHasta.Text + "','"+cbxBodega.ValueMember+"' ,'" + 
                    cbxBodega.Text + "'," + cbxEmpleado.SelectedValue + ",'" + cbxEmpleado.Text + "','" + cbxTipoTercero.Text + "'," + txtNumContrato.Text 
                    + ",'" + txtComentarios.Text + "','" + linkArchivo.Text + "')";
                    
                    
                    ClaseDatos.nonQuery(miInsert);


                     string miSql = "";
                     miSql = "SELECT isnull(MAX(convert(INT,U_CSS_FKEY)),0) as Contador FROM [@CSS_CONTRATOSC_LINE] WHERE U_CSS_FKEY=" + ultimo ;
                   string miCodigo = ClaseDatos.scalarStringSql(miSql);
                   if (miCodigo == "0" || miCodigo.StartsWith("0"))
                   {

                       int ultimo1 = 0;
                       for (int a = 0; a < dataGridView1.Rows.Count; a++)
                       {


                           if (ultimo1 == 0)
                           {
                               validaElUltimo("[@CSS_CONTRATOSC_LINE]", 2);


                               ultimo1 = Convert.ToInt16(Ultimonumero1);
                           }
                           else
                           {

                               ultimo1 = ultimo1 + 1;

                           }






                           string miInsert2 = "INSERT INTO [@CSS_CONTRATOSC_LINE](Code,Name,U_CSS_CODARTICULO,U_CSS_NOMARTICULO,U_CSS_KILOS,U_CSS_PRECIO,U_CSS_FKEY) "
                   + "	VALUES(" + ultimo1 + ",'" + ultimo1 + "','" + dataGridView1.Rows[a].Cells[0].Value + "','" + dataGridView1.Rows[a].Cells[1].Value + "'," + dataGridView1.Rows[a].Cells[2].Value + "," + dataGridView1.Rows[a].Cells[3].Value + "," + ultimo + ")";

                           ClaseDatos.nonQuery(miInsert2);




                       }


                       cbxCodCliente.DataSource = null;
                       cbxCodCliente.Text = "";

                       cbxNomCliente.DataSource = null;
                       cbxNomCliente.Text = "";

                       cbxBodega.DataSource = null;
                       cbxBodega.Text = "";

                       dtpVigenciaDesde.ResetText();
                       dtpVigenciaHasta.ResetText();
                       dataGridView1.Rows.Clear();
                       cbxTipoTercero.Text = "";
                       cbxEmpleado.Text = "";
                       linkArchivo.Text = "";
                       maskedTextBox1.Text = "";
                       maskedTextBox2.Text = "";

                       txtComentarios.Text = "COMENTARIOS";
                       MessageBox.Show("Contrato # " + ultimo + " Creado ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       ultimo = ultimo + 1;
                       txtNumContrato.Text = "" + ultimo;
                       ultimo = 0;
                       ultimo1 = 0;
                   }
                   else {

                       MessageBox.Show("El contrato Numero: "+ultimo +" ya existe. Utilice Actualizar para modificarlo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information );
                   
                   }
                    


                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar la Bodega: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();

               
            }



        }

        public void validaElUltimo(string tabla,int puesto)
        {

            Ultimonumero = "0"; //Publica y Global
            

            try
            {



                string miConsulta = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId FROM  " + tabla + " ";
                IDataReader ultimo;

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                ultimo = ClaseDatos.procesaDataReader(miConsulta);

                while (ultimo.Read())
                {
                    if (puesto == 1) { Ultimonumero = ultimo.GetValue(0).ToString(); }
                    if (puesto == 2) { Ultimonumero1 = ultimo.GetValue(0).ToString(); }

                                      
                }

            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los datos del ultimo numero: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();


        }

        private void cbxEmpleado_DropDown(object sender, EventArgs e)
        {
            if (cbxCodCliente.Text == "" && cbxNomCliente.Text == "")
            {
            }
            else
            {



                try
                {

                    string miConsulta = "select distinct Memo,slpcode from OSLP where SlpCode in( select SlpCode from OCRD where OCRD.CardCode='" + cbxCodCliente.Text 
                        + "' Union  SELECT distinct T1.slpcode from OSLP T1 inner join CRD1 T2 ON T2.Block=T1.SlpName and T2.CardCode='" + cbxCodCliente.Text + "')";


                    DataSet dato;

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    dato = ClaseDatos.procesaDataSet(miConsulta);



                    cbxEmpleado.DisplayMember = "Memo";
                    cbxEmpleado.ValueMember = "slpcode";
                    cbxEmpleado.DataSource = dato.Tables[0];



                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar el empleado: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
            if (e.Control is DataGridViewComboBoxEditingControl)
            {


                DataGridViewComboBoxEditingControl CellComboBox = (DataGridViewComboBoxEditingControl)e.Control;
                
                CellComboBox.DropDown +=new EventHandler(CellComboBox_DropDown);
               
            }
        }

        private void CellComboBox_DropDown(object sender, EventArgs e)
        {
           

        }

        private void frmContratosConsignacion_Load(object sender, EventArgs e)
        {
            indice = 0;
            //Ultimonumero = "0";
            ultimo = 0;
            try
            {

                string cSql = "select ISNULL(MAX(CAST(code AS INT)) ,0)+1 AS nextId from [@CSS_CONTRATOSCONSIG]";
                txtNumContrato.Text = ClaseDatos.scalarIntSql(cSql).ToString();
                txtNumContrato.Enabled = false;
            }
            catch (Exception miExcepcion){
            
                
             MessageBox.Show("Error al cargar el empleado: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            cbxCodCliente.DataSource = null;
            cbxCodCliente.Text = "";

            cbxNomCliente.DataSource = null;
            cbxNomCliente.Text = "";

            cbxBodega.DataSource = null;
            cbxBodega.Text = "";
            maskedTextBox1.ResetText();
            dtpVigenciaDesde.ResetText();
            dtpVigenciaHasta.ResetText();

            btnActualiza.Enabled = true;
            btnCrear.Enabled = true;
            btnElimina.Enabled = true;
            dataGridView1.Rows.Clear();
            cbxTipoTercero.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            linkArchivo.Text = "";
            cbxEmpleado.Text = "";
            txtComentarios.Text = "";
            
           

            txtNumContrato.Text = "";
            txtNumContrato.Enabled = true;
            txtNumContrato.Focus();



        }

        private void txtNumContrato_KeyPress(object sender, KeyPressEventArgs e)
      {
            code="";
            if (e.KeyChar == 13)
            {

                try
                {

                    string miConsulta = "SELECT Code, Name, U_CSS_TIPOSOCIO, U_CSS_CODCLIENTE, U_CSS_NOMCLIENTE, U_CSS_DIACORTE1,U_CSS_DIACORTE2, U_CSS_IDBODEGA, U_CSS_NOMBODEGA, " +
                    "  U_CSS_IDEMPLEADO,U_CSS_NOMEMPLEADO, U_CSS_FECHAINI, U_CSS_FECHAFIN,U_CSS_COMENTARIOS,U_CSS_RUTA " +
                       " FROM         [@CSS_CONTRATOSCONSIG] where CODE =" + txtNumContrato.Text + "";
                    IDataReader datos;

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    datos = ClaseDatos.procesaDataReader(miConsulta);


                    while (datos.Read())
                    {

                        Object[] arreglo = new Object[4];


                        code = datos.GetValue(0).ToString();
                        arreglo[1] = datos.GetValue(1);
                        if (datos.GetValue(2).ToString() != "")
                        {
                            string dato = datos.GetValue(2).ToString();

                            if (datos.GetValue(2).ToString() == "Cliente" || datos.GetValue(2).ToString() == "C")
                            {
                                cbxTipoTercero.Text = "Cliente"; // TipoSocio
                            }
                            else
                            {

                                cbxTipoTercero.Text = "Proveedor"; // TipoSocio
                            }


                        }

                        if (datos.GetValue(3).ToString() != "")
                        {

                            cbxCodCliente.Text = datos.GetValue(3).ToString();  // Cod Cliente

                        }

                        if (datos.GetValue(4).ToString() != "")
                        {

                            cbxNomCliente.Text = datos.GetValue(4).ToString();  // Nom CLiente

                        }

                        if (datos.GetValue(5).ToString() != "")
                        {

                            maskedTextBox1.Text = datos.GetValue(5).ToString();  // Dia COrte

                        }

                        if (datos.GetValue(6).ToString() != "")
                        {

                            maskedTextBox2.Text = datos.GetValue(6).ToString();  // Dia COrte

                        }


                        if (datos.GetValue(7).ToString() != "")
                        {

                            //cbxBodega.SelectedValue=datos.GetValue(7).ToString();  // id bodega

                        }

                        if (datos.GetValue(8).ToString() != "")
                        {

                            cbxBodega.Text = datos.GetValue(8).ToString();  // Nom Bodega

                        }

                        if (datos.GetValue(9).ToString() != "")
                        {

                            //cbxEmpleado.SelectedValue=datos.GetValue(9).ToString();  // id empleado

                        }
                        if (datos.GetValue(10).ToString() != "" || datos.GetValue(10).ToString() != null)
                        {

                            cbxEmpleado.Text = datos.GetValue(10).ToString();  // Nom empleado

                        }
                        if (datos.GetValue(11).ToString() != "")
                        {

                            dtpVigenciaDesde.Text = datos.GetValue(11).ToString();  // Fecha Ini

                        }
                        if (datos.GetValue(12).ToString() != "")
                        {

                            dtpVigenciaHasta.Text = datos.GetValue(12).ToString();  // Fecha Fin

                        }
                        if (datos.GetValue(13).ToString() != "")
                        {

                            txtComentarios.Text = datos.GetValue(13).ToString();  // Comentarios

                        }
                        if (datos.GetValue(14).ToString() != "")
                        {

                            linkArchivo.Text = datos.GetValue(14).ToString();  // Ruta

                        }


                    }

                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("La busqueda no arrojo datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();

                if (code == "" || code == null) {

                    MessageBox.Show("La busqueda no arrojo datos para el Contrato: " +txtNumContrato.Text , "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                }

                else  {

                    try
                    {

                        string miConsulta = "SELECT U_CSS_CODARTICULO,U_CSS_NOMARTICULO,U_CSS_KILOS,U_CSS_PRECIO" +
                           " FROM     [@CSS_CONTRATOSC_LINE] where  U_CSS_FKEY=" + code + "";
                        IDataReader datos;

                        ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                        datos = ClaseDatos.procesaDataReader(miConsulta);


                        while (datos.Read())
                        {

                            Object[] arreglo = new Object[4];

                            arreglo[0] = datos.GetValue(0);
                            arreglo[1] = datos.GetValue(1);
                            arreglo[2] = Convert.ToDouble(datos.GetValue(2).ToString()).ToString("N2");
                            arreglo[3] = Convert.ToDouble(datos.GetValue(3).ToString()).ToString("C2");
                            dataGridView1.Rows.Add(arreglo);
                        }
                    }
                    catch (Exception miExcepcion)
                    {
                        MessageBox.Show("La busqueda no arrojo datos, en la carga del grid : " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ClaseDatos.SqlUnConnex();
                    btnCrear.Enabled = false;
                    btnActualiza.Enabled = true;
                    btnElimina.Enabled = true;
                    txtNumContrato.Enabled = false;

                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[indice].Cells[0].Value = dataGridView2.Rows[e.RowIndex].Cells[0].Value;
                dataGridView1.Rows[indice].Cells[1].Value = dataGridView2.Rows[e.RowIndex].Cells[1].Value;
                dataGridView2.Visible = false;
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("problema en la copia al datagrid : " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = dataGridView1.CurrentRow.Index;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (1 >= dataGridView1.CurrentCell.ColumnIndex)
            {

                try
                {

                    string miConsulta = "select itemcode,itemname from OITM";
                    DataSet dato;

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    dato = ClaseDatos.procesaDataSet(miConsulta);


                    dataGridView2.DataSource = dato.Tables[0];
                    dataGridView2.Visible = true;
                    dataGridView2.Columns[0].ReadOnly = true;
                    dataGridView2.Columns[1].ReadOnly = true;


                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar los datos del producto en la grilla: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
            }

        }

        private void btnActualiza_Click(object sender, EventArgs e)
        {
                       
                try
                {

                    string miUpdate = "UPDATE [@CSS_CONTRATOSCONSIG] SET U_CSS_CODCLIENTE='" + cbxCodCliente.Text +
                        "',U_CSS_NOMCLIENTE='" + cbxNomCliente.Text + "',U_CSS_DIACORTE='" + maskedTextBox1.Text + "',U_CSS_FECHAINI='" +
                        dtpVigenciaDesde.Text + "',U_CSS_FECHAFIN='" + dtpVigenciaHasta.Text + "',U_CSS_IDBODEGA='" + cbxBodega.ValueMember + "',U_CSS_NOMBODEGA='" +
                        cbxBodega.Text + "', U_CSS_NOMEMPLEADO='" + cbxEmpleado.Text + "',U_CSS_TIPOSOCIO='" + cbxTipoTercero.Text
                        + "'  WHERE U_CSS_NUMCONTRATO= " + txtNumContrato.Text + "";
           

                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

                    ClaseDatos.nonQuery(miUpdate);

                    for (int a = 0; a + 1 < dataGridView1.Rows.Count; a++)
                    {


                        string miUpdate2 = "UPDATE [@CSS_CONTRATOSC_LINE] SET U_CSS_CODARTICULO='" +
                            dataGridView1.Rows[a].Cells[0].Value + "',U_CSS_NOMARTICULO='" + dataGridView1.Rows[a].Cells[1].Value
                            + "',U_CSS_KILOS=" + dataGridView1.Rows[a].Cells[2].Value + ",U_CSS_PRECIO=" + dataGridView1.Rows[a].Cells[3].Value + " ";
       

                        ClaseDatos.nonQuery(miUpdate2);

                    }

                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al cargar la Bodega: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClaseDatos.SqlUnConnex();
                MessageBox.Show("Datos Actualizados Satisfactoriamente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Esta seguro que desea eliminar este registro ", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)==DialogResult.Yes){

                string delete = "DELETE FROM [@CSS_CONTRATOSCONSIG] WHERE CODE= " + code;
                string delete1 = "DELETE FROM [@CSS_CONTRATOSC_LINE] WHERE  U_CSS_FKEY=" + code + "";

                ClaseDatos.nonQuery(delete);
                ClaseDatos.nonQuery(delete1);

            MessageBox.Show("Datos eliminados Correctamente ", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnBuscar_Click(sender, e);

            }

        }

        private void txtComentarios_Click(object sender, EventArgs e)
        {
            if (txtComentarios.Text == "COMENTARIOS")
            {

                txtComentarios.Text = "";
                this.txtComentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.txtComentarios.ForeColor = System.Drawing.Color.Black;
            
            
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string validado = "";
            try
            {
                
                string miSelect = "select OnHand from OITW where ItemCode='"+dataGridView1.Rows[e.RowIndex].Cells[0].Value+"' and WhsCode in( select WhsCode from OWHS where U_QCA_SOCIONEGOCIOS='" + cbxCodCliente.Text + "')";
                   

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

                IDataReader datos= ClaseDatos.procesaDataReader(miSelect);

                while (datos.Read()) {

                    validado =""+ datos.GetValue(0);
                
                }


                if (validado == null || validado == "" || validado != "1")
                {
                    MessageBox.Show("El producto no se puede eliminar del contrato por restricciones del sistema.Verifique que el producto no exixta en la Bodega del cliente","Mensaje del Sistema",MessageBoxButtons.OK);

                }
                else {

                    dataGridView1.Rows[e.RowIndex].Visible = false;
                }


            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al verificar el producto a eliminar en la bodega: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClaseDatos.SqlUnConnex();  

            
            
           
        }

        private void btnArchivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            linkArchivo.Text = openFileDialog1.FileName;
        }

        private void linkArchivo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkArchivo.Text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace BP
{

  public partial class loging : Form
  {

    public static SAPbobsCOM.Company objCompany = null;
    public static string cDataBase = null;
    public static bool salesEmpl = false;
    public static string usrName = null;
    public static string usrNombreRepventas = null; // Almacena el nombre del representante de ventas, si el usuario posee ese privilegio.
    public static string usrCode = null;
    public frmMenuPrincipal frmPrincipal;

    public loging()
    {
      InitializeComponent();
    }

    #region Inicio de sesion
    private void FromsSDK_SBD_Load(object sender, EventArgs e)
    {
      try
      {
        //lleno el comboBox de compañias
        DataSet dsCompany = null;
        ClaseDatos.SqlConnex("SBO-COMMON");
        dsCompany = ClaseDatos.procesaDataSet("select dbname, cmpname from SRGC");
        cboCompany.DataSource = dsCompany.Tables[0];
        cboCompany.DisplayMember = "cmpname";
        cboCompany.ValueMember = "dbname";
        cboCompany.Text = "C.I QUIMICA COMERCIAL ANDINA S.A";
        toolStripStatusLabel1.Text = ClaseDatos.cStatus;
      }
      catch (Exception er)
      {
        //MessageBox.Show(er.Message);
        toolStripStatusLabel1.Text = er.Message;
      }
    }
    #endregion

    #region LogIn
    private void button1_Click(object sender, EventArgs e)
    {
      usrNombreRepventas = txtUsrSap.Text;
      toolStripProgressBar1.Value = 30;
      //Conectamos con SAP
      try
      {
        objCompany = ClaseDatos.SapCompany(txtUsrSap.Text, txtPassSap.Text, cboCompany.SelectedValue.ToString(), cbxTipoSver.Text);
        if (ClaseDatos.nSapConnected)
        {
          toolStripStatusLabel1.Text = ClaseDatos.cStatus;
          ClaseDatos.SqlUnConnex();
          ClaseDatos.SqlConnex(cboCompany.SelectedValue.ToString());
          findUserName();
          usrCode = txtUsrSap.Text;
          toolStripProgressBar1.Value = 100;
          frmMenuPrincipal frmconsola = new frmMenuPrincipal(cboCompany.SelectedValue.ToString());
          //frmconsola.Show();
          frmconsola.ShowDialog();
          this.Hide();


        }
      }
      catch (Exception er)
      {
        toolStripStatusLabel1.Text = er.Message;
      }
    }
    #endregion

    #region Terminar
    private void button1_Click_1(object sender, EventArgs e)
    {
      Application.Exit();
    }
    #endregion


    #region Nombre del usuario actual
    private void findUserName()
    {
      SAPbobsCOM.Recordset oRS = null;
      SAPbobsCOM.Users oUsr = null;
      string cQuery = null;
      try
      {

        //traigo los codigos de socios de negocio y aprovecho las bondades de BoRecordset para traer el nombre
        oRS = (SAPbobsCOM.Recordset)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        cQuery = "select U_NAME from oUsr where user_code = '" + txtUsrSap.Text + "'";
        oRS.DoQuery(cQuery);

        if (oRS.RecordCount > 0)
        {
          oUsr = (SAPbobsCOM.Users)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUsers);
          //                        (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
          oUsr.Browser.Recordset = oRS;
          usrName = oUsr.UserName;
        }
      }
      catch (Exception er)
      {
        //MessageBox.Show(er.Message);
        toolStripStatusLabel1.Text = er.Message;

      }
    }
    #endregion

    private void txtUsrSap_TextChanged(object sender, EventArgs e)
    {

    }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CEMW
{

    public partial class loging : Form
    {
        
        public SAPbobsCOM.Company objCompany = null;
        ClaseDatos SapClass = new ClaseDatos();
       

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
                SapClass.SqlConnex("SBO-COMMON");
                dsCompany = SapClass.procesaDataSet("select dbname, cmpname from SRGC");
                cboCompany.DataSource = dsCompany.Tables[0];
                cboCompany.DisplayMember = "cmpname";
                cboCompany.ValueMember = "dbname";
                toolStripStatusLabel1.Text = SapClass.cStatus;
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
                toolStripStatusLabel1.Text = er.Message;
            }
        }
        #endregion 

        #region conactar sap
        //Accion de Conectar a SAP
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Conectamos con SAP
                objCompany = SapClass.SapCompany(txtUsrSap.Text, txtPassSap.Text, cboCompany.SelectedValue.ToString());
                
                if (SapClass.nSapConnected)
                {
                    toolStripStatusLabel1.Text = SapClass.cStatus;
                    //accion = 0;   
                    consola frmconsola = new consola ();
                    loging.ActiveForm.Hide();
                    //this.Visible = false;
                    frmconsola.db = cboCompany.SelectedValue.ToString();
                    frmconsola.Show();
                    
                }                    
            }

            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
                toolStripStatusLabel1.Text = er.Message;

            }

        }
        #endregion 

        #region Terminar
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (objCompany != null && objCompany.Connected)
            {
                objCompany.Disconnect();
                btnConectar.Text = "Conectar con SAP";
                toolStripStatusLabel1.Text = "No Conectado a SAP";
            }
            Application.Exit();
        }
        #endregion




        //        #region Gestion de Socios de Negocio
//        //Traer los socios de negocio
//        private void btnGetBP_Click(object sender, EventArgs e)
//        {
//            SAPbobsCOM.Recordset oRS = null;
//            SAPbobsCOM.BusinessPartners oBP = null;
//            string cQuery = null;
//            try
//            {
//                //limpio el ListBox por si tenia algun dato
//                lstBP.DataSource = null;
//                lstBP.Items.Clear();

//                //creo un dataTable para almacenar los datos de nombre y codigo 
//                DataTable oDT = new DataTable ("tOCRD");
//                oDT.Columns.Add("cardCode");
//                oDT.Columns.Add("cardName");
                
//                //traigo los codigos de socios de negocio y aprovecho las bondades de BoRecordset para traer el nombre
//                oRS = (SAPbobsCOM.Recordset)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
//                cQuery = "select cardcode from ocrd";
//                oRS.DoQuery(cQuery);

//                accion = 0;
//                ctrlCamposSN();

//                #region llenar el ListBox
//                //lleno el dataTable con los datos del BoRecordset para luego llenar el ListBox con el DataTable
//                //esto lo hago asi para poder usar las propiedades de DisplayMember y ValueMember de ListBox
//                if (oRS.RecordCount > 0)
//                {
//                    oBP = (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
//                    oBP.Browser.Recordset = oRS;

//                    oBP.Browser.MoveFirst();
//                    for (int i = 0; i <oRS.RecordCount; i++)
//                    {
//                        DataRow oDR = oDT.NewRow();
//                        oDR[0] = oBP.CardCode;
//                        oDR[1] = oBP.CardName;

//                        oDT.Rows.Add(oDR);
//                        oBP.Browser.MoveNext();
//                    }

//                    lstBP.DataSource = oDT;
//                    lstBP.DisplayMember = "cardName";
//                    lstBP.ValueMember = "cardcode";
//                }
//                #endregion 
//            }
//            catch (Exception er)
//            {
//                MessageBox.Show(er.Message);
//                toolStripStatusLabel1.Text = er.Message;
//            }
//        }

//        //Habilita los campos de socio de negocio para crear nuevo SN
//        private void btnAddBP_Click(object sender, EventArgs e)
//        {
//            accion = 1;
//            ctrlCamposSN();
//        }

//        //Habilita los campos de socio de negocio para editar nuevo SN
//        private void lstBP_DoubleClick(object sender, EventArgs e)
//        {
//            accion = 2;
//            ctrlCamposSN();
//            txtCodeBP.Text = lstBP.SelectedValue.ToString();
//            txtNameBP.Text = lstBP.Text;
//        }

//        //gestion de socios de negocio
//        private void btnSaveBP_Click(object sender, EventArgs e)
//        {
//            SAPbobsCOM.BusinessPartners objBP = null;
//            string errMessage = "";
//            int intErrCode = -1;
//            int intRetCode = -1;

//            if (accion == 1)
//            {
//                #region crea un socio de negocio

//                try
//                {
//                    if (objCompany != null && objCompany.Connected)
//                    {
//                        objBP = (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
//                        objBP.CardCode = txtCodeBP.Text;
//                        objBP.FederalTaxID = txtCodeBP.Text;
//                        objBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer;
//                        objBP.CardName = txtNameBP.Text;
//                        objBP.PayTermsGrpCode = 30;

//                        /* 
//                         * Esta linea se usa para agregar informacion a campos de usuario de SN
//                         * objBP.UserFields.Fields.Item("U_Fav_Rest").Value = "carbon";
//                         */

//                        intRetCode = objBP.Add();
//                        if (intRetCode != 0)
//                        {
//                            objCompany.GetLastError(out intErrCode, out errMessage);
//                            toolStripStatusLabel1.Text = errMessage;

//                            accion = 0;
//                            ctrlCamposSN();
//                        }
//                        else
//                        {
//                            btnGetBP.PerformClick();
//                            toolStripStatusLabel1.Text = "SN Creado con exito!";

//                            accion = 0;
//                            ctrlCamposSN();
//                        }
//                    }
//                }
//                catch (Exception er)
//                {
//                    toolStripStatusLabel1.Text = er.Message;
//                    accion = 0;
//                    ctrlCamposSN();
//                }
//                #endregion
//            }
//            else if (accion == 2)
//            {
//                #region Edita un socio de negocio

//                try
//                {
//                    if (objCompany != null && objCompany.Connected)
//                    {
//                        objBP = (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
//                        if (objBP.GetByKey(txtCodeBP.Text))
//                        {
                            
//                            /* 
//                            * Esta linea se usa para agregar informacion a campos de usuario de SN
//                            *objBP.UserFields.Fields.Item("U_Fav_Rest").Value = "Andres ";
//                            */
                            
//                            objBP.CardName = txtNameBP.Text ;

//                            intRetCode = objBP.Update();
//                            if (intRetCode != 0)
//                            {
//                                objCompany.GetLastError(out intErrCode, out errMessage);
//                                toolStripStatusLabel1.Text = errMessage;
//                                accion = 0;
//                                ctrlCamposSN();
//                            }
//                            else
//                            {
//                                btnGetBP.PerformClick();
//                                toolStripStatusLabel1.Text = "Informacion de SN almacenada con exito!";

//                                accion = 0;
//                                ctrlCamposSN();
//                            }
//                        }
//                        else
//                        {
//                            toolStripStatusLabel1.Text = "SN no encontrado";

//                            accion = 0;
//                            ctrlCamposSN();
//                        }
//                    }
//                }
//                catch (Exception er)
//                {
//                    toolStripStatusLabel1.Text = er.Message;

//                    accion = 0;
//                    ctrlCamposSN();
//                }
//                #endregion  
//            }
//        }

//        // Eliminacion de un socio de negocio
//        private void btnDelBP_Click(object sender, EventArgs e)
//        {
//            SAPbobsCOM.BusinessPartners objBP = null;
//            string errMessage = "";
//            int intErrCode = -1;
//            int intRetCode = -1;

//            try
//            {
//                if (objCompany != null && objCompany.Connected)
//                {
//                    objBP = (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
//                    if (objBP.GetByKey(txtCodeBP.Text))
//                    {
//                        intRetCode = objBP.Remove();
//                        if (intRetCode != 0)
//                        {
//                            objCompany.GetLastError(out intErrCode, out errMessage);
//                            toolStripStatusLabel1.Text = errMessage;
//                            accion = 0;
//                            ctrlCamposSN();
//                        }
//                        else
//                        {
//                            btnGetBP.PerformClick();
//                            toolStripStatusLabel1.Text = "SN eliminado con exito!";

//                            accion = 0;
//                            ctrlCamposSN();
//                        }
//                    }
//                    else
//                    {
//                        toolStripStatusLabel1.Text = "SN no encontrado";

//                        accion = 0;
//                        ctrlCamposSN();
//                    }
//                }
//            }
//            catch (Exception er)
//            {
//                toolStripStatusLabel1.Text = er.Message;

//                accion = 0;
//                ctrlCamposSN();
//            }

//        }

//        private void ctrlCamposSN()
//        {
//            switch (accion)
//            { 
//                case 1:
//                    txtCodeBP.Enabled = true;
//                    txtNameBP.Enabled = true;
//                    txtCodeBP.Text = null;
//                    txtNameBP.Text = null;

//                    txtCodeBP.Focus();

//                    btnAddBP.Enabled = false;
//                    btnSaveBP.Enabled = true;
//                    btnDelBP.Enabled = false;
//                    break;
//                case 2:
//                    txtCodeBP.Enabled = false;
//                    txtNameBP.Enabled = true;

//                    txtNameBP.Focus();

//                    btnAddBP.Enabled = false;
//                    btnSaveBP.Enabled = true;
//                    btnDelBP.Enabled = true;
//                    break;
//                default:
//                    txtCodeBP.Enabled = false;
//                    txtNameBP.Enabled = false;
//                    txtCodeBP.Text = null;
//                    txtNameBP.Text = null;

//                    btnAddBP.Enabled = true;
//                    btnSaveBP.Enabled = false;
//                    btnDelBP.Enabled = false;
//                    break;
//            }
//        }

//        #endregion 

//        //Busqueda dinamica del socio d negocio por el codigo
//        private void txtSnCod_TextChanged(object sender, EventArgs e)
//        {
//            SAPbobsCOM.BusinessPartners objBP = null;

//            try
//            {
//                if (objCompany != null && objCompany.Connected)
//                {
//                    objBP = (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
//                    if (objBP.GetByKey(this.txtSnCod.Text))
//                    {
//                        txtSnName.Text = objBP.CardName ;
//                        toolStripStatusLabel1.Text = "BP Found!";

//                        btnGetInvoices.Enabled = true;
//                    }
//                    else
//                    {
//                        toolStripStatusLabel1.Text = "BP Not Found!";
//                        txtSnName.Text = null;
//                        btnGetInvoices.Enabled = false;
//                    }
//                }
//            }
//            catch (Exception er)
//            {
//                toolStripStatusLabel1.Text = er.Message;
//            }
//        }

//        //Busqueda de las facturas cerradas en el sistema por SN
//        private void btnGetInvoices_Click(object sender, EventArgs e)
//        {
//            SAPbobsCOM.Recordset oRS = null;
//            string cQuery = null;
//            try
//            {
//                //creo un dataTable para almacenar los datos de nombre y codigo 
//                DataTable oDT = new DataTable("tOINV");
//                oDT.Columns.Add("No. Doc");
//                oDT.Columns.Add("Valor Pagado");
//                oDT.Columns.Add("Estatus");
//                oDT.Columns.Add("Marcar",typeof(bool) );

//                //traigo los Documentos del socio de negocio y aprovecho las bondades de BoRecordset para traer 
//                //el valor a pagar y el status

//                oRS = (SAPbobsCOM.Recordset)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
//                cQuery = "select docnum, paidtodate from oinv where cardcode = '" + txtSnCod.Text + "'" ;
                
//                oRS.DoQuery(cQuery);
                
//                if (oRS.RecordCount > 0)
//                {
//                    objDocumentBr = (SAPbobsCOM.Documents)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
//                    objDocumentBr.Browser.Recordset = oRS;
                    
//                    objDocumentBr.Browser.MoveFirst();

//                    #region LLenar el DataGrid
//                    for (int i = 0; i < oRS.RecordCount; i++)
//                    {
//                        DataRow oDR = oDT.NewRow();
//                        oDR[0] = objDocumentBr.DocNum;
//                        oDR[1] = oRS.Fields.Item(1).Value;
//                        switch (objDocumentBr.DocumentStatus.ToString())
//                        {
//                            case "bost_Close":
//                                oDR[2] = "C";
//                                break;
//                            default:
//                                oDR[2] = "O";
//                                break;
//                        }

//                        oDT.Rows.Add(oDR);
//                        objDocumentBr.Browser.MoveNext();
//                    }

//                    grdInvoice.DataSource = oDT;
//                    grdInvoice.Columns[0].Width = 62;
//                    grdInvoice.Columns[1].Width = 62;
//                    grdInvoice.Columns[2].Width = 62;
//                    grdInvoice.Columns[3].Width = 62;

//                    #endregion
//                }
//            }
//            catch (Exception er)
//            {
//                MessageBox.Show(er.Message);
//                toolStripStatusLabel1.Text = er.Message;
//            }
//        }
    }
}

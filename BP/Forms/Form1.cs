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
    public partial class Form1 : Form
    {
        SAPbobsCOM.Company objCompany = null;
        SAPbobsCOM.Documents objDocumentBr = null;

        public Form1()
        {
            InitializeComponent();
        }
        


        private void button1_Click(object sender, EventArgs e)
        {
            string errMessage = "";
            int intErrCode = -1;
            int intRetCode = -1;

            try
            {
                objCompany = new SAPbobsCOM.Company();
                objCompany.UseTrusted = true;
                objCompany.Server = ".";
                objCompany.UserName = "manager";
                objCompany.Password = "manager";
                objCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                objCompany.CompanyDB = "SBODemo_Master";

                intRetCode = objCompany.Connect();
                if (intRetCode != 0)
                {
                    objCompany.GetLastError(out intErrCode, out errMessage);
                    MessageBox.Show(errMessage);
                }
                else
                {
                    MessageBox.Show("Connected to " + objCompany.CompanyName);
                    this.llenarcombo();
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.BusinessPartners objBP = null;
            string errMessage = "";
            int intErrCode = -1;
            int intRetCode = -1;

            
            try
            {
                if (objCompany != null && objCompany.Connected)
                {
                    objBP = (SAPbobsCOM.BusinessPartners) objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    objBP.CardCode = this.txtCardCode.Text;
                    objBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer;
                    objBP.CardName = "BP Ejemplo 1";
                    objBP.UserFields.Fields.Item("U_Fav_Rest").Value = "carbon";
                    intRetCode = objBP.Add();
                    if (intRetCode != 0)
                    {
                        objCompany.GetLastError(out intErrCode, out errMessage);
                        MessageBox.Show(errMessage);
                    }
                    else
                    {
                        MessageBox.Show("Done!");
                    }


                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.BusinessPartners objBP = null;
            string errMessage = "";
            int intErrCode = -1;
            int intRetCode = -1;


            try
            {
                if (objCompany != null && objCompany.Connected)
                {
                    objBP = (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    if (objBP.GetByKey(this.txtCardCode.Text))
                    {
                        objBP.UserFields.Fields.Item("U_Fav_Rest").Value = "Andres ";
                        intRetCode = objBP.Update();
                        if (intRetCode != 0)
                        {
                            objCompany.GetLastError(out intErrCode, out errMessage);
                            MessageBox.Show(errMessage);
                        }
                        else
                        {
                            MessageBox.Show("Done!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("BP Not Found!");
                    }
                    


                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.BusinessPartners objBP = null;
            string errMessage = "";
            int intErrCode = -1;
            int intRetCode = -1;


            try
            {
                if (objCompany != null && objCompany.Connected)
                {
                    objBP = (SAPbobsCOM.BusinessPartners)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    if (objBP.GetByKey(this.txtCardCode.Text))
                    {
                        
                        intRetCode = objBP.Remove();
                        if (intRetCode != 0)
                        {
                            objCompany.GetLastError(out intErrCode, out errMessage);
                            MessageBox.Show(errMessage);
                        }
                        else
                        {
                            MessageBox.Show("Done!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("BP Not Found!");
                    }



                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            string errMessage = "";
            int intErrCode = -1;
            int intRetCode = -1;


            try {
                SAPbobsCOM.Documents objDocumento = null;
            

                if (objCompany != null && objCompany.Connected)
                {
                    objDocumento = (SAPbobsCOM.Documents)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                    objDocumento.CardCode = txtCardCode.Text;
                    objDocumento.DocDueDate = Convert.ToDateTime(txtFecha.Text);
                    objDocumento.Lines.ItemCode = txtItemCode.Text;
                    objDocumento.Lines.Quantity = 10;
                        
                        intRetCode = objDocumento.Add();
                        if (intRetCode != 0)
                        {
                            objCompany.GetLastError(out intErrCode, out errMessage);
                            MessageBox.Show(errMessage);
                        }
                        else
                        {
                            MessageBox.Show("Done!");
                        }
                    }
                    


                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string errMessage = "";
            int intErrCode = -1;
            int intRetCode = -1;


            try
            {
                SAPbobsCOM.Documents objDocumento = null;


                if (objCompany != null && objCompany.Connected)
                {
                    objDocumento = (SAPbobsCOM.Documents)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes);
                    objDocumento.CardCode = txtCardCode.Text;
                    objDocumento.DocDueDate = Convert.ToDateTime(txtFecha.Text);

                    objDocumento.Lines.BaseType = 13;
                    objDocumento.Lines.BaseEntry = 45;
                    objDocumento.Lines.BaseLine = 0;
                    objDocumento.Lines.Quantity = 5;


                    intRetCode = objDocumento.Add();
                    if (intRetCode != 0)
                    {
                        objCompany.GetLastError(out intErrCode, out errMessage);
                        MessageBox.Show(errMessage);
                    }
                    else
                    {
                        MessageBox.Show("Done!");
                    }
                }




            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            SAPbobsCOM.Recordset objRecordSet = null;
            string strSQL = "";


            try
            {
                


                if (objCompany != null && objCompany.Connected)
                {
                    objRecordSet = (SAPbobsCOM.Recordset)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    strSQL = "select count(cardcode) as Modif from OINV where DocStatus = 'O' and CardCode='"+txtCardCode.Text+"'";
                    objRecordSet.DoQuery(strSQL);

                    if (objRecordSet.RecordCount > 0)
                    {
                        objRecordSet.MoveFirst();
                        MessageBox.Show("It has " + Convert.ToString(objRecordSet.Fields.Item("Modif").Value) + " invoices open");
                    }
                    else
                    {
                        MessageBox.Show("No items found");
                    }


                    
                    
                }




            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (objDocumentBr != null)
                {
                    if (!objDocumentBr.Browser.BoF)
                    {
                        objDocumentBr.Browser.MoveFirst();
                    }
                    int installTotalCount = objDocumentBr.Installments.Count;
                    double dbTotal = 0;
                    if(installTotalCount > 1)
                    {
                        for(int i=0; i < installTotalCount; i ++)
                        {
                            objDocumentBr.Installments.SetCurrentLine(i);
                            dbTotal += objDocumentBr.Installments.Total;

                        }
                    }
                    else
                    {
                        dbTotal = objDocumentBr.Installments.Total;
                    }

                    txtDocTotal.Text = dbTotal.ToString();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.Recordset objRecordSet = null;
            string strSQL = "";
            
            try
            {
                if (objCompany != null && objCompany.Connected)
                {
                    objRecordSet = (SAPbobsCOM.Recordset)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    strSQL = "select docentry from oinv where CardCode='"+txtCardCode.Text+"' AND DocStatus='O'";
                    objRecordSet.DoQuery(strSQL);
                    if (objRecordSet.RecordCount > 0)
                    {
                        objDocumentBr = (SAPbobsCOM.Documents)objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                        objDocumentBr.Browser.Recordset = objRecordSet;
                        MessageBox.Show("Done");

                    }
                    else
                    {
                        MessageBox.Show("No items found");
                    }




                }




            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (objDocumentBr != null)
                {
                    if (objDocumentBr.Browser.BoF)
                    {
                        objDocumentBr.Browser.MoveLast();
                        
                    }
                    else if (objDocumentBr.Browser.EoF)
                    {
                        objDocumentBr.Browser.MoveFirst();
                    }
                    else
                    {
                        objDocumentBr.Browser.MovePrevious();
                    }
                    txtDocTotal.Text = objDocumentBr.Lines.Count.ToString();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (objDocumentBr != null)
                {
                    if (objDocumentBr.Browser.EoF)
                    {
                        objDocumentBr.Browser.MoveFirst();

                    }
                    else if (objDocumentBr.Browser.BoF)
                    {
                        objDocumentBr.Browser.MoveLast();
                    }
                    else
                    {
                        objDocumentBr.Browser.MoveNext();
                    }
                    txtDocTotal.Text = objDocumentBr.Lines.Count.ToString();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (objDocumentBr != null)
                {
                    if (!objDocumentBr.Browser.EoF)
                    {
                        objDocumentBr.Browser.MoveLast();
                        
                    }
                    txtDocTotal.Text = objDocumentBr.Lines.Count.ToString();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                int coderr = -1;
                int coderrRet = -1;
                string msgErr = "";

                if (objCompany != null && objCompany.Connected)
                {
                    SAPbobsCOM.UserTable uTable = objCompany.UserTables.Item("ACAD1");
                    if (uTable != null)
                    {
                        uTable.Code = txtcod.Text;
                        uTable.Name = txtname.Text;
                        uTable.UserFields.Fields.Item("U_Name").Value = txtnombre.Text;
                        uTable.UserFields.Fields.Item("U_Partner").Value = (string)this.comboBox1.SelectedItem;
                        uTable.UserFields.Fields.Item("U_Nac").Value = txtnacion.Text;
                        coderrRet= uTable.Add();

                        if (coderrRet != 0)
                        {
                            coderr = objCompany.GetLastErrorCode();
                            msgErr = objCompany.GetLastErrorDescription();
                            MessageBox.Show(msgErr);
                        }
                        else
                        {
                            MessageBox.Show("Done!");
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        
        private void llenarcombo() // llenar un objeto Combo Box
        {
            SAPbobsCOM.UserTable Utable = objCompany.UserTables.Item("ACAD1");
            SAPbobsCOM.ValidValues objValidValues = Utable.UserFields.Fields.Item("U_partner").ValidValues;

            for (int i = 0; i < objValidValues.Count; i++)
            {
                this.comboBox1.Items.Add(objValidValues.Item(i).Value);
            }
        }

    }
}

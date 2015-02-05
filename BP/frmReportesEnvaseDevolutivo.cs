using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SAPbobsCOM;

namespace BP
{
    public partial class frmReportesEnvaseDevolutivo : Form
    {
        public frmReportesEnvaseDevolutivo()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            strSql1.AppendLine("select U_objType, U_docNum, U_docEntry, c.CardCode, c.CardName, U_itemCode, ItemName, sum(U_delivered) delivered, sum (U_returned) returned, sum(U_maintenance) maintenance, sum (U_ready ) ready ");
            strSql1.AppendLine("FROM [@ORK_ENVASE_DEV] a ");
            strSql1.AppendLine("inner join OITM b on a.U_itemCode = b.ItemCode ");

            strSql2.AppendLine("group by U_objType, U_docNum, U_docEntry, c.CardCode, c.CardName,U_itemCode, ItemName ");
            strSql2.AppendLine("order by CardName, a.U_docEntry");
            switch (cbReporte.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:                    
                    strSql1.AppendLine("inner join ODLN c on a.U_docEntry = c.DocEntry and a.U_objType = c.ObjType ");
                    strSql1.AppendLine("where U_objType = 15 ");                    
                    break;
                case 4:
                    strSql1.AppendLine("inner join OPDN c on a.U_docEntry = c.DocEntry and a.U_objType = c.ObjType ");
                    strSql1.AppendLine("where U_objType = 20 ");
                    break;
                default:
                    break;
            }

            string strSql = strSql1.ToString() + strSql2.ToString();
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
            IDataReader misDatos = ClaseDatos.procesaDataReader(strSql);

            grdReporte.Rows.Clear();

            while (misDatos.Read())
            {
                object[] misValores = new object[11];
                misValores[0] = misDatos.GetValue(0);
                misValores[1] = misDatos.GetValue(1);
                misValores[2] = misDatos.GetValue(2);
                misValores[3] = misDatos.GetValue(3);
                misValores[4] = misDatos.GetValue(4);
                misValores[5] = misDatos.GetValue(5);
                misValores[6] = misDatos.GetValue(6);
                misValores[7] = misDatos.GetValue(7);
                misValores[8] = misDatos.GetValue(8);
                misValores[9] = misDatos.GetValue(9);
                misValores[10] = misDatos.GetValue(10);

                grdReporte.Rows.Add(misValores);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Vendedor.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(grdReporte, sfd.FileName); // Here dataGridview1 is your grid view name 
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }
    }
}

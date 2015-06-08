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

        private void frmReportesEnvaseDevolutivo_Load(object sender, EventArgs e)
        {
            dpRemisionDesde.Value = DateTime.Now;
            dpRemisionHasta.Value = DateTime.Now;

            dpReciboDesde.Value = DateTime.Now;
            dpReciboHasta.Value = DateTime.Now;
        }

        private void filtraFechaRemision_CheckedChanged(object sender, EventArgs e)
        {
            if (filtraFechaRemision.Checked)
            {
                dpRemisionDesde.Enabled = true;
                dpRemisionHasta.Enabled = true;


                filtraFechaRecibo.Checked = false;
                dpReciboDesde.Enabled = false;
                dpReciboHasta.Enabled = false;


                filtraProveedor.Checked = false;
                cboProveedor.Items.Clear();
                cboProveedor.DataSource = null;
                cboProveedor.Enabled = false;
            }
            else
            {
                dpRemisionDesde.Enabled = false;
                dpRemisionHasta.Enabled = false;
            }
        }

        private void filtraFechaRecibo_CheckedChanged(object sender, EventArgs e)
        {
            if (filtraFechaRecibo.Checked)
            {
                dpReciboDesde.Enabled = true;
                dpReciboHasta.Enabled = true;

                filtraFechaRemision.Checked = false;
                dpRemisionDesde.Enabled = false;
                dpRemisionHasta.Enabled = false;


                filtraProveedor.Checked = false;
                cboProveedor.Items.Clear();
                cboProveedor.DataSource = null;
                cboProveedor.Enabled = false;
            }
            else
            {
                dpReciboDesde.Enabled = false;
                dpReciboHasta.Enabled = false;
            }
        }

        private void filtraProveedor_CheckedChanged(object sender, EventArgs e)
        {
            if (filtraProveedor.Checked)
            {
                List<Proveedor> proveedores = ClsEnvaseDevolutivo.GetProveedores();

                cboProveedor.ValueMember = "CardCode";
                cboProveedor.DisplayMember = "CardName";
                cboProveedor.DataSource = proveedores;

                cboProveedor.Enabled = true;

                filtraFechaRemision.Checked = false;
                dpRemisionDesde.Enabled = false;
                dpRemisionHasta.Enabled = false;

                filtraFechaRecibo.Checked = false;
                dpReciboDesde.Enabled = false;
                dpReciboHasta.Enabled = false;
            }
            else
            {
                cboProveedor.Items.Clear();
                cboProveedor.DataSource = null;
                cboProveedor.Enabled = false;
            }
        }

        private void btnGeneraRepCartera_Click(object sender, EventArgs e)
        {
            List<reporteCarteraCliente> reporte = new List<reporteCarteraCliente>();
            if (filtraFechaRemision.Checked)
                reporte = ClsEnvaseDevolutivo.GetReporteCarteraCliente(dpRemisionDesde.Value, dpRemisionHasta.Value);

            if (filtraFechaRecibo.Checked)
                reporte = ClsEnvaseDevolutivo.GetReporteCarteraClienteRecibos(dpReciboDesde.Value, dpReciboHasta.Value);



            limpiarGrids(grdCarteraClientes);

            grdCarteraClientes.Columns.Add("fecha", "Linea Entrega");
            grdCarteraClientes.Columns.Add("numeroDocumento", "Remisión");
            grdCarteraClientes.Columns.Add("codigoSocioNegocio", "Cod. SN");
            grdCarteraClientes.Columns.Add("socioNegocio", "Nombre SN");
            grdCarteraClientes.Columns.Add("nombreVendedor", "Vendedor");
            grdCarteraClientes.Columns.Add("codigoArticulo", "Cod. Artículo");
            grdCarteraClientes.Columns.Add("nombreArticulo", "Artículo");
            grdCarteraClientes.Columns.Add("entregado", "Cant. Entregada");
            grdCarteraClientes.Columns.Add("retornado", "Cant. Retornada");
            grdCarteraClientes.Columns.Add("enReacondicionamiento", "Cant.Reacondicionado");
            grdCarteraClientes.Columns.Add("saldo", "Saldo");

            grdCarteraClientes.Columns[0].DataPropertyName = "fecha";
            grdCarteraClientes.Columns[1].DataPropertyName = "numeroDocumento";
            grdCarteraClientes.Columns[2].DataPropertyName = "codigoSocioNegocio";
            grdCarteraClientes.Columns[3].DataPropertyName = "socioNegocio";
            grdCarteraClientes.Columns[4].DataPropertyName = "nombreVendedor";
            grdCarteraClientes.Columns[5].DataPropertyName = "codigoArticulo";
            grdCarteraClientes.Columns[6].DataPropertyName = "nombreArticulo";
            grdCarteraClientes.Columns[7].DataPropertyName = "entregado";
            grdCarteraClientes.Columns[8].DataPropertyName = "retornado";
            grdCarteraClientes.Columns[9].DataPropertyName = "enReacondicionamiento";
            grdCarteraClientes.Columns[10].DataPropertyName = "saldo";


            grdCarteraClientes.DataSource = reporte;
        }

        private void btnGenerarKC_Click(object sender, EventArgs e)
        {
            List<reporteKardex> reporte = new List<reporteKardex>();

            reporte = ClsEnvaseDevolutivo.GetReporteKardexCliente(dpKCDesde.Value, dpKCHasta.Value);

            limpiarGrids(grdKardexClientes);

            grdKardexClientes.Columns.Add("tipoDocumento", "Tipo de documento");
            grdKardexClientes.Columns.Add("fechaDocumento", "fecha documento");
            grdKardexClientes.Columns.Add("numeroDocumento", "Numero documento");
            grdKardexClientes.Columns.Add("fechaFactura", "Fecha factura");
            grdKardexClientes.Columns.Add("numeroFactura", "No. Factura");
            grdKardexClientes.Columns.Add("codigoSocioNegocio", "Cod SN");
            grdKardexClientes.Columns.Add("socioNegocio", "Nombre SN");
            grdKardexClientes.Columns.Add("codigoArticulo", "Cod. artículo");
            grdKardexClientes.Columns.Add("nombreArticulo", "Nombre artículo");
            grdKardexClientes.Columns.Add("entradas", "Entradas");
            grdKardexClientes.Columns.Add("salidas", "Salidas");
            grdKardexClientes.Columns.Add("saldo", "Saldo");

            grdKardexClientes.Columns[0].DataPropertyName = "tipoDocumento";
            grdKardexClientes.Columns[1].DataPropertyName = "fechaDocumento";
            grdKardexClientes.Columns[2].DataPropertyName = "numeroDocumento";
            grdKardexClientes.Columns[3].DataPropertyName = "fechaFactura";
            grdKardexClientes.Columns[4].DataPropertyName = "numeroFactura";
            grdKardexClientes.Columns[5].DataPropertyName = "codigoSocioNegocio";
            grdKardexClientes.Columns[6].DataPropertyName = "socioNegocio";
            grdKardexClientes.Columns[7].DataPropertyName = "codigoArticulo";
            grdKardexClientes.Columns[8].DataPropertyName = "nombreArticulo";
            grdKardexClientes.Columns[9].DataPropertyName = "entradas";
            grdKardexClientes.Columns[10].DataPropertyName = "salidas";
            grdKardexClientes.Columns[11].DataPropertyName = "saldo";


            grdKardexClientes.DataSource = reporte;
        }

        private void limpiarGrids(DataGridView grid)
        {
            grid.DataSource = null;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
        }

        private void btnGenerarKP_Click(object sender, EventArgs e)
        {
            List<reporteKardex> reporte = new List<reporteKardex>();

            reporte = ClsEnvaseDevolutivo.GetReporteKardexCliente(dpKCDesde.Value, dpKCHasta.Value);

            limpiarGrids(grdKardexProveedores);

            grdKardexProveedores.Columns.Add("tipoDocumento", "Tipo de documento");
            grdKardexProveedores.Columns.Add("fechaDocumento", "fecha documento");
            grdKardexProveedores.Columns.Add("numeroDocumento", "Numero documento");
            grdKardexProveedores.Columns.Add("codigoSocioNegocio", "Cod SN");
            grdKardexProveedores.Columns.Add("socioNegocio", "Nombre SN");
            grdKardexProveedores.Columns.Add("codigoArticulo", "Cod. artículo");
            grdKardexProveedores.Columns.Add("nombreArticulo", "Nombre artículo");
            grdKardexProveedores.Columns.Add("entradas", "Entradas");
            grdKardexProveedores.Columns.Add("salidas", "Salidas");
            grdKardexProveedores.Columns.Add("saldo", "Saldo");

            grdKardexProveedores.Columns[0].DataPropertyName = "tipoDocumento";
            grdKardexProveedores.Columns[1].DataPropertyName = "fechaDocumento";
            grdKardexProveedores.Columns[2].DataPropertyName = "numeroDocumento";
            grdKardexProveedores.Columns[3].DataPropertyName = "codigoSocioNegocio";
            grdKardexProveedores.Columns[4].DataPropertyName = "socioNegocio";
            grdKardexProveedores.Columns[5].DataPropertyName = "codigoArticulo";
            grdKardexProveedores.Columns[6].DataPropertyName = "nombreArticulo";
            grdKardexProveedores.Columns[7].DataPropertyName = "entradas";
            grdKardexProveedores.Columns[8].DataPropertyName = "salidas";
            grdKardexProveedores.Columns[9].DataPropertyName = "saldo";


            grdKardexProveedores.DataSource = reporte;
        }

        private void btnExportaRepCartera_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Vendedor.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(grdCarteraClientes, sfd.FileName); // Here dataGridview1 is your grid view name 
            }
        }

        private void btnExportarKC_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Vendedor.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(grdKardexClientes, sfd.FileName); // Here dataGridview1 is your grid view name 
            }
        }

        private void btnExportarKP_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Vendedor.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(grdKardexProveedores, sfd.FileName); // Here dataGridview1 is your grid view name 
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

        //private void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    StringBuilder strSql1 = new StringBuilder();
        //    StringBuilder strSql2 = new StringBuilder();

        //    strSql1.AppendLine("select U_objType, U_docNum, U_docEntry, c.CardCode, c.CardName, U_itemCode, ItemName, sum(U_delivered) delivered, sum (U_returned) returned, sum(U_maintenance) maintenance, sum (U_ready ) ready ");
        //    strSql1.AppendLine("FROM [@ORK_ENVASE_DEV] a ");
        //    strSql1.AppendLine("inner join OITM b on a.U_itemCode = b.ItemCode ");

        //    strSql2.AppendLine("group by U_objType, U_docNum, U_docEntry, c.CardCode, c.CardName,U_itemCode, ItemName ");
        //    strSql2.AppendLine("order by CardName, a.U_docEntry");
        //    switch (cbReporte.SelectedIndex)
        //    {
        //        case 0:
        //            break;
        //        case 1:
        //            break;
        //        case 2:
        //            break;
        //        case 3:                    
        //            strSql1.AppendLine("inner join ODLN c on a.U_docEntry = c.DocEntry and a.U_objType = c.ObjType ");
        //            strSql1.AppendLine("where U_objType = 15 ");                    
        //            break;
        //        case 4:
        //            strSql1.AppendLine("inner join OPDN c on a.U_docEntry = c.DocEntry and a.U_objType = c.ObjType ");
        //            strSql1.AppendLine("where U_objType = 20 ");
        //            break;
        //        default:
        //            break;
        //    }

        //    string strSql = strSql1.ToString() + strSql2.ToString();
        //    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
        //    IDataReader misDatos = ClaseDatos.procesaDataReader(strSql);

        //    grdReporte.Rows.Clear();

        //    while (misDatos.Read())
        //    {
        //        object[] misValores = new object[11];
        //        misValores[0] = misDatos.GetValue(0);
        //        misValores[1] = misDatos.GetValue(1);
        //        misValores[2] = misDatos.GetValue(2);
        //        misValores[3] = misDatos.GetValue(3);
        //        misValores[4] = misDatos.GetValue(4);
        //        misValores[5] = misDatos.GetValue(5);
        //        misValores[6] = misDatos.GetValue(6);
        //        misValores[7] = misDatos.GetValue(7);
        //        misValores[8] = misDatos.GetValue(8);
        //        misValores[9] = misDatos.GetValue(9);
        //        misValores[10] = misDatos.GetValue(10);

        //        grdReporte.Rows.Add(misValores);
        //    }
        //}

        //private void btnExportar_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog sfd = new SaveFileDialog();
        //    sfd.Filter = "Excel Documents (*.xls)|*.xls";
        //    sfd.FileName = "Vendedor.xls";
        //    if (sfd.ShowDialog() == DialogResult.OK)
        //    {
        //        //ToCsV(dataGridView1, @"c:\export.xls");
        //        ToCsV(grdReporte, sfd.FileName); // Here dataGridview1 is your grid view name 
        //    }
        //}

        

    }
}

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
    public partial class frmEnvaseDevolutivo : Form
    {
        EnvaseDevolutivo envaseDevolutivo = new EnvaseDevolutivo();
        List<MktDocHeader> lsSaleDocuments = new List<MktDocHeader>();
        List<MktDocHeader> lsPurchaseDocuments = new List<MktDocHeader>();
        List<ItemSummary> documentItems = new List<ItemSummary>();
        List<ItemSummary> documentItemsOriginal = new List<ItemSummary>();
        DataGridViewRow selectedRow = new DataGridViewRow();

        int objType;

        public frmEnvaseDevolutivo()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmEnvaseDevolutivo_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format("Ultima sincronizacion: {0}, pulse F2 para sincronizar ahora.", envaseDevolutivo.ultimaSincronizacion.ToString("yyyy-MM-dd"));

            if (envaseDevolutivo.ultimaSincronizacion < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                if (MessageBox.Show(string.Format("No sincroniza desde {0}. Desea sincronizar los datos en este momento?",
                    envaseDevolutivo.ultimaSincronizacion.ToString("yyyy-MM-dd")), "Sincronizacion de envase devolutivo",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    SaveWkr.RunWorkerAsync();

            BtnAceptar.Enabled = false;
            btnGuardar.Enabled = false;
            txtCosto.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            objType = 0;
            string docEntry = txtRemision.Text;

            if (rbClientes.Checked)
                objType = 15;

            if (rbProveedores.Checked)
                objType = 20;

            try
            {
                documentItems = envaseDevolutivo.SearcDocument(objType, docEntry);
                documentItemsOriginal = envaseDevolutivo.SearcDocument(objType, docEntry);
                if (documentItems.Count > 0)
                {
                    bindGrid();
                    habilitaRadioButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SaveWkr_DoWork(object sender, DoWorkEventArgs e)
        {
            lsSaleDocuments.AddRange(envaseDevolutivo.GetBaseDocumentsForSync(15));
            lsPurchaseDocuments.AddRange(envaseDevolutivo.GetBaseDocumentsForSync(20));

            int totalRecords = lsSaleDocuments.Count + lsPurchaseDocuments.Count;
            int processedRecords = 1;
            int advancePerc = 0;

            foreach (MktDocHeader item in lsSaleDocuments)
            {
                //si no existe el documento se crea con sus productos en tabla de envase devolutivo
                if (!envaseDevolutivo.ExistsBaseDocument(item))
                    envaseDevolutivo.SaveBaseDocument(item);

                //Busca y guarda Notas Credito relacionadas
                envaseDevolutivo.SaveCreditNoteReturnDocument(item, 'N');

                //Busca y guarda devoluciones relacionadas
                envaseDevolutivo.SaveCreditNoteReturnDocument(item, 'R');

                advancePerc = (100 * processedRecords) / totalRecords;
                SaveWkr.ReportProgress(advancePerc, "Guardando Información de clientes");
                processedRecords++;
            }

            foreach (MktDocHeader item in lsPurchaseDocuments)
            {
                //si no existe el documento se crea con sus productos en tabla de envase devolutivo
                if (!envaseDevolutivo.ExistsBaseDocument(item))
                    envaseDevolutivo.SaveBaseDocument(item);

                //Busca y guarda Notas Credito relacionadas
                envaseDevolutivo.SaveCreditNoteReturnDocument(item, 'N');

                //Busca y guarda devoluciones relacionadas
                envaseDevolutivo.SaveCreditNoteReturnDocument(item, 'R');

                advancePerc = (100 * processedRecords) / totalRecords;
                SaveWkr.ReportProgress(advancePerc, "Guardando Información de proveedores");
                processedRecords++;
            }

            SaveWkr.ReportProgress(100, "Actualizando ultima fecha de sincronizacion");
            envaseDevolutivo.UpdateLastSyncDate();
        }

        private void SaveWkr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = e.UserState.ToString();
        }

        private void SaveWkr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = "Proceso terminado";
        }

        private void frmEnvaseDevolutivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                SaveWkr.RunWorkerAsync();
            }
        }

        private void grdProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (grdProductos.SelectedRows.Count > 0)
            {
                selectedRow = grdProductos.SelectedRows[0];
                string item = selectedRow.Cells["U_itemCode"].Value.ToString();
                txtProducto.Text = item;
            }
        }

        private void rbEntrada_CheckedChanged(object sender, EventArgs e)
        {
            BtnAceptar.Enabled = true;
            txtCosto.Enabled = false;
            deshabilitaRadioButtons();
        }

        private void rbMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            BtnAceptar.Enabled = true;
            txtCosto.Enabled = false;
            deshabilitaRadioButtons();
        }

        private void rbListo_CheckedChanged(object sender, EventArgs e)
        {
            BtnAceptar.Enabled = true;
            txtCosto.Enabled = true;
            deshabilitaRadioButtons();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            ItemSummary original = documentItemsOriginal.Where(x => x.U_itemCode == txtProducto.Text).FirstOrDefault();
            ItemSummary modificado = documentItems.Where(x => x.U_itemCode == txtProducto.Text).FirstOrDefault();

            if (rbEntrada.Checked)
            {
                if (original.U_delivered >= (original.U_returned + int.Parse(txtCantidad.Text)))
                {
                    modificado.U_returned = int.Parse(txtCantidad.Text);
                    modificado.U_maintenance = 0;
                    modificado.U_ready = 0;
                    modificado.U_costoReady = 0;
                }
                else
                {
                    MessageBox.Show(string.Format("La cantidad a devolver no puede ser mayor a la diferencia entre cantidad enviada y la cantidad devuelta a la fecha ({0} unidades)", original.U_returned));
                    return;
                }
            }

            if (rbMantenimiento.Checked)
            {
                if (original.U_returned >= (original.U_maintenance + int.Parse(txtCantidad.Text)))
                {
                    modificado.U_maintenance = int.Parse(txtCantidad.Text);

                    modificado.U_returned = 0;
                    modificado.U_ready = 0;
                    modificado.U_costoReady = 0;
                }
                else
                {
                    MessageBox.Show(string.Format("La cantidad a reacondicionar no puede ser mayor a la diferencia entre cantidad devuelta y la cantidad en reacondicionamiento a la fecha ({0} unidades)", original.U_maintenance));
                    return;
                }
            }

            if (rbListo.Checked)
            {
                if (original.U_maintenance >= (int.Parse(txtCantidad.Text)))
                {
                    modificado.U_maintenance = (int.Parse(txtCantidad.Text) * -1);
                    modificado.U_ready = int.Parse(txtCantidad.Text);
                    modificado.U_returned = 0;

                    int costo;

                    if (int.TryParse(txtCosto.Text, out costo))
                        modificado.U_costoReady = costo;
                }
                else
                {
                    MessageBox.Show(string.Format("La cantidad que sale de reacondicionamiento no puede ser mayor a la cantidad en reacondicionamiento a la fecha ({0} unidades)", original.U_maintenance));
                    return;
                }
            }

            bindGrid();
            limpiaInfoProd();
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int targetType = 0;

                if (rbEntrada.Checked)
                    targetType = 501;

                if (rbMantenimiento.Checked)
                    targetType = 502;

                if (rbListo.Checked)
                    targetType = 503;

                foreach (ItemSummary item in documentItems)
                {
                    ItemDetail newRecord = new ItemDetail()
                    {
                        U_itemCode = item.U_itemCode,
                        U_targetType = targetType,
                        U_returned = item.U_returned,
                        U_maintenance = item.U_maintenance,
                        U_ready = item.U_ready,
                        U_costoReady = item.U_costoReady,
                        U_docEntry = item.u_docEntry,
                        U_objType = objType, 
                        U_docNum = txtRemision.Text
                    };

                    envaseDevolutivo.Create(newRecord, targetType);
                }

                MessageBox.Show("Informacion guardada con exito");

                grdProductos.DataSource = null;
                grdProductos.AutoGenerateColumns = false;
                grdProductos.Columns.Clear();

                txtRemision.Text = "";
                limpiaInfoProd();
                habilitaRadioButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bindGrid()
        {
            grdProductos.DataSource = null;
            grdProductos.AutoGenerateColumns = false;
            grdProductos.Columns.Clear();

            grdProductos.Columns.Add("U_itemCode", "Item");
            grdProductos.Columns.Add("U_delivered", "Envases despachados");
            grdProductos.Columns.Add("U_returned", "Envases retornados");
            grdProductos.Columns.Add("U_maintenance", "En Reacondicionamiento");
            grdProductos.Columns.Add("U_ready", "Reacondicionados");
            grdProductos.Columns.Add("U_costoReady", "Costo reacondicionamiento");

            grdProductos.Columns[0].DataPropertyName = "U_itemCode";
            grdProductos.Columns[1].DataPropertyName = "U_delivered";
            grdProductos.Columns[2].DataPropertyName = "U_returned";
            grdProductos.Columns[3].DataPropertyName = "U_maintenance";
            grdProductos.Columns[4].DataPropertyName = "U_ready";
            grdProductos.Columns[5].DataPropertyName = "U_costoReady";
            grdProductos.DataSource = documentItems;
        }

        private void deshabilitaRadioButtons()
        {
            rbEntrada.Enabled = false;
            rbMantenimiento.Enabled = false;
            rbListo.Enabled = false;
        }

        private void habilitaRadioButtons()
        {
            rbEntrada.Enabled = true;
            rbMantenimiento.Enabled = true;
            rbListo.Enabled = true;

            rbEntrada.Checked = false;
            rbMantenimiento.Checked = false;
            rbListo.Checked = false;
        }

        private void limpiaInfoProd()
        {
            txtProducto.Text = "";
            txtCantidad.Text = "";
            txtCosto.Text = "";
        }
    }
}

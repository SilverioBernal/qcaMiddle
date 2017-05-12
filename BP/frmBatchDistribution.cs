using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entities;

namespace BP
{
    public partial class frmBatchDistribution : Form
    {
        ClsDataInventario data = new ClsDataInventario();
        List<Almacen> bodegasOrigen = new List<Almacen>();
        List<Almacen> bodegas = new List<Almacen>();
        List<Almacen> bodegasDestino = new List<Almacen>();

        LoteoArticulo loteoArticulo = new LoteoArticulo();

        public frmBatchDistribution()
        {
            InitializeComponent();
        }

        private void frmBatchDistribution_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            statusLabel.Text = "Buscando bodegas origen";
            bodegasOrigen.Add(new Almacen() { WhsCode = string.Empty, WhsName = "Seleccione un almacén" });
            bodegasOrigen.AddRange(data.ListarAlmacenesReloteo());
            cboWhFrom.DataSource = bodegasOrigen;
            cboWhFrom.ValueMember = "WhsCode";
            cboWhFrom.DisplayMember = "WhsName";

            statusLabel.Text = "Buscando bodegas destino";
            bodegas = data.ListarAlmacenes();

            bodegasDestino.Add(new Almacen() { WhsCode = string.Empty, WhsName = "Seleccione un almacén" });
            foreach (Almacen item in bodegas)
            {
                if (bodegasOrigen.Where(x => x.WhsCode == item.WhsCode).Count() == 0)
                    bodegasDestino.Add(item);
            }

            cboWhTo.DataSource = bodegasDestino;
            cboWhTo.ValueMember = "WhsCode";
            cboWhTo.DisplayMember = "WhsName";

            statusLabel.Text = "Listo";
        }

        private void grdItem_SelectionChanged(object sender, EventArgs e)
        {
            if (grdItem.SelectedRows.Count > 0)
            {
                DataGridViewRow itemSelectedRow = new DataGridViewRow();
                itemSelectedRow = grdItem.SelectedRows[0];
                string itemCode = itemSelectedRow.Cells["ItemCode"].Value.ToString();

                cleanGrids(grdBatch);

                List<Lote> lotes = data.ConsultarLotesDisponibles(itemCode, cboWhFrom.SelectedValue.ToString());

                grdBatch.Columns.Add("DistNumber", "Num. lote");
                grdBatch.Columns.Add("Quantity", "Cantidad");
                grdBatch.Columns.Add("ExpDate", "Expira");
                grdBatch.Columns.Add("MnfDate", "Fabricado");

                grdBatch.Columns[0].DataPropertyName = "DistNumber";
                grdBatch.Columns[1].DataPropertyName = "Quantity";
                grdBatch.Columns[2].DataPropertyName = "ExpDate";
                grdBatch.Columns[3].DataPropertyName = "MnfDate";

                grdBatch.DataSource = lotes;
            }
        }

        private void grdTrn_SelectionChanged(object sender, EventArgs e)
        {
            if (grdTrn.SelectedRows.Count > 0)
                btnDeleteRow.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboWhFrom.SelectedValue.ToString() != "")
            {
                cleanGrids(grdBatch);
                cleanGrids(grdItem);

                List<Articulo> articulos = data.ConsultarArticulosXAlmacen(new Almacen() { WhsCode = cboWhFrom.SelectedValue.ToString() }, true);

                grdItem.Columns.Add("ItemCode", "Código");
                grdItem.Columns.Add("ItemName", "Nombre");
                grdItem.Columns.Add("AvgPrice", "Costo");

                grdItem.Columns[0].DataPropertyName = "ItemCode";
                grdItem.Columns[1].DataPropertyName = "ItemName";
                grdItem.Columns[2].DataPropertyName = "AvgPrice";

                grdItem.DataSource = articulos.OrderBy(x => x.ItemName).ToList();
            }
        }        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            double qty = 0;

            if (string.IsNullOrEmpty(cboWhTo.SelectedValue.ToString()) || string.IsNullOrEmpty(txtRealBatch.Text) || string.IsNullOrEmpty(txtQty.Text) || string.IsNullOrEmpty(txtIq.Text))
            {
                statusLabel.Text = "Todos los campos son obligatorios";
                return;
            }

            if (!double.TryParse(txtQty.Text, out qty))
            {
                statusLabel.Text = "La cantidad no es numerica";
                return;
            }


            btnSearch.Enabled = false;
            cboWhFrom.Enabled = false;

            LoteoArticuloLinea nuevaLinea = new LoteoArticuloLinea()
            {
                toWhsCode = cboWhTo.SelectedValue.ToString(),
                itemCode = grdItem.SelectedRows[0].Cells["ItemCode"].Value.ToString(),
                originalBatchNumber = grdBatch.SelectedRows[0].Cells["DistNumber"].Value.ToString(),
                finallyBatchNumber = txtRealBatch.Text,
                iqNumber = txtIq.Text,
                quantity = double.Parse(txtQty.Text),
                AvgPrice = double.Parse(grdItem.SelectedRows[0].Cells["AvgPrice"].Value.ToString())                
            };

            if (grdBatch.SelectedRows[0].Cells["ExpDate"].Value != null)
                nuevaLinea.ExpDate = grdBatch.SelectedRows[0].Cells["ExpDate"].Value.ToString();

            if (grdBatch.SelectedRows[0].Cells["MnfDate"].Value != null)
                nuevaLinea.MnfDate = grdBatch.SelectedRows[0].Cells["MnfDate"].Value.ToString();

            loteoArticulo.lineas.Add(nuevaLinea);

            cleanGrids(grdTrn);

            grdTrn.Columns.Add("itemCode", "Codigo artículo");
            grdTrn.Columns.Add("originalBatchNumber", "Lote original");
            grdTrn.Columns.Add("finallyBatchNumber", "Nuevo lote");
            grdTrn.Columns.Add("toWhsCode", "Bodega destino");
            grdTrn.Columns.Add("quantity", "Cantidad");
            grdTrn.Columns.Add("iqNumber", "IQ");
            grdTrn.Columns.Add("ExpDate", "Expira");
            grdTrn.Columns.Add("MfnDate", "Fabricado");
            grdTrn.Columns.Add("AvgPrice", "Costo");
            //grdTrn.Columns.Add("comments", "Observaciones");

            grdTrn.Columns[0].DataPropertyName = "itemCode";
            grdTrn.Columns[1].DataPropertyName = "originalBatchNumber";
            grdTrn.Columns[2].DataPropertyName = "finallyBatchNumber";
            grdTrn.Columns[3].DataPropertyName = "toWhsCode";
            grdTrn.Columns[4].DataPropertyName = "quantity";
            grdTrn.Columns[5].DataPropertyName = "iqNumber";
            grdTrn.Columns[6].DataPropertyName = "ExpDate";
            grdTrn.Columns[7].DataPropertyName = "MfnDate";
            grdTrn.Columns[8].DataPropertyName = "AvgPrice";
            //grdTrn.Columns[9].DataPropertyName = "comments";

            grdTrn.DataSource = loteoArticulo.lineas;

            cboWhTo.SelectedIndex = 0;
            
            txtIq.Text = "";
            txtQty.Text = "";
            txtRealBatch.Text = "";
            txtComments.Text = "";
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (loteoArticulo.lineas.Count() == 0)
            {
                statusLabel.Text = "No hay registros a procesar";
                return;
            }

            loteoArticulo.FromWhsCode = cboWhFrom.SelectedValue.ToString();
            loteoArticulo.DocDate = DateTime.Now;
            loteoArticulo.TaxDate = DateTime.Now;
            loteoArticulo.Comments = txtComments.Text;

            try
            {
                statusLabel.Text = data.CrearReloteoArticulo(loteoArticulo);
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
            finally
            {
                cleanForm();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cleanForm();
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (grdTrn.SelectedRows.Count > 0)
            {
                int rowNum = 0;

                foreach (DataGridViewRow item in grdTrn.Rows)
                {
                    if (item.Selected)
                    {
                        LoteoArticuloLinea linea = loteoArticulo.lineas[rowNum];
                        loteoArticulo.lineas.Remove(linea);

                        cleanGrids(grdTrn);

                        grdTrn.Columns.Add("itemCode", "Codigo artículo");
                        grdTrn.Columns.Add("originalBatchNumber", "Lote original");
                        grdTrn.Columns.Add("finallyBatchNumber", "Nuevo lote");
                        grdTrn.Columns.Add("toWhsCode", "Bodega destino");
                        grdTrn.Columns.Add("quantity", "Cantidad");
                        grdTrn.Columns.Add("iqNumber", "IQ");

                        grdTrn.Columns[0].DataPropertyName = "itemCode";
                        grdTrn.Columns[1].DataPropertyName = "originalBatchNumber";
                        grdTrn.Columns[2].DataPropertyName = "finallyBatchNumber";
                        grdTrn.Columns[3].DataPropertyName = "toWhsCode";
                        grdTrn.Columns[4].DataPropertyName = "quantity";
                        grdTrn.Columns[5].DataPropertyName = "iqNumber";

                        grdTrn.DataSource = loteoArticulo.lineas;

                        btnDeleteRow.Enabled = false;
                    }

                    rowNum++;
                }
            }
        }

        private void cleanGrids(DataGridView grid)
        {
            grid.DataSource = null;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();


        }

        private void cleanForm()
        {

            cleanGrids(grdItem);
            cleanGrids(grdBatch);
            cleanGrids(grdTrn);

            cboWhFrom.SelectedIndex = 0;
            cboWhTo.SelectedIndex = 0;

            cboWhFrom.Enabled = true;
            btnSearch.Enabled = true;

            txtIq.Text = "";
            txtQty.Text = "";
            txtRealBatch.Text = "";
            txtComments.Text = "";

            loteoArticulo = new LoteoArticulo();
        }
    }
}

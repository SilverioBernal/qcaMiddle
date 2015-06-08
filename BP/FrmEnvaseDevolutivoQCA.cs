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
    public partial class FrmEnvaseDevolutivoQCA : Form
    {
        Remision remision = null;
        LineasRemision articulo = null;
        DateTime inicioEnvaseDevolutivo = ClsEnvaseDevolutivo.GetFechaCorte();
        Accion action = Accion.limpiar;
        //int objectType = 0;

        enum Accion { entradaManual, entraReacondicionamiento, saleReacndicionamiento, limpiar }

        public FrmEnvaseDevolutivoQCA()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmEnvaseDevolutivoQCA_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            escribirTolStrip("");

            logicaControles(Accion.limpiar);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarGrids(grdItems);
            limpiarGrids(grdFacturas);
            limpiarGrids(grdEntradas);
            limpiarGrids(grdNotas);
            getRemision();
        }

        private void grdItems_SelectionChanged(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                DataGridViewRow itemSelectedRow = new DataGridViewRow();
                itemSelectedRow = grdItems.SelectedRows[0];
                int linea = int.Parse(itemSelectedRow.Cells["lineNum"].Value.ToString());

                articulo = remision.Lineas.Where(x => x.lineNum.Equals(linea)).First();

                txtCantRemisionada.Text = articulo.quantity.ToString();
                txtCantDevuelta.Text = obtenerCantidadRetornada(articulo).ToString();
                txtCantEnReacondicionamiento.Text = (articulo.lineasEntradaManual.Where(x => x.objType.Equals(91)).Sum(x => x.quantity)).ToString();
                txtCantidadReacondicionado.Text = (articulo.lineasEntradaManual.Where(x => x.objType.Equals(92)).Sum(x => x.quantity)).ToString();
                txtCantBaja.Text = (articulo.lineasEntradaManual.Where(x => x.objType.Equals(92)).Sum(x => x.itemsBaja)).ToString();
                txtItemCode.Text = string.Format("{0}-{1}", articulo.itemCode, articulo.itemName);

                bindGridFacturas();
                bindGridEntregas();
            }
        }

        private void grdFacturas_SelectionChanged(object sender, EventArgs e)
        {
            if (grdFacturas.SelectedRows.Count > 0)
            {
                DataGridViewRow facturaSelectedRow = new DataGridViewRow();
                facturaSelectedRow = grdFacturas.SelectedRows[0];

                int factura = int.Parse(facturaSelectedRow.Cells["docNum"].Value.ToString());
                int linea = int.Parse(facturaSelectedRow.Cells["lineNum"].Value.ToString());

                LineasFactura lineasFactura = articulo.lineasFactura.Where(x => x.lineNum.Equals(linea) && x.docNum.Equals(factura)).First();

                bindGridNotasCredito(lineasFactura);
            }
        }

        private void grdEntradas_SelectionChanged(object sender, EventArgs e)
        {
            if (grdEntradas.SelectedRows.Count > 0)
            {
                DataGridViewRow entradaSelectedRow = new DataGridViewRow();
                entradaSelectedRow = grdEntradas.SelectedRows[0];

                //string code = entradaSelectedRow.Cells["code"].Value.ToString();

                int retornadas = 0, reacondicionando = 0, reacondicionadas = 0;

                retornadas = articulo.lineasEntradaManual.Where(x => x.objType.Equals(90)).Sum(x => x.quantity);
                reacondicionando = articulo.lineasEntradaManual.Where(x => x.objType.Equals(91)).Sum(x => x.quantity);
                reacondicionadas = articulo.lineasEntradaManual.Where(x => x.objType.Equals(92)).Sum(x => x.quantity);

                if (articulo.quantity < retornadas)
                    btnEntradaManual.Enabled = true;

                if (reacondicionando < retornadas)
                    btnReacindicionaIn.Enabled = true;

                if (reacondicionadas < reacondicionando)
                    btnReacindicionaOut.Enabled = true;
            }
        }

        private void btnEntradaManual_Click(object sender, EventArgs e)
        {
            try
            {
                if (remision != null)
                {
                    logicaControles(Accion.entradaManual);
                    action = Accion.entradaManual;
                }
                else
                    throw new Exception("No ha buscado una remisión");
            }
            catch (Exception ex)
            {
                escribirTolStrip(ex.Message);
            }
        }

        private void btnReacindicionaIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (remision != null)
                {
                    logicaControles(Accion.entraReacondicionamiento);
                    action = Accion.entraReacondicionamiento;
                }
                else
                    throw new Exception("No ha buscado una remisión");
            }
            catch (Exception ex)
            {
                escribirTolStrip(ex.Message);
            }
        }

        private void btnReacindicionaOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (remision != null)
                {
                    logicaControles(Accion.saleReacndicionamiento);
                    action = Accion.saleReacndicionamiento;
                }
                else
                    throw new Exception("No ha buscado una remisión");
            }
            catch (Exception ex)
            {
                escribirTolStrip(ex.Message);
            }

        }

        private void getRemision()
        {
            try
            {
                escribirTolStrip("");

                if (!string.IsNullOrEmpty(txtRemision.Text))
                {
                    int numRemision = 0;
                    if (!int.TryParse(txtRemision.Text, out numRemision))
                        throw new Exception("Ingrese un número válido de remisión");

                    remision = ClsEnvaseDevolutivo.GetRemision(int.Parse(txtRemision.Text));

                    if (remision.docDate < inicioEnvaseDevolutivo)
                        throw new Exception(string.Format("Fecha de remisión {0} inferior a la fecha de corte {1}", remision.docDate.ToString("yyyy-MM.dd"), inicioEnvaseDevolutivo.ToString("yyyy-MM.dd")));

                    bindGridRemision();
                }
            }
            catch (Exception ex)
            {
                escribirTolStrip(ex.Message);
            }
        }

        private void bindGridRemision()
        {
            limpiarGrids(grdItems);

            grdItems.Columns.Add("lineNum", "Linea Entrega");
            grdItems.Columns.Add("itemCode", "Cod. Artículo");
            grdItems.Columns.Add("itemName", "Artículo");
            grdItems.Columns.Add("quantity", "Env. despachados");

            grdItems.Columns[0].DataPropertyName = "lineNum";
            grdItems.Columns[1].DataPropertyName = "itemCode";
            grdItems.Columns[2].DataPropertyName = "itemName";
            grdItems.Columns[3].DataPropertyName = "quantity";

            grdItems.DataSource = remision.Lineas;
        }

        private void bindGridFacturas()
        {
            limpiarGrids(grdFacturas);

            grdFacturas.Columns.Add("docNum", "No. factura");
            grdFacturas.Columns.Add("lineNum", "Linea factura");
            grdFacturas.Columns.Add("quantity", "Cantidad facturada");

            grdFacturas.Columns[0].DataPropertyName = "docNum";
            grdFacturas.Columns[1].DataPropertyName = "lineNum";
            grdFacturas.Columns[2].DataPropertyName = "quantity";

            grdFacturas.DataSource = articulo.lineasFactura;
        }

        private void bindGridEntregas()
        {
            limpiarGrids(grdEntradas);

            grdEntradas.Columns.Add("code", "code");
            grdEntradas.Columns.Add("objTypeName", "Tipo transacción");
            grdEntradas.Columns.Add("quantity", "Cantidad");

            grdEntradas.Columns[0].DataPropertyName = "code";
            grdEntradas.Columns[1].DataPropertyName = "objTypeName";
            grdEntradas.Columns[2].DataPropertyName = "quantity";

            grdEntradas.DataSource = articulo.lineasEntradaManual;
        }

        private void bindGridNotasCredito(LineasFactura factura)
        {
            limpiarGrids(grdNotas);

            grdNotas.Columns.Add("docNum", "No. Nota");
            grdNotas.Columns.Add("lineNum", "Linea Nota");
            grdNotas.Columns.Add("quantity", "Cantidad retornada");

            grdNotas.Columns[0].DataPropertyName = "docNum";
            grdNotas.Columns[1].DataPropertyName = "lineNum";
            grdNotas.Columns[2].DataPropertyName = "quantity";

            grdNotas.DataSource = factura.lineasNotaCredito;
        }

        private void limpiarGrids(DataGridView grid)
        {
            grid.DataSource = null;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();


        }

        private void escribirTolStrip(string texto)
        {
            toolStripStatusLabel1.Text = texto;
        }

        private int obtenerCantidadRetornada(LineasRemision articulo)
        {
            int entregasManuales = articulo.lineasEntradaManual.Where(x => x.objType.Equals(90)).Sum(x => x.quantity);
            int devueltoNotaCredito = 0;

            foreach (LineasFactura item in articulo.lineasFactura)
            {
                foreach (LineasNotaCredito itemNc in item.lineasNotaCredito)
                    devueltoNotaCredito += itemNc.quantity;
            }

            return entregasManuales + devueltoNotaCredito;
        }

        private void logicaControles(Accion accion)
        {
            txtCantidad.Text = "";
            txtReciboCliente.Text = "";
            txtReciboProveedor.Text = "";
            txtBaja.Text = "";
            txtObservaciones.Text = "";
            txtCosto.Text = "";

            //cboProveedor.Items.Clear();
            //cboProveedor.DataSource = null;


            switch (accion)
            {
                case Accion.entradaManual:
                    txtRemision.Enabled = false;
                    txtCantidad.Enabled = true;
                    txtReciboCliente.Enabled = true;
                    dpFechaReciboCliente.Enabled = true;

                    cboProveedor.Enabled = false;
                    txtReciboProveedor.Enabled = false;
                    dpFechaReciboProveedor.Enabled = false;
                    txtBaja.Enabled = false;
                    txtObservaciones.Enabled = false;
                    txtCosto.Enabled = false;

                    btnBuscar.Enabled = false;
                    btnEntradaManual.Enabled = false;
                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;
                    break;
                case Accion.entraReacondicionamiento:
                    txtRemision.Enabled = false;
                    txtCantidad.Enabled = true;
                    txtReciboCliente.Enabled = false;
                    dpFechaReciboCliente.Enabled = false;

                    cboProveedor.Enabled = true;
                    txtReciboProveedor.Enabled = false;
                    dpFechaReciboProveedor.Enabled = false;
                    txtBaja.Enabled = false;
                    txtObservaciones.Enabled = false;
                    txtCosto.Enabled = false;

                    btnBuscar.Enabled = false;
                    btnEntradaManual.Enabled = false;
                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;

                    List<Proveedor> proveedores = ClsEnvaseDevolutivo.GetProveedores();

                    cboProveedor.ValueMember = "CardCode";
                    cboProveedor.DisplayMember = "CardName";
                    cboProveedor.DataSource = proveedores;

                    break;
                case Accion.saleReacndicionamiento:
                    txtRemision.Enabled = false;
                    txtCantidad.Enabled = true;
                    txtReciboCliente.Enabled = false;
                    dpFechaReciboCliente.Enabled = false;

                    cboProveedor.Enabled = true;
                    txtReciboProveedor.Enabled = true;
                    dpFechaReciboProveedor.Enabled = true;
                    txtBaja.Enabled = true;
                    txtObservaciones.Enabled = true;
                    txtCosto.Enabled = true;

                    btnBuscar.Enabled = false;
                    btnEntradaManual.Enabled = false;
                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;
                    break;
                case Accion.limpiar:
                    txtRemision.Text = "";
                    txtRemision.Enabled = true;
                    txtCantidad.Enabled = false;
                    txtReciboCliente.Enabled = false;
                    dpFechaReciboCliente.Enabled = false;

                    cboProveedor.Enabled = false;
                    txtReciboProveedor.Enabled = false;
                    dpFechaReciboProveedor.Enabled = false;
                    txtBaja.Enabled = false;
                    txtObservaciones.Enabled = false;
                    txtCosto.Enabled = false;

                    btnBuscar.Enabled = true;
                    btnEntradaManual.Enabled = true;
                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;

                    limpiarGrids(grdNotas);
                    limpiarGrids(grdFacturas);
                    limpiarGrids(grdEntradas);
                    limpiarGrids(grdItems);

                    break;
                default:
                    break;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            LineasEntradaManual linea = new LineasEntradaManual()
                    {
                        quantity = int.Parse(txtCantidad.Text),
                        baseEntry = remision.docEntry,
                        baseLine = articulo.lineNum
                    };
            try
            {
                switch (action)
                {
                    case Accion.entradaManual:
                        if (int.Parse(txtCantidad.Text) > (int.Parse(txtCantRemisionada.Text) - int.Parse(txtCantDevuelta.Text)))
                            throw new Exception("Cantidad excede el valor permitido");

                        linea.objType = 90;
                        linea.fechaReciboCliente = dpFechaReciboCliente.Value;
                        linea.numReciboCliente = txtReciboCliente.Text;
                        linea.costoReacondic = 0;
                        linea.itemsBaja = 0;
                        break;
                    case Accion.entraReacondicionamiento:
                        if (int.Parse(txtCantidad.Text) > (int.Parse(txtCantDevuelta.Text) - int.Parse(txtCantEnReacondicionamiento.Text)))
                            throw new Exception("Cantidad excede el valor permitido");

                        linea.objType = 91;
                        linea.cardCode = cboProveedor.SelectedValue.ToString();
                        linea.fechaReciboProveedor  =new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
                        linea.costoReacondic = 0;
                        linea.itemsBaja = 0;
                        break;
                    case Accion.saleReacndicionamiento:
                        if (int.Parse(txtCantidad.Text) > (int.Parse(txtCantEnReacondicionamiento.Text) - (int.Parse(txtCantidadReacondicionado.Text) + int.Parse(txtCantBaja.Text))))
                            throw new Exception("Cantidad excede el valor permitido");

                        linea.objType = 92;
                        linea.cardCode = cboProveedor.SelectedValue.ToString();
                        linea.numReciboProveedor = txtReciboProveedor.Text;
                        linea.fechaReciboProveedor = dpFechaReciboProveedor.Value;
                        linea.costoReacondic = string.IsNullOrEmpty(txtCosto.Text) ? 0 : int.Parse(txtCosto.Text);
                        linea.itemsBaja = string.IsNullOrEmpty(txtBaja.Text) ? 0 : int.Parse(txtBaja.Text);
                        linea.observaciones = txtObservaciones.Text;
                        break;
                    case Accion.limpiar:
                        escribirTolStrip("No hay nada para guardar");
                        break;
                    default:
                        break;
                }


                ClsEnvaseDevolutivo.SaveEntrada(linea);
                getRemision();
                action = Accion.limpiar;
            }
            catch (Exception ex)
            {
                escribirTolStrip(ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            logicaControles(Accion.limpiar);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }
    }
}

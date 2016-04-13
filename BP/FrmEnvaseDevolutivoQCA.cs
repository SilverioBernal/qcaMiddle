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
    public partial class FrmEnvaseDevolutivoQCA : Form
    {
        Remision remision = null;
        LineasRemision articulo = null;
        DateTime inicioEnvaseDevolutivo = ClsEnvaseDevolutivo.GetFechaCorte();
        Accion action = Accion.limpiar;
        //int objectType = 0;

        enum Accion { entradaManual, entraReacondicionamiento, saleReacndicionamiento, limpiar, esperando }

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
                logicaControles(Accion.entraReacondicionamiento);
                action = Accion.entraReacondicionamiento;
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
                logicaControles(Accion.saleReacndicionamiento);
                action = Accion.saleReacndicionamiento;                
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

                    logicaControles(Accion.esperando);
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
            //txtBaja.Text = "";
            txtObservaciones.Text = "";
            //txtCosto.Text = "";

            List<Proveedor> proveedores = new List<Proveedor>();
            List<Articulo> articulos = new List<Articulo>();
            List<Almacen> almacenes = new List<Almacen>();
            List<Almacen> almacenesDestino = new List<Almacen>();

            proveedores.Add(new Proveedor() { CardCode= string.Empty, CardName="Seleccione uno" });
            articulos.Add(new Articulo() { ItemCode = string.Empty, ItemName = "Seleccione uno" });
            almacenes.Add(new Almacen() { WhsCode = string.Empty, WhsName = "Seleccione uno" });
            
            proveedores.AddRange(ClsEnvaseDevolutivo.GetProveedores());
            articulos.AddRange(ClsEnvaseDevolutivo.GetItems("ME"));
            almacenes.AddRange(ClsEnvaseDevolutivo.GetAlmacenes());
            almacenesDestino.AddRange(almacenes.ToList()); 

            switch (accion)
            {
                case Accion.entradaManual:
                    txtRemision.Enabled = false;
                    txtCantidad.Enabled = true;
                    txtReciboCliente.Enabled = true;
                    dpFechaReciboCliente.Enabled = true;

                    cboProveedor.Enabled = false;
                    cboEnvase.Enabled = false;
                    cboBodegaOrigen.Enabled = false;
                    TxtCantidadProveedor.Enabled = false;
                    txtValor.Enabled = false;
                    txtReciboProveedor.Enabled = false;
                    cboBodegaDestino.Enabled = false;
                    txtObservaciones.Enabled = false;

                    btnBuscar.Enabled = false;
                    btnEntradaManual.Enabled = false;
                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;
                    break;
                case Accion.entraReacondicionamiento:
                    txtRemision.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtReciboCliente.Enabled = false;
                    dpFechaReciboCliente.Enabled = false;

                    cboProveedor.Enabled = true;
                    cboEnvase.Enabled = true;
                    cboBodegaOrigen.Enabled = true;
                    TxtCantidadProveedor.Enabled = true;
                    txtValor.Enabled = true;
                    txtReciboProveedor.Enabled = true;
                    cboBodegaDestino.Enabled = false;
                    txtObservaciones.Enabled = false;

                    btnBuscar.Enabled = false;
                    btnEntradaManual.Enabled = false;
                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;

                    
                    cboProveedor.ValueMember = "CardCode";
                    cboProveedor.DisplayMember = "CardName";
                    cboProveedor.DataSource = proveedores;

                    
                    cboEnvase.ValueMember = "ItemCode";
                    cboEnvase.DisplayMember = "ItemName";
                    cboEnvase.DataSource = articulos;

                    
                    cboBodegaOrigen.ValueMember = "WhsCode";
                    cboBodegaOrigen.DisplayMember = "WhsName";
                    cboBodegaOrigen.DataSource = almacenes;

                    break;
                case Accion.saleReacndicionamiento:
                    txtRemision.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtReciboCliente.Enabled = false;
                    dpFechaReciboCliente.Enabled = false;

                    cboProveedor.Enabled = true;
                    cboEnvase.Enabled = true;
                    cboBodegaOrigen.Enabled = true;
                    TxtCantidadProveedor.Enabled = true;
                    txtValor.Enabled = false;
                    txtReciboProveedor.Enabled = false;
                    cboBodegaDestino.Enabled = true;
                    txtObservaciones.Enabled = true;

                    btnBuscar.Enabled = false;
                    btnEntradaManual.Enabled = false;
                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;

                    
                    cboProveedor.ValueMember = "CardCode";
                    cboProveedor.DisplayMember = "CardName";
                    cboProveedor.DataSource = proveedores;

                    
                    cboEnvase.ValueMember = "ItemCode";
                    cboEnvase.DisplayMember = "ItemName";
                    cboEnvase.DataSource = articulos;
                    
                    cboBodegaOrigen.ValueMember = "WhsCode";
                    cboBodegaOrigen.DisplayMember = "WhsName";
                    cboBodegaOrigen.DataSource = almacenes;

                    cboBodegaDestino.ValueMember = "WhsCode";
                    cboBodegaDestino.DisplayMember = "WhsName";
                    cboBodegaDestino.DataSource = almacenesDestino;

                    break;
                case Accion.limpiar:
                    txtRemision.Text = "";
                    txtCantidad.Text = "";
                    TxtCantidadProveedor.Text = "";
                    txtValor.Text = "";
                    txtReciboProveedor.Text = "";
                    txtObservaciones.Text = "";
                    txtItemCode.Text = "";
                    txtCantRemisionada.Text = "";
                    txtCantDevuelta.Text = "";
                    txtCantEnReacondicionamiento.Text = "";
                    txtCantidadReacondicionado.Text = "";
                    txtCantBaja.Text = "";
                    dpFechaReciboCliente.Value = DateTime.Now;

                    cboProveedor.DataSource = null;
                    cboEnvase.DataSource = null;
                    cboBodegaOrigen.DataSource = null;
                    cboBodegaDestino.DataSource = null;

                    cboProveedor.Items.Clear();
                    cboEnvase.Items.Clear();
                    cboBodegaOrigen.Items.Clear();
                    cboBodegaDestino.Items.Clear();                                       

                    txtRemision.Enabled = true;
                    txtCantidad.Enabled = false;
                    txtReciboCliente.Enabled = false;
                    dpFechaReciboCliente.Enabled = false;

                    cboProveedor.Enabled = false;
                    cboEnvase.Enabled = false;
                    cboBodegaOrigen.Enabled = false;
                    TxtCantidadProveedor.Enabled = false;
                    txtValor.Enabled = false;
                    txtReciboProveedor.Enabled = false;
                    cboBodegaDestino.Enabled = false;
                    txtObservaciones.Enabled = false;

                    btnBuscar.Enabled = true;
                    btnEntradaManual.Enabled = false;
                    btnReacindicionaIn.Enabled = true;
                    btnReacindicionaOut.Enabled = true;

                    limpiarGrids(grdNotas);
                    limpiarGrids(grdFacturas);
                    limpiarGrids(grdEntradas);
                    limpiarGrids(grdItems);
                    break;
                case Accion.esperando:
                    txtRemision.Enabled = false;
                    txtCantidad.Enabled = false;
                    txtReciboCliente.Enabled = false;
                    dpFechaReciboCliente.Enabled = false;

                    cboProveedor.Enabled = false;
                    cboEnvase.Enabled = false;
                    cboBodegaOrigen.Enabled = false;
                    TxtCantidadProveedor.Enabled = false;
                    txtValor.Enabled = false;
                    txtReciboProveedor.Enabled = false;
                    cboBodegaDestino.Enabled = false;
                    txtObservaciones.Enabled = false;

                    btnBuscar.Enabled = false;

                    if (txtCantRemisionada.Text != txtCantDevuelta.Text)
                        btnEntradaManual.Enabled = true;
                    else
                        btnEntradaManual.Enabled = false;

                    btnReacindicionaIn.Enabled = false;
                    btnReacindicionaOut.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (action)
                {
                    case Accion.entradaManual:
                        int cantidadDevuelta = 0;
                        if (!int.TryParse(txtCantidad.Text, out cantidadDevuelta))
                            throw new Exception("Ingrese una cantidad válida");

                        LineasEntradaManual linea = new LineasEntradaManual()
                        {
                            quantity = int.Parse(txtCantidad.Text),
                            baseEntry = remision.docEntry,
                            baseLine = articulo.lineNum
                        };

                        //if (int.Parse(txtCantidad.Text) > (int.Parse(txtCantRemisionada.Text) - int.Parse(txtCantDevuelta.Text)))
                        //    throw new Exception("Cantidad excede el valor permitido");

                        linea.objType = 90;
                        linea.fechaReciboCliente = dpFechaReciboCliente.Value;
                        linea.numReciboCliente = txtReciboCliente.Text;
                        linea.costoReacondic = 0;
                        linea.itemsBaja = 0;
                        ClsEnvaseDevolutivo.SaveEntrada(linea);
                        getRemision();
                        break;
                    case Accion.entraReacondicionamiento:
                        int cantidadReacondicionar = 0;
                        if (!int.TryParse(TxtCantidadProveedor.Text, out cantidadReacondicionar))
                            throw new Exception("Ingrese una cantidad válida");

                        int valorReacondicionar = 0;
                        if (!int.TryParse(txtValor.Text, out valorReacondicionar))
                            throw new Exception("Ingrese un valor válido");

                        DocumentoMktng doc = new DocumentoMktng()
                        {
                            CardCode = cboProveedor.SelectedValue.ToString(),
                            Comments = "Recibo Cliente No. " + txtReciboCliente.Text
                        };

                        doc.lineas.Add(new DocumentoLineas()
                        {
                            ItemCode = cboEnvase.SelectedValue.ToString(),
                            WhsCode = cboBodegaOrigen.SelectedValue.ToString(),
                            Quantity = double.Parse(TxtCantidadProveedor.Text),
                            Price = double.Parse(txtValor.Text),
                        });


                        int entrada = BusinessDocumento.CrearEntradaMercancia(doc);

                        if (entrada > 0)
                            escribirTolStrip("Se generó la entrada " + entrada.ToString());

                        break;
                    case Accion.saleReacndicionamiento:                        
                        int cantidadReacondicionada = 0;
                        if (!int.TryParse(TxtCantidadProveedor.Text, out cantidadReacondicionada))
                            throw new Exception("Ingrese una cantidad válida");

                        Transferencia tr = new Transferencia()
                        {
                            DocDate = DateTime.Now,
                            WhsCode = cboBodegaOrigen.SelectedValue.ToString(),
                            Comments = txtObservaciones.Text
                        };

                        tr.lineas.Add(new TransferenciaLinea()
                        {
                            ItemCode = cboEnvase.SelectedValue.ToString(),
                            Quantity = double.Parse(TxtCantidadProveedor.Text),
                            WhsCode = cboBodegaDestino.SelectedValue.ToString()
                        });

                        int transDoc = BusinessInventario.CrearTransferenciaInventario(tr);

                        if (transDoc > 0)
                            escribirTolStrip("Se generó la transferencia " + transDoc.ToString());

                        break;
                    case Accion.limpiar:
                        escribirTolStrip("No hay nada para guardar");
                        break;
                    default:
                        break;
                }

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

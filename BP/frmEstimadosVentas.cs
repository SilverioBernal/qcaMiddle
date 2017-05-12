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
    public partial class frmEstimadosVentas : Form
    {
        BP.AppCode.ClsEstimadosVentas oEstimadosVentas;

        AppData.DataSetEstimadosVentas dsEstimadosVentas;

        Dictionary<int, List<CityAndValue>> presupuestoPorCiudad = new Dictionary<int, List<CityAndValue>>();
        List<CityAndValue> ultimosValoresPorCiudadIngresados;

        double totalUltimosValoresPorCiudadIngresados;

        ArrayControlQCA m_popedContainerForButton;
        PoperContainer m_poperContainerForButton;

        public frmEstimadosVentas()
        {
            InitializeComponent();

            m_popedContainerForButton = new ArrayControlQCA();
            m_poperContainerForButton = new PoperContainer(m_popedContainerForButton);

            m_popedContainerForButton.btnOk.Click += new System.EventHandler(this.enviarCantidadesPorCiudad);

            lblPresupuestoActual.Text = "Presupuesto actual (" + DateTime.Now.Year.ToString() + ")";
            lblPresupuestoAnterior.Text = "Presupuesto anterior (" + DateTime.Now.AddYears(-1).Year.ToString() + ")";
            lblPresupuestoSiguiente.Text = "Presupuesto siguiente (" + DateTime.Now.AddYears(1).Year.ToString() + ")";
            this.WindowState = FormWindowState.Maximized;

            oEstimadosVentas = new BP.AppCode.ClsEstimadosVentas();
            dsEstimadosVentas = new BP.AppData.DataSetEstimadosVentas();

        }

        private void frmEstimadosVentas_Load(object sender, EventArgs e)
        {
            try
            {

                if (!oEstimadosVentas.ValidaFechaEstimados())
                {
                    try
                    {
                        MessageBox.Show("Periodo cerrado. no se pueden ingresar estimados en un periodo cerrado");
                        toolStrip1.Enabled = false;
                        panel1.Enabled = false;
                        panel2.Enabled = false;
                    }
                    catch (Exception)
                    { }
                }

                grdEstimadoLineas.DataSource = dsEstimadosVentas.Tables["Cabecera"];

                grdEstimadoLineas.Columns["codigoCliente"].HeaderText = "Código cliente";
                grdEstimadoLineas.Columns["nombreCliente"].HeaderText = "Nombre cliente";
                grdEstimadoLineas.Columns["idItem"].HeaderText = "Código artículo";
                grdEstimadoLineas.Columns["nombreItem"].HeaderText = "Nombre articulo";
                grdEstimadoLineas.Columns["enero"].HeaderText = "Enero";
                grdEstimadoLineas.Columns["febrero"].HeaderText = "Febrero";
                grdEstimadoLineas.Columns["marzo"].HeaderText = "Marzo";
                grdEstimadoLineas.Columns["abril"].HeaderText = "Abril";
                grdEstimadoLineas.Columns["mayo"].HeaderText = "Mayo";
                grdEstimadoLineas.Columns["junio"].HeaderText = "Junio";
                grdEstimadoLineas.Columns["julio"].HeaderText = "Julio";
                grdEstimadoLineas.Columns["agosto"].HeaderText = "Agosto";
                grdEstimadoLineas.Columns["septiembre"].HeaderText = "Septiembre";
                grdEstimadoLineas.Columns["octubre"].HeaderText = "Octubre";
                grdEstimadoLineas.Columns["noviembre"].HeaderText = "Noviembre";
                grdEstimadoLineas.Columns["diciembre"].HeaderText = "Diciembre";

                grdEstimadoLineas.Columns["codigoCliente"].Width = 80;
                grdEstimadoLineas.Columns["idItem"].Width = 90;
                grdEstimadoLineas.Columns["nombreItem"].Width = 170;
                grdEstimadoLineas.Columns["enero"].Width = 70;
                grdEstimadoLineas.Columns["febrero"].Width = 70;
                grdEstimadoLineas.Columns["marzo"].Width = 70;
                grdEstimadoLineas.Columns["abril"].Width = 70;
                grdEstimadoLineas.Columns["mayo"].Width = 70;
                grdEstimadoLineas.Columns["junio"].Width = 70;
                grdEstimadoLineas.Columns["julio"].Width = 70;
                grdEstimadoLineas.Columns["agosto"].Width = 70;
                grdEstimadoLineas.Columns["septiembre"].Width = 70;
                grdEstimadoLineas.Columns["octubre"].Width = 70;
                grdEstimadoLineas.Columns["noviembre"].Width = 70;
                grdEstimadoLineas.Columns["diciembre"].Width = 70;

                grdEstimadoLineas.Columns["ingenieroVtas"].Visible = false;
                grdEstimadoLineas.Columns["codigoCliente"].Visible = false;

                tabControl1.SelectedTab = tabPage1;

                if (oEstimadosVentas.IngenieroVentasValido(loging.usrCode))
                {
                    //if (!oEstimadosVentas.FechaPresupuestoValida())
                    //{
                    //    cboCustomers.Enabled = false;
                    //}

                    llenaCombosCiudades();
                    deshabilitarMesesNoValidos();
                }
                else
                {
                    MessageBox.Show("Este usuario no es un empleado de ventas");
                    this.DestroyHandle();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cbxSalesName_DropDown(object sender, EventArgs e)
        {
            llenaComboIngenierosVentas();
        }

        private void cbxSalesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaCombosClientes();
        }

        private void cboClientes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (cboClientes.SelectedValue != null && !cboClientes.SelectedValue.ToString().Contains("System.Data"))
                {
                    txtCodigoCliente.Text = cboClientes.SelectedValue.ToString();
                }

                try
                {
                    if (presupuestoPorCiudad != null && presupuestoPorCiudad.Count > 0)
                    {
                        presupuestoPorCiudad.Clear();
                        for (int m = 0; m < 12; m++) { dataGridView1C.Rows[0].Cells[m].Value = null; }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Clear:" + ex.Message, "Mensaje de Sistema"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboProductos_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (cboProductos.SelectedValue != null && !cboProductos.SelectedValue.ToString().Contains("System.Data"))
                {
                    txtItemCode.Text = cboProductos.SelectedValue.ToString();
                }

                try
                {
                    if (presupuestoPorCiudad != null && presupuestoPorCiudad.Count > 0)
                    {
                        presupuestoPorCiudad.Clear();
                        for (int m = 0; m < 12; m++) { dataGridView1C.Rows[0].Cells[m].Value = null; }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Clear:" + ex.Message, "Mensaje de Sistema"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkMultiplesLineas_CheckedChanged(object sender, EventArgs e)
        {
            llenaComboIngenierosVentas();
        }

        private void dataGridView1C_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (oEstimadosVentas.ValidaMes(e.ColumnIndex + 1))
                {
                    m_popedContainerForButton.resetControls();
                    m_poperContainerForButton.Show(dataGridView1C, e.ColumnIndex);
                    m_popedContainerForButton.Mes = e.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                if (presupuestoPorCiudad != null && presupuestoPorCiudad.Count > 0)
                {
                    presupuestoPorCiudad.Clear();
                    for (int m = 0; m < 12; m++) { dataGridView1C.Rows[0].Cells[m].Value = null; }
                }
            }
            catch (Exception ex) { MessageBox.Show("Clear:" + ex.Message, "Mensaje de Sistema"); }
        }

        private void btnCopiarRegistro_Click(object sender, EventArgs e)
        {
            copiarUltimoRegistroIngresado();
        }

        private void btnAgregarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                int ingenieroVtas = int.Parse(cbxSalesName.SelectedValue.ToString());

                string idCliente = cboClientes.SelectedValue.ToString();
                string nombreCliente = cboClientes.Text;
                string idItem = cboProductos.SelectedValue.ToString();
                string desItem = cboProductos.Text;

                DataGridViewRow datosPorItem = dataGridView1C.Rows[0];

                Double cantidad = 0;
                foreach (DataGridViewCell celdaMes in datosPorItem.Cells)
                {
                    double valorMes = 0;
                    if (celdaMes.Value != null) { valorMes = Double.Parse(celdaMes.Value.ToString()); }
                    cantidad = cantidad + valorMes;
                }

                if (cantidad == 0) { throw new Exception("No puede estimar productos con ninguna cantidad en el año"); }

                if (!buscaRegistroEnDataSet(ingenieroVtas)) { registrarIngenieroPresupuesto(ingenieroVtas); }

                if (!buscaRegistroEnDataSet(ingenieroVtas, idCliente, idItem))
                {
                    registrarPresupuesto(ingenieroVtas, idCliente, nombreCliente, idItem, desItem, datosPorItem);
                    calculaTotalEstimado();
                }
                else
                {
                    if (MessageBox.Show("El registro para el Ingeniero-Cliente-Producto seleccionados ya existe desea reemplazarlo?", "Modificación de presupuesto", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        eliminarPresupuesto(ingenieroVtas, idCliente, idItem);
                        registrarPresupuesto(ingenieroVtas, idCliente, nombreCliente, idItem, desItem, datosPorItem);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdEstimadoLineas.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow fila in grdEstimadoLineas.SelectedRows)
                    {
                        int ingenieroVtas = int.Parse(fila.Cells["ingenieroVtas"].Value.ToString());
                        string idCliente = fila.Cells["codigoCliente"].Value.ToString();
                        string idItem = fila.Cells["idItem"].Value.ToString();

                        eliminarPresupuesto(ingenieroVtas, idCliente, idItem);

                    }
                    DataGridViewSelectedRowCollection cabecerasSeleccionadas = grdEstimadoLineas.SelectedRows;
                }
                else
                {
                    throw new Exception("Debe seleccionar al menos un registro");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btGuardarDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow drIngeniero in dsEstimadosVentas.Tables["Ingeniero"].Rows)
                {
                    int ingenieroVentas = (int)drIngeniero["ingenieroVtas"];

                    foreach (DataRow drCabecera in dsEstimadosVentas.Tables["Cabecera"].Rows)
                    {
                        if (ingenieroVentas == (int)drIngeniero["ingenieroVtas"])
                        {
                            string idCliente = drCabecera["codigoCliente"].ToString();
                            string idItem = drCabecera["idItem"].ToString();
                            string desItem = drCabecera["nombreItem"].ToString();

                            foreach (DataRow drPresupuestoItem in dsEstimadosVentas.Tables["LineasPorItem"].Rows)
                            {
                                if (ingenieroVentas == (int)drPresupuestoItem["ingenieroVtas"]
                                    && idCliente == drPresupuestoItem["codigoCliente"].ToString()
                                    && idItem == drPresupuestoItem["idItem"].ToString())
                                {
                                    int año;
                                    int mes = (int)drPresupuestoItem["mesPresupuestado"];
                                    double cantidad = (double)drPresupuestoItem["pesoKgEstimado"];

                                    if (mes <= DateTime.Now.Month)
                                    {
                                        año = DateTime.Now.Year + 1;

                                    }
                                    else
                                    {
                                        año = DateTime.Now.Year;
                                    }

                                    if (!oEstimadosVentas.existePresupuesto(año, ingenieroVentas))
                                    {
                                        if (!oEstimadosVentas.guardarPresupuesto(año, ingenieroVentas)) { throw new Exception(oEstimadosVentas.Mensaje); }
                                    }
                                    if (cantidad != 0)
                                    {
                                        int numeroDocumentoCabecera = oEstimadosVentas.ObtenerNumeroPresupuesto(año, ingenieroVentas);

                                        if (!oEstimadosVentas.guardarPresupuesto(numeroDocumentoCabecera, idCliente, mes, idItem, cantidad, desItem))
                                        {
                                            throw new Exception(oEstimadosVentas.Mensaje);
                                        }
                                        else
                                        {
                                            int numeroRregistroArticulo = oEstimadosVentas.ObtenerNumeroPresupuesto(numeroDocumentoCabecera, idCliente, mes, idItem);

                                            string cSql = "IngenieroVtas = " + ingenieroVentas.ToString() + " and codigoCliente = '" + idCliente + "' and idItem = '" + idItem +
                                                "' and mesPresupuestado = " + mes.ToString();
                                            DataRow[] PresupuestoCiudad = dsEstimadosVentas.Tables["LineasPorCiudad"].Select(cSql);

                                            foreach (DataRow drCiudades in PresupuestoCiudad)
                                            {
                                                if (ingenieroVentas == (int)drCiudades["ingenieroVtas"]
                                                    && idCliente == drCiudades["codigoCliente"].ToString()
                                                    && idItem == drCiudades["idItem"].ToString()
                                                    && mes == (int)drCiudades["mesPresupuestado"])
                                                {
                                                    string ciudad = drCiudades["Ciudad"].ToString();
                                                    double cantidadCiudad = (double)drCiudades["Valor"];

                                                    if (!oEstimadosVentas.guardarPresupuesto(numeroDocumentoCabecera, numeroRregistroArticulo, mes, ciudad, cantidadCiudad))
                                                    {
                                                        throw new Exception(oEstimadosVentas.Mensaje);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Registros Guardados con exito");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            DataSet dsEstadistica = oEstimadosVentas.ObtenerEstadistica(int.Parse(cbxSalesName.SelectedValue.ToString()), cboClientes.SelectedValue.ToString(), txtItemCode.Text);

            if (dsEstadistica.Tables.Count > 0)
            {
                grdEstadisticaActual.DataSource = dsEstadistica.Tables[0];
                grdEstadisticaAnterior.DataSource = dsEstadistica.Tables[1];
                grdEstadisticaSiguiente.DataSource = dsEstadistica.Tables[2];

                tabControl1.SelectedTab = tabPage2;
            }
        }

        /*Metodos de apoyo*/

        /// <summary>
        /// llena el combo de ingenieros
        /// </summary>
        private void llenaComboIngenierosVentas()
        {
            DataSet dsEmpleadosVentas = oEstimadosVentas.ObtenerIngenierosVentas(chkMultiplesLineas.Checked);
            cbxSalesName.DataSource = dsEmpleadosVentas.Tables[0].DefaultView;
            cbxSalesName.ValueMember = "SlpCode";
            cbxSalesName.DisplayMember = "SlpName";
        }

        /// <summary>
        /// llena el combo de clientes
        /// </summary>
        private void llenaCombosClientes()
        {
            DataSet dsClientes = new DataSet();
            if (cbxSalesName.SelectedValue.ToString() != "System.Data.DataRowView" && cbxSalesName.SelectedValue.ToString() != oEstimadosVentas.CodigoIngenieroVentas)
            {
                dsClientes = oEstimadosVentas.ObtenerClientesIngenieroVentas(cbxSalesName.SelectedValue.ToString());
            }
            else
            {
                dsClientes = oEstimadosVentas.ObtenerClientesIngenieroVentas();
            }

            cboClientes.DataSource = dsClientes.Tables[0];
            cboClientes.DisplayMember = "cardname";
            cboClientes.ValueMember = "cardcode";

            cboClientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboClientes.Text = "";
        }

        /// <summary>
        /// llena el combo de productos
        /// </summary>
        private void llenaComboProductos()
        {
            DataSet dsProductos = new DataSet();

            if (rbCatalogoCliente.Checked)
            {
                dsProductos = oEstimadosVentas.ObtenerProductos(BP.AppCode.ClsEstimadosVentas.OrigenProductos.catalogoClientes, cboClientes.SelectedValue.ToString());
            }
            else
            {
                dsProductos = oEstimadosVentas.ObtenerProductos(BP.AppCode.ClsEstimadosVentas.OrigenProductos.maestroArticulos, "");
            }

            cboProductos.DataSource = dsProductos.Tables[0];
            cboProductos.ValueMember = "itemName";
            cboProductos.ValueMember = "itemCode";

            cboProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProductos.Text = "";
        }

        /// <summary>
        /// llena los combos de ciudades del control personalizado
        /// </summary>
        private void llenaCombosCiudades()
        {
            DataSet dsSucursales = oEstimadosVentas.ObtenerSucursales();
            m_popedContainerForButton.CbSource = dsSucursales.Tables[0];
        }

        /// <summary>
        /// Inhabilita las celdas del grid principal para que no se puedan ingresar datos en meses no validos
        /// </summary>
        private void deshabilitarMesesNoValidos()
        {
            for (int i = 0; i <= 11; i++)
            {
                if (!oEstimadosVentas.ValidaMes(i + 1))
                {
                    System.Windows.Forms.DataGridViewCellStyle estiloCelda = new System.Windows.Forms.DataGridViewCellStyle();
                    estiloCelda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                    estiloCelda.ForeColor = System.Drawing.Color.Black;
                    dataGridView1C.Rows[0].Cells[i].Style = estiloCelda;
                    grdTotales.Rows[0].Cells[i].Style = estiloCelda;
                }
            }
        }

        /// <summary>
        /// envia los datos digitados por ciudad al grid principal
        /// </summary>
        private void enviarCantidadesPorCiudad(object sender, EventArgs e) //buttonListo_Click
        {
            if (presupuestoPorCiudad.ContainsKey(m_popedContainerForButton.Mes))
            {
                presupuestoPorCiudad.Remove(m_popedContainerForButton.Mes);
                presupuestoPorCiudad.Add(m_popedContainerForButton.Mes, m_popedContainerForButton.CiudadValor);
            }
            else
            {
                presupuestoPorCiudad.Add(m_popedContainerForButton.Mes, m_popedContainerForButton.CiudadValor);
            }

            dataGridView1C.Rows[0].Cells[m_popedContainerForButton.Mes].Value = m_popedContainerForButton.Total.ToString();

            ultimosValoresPorCiudadIngresados = m_popedContainerForButton.CiudadValor;
            totalUltimosValoresPorCiudadIngresados = m_popedContainerForButton.Total;
            m_popedContainerForButton.Datos = null;
            m_popedContainerForButton.Total = 0;
            m_poperContainerForButton.Close();
        }

        /// <summary>
        /// Verifica si un ingeniero existe en el dataset
        /// </summary>
        private bool buscaRegistroEnDataSet(int ingenieroVtas)
        {
            bool res = false;

            try
            {
                string cSql = "IngenieroVtas = " + ingenieroVtas.ToString();
                DataRow[] ingenieros = dsEstimadosVentas.Tables["Ingeniero"].Select(cSql);

                if (ingenieros.Count() > 0) res = true;
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        /// <summary>
        /// Verifica si un registro de cabecera ya existe en el dataset
        /// </summary>
        private bool buscaRegistroEnDataSet(int ingenieroVtas, string idCliente, string idItem)
        {
            bool res = false;

            try
            {
                string cSql = "IngenieroVtas = " + ingenieroVtas.ToString() + " and codigoCliente = '" + idCliente + "' and idItem = '" + idItem + "'";
                DataRow[] cabecera = dsEstimadosVentas.Tables["Cabecera"].Select(cSql);

                if (cabecera.Count() > 0) res = true;
            }
            catch (Exception ex) { }

            return res;
        }

        /// <summary>
        /// Crea el registro del ingeniero de ventas en el dataset
        /// </summary>
        private void registrarIngenieroPresupuesto(int ingenieroVtas)
        {
            try
            {
                BP.AppData.DataSetEstimadosVentas.IngenieroRow nuevoRegistroIngeniero = dsEstimadosVentas.Ingeniero.NewIngenieroRow();
                nuevoRegistroIngeniero.IngenieroVtas = ingenieroVtas;
                dsEstimadosVentas.Ingeniero.Rows.Add(nuevoRegistroIngeniero);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Crea la cabecera del registro de presupuesto
        /// </summary>
        private void registrarPresupuesto(int ingenieroVtas, string idCliente, string nombreCliente, string idItem, string desItem, DataGridViewRow datosPorItem)
        {
            try
            {
                BP.AppData.DataSetEstimadosVentas.CabeceraRow nuevoRegistroCabecera = dsEstimadosVentas.Cabecera.NewCabeceraRow();

                nuevoRegistroCabecera.ingenieroVtas = ingenieroVtas;
                nuevoRegistroCabecera.codigoCliente = idCliente;
                nuevoRegistroCabecera.nombreCliente = nombreCliente;
                nuevoRegistroCabecera.idItem = idItem;
                nuevoRegistroCabecera.nombreItem = desItem;

                if (datosPorItem.Cells[0].Value != null) nuevoRegistroCabecera.enero = double.Parse(datosPorItem.Cells[0].Value.ToString());
                if (datosPorItem.Cells[1].Value != null) nuevoRegistroCabecera.febrero = double.Parse(datosPorItem.Cells[1].Value.ToString());
                if (datosPorItem.Cells[2].Value != null) nuevoRegistroCabecera.marzo = double.Parse(datosPorItem.Cells[2].Value.ToString());
                if (datosPorItem.Cells[3].Value != null) nuevoRegistroCabecera.abril = double.Parse(datosPorItem.Cells[3].Value.ToString());
                if (datosPorItem.Cells[4].Value != null) nuevoRegistroCabecera.mayo = double.Parse(datosPorItem.Cells[4].Value.ToString());
                if (datosPorItem.Cells[5].Value != null) nuevoRegistroCabecera.junio = double.Parse(datosPorItem.Cells[5].Value.ToString());
                if (datosPorItem.Cells[6].Value != null) nuevoRegistroCabecera.julio = double.Parse(datosPorItem.Cells[6].Value.ToString());
                if (datosPorItem.Cells[7].Value != null) nuevoRegistroCabecera.agosto = double.Parse(datosPorItem.Cells[7].Value.ToString());
                if (datosPorItem.Cells[8].Value != null) nuevoRegistroCabecera.septiembre = double.Parse(datosPorItem.Cells[8].Value.ToString());
                if (datosPorItem.Cells[9].Value != null) nuevoRegistroCabecera.octubre = double.Parse(datosPorItem.Cells[9].Value.ToString());
                if (datosPorItem.Cells[10].Value != null) nuevoRegistroCabecera.noviembre = double.Parse(datosPorItem.Cells[10].Value.ToString());
                if (datosPorItem.Cells[11].Value != null) nuevoRegistroCabecera.diciembre = double.Parse(datosPorItem.Cells[11].Value.ToString());

                dsEstimadosVentas.Cabecera.Rows.Add(nuevoRegistroCabecera);

                foreach (DataGridViewCell celdaMes in datosPorItem.Cells)
                {
                    BP.AppData.DataSetEstimadosVentas.LineasPorItemRow nuevaLineaPresupuestoPorItem = dsEstimadosVentas.LineasPorItem.NewLineasPorItemRow();

                    Double cantidad = 0;

                    if (celdaMes.Value != null) { cantidad = Double.Parse(celdaMes.Value.ToString()); }

                    nuevaLineaPresupuestoPorItem.ingenieroVtas = ingenieroVtas;
                    nuevaLineaPresupuestoPorItem.codigoCliente = idCliente;
                    nuevaLineaPresupuestoPorItem.pesoKgEstimado = cantidad;
                    nuevaLineaPresupuestoPorItem.idItem = idItem;
                    nuevaLineaPresupuestoPorItem.mesPresupuestado = celdaMes.ColumnIndex + 1;

                    dsEstimadosVentas.LineasPorItem.Rows.Add(nuevaLineaPresupuestoPorItem);
                }

                foreach (var mesPresupuestado in presupuestoPorCiudad)
                {
                    foreach (var presupuestoDetalle in mesPresupuestado.Value)
                    {
                        string ciudad = presupuestoDetalle.Ciudad;
                        double valorPresupuestado = presupuestoDetalle.Valor;

                        BP.AppData.DataSetEstimadosVentas.LineasPorCiudadRow nuevaLineaPorCiudad = dsEstimadosVentas.LineasPorCiudad.NewLineasPorCiudadRow();

                        nuevaLineaPorCiudad.ingenieroVtas = ingenieroVtas;
                        nuevaLineaPorCiudad.codigoCliente = idCliente;
                        nuevaLineaPorCiudad.idItem = idItem;
                        nuevaLineaPorCiudad.mesPresupuestado = mesPresupuestado.Key + 1;
                        nuevaLineaPorCiudad.Ciudad = ciudad;
                        nuevaLineaPorCiudad.Valor = valorPresupuestado;

                        dsEstimadosVentas.LineasPorCiudad.Rows.Add(nuevaLineaPorCiudad);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// elimina el registro de presupuesto y sus dependientes
        /// </summary>
        private void eliminarPresupuesto(int ingenieroVtas, string idCliente, string idItem)
        {
            try
            {
                string cSql = "IngenieroVtas = " + ingenieroVtas.ToString() + " and codigoCliente = '" + idCliente + "' and idItem = '" + idItem + "'";
                DataRow[] cabecera = dsEstimadosVentas.Tables["Cabecera"].Select(cSql);

                if (cabecera.Count() > 0)
                {
                    foreach (DataRow dataRow in cabecera)
                    {
                        dataRow.Delete();
                    }
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Replica el ultimo registro ingresado en el resto del presupuesto 
        /// </summary>
        private void copiarUltimoRegistroIngresado()
        {
            try
            {
                for (int i = 0; i <= 11; i++)
                {
                    if (oEstimadosVentas.ValidaMes(i + 1))
                    {
                        if (presupuestoPorCiudad.ContainsKey(i))
                        {
                            presupuestoPorCiudad.Remove(i);
                            presupuestoPorCiudad.Add(i, ultimosValoresPorCiudadIngresados);
                        }
                        else
                        {
                            presupuestoPorCiudad.Add(i, ultimosValoresPorCiudadIngresados);
                        }

                        dataGridView1C.Rows[0].Cells[i].Value = totalUltimosValoresPorCiudadIngresados.ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// muestra el total de kilos presupuestados por el ingeniero de ventas
        /// </summary>
        private void calculaTotalEstimado()
        {
            try
            {
                for (int i = 0; i <= 11; i++)
                {
                    grdTotales.Rows[0].Cells[i].Value = dsEstimadosVentas.Cabecera.Compute("sum(" + nombreMes(i + 1) + ")", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string nombreMes(int mes)
        {
            string nombre = "";
            switch (mes)
            {
                case 1:
                    nombre = "enero";
                    break;
                case 2:
                    nombre = "febrero";
                    break;
                case 3:
                    nombre = "marzo";
                    break;
                case 4:
                    nombre = "abril";
                    break;
                case 5:
                    nombre = "mayo";
                    break;
                case 6:
                    nombre = "junio";
                    break;
                case 7:
                    nombre = "julio";
                    break;
                case 8:
                    nombre = "agosto";
                    break;
                case 9:
                    nombre = "septiembre";
                    break;
                case 10:
                    nombre = "octubre";
                    break;
                case 11:
                    nombre = "noviembre";
                    break;
                case 12:
                    nombre = "diciembre";
                    break;
                default:
                    break;
            }

            return nombre;
        }

        private void rbCatalogoCliente_CheckedChanged(object sender, EventArgs e)
        {
            llenaComboProductos();
        }

        private void rbMaestroArticulos_CheckedChanged(object sender, EventArgs e)
        {
            llenaComboProductos();
        }
    }
}

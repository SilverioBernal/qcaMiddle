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
    public partial class frmGestionFletes : Form
    {
        ClsDataFletes gestionFletes = new ClsDataFletes();
        ClsDataInventario inventario = new ClsDataInventario();

        List<Transporter> transportadoras = new List<Transporter>();
        List<Vehicle> placas = new List<Vehicle>();
        List<Vehicle> placasSinFiltro = new List<Vehicle>();
        List<Driver> conductores = new List<Driver>();
        List<VehicleType> tiposVehiculo = new List<VehicleType>();
        List<Almacen> almacenes = new List<Almacen>();
        List<Flete> fletes = new List<Flete>();
        List<Flete> tarifasFlete = new List<Flete>();

        public frmGestionFletes()
        {
            InitializeComponent();
        }

        private void frmGestionFletes_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnCancel.Enabled = false;

            tarifasFlete = gestionFletes.GetTarifasFlete();

            almacenes.Add(new Almacen() { WhsName = "Seleccione" });
            almacenes.AddRange(gestionFletes.GetZonas().ToList());
            //almacenes.AddRange(inventario.ListarAlmacenes());

            tiposVehiculo.Add(new VehicleType() { name = "Seleccione" });
            tiposVehiculo.AddRange(gestionFletes.GetTiposVehiculo().OrderBy(x => x.name).ToList());

            transportadoras.Add(new Transporter() { name = "Seleccione" });
            transportadoras.AddRange(gestionFletes.GetTransportadoras().OrderBy(x => x.name).ToList());

            conductores.Add(new Driver() { name = "Seleccione" });
            conductores.AddRange(gestionFletes.GetConductores().OrderBy(x => x.name).ToList());

            placas.Add(new Vehicle() { name = "Seleccione" });
            placas.AddRange(gestionFletes.GetPlacas());
            placasSinFiltro.AddRange(gestionFletes.GetPlacas().OrderBy(x => x.name).ToList());

            cboWharehouse.DataSource = almacenes;
            cboWharehouse.ValueMember = "WhsCode";
            cboWharehouse.DisplayMember = "WhsName";

            cboVehicleType.DataSource = tiposVehiculo;
            cboVehicleType.ValueMember = "code";
            cboVehicleType.DisplayMember = "name";

            cboTransporter.DataSource = transportadoras;
            cboTransporter.ValueMember = "code";
            cboTransporter.DisplayMember = "name";

            cboDriver.DataSource = conductores;
            cboDriver.ValueMember = "code";
            cboDriver.DisplayMember = "name";

            cboVehicle.DataSource = placas;
            cboVehicle.ValueMember = "code";
            cboVehicle.DisplayMember = "name";

            filteringParameters();
        }

        private void cboVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVehicleType.SelectedText != "Seleccione")
                filteringParameters();
        }

        private void cboTransporter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVehicleType.SelectedText != "Seleccione")
                filteringParameters();
        }       

        private void cboVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVehicle.SelectedValue != null)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                statusLabel.Text = "";

                fletes = gestionFletes.GetDeliverys(dpFrom.Value, cboWharehouse.SelectedValue.ToString());
                limpiarGrids(dgDeliverys);

                dgDeliverys.Columns.Add("destino", "Destino");
                dgDeliverys.Columns.Add("tipo", "Tipo");
                dgDeliverys.Columns.Add("cliente", "Cliente");
                dgDeliverys.Columns.Add("vendedor", "Rep. ventas");
                dgDeliverys.Columns.Add("orden", "No. orden");
                dgDeliverys.Columns.Add("serie", "Serie");
                dgDeliverys.Columns.Add("peso", "Cant. kilos");
                dgDeliverys.Columns.Add("tarifa", "Tarifa");
                dgDeliverys.Columns.Add("docEntry", "docEntry");

                dgDeliverys.Columns[0].DataPropertyName = "destino";
                dgDeliverys.Columns[1].DataPropertyName = "tipo";
                dgDeliverys.Columns[2].DataPropertyName = "cliente";
                dgDeliverys.Columns[3].DataPropertyName = "vendedor";
                dgDeliverys.Columns[4].DataPropertyName = "orden";
                dgDeliverys.Columns[5].DataPropertyName = "serie";
                dgDeliverys.Columns[6].DataPropertyName = "peso";
                dgDeliverys.Columns[7].DataPropertyName = "tarifa";
                dgDeliverys.Columns[8].DataPropertyName = "docEntry";

                dgDeliverys.Columns[0].ReadOnly = true;
                dgDeliverys.Columns[1].ReadOnly = true;
                dgDeliverys.Columns[2].ReadOnly = true;
                dgDeliverys.Columns[3].ReadOnly = true;
                dgDeliverys.Columns[4].ReadOnly = true;
                dgDeliverys.Columns[5].ReadOnly = true;
                dgDeliverys.Columns[6].ReadOnly = true;
                dgDeliverys.Columns[7].ReadOnly = true;

                dgDeliverys.Columns[5].Visible = false;
                dgDeliverys.Columns[8].Visible = false;

                dgDeliverys.DataSource = fletes;

                DataGridViewCheckBoxColumn dch = new DataGridViewCheckBoxColumn();
                dch.HeaderText = "Selección";
                dgDeliverys.Columns.Add(dch);
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
        }

        private void txtMacroguia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                loadMacroGuia(this.txtMacroguia.Text);
            }
        }

        private void dgDeliverys_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                double pesoTotal = 0, valorTotal = 0;
                int totalSeleccionado = 0;

                foreach (DataGridViewRow item in dgDeliverys.Rows)
                {
                    bool seleccionado = Convert.ToBoolean(item.Cells[9].EditedFormattedValue);

                    if (seleccionado)
                    {
                        pesoTotal += Convert.ToDouble(item.Cells[6].Value.ToString());
                        valorTotal += Convert.ToDouble(item.Cells[7].Value.ToString());
                        totalSeleccionado++;
                    }
                }

                Dictionary<string, string> totales = new Dictionary<string, string>();
                totales.Add("Peso total", pesoTotal.ToString());
                totales.Add("Valor total", valorTotal.ToString());
                totales.Add("Seleccionados", totalSeleccionado.ToString());

                dgTotals.DataSource = totales.ToArray();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult miDialogo = MessageBox.Show("La información será actualizada", "Mensaje del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (miDialogo == DialogResult.OK)
            {
                try
                {
                    Vehicle vehiculo = placasSinFiltro.Where(x => x.code == cboVehicle.SelectedValue.ToString()).First();

                    List<Flete> fletes = new List<Flete>();

                    foreach (DataGridViewRow item in dgDeliverys.Rows)
                    {
                        bool seleccionado = Convert.ToBoolean(item.Cells[9].EditedFormattedValue);
                        string documento = item.Cells["orden"].Value.ToString();

                        if (seleccionado)
                        {
                            double tarifa = Convert.ToDouble(item.Cells["tarifa"].Value.ToString());

                            if (tarifa > 0)
                            {
                                Flete flete = new Flete()
                                {
                                    origen = cboWharehouse.SelectedValue.ToString(),
                                    destino = item.Cells["destino"].Value.ToString(),
                                    tipo = item.Cells["tipo"].Value.ToString(),
                                    cliente = item.Cells["cliente"].Value.ToString(),
                                    vendedor = item.Cells["vendedor"].Value.ToString(),
                                    orden = item.Cells["orden"].Value.ToString(),
                                    serie = item.Cells["serie"].Value.ToString(),
                                    peso = double.Parse(item.Cells["peso"].Value.ToString()),
                                    tarifa = double.Parse(item.Cells["tarifa"].Value.ToString()),
                                    docEntry = item.Cells["docEntry"].Value.ToString(),
                                };
                                fletes.Add(flete);
                            }
                            else
                            {
                                throw new Exception(string.Format("Existen acuerdos tarifarios NO validos. Por favor verificar", documento));
                            }
                        }
                    }

                    int idMacroguide = gestionFletes.SaveMacroGuide(fletes, vehiculo, cboDriver.SelectedValue.ToString(), txtObservaciones.Text);
                    MessageBox.Show(string.Format("Actualización realizada con éxito. Documento de Transporte: {0}", idMacroguide.ToString()), "Fletes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtMacroguia.Text = idMacroguide.ToString();

                    loadMacroGuia(idMacroguide.ToString());
                }
                catch (Exception ex)
                {
                    statusLabel.Text = ex.Message;
                    MessageBox.Show(ex.Message, "Error tarifario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Macroguia reporte = gestionFletes.PrintMacroGuide(txtMacroguia.Text);

                frmReporteFletes formularioReporte = new frmReporteFletes(reporte);
                formularioReporte.Show();
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gestionFletes.CancelMacroGuide(txtMacroguia.Text))
                    MessageBox.Show("El documento de Transporte fue anulado con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                statusLabel.Text = string.Format("Error al anular el documento de transporte: {0} ", ex.Message);
            }

            txtMacroguia.Text = string.Empty;

            cboWharehouse.SelectedIndex = 0;
            cboVehicleType.SelectedIndex = 0;
            cboTransporter.SelectedIndex = 0;
            cboDriver.SelectedIndex = 0;
            cboVehicle.SelectedIndex = 0;
            
            dgDeliverys.DataSource = null;
        }

        private void limpiarGrids(DataGridView grid)
        {
            grid.DataSource = null;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
        }

        private void loadMacroGuia(string id)
        {
            btnSave.Enabled = false;
            int macroguiaId = 0;

            if (Int32.TryParse(id, out macroguiaId))
            {
                limpiarGrids(dgDeliverys);

                fletes = gestionFletes.GetMacroGiude(macroguiaId);

                dgDeliverys.Columns.Add("destino", "Destino");
                dgDeliverys.Columns.Add("tipo", "Tipo");
                dgDeliverys.Columns.Add("cliente", "Cliente");
                dgDeliverys.Columns.Add("vendedor", "Rep. ventas");
                dgDeliverys.Columns.Add("orden", "No. orden");
                dgDeliverys.Columns.Add("serie", "Serie");
                dgDeliverys.Columns.Add("peso", "Cant. kilos");
                dgDeliverys.Columns.Add("seleccionado", "Seleccionado");

                dgDeliverys.Columns[0].DataPropertyName = "destino";
                dgDeliverys.Columns[1].DataPropertyName = "tipo";
                dgDeliverys.Columns[2].DataPropertyName = "cliente";
                dgDeliverys.Columns[3].DataPropertyName = "vendedor";
                dgDeliverys.Columns[4].DataPropertyName = "orden";
                dgDeliverys.Columns[5].DataPropertyName = "serie";
                dgDeliverys.Columns[6].DataPropertyName = "peso";
                dgDeliverys.Columns[7].DataPropertyName = "seleccionado";

                dgDeliverys.Columns[5].Visible = false;

                dgDeliverys.DataSource = fletes;

                btnPrint.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        public void filteringParameters()
        {
            placas.Clear();
            List<Vehicle> placasFiltered = new List<Vehicle>();
            statusLabel.Text = "";

            try
            {
                if (cboVehicleType.SelectedValue != null && cboTransporter.SelectedValue != null )
                {
                    placas = placasSinFiltro
                        .Where(x =>
                            x.tipoVehiculo == cboVehicleType.SelectedValue.ToString() &&
                            x.transportadora == cboTransporter.SelectedValue.ToString())
                            .ToList();

                    bool tarifaOk = false, tarifasOk = true;

                    foreach (Flete item in fletes)
                    {
                        tarifaOk = tarifasFlete.Any(x => x.origen == item.origen 
                            && x.destino == item.destino 
                            && x.tipoVehiculo == cboVehicleType.SelectedValue.ToString()
                            && x.transportadora == cboTransporter.SelectedValue.ToString());

                        if (tarifaOk)
                        {
                            double tarifa = tarifasFlete.Where(x => x.origen == item.origen
                            && x.destino == item.destino
                            && x.tipoVehiculo == cboVehicleType.SelectedValue.ToString()
                            && x.transportadora == cboTransporter.SelectedValue.ToString()).Select(x=> x.tarifa).FirstOrDefault();

                            item.tarifa = tarifa * item.peso;
                        }
                        else
                        {
                            tarifasOk = false;
                            item.tarifa = -1;
                        }
                    }

                    if (!tarifasOk)
                        statusLabel.Text = "Revise sus acuerdos tarifarios.";

                    limpiarGrids(dgDeliverys);

                    dgDeliverys.Columns.Add("destino", "Destino");
                    dgDeliverys.Columns.Add("tipo", "Tipo");
                    dgDeliverys.Columns.Add("cliente", "Cliente");
                    dgDeliverys.Columns.Add("vendedor", "Rep. ventas");
                    dgDeliverys.Columns.Add("orden", "No. orden");
                    dgDeliverys.Columns.Add("serie", "Serie");
                    dgDeliverys.Columns.Add("peso", "Cant. kilos");
                    dgDeliverys.Columns.Add("tarifa", "Tarifa");
                    dgDeliverys.Columns.Add("docEntry", "docEntry");

                    dgDeliverys.Columns[0].DataPropertyName = "destino";
                    dgDeliverys.Columns[1].DataPropertyName = "tipo";
                    dgDeliverys.Columns[2].DataPropertyName = "cliente";
                    dgDeliverys.Columns[3].DataPropertyName = "vendedor";
                    dgDeliverys.Columns[4].DataPropertyName = "orden";
                    dgDeliverys.Columns[5].DataPropertyName = "serie";
                    dgDeliverys.Columns[6].DataPropertyName = "peso";
                    dgDeliverys.Columns[7].DataPropertyName = "tarifa";
                    dgDeliverys.Columns[8].DataPropertyName = "docEntry";

                    dgDeliverys.Columns[0].ReadOnly = true;
                    dgDeliverys.Columns[1].ReadOnly = true;
                    dgDeliverys.Columns[2].ReadOnly = true;
                    dgDeliverys.Columns[3].ReadOnly = true;
                    dgDeliverys.Columns[4].ReadOnly = true;
                    dgDeliverys.Columns[5].ReadOnly = true;
                    dgDeliverys.Columns[6].ReadOnly = true;
                    dgDeliverys.Columns[7].ReadOnly = true;

                    dgDeliverys.Columns[5].Visible = false;
                    dgDeliverys.Columns[8].Visible = false;

                    dgDeliverys.DataSource = fletes;

                    DataGridViewCheckBoxColumn dch = new DataGridViewCheckBoxColumn();
                    dch.HeaderText = "Selección";
                    dgDeliverys.Columns.Add(dch);
                }                

                if (cboVehicleType.SelectedValue != null && cboTransporter.SelectedValue == null)
                {
                    placas = placasSinFiltro
                        .Where(x =>
                            x.tipoVehiculo == cboVehicleType.SelectedValue.ToString())
                            .ToList();
                }

                if (cboVehicleType.SelectedValue == null && cboTransporter.SelectedValue != null)
                {
                    placas = placasSinFiltro
                        .Where(x =>
                            x.transportadora == cboTransporter.SelectedValue.ToString())
                            .ToList();
                }

                if (cboVehicleType.SelectedValue == null && cboTransporter.SelectedValue == null)
                {
                    placas = placasSinFiltro.ToList();
                }
                
                placasFiltered.Add(new Vehicle() { name = "Seleccione" });
                placasFiltered.AddRange(placas);
            }
            catch (Exception)
            {
                statusLabel.Text = "No hay coincidencias para los datos ingresados";
                placasFiltered = new List<Vehicle>();
                placasFiltered.Add(new Vehicle() { name = "Seleccione" });
            }

            cboVehicle.DataSource = placasFiltered;
            cboVehicle.ValueMember = "code";
            cboVehicle.DisplayMember = "name";
        }
    }
}

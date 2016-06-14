using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAPbobsCOM;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using BP.EsquemasReporteFletes;

namespace BP
{
    public partial class frmFletes : Form
    {
        bool cargaValorInicialBodega = false;
        public frmFletes()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmFletes_Load(object sender, EventArgs e)
        {
            try
            {
                string miConsulta = "SELECT '0' AS Code,' --Seleccione Transportadora--' AS Razon UNION SELECT Code,U_CSS_Razon_Social AS Razon FROM [@CSS_TRANSPORTADORA] " +
                                    "SELECT ' --Seleccione Placa--' AS Code UNION SELECT Code  FROM [@CSS_VEHICULO] " +
                                    "SELECT '0' AS ZipCode,' --Seleccione Bodega--' AS WhsCode UNION SELECT ZipCode,WhsCode FROM OWHS " +
                                    "SELECT '0' As Code,' --Seleccione Conductor--' AS U_CSS_Nombre UNION SELECT Code,U_CSS_Nombre FROM [@CSS_CONDUCTOR] " +
                                    "SELECT '0' As Code,' --Seleccione Tipo Vehículo--' AS Name UNION SELECT Code,Name FROM [@CSS_TIPO_VEHICULO] ";
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);
                ClaseDatos.SqlUnConnex();
                cargaValorInicialBodega = false;
                this.cmbBodega.DisplayMember = "WhsCode";
                this.cmbBodega.ValueMember = "ZipCode";
                this.cmbBodega.DataSource = misDatos.Tables[2];
                this.cmbBodega.SelectedIndex = this.cmbBodega.FindString(" --Seleccione Bodega--");
                cargaValorInicialBodega = true;
                this.cmbPlaca.DataSource = misDatos.Tables[1];
                this.cmbPlaca.DisplayMember = "Code";
                this.cmbPlaca.ValueMember = "Code";
                this.cmbPlaca.SelectedIndex = this.cmbPlaca.FindString(" --Seleccione Placa--");
                this.cmdTransportadora.DataSource = misDatos.Tables[0];
                this.cmdTransportadora.DisplayMember = "Razon";
                this.cmdTransportadora.ValueMember = "Code";
                this.cmdTransportadora.SelectedIndex = this.cmdTransportadora.FindString(" --Seleccione Transportadora--");
                this.cmbConductor.DataSource = misDatos.Tables[3];
                this.cmbConductor.DisplayMember = "U_CSS_Nombre";
                this.cmbConductor.ValueMember = "Code";
                this.cmbConductor.SelectedIndex = this.cmbConductor.FindString(" --Seleccione Conductor--");
                this.cmbTipoVehiculo.DataSource = misDatos.Tables[4];
                this.cmbTipoVehiculo.DisplayMember = "Name";
                this.cmbTipoVehiculo.ValueMember = "Code";
                this.cmbTipoVehiculo.SelectedIndex = this.cmbTipoVehiculo.FindString(" --Seleccione Tipo Vehículo--");
                this.btnGrabar.Enabled = false;
                this.btnAnular.Enabled = false;
                this.btnImprimir.Enabled = false;
                this.cmbTipoVehiculo.Enabled = false;
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string miConsulta = "";
                bool miEstadoValidacion = true;
                string miFecha = this.dtpFechaEntrega.Value.ToString("yyyyMMdd");
                if (this.cmbBodega.Text.ToString().Equals(" --Seleccione Bodega--"))
                {
                    MessageBox.Show("Por Favor seleccione Bodega", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    miEstadoValidacion = false;
                }
                else if (this.cmdTransportadora.Text.ToString().Equals(" --Seleccione Transportadora--"))
                {
                    MessageBox.Show("Por Favor seleccione Transportadora", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    miEstadoValidacion = false;
                }
                else if (this.cmbTipoVehiculo.Text.ToString().Equals(" --Seleccione Tipo Vehículo--"))
                {
                    MessageBox.Show("Por Favor seleccione un Tipo de Vehículo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    miEstadoValidacion = false;
                }
                else if (!this.cmbZonaDestinoInicial.Text.ToString().Equals(" --Seleccione Zona--") && this.cmbZonaDestinoInicial.Enabled)
                {
                    if (!this.cmbZonaDestinoFinal.Text.ToString().Equals(" --Seleccione Zona--"))
                    {
                        miConsulta = "SELECT T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.DocNum,T0.Series,T0.Peso, " +
                                     "T1.U_CSS_Tarifa,T0.DocEntry,T0.Origen " +
                                     "FROM CSS_ZONA_FLETE T0 " +
                                     "LEFT JOIN [@CSS_TARIFA_FLETE] T1 " +
                                     "ON T0.Peso >= T1.U_CSS_Peso_Inicial " +
                                     "AND T0.Peso <= T1.U_CSS_Peso_Final " +
                                     "AND T1.U_CSS_Transportadora='" + this.cmdTransportadora.SelectedValue + "' " +
                                     "AND T0.Origen=T1.U_CSS_Zona_Origen " +
                                     "AND T0.Destino=T1.U_CSS_Zona_Destino " +
                                     "AND T1.U_CSS_Tipo_Vehiculo='" + this.cmbTipoVehiculo.SelectedValue.ToString() + "' " +
                                     "WHERE T0.DocDueDate='" + miFecha + "' " +
                                     "AND T0.Destino IN ('" + this.cmbZonaDestinoInicial.Text + "','" + this.cmbZonaDestinoFinal.Text + "') " +
                                     "AND WhsCode='" + this.cmbBodega.Text + "' " +
                                     "GROUP BY T0.DocNum,T0.Series,T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.Peso,T1.U_CSS_Tarifa,T0.DocEntry,T0.Origen " +
                                     "ORDER BY T0.Destino,T0.Tipo";
                    }
                    else
                    {
                        miConsulta = "SELECT T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.DocNum,T0.Series,T0.Peso, " +
                                     "T1.U_CSS_Tarifa,T0.DocEntry,T0.Origen " +
                                     "FROM CSS_ZONA_FLETE T0 " +
                                     "LEFT JOIN [@CSS_TARIFA_FLETE] T1 " +
                                     "ON T0.Peso >= T1.U_CSS_Peso_Inicial " +
                                     "AND T0.Peso <= T1.U_CSS_Peso_Final " +
                                     "AND T1.U_CSS_Transportadora='" + this.cmdTransportadora.SelectedValue + "' " +
                                     "AND T0.Origen=T1.U_CSS_Zona_Origen " +
                                     "AND T0.Destino=T1.U_CSS_Zona_Destino " +
                                     "AND T1.U_CSS_Tipo_Vehiculo='" + this.cmbTipoVehiculo.SelectedValue.ToString() + "' " +
                                     "WHERE T0.DocDueDate='" + miFecha + "' " +
                                     "AND T0.Destino='" + this.cmbZonaDestinoInicial.Text + "' " +
                                     "AND WhsCode='" + this.cmbBodega.Text + "' " +
                                     "GROUP BY T0.DocNum,T0.Series,T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.Peso,T1.U_CSS_Tarifa,T0.DocEntry,T0.Origen " +
                                     "ORDER BY T0.Destino,T0.Tipo";
                    }
                }
                else
                {
                    miConsulta = "SELECT T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.DocNum,T0.Series,T0.Peso, " +
                                 "T1.U_CSS_Tarifa,T0.DocEntry,T0.Origen " +
                                 "FROM CSS_ZONA_FLETE T0 " +
                                 "LEFT JOIN [@CSS_TARIFA_FLETE] T1 " +
                                 "ON T0.Peso >= T1.U_CSS_Peso_Inicial " +
                                 "AND T0.Peso <= T1.U_CSS_Peso_Final " +
                                 "AND T1.U_CSS_Transportadora='" + this.cmdTransportadora.SelectedValue + "' " +
                                 "AND T0.Origen=T1.U_CSS_Zona_Origen " +
                                 "AND T0.Destino=T1.U_CSS_Zona_Destino " +
                                 "AND T1.U_CSS_Tipo_Vehiculo='" + this.cmbTipoVehiculo.SelectedValue.ToString() + "' " +
                                 "WHERE T0.DocDueDate='" + miFecha + "' " +
                                 "AND WhsCode='" + this.cmbBodega.Text + "' " +
                                 "GROUP BY T0.DocNum,T0.Series,T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.Peso,T1.U_CSS_Tarifa,T0.DocEntry,T0.Origen " +
                                 "ORDER BY T0.Destino,T0.Tipo";
                }
                if (miEstadoValidacion)
                {
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    IDataReader misDatos = ClaseDatos.procesaDataReader(miConsulta);
                    this.dgvResultados.Rows.Clear();
                    bool miEstadoCarga = false;
                    while (misDatos.Read())
                    {
                        miEstadoCarga = true;
                        object[] misValores = new object[10];
                        misValores[0] = misDatos.GetValue(0);
                        misValores[1] = misDatos.GetValue(1);
                        misValores[2] = misDatos.GetValue(2);
                        misValores[3] = misDatos.GetValue(3);
                        misValores[4] = misDatos.GetValue(4);
                        misValores[5] = misDatos.GetValue(5);
                        misValores[6] = Convert.ToDouble(misDatos.GetValue(6));
                        if (misDatos.GetValue(7).ToString().Length > 0)
                        {
                            misValores[7] = misDatos.GetValue(7);
                        }
                        else
                        {
                            string miConsultaInterna = "SELECT U_CSS_Tarifa,U_CSS_Peso " +
                                                       "FROM [@CSS_MOVIMIENTO] " +
                                                       "WHERE U_CSS_Zona_Origen='" + misDatos.GetValue(9) + "' " +
                                                       "AND U_CSS_Zona_Destino='" + misDatos.GetValue(0) + "' ";
                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            IDataReader misDatosInternos = ClaseDatos.procesaDataReader(miConsultaInterna);
                            if (misDatosInternos.Read())
                            {
                                misValores[7] = (Convert.ToDouble(misValores[6]) * Convert.ToDouble(misDatosInternos.GetValue(0))) / Convert.ToDouble(misDatosInternos.GetValue(1));
                            }
                            else
                            {
                                misValores[7] = "0";
                            }
                            ClaseDatos.SqlUnConnex();
                        }
                        misValores[8] = false;
                        misValores[9] = misDatos.GetValue(8);
                        this.dgvResultados.Rows.Add(misValores);
                    }
                    if (!miEstadoCarga)
                    {
                        MessageBox.Show("No hay resultados que correspondan con los parámetros elegidos", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.cmbConductor.Enabled = false;
                        this.cmbPlaca.Enabled = false;
                    }
                    else
                    {
                        this.cmbConductor.Enabled = true;
                        this.cmbPlaca.Enabled = true;
                        this.btnGrabar.Enabled = true;
                    }
                    this.btnImprimir.Enabled = false;
                    this.btnAnular.Enabled = false;
                    ClaseDatos.SqlUnConnex();
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error el cargar los datos: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBodega_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cargaValorInicialBodega)
                {
                    if (this.cmbBodega.SelectedValue != null)
                    {
                        if (!this.cmbBodega.Text.ToString().Equals(" --Seleccione Bodega--"))
                        {
                            string miConsulta = "SELECT ' --Seleccione Zona--' AS Name UNION SELECT U_CSS_Zona FROM [@CSS_ALMACEN_ZONA] " +
                                                "WHERE U_CSS_Almacen='" + this.cmbBodega.Text.ToString() + "' " +
                                                "SELECT ' --Seleccione Zona--' AS Name UNION SELECT U_CSS_Zona FROM [@CSS_ALMACEN_ZONA] " +
                                                "WHERE U_CSS_Almacen='" + this.cmbBodega.Text.ToString() + "'";
                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            DataSet misDatos = ClaseDatos.procesaDataSet(miConsulta);
                            ClaseDatos.SqlUnConnex();
                            if (misDatos.Tables[0].Rows.Count > 1)
                            {
                                this.cmbZonaDestinoInicial.DataSource = misDatos.Tables[0];
                                this.cmbZonaDestinoInicial.DisplayMember = "Name";
                                this.cmbZonaDestinoInicial.ValueMember = "Name";
                                this.cmbZonaDestinoInicial.SelectedIndex = this.cmbZonaDestinoInicial.FindString(" --Seleccione Zona--");
                                this.cmbZonaDestinoFinal.DataSource = misDatos.Tables[1];
                                this.cmbZonaDestinoFinal.DisplayMember = "Name";
                                this.cmbZonaDestinoFinal.ValueMember = "Name";
                                this.cmbZonaDestinoFinal.SelectedIndex = this.cmbZonaDestinoInicial.FindString(" --Seleccione Zona--");
                                this.cmbZonaDestinoInicial.Enabled = true;
                            }
                            else
                            {
                                this.cmbZonaDestinoInicial.Enabled = false;
                                this.cmbZonaDestinoFinal.Enabled = false;
                            }
                            this.cmdTransportadora.Enabled = true;
                            this.cmbConductor.Enabled = true;
                            this.cmbPlaca.Enabled = true;
                            this.cmbTipoVehiculo.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar las zonas asociadas a la bodega: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbZonaDestinoInicial_TextChanged(object sender, EventArgs e)
        {
            if (!this.cmbZonaDestinoInicial.Text.Equals(" --Seleccione Zona--"))
            {
                this.cmbZonaDestinoFinal.Enabled = true;
            }
            else
            {
                this.cmbZonaDestinoFinal.Enabled = false;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvResultados.Rows.Count == 0)
                {
                    MessageBox.Show("No hay valores para almacenar sobre la matriz de resultados", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (this.cmbConductor.Text.Equals(" --Seleccione Conductor--"))
                {
                    MessageBox.Show("Por Favor Seleccione un conductor", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (this.cmbPlaca.Text.Equals(" --Seleccione Placa--"))
                {
                    MessageBox.Show("Por Favor Seleccione una placa", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Recordset miRecordSet = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    Documents miEntregaVentas = (Documents)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);

                    StockTransfer miMovimientoInventario = (StockTransfer)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                    UserTable miVehiculo = ClaseDatos.objCompany.UserTables.Item("CSS_VEHICULO");
                    UserTable miMovimiento = ClaseDatos.objCompany.UserTables.Item("CSS_MOVIMIENTO");
                    bool miEstadoCarga = false;

                    #region try guardar
                    try
                    {
                        DialogResult miDialogo = MessageBox.Show("La información será actualizada", "Mensaje del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (miDialogo == DialogResult.OK)
                        {
                            ClaseDatos.objCompany.StartTransaction();
                            miVehiculo.GetByKey(this.cmbPlaca.SelectedValue.ToString());
                            string miSQL = "SELECT isnull(MAX(convert(int,U_CSS_Macroguia)),0) as Contador FROM [@CSS_MOVIMIENTO]";
                            miRecordSet.DoQuery(miSQL);
                            miRecordSet.MoveFirst();
                            int miMacroguia = Convert.ToInt32(miRecordSet.Fields.Item(0).Value.ToString()) + 1;
                            string miZona = "";
                            for (int miContador = 0; miContador < this.dgvResultados.Rows.Count; miContador++)
                            {
                                if (Convert.ToBoolean(this.dgvResultados.Rows[miContador].Cells[8].Value.ToString()))
                                {
                                    if (Convert.ToDouble(this.dgvResultados.Rows[miContador].Cells[7].Value.ToString()) > 0)
                                    {
                                        if (miZona.Length == 0)
                                        {
                                            miZona = this.dgvResultados.Rows[miContador].Cells[0].Value.ToString();
                                        }
                                        else if (!miZona.Equals(this.dgvResultados.Rows[miContador].Cells[0].Value.ToString()))
                                        {
                                            throw new Exception("Solo se puede asignar una zona por Documento de Transporte");
                                        }
                                        miEstadoCarga = true;
                                        miSQL = "SELECT isnull(MAX(convert(int,Code)),0) as Contador FROM [@CSS_MOVIMIENTO]";
                                        miRecordSet.DoQuery(miSQL);
                                        miRecordSet.MoveFirst();
                                        int miCodigo = Convert.ToInt32(miRecordSet.Fields.Item(0).Value.ToString()) + 1;
                                        miMovimiento.Code = miCodigo.ToString();
                                        miMovimiento.Name = miCodigo.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Macroguia").Value = miMacroguia;
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Documento_SAP").Value = this.dgvResultados.Rows[miContador].Cells[4].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Serie").Value = this.dgvResultados.Rows[miContador].Cells[5].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Tipo_Documento").Value = this.dgvResultados.Rows[miContador].Cells[1].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Estado").Value = "Activo";
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Zona_Origen").Value = this.cmbBodega.SelectedValue;
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Zona_Destino").Value = this.dgvResultados.Rows[miContador].Cells[0].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Tarifa").Value = this.dgvResultados.Rows[miContador].Cells[7].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Peso").Value = this.dgvResultados.Rows[miContador].Cells[6].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Cliente").Value = this.dgvResultados.Rows[miContador].Cells[2].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Representante").Value = this.dgvResultados.Rows[miContador].Cells[3].Value.ToString().Length > 32 ? this.dgvResultados.Rows[miContador].Cells[3].Value.ToString().Substring(0, 32) : this.dgvResultados.Rows[miContador].Cells[3].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_DocEntry").Value = this.dgvResultados.Rows[miContador].Cells[9].Value.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Fecha").Value = DateTime.Now.ToString("yyyyMMdd hh:mm tt");
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Transportadora").Value = this.cmdTransportadora.SelectedValue.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Vehiculo").Value = this.cmbPlaca.SelectedValue.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_Conductor").Value = this.cmbConductor.SelectedValue.ToString();
                                        miMovimiento.UserFields.Fields.Item("U_CSS_OBSERVACIONES").Value = this.txt_Comentario.Text;

                                        int resultado = miMovimiento.Add();

                                        if (resultado < 0)
                                        {
                                            throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                                        }

                                        if (this.dgvResultados.Rows[miContador].Cells[1].Value.ToString().Equals("T"))
                                        {
                                            miMovimientoInventario.GetByKey(Convert.ToInt32(this.dgvResultados.Rows[miContador].Cells[9].Value.ToString()));
                                            miMovimientoInventario.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = this.dgvResultados.Rows[miContador].Cells[7].Value.ToString();
                                            miMovimientoInventario.UserFields.Fields.Item("U_CSS_Vehiculo").Value = this.cmbPlaca.SelectedValue.ToString();
                                            miMovimientoInventario.UserFields.Fields.Item("U_CSS_Transportadora").Value = this.cmdTransportadora.SelectedValue.ToString();
                                            miMovimientoInventario.UserFields.Fields.Item("U_CSS_Conductor").Value = this.cmbConductor.SelectedValue.ToString();
                                            miMovimientoInventario.UserFields.Fields.Item("U_CSS_Propietario").Value = miVehiculo.UserFields.Fields.Item("U_CSS_Propietario").Value;
                                            miMovimientoInventario.UserFields.Fields.Item("U_CSS_Macroguia").Value = miMacroguia;
                                            resultado = miMovimientoInventario.Update();

                                            if (resultado < 0)
                                            {
                                                throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                                            }
                                        }
                                        else
                                        {
                                            miEntregaVentas.GetByKey(Convert.ToInt32(this.dgvResultados.Rows[miContador].Cells[9].Value.ToString()));
                                            miEntregaVentas.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = this.dgvResultados.Rows[miContador].Cells[7].Value.ToString();
                                            miEntregaVentas.UserFields.Fields.Item("U_CSS_Vehiculo").Value = this.cmbPlaca.SelectedValue.ToString();
                                            miEntregaVentas.UserFields.Fields.Item("U_CSS_Transportadora").Value = this.cmdTransportadora.SelectedValue.ToString();
                                            miEntregaVentas.UserFields.Fields.Item("U_CSS_Conductor").Value = this.cmbConductor.SelectedValue.ToString();
                                            miEntregaVentas.UserFields.Fields.Item("U_CSS_Propietario").Value = miVehiculo.UserFields.Fields.Item("U_CSS_Propietario").Value;
                                            miEntregaVentas.UserFields.Fields.Item("U_CSS_Macroguia").Value = miMacroguia;
                                            resultado = miEntregaVentas.Update();

                                            if (resultado < 0)
                                            {
                                                throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("El valor para el documento " + this.dgvResultados.Rows[miContador].Cells[4].Value.ToString() +
                                            " en la línea " + Convert.ToInt32(miContador + 1).ToString() + " debe ser mayor a 0");
                                    }
                                }
                            }
                            if (ClaseDatos.objCompany.InTransaction)
                            {
                                ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                            }

                            if (miEstadoCarga)
                            {
                                MessageBox.Show("Actualización realizada con éxito. Documento de Transporte: " + miMacroguia.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.btnGrabar.Enabled = false;
                                this.btnImprimir.Enabled = true;
                                this.txtMacroguia.Text = miMacroguia.ToString();
                            }
                            else
                            {
                                MessageBox.Show("No ha seleccionado ningún documento para actualizar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception miExcepcion)
                    {
                        ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        MessageBox.Show("Error al realizar la actualización: " + miExcepcion.Message.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(miEntregaVentas);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(miMovimientoInventario);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(miRecordSet);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(miMovimiento);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error inesperado: " + ex.Message.ToString() + " - " + ex.StackTrace.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                IDataReader miLectorMovimientos;
                string miConsulta = "SELECT Code " +
                                  "FROM [@CSS_MOVIMIENTO] " +
                                  "WHERE U_CSS_Macroguia='" + this.txtMacroguia.Text + "'";
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                miLectorMovimientos = ClaseDatos.procesaDataReader(miConsulta);
                UserTable miTablaMovimientos = ClaseDatos.objCompany.UserTables.Item("CSS_MOVIMIENTO");
                while (miLectorMovimientos.Read())
                {
                    miTablaMovimientos.GetByKey(miLectorMovimientos.GetString(0));
                    miTablaMovimientos.UserFields.Fields.Item("U_CSS_Fecha_Impresio").Value = DateTime.Now.ToString("yyyyMMdd hh:mm tt");
                    miTablaMovimientos.Update();
                }
                ClaseDatos.SqlUnConnex();
                DataSetFletes miDataSetFletes = new DataSetFletes();
                Macroguia miReporte = new Macroguia();
                miConsulta = "SELECT T0.CompnyName,T0.CompnyAddr,T0.Phone1,T0.Fax,T0.TaxIdNum,T1.City,T2.Name " +
                                "FROM ADM1 T1,OADM T0 " +
                                "INNER JOIN OCRY T2 " +
                                "ON T0.Country=T2.Code " +

                                "SELECT substring(T0.U_CSS_Fecha,0,10) AS Fecha,substring(T0.U_CSS_Fecha,9,15) As Hora ,T0.U_CSS_Zona_Origen,T0.U_CSS_Zona_Destino,T0.U_CSS_Macroguia,T0.U_CSS_Vehiculo,T0.U_CSS_Estado,T0.U_CSS_OBSERVACIONES, " +
                                "T1.U_CSS_Nombre,T2.Name,T2.U_CSS_Razon_Social,Cast(SUM(U_CSS_Tarifa) as integer) AS Total " +
                                "FROM [@CSS_MOVIMIENTO] T0 " +
                                "INNER JOIN [@CSS_CONDUCTOR] T1 " +
                                "ON T0.U_CSS_Conductor=T1.Code " +
                                "INNER JOIN [@CSS_Transportadora] T2 " +
                                "ON T0.U_CSS_Transportadora=T2.Code " +
                                "WHERE T0.U_CSS_Macroguia='" + this.txtMacroguia.Text + "' " +
                                "GROUP BY T0.U_CSS_Fecha,T0.U_CSS_Zona_Origen,T0.U_CSS_Zona_Destino,T0.U_CSS_Macroguia,T0.U_CSS_Vehiculo, " +
                                "T1.U_CSS_Nombre,T2.Name,T2.U_CSS_Razon_Social,T0.U_CSS_Estado,T0.U_CSS_OBSERVACIONES " +

                                "SELECT 'ENTREGA' AS TIPO,T0.U_CSS_Serie,T0.U_CSS_DocEntry,T0.U_CSS_Documento_SAP,T0.U_CSS_Tipo_Documento,T0.U_CSS_Tarifa, " +
                                "T4.Street,T4.U_CSS_Telefono, " +
                                "T1.CardName " +
                                "FROM [@CSS_MOVIMIENTO] T0 " +
                                "INNER JOIN ODLN T1 " +
                                "ON T0.U_CSS_DocEntry=T1.DocEntry " +
                                "INNER JOIN CRD1 T4 " +
                                "ON T1.ShipToCode=T4.Address " +
                                "AND T1.CardCode=T4.CardCode " +
                                "WHERE T0.U_CSS_Macroguia='" + this.txtMacroguia.Text + "' " +
                                "AND T0.U_CSS_Tipo_Documento IN ('D','P') " +
                                "UNION ALL " +
                                "SELECT 'TRASLADO' AS TIPO, T0.U_CSS_Serie,T0.U_CSS_DocEntry,T0.U_CSS_Documento_SAP,T0.U_CSS_Tipo_Documento,T0.U_CSS_Tarifa, " +
                                "T4.Street, T4.U_CSS_Telefono, " +
                                "'' as CardName " +
                                "FROM [@CSS_MOVIMIENTO] T0 " +
                                "INNER JOIN OWTR T1 " +
                                "ON T0.U_CSS_DocEntry=T1.DocEntry " +
                                "INNER JOIN WTR1 T5 " +
                                "ON T5.DocEntry=T1.DocEntry " +
                                "INNER JOIN OITM T6 " +
                                "ON T5.ItemCode=T6.ItemCode " +
                                "INNER JOIN OWHS T4 " +
                                "ON T5.WhsCode=T4.WhsCode " +
                                "WHERE T0.U_CSS_Tipo_Documento='T' " +
                                "AND T0.U_CSS_Macroguia='" + this.txtMacroguia.Text + "' " +
                                "GROUP BY T0.U_CSS_Serie,T0.U_CSS_DocEntry,T0.U_CSS_Documento_SAP,T0.U_CSS_Tipo_Documento,T4.Street, T4.U_CSS_Telefono,T0.U_CSS_Tarifa " +

                                "SELECT T0.U_CSS_Tipo_Documento as U_CSS_Tipo_Doc,cast(T0.U_CSS_Documento_SAP as int) AS DocNum,T0.U_CSS_Serie, T0.U_CSS_DocEntry as U_CSS_DocEntryDetalle," +
                                "cast(T1.Quantity*T2.SWeight1 as numeric(19,2)) AS Cantidad,T1.LineTotal Price, " +
                                "T2.ItemCode,T2.ItemName " +
                                "FROM [@CSS_MOVIMIENTO] T0 " +
                                "INNER JOIN DLN1 T1 " +
                                "ON T0.U_CSS_DocEntry=T1.DocEntry " +
                                "INNER JOIN OITM T2 " +
                                "ON T1.ItemCode=T2.ItemCode " +
                                "WHERE T0.U_CSS_Tipo_Documento IN ('D','P') " +
                                "AND T0.U_CSS_Macroguia='" + this.txtMacroguia.Text + "' " +
                                "UNION ALL " +
                                "SELECT T0.U_CSS_Tipo_Documento as U_CSS_Tipo_Doc,cast(T0.U_CSS_Documento_SAP as int) AS DocNum,T0.U_CSS_Serie,  T0.U_CSS_DocEntry as U_CSS_DocEntryDetalle," +
                                "cast(T1.Quantity*T2.SWeight1 as numeric(19,2)) AS Cantidad,T1.StockPrice, " +
                                "T2.ItemCode,T2.ItemName " +
                                "FROM [@CSS_MOVIMIENTO] T0 " +
                                "INNER JOIN WTR1 T1 " +
                                "ON T0.U_CSS_DocEntry=T1.DocEntry " +
                                "INNER JOIN OITM T2 " +
                                "ON T1.ItemCode=T2.ItemCode " +
                                "WHERE T0.U_CSS_Tipo_Documento='T' " +
                                "AND T0.U_CSS_Macroguia='" + this.txtMacroguia.Text + "' " +

                                "SELECT SUM(CANTIDAD) AS CANTIDAD,SUM(PRECIO) AS PRECIO FROM (SELECT sum(T1.LineTotal) AS PRECIO,sum(T1.Quantity*T2.SWeight1) AS CANTIDAD " +
                                "FROM [@CSS_MOVIMIENTO] T0 " +
                                "INNER JOIN DLN1 T1 " +
                                "ON T0.U_CSS_DocEntry=T1.DocEntry " +
                                "INNER JOIN OITM T2 " +
                                "ON T1.ItemCode=T2.ItemCode " +
                                "AND T0.U_CSS_Macroguia='" + this.txtMacroguia.Text + "' " +
                                "AND T0.U_CSS_Tipo_Documento IN('P','D') " +
                                "UNION ALL " +
                                "SELECT sum(T1.StockPrice) AS PRECIO,sum(T1.Quantity*T2.SWeight1) AS CANTIDAD " +
                                "FROM [@CSS_MOVIMIENTO] T0 " +
                                "INNER JOIN WTR1 T1 " +
                                "ON T0.U_CSS_DocEntry=T1.DocEntry " +
                                "INNER JOIN OITM T2 " +
                                "ON T1.ItemCode=T2.ItemCode " +
                                "AND T0.U_CSS_Macroguia='" + this.txtMacroguia.Text + "' " +
                                "AND T0.U_CSS_Tipo_Documento IN('T')) TABLA";

                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
                using (ClaseDatos.SqlConn)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(
                        miConsulta, ClaseDatos.SqlConn);
                    adapter.Fill(miDataSetFletes);
                }
                miReporte.SetDataSource(miDataSetFletes.Tables[2]);

                ReportDocument subReporte = new ReportDocument();
                SubreportObject miSubreportObject;
                ReportDocument subReporteMacroguia = new ReportDocument();
                SubreportObject subReporteDatosMacroguiaObject;
                ReportDocument subReporteMacroguiaDetalles = new ReportDocument();
                SubreportObject subReporteTotales;
                ReportDocument subReporteMacroguiaTotales = new ReportDocument();


                miSubreportObject = miReporte.ReportDefinition.ReportObjects["EncabezadoCompania"] as SubreportObject;
                subReporte = miReporte.OpenSubreport(miSubreportObject.SubreportName);
                subReporte.SetDataSource(miDataSetFletes.Tables[0]);

                subReporteDatosMacroguiaObject = miReporte.ReportDefinition.ReportObjects["DatosMacroguia"] as SubreportObject;
                subReporteMacroguia = miReporte.OpenSubreport(subReporteDatosMacroguiaObject.SubreportName);
                subReporteMacroguia.SetDataSource(miDataSetFletes.Tables[1]);

                miSubreportObject = miReporte.ReportDefinition.ReportObjects["Lineas"] as SubreportObject;
                subReporte = miReporte.OpenSubreport(miSubreportObject.SubreportName);
                subReporte.SetDataSource(miDataSetFletes.Tables[3]);

                subReporteTotales = miReporte.ReportDefinition.ReportObjects["Totales"] as SubreportObject;
                subReporteMacroguiaTotales = miReporte.OpenSubreport(subReporteTotales.SubreportName);
                subReporteMacroguiaTotales.SetDataSource(miDataSetFletes.Tables[4]);

                frmReporteFletes miFormularioReporte = new frmReporteFletes(miReporte);
                miFormularioReporte.Show();
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al generar el reporte: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMacroguia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnGrabar.Enabled = false;
                int miMacroguia = 0;
                double miPesoTotal = 0;
                double miValorTotal = 0;
                int misDocumentosSeleccionados = 0;
                try
                {
                    this.dgvResultados.Rows.Clear();
                    this.dgvTotales.Rows.Clear();
                    if (this.txtMacroguia.Text.ToString().Length > 0)
                    {
                        Int32.TryParse(this.txtMacroguia.Text.ToString(), out miMacroguia);
                        if (miMacroguia != 0)
                        {
                            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                            string miSql = "SELECT T0.U_CSS_Zona_Destino,T0.U_CSS_Tipo_Documento,T0.U_CSS_Cliente,T0.U_CSS_Representante,T0.U_CSS_Documento_SAP,T0.U_CSS_Serie, T0.U_CSS_Peso,T0.U_CSS_Tarifa,T0.U_CSS_Estado " +
                                           "FROM [@CSS_MOVIMIENTO] T0 " +
                                           "WHERE U_CSS_Macroguia='" + this.txtMacroguia.Text + "'";
                            IDataReader misDatos = ClaseDatos.procesaDataReader(miSql);
                            bool miEstadoCarga = false;
                            string miEstadoMacroguia = "";
                            while (misDatos.Read())
                            {
                                miEstadoCarga = true;
                                object[] misValores = new object[10];
                                misValores[0] = misDatos.GetValue(0);
                                misValores[1] = misDatos.GetValue(1);
                                misValores[2] = misDatos.GetValue(2);
                                misValores[3] = misDatos.GetValue(3);
                                misValores[4] = misDatos.GetValue(4);
                                misValores[5] = misDatos.GetValue(5);
                                misValores[6] = Convert.ToDouble(misDatos.GetValue(6));
                                misValores[7] = Convert.ToDouble(misDatos.GetValue(7));
                                misValores[8] = true;
                                this.dgvResultados.Rows.Add(misValores);
                                misDocumentosSeleccionados++;
                                miPesoTotal += Convert.ToDouble(misDatos.GetValue(6));
                                miValorTotal += Convert.ToDouble(misDatos.GetValue(7));
                                miEstadoMacroguia = misDatos.GetString(8);
                            }
                            if (!miEstadoCarga)
                            {
                                MessageBox.Show("No existe el Documento de Transporte", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.btnAnular.Enabled = false;

                            }
                            else
                            {
                                if (miEstadoMacroguia.Equals("Activo"))
                                {
                                    this.btnAnular.Enabled = true;
                                }
                                else
                                {
                                    this.btnAnular.Enabled = false;
                                    MessageBox.Show("El documento de transporte esta anulado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                this.btnImprimir.Enabled = true;
                                this.dgvTotales.Rows.Clear();
                                this.dgvTotales.Rows.Add("PESO TOTAL", miPesoTotal);
                                this.dgvTotales.Rows.Add("VALOR TOTAL", miValorTotal);
                                this.dgvTotales.Rows.Add("SELECCIONADOS", misDocumentosSeleccionados);
                            }
                            ClaseDatos.SqlUnConnex();
                        }
                        else
                        {
                            MessageBox.Show("El valor digitado debe ser un número entero", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe digitar el número del documento de transporte", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al buscar el documento de transporte: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (this.dgvResultados.Rows.Count == 0)
            {
                MessageBox.Show("No existe el documento de transporte", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Documents miEntregaVenta = (Documents)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                StockTransfer miTransferencia = (StockTransfer)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                UserTable miMovimiento = ClaseDatos.objCompany.UserTables.Item("CSS_MOVIMIENTO");
                try
                {
                    string miSql = "SELECT Code,U_CSS_DocEntry,U_CSS_Tipo_Documento " +
                                 "FROM [@CSS_MOVIMIENTO] " +
                                 "WHERE U_CSS_Macroguia='" + this.txtMacroguia.Text.ToString() + "'";
                    ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
                    IDataReader misDatos = ClaseDatos.procesaDataReader(miSql);
                    ClaseDatos.objCompany.StartTransaction();
                    while (misDatos.Read())
                    {
                        miMovimiento.GetByKey(misDatos.GetString(0));
                        miMovimiento.UserFields.Fields.Item("U_CSS_Estado").Value = "Anulado";
                        miMovimiento.Update();
                        if (misDatos.GetString(2).Equals("T"))
                        {
                            miTransferencia.GetByKey(Convert.ToInt32(misDatos.GetValue(1)));
                            miTransferencia.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = "0";
                            miTransferencia.UserFields.Fields.Item("U_CSS_Vehiculo").Value = "0";
                            miTransferencia.UserFields.Fields.Item("U_CSS_Transportadora").Value = "0";
                            miTransferencia.UserFields.Fields.Item("U_CSS_Conductor").Value = "0";
                            miTransferencia.UserFields.Fields.Item("U_CSS_Propietario").Value = "0";
                            miTransferencia.Update();
                        }
                        else
                        {
                            miEntregaVenta.GetByKey(Convert.ToInt32(misDatos.GetValue(1)));
                            miEntregaVenta.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = "0";
                            miEntregaVenta.UserFields.Fields.Item("U_CSS_Vehiculo").Value = "0";
                            miEntregaVenta.UserFields.Fields.Item("U_CSS_Transportadora").Value = "0";
                            miEntregaVenta.UserFields.Fields.Item("U_CSS_Conductor").Value = "0";
                            miEntregaVenta.UserFields.Fields.Item("U_CSS_Propietario").Value = "0";
                            miEntregaVenta.Update();
                        }
                    }
                    ClaseDatos.SqlUnConnex();
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    MessageBox.Show("El documento de Transporte fue anulado con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception miExcepcion)
                {
                    MessageBox.Show("Error al anular el documento de transporte: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(miEntregaVenta);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(miTransferencia);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(miMovimiento);
                }
            }
        }

        private void dgvResultados_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvResultados.IsCurrentCellDirty)
            {
                dgvResultados.CommitEdit(DataGridViewDataErrorContexts.Commit);

            }
        }

        private void dgvResultados_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double miPesoTotal = 0;
            double miValorTotal = 0;
            int misDocumentosSeleccionados = 0;
            try
            {
                if (e.ColumnIndex == 8)
                {
                    for (int miContador = 0; miContador < this.dgvResultados.Rows.Count; miContador++)
                    {
                        if (Convert.ToBoolean(this.dgvResultados.Rows[miContador].Cells[8].Value.ToString()))
                        {
                            misDocumentosSeleccionados++;
                            miPesoTotal += Convert.ToDouble(this.dgvResultados.Rows[miContador].Cells[6].Value.ToString());
                            miValorTotal += Convert.ToDouble(this.dgvResultados.Rows[miContador].Cells[7].Value.ToString());
                        }
                    }
                    if (this.dgvResultados.Rows.Count > 0)
                    {
                        this.dgvTotales.Rows.Clear();
                        this.dgvTotales.Rows.Add("PESO TOTAL", miPesoTotal);
                        this.dgvTotales.Rows.Add("VALOR TOTAL", miValorTotal);
                        this.dgvTotales.Rows.Add("SELECCIONADOS", misDocumentosSeleccionados);
                    }
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al calcular el total: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtNumOrden_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_Comentario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Comentario_MouseClick(object sender, MouseEventArgs e)
        {
            txt_Comentario.Text = "";
            this.txt_Comentario.ForeColor = System.Drawing.SystemColors.WindowText;
        }
    }
}
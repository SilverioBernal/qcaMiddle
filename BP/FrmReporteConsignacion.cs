using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entities;
using BP.AppData;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace BP
{
    public partial class FrmReporteConsignacion : Form
    {
        ClsDataInventario itemsData = new ClsDataInventario();
        ClsDataBusinessPartner customerData = new ClsDataBusinessPartner();
        ClsCommonData commonData = new ClsCommonData();
        ClsReporteConsignacion oReporte = new ClsReporteConsignacion();

        List<GenericBusinessPartner> clientes = new List<GenericBusinessPartner>();
        List<Articulo> items = new List<Articulo>();
        List<Territory> territorios = new List<Territory>();

        public FrmReporteConsignacion()
        {
            InitializeComponent();
        }

        private void FrmReporteConsignacion_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dpFrom.Value = DateTime.Now;
            dpTo.Value = DateTime.Now;

            statusLabel.Text = "Buscando clientes";
            clientes.Add(new GenericBusinessPartner() { cardCode = string.Empty, cardName = "Seleccione un cliente" });

            clientes.AddRange(customerData.GetList(CardType.Customer).OrderBy(x => x.cardName).ToList());

            cbCustomer.DataSource = clientes;
            cbCustomer.ValueMember = "cardCode";
            cbCustomer.DisplayMember = "cardName";


            statusLabel.Text = "Buscando territorios";
            territorios.Add(new Territory() { territryID= string.Empty, descript = "Seleccione un territorio" });

            territorios.AddRange(commonData.GetTerritories());

            cbTerritory.DataSource = territorios;
            cbTerritory.ValueMember = "territryID";
            cbTerritory.DisplayMember = "descript";

            statusLabel.Text = "Buscando productos";
            items.Add(new Articulo() { ItemCode = string.Empty, ItemName = "Seleccione un articulo" });
            items.AddRange(itemsData.ListarArticulos());
            
            cbProduct.DataSource = items;
            cbProduct.ValueMember = "ItemCode";
            cbProduct.DisplayMember = "ItemName";

            statusLabel.Text = "Listo";
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            DsConsignacion dsReport = buildReport();

            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string rutaRpt = string.Format(@"{0}\RptConsignacion.rpt", appPath);

            ReportDocument rpt;

            rpt = new ReportDocument();
            rpt.Load(rutaRpt);
            rpt.SetDataSource(dsReport.Tables[0]);

            crystalReportViewer1.ReportSource = rpt;
        }        

        public DsConsignacion buildReport()
        {
            DateTime from, to;
            string cardCode, itemCode, territoryCode, docStatus;

            from = dpFrom.Value;
            to = dpTo.Value;
            cardCode = cbCustomer.SelectedValue.ToString();
            itemCode = cbProduct.SelectedValue.ToString();
            territoryCode = cbTerritory.SelectedValue.ToString();

            if (rbOpen.Checked)
                docStatus = "O";
            else if (rbClosed.Checked)
                docStatus = "O";
            else
                docStatus = "";

            List<ReporteConsignacion> reporte = oReporte.GetReporteConsignacion(dpFrom.Value, dpTo.Value, cardCode, itemCode, territoryCode, docStatus);

            DsConsignacion dsReport = new DsConsignacion();

            foreach (ReporteConsignacion item in reporte)
            {
                DsConsignacion.DtReporteConsignacionRow row = dsReport.DtReporteConsignacion.NewDtReporteConsignacionRow();

                row.orderType = item.orderType;
                row.orderEntry = item.orderEntry.ToString();
                row.orderLine = item.orderLine.ToString();
                row.DeliveryType = item.DeliveryType;
                row.DeliveryEntry = item.DeliveryEntry.ToString();
                row.DeliveryLine = item.DeliveryLine.ToString();
                row.ReturnType = item.ReturnType;
                row.ReturnEntry = item.ReturnEntry.ToString();
                row.ReturnLine = item.ReturnLine.ToString();
                row.InvoiceType = item.InvoiceType;
                row.InvoiceEntry = item.InvoiceEntry.ToString();
                row.InvoiceLine = item.InvoiceLine.ToString();
                row.CreditNoteType = item.CreditNoteType;
                row.CreditNoteEntry = item.CreditNoteEntry.ToString();
                row.CreditNoteLine = item.CreditNoteLine.ToString();
                row.Order = item.Order;
                row.Delivery = item.Delivery;
                row.Return = item.Return;
                row.Invoice = item.Invoice;
                row.CreditNote = item.CreditNote;
                row.SlpCode = item.SlpCode;
                row.SlpName = item.SlpName;
                row.DocDate = (DateTime)item.DocDate;
                row.CardCode = item.CardCode;
                row.CardName = item.CardName;
                row.ItemCode = item.ItemCode;
                row.ItemName = item.ItemName;
                row.OrderQuantity = item.OrderQuantity;
                row.DeliveryQuantity = item.DeliveryQuantity;
                row.ReturnQuantity = item.ReturnQuantity;
                row.InvoiceQuantity = item.InvoiceQuantity;
                row.CreditNoteQuantity = item.CreditNoteQuantity;
                row.DocStatus = item.DocStatus;
                row.TerritoryCode = item.TerritoryCode;
                row.TerritoryName = item.TerritoryName;
                row.PendingQuantity = item.PendingQuantity.ToString();
                row.PendingCost = item.PendingCost.ToString();
                row.avgPrice = item.AvgPrice;
                row.itemPrice = item.itemPrice;
                row.ConsigmentDays = item.ConsigmentDays.ToString();

                dsReport.DtReporteConsignacion.AddDtReporteConsignacionRow(row);
            }

            return dsReport;
        }
    }
}

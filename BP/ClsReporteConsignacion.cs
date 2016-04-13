using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.IO;
using System.Configuration;
using System.Data;

namespace BP
{
    public class ClsReporteConsignacion
    {
        #region Atributos
        /// <summary>
        /// Lector
        /// </summary>
        private IDataReader reader;
        #endregion

        #region Metodos
        public List<ReporteConsignacion> GetReporteConsignacion(DateTime from, DateTime to)
        {
            List<ReporteConsignacion> reporte = new List<ReporteConsignacion>();

            string cPath = System.Windows.Forms.Application.StartupPath + ConfigurationManager.AppSettings["QueryConsignacion"].ToString();
            StreamReader oReader = new StreamReader(cPath);
            string cQueryConsignacion = oReader.ReadToEnd();

            string cmd = string.Format(cQueryConsignacion, from.ToString("yyyy-MM-dd"), to.ToString("yyyy-MM-dd"));

            using (this.reader = ClaseDatos.procesaDataReader(cmd.ToString()))
            {
                while (this.reader.Read())
                {
                    ReporteConsignacion linea = new ReporteConsignacion();

                    linea.orderType = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    linea.orderEntry = this.reader.IsDBNull(1) ? -1 : int.Parse(this.reader.GetValue(1).ToString());
                    linea.orderLine = this.reader.IsDBNull(2) ? -1 : int.Parse(this.reader.GetValue(2).ToString());
                    linea.DeliveryType = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    linea.DeliveryEntry = this.reader.IsDBNull(4) ? -1 : int.Parse(this.reader.GetValue(4).ToString());
                    linea.DeliveryLine = this.reader.IsDBNull(5) ? -1 : int.Parse(this.reader.GetValue(5).ToString());
                    linea.ReturnType = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    linea.ReturnEntry = this.reader.IsDBNull(7) ? -1 : int.Parse(this.reader.GetValue(7).ToString());
                    linea.ReturnLine = this.reader.IsDBNull(8) ? -1 : int.Parse(this.reader.GetValue(8).ToString());
                    linea.InvoiceType = this.reader.IsDBNull(9) ? "" : this.reader.GetValue(9).ToString();
                    linea.InvoiceEntry = this.reader.IsDBNull(10) ? -1 : int.Parse(this.reader.GetValue(10).ToString());
                    linea.InvoiceLine = this.reader.IsDBNull(11) ? -1 : int.Parse(this.reader.GetValue(11).ToString());
                    linea.CreditNoteType = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    linea.CreditNoteEntry = this.reader.IsDBNull(13) ? -1 : int.Parse(this.reader.GetValue(13).ToString());
                    linea.CreditNoteLine = this.reader.IsDBNull(14) ? -1 : int.Parse(this.reader.GetValue(14).ToString());
                    linea.Order = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                    linea.Delivery = this.reader.IsDBNull(16) ? "" : this.reader.GetValue(16).ToString();
                    linea.Return = this.reader.IsDBNull(17) ? "" : this.reader.GetValue(17).ToString();
                    linea.Invoice = this.reader.IsDBNull(18) ? "" : this.reader.GetValue(18).ToString();
                    linea.CreditNote = this.reader.IsDBNull(19) ? "" : this.reader.GetValue(19).ToString();
                    linea.SlpCode = this.reader.IsDBNull(20) ? "" : this.reader.GetValue(20).ToString();
                    linea.SlpName = this.reader.IsDBNull(21) ? "" : this.reader.GetValue(21).ToString();
                    linea.DocDate = DateTime.Parse(this.reader.IsDBNull(22) ? null : this.reader.GetValue(22).ToString());
                    linea.CardCode = this.reader.IsDBNull(23) ? "" : this.reader.GetValue(23).ToString();
                    linea.CardName = this.reader.IsDBNull(24) ? "" : this.reader.GetValue(24).ToString();
                    linea.ItemCode = this.reader.IsDBNull(25) ? "" : this.reader.GetValue(25).ToString();
                    linea.ItemName = this.reader.IsDBNull(26) ? "" : this.reader.GetValue(26).ToString();
                    linea.OrderQuantity = this.reader.IsDBNull(27) ? 0 : double.Parse(this.reader.GetValue(27).ToString());
                    linea.DeliveryQuantity = this.reader.IsDBNull(28) ? 0 : double.Parse(this.reader.GetValue(28).ToString());
                    linea.ReturnQuantity = this.reader.IsDBNull(29) ? 0 : double.Parse(this.reader.GetValue(29).ToString());
                    linea.InvoiceQuantity = this.reader.IsDBNull(30) ? 0 : double.Parse(this.reader.GetValue(30).ToString());
                    linea.CreditNoteQuantity = this.reader.IsDBNull(31) ? 0 : double.Parse(this.reader.GetValue(31).ToString());
                    linea.DocStatus = this.reader.IsDBNull(32) ? "" : this.reader.GetValue(32).ToString();
                    linea.TerritoryCode = this.reader.IsDBNull(33) ? "" : this.reader.GetValue(33).ToString();
                    linea.TerritoryName = this.reader.IsDBNull(34) ? "" : this.reader.GetValue(34).ToString();
                    linea.AvgPrice = this.reader.IsDBNull(35) ? 0 : double.Parse(this.reader.GetValue(35).ToString());
                    linea.itemPrice = this.reader.IsDBNull(36) ? 0 : double.Parse(this.reader.GetValue(36).ToString());
                    linea.PendingQuantity = 0;
                    linea.ConsigmentDays = 0;
                    linea.PendingCost = 0;                    
                    reporte.Add(linea);
                }
            }

            return reporte;
        }

        public List<ReporteConsignacion> GetReporteConsignacion(DateTime from, DateTime to, string cardCode, string itemCode, string territoryCode, string docStatus)
        {
            List<ReporteConsignacion> reporte = new List<ReporteConsignacion>();
            List<ReporteConsignacion> reporteDef = new List<ReporteConsignacion>();

            reporte = GetReporteConsignacion(from, to);

            if (!string.IsNullOrEmpty(cardCode))
                reporte = reporte.Where(x => x.CardCode == cardCode).ToList();

            if (!string.IsNullOrEmpty(itemCode))
                reporte = reporte.Where(x => x.ItemCode == itemCode).ToList();

            if (!string.IsNullOrEmpty(territoryCode))
                reporte = reporte.Where(x => x.TerritoryCode == territoryCode).ToList();

            if (!string.IsNullOrEmpty(docStatus))
                reporte = reporte.Where(x => x.DocStatus == docStatus).ToList();

            double ConsigmentDays = 0;
            DateTime deliveryDate = DateTime.Now;

            foreach (ReporteConsignacion item in reporte)
            {                
                if (string.IsNullOrEmpty(item.Return) && string.IsNullOrEmpty(item.Invoice) && string.IsNullOrEmpty(item.CreditNote))
                {
                    item.PendingQuantity = item.DeliveryQuantity;
                    item.PendingCost = item.DeliveryQuantity * item.AvgPrice;
                    deliveryDate = (DateTime)item.DocDate;

                    if (item.DocStatus == "O")
                        ConsigmentDays = (DateTime.Now - (DateTime)item.DocDate).TotalDays;

                }
                else
                {
                    double deliveriedQty = 0;
                    double returnedQty = 0;
                    double invoicedQty = 0;
                    double creditedNoteQty = 0;

                    deliveriedQty = reporte
                        .Where(x =>
                            x.DeliveryEntry.Equals(item.DeliveryEntry)
                            && x.DeliveryLine.Equals(item.DeliveryLine)                            
                        )
                        .Select(X => X.DeliveryQuantity).FirstOrDefault();

                    returnedQty = reporte
                        .Where(x =>
                            x.DeliveryEntry.Equals(item.DeliveryEntry)
                            && x.DeliveryLine.Equals(item.DeliveryLine)
                            && x.ReturnEntry > 0
                            && x.ReturnEntry <= item.ReturnEntry 
                        )
                        .Select(X => X.ReturnQuantity).Sum();

                    invoicedQty = reporte
                        .Where(x =>
                            x.DeliveryEntry.Equals(item.DeliveryEntry)
                            && x.DeliveryLine.Equals(item.DeliveryLine)
                            && x.InvoiceEntry > 0
                            && x.InvoiceEntry <= item.InvoiceEntry
                            && x.CreditNoteEntry < 0
                        )
                        .Select(X => X.InvoiceQuantity).Sum();

                    creditedNoteQty = reporte
                        .Where(x =>
                            x.DeliveryEntry.Equals(item.DeliveryEntry)
                            && x.DeliveryLine.Equals(item.DeliveryLine)
                            && x.CreditNoteEntry > 0
                            && x.CreditNoteEntry <= item.CreditNoteEntry
                        )
                        .Select(X => X.CreditNoteQuantity).Sum();

                    item.PendingQuantity = deliveriedQty - (returnedQty + invoicedQty );
                    item.PendingCost = (deliveriedQty - (returnedQty + invoicedQty)) * item.AvgPrice; 
                    
                    
                }
                try
                {
                    item.ConsigmentDays = (double)((int)ConsigmentDays);
                }
                catch (Exception)
                {
                    item.ConsigmentDays = -1; 
                }
                
                reporteDef.Add(item);
            }
           
            return reporteDef;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class ReporteConsignacion
    {
        public string orderType { get; set; }
        public int orderEntry { get; set; }
        public int orderLine { get; set; }
        public string DeliveryType { get; set; }
        public int DeliveryEntry { get; set; }
        public int DeliveryLine { get; set; }
        public string ReturnType { get; set; }
        public int ReturnEntry { get; set; }
        public int ReturnLine { get; set; }
        public string InvoiceType { get; set; }
        public int InvoiceEntry { get; set; }
        public int InvoiceLine { get; set; }
        public string CreditNoteType { get; set; }
        public int CreditNoteEntry { get; set; }
        public int CreditNoteLine { get; set; }
        public string Order { get; set; }
        public string Delivery { get; set; }
        public string Return { get; set; }
        public string Invoice { get; set; }
        public string CreditNote { get; set; }
        public string SlpCode { get; set; }
        public string SlpName { get; set; }
        public DateTime? DocDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double OrderQuantity { get; set; }
        public double DeliveryQuantity { get; set; }
        public double ReturnQuantity { get; set; }
        public double InvoiceQuantity { get; set; }
        public double CreditNoteQuantity { get; set; }
        public string DocStatus { get; set; }
        public string TerritoryCode { get; set; }
        public string TerritoryName { get; set; }
        public double AvgPrice { get; set; }
        public double PendingQuantity { get; set; }
        public double PendingCost { get; set; }
        public double ConsigmentDays { get; set; }
        public double itemPrice { get; set; }
        public double invItemPrice { get; set; }
    }
}

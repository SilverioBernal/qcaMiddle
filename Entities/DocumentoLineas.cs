using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class DocumentoLineas
    {
        #region Atributos
        /// <summary>
        /// Número Interno del documento
        /// </summary>
        
        public int DocEntry { set; get; }
        /// <summary>
        /// Código del ítem
        /// </summary>
        
        public string ItemCode { set; get; }
        /// <summary>
        /// Cantidad
        /// </summary>
        
        public double Quantity { set; get; }

        public double Price{ get; set; }
        /// <summary>
        /// Código del almacén
        /// </summary>
        
        public string WhsCode { set; get; }
        /// <summary>
        /// Nombre del almacén
        /// </summary>
        
        public string WhsName { set; get; }
        /// <summary>
        /// Descripción del artículo
        /// </summary>
        
        public string Dscription { set; get; }
        /// <summary>
        /// Estado de la línea (Abierto (O), Cerrado (C))
        /// </summary>
        
        public string LineStatus { set; get; }
        /// <summary>
        /// Número Interno referenciado para la creación con documento base
        /// </summary>
        
        public int BaseEntry { set; get; }
        /// <summary>
        /// Número de la línea del documento base
        /// </summary>
        
        public int BaseLine { set; get; }
        /// <summary>
        /// Tipo de documento referenciado (202 Órdenes de producción, 17 Pedido en ventas, 22 Orden de compra, 18 Factura de proveedores)
        /// </summary>
        
        public int BaseType { set; get; }
        /// <summary>
        /// Cantidad pendiente por recibir
        /// </summary>
        
        public double OpenCreQty { set; get; }
        /// <summary>
        /// Cantidad pendiente por recibir
        /// </summary>
        
        public double OpenQty { set; get; }
        /// <summary>
        /// Unidad de medida de ingreso
        /// </summary>
        
        public string unitMsr { set; get; }
        /// <summary>
        /// Cantidad de unidades por unidad de medida
        /// </summary>
        
        public double NumPerMsr { set; get; }
        /// <summary>
        /// Señala de que tipo de documento se copio la linea
        /// </summary>
        
        public int TargetType { set; get; }
        /// <summary>
        /// Refiere  al DocEntry del campo destino en la tabla base
        /// </summary>
        
        public int TrgetEntry { set; get; }
        /// <summary>
        /// Numero de linea
        /// </summary>
        
        public int LineNum { set; get; }
        /// <summary>
        /// Potencia en milicurios
        /// </summary>
        
        public int U_Actividad_mCi { set; get; }
        /// <summary>
        /// Lista de Lotes para el artículo en documento de Marketing
        /// </summary>
        
        public List<Lote> BatchNumbers { set; get; }
        /// <summary>
        /// Lista de series para el artículo en documento de Marketing
        /// </summary>
        
        public List<SerialNumber> SerialNumbers { set; get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentoLineas()
        {
            this.DocEntry = 0;
            this.ItemCode = String.Empty;
            this.Quantity = 0;
            this.WhsCode = String.Empty;
            this.WhsName = String.Empty;
            this.Dscription = String.Empty;
            this.LineStatus = String.Empty;
            this.BaseEntry = 0;
            this.BaseLine = 0;
            this.BaseType = 0;
            this.OpenCreQty = 0;
            this.OpenQty = 0;
            this.unitMsr = String.Empty;
            this.NumPerMsr = 0;
            this.TargetType = 0;
            this.TrgetEntry = 0;
            this.LineNum = 0;
            this.BatchNumbers = new List<Lote>();
            this.SerialNumbers = new List<SerialNumber>();
        }
        #endregion
    }
}

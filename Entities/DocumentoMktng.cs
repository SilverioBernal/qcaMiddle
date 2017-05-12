using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class DocumentoMktng
    {
        #region Atributos
        /// <summary>
        /// Numero Interno del documento
        /// </summary>
        
        public int DocEntry { set; get; }
        /// <summary>
        /// Número del documento de acuerdo a la serie
        /// </summary>
        
        public int DocNum { set; get; }
        /// <summary>
        /// Código del proveedor / cliente
        /// </summary>

        public int Series { get; set; }

        public string CardCode { set; get; }
        /// <summary>
        /// Nombre del proveedor / cliente
        /// </summary>
        
        public string CardName { set; get; }
        /// <summary>
        /// Número del documento referencia
        /// </summary>
        
        public string NumAtCard { set; get; }
        /// <summary>
        /// Fecha del documento
        /// </summary>
        
        public DateTime DocDate { set; get; }
        /// <summary>
        /// Estado del documento (Cerrrado o Abierto)
        /// </summary>
        
        public string DocStatus { set; get; }
        /// <summary>
        /// Tipo de documento (Item o Servicio)
        /// </summary>
        
        public string Doctype { set; get; }
        /// <summary>
        /// Estado del inventario
        /// </summary>
        
        public string invntsttus { set; get; }
        /// <summary>
        /// El documento esta cancelado (true = si)
        /// </summary>
        
        public bool Canceled { set; get; }
        /// <summary>
        /// Fecha de la ultima actualizacion
        /// </summary>
        
        public DateTime UpdateDate { set; get; }
        /// <summary>
        /// Líneas de Documentos
        /// </summary>
        
        public List<DocumentoLineas> lineas { set; get; }
        /// <summary>
        /// Tipo de objeto (13- Facturas, 22 - órdenes de compra, 17 - Órdenes de venta)
        /// </summary>
        
        public string Objtype { set; get; }

        public string Comments { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentoMktng()
        {
            this.Canceled = false;
            this.CardCode = "";
            this.CardName = "";
            this.DocDate = DateTime.Now;
            this.DocEntry = 0;
            this.DocNum = 0;
            this.DocStatus = "";
            this.lineas = new List<DocumentoLineas>();
            this.NumAtCard = "";
            this.invntsttus = "";
            this.Doctype = "";
            this.Objtype = "";
        }
        #endregion
    }
}

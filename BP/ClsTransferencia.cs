using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP
{
    public class ClsTransferencia
    {
        /// <summary>
        /// Número Interno del documento
        /// </summary>        
        public int DocEntry { set; get; }

        /// <summary>
        /// Fecha del documento
        /// </summary>        
        public DateTime DocDate { set; get; }
        
        /// <summary>
        /// Fecha de contabilización
        /// </summary>        
        public DateTime TaxDate { set; get; }

        /// <summary>
        /// Código del almacén
        /// </summary>        
        public string WhsCode { set; get; }
        /// <summary>
        /// Líneas de Documentos
        /// </summary>
        
        public List<ClsTransferenciaLinea> lineas { set; get; }
    }
}

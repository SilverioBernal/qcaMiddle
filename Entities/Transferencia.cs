using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Transferencia
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

        public string Comments { get; set; }

        /// <summary>
        /// Líneas de Documentos
        /// </summary>
        public List<TransferenciaLinea> lineas { set; get; }

        public Transferencia()
        {
            lineas = new List<TransferenciaLinea>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class SerialNumber
    {
        /// <summary>
        /// Número de Serie del fabricante
        /// </summary>
        public string MnfSerial { set; get; }

        /// <summary>
        /// Número de Serie
        /// </summary>
        public string DisNumber { set; get; }

        /// <summary>
        /// Código que identifica el lote
        /// </summary>
        public string LotNumber { set; get; }

        /// <summary>
        /// Fecha de expiración para el ítem
        /// </summary>
        public DateTime ExpDate { set; get; }

        /// <summary>
        /// Fecha de fabricación del fabricante para el lote
        /// </summary>
        public DateTime MnfDate { set; get; }

        /// <summary>
        /// Código del artículo en SAP Business One
        /// </summary>
        public string ItemCode { set; get; }

        /// <summary>
        /// Estado del serial, disponible o no disponible
        /// </summary>
        public string Status { set; get; }
    }
}

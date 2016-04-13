using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Lote
    {
        /// <summary>
        /// Código que identifica el lote
        /// </summary>
        public string DistNumber { set; get; }

        /// <summary>
        /// Cantidad del lote
        /// </summary>
        public double Quantity { set; get; }

        /// <summary>
        /// Fecha de Vencimiento
        /// </summary>
        public string ExpDate { set; get; }

        /// <summary>
        /// Attributo 1
        /// </summary>
        public string MnfSerial { set; get; }

        /// <summary>
        /// Attributo 2
        /// </summary>
        public string LotNumber { set; get; }

        /// <summary>
        /// Fecha de fabricacion
        /// </summary>
        public string MfnDate { set; get; }
    }
}

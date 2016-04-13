using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class LoteoArticuloLinea
    {
        /// <summary>
        /// Código del ítem
        /// </summary>        
        public string itemCode { set; get; }

        /// <summary>
        /// Código del almacén
        /// </summary>       
        public string toWhsCode { set; get; }

        /// <summary>
        /// Lista de Lotes para el artículo en documento de Marketing
        /// </summary>
        public string originalBatchNumber { set; get; }

        public string finallyBatchNumber { set; get; }

        public string iqNumber { set; get; }

        public double quantity { set; get; }

        public string MnfDate { get; set; }

        public string ExpDate { get; set; }

        public double AvgPrice { get; set; }
    }
}

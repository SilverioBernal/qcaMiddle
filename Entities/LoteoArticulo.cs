using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class LoteoArticulo
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
        public string FromWhsCode { set; get; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }

        public int inventoryExitNumber { get; set; }

        public int inventoryEntryNumber { get; set; }

        public List<LoteoArticuloLinea> lineas { get; set; }

        public LoteoArticulo()
        {
            lineas = new List<LoteoArticuloLinea>();
        }
    }
}

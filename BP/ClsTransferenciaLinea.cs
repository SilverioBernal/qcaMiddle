using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP
{
    public class ClsTransferenciaLinea
    {
        /// <summary>
        /// Código del ítem
        /// </summary>        
        public string ItemCode { set; get; }

        /// <summary>
        /// Código del almacén
        /// </summary>        
        public string WhsCode { set; get; }
        
        /// <summary>
        /// Lista de Lotes para el artículo en documento de Marketing
        /// </summary>        
        //public List<Lote> BatchNumbers { set; get; }
        
        /// <summary>
        /// Lista de series para el artículo en documento de Marketing
        /// </summary>        
        //public List<SerialNumber> SerialNumbers { set; get; }
        
        /// <summary>
        /// Cantidad
        /// </summary>        
        public double Quantity { set; get; }
    }
}

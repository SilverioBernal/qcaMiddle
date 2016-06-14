using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Flete
    {
        public string destino { get; set; }
        public string tipo { get; set; }
        public string cliente { get; set; }
        public string vendedor { get; set; }
        public string orden { get; set; }
        public string serie { get; set; }
        public double peso { get; set; }
        public double tarifa { get; set; }              
        public string docEntry { get; set; }
        public string origen { get; set; }
        public string estado { get; set; }
        public bool seleccionado { get; set; }
    }
}

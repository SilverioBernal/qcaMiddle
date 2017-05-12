using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ORK_ENV_DEV_MASTER
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime U_fecha { get; set; }
        public string U_objType { get; set; }
        public string U_usuario { get; set; }
        public string U_socioNegocio { get; set; }

        public List<ORK_ENV_DEV_DETALLE> detalleEnvaseDevolutivo { get; set; }


        public ORK_ENV_DEV_MASTER()
        {
            detalleEnvaseDevolutivo = new List<ORK_ENV_DEV_DETALLE>();
        }
    }
}

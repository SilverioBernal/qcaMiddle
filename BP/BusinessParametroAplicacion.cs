using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP
{
    public static class BusinessParametroAplicacion
    {
        public static string GetParamValue(string parameterName)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT u_value FROM [@ORKPARAMETROSMIDDLE] ");
            miSentencia.Append(string.Format("where name = '{0}'", parameterName));

            return ClaseDatos.scalarStringSql(miSentencia.ToString());
        }
    }
}

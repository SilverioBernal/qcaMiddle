using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace Negocio
{
    public class EnvaseDevolutivo
    {
        /// <summary>
        /// Creación de movimiento de envase
        /// </summary>
        /// <param name="Maestro">entidad</param>

        /// Atributos de conexión a la base de datos
        /// </summary>
        private Database baseDatos;
        private IDataReader reader;
        /// <returns>Código del maestro</returns>
        public int CrearDocumento(ORK_ENV_DEV_MASTER Documento) { 

        }

        public ORK_ENV_DEV_MASTER  ConsultarDocumento(int numeroDocumento)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT  T0.Code, T0.Name, T0.U_fecha, T0.U_objType, T0.U_usuario, T0.U_socioNegocio, ");
            miSentencia.Append("T1.code As CodDetalle, T1.name as NameDetalle, T1.U_idMaestro, T1.U_tipoMovimiento, T1.U_itemcode, T1.U_cantidad, T1.U_ubicacion ");
            miSentencia.Append("FROM ORK_ENV_DEV_MASTER T0 ");
            miSentencia.Append("INNER JOIN ORK_ENV_DEV_MASTER T1 ");
            miSentencia.Append("ON T0.code = T1.U_idMaestro ");
            
            DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
            this.baseDatos.AddInParameter(miComando, "ORK_ENV_DEV_MASTER.Code", DbType.Int32, numeroDocumento);
            ORK_ENV_DEV_MASTER Documento = new ORK_ENV_DEV_MASTER();

            using (this.reader = this.baseDatos.ExecuteReader(miComando))
            {
                while (this.reader.Read())
                {
                   
                    //documento = new OrdenProduccion();
                    Documento.Code = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                    Documento.Name = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    Documento.U_fecha = this.reader.IsDBNull(2) ?  DateTime.Now : Convert.ToDateTime(this.reader.GetValue(2).ToString());
                    Documento.U_objType = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    Documento.U_usuario = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    Documento.U_socioNegocio = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();

                    ORK_ENV_DEV_DETALLE linea = new ORK_ENV_DEV_DETALLE();

                    linea.Code = this.reader.IsDBNull(6) ? 0 : Convert.ToInt32(this.reader.GetValue(6).ToString());
                    linea.Name = this.reader.IsDBNull(7) ? "" : this.reader.GetValue(7).ToString();
                    linea.U_idMaestro = this.reader.IsDBNull(8) ? 0 : Convert.ToInt32(this.reader.GetValue(8).ToString());
                    linea.U_tipoMovimiento = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(9).ToString();
                    linea.U_itemCode = this.reader.IsDBNull(10) ? "" : this.reader.GetValue(10).ToString();
                    linea.U_cantidad = this.reader.IsDBNull(11) ? 0 : Convert.ToInt32(this.reader.GetValue(11).ToString());
                    linea.U_ubicacion = this.reader.IsDBNull(12) ? 0 : Convert.ToInt32(this.reader.GetValue(12).ToString());
                    Documento.detalleEnvaseDevolutivo.Add(linea);
                }
            }
            return Documento;
        }









    }
}

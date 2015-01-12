using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using System.Data.Common;

namespace BP
{
    public class EnvaseDevolutivo
    {
        /// <summary>
        /// Creación de movimiento de envase
        /// </summary>
        /// <param name="Maestro">entidad</param>

        /// Atributos de conexión a la base de datos
        /// </summary>

        public Boolean EliminarDocumento(int numeroDocumento)
        {
            Boolean bResultado =false;

            try{
                //Eliminar detalles
                String sQuery = "delete from [@ORK_ENV_DEV_DETALLE] where (U_idMaestro = " + numeroDocumento.ToString() + ")";
                ClaseDatos.nonQuery(sQuery);

                //Eliminar encabezado
                sQuery = "delete from [@ORK_ENV_DEV_MASTER] where (code = " + numeroDocumento.ToString() + ")";
                ClaseDatos.nonQuery(sQuery);

                bResultado = true;
               }
                catch (Exception)
                {

                    throw;
                }

        

            return(bResultado);
        }
        /// <returns>Código del maestro</returns>
        public int CrearDocumento(ORK_ENV_DEV_MASTER Documento)
        {
            int noDoc = 0;
            UserTable oORK_ENV_DEV_MASTER = ClaseDatos.objCompany.UserTables.Item("ORK_ENV_DEV_MASTER");
             
            string masterCode = ObtenerSiguienteLinea("ORK_ENV_DEV_MASTER");

            oORK_ENV_DEV_MASTER.Code = masterCode;
            oORK_ENV_DEV_MASTER.Name = masterCode;

            oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_fecha").Value = Documento.U_fecha;
            oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_objType").Value = Documento.U_objType;
            oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_usuario").Value = Documento.U_usuario;
            oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_socioNegocio").Value = Documento.U_socioNegocio;

            noDoc = oORK_ENV_DEV_MASTER.Add();

            foreach (ORK_ENV_DEV_DETALLE linea in Documento.detalleEnvaseDevolutivo)
            {
                UserTable oORK_ENV_DEV_DETALLE = ClaseDatos.objCompany.UserTables.Item("ORK_ENV_DEV_DETALLE");

                string detailCode = ObtenerSiguienteLinea("oORK_ENV_DEV_DETALLE");

                oORK_ENV_DEV_DETALLE.Code = detailCode;
                oORK_ENV_DEV_DETALLE.Name = detailCode;

                oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_idMaestro").Value = noDoc; 
                oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_tipoMovimiento").Value = linea.U_tipoMovimiento;
                oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_itemCode").Value = linea.U_itemCode;
                oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_cantidad").Value = linea.U_cantidad;
                oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_ubicacion").Value = linea.U_ubicacion;

                oORK_ENV_DEV_DETALLE.Add();
            }

            return noDoc;
        }

        public ORK_ENV_DEV_MASTER ConsultarDocumento(int numeroDocumento)
        {
            Recordset miRecordSet = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            StringBuilder miSentencia = new StringBuilder("SELECT  T0.Code, T0.Name, T0.U_fecha, T0.U_objType, T0.U_usuario, T0.U_socioNegocio, ");
            miSentencia.Append("T1.code As CodDetalle, T1.name as NameDetalle, T1.U_idMaestro, T1.U_tipoMovimiento, T1.U_itemcode, T1.U_cantidad, T1.U_ubicacion ");
            miSentencia.Append("FROM [@ORK_ENV_DEV_MASTER] T0 ");
            miSentencia.Append("INNER JOIN [@ORK_ENV_DEV_MASTER] T1 ");
            miSentencia.Append("ON T0.code = T1.U_idMaestro ");
            miSentencia.Append(string.Format("where T0.Code = {0}", numeroDocumento));

            ClaseDatos.objCompany.StartTransaction();

            miRecordSet.DoQuery(miSentencia.ToString());

            ORK_ENV_DEV_MASTER Documento = new ORK_ENV_DEV_MASTER();

            if (miRecordSet.RecordCount > 0)
            {
                miRecordSet.MoveFirst();                

                for (int i = 0; i < miRecordSet.RecordCount; i++)
                {

                    if (!string.IsNullOrEmpty(miRecordSet.Fields.Item(0).ToString()))
                    {
                        if (i == 0)
                        {
                            Documento.Code = Convert.ToInt32(miRecordSet.Fields.Item(0).ToString());
                            Documento.Name = miRecordSet.Fields.Item(1).ToString();
                            Documento.U_fecha = Convert.ToDateTime(miRecordSet.Fields.Item(2).ToString());
                            Documento.U_objType = miRecordSet.Fields.Item(3).ToString();
                            Documento.U_usuario = miRecordSet.Fields.Item(4).ToString();
                            Documento.U_socioNegocio = miRecordSet.Fields.Item(5).ToString();
                        }
                        else
                        {
                            ORK_ENV_DEV_DETALLE linea = new ORK_ENV_DEV_DETALLE();

                            linea.Code = Convert.ToInt32(miRecordSet.Fields.Item(6).ToString());
                            linea.Name = miRecordSet.Fields.Item(7).ToString();
                            linea.U_idMaestro = Convert.ToInt32(miRecordSet.Fields.Item(8).ToString());
                            linea.U_tipoMovimiento = miRecordSet.Fields.Item(9).ToString();
                            linea.U_itemCode = miRecordSet.Fields.Item(10).ToString();
                            linea.U_cantidad = Convert.ToInt32(miRecordSet.Fields.Item(11).ToString());
                            linea.U_ubicacion = Convert.ToInt32(miRecordSet.Fields.Item(12).ToString());
                            Documento.detalleEnvaseDevolutivo.Add(linea);
                        }
                    }

                    miRecordSet.MoveNext();
                }
            }

            return Documento;
        }

        private string ObtenerSiguienteLinea(string tabla)
        {
            int noDoc = -1;

            try
            {
                Recordset miRecordSet = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                StringBuilder miSentencia = new StringBuilder("select isnull(MAX (code), 1) + 1 ");
                miSentencia.Append(string.Format("FROM [@{0}] where 1 = @numeroDocumento", tabla));

                ClaseDatos.objCompany.StartTransaction();

                miRecordSet.DoQuery(miSentencia.ToString());
                miRecordSet.MoveFirst(); 
            }
            catch (Exception)
            {

                throw;
            }

            return noDoc.ToString();
        }
    }

    public class ORK_ENV_DEV_DETALLE
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int U_idMaestro { get; set; }
        public string U_tipoMovimiento { get; set; }
        public string U_itemCode { get; set; }
        public int U_cantidad { get; set; }
        public int U_ubicacion { get; set; }

    }

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


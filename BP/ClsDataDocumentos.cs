using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAPbobsCOM;
using Entities;
using System.Configuration;

namespace BP
{
    public class ClsDataDocumentos
    {
        #region Atributos
        /// <summary>
        /// Atributos de conexión a la base de datos
        /// </summary>
        //private Database baseDatos;
        /// <summary>
        /// Lector
        /// </summary>
        private IDataReader reader;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ClsDataDocumentos()
        {
            //this.baseDatos = DatabaseFactory.CreateDatabase("SAP");
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Crear documento en SAP Business One
        /// </summary>
        /// <param name="documento">Documento con la información</param>
        /// <param name="tipo">Tipo de documento: "SalidaInventario", "EntradaInventario", "EntradaInventarioCompras", "EntregaenVentas"</param>
        /// <param name="userWSSAP">Usuario del servicio</param>
        /// <returns>Número Interno generado por SAP</returns> 
        public void CrearEntradaMercancia(DocumentoMktng documento)
        {
            Documents miDocumento;            

            miDocumento = (Documents)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes);
            miDocumento.CardCode = documento.CardCode;
            miDocumento.Series = GetSeriesDocumento("SerieEntradaEnvaseDevolutivo");
            miDocumento.Comments = documento.Comments;

            foreach (DocumentoLineas linea in documento.lineas)
            {
                miDocumento.Lines.ItemCode = linea.ItemCode;
                miDocumento.Lines.Quantity = linea.Quantity;
                miDocumento.Lines.WarehouseCode = linea.WhsCode;
                miDocumento.Lines.Price = linea.Price;
                miDocumento.DocDate = DateTime.Now;
                miDocumento.TaxDate = DateTime.Now;
                miDocumento.DocDueDate = DateTime.Now;

                if (linea.BatchNumbers != null)
                    foreach (Lote lote in linea.BatchNumbers)
                    {
                        miDocumento.Lines.BatchNumbers.BatchNumber = lote.DistNumber;
                        miDocumento.Lines.BatchNumbers.Quantity = lote.Quantity;
                        //miDocumento.Lines.BatchNumbers.ExpiryDate = lote.ExpDate;
                        miDocumento.Lines.BatchNumbers.Add();
                    }

                miDocumento.Lines.Add();
            }

            if (miDocumento.Add() != 0)
            {
                throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
            }
            documento.DocEntry = Convert.ToInt32(ClaseDatos.objCompany.GetNewObjectKey());

        }

        public static int GetSeriesDocumento(string parametro)
        {
            string nombreSerie = ConfigurationManager.AppSettings[parametro].ToString();

            StringBuilder miSentencia = new StringBuilder("SELECT Series FROM NNM1 ");
            if (parametro == "SerieEntradaEnvaseDevolutivo")
                miSentencia.Append(string.Format("where objectcode = '20' and SeriesName = '{0}'", nombreSerie));
            else
                miSentencia.Append(string.Format("where objectcode = '67' and SeriesName = '{0}'", nombreSerie));
                        
            return ClaseDatos.scalarIntSql(miSentencia.ToString());                
        }

        public List<ReporteConsignacion> GetReporteConsignacion(DateTime from, DateTime to) {
            List<ReporteConsignacion> reporte = new List<ReporteConsignacion>();

            StringBuilder oSql = new StringBuilder();



            return reporte;
        }
        #endregion
    }
}

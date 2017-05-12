using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data;
using System.Data.Common;
using SAPbobsCOM;
using BP.EsquemasReporteFletes;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace BP
{
    public class ClsDataFletes
    {
        #region Atributos
        /// <summary>
        /// Lector
        /// </summary>
        private IDataReader reader;
        #endregion

        #region Metodos
        public List<Transporter> GetTransportadoras()
        {
            List<Transporter> coleccion = new List<Transporter>();

            string oSql = "SELECT Code, U_CSS_Razon_Social FROM [@CSS_TRANSPORTADORA] ";

            using (this.reader = ClaseDatos.procesaDataReader(oSql))
            {
                while (this.reader.Read())
                {
                    Transporter item = new Transporter();
                    item.code = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.name = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    coleccion.Add(item);
                }
            }

            return coleccion;
        }

        public List<Almacen> GetZonas()
        {
            List<Almacen> coleccion = new List<Almacen>();

            string oSql = "select code, name from [@css_zona] order by name ";

            using (this.reader = ClaseDatos.procesaDataReader(oSql))
            {
                while (this.reader.Read())
                {
                    Almacen zona = new Almacen();
                    zona.WhsCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    zona.WhsName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    
                    coleccion.Add(zona);
                }
            }

            return coleccion;
        }

        public List<Vehicle> GetPlacas()
        {
            List<Vehicle> coleccion = new List<Vehicle>();

            string oSql = "select Code, U_CSS_Propietario, U_CSS_Tipo_Vehiculo, U_CSS_Transportadora FROM [@CSS_VEHICULO] ";

            using (this.reader = ClaseDatos.procesaDataReader(oSql))
            {
                while (this.reader.Read())
                {
                    Vehicle item = new Vehicle();
                    item.code = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.name = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.propietario = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    item.tipoVehiculo = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    item.transportadora = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    coleccion.Add(item);
                }
            }

            return coleccion;
        }

        public List<Driver> GetConductores()
        {
            List<Driver> coleccion = new List<Driver>();

            string oSql = "SELECT Code, U_CSS_Nombre FROM [@CSS_CONDUCTOR] ";

            using (this.reader = ClaseDatos.procesaDataReader(oSql))
            {
                while (this.reader.Read())
                {
                    Driver item = new Driver();
                    item.code = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.name = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    coleccion.Add(item);
                }
            }

            return coleccion;
        }

        public List<VehicleType> GetTiposVehiculo()
        {
            List<VehicleType> coleccion = new List<VehicleType>();

            string oSql = "SELECT Code, Name FROM [@CSS_TIPO_VEHICULO] ";

            using (this.reader = ClaseDatos.procesaDataReader(oSql))
            {
                while (this.reader.Read())
                {
                    VehicleType item = new VehicleType();
                    item.code = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.name = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    coleccion.Add(item);
                }
            }

            return coleccion;
        }

        public List<Flete> GetTarifasFlete()
        {
            List<Flete> coleccion = new List<Flete>();

            string oSql = "select distinct t0.U_CSS_Zona_Origen, t0.U_CSS_Zona_Destino, t0.U_CSS_Transportadora, t0.U_CSS_Tipo_Vehiculo, isnull(t0.U_CSS_Tarifa , 0) from [@css_tarifa_flete] t0 ";

            using (this.reader = ClaseDatos.procesaDataReader(oSql))
            {
                while (this.reader.Read())
                {
                    Flete item = new Flete();
                    item.origen = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.destino = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    item.transportadora = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    item.tipoVehiculo = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    item.tarifa = this.reader.IsDBNull(4) ? 0 : Convert.ToDouble(this.reader.GetValue(4).ToString());
                    coleccion.Add(item);
                }
            }

            return coleccion;
        }

        public List<Flete> GetDeliverys(DateTime fecha, string bodega)
        {
            List<Flete> coleccion = new List<Flete>();

            StringBuilder oSql = new StringBuilder();
            //oSql.Append("SELECT DISTINCT T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.DocNum,T0.Series,T0.Peso, (select U_CSS_Tarifa from [@css_tarifa_flete] where U_CSS_Zona_Origen = T0.ORIGEN and U_CSS_Zona_Destino = T0.DESTINO ) * T0.Peso U_CSS_Tarifa,T0.DocEntry,T0.Origen ");
            oSql.Append("SELECT DISTINCT T0.Destino,T0.Tipo,T0.CardName,T0.SlpName,T0.DocNum,T0.Series,T0.Peso, 0,T0.DocEntry,T0.Origen ");
            oSql.Append("FROM CSS_ZONA_FLETE T0 ");
            oSql.Append(string.Format("WHERE T0.DocDueDate='{0}' AND origen='{1}' ORDER BY T0.Destino,T0.Tipo", fecha.ToString("yyyy-MM-dd"), bodega));
            //oSql.Append(string.Format("WHERE T0.DocDueDate='{0}' AND WhsCode='{1}' ORDER BY T0.Destino,T0.Tipo", fecha.ToString("yyyy-MM-dd"), bodega));

            using (this.reader = ClaseDatos.procesaDataReader(oSql.ToString()))
            {
                while (this.reader.Read())
                {
                    Flete item = new Flete();
                    item.destino = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.tipo = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    item.cliente = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    item.vendedor = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    item.orden = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    item.serie = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    item.peso = this.reader.IsDBNull(6) ? 0 : Convert.ToDouble(this.reader.GetValue(6).ToString());
                    item.tarifa = 0;//this.reader.IsDBNull(7) ? 0 : Convert.ToDouble(this.reader.GetValue(7).ToString());
                    item.docEntry = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                    item.origen = this.reader.IsDBNull(9) ? "" : this.reader.GetValue(9).ToString();

                    coleccion.Add(item);
                }
            }

            //foreach (Flete item in coleccion)
            //{
            //    if (item.tarifa == null || item.tarifa == 0)
            //    {
            //        Dictionary<double, double> tarifaPeso = GetTarifaPeso(item.origen, item.destino);
            //        double tarifaUnitaria = 0, tarifa = 0, peso = 0;

            //        tarifa = tarifaPeso.Select(x => x.Key).FirstOrDefault();
            //        peso = tarifaPeso.Select(x => x.Value).FirstOrDefault();

            //        tarifaUnitaria = peso == 0 ? 0 : tarifa / peso;

            //        item.tarifa = Math.Round(tarifaUnitaria * item.peso, 0);
            //    }
            //}

            return coleccion;
        }

        public List<Flete> GetMacroGiude(int id)
        {
            List<Flete> coleccion = new List<Flete>();

            StringBuilder oSql = new StringBuilder();
            oSql.Append("SELECT T0.U_CSS_Zona_Destino,T0.U_CSS_Tipo_Documento,T0.U_CSS_Cliente,T0.U_CSS_Representante,T0.U_CSS_Documento_SAP,T0.U_CSS_Serie, T0.U_CSS_Peso,T0.U_CSS_Tarifa,T0.U_CSS_Estado ");
            oSql.Append(string.Format("FROM [@CSS_MOVIMIENTO] T0 WHERE U_CSS_Macroguia='{0}'", id.ToString()));

            using (this.reader = ClaseDatos.procesaDataReader(oSql.ToString()))
            {
                if (((DbDataReader)this.reader).HasRows)
                    while (this.reader.Read())
                    {
                        Flete item = new Flete();
                        item.tipo = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                        item.cliente = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                        item.vendedor = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                        item.orden = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                        item.serie = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                        item.peso = this.reader.IsDBNull(6) ? 0 : Convert.ToDouble(this.reader.GetValue(6).ToString());
                        item.tarifa = this.reader.IsDBNull(7) ? 0 : Convert.ToDouble(this.reader.GetValue(7).ToString());
                        item.estado = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                        item.seleccionado = true;
                        coleccion.Add(item);
                    }
                else
                    throw new Exception("No existe el documento de transporte");
            }

            return coleccion;
        }

        public Dictionary<double, double> GetTarifaPeso(string origen, string destino)
        {
            Dictionary<double, double> Coleccion = new Dictionary<double, double>();

            StringBuilder oSql = new StringBuilder();
            oSql.Append("select top 1 U_CSS_Tarifa, U_CSS_Peso from [@CSS_MOVIMIENTO]");
            oSql.Append(string.Format("where U_CSS_Zona_Origen = '{0}' and U_CSS_Zona_Destino = '{1}' order by CAST(Code as int) desc", origen, destino));

            using (this.reader = ClaseDatos.procesaDataReader(oSql.ToString()))
            {
                if (((DbDataReader)this.reader).HasRows)
                    while (this.reader.Read())
                    {
                        double tarifa = this.reader.IsDBNull(0) ? 0 : Convert.ToDouble(this.reader.GetValue(0).ToString());
                        double peso = this.reader.IsDBNull(1) ? 0 : Convert.ToDouble(this.reader.GetValue(1).ToString());

                        Coleccion.Add(tarifa, peso);
                    }
                else
                    throw new Exception("No existe el documento de transporte");
            }

            return Coleccion;
        }

        public int GetNexMacroGuideNumber()
        {
            string oSql = "SELECT ISNULL(MAX(CONVERT(INT,U_CSS_Macroguia)),0) FROM [@CSS_MOVIMIENTO]";

            return ClaseDatos.scalarIntSql(oSql)+1;
        }

        public int GetNexMacroGuideCode()
        {
            string oSql = "SELECT ISNULL(MAX(CONVERT(INT,Code)),0) FROM [@CSS_MOVIMIENTO]";

            return ClaseDatos.scalarIntSql(oSql)+1;
        }

        public int SaveMacroGuide(List<Flete> fletes, Vehicle vehiculo, string conductor, string observaciones)
        {
            UserTable movimiento = ClaseDatos.objCompany.UserTables.Item("CSS_MOVIMIENTO");
            StockTransfer movimientoInventario = (StockTransfer)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
            Documents entregaVentas = (Documents)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);

            int nextMacroGuide = GetNexMacroGuideNumber();
            int nextCode = GetNexMacroGuideCode();

            try
            {
                ClaseDatos.objCompany.StartTransaction();

                foreach (Flete flete in fletes)
                {                    
                    movimiento.Code = nextCode.ToString();
                    movimiento.Name = nextCode.ToString();
                    movimiento.UserFields.Fields.Item("U_CSS_Macroguia").Value = nextMacroGuide;
                    movimiento.UserFields.Fields.Item("U_CSS_Documento_SAP").Value = flete.orden;
                    movimiento.UserFields.Fields.Item("U_CSS_Serie").Value = flete.serie;
                    movimiento.UserFields.Fields.Item("U_CSS_Tipo_Documento").Value = flete.tipo;
                    movimiento.UserFields.Fields.Item("U_CSS_Estado").Value = "Activo";
                    movimiento.UserFields.Fields.Item("U_CSS_Zona_Origen").Value = flete.origen;
                    movimiento.UserFields.Fields.Item("U_CSS_Zona_Destino").Value = flete.destino;
                    movimiento.UserFields.Fields.Item("U_CSS_Tarifa").Value = flete.tarifa.ToString();
                    movimiento.UserFields.Fields.Item("U_CSS_Peso").Value = flete.peso.ToString();
                    movimiento.UserFields.Fields.Item("U_CSS_Cliente").Value = flete.cliente;
                    movimiento.UserFields.Fields.Item("U_CSS_Representante").Value = flete.vendedor;
                    movimiento.UserFields.Fields.Item("U_CSS_DocEntry").Value = flete.docEntry.ToString();
                    movimiento.UserFields.Fields.Item("U_CSS_Fecha").Value = DateTime.Now.ToString("yyyyMMdd hh:mm tt");
                    movimiento.UserFields.Fields.Item("U_CSS_Transportadora").Value = vehiculo.transportadora;
                    movimiento.UserFields.Fields.Item("U_CSS_Vehiculo").Value = vehiculo.code;
                    movimiento.UserFields.Fields.Item("U_CSS_Conductor").Value = conductor;
                    movimiento.UserFields.Fields.Item("U_CSS_OBSERVACIONES").Value = observaciones;

                    if (movimiento.Add() < 0)
                        throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());

                    nextCode++;

                    if (flete.tipo == "T")
                    {
                        movimientoInventario.GetByKey(int.Parse(flete.docEntry));
                        movimientoInventario.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = flete.tarifa.ToString();
                        movimientoInventario.UserFields.Fields.Item("U_CSS_Vehiculo").Value = vehiculo.code;
                        movimientoInventario.UserFields.Fields.Item("U_CSS_Transportadora").Value = vehiculo.transportadora;
                        movimientoInventario.UserFields.Fields.Item("U_CSS_Conductor").Value = conductor;
                        movimientoInventario.UserFields.Fields.Item("U_CSS_Propietario").Value = vehiculo.propietario;
                        movimientoInventario.UserFields.Fields.Item("U_CSS_Macroguia").Value = nextMacroGuide;

                        if (movimientoInventario.Update() < 0)
                            throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                    }
                    else
                    {
                        entregaVentas.GetByKey(int.Parse(flete.docEntry));
                        entregaVentas.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = flete.tarifa.ToString();
                        entregaVentas.UserFields.Fields.Item("U_CSS_Vehiculo").Value = vehiculo.code;
                        entregaVentas.UserFields.Fields.Item("U_CSS_Transportadora").Value = vehiculo.transportadora;
                        entregaVentas.UserFields.Fields.Item("U_CSS_Conductor").Value = conductor;
                        entregaVentas.UserFields.Fields.Item("U_CSS_Propietario").Value = vehiculo.propietario;
                        entregaVentas.UserFields.Fields.Item("U_CSS_Macroguia").Value = nextMacroGuide;

                        if (entregaVentas.Update() < 0)
                            throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                    }
                }

                if (ClaseDatos.objCompany.InTransaction)
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            catch (Exception)
            {
                if (ClaseDatos.objCompany.InTransaction)
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(entregaVentas);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(movimientoInventario);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(movimiento);
            }

            return nextMacroGuide;
        }

        public Macroguia PrintMacroGuide(string id)
        {
            string oSql = string.Format("SELECT Code FROM [@CSS_MOVIMIENTO] WHERE U_CSS_Macroguia='{0}'", id);
            string codeMacroguide = ClaseDatos.scalarStringSql(oSql);

            UserTable movimientos = ClaseDatos.objCompany.UserTables.Item("CSS_MOVIMIENTO");
            movimientos.GetByKey(codeMacroguide);
            movimientos.UserFields.Fields.Item("U_CSS_Fecha_Impresio").Value = DateTime.Now.ToString("yyyyMMdd hh:mm tt");
            movimientos.Update();

            DataSetFletes dsFletes = new DataSetFletes();

            StringBuilder oSqlReport = new StringBuilder();
            oSqlReport.Append("SELECT T0.CompnyName,T0.CompnyAddr,T0.Phone1,T0.Fax,T0.TaxIdNum,T1.City,T2.Name FROM ADM1 T1,OADM T0 INNER JOIN OCRY T2 ON T0.Country=T2.Code ");

            oSqlReport.Append("SELECT substring(T0.U_CSS_Fecha,0,10) AS Fecha,substring(T0.U_CSS_Fecha,9,15) As Hora ,T0.U_CSS_Zona_Origen,T0.U_CSS_Zona_Destino,T0.U_CSS_Macroguia,T0.U_CSS_Vehiculo, ");
            oSqlReport.Append("T0.U_CSS_Estado,T0.U_CSS_OBSERVACIONES, T1.U_CSS_Nombre,T2.Name,T2.U_CSS_Razon_Social,Cast(SUM(U_CSS_Tarifa) as integer) AS Total ");
            oSqlReport.Append("FROM [@CSS_MOVIMIENTO] T0 INNER JOIN [@CSS_CONDUCTOR] T1 ON T0.U_CSS_Conductor=T1.Code INNER JOIN [@CSS_Transportadora] T2 ON T0.U_CSS_Transportadora=T2.Code ");
            oSqlReport.Append(string.Format("WHERE T0.U_CSS_Macroguia='{0}' ", id));
            oSqlReport.Append("GROUP BY T0.U_CSS_Fecha,T0.U_CSS_Zona_Origen,T0.U_CSS_Zona_Destino,T0.U_CSS_Macroguia,T0.U_CSS_Vehiculo, T1.U_CSS_Nombre,T2.Name,T2.U_CSS_Razon_Social,T0.U_CSS_Estado,T0.U_CSS_OBSERVACIONES ");

            oSqlReport.Append("SELECT 'ENTREGA' AS TIPO,T0.U_CSS_Serie,T0.U_CSS_DocEntry,T0.U_CSS_Documento_SAP,T0.U_CSS_Tipo_Documento,T0.U_CSS_Tarifa, T4.Street,T4.U_CSS_Telefono, T1.CardName ");
            oSqlReport.Append("FROM [@CSS_MOVIMIENTO] T0 INNER JOIN ODLN T1 ON T0.U_CSS_DocEntry=T1.DocEntry INNER JOIN CRD1 T4 ON T1.ShipToCode=T4.Address AND T1.CardCode=T4.CardCode ");
            oSqlReport.Append(string.Format("WHERE T0.U_CSS_Macroguia='{0}' AND T0.U_CSS_Tipo_Documento IN ('D','P') ", id));
            oSqlReport.Append("UNION ALL ");
            oSqlReport.Append("SELECT 'TRASLADO' AS TIPO, T0.U_CSS_Serie,T0.U_CSS_DocEntry,T0.U_CSS_Documento_SAP,T0.U_CSS_Tipo_Documento,T0.U_CSS_Tarifa, T4.Street, T4.U_CSS_Telefono, '' as CardName ");
            oSqlReport.Append("FROM [@CSS_MOVIMIENTO] T0 INNER JOIN OWTR T1 ON T0.U_CSS_DocEntry=T1.DocEntry INNER JOIN WTR1 T5 ON T5.DocEntry=T1.DocEntry INNER JOIN OITM T6 ON T5.ItemCode=T6.ItemCode ");
            oSqlReport.Append("INNER JOIN OWHS T4 ON T5.WhsCode=T4.WhsCode ");
            oSqlReport.Append(string.Format("WHERE T0.U_CSS_Tipo_Documento='T' AND T0.U_CSS_Macroguia='{0}' ", id));
            oSqlReport.Append("GROUP BY T0.U_CSS_Serie,T0.U_CSS_DocEntry,T0.U_CSS_Documento_SAP,T0.U_CSS_Tipo_Documento,T4.Street, T4.U_CSS_Telefono,T0.U_CSS_Tarifa ");

            oSqlReport.Append("SELECT T0.U_CSS_Tipo_Documento as U_CSS_Tipo_Doc,cast(T0.U_CSS_Documento_SAP as int) AS DocNum,T0.U_CSS_Serie, T0.U_CSS_DocEntry as U_CSS_DocEntryDetalle, ");
            oSqlReport.Append("cast(T1.Quantity*T2.SWeight1 as numeric(19,2)) AS Cantidad,T1.LineTotal Price, T2.ItemCode,T2.ItemName ");
            oSqlReport.Append("FROM [@CSS_MOVIMIENTO] T0 INNER JOIN DLN1 T1 ON T0.U_CSS_DocEntry=T1.DocEntry INNER JOIN OITM T2 ON T1.ItemCode=T2.ItemCode ");
            oSqlReport.Append(string.Format("WHERE T0.U_CSS_Tipo_Documento IN ('D','P') AND T0.U_CSS_Macroguia='{0}' ", id));
            oSqlReport.Append("UNION ALL ");
            oSqlReport.Append("SELECT T0.U_CSS_Tipo_Documento as U_CSS_Tipo_Doc,cast(T0.U_CSS_Documento_SAP as int) AS DocNum,T0.U_CSS_Serie,  T0.U_CSS_DocEntry as U_CSS_DocEntryDetalle,");
            oSqlReport.Append("cast(T1.Quantity*T2.SWeight1 as numeric(19,2)) AS Cantidad,T1.StockPrice, T2.ItemCode,T2.ItemName ");
            oSqlReport.Append("FROM [@CSS_MOVIMIENTO] T0 INNER JOIN WTR1 T1 ON T0.U_CSS_DocEntry=T1.DocEntry ");
            oSqlReport.Append("INNER JOIN OITM T2 ON T1.ItemCode=T2.ItemCode ");
            oSqlReport.Append(string.Format("WHERE T0.U_CSS_Tipo_Documento='T' AND T0.U_CSS_Macroguia='{0}' ", id));

            oSqlReport.Append("SELECT SUM(CANTIDAD) AS CANTIDAD,SUM(PRECIO) AS PRECIO ");
            oSqlReport.Append("FROM (SELECT sum(T1.LineTotal) AS PRECIO,sum(T1.Quantity*T2.SWeight1) AS CANTIDAD ");
            oSqlReport.Append("FROM [@CSS_MOVIMIENTO] T0 INNER JOIN DLN1 T1 ON T0.U_CSS_DocEntry=T1.DocEntry INNER JOIN OITM T2 ON T1.ItemCode=T2.ItemCode ");
            oSqlReport.Append(string.Format("AND T0.U_CSS_Macroguia='{0}' AND T0.U_CSS_Tipo_Documento IN('P','D') ", id));
            oSqlReport.Append("UNION ALL ");
            oSqlReport.Append("SELECT sum(T1.StockPrice) AS PRECIO,sum(T1.Quantity*T2.SWeight1) AS CANTIDAD FROM [@CSS_MOVIMIENTO] T0 INNER JOIN WTR1 T1 ON T0.U_CSS_DocEntry=T1.DocEntry ");
            oSqlReport.Append(string.Format("INNER JOIN OITM T2 ON T1.ItemCode=T2.ItemCode AND T0.U_CSS_Macroguia='{0}' AND T0.U_CSS_Tipo_Documento IN('T')) TABLA", id));


            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB);
            //using (ClaseDatos.SqlConn)
            //{
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(oSqlReport.ToString(), ClaseDatos.SqlConn);
                adapter.Fill(dsFletes);
            //}

            Macroguia reporte = new Macroguia();
            reporte.SetDataSource(dsFletes.Tables[2]);

            ReportDocument subReporte = new ReportDocument();
            SubreportObject subreportObject;
            ReportDocument subReporteMacroguia = new ReportDocument();
            SubreportObject subReporteDatosMacroguiaObject;
            ReportDocument subReporteMacroguiaDetalles = new ReportDocument();
            SubreportObject subReporteTotales;
            ReportDocument subReporteMacroguiaTotales = new ReportDocument();

            subreportObject = reporte.ReportDefinition.ReportObjects["EncabezadoCompania"] as SubreportObject;
            subReporte = reporte.OpenSubreport(subreportObject.SubreportName);
            subReporte.SetDataSource(dsFletes.Tables[0]);

            subReporteDatosMacroguiaObject = reporte.ReportDefinition.ReportObjects["DatosMacroguia"] as SubreportObject;
            subReporteMacroguia = reporte.OpenSubreport(subReporteDatosMacroguiaObject.SubreportName);
            subReporteMacroguia.SetDataSource(dsFletes.Tables[1]);

            subreportObject = reporte.ReportDefinition.ReportObjects["Lineas"] as SubreportObject;
            subReporte = reporte.OpenSubreport(subreportObject.SubreportName);
            subReporte.SetDataSource(dsFletes.Tables[3]);

            subReporteTotales = reporte.ReportDefinition.ReportObjects["Totales"] as SubreportObject;
            subReporteMacroguiaTotales = reporte.OpenSubreport(subReporteTotales.SubreportName);
            subReporteMacroguiaTotales.SetDataSource(dsFletes.Tables[4]);

            return reporte;
        }

        public bool CancelMacroGuide(string id)
        {
            bool res = false;

            string oSql = string.Format("SELECT Code,U_CSS_DocEntry,U_CSS_Tipo_Documento FROM [@CSS_MOVIMIENTO] WHERE U_CSS_Macroguia='{0}' ", id);

            using (this.reader = ClaseDatos.procesaDataReader(oSql.ToString()))
            {
                if (((DbDataReader)this.reader).HasRows)
                {
                    Documents entregaVenta = (Documents)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    StockTransfer transferencia = (StockTransfer)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                    UserTable movimiento = ClaseDatos.objCompany.UserTables.Item("CSS_MOVIMIENTO");

                    ClaseDatos.objCompany.StartTransaction();

                    try
                    {                        
                        while (this.reader.Read())
                        {
                            movimiento.GetByKey(this.reader.GetString(0));
                            movimiento.UserFields.Fields.Item("U_CSS_Estado").Value = "Anulado";
                            movimiento.Update();
                            if (this.reader.GetString(2).Equals("T"))
                            {
                                transferencia.GetByKey(Convert.ToInt32(this.reader.GetValue(1)));
                                transferencia.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = "0";
                                transferencia.UserFields.Fields.Item("U_CSS_Vehiculo").Value = "0";
                                transferencia.UserFields.Fields.Item("U_CSS_Transportadora").Value = "0";
                                transferencia.UserFields.Fields.Item("U_CSS_Conductor").Value = "0";
                                transferencia.UserFields.Fields.Item("U_CSS_Propietario").Value = "0";
                                transferencia.Update();
                            }
                            else
                            {
                                entregaVenta.GetByKey(Convert.ToInt32(this.reader.GetValue(1)));
                                entregaVenta.UserFields.Fields.Item("U_CSS_Valor_Flete").Value = "0";
                                entregaVenta.UserFields.Fields.Item("U_CSS_Vehiculo").Value = "0";
                                entregaVenta.UserFields.Fields.Item("U_CSS_Transportadora").Value = "0";
                                entregaVenta.UserFields.Fields.Item("U_CSS_Conductor").Value = "0";
                                entregaVenta.UserFields.Fields.Item("U_CSS_Propietario").Value = "0";
                                entregaVenta.Update();
                            }
                        }

                        ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);

                        res = true;
                    }
                    catch (Exception)
                    {
                        ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        throw;
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(entregaVenta);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(transferencia);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(movimiento);
                    }
                }
                else
                    throw new Exception("No existe el documento de transporte");
            }

            return res;
        }
        #endregion
    }
}

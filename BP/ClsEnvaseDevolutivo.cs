using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using Entities;

namespace BP
{
    public static class ClsEnvaseDevolutivo
    {
        public static DateTime GetFechaCorte()
        {
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            DateTime inicioEnvaseDevolutivo = new DateTime();
            string consultaDoc = "select U_inicioEnvDev, U_ultSincronizacion from [@ORK_ENV_DEV_INICIO]";

            rsDocumento.DoQuery(consultaDoc.ToString());

            if (rsDocumento.RecordCount > 0)
            {
                rsDocumento.MoveFirst();

                inicioEnvaseDevolutivo = (DateTime)rsDocumento.Fields.Item(0).Value;
            }

            return inicioEnvaseDevolutivo;
        }

        public static Remision GetRemision(int DocNum)
        {
            Remision remision = null;

            try
            {
                string query = QueryCabeceraRemision(DocNum.ToString());

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    rsDocumento.MoveFirst();

                    remision = new Remision()
                    {
                        docNum = DocNum,
                        docEntry = int.Parse(rsDocumento.Fields.Item(0).Value.ToString()),
                        docDate = DateTime.Parse(rsDocumento.Fields.Item(1).Value.ToString()),
                        CardCode = rsDocumento.Fields.Item(2).Value.ToString(),
                        CardName = rsDocumento.Fields.Item(3).Value.ToString(),
                        SlpCode = rsDocumento.Fields.Item(4).Value.ToString(),
                        Lineas = GetLineasRemision(int.Parse(rsDocumento.Fields.Item(0).Value.ToString()))
                    };
                }
            }
            catch (Exception)
            {

            }

            return remision;
        }

        public static List<Proveedor> GetProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            string query = "select CardCode, CardName from OCRD where CardType = 'S' order by CardName";

            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            rsDocumento.DoQuery(query.ToString());

            if (rsDocumento.RecordCount > 0)
            {
                while (!rsDocumento.EoF)
                {
                    proveedores.Add(new Proveedor()
                    {
                        CardCode = rsDocumento.Fields.Item(0).Value.ToString(),
                        CardName = rsDocumento.Fields.Item(1).Value.ToString(),
                    });

                    rsDocumento.MoveNext();
                }
            }

            return proveedores;
        }

        public static int SaveEntrada(LineasEntradaManual linea)
        {
            int noDoc = 0;
            string masterCode = "";

            try
            {
                ClaseDatos.objCompany.StartTransaction();

                UserTable objORK_ENVASE_DEV = ClaseDatos.objCompany.UserTables.Item("ORK_ENVASE_DEV");

                masterCode = GetNextLineNum();

                objORK_ENVASE_DEV.Code = masterCode;
                objORK_ENVASE_DEV.Name = masterCode;

                objORK_ENVASE_DEV.UserFields.Fields.Item("U_objType").Value = linea.objType;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_baseEntry").Value = linea.baseEntry;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_baseLine").Value = linea.baseLine;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_quantity").Value = linea.quantity;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_numReciboCliente").Value = string.IsNullOrEmpty(linea.numReciboCliente) ? "" : linea.numReciboCliente;

                objORK_ENVASE_DEV.UserFields.Fields.Item("U_cardCode").Value = string.IsNullOrEmpty(linea.cardCode) ? "" : linea.cardCode;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_numReciboProveedor").Value = string.IsNullOrEmpty(linea.numReciboProveedor) ? "" : linea.numReciboProveedor;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_costoReacondic").Value = linea.costoReacondic;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_itemsBaja").Value = linea.itemsBaja;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_observaciones").Value = string.IsNullOrEmpty(linea.observaciones) ? "" : linea.observaciones;


                objORK_ENVASE_DEV.UserFields.Fields.Item("U_fechaReciboCliente").Value = new DateTime(1980, 1, 1);
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_fechaReciboProv").Value = new DateTime(1980, 1, 1);

                if (!string.IsNullOrEmpty(linea.numReciboCliente))
                    objORK_ENVASE_DEV.UserFields.Fields.Item("U_fechaReciboCliente").Value = linea.fechaReciboCliente.ToString("yyyy-MM-dd");

                if (linea.objType == 91 || linea.objType == 92)
                    objORK_ENVASE_DEV.UserFields.Fields.Item("U_fechaReciboProv").Value = linea.fechaReciboProveedor.ToString("yyyy-MM-dd");

                noDoc = objORK_ENVASE_DEV.Add();

                if (noDoc < 0)
                {
                    throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                }

                if (ClaseDatos.objCompany.InTransaction)
                {
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);

                throw (new Exception(ex.Message));
            }
            return noDoc;
        }

        public static int SaveEntrada(int odlnDocEntry, int opdnDocEntry)
        {
            int noDoc = 0;
            string masterCode = "";

            try
            {
                ClaseDatos.objCompany.StartTransaction();

                UserTable objORK_ENV_DEV_REACON = ClaseDatos.objCompany.UserTables.Item("ORK_ENV_DEV_REACON");

                masterCode = GetNextLineNum();

                objORK_ENV_DEV_REACON.Code = masterCode;
                objORK_ENV_DEV_REACON.Name = masterCode;

                objORK_ENV_DEV_REACON.UserFields.Fields.Item("U_odlnDocEntry").Value = odlnDocEntry;
                objORK_ENV_DEV_REACON.UserFields.Fields.Item("U_opdnDocEntry").Value = opdnDocEntry;

                noDoc = objORK_ENV_DEV_REACON.Add();

                if (noDoc < 0)
                {
                    throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                }

                if (ClaseDatos.objCompany.InTransaction)
                {
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                }
            }
            catch (Exception ex)
            {
                ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);

                throw (new Exception(ex.Message));
            }
            return noDoc;
        }

        public static List<reporteCarteraCliente> GetReporteCarteraCliente()
        {
            List<reporteCarteraCliente> reporte = new List<reporteCarteraCliente>();
            //List<Remision> remisiones = new List<Remision>();

            try
            {
                string query = QueryBusquedaRemisiones();

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        reporte.Add(calculaReporteCarteraCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(1).Value.ToString()))));
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<reporteCarteraCliente> GetReporteCarteraCliente(DateTime desdeRemision, DateTime hastaRemision)
        {
            List<reporteCarteraCliente> reporte = new List<reporteCarteraCliente>();

            try
            {
                string query = QueryBusquedaRemisiones(desdeRemision, hastaRemision);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        reporte.Add(calculaReporteCarteraCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(1).Value.ToString()))));
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<reporteCarteraCliente> GetReporteCarteraCliente(DateTime desdeRemision, DateTime hastaRemision, string proveedor)
        {
            List<reporteCarteraCliente> reporte = new List<reporteCarteraCliente>();

            try
            {
                string query = QueryBusquedaRemisiones(desdeRemision, hastaRemision, proveedor);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        reporte.Add(calculaReporteCarteraCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(1).Value.ToString()))));
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<reporteCarteraCliente> GetReporteCarteraClienteRecibos(DateTime desdeRecibo, DateTime hastaRecibo)
        {
            List<reporteCarteraCliente> reporte = new List<reporteCarteraCliente>();

            try
            {
                string query = QueryBusquedaRemisionesRecibo(desdeRecibo, hastaRecibo);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        //reporte.Add(calculaReporteCarteraCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(0).Value.ToString()))));

                        if (rsDocumento.Fields.Item(4).Value.ToString().Contains("Recibo Cliente No."))
                            reporte.Add(new reporteCarteraCliente()
                            {
                                fecha = DateTime.Parse(rsDocumento.Fields.Item(0).Value.ToString()).ToString("yyyy-MM-dd"),
                                numeroDocumento = rsDocumento.Fields.Item(1).Value.ToString(),
                                codigoSocioNegocio = rsDocumento.Fields.Item(2).Value.ToString(),
                                socioNegocio = rsDocumento.Fields.Item(3).Value.ToString(),
                                nombreVendedor = rsDocumento.Fields.Item(4).Value.ToString().Split('-')[0].Replace("Recibo Cliente No.", " "),
                                codigoArticulo = rsDocumento.Fields.Item(5).Value.ToString(),
                                nombreArticulo = rsDocumento.Fields.Item(6).Value.ToString(),
                                entregado = int.Parse(rsDocumento.Fields.Item(7).Value.ToString())
                            });

                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<reporteCarteraCliente> GetReporteCarteraClienteRecibos(DateTime desdeRecibo, DateTime hastaRecibo, string proveedor)
        {
            List<reporteCarteraCliente> reporte = new List<reporteCarteraCliente>();

            try
            {
                string query = QueryBusquedaRemisionesRecibo(desdeRecibo, hastaRecibo, proveedor);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        //reporte.Add(calculaReporteCarteraCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(0).Value.ToString()))));
                        if (rsDocumento.Fields.Item(4).Value.ToString().Contains("Recibo Cliente No."))
                            reporte.Add(new reporteCarteraCliente()
                            {
                                fecha = DateTime.Parse(rsDocumento.Fields.Item(0).Value.ToString()).ToString("yyyy-MM-dd"),
                                numeroDocumento = rsDocumento.Fields.Item(1).Value.ToString(),
                                codigoSocioNegocio = rsDocumento.Fields.Item(2).Value.ToString(),
                                socioNegocio = rsDocumento.Fields.Item(3).Value.ToString(),
                                nombreVendedor = rsDocumento.Fields.Item(4).Value.ToString().Split('-')[0].Replace("Recibo Cliente No.", " "),
                                codigoArticulo = rsDocumento.Fields.Item(5).Value.ToString(),
                                nombreArticulo = rsDocumento.Fields.Item(6).Value.ToString(),
                                entregado = int.Parse(rsDocumento.Fields.Item(7).Value.ToString())
                            });
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<reporteCarteraCliente> GetReporteCarteraClienteRecibos(string CardCode)
        {
            List<reporteCarteraCliente> reporte = new List<reporteCarteraCliente>();

            try
            {
                string query = QueryBusquedaRemisionesRecibo(CardCode);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        reporte.Add(calculaReporteCarteraCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(0).Value.ToString()))));
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<reporteKardex> GetReporteKardexCliente(DateTime desdeRemision, DateTime hastaRemision)
        {
            List<reporteKardex> reporte = new List<reporteKardex>();

            try
            {
                string query = QueryBusquedaRemisiones(desdeRemision, hastaRemision);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        reporte.AddRange(calculaReporteKardexCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(1).Value.ToString()))));
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<reporteKardex> GetReporteKardexCliente(DateTime desdeRemision, DateTime hastaRemision, string cliente)
        {
            List<reporteKardex> reporte = new List<reporteKardex>();

            try
            {
                string query = QueryBusquedaRemisiones(desdeRemision, hastaRemision);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        reporte.AddRange(calculaReporteKardexCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(1).Value.ToString()))));
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte.Where(x => x.codigoSocioNegocio == cliente).ToList();
        }

        public static List<reporteKardex> GetReporteKardexProveedor(DateTime desdeRemision, DateTime hastaRemision)
        {
            List<reporteKardex> reporte = new List<reporteKardex>();

            try
            {
                string query = QueryBusquedaRemisiones(desdeRemision, hastaRemision);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        reporte.AddRange(calculaReporteKardexCliente(GetRemision(int.Parse(rsDocumento.Fields.Item(1).Value.ToString()))));
                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reporte;
        }

        public static List<Almacen> GetAlmacenes()
        {
            return BusinessInventario.ListarAlmacenes();
        }

        public static List<Articulo> GetItems(string familia)
        {
            return BusinessInventario.ListarArticulos(familia);
        }



        private static reporteCarteraCliente calculaReporteCarteraCliente(Remision remision)
        {
            reporteCarteraCliente reporte = null;

            foreach (LineasRemision item in remision.Lineas)
            {
                reporte = new reporteCarteraCliente()
               {
                   numeroDocumento = remision.docNum.ToString(),
                   fecha = remision.docDate.ToString("yyyy-MM-dd"),
                   codigoSocioNegocio = remision.CardCode,
                   socioNegocio = remision.CardName,
                   nombreVendedor = GetNombreVendedor(remision.SlpCode),
                   codigoArticulo = item.itemCode,
                   nombreArticulo = item.itemName,
                   entregado = item.quantity,
                   retornado = GetCantidadRetornada(item),
                   //enReacondicionamiento = item.lineasEntradaManual.Where(x => x.objType.Equals(91)).Sum(x => x.quantity),
                   saldo = item.quantity - GetCantidadRetornada(item)
                   //item.lineasEntradaManual.Where(x => x.objType.Equals(91)).Sum(x => x.quantity) - item.lineasEntradaManual.Where(x => x.objType.Equals(92)).Sum(x => x.quantity)
               };
            }


            return reporte;
        }

        private static List<reporteKardex> calculaReporteKardexCliente(Remision remision)
        {
            List<reporteKardex> reporteBorrador = new List<reporteKardex>();
            List<reporteKardex> reporte = new List<reporteKardex>();

            foreach (LineasRemision item in remision.Lineas)
            {
                reporteBorrador.Add(new reporteKardex()
                {
                    tipoDocumento = "Remisión",
                    fechaDocumento = remision.docDate.ToString("yyyy-MM-dd"),
                    fecha = remision.docDate,
                    numeroDocumento = remision.docNum.ToString(),
                    codigoSocioNegocio = remision.CardCode,
                    socioNegocio = remision.CardName,
                    codigoArticulo = item.itemCode,
                    nombreArticulo = item.itemName,
                    entradas = 0,
                    salidas = item.quantity,
                    saldo = 0
                });

                foreach (LineasFactura lineasFactura in item.lineasFactura)
                {
                    if (lineasFactura.lineasNotaCredito.Count() > 0)
                    {
                        foreach (LineasNotaCredito lineasNC in lineasFactura.lineasNotaCredito)
                        {
                            reporteBorrador.Add(new reporteKardex()
                            {
                                tipoDocumento = "Nota Crédito",
                                fechaDocumento = lineasNC.docDate.ToString("yyyy-MM-dd"),
                                fecha = lineasNC.docDate,
                                numeroDocumento = remision.docNum.ToString(),
                                numeroFactura = lineasFactura.docNum.ToString(),
                                fechaFactura = lineasFactura.docDate.ToString("yyyy-MM-dd"),
                                codigoSocioNegocio = remision.CardCode,
                                socioNegocio = remision.CardName,
                                codigoArticulo = item.itemCode,
                                nombreArticulo = item.itemName,
                                entradas = lineasNC.quantity,
                                salidas = 0,
                                saldo = 0
                            });
                        }
                    }
                }

                foreach (LineasEntradaManual lineaEM in item.lineasEntradaManual)
                {
                    if (lineaEM.objType == 90)
                        reporteBorrador.Add(new reporteKardex()
                        {
                            tipoDocumento = "Entrada manual",
                            fechaDocumento = lineaEM.fechaReciboCliente.ToString("yyyy-MM-dd"),
                            fecha = lineaEM.fechaReciboCliente,
                            numeroDocumento = lineaEM.numReciboCliente,
                            codigoSocioNegocio = remision.CardCode,
                            socioNegocio = remision.CardName,
                            codigoArticulo = item.itemCode,
                            nombreArticulo = item.itemName,
                            entradas = lineaEM.quantity,
                            salidas = 0,
                            saldo = 0
                        });
                }
            }

            reporte.AddRange(reporteBorrador.OrderBy(x => x.fecha).ToList());

            int entradas = 0, salidas = 0;
            foreach (reporteKardex item in reporte)
            {
                entradas += item.entradas;
                salidas += item.salidas;
                item.saldo = salidas - entradas;
            }

            return reporte;
        }

        private static List<reporteKardex> calculaReporteKardexProveedor(Remision remision)
        {
            List<reporteKardex> reporteBorrador = new List<reporteKardex>();
            List<reporteKardex> reporte = new List<reporteKardex>();

            foreach (LineasRemision item in remision.Lineas)
            {
                foreach (LineasEntradaManual lineaEM in item.lineasEntradaManual)
                {
                    if (lineaEM.objType == 91)
                        reporteBorrador.Add(new reporteKardex()
                        {
                            tipoDocumento = "Salida a Reacondicionamiento",
                            fechaDocumento = lineaEM.fechaReciboProveedor.ToString("yyyy-MM-dd"),
                            fecha = lineaEM.fechaReciboCliente,
                            numeroDocumento = lineaEM.numReciboCliente,
                            codigoSocioNegocio = remision.CardCode,
                            socioNegocio = remision.CardName,
                            codigoArticulo = item.itemCode,
                            nombreArticulo = item.itemName,
                            entradas = 0,
                            salidas = lineaEM.quantity,
                            saldo = 0
                        });

                    if (lineaEM.objType == 92)
                        reporteBorrador.Add(new reporteKardex()
                        {
                            tipoDocumento = "Entrada de Reacondicionamiento",
                            fechaDocumento = lineaEM.fechaReciboProveedor.ToString("yyyy-MM-dd"),
                            fecha = lineaEM.fechaReciboCliente,
                            numeroDocumento = lineaEM.numReciboCliente,
                            codigoSocioNegocio = remision.CardCode,
                            socioNegocio = remision.CardName,
                            codigoArticulo = item.itemCode,
                            nombreArticulo = item.itemName,
                            entradas = lineaEM.quantity,
                            salidas = 0,
                            saldo = 0
                        });
                }
            }

            reporte.AddRange(reporteBorrador.OrderBy(x => x.fecha).ToList());

            int entradas = 0, salidas = 0;
            foreach (reporteKardex item in reporte)
            {
                entradas += item.entradas;
                salidas += item.salidas;
                item.saldo = salidas - entradas;
            }

            return reporte;
        }

        private static int GetCantidadRetornada(LineasRemision lineaRemision)
        {
            int retornado = 0;
            retornado = lineaRemision.lineasEntradaManual.Where(x => x.objType.Equals(90)).Sum(x => x.quantity);

            foreach (LineasFactura lineaFactura in lineaRemision.lineasFactura)
                retornado += lineaFactura.lineasNotaCredito.Sum(x => x.quantity);

            return retornado;
        }

        private static string GetNombreVendedor(string SlpCode)
        {
            string res = "";
            try
            {
                string query = string.Format("select SlpName from OSLP where SlpCode = {0}", SlpCode);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query);

                if (rsDocumento.RecordCount > 0)
                {
                    rsDocumento.MoveFirst();
                    res = rsDocumento.Fields.Item(0).Value.ToString();

                }
            }
            catch (Exception)
            {

            }

            return res;
        }

        private static List<LineasRemision> GetLineasRemision(int DocEntry)
        {
            List<LineasRemision> Lineas = new List<LineasRemision>();

            try
            {
                string query = QueryDetalleRemision(DocEntry.ToString());

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query.ToString());

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        Lineas.Add(new LineasRemision()
                        {
                            lineNum = int.Parse(rsDocumento.Fields.Item(0).Value.ToString()),
                            itemCode = rsDocumento.Fields.Item(1).Value.ToString(),
                            itemName = rsDocumento.Fields.Item(2).Value.ToString(),
                            quantity = int.Parse(rsDocumento.Fields.Item(3).Value.ToString()),
                            lineasFactura = GetLineasFacturas(DocEntry.ToString(), rsDocumento.Fields.Item(0).Value.ToString()),
                            lineasEntradaManual = GetLineasEntradasManuales(DocEntry.ToString(), rsDocumento.Fields.Item(0).Value.ToString())
                        });

                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {

            }

            return Lineas;
        }

        private static List<LineasFactura> GetLineasFacturas(string baseEntry, string baseLine)
        {
            List<LineasFactura> Lineas = new List<LineasFactura>();

            try
            {
                string query = QueryDocumentosRelacionados(TipoDocumento.Factura, baseEntry, baseLine);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query.ToString());

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        Lineas.Add(new LineasFactura()
                        {
                            baseEntry = int.Parse(baseEntry),
                            baseLine = int.Parse(baseLine),
                            docEntry = int.Parse(rsDocumento.Fields.Item(0).Value.ToString()),
                            docNum = int.Parse(rsDocumento.Fields.Item(1).Value.ToString()),
                            lineNum = int.Parse(rsDocumento.Fields.Item(2).Value.ToString()),
                            itemCode = rsDocumento.Fields.Item(3).Value.ToString(),
                            quantity = int.Parse(rsDocumento.Fields.Item(4).Value.ToString()),
                            baseType = int.Parse(rsDocumento.Fields.Item(5).Value.ToString()),
                            docDate = DateTime.Parse(rsDocumento.Fields.Item(6).Value.ToString()),
                            lineasNotaCredito = GetLineasNotasCredito(rsDocumento.Fields.Item(0).Value.ToString(), rsDocumento.Fields.Item(2).Value.ToString())
                        });

                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {

            }

            return Lineas;
        }

        private static List<LineasNotaCredito> GetLineasNotasCredito(string baseEntry, string baseLine)
        {
            List<LineasNotaCredito> Lineas = new List<LineasNotaCredito>();

            try
            {
                string query = QueryDocumentosRelacionados(TipoDocumento.NotaCr, baseEntry, baseLine);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query.ToString());

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        Lineas.Add(new LineasNotaCredito()
                        {
                            baseEntry = int.Parse(baseEntry),
                            baseLine = int.Parse(baseLine),
                            docEntry = int.Parse(rsDocumento.Fields.Item(0).Value.ToString()),
                            docNum = int.Parse(rsDocumento.Fields.Item(1).Value.ToString()),
                            lineNum = int.Parse(rsDocumento.Fields.Item(2).Value.ToString()),
                            itemCode = rsDocumento.Fields.Item(3).Value.ToString(),
                            quantity = int.Parse(rsDocumento.Fields.Item(4).Value.ToString()),
                            baseType = int.Parse(rsDocumento.Fields.Item(5).Value.ToString()),
                            docDate = DateTime.Parse(rsDocumento.Fields.Item(6).Value.ToString()),
                        });

                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {

            }

            return Lineas;
        }

        private static List<LineasEntradaManual> GetLineasEntradasManuales(string baseEntry, string baseLine)
        {
            List<LineasEntradaManual> Lineas = new List<LineasEntradaManual>();

            try
            {
                string query = QueryEntradasManualesRelacionadas(baseEntry, baseLine);

                Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                rsDocumento.DoQuery(query.ToString());

                if (rsDocumento.RecordCount > 0)
                {
                    while (!rsDocumento.EoF)
                    {
                        Lineas.Add(new LineasEntradaManual()
                        {
                            code = rsDocumento.Fields.Item(0).Value.ToString(),
                            objType = int.Parse(rsDocumento.Fields.Item(1).Value.ToString()),
                            objTypeName = rsDocumento.Fields.Item(2).Value.ToString(),
                            baseEntry = int.Parse(baseEntry),
                            baseLine = int.Parse(baseLine),
                            quantity = int.Parse(rsDocumento.Fields.Item(5).Value.ToString()),
                            numReciboCliente = rsDocumento.Fields.Item(6).Value.ToString(),
                            fechaReciboCliente = DateTime.Parse(rsDocumento.Fields.Item(7).Value.ToString()),
                            cardCode = rsDocumento.Fields.Item(8).Value.ToString(),
                            numReciboProveedor = rsDocumento.Fields.Item(9).Value.ToString(),
                            fechaReciboProveedor = DateTime.Parse(rsDocumento.Fields.Item(10).Value.ToString()),
                            costoReacondic = int.Parse(rsDocumento.Fields.Item(11).Value.ToString()),
                            itemsBaja = int.Parse(rsDocumento.Fields.Item(12).Value.ToString()),
                            observaciones = rsDocumento.Fields.Item(13).Value.ToString()
                        });

                        rsDocumento.MoveNext();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Lineas;
        }

        private static string QueryCabeceraRemision(string DocNum)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("select docentry, docdate, CardCode, CardName, SlpCode ");
            query.AppendLine(string.Format("from ODLN where DocNum = {0} ", DocNum));

            return query.ToString();
        }

        private static string QueryDetalleRemision(string DocEntry)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("select a.LineNum, a.ItemCode, b.ItemName, a.PackQty ");
            query.AppendLine("from DLN1 a inner join OITM b on a.ItemCode = b.ItemCode ");
            query.AppendLine(string.Format("where a.DocEntry = {0} and a.U_CSS_ENVASEDEVOL = 'SI'", DocEntry));

            return query.ToString();
        }

        private static string QueryDocumentosRelacionados(TipoDocumento tipoDocumento, string baseEntry, string baseLine)
        {
            string maestro = "", detalle = "", baseType = "";

            switch (tipoDocumento)
            {
                case TipoDocumento.Factura:
                    maestro = "OINV";
                    detalle = "INV1";
                    baseType = "15";
                    break;
                case TipoDocumento.NotaCr:
                    maestro = "ORIN";
                    detalle = "RIN1";
                    baseType = "13";
                    break;
                default:
                    break;
            }

            StringBuilder query = new StringBuilder();

            query.AppendLine("select a.DocEntry, a.DocNum, b.LineNum, b.ItemCode, b.PackQty, b.BaseType, a.docdate ");
            query.AppendLine(string.Format("from {0} a inner join {1} b on a.DocEntry = b.DocEntry ", maestro, detalle));
            query.AppendLine(string.Format("where BaseType = {0} and b.BaseEntry = {1} and b.BaseLine = {2} ", baseType, baseEntry, baseLine));
            return query.ToString();
        }

        private static string QueryEntradasManualesRelacionadas(string baseEntry, string baseLine)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("select ");
            query.AppendLine("code, U_objType, ");
            query.AppendLine("case U_objType  ");
            query.AppendLine("when 90 then 'Entrada manual' ");
            query.AppendLine("when 91 then 'Entrada reacondicionamiento' ");
            query.AppendLine("when 92 then 'Salida reacondicionamiento' ");
            query.AppendLine("end objTypeName, ");
            query.AppendLine("U_baseEntry, U_baseLine, U_quantity, U_numReciboCliente, ");
            query.AppendLine("U_fechaReciboCliente, U_cardCode, U_numReciboProveedor, ");
            query.AppendLine("U_fechaReciboProv, U_costoReacondic, U_itemsBaja, U_observaciones ");
            query.AppendLine("from [@ORK_ENVASE_DEV] ");
            query.AppendLine(string.Format("where U_baseEntry = {0} and U_baseLine = {1}", baseEntry, baseLine));
            return query.ToString();
        }

        private static string QueryBusquedaRemisiones()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("select distinct a.DocEntry, a.DocNum, a.DocDate ");
            query.AppendLine("from ODLN a inner join DLN1 b on a.DocEntry = b.DocEntry ");
            query.AppendLine("where b.U_CSS_ENVASEDEVOL = 'SI' and a.DocDate >= (select U_inicioEnvDev from [@ORK_ENV_DEV_INICIO]) ");
            query.AppendLine("order by a.DocNum");

            return query.ToString();
        }

        private static string QueryBusquedaRemisiones(DateTime desde, DateTime hasta)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("select distinct a.DocEntry, a.DocNum ");
            query.AppendLine("from ODLN a inner join DLN1 b on a.DocEntry = b.DocEntry ");
            query.AppendLine("where b.U_CSS_ENVASEDEVOL = 'SI' and a.DocDate >= (select U_inicioEnvDev from [@ORK_ENV_DEV_INICIO]) ");
            query.AppendLine(string.Format("and (a.DocDate between '{0}' and '{1}') ", desde.ToString("yyyy-MM-dd"), hasta.ToString("yyyy-MM-dd")));
            query.AppendLine("order by a.DocNum");

            return query.ToString();
        }

        private static string QueryBusquedaRemisiones(DateTime desde, DateTime hasta, string cardCode)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("select distinct a.DocEntry, a.DocNum  ");
            query.AppendLine("from odln a inner join DLN1 b on a.DocEntry = b.DocEntry left join  [@ORK_ENVASE_DEV] c on  a.DocEntry = c.U_baseEntry ");
            query.AppendLine("where b.U_CSS_ENVASEDEVOL = 'SI' and a.DocDate >= (select U_inicioEnvDev from [@ORK_ENV_DEV_INICIO]) ");
            query.AppendLine(string.Format("and (a.DocDate between '{0}' and '{1}') ", desde.ToString("yyyy-MM-dd"), hasta.ToString("yyyy-MM-dd")));
            query.AppendLine(string.Format("and c.U_cardCode = '{0}'", cardCode));
            query.AppendLine("order by a.DocNum");

            return query.ToString();
        }

        private static string QueryBusquedaRemisionesRecibo(DateTime desde, DateTime hasta)
        {
            StringBuilder query = new StringBuilder();
            //query.AppendLine("select distinct b.DocNum ");
            //query.AppendLine("from [@ORK_ENVASE_DEV] a inner join ODLN b on a.U_baseEntry = b.DocEntry ");
            //query.AppendLine(string.Format("where a.U_fechaReciboProv between '{0}' and '{1}'", desde.ToString("yyyy-MM-dd"), hasta.ToString("yyyy-MM-dd")));

            query.AppendLine("select a.docdate, a.docnum, a.cardCode, a.cardName, a.comments, b.itemCode, c.itemName, b.quantity from opdn a ");
            query.AppendLine("inner join pdn1 b on a.docentry = b.docentry ");
            query.AppendLine("inner join oitm c on b.itemcode = c.itemcode ");
            query.AppendLine(string.Format("where a.series = 272 and a.docdate between '{0}' and '{1}'", desde.ToString("yyyy-MM-dd"), hasta.ToString("yyyy-MM-dd")));

            return query.ToString();
        }

        private static string QueryBusquedaRemisionesRecibo(DateTime desde, DateTime hasta, string cardCode)
        {
            StringBuilder query = new StringBuilder();
            //query.AppendLine("select distinct b.DocNum ");
            //query.AppendLine("from [@ORK_ENVASE_DEV] a inner join ODLN b on a.U_baseEntry = b.DocEntry ");
            //query.AppendLine(string.Format("where a.U_fechaReciboProv between '{0}' and '{1}' and a.U_cardcode = '{2}'", desde.ToString("yyyy-MM-dd"), hasta.ToString("yyyy-MM-dd"), cardCode));

            query.AppendLine("select a.docdate, a.docnum, a.cardCode, a.cardName, a.comments, b.itemCode, c.itemName, b.quantity from opdn a ");
            query.AppendLine("inner join pdn1 b on a.docentry = b.docentry ");
            query.AppendLine("inner join oitm c on b.itemcode = c.itemcode ");
            query.AppendLine(string.Format("where a.series = 272 and a.cardCode = '{0}' and a.docdate between '{1}' and '{2}'", cardCode, desde.ToString("yyyy-MM-dd"), hasta.ToString("yyyy-MM-dd")));

            return query.ToString();
        }

        private static string QueryBusquedaRemisionesRecibo(string CardCode)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("select distinct b.DocNum ");
            query.AppendLine("from [@ORK_ENVASE_DEV] a inner join ODLN b on a.U_baseEntry = b.DocEntry ");
            query.AppendLine(string.Format("where a.U_cardCode = '{0}'", CardCode));

            return query.ToString();
        }



        private static string GetNextLineNum()
        {
            int noDoc = -1;

            try
            {
                Recordset rs = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                StringBuilder query = new StringBuilder(string.Format("select isnull(MAX (cast(code as int)), 0) + 1 cuenta "));
                query.Append("FROM [@ORK_ENVASE_DEV] ");

                rs.DoQuery(query.ToString());
                rs.MoveFirst();

                noDoc = Convert.ToInt32(rs.Fields.Item(0).Value.ToString());
            }
            catch (Exception)
            {
                throw;
            }

            return noDoc.ToString();
        }
    }

    public enum TipoDocumento { Factura, NotaCr }

    public class Documento
    {
        public int docEntry { get; set; }
        public int docNum { get; set; }
        public DateTime docDate { get; set; }
    }

    public class ItemDocumento
    {
        public int lineNum { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
    }

    public class Remision : Documento
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string SlpCode { get; set; }
        public List<LineasRemision> Lineas { get; set; }

        public Remision()
        {
            Lineas = new List<LineasRemision>();
        }
    }

    public class LineasRemision : ItemDocumento
    {
        public List<LineasFactura> lineasFactura { get; set; }
        public List<LineasEntradaManual> lineasEntradaManual { get; set; }

        public LineasRemision()
        {
            lineasFactura = new List<LineasFactura>();
            lineasEntradaManual = new List<LineasEntradaManual>();
        }
    }

    public class LineasFactura : ItemDocumento
    {
        public int baseType { get; set; }
        public int baseEntry { get; set; }
        public int baseLine { get; set; }
        public int docEntry { get; set; }
        public int docNum { get; set; }
        public DateTime docDate { get; set; }
        public List<LineasNotaCredito> lineasNotaCredito { get; set; }

        public LineasFactura()
        {
            lineasNotaCredito = new List<LineasNotaCredito>();
        }
    }


    public class LineasNotaCredito : ItemDocumento
    {
        public int baseType { get; set; }
        public int baseEntry { get; set; }
        public int baseLine { get; set; }
        public int docEntry { get; set; }
        public int docNum { get; set; }
        public DateTime docDate { get; set; }
    }

    public class LineasEntradaManual
    {
        public string code { get; set; }
        public string name { get; set; }

        public int objType { get; set; }
        public string objTypeName { get; set; }
        public int baseEntry { get; set; }
        public int baseLine { get; set; }
        public int quantity { get; set; }

        public string numReciboCliente { get; set; }
        public DateTime fechaReciboCliente { get; set; }

        public string cardCode { get; set; }
        public string numReciboProveedor { get; set; }
        public DateTime fechaReciboProveedor { get; set; }
        public int costoReacondic { get; set; }
        public int itemsBaja { get; set; }
        public string observaciones { get; set; }
    }

    public class Proveedor
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
    }

    public class reporteCarteraCliente
    {
        public string fecha { get; set; }
        public string numeroDocumento { get; set; }
        public string codigoSocioNegocio { get; set; }
        public string socioNegocio { get; set; }
        //public string codigoProveedor { get; set; }
        //public string proveedor { get; set; }
        public string nombreVendedor { get; set; }
        public string codigoArticulo { get; set; }
        public string nombreArticulo { get; set; }
        public int entregado { get; set; }
        public int retornado { get; set; }
        public int enReacondicionamiento { get; set; }
        public int saldo { get; set; }
    }

    public class reporteKardex
    {
        public string tipoDocumento { get; set; }
        public DateTime fecha { get; set; }
        public string fechaDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaFactura { get; set; }
        public string numeroFactura { get; set; }
        public string codigoSocioNegocio { get; set; }
        public string socioNegocio { get; set; }
        public string bodega { get; set; }
        public string codigoArticulo { get; set; }
        public string nombreArticulo { get; set; }
        public int entradas { get; set; }
        public int salidas { get; set; }
        public int saldo { get; set; }
    }
}

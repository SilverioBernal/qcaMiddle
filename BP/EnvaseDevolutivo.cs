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
        public DateTime inicioEnvaseDevolutivo { get; set; }
        public DateTime ultimaSincronizacion { get; set; }

        public EnvaseDevolutivo()
        {
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            string consultaDoc = "select U_inicioEnvDev, U_ultSincronizacion from [@ORK_ENV_DEV_INICIO]";

            rsDocumento.DoQuery(consultaDoc.ToString());

            if (rsDocumento.RecordCount > 0)
            {
                rsDocumento.MoveFirst();

                inicioEnvaseDevolutivo = (DateTime)rsDocumento.Fields.Item(0).Value;
                ultimaSincronizacion = (DateTime)rsDocumento.Fields.Item(1).Value;
            }
        }

        #region Estructura antigua
        //public Boolean EliminarDocumento(int numeroDocumento)
        //{
        //    Boolean bResultado = false;

        //    try
        //    {
        //        //Eliminar detalles
        //        //String sQuery = "delete from [@ORK_ENV_DEV_DETALLE] where (U_idMaestro = " + numeroDocumento.ToString() + ")";
        //        //ClaseDatos.nonQuery(sQuery);

        //        //Eliminar encabezado
        //        //sQuery = "delete from [@ORK_ENV_DEV_MASTER] where (code = " + numeroDocumento.ToString() + ")";
        //        //ClaseDatos.nonQuery(sQuery);




        //        bResultado = true;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }



        //    return (bResultado);
        //}

        //public int CrearDocumento(ORK_ENV_DEV_MASTER Documento)
        //{
        //    int noDoc = 0;
        //    UserTable oORK_ENV_DEV_MASTER = ClaseDatos.objCompany.UserTables.Item("ORK_ENV_DEV_MASTER");

        //    string masterCode = ObtenerSiguienteLinea("ORK_ENV_DEV_MASTER");

        //    oORK_ENV_DEV_MASTER.Code = masterCode;
        //    oORK_ENV_DEV_MASTER.Name = masterCode;

        //    oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_fecha").Value = Documento.U_fecha;
        //    oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_objType").Value = Documento.U_objType;
        //    oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_usuario").Value = Documento.U_usuario;
        //    oORK_ENV_DEV_MASTER.UserFields.Fields.Item("U_socioNegocio").Value = Documento.U_socioNegocio;

        //    noDoc = oORK_ENV_DEV_MASTER.Add();

        //    foreach (ORK_ENV_DEV_DETALLE linea in Documento.detalleEnvaseDevolutivo)
        //    {
        //        UserTable oORK_ENV_DEV_DETALLE = ClaseDatos.objCompany.UserTables.Item("ORK_ENV_DEV_DETALLE");

        //        string detailCode = ObtenerSiguienteLinea("oORK_ENV_DEV_DETALLE");

        //        oORK_ENV_DEV_DETALLE.Code = detailCode;
        //        oORK_ENV_DEV_DETALLE.Name = detailCode;

        //        oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_idMaestro").Value = noDoc;
        //        oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_tipoMovimiento").Value = linea.U_tipoMovimiento;
        //        oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_itemCode").Value = linea.U_itemCode;
        //        oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_cantidad").Value = linea.U_cantidad;
        //        oORK_ENV_DEV_DETALLE.UserFields.Fields.Item("U_ubicacion").Value = linea.U_ubicacion;

        //        oORK_ENV_DEV_DETALLE.Add();
        //    }

        //    return noDoc;
        //}

        //public ORK_ENV_DEV_MASTER ConsultarDocumento(int code)
        //{
        //    Recordset miRecordSet = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

        //    StringBuilder miSentencia = new StringBuilder("SELECT  T0.Code, T0.Name, T0.U_fecha, T0.U_objType, T0.U_usuario, T0.U_socioNegocio, ");
        //    miSentencia.Append("T1.code As CodDetalle, T1.name as NameDetalle, T1.U_idMaestro, T1.U_tipoMovimiento, T1.U_itemcode, T1.U_cantidad, T1.U_ubicacion ");
        //    miSentencia.Append("FROM [@ORK_ENV_DEV_MASTER] T0 ");
        //    miSentencia.Append("INNER JOIN [@ORK_ENV_DEV_MASTER] T1 ");
        //    miSentencia.Append("ON T0.code = T1.U_idMaestro ");
        //    miSentencia.Append(string.Format("where T0.Code = {0}", code));

        //    ClaseDatos.objCompany.StartTransaction();

        //    miRecordSet.DoQuery(miSentencia.ToString());

        //    ORK_ENV_DEV_MASTER Documento = new ORK_ENV_DEV_MASTER();

        //    if (miRecordSet.RecordCount > 0)
        //    {
        //        miRecordSet.MoveFirst();

        //        for (int i = 0; i < miRecordSet.RecordCount; i++)
        //        {

        //            if (!string.IsNullOrEmpty(miRecordSet.Fields.Item(0).ToString()))
        //            {
        //                if (i == 0)
        //                {
        //                    Documento.Code = Convert.ToInt32(miRecordSet.Fields.Item(0).ToString());
        //                    Documento.Name = miRecordSet.Fields.Item(1).ToString();
        //                    Documento.U_fecha = Convert.ToDateTime(miRecordSet.Fields.Item(2).ToString());
        //                    Documento.U_objType = miRecordSet.Fields.Item(3).ToString();
        //                    Documento.U_usuario = miRecordSet.Fields.Item(4).ToString();
        //                    Documento.U_socioNegocio = miRecordSet.Fields.Item(5).ToString();
        //                }
        //                else
        //                {
        //                    ORK_ENV_DEV_DETALLE linea = new ORK_ENV_DEV_DETALLE();

        //                    linea.Code = Convert.ToInt32(miRecordSet.Fields.Item(6).ToString());
        //                    linea.Name = miRecordSet.Fields.Item(7).ToString();
        //                    linea.U_idMaestro = Convert.ToInt32(miRecordSet.Fields.Item(8).ToString());
        //                    linea.U_tipoMovimiento = miRecordSet.Fields.Item(9).ToString();
        //                    linea.U_itemCode = miRecordSet.Fields.Item(10).ToString();
        //                    linea.U_cantidad = Convert.ToInt32(miRecordSet.Fields.Item(11).ToString());
        //                    linea.U_ubicacion = Convert.ToInt32(miRecordSet.Fields.Item(12).ToString());
        //                    Documento.detalleEnvaseDevolutivo.Add(linea);
        //                }
        //            }

        //            miRecordSet.MoveNext();
        //        }
        //    }

        //    return Documento;
        //} 
        #endregion

        /* Nueva estructura */
        public int Create(ItemDetail Documento)
        {
            int noDoc = 0;
            string masterCode = "";

            try
            {
                ClaseDatos.objCompany.StartTransaction();

                UserTable objORK_ENVASE_DEV = ClaseDatos.objCompany.UserTables.Item("ORK_ENVASE_DEV");

                masterCode = GetNextLineNum("Code", 0);

                objORK_ENVASE_DEV.Code = masterCode;
                objORK_ENVASE_DEV.Name = masterCode;

                objORK_ENVASE_DEV.UserFields.Fields.Item("U_objType").Value = Documento.U_objType;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_docEntry").Value = Documento.U_docEntry;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_docNum").Value = Documento.U_docNum;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_itemCode").Value = Documento.U_itemCode;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_delivered").Value = Documento.U_delivered;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_returned").Value = Documento.U_returned;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_maintenance").Value = Documento.U_maintenance;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_ready").Value = Documento.U_ready;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_targetType").Value = Documento.U_targetType;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_targetDocNum").Value = Documento.U_targetDocNum;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_targetDocEntry").Value = Documento.U_targetDocEntry;


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

        public DocumentSummary Search(int objType, int docNum)
        {
            DocumentSummary documento = new DocumentSummary();

            #region Consulta de info relacionada al documento
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            StringBuilder consultaDoc = new StringBuilder();

            consultaDoc.Append("SELECT ");
            consultaDoc.Append("T0.Code, ");
            consultaDoc.Append("T0.Name, ");
            consultaDoc.Append("T0.U_date, ");
            consultaDoc.Append("T0.U_objType ");
            consultaDoc.Append("T0.U_docNum ");
            consultaDoc.Append("T0.U_itemCode ");
            consultaDoc.Append("T0.U_delivered ");
            consultaDoc.Append("T0.U_returned ");
            consultaDoc.Append("T0.U_maintenance ");
            consultaDoc.Append("T0.U_ready ");
            consultaDoc.Append("T0.U_targetType ");
            consultaDoc.Append("T0.U_targetDocNum ");
            consultaDoc.Append("FROM [@ORK_ENVASE_DEV] T0 ");
            consultaDoc.Append(string.Format("where T0.U_objType = {0} and T0.U_docNum = {1}", objType, docNum));

            //ClaseDatos.objCompany.StartTransaction();

            rsDocumento.DoQuery(consultaDoc.ToString());
            #endregion

            #region Formateo de la informacion encontrada

            List<ItemDetail> DetalleDocumento = new List<ItemDetail>();

            if (rsDocumento.RecordCount > 0)
            {


                while (!rsDocumento.EoF)
                {
                    DetalleDocumento.Add(new ItemDetail()
                    {
                        Code = rsDocumento.Fields.Item(0).ToString(),
                        Name = rsDocumento.Fields.Item(1).ToString(),
                        //U_date = (DateTime)rsDocumento.Fields.Item(2).Value,
                        U_objType = (int)rsDocumento.Fields.Item(3).Value,
                        U_docNum = rsDocumento.Fields.Item(4).ToString(),
                        U_itemCode = rsDocumento.Fields.Item(5).ToString(),
                        U_delivered = (int)rsDocumento.Fields.Item(6).Value,
                        U_returned = (int)rsDocumento.Fields.Item(7).Value,
                        U_maintenance = (int)rsDocumento.Fields.Item(8).Value,
                        U_ready = (int)rsDocumento.Fields.Item(9).Value,
                        U_targetType = (int)rsDocumento.Fields.Item(10).Value,
                        U_targetDocNum = rsDocumento.Fields.Item(11).ToString(),
                    });
                }
            }

            List<string> lsItems = DetalleDocumento.Select(x => x.U_itemCode).Distinct().ToList();

            documento.objType = DetalleDocumento[0].U_objType;
            documento.docNum = DetalleDocumento[0].U_docNum;

            foreach (string item in lsItems)
            {
                ItemSummary resumenItem = new ItemSummary();

                resumenItem.U_itemCode = item;
                resumenItem.U_delivered = DetalleDocumento.Where(x => x.U_itemCode == item).Sum(x => x.U_delivered);
                resumenItem.U_returned = DetalleDocumento.Where(x => x.U_itemCode == item).Sum(x => x.U_returned);
                resumenItem.U_maintenance = DetalleDocumento.Where(x => x.U_itemCode == item).Sum(x => x.U_maintenance);
                resumenItem.U_ready = DetalleDocumento.Where(x => x.U_itemCode == item).Sum(x => x.U_ready);

                resumenItem.lsDetalleItems.AddRange(DetalleDocumento.Where(x => x.U_itemCode == item).ToList());

                documento.lsItems.Add(resumenItem);
            }
            #endregion

            return documento;
        }

        public List<MktDocHeader> Search(int objType)
        {
            #region Consulta de info relacionada al documento
            string maestroEntrega = "", detalleEntrega = "", maestroDevolucion = "", detalleDevolucion = "", maestroNotaCr = "", detalleNotaCr = "";

            switch (objType)
            {
                case 15:
                    maestroEntrega = "ODLN";
                    detalleEntrega = "DLN1";
                    maestroDevolucion = "ORDN";
                    detalleDevolucion = "RDN1";
                    maestroNotaCr = "ORIN";
                    detalleNotaCr = "RIN1";
                    break;
                case 20:
                    maestroEntrega = "OPDN";
                    detalleEntrega = "PDN1";
                    maestroDevolucion = "ORPD";
                    detalleDevolucion = "RPD1";
                    maestroNotaCr = "ORPC";
                    detalleNotaCr = "RPC1";
                    break;
                default:
                    break;
            }
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            string queryDoc = GetHeaderQuery(maestroEntrega, detalleEntrega, "", "");

            rsDocumento.DoQuery(queryDoc);

            #endregion

            #region Formateo de la informacion encontrada
            List<MktDocHeader> documents = new List<MktDocHeader>();

            if (rsDocumento.RecordCount > 0)
            {
                while (!rsDocumento.EoF)
                {
                    MktDocHeader document = new MktDocHeader()
                    {
                        objType = objType,
                        docNum = rsDocumento.Fields.Item(0).ToString(),
                        docEntry = rsDocumento.Fields.Item(1).ToString()
                    };

                    #region Devoluciones
                    string queryReturns = GetHeaderQuery(maestroDevolucion, detalleDevolucion, objType.ToString(), document.docEntry);

                    Recordset rsReturns = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    rsReturns.DoQuery(queryReturns);

                    if (rsReturns.RecordCount > 0)
                    {
                        while (!rsReturns.EoF)
                        {
                            document.lsReturns.Add(new MktDocHeader()
                            {
                                docNum = rsReturns.Fields.Item(0).ToString(),
                                docEntry = rsReturns.Fields.Item(1).ToString(),
                                objType = (int)rsReturns.Fields.Item(2).Value,
                            });
                        }
                    }
                    #endregion

                    #region Notas
                    string queryCreditNotes = GetHeaderQuery(maestroDevolucion, detalleDevolucion, objType.ToString(), document.docEntry);

                    Recordset rsCreditNotes = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    rsCreditNotes.DoQuery(queryCreditNotes);

                    if (rsCreditNotes.RecordCount > 0)
                    {
                        while (!rsCreditNotes.EoF)
                        {
                            document.lsCreditNote.Add(new MktDocHeader()
                            {
                                docNum = rsCreditNotes.Fields.Item(0).ToString(),
                                docEntry = rsCreditNotes.Fields.Item(1).ToString(),
                                objType = (int)rsCreditNotes.Fields.Item(2).Value,
                            });
                        }
                    }
                    #endregion

                    documents.Add(document);
                }
            }
            #endregion

            return documents;
        }

        public List<MktDocHeader> GetBaseDocumentsForSync(int objType)
        {
            #region Consulta de info relacionada al documento
            string maestroEntrega = "", detalleEntrega = "";

            switch (objType)
            {
                case 15:
                    maestroEntrega = "ODLN";
                    detalleEntrega = "DLN1";
                    break;
                case 20:
                    maestroEntrega = "OPDN";
                    detalleEntrega = "PDN1";
                    break;
                default:
                    break;
            }
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            string queryDoc = GetHeaderQuery(maestroEntrega, detalleEntrega, "", "");

            rsDocumento.DoQuery(queryDoc);

            #endregion

            #region Formateo de la informacion encontrada
            List<MktDocHeader> documents = new List<MktDocHeader>();

            if (rsDocumento.RecordCount > 0)
            {
                while (!rsDocumento.EoF)
                {
                    MktDocHeader document = new MktDocHeader()
                    {
                        objType = objType,
                        docNum = rsDocumento.Fields.Item(0).Value.ToString(),
                        docEntry = rsDocumento.Fields.Item(1).Value.ToString()
                    };
                    documents.Add(document);

                    rsDocumento.MoveNext();
                }
            }
            #endregion

            return documents;
        }

        public bool ExistsBaseDocument(MktDocHeader baseDocument)
        {
            bool res = false;
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            string queryDoc = string.Format("select U_docEntry from [@ORK_ENVASE_DEV] where U_objType = {0} and U_docEntry = {1}", baseDocument.objType, baseDocument.docEntry);

            rsDocumento.DoQuery(queryDoc);

            if (rsDocumento.RecordCount > 0)
                res = true;

            return res;
        }

        public bool SaveBaseDocument(MktDocHeader baseDocument)
        {
            bool res = false;

            string maestroEntrega = "", detalleEntrega = "";

            switch (baseDocument.objType)
            {
                case 15:
                    maestroEntrega = "ODLN";
                    detalleEntrega = "DLN1";
                    break;
                case 20:
                    maestroEntrega = "OPDN";
                    detalleEntrega = "PDN1";
                    break;
                default:
                    break;
            }

            string query = GetBaseDocumentQuery(maestroEntrega, detalleEntrega, baseDocument.docEntry);
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            rsDocumento.DoQuery(query);

            if (rsDocumento.RecordCount > 0)
            {

                while (!rsDocumento.EoF)
                {
                    string valor = rsDocumento.Fields.Item(3).Value.ToString();

                    Create(new ItemDetail()
                    {
                        U_objType = baseDocument.objType,
                        U_docNum = rsDocumento.Fields.Item(1).Value.ToString(),
                        U_itemCode = rsDocumento.Fields.Item(2).Value.ToString(),
                        U_delivered = int.Parse(rsDocumento.Fields.Item(3).Value.ToString()),
                        U_returned = 0,
                        U_maintenance = 0,
                        U_ready = 0,
                        U_docEntry = rsDocumento.Fields.Item(4).Value.ToString(),
                        U_targetDocEntry = "",
                        U_targetDocNum = "",
                        U_targetType = 0
                    });

                    rsDocumento.MoveNext();
                }

                res = true;
            }

            return res;
        }

        //public List<MktDocHeader> GetReturnDocumentsForSync(int baseType, string baseEntry)
        //{
        //    #region Consulta de info relacionada al documento
        //    string maestroDevolucion = "", detalleDevolucion = "";

        //    switch (baseType)
        //    {
        //        case 15:
        //            maestroDevolucion = "ORDN";
        //            detalleDevolucion = "RDN1";
        //            break;
        //        case 20:
        //            maestroDevolucion = "ORPD";
        //            detalleDevolucion = "RPD1";
        //            break;
        //        default:
        //            break;
        //    }
        //    Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

        //    string queryDoc = GetHeaderQuery(maestroDevolucion, detalleDevolucion, baseType.ToString(), baseEntry);

        //    rsDocumento.DoQuery(queryDoc);

        //    #endregion

        //    #region Formateo de la informacion encontrada
        //    List<MktDocHeader> documents = new List<MktDocHeader>();

        //    if (rsDocumento.RecordCount > 0)
        //    {
        //        while (!rsDocumento.EoF)
        //        {
        //            MktDocHeader document = new MktDocHeader()
        //            {                        
        //                docNum = rsDocumento.Fields.Item(0).Value.ToString(),
        //                docEntry = rsDocumento.Fields.Item(1).Value.ToString(),
        //                objType = int.Parse(rsDocumento.Fields.Item(2).Value.ToString())
        //            };
        //            documents.Add(document);

        //            rsDocumento.MoveNext();
        //        }
        //    }
        //    #endregion

        //    return documents;
        //}

        //public List<MktDocHeader> GetCreditNoteDocumentsForSync(int baseType, string baseEntry)
        //{
        //    #region Consulta de info relacionada al documento
        //    string maestroNotaCr = "", detalleNotaCr = "";

        //    switch (baseType)
        //    {
        //        case 15:
        //            maestroNotaCr = "ORIN";
        //            detalleNotaCr = "RIN1";
        //            break;
        //        case 20:
        //            maestroNotaCr = "ORPC";
        //            detalleNotaCr = "RPC1";
        //            break;
        //        default:
        //            break;
        //    }
        //    Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

        //    string queryDoc = GetHeaderQuery(maestroNotaCr, detalleNotaCr, baseType.ToString(), baseEntry);

        //    rsDocumento.DoQuery(queryDoc);

        //    #endregion

        //    #region Formateo de la informacion encontrada
        //    List<MktDocHeader> documents = new List<MktDocHeader>();

        //    if (rsDocumento.RecordCount > 0)
        //    {
        //        while (!rsDocumento.EoF)
        //        {
        //            MktDocHeader document = new MktDocHeader()
        //            {
        //                docNum = rsDocumento.Fields.Item(0).Value.ToString(),
        //                docEntry = rsDocumento.Fields.Item(1).Value.ToString(),
        //                baseEntry = baseEntry,
        //                baseType = baseType,
        //                objType = int.Parse(rsDocumento.Fields.Item(2).Value.ToString())
        //            };
        //            documents.Add(document);

        //            rsDocumento.MoveNext();
        //        }
        //    }
        //    #endregion

        //    return documents;
        //}

        public bool ExistsChildDocument(MktDocHeader document)
        {
            bool res = false;
            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            string queryDoc = string.Format("select U_docEntry from [@ORK_ENVASE_DEV] where U_targetType = {0} and U_targetDocEntry = {1}", document.objType, document.docEntry);

            rsDocumento.DoQuery(queryDoc);

            if (rsDocumento.RecordCount > 0)
                res = true;

            return res;
        }

        public bool SaveCreditNoteDocument(MktDocHeader document)
        {
            bool res = false;

            string invoiceMaster = "", invoiceDetail = "", deliveryMaster = "", creditNoteMaster = "", creditNoteDetail = "";
            int targetType = 0;

            switch (document.objType)
            {
                case 15:
                    invoiceMaster = "OINV";
                    invoiceDetail = "INV1";
                    deliveryMaster = "ODLN";
                    creditNoteMaster = "ORIN";
                    creditNoteDetail = "RIN1";
                    targetType = 14;
                    break;
                case 20:
                    invoiceMaster = "OPCH";
                    invoiceDetail = "PCH1";
                    deliveryMaster = "OPDN";
                    creditNoteMaster = "ORPC";
                    creditNoteDetail = "RPC1";
                    targetType = 19;
                    break;
                default:
                    break;
            }

            string query = GetCreditNoteDocumentQuery(invoiceMaster, invoiceDetail, deliveryMaster, creditNoteMaster, creditNoteDetail, document.objType, targetType, document.docEntry);

            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            rsDocumento.DoQuery(query);

            if (rsDocumento.RecordCount > 0)
            {
                while (!rsDocumento.EoF)
                {
                    if (int.Parse(rsDocumento.Fields.Item(5).Value.ToString()) == 0)
                        Create(new ItemDetail()
                        {
                            U_objType = document.objType,
                            U_docNum = document.docNum,
                            U_itemCode = rsDocumento.Fields.Item(2).Value.ToString(),
                            U_delivered = 0,
                            U_returned = int.Parse(rsDocumento.Fields.Item(3).Value.ToString()),
                            U_maintenance = 0,
                            U_ready = 0,
                            U_docEntry = document.docEntry,
                            U_targetDocEntry = rsDocumento.Fields.Item(4).Value.ToString(),
                            U_targetDocNum = rsDocumento.Fields.Item(1).Value.ToString(),
                            U_targetType = int.Parse(rsDocumento.Fields.Item(0).Value.ToString())
                        });
                    rsDocumento.MoveNext();
                }

                res = true;
            }

            return res;
        }

        /* Utilidades */
        private string GetNextLineNum(string field, int targetObjType)
        {
            int noDoc = -1;

            try
            {
                Recordset rs = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                StringBuilder query = new StringBuilder(string.Format("select isnull(MAX (cast({0} as int)), 0) + 1 cuenta ", field));
                query.Append("FROM [@ORK_ENVASE_DEV] ");

                if (targetObjType != 0)
                    query.Append(string.Format("where U_targetType = {0}", targetObjType));

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

        private string GetHeaderQuery(string master, string detail, string baseType, string baseEntry)
        {
            StringBuilder queryDoc = new StringBuilder();
            queryDoc.Append("SELECT DISTINCT T0.DocNum, T0.DocEntry, T0.DocType ");
            queryDoc.Append(string.Format("FROM {0} T0 ", master));
            queryDoc.Append(string.Format("INNER JOIN {0} T1 ON T0.DocEntry = T1.DocEntry ", detail));
            queryDoc.Append(string.Format("where t0.DocDate >= '{0}' and U_CSS_ENVASEDEVOL = 'SI' ", inicioEnvaseDevolutivo.ToString("yyyy-MM-dd")));

            if (!string.IsNullOrEmpty(baseType))
                queryDoc.Append(string.Format("and BaseType = {0} and BaseEntry = {1}", baseType, baseEntry));

            return queryDoc.ToString();
        }

        private string GetBaseDocumentQuery(string master, string detail, string docEntry)
        {
            StringBuilder queryDoc = new StringBuilder();
            queryDoc.Append("SELECT T0.ObjType, T0.DocNum, T1.ItemCode, T1.PackQty delivered, T0.DocEntry ");
            queryDoc.Append(string.Format("FROM {0} T0 ", master));
            queryDoc.Append(string.Format("INNER JOIN {0} T1 ON T0.DocEntry = T1.DocEntry ", detail));
            queryDoc.Append(string.Format("where T0.DocEntry = {0} and U_CSS_ENVASEDEVOL = 'SI' ", docEntry));

            return queryDoc.ToString();
        }

        private string GetCreditNoteDocumentQuery(string invoiceMaster, string invoiceDetail, string deliveryMaster, string creditNoteMaster, string creditNoteDetail, int baseType, int targetType, string baseEntry)
        {
            StringBuilder queryDoc = new StringBuilder();
            queryDoc.Append("SELECT T3.ObjType, T3.DocNum, T3.ItemCode, T3.PackQty, T3.DocEntry, ");
            queryDoc.Append("(select COUNT(*) from [@ORK_ENVASE_DEV] t6 ");
            queryDoc.Append("where t6.U_docEntry = t1.BaseEntry and t6.U_objType = t1.BaseType and t6.U_targetType = t3.ObjType ");
            queryDoc.Append("and t6.U_targetDocEntry = t3.DocEntry) saved ");
            queryDoc.Append(string.Format("FROM {0} T0 ", invoiceMaster));
            queryDoc.Append(string.Format("INNER JOIN {0} T1 ON T0.DocEntry = T1.DocEntry  ", invoiceDetail));
            queryDoc.Append(string.Format("INNER JOIN {0} T4 on T1.BaseEntry = T4.DocEntry  ", deliveryMaster));
            queryDoc.Append("INNER JOIN (SELECT T10.ObjType, T10.DocEntry, T10.DocNum, T11.ItemCode, T11.PackQty, T11.BaseEntry, T11.BaseType, T11.BaseLine ");
            queryDoc.Append(string.Format("FROM {0} T10 INNER JOIN {1} T11 ON T10.DocEntry = T11.DocEntry) T3 ", creditNoteMaster, creditNoteDetail));
            queryDoc.Append("on T0.ObjType = T3.BaseType and T0.DocEntry = T3.BaseEntry and T1.LineNum = T3.BaseLine ");
            queryDoc.Append(string.Format("where T1.U_CSS_ENVASEDEVOL = 'SI' and T1.TargetType = {0} and T1.BaseType = {1}  and t1.BaseEntry = {2} ", targetType, baseType, baseEntry));
            queryDoc.Append("order by T3.ObjType, T3.DocEntry");

            return queryDoc.ToString();
        }
    }

    //public class ORK_ENV_DEV_DETALLE
    //{
    //    public int Code { get; set; }
    //    public string Name { get; set; }
    //    public int U_idMaestro { get; set; }
    //    public string U_tipoMovimiento { get; set; }
    //    public string U_itemCode { get; set; }
    //    public int U_cantidad { get; set; }
    //    public int U_ubicacion { get; set; }

    //}

    //public class ORK_ENV_DEV_MASTER
    //{
    //    public int Code { get; set; }
    //    public string Name { get; set; }
    //    public DateTime U_fecha { get; set; }
    //    public string U_objType { get; set; }
    //    public string U_usuario { get; set; }
    //    public string U_socioNegocio { get; set; }

    //    public List<ORK_ENV_DEV_DETALLE> detalleEnvaseDevolutivo { get; set; }


    //    public ORK_ENV_DEV_MASTER()
    //    {
    //        detalleEnvaseDevolutivo = new List<ORK_ENV_DEV_DETALLE>();
    //    }
    //}

    public class ItemDetail
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int U_objType { get; set; }
        public string U_docNum { get; set; }
        public string U_itemCode { get; set; }
        public int U_delivered { get; set; }
        public int U_returned { get; set; }
        public int U_maintenance { get; set; }
        public int U_ready { get; set; }
        public int U_targetType { get; set; }
        public string U_targetDocNum { get; set; }
        public string U_docEntry { get; set; }
        public string U_targetDocEntry { get; set; }
        //public DateTime U_date { get; set; }
    }

    public class ItemSummary
    {
        public string U_itemCode { get; set; }
        public int U_delivered { get; set; }
        public int U_returned { get; set; }
        public int U_maintenance { get; set; }
        public int U_ready { get; set; }
        public List<ItemDetail> lsDetalleItems { get; set; }

        public ItemSummary()
        {
            lsDetalleItems = new List<ItemDetail>();
        }
    }

    public class DocumentSummary
    {
        public int objType { get; set; }
        public string docNum { get; set; }
        public List<ItemSummary> lsItems { get; set; }

        public DocumentSummary()
        {
            lsItems = new List<ItemSummary>();
        }
    }

    public class MktDocHeader
    {
        public int objType { get; set; }
        public string docNum { get; set; }
        public string docEntry { get; set; }
        public int baseType { get; set; }
        public string baseEntry { get; set; }
        public List<MktDocHeader> lsReturns { get; set; }
        public List<MktDocHeader> lsCreditNote { get; set; }

        public MktDocHeader()
        {
            lsReturns = new List<MktDocHeader>();
            lsCreditNote = new List<MktDocHeader>();
        }
    }
}


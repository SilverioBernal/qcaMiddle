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
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_costoReady").Value = Documento.U_costoReady;
                objORK_ENVASE_DEV.UserFields.Fields.Item("U_date").Value = Documento.U_fecha.ToString("yyyy-MM-dd");

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

        public int Create(ItemDetail Documento, int targetObjType)
        {
            string masterCode = "";
            masterCode = GetNextLineNum("U_targetDocNum", targetObjType);

            Documento.U_targetDocEntry = masterCode;
            Documento.U_targetDocNum = masterCode;
            Documento.U_fecha = DateTime.Now;
            return Create(Documento);
        }

        public List<ItemSummary> SearcDocument(int objType, string docNum)
        {
            List<ItemSummary> itemsDocument = new List<ItemSummary>();

            StringBuilder query = new StringBuilder();

            query.Append("select U_itemCode, SUM(U_delivered)U_delivered , SUM(U_returned) U_returned, SUM(U_maintenance) U_maintenance, SUM(U_ready) U_ready, SUM(isnull(U_costoReady, 0)) U_costoReady, u_docEntry ");
            query.Append(string.Format("from [@ORK_ENVASE_DEV] where u_objType = {0} and u_docnum = {1} group by U_itemCode, u_docEntry", objType, docNum));

            Recordset rsDocumento = (Recordset)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            rsDocumento.DoQuery(query.ToString());

            if (rsDocumento.RecordCount > 0)
            {
                while (!rsDocumento.EoF)
                {

                    itemsDocument.Add(new ItemSummary()
                    {
                        U_itemCode = rsDocumento.Fields.Item(0).Value.ToString(),
                        U_delivered = int.Parse(rsDocumento.Fields.Item(1).Value.ToString()),
                        U_returned = int.Parse(rsDocumento.Fields.Item(2).Value.ToString()),
                        U_maintenance = int.Parse(rsDocumento.Fields.Item(3).Value.ToString()),
                        U_ready = int.Parse(rsDocumento.Fields.Item(4).Value.ToString()),
                        U_costoReady = int.Parse(rsDocumento.Fields.Item(5).Value.ToString()),
                        u_docEntry = rsDocumento.Fields.Item(6).Value.ToString()
                    });

                    rsDocumento.MoveNext();
                }
            }
            else
                throw new Exception("No se encontró el documento. Es posible que requiera sincronizar. Para sincronizar ahora presione F2");

            return itemsDocument;
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
                        U_targetType = 0,
                        U_costoReady = 0,
                        U_fecha = DateTime.Parse(rsDocumento.Fields.Item(5).Value.ToString())
                    });

                    rsDocumento.MoveNext();
                }

                res = true;
            }

            return res;
        }

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

        public bool SaveCreditNoteReturnDocument(MktDocHeader document, char tipoDoc)
        {
            bool res = false;

            string invoiceMaster = "", invoiceDetail = "", deliveryMaster = "", creditNoteMaster = "", creditNoteDetail = "";
            int targetType = 0;

            switch (document.objType)
            {
                case 15:
                    switch (tipoDoc)
                    {
                        case 'N':
                            invoiceMaster = "OINV";
                            invoiceDetail = "INV1";
                            deliveryMaster = "ODLN";
                            creditNoteMaster = "ORIN";
                            creditNoteDetail = "RIN1";
                            targetType = 14;
                            break;
                        case 'R':
                            invoiceMaster = "OINV";
                            invoiceDetail = "INV1";
                            deliveryMaster = "ODLN";
                            creditNoteMaster = "ORDN";
                            creditNoteDetail = "RDN1";
                            targetType = 16;
                            break;
                        default:
                            break;
                    }
                    break;
                case 20:
                    switch (tipoDoc)
                    {
                        case 'N':
                            invoiceMaster = "OPCH";
                            invoiceDetail = "PCH1";
                            deliveryMaster = "OPDN";
                            creditNoteMaster = "ORPC";
                            creditNoteDetail = "RPC1";
                            targetType = 19;
                            break;
                        case 'R':
                            invoiceMaster = "OPCH";
                            invoiceDetail = "PCH1";
                            deliveryMaster = "OPDN";
                            creditNoteMaster = "ORPC";
                            creditNoteDetail = "RPC1";
                            targetType = 21;
                            break;
                        default:
                            break;
                    }
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
                            U_targetType = int.Parse(rsDocumento.Fields.Item(0).Value.ToString()),
                            U_costoReady = 0,
                            U_fecha = DateTime.Parse(rsDocumento.Fields.Item(6).Value.ToString())
                        });
                    rsDocumento.MoveNext();
                }

                res = true;
            }

            return res;
        }

        public void UpdateLastSyncDate()
        {
            UserTable ORK_ENV_DEV_INICIO = (UserTable)ClaseDatos.objCompany.UserTables.Item("ORK_ENV_DEV_INICIO");
            ORK_ENV_DEV_INICIO.GetByKey("1");

            ORK_ENV_DEV_INICIO.UserFields.Fields.Item("U_ultSincronizacion").Value = DateTime.Now;

            ORK_ENV_DEV_INICIO.Update();
        }

        //public 

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
            queryDoc.Append("SELECT T0.ObjType, T0.DocNum, T1.ItemCode, T1.PackQty delivered, T0.DocEntry, t0.DocDate ");
            queryDoc.Append(string.Format("FROM {0} T0 ", master));
            queryDoc.Append(string.Format("INNER JOIN {0} T1 ON T0.DocEntry = T1.DocEntry ", detail));
            queryDoc.Append(string.Format("where T0.DocEntry = {0} and U_CSS_ENVASEDEVOL = 'SI' ", docEntry));

            return queryDoc.ToString();
        }

        private string GetCreditNoteDocumentQuery(string invoiceMaster, string invoiceDetail, string deliveryMaster, string creditNoteReturnMaster, string creditNoteReturnDetail, int baseType, int targetType, string baseEntry)
        {
            StringBuilder queryDoc = new StringBuilder();
            queryDoc.Append("SELECT T3.ObjType, T3.DocNum, T3.ItemCode, T3.PackQty, T3.DocEntry, ");
            queryDoc.Append("(select COUNT(*) from [@ORK_ENVASE_DEV] t6 ");
            queryDoc.Append("where t6.U_docEntry = t1.BaseEntry and t6.U_objType = t1.BaseType and t6.U_targetType = t3.ObjType ");
            queryDoc.Append("and t6.U_targetDocEntry = t3.DocEntry) saved, T3.DocDate ");
            queryDoc.Append(string.Format("FROM {0} T0 ", invoiceMaster));
            queryDoc.Append(string.Format("INNER JOIN {0} T1 ON T0.DocEntry = T1.DocEntry  ", invoiceDetail));
            queryDoc.Append(string.Format("INNER JOIN {0} T4 on T1.BaseEntry = T4.DocEntry  ", deliveryMaster));
            queryDoc.Append("INNER JOIN (SELECT T10.ObjType, T10.DocEntry, T10.DocNum, T11.ItemCode, T11.PackQty, T11.BaseEntry, T11.BaseType, T11.BaseLine, T10.DocDate ");
            queryDoc.Append(string.Format("FROM {0} T10 INNER JOIN {1} T11 ON T10.DocEntry = T11.DocEntry) T3 ", creditNoteReturnMaster, creditNoteReturnDetail));
            queryDoc.Append("on T0.ObjType = T3.BaseType and T0.DocEntry = T3.BaseEntry and T1.LineNum = T3.BaseLine ");
            queryDoc.Append(string.Format("where T1.U_CSS_ENVASEDEVOL = 'SI' and T1.TargetType = {0} and T1.BaseType = {1}  and t1.BaseEntry = {2} ", targetType, baseType, baseEntry));
            queryDoc.Append("order by T3.ObjType, T3.DocEntry");

            return queryDoc.ToString();
        }
    }

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
        public int U_costoReady { get; set; }
        public DateTime U_fecha{ get; set; }
    }

    public class ItemSummary
    {
        public string U_itemCode { get; set; }
        public int U_delivered { get; set; }
        public int U_returned { get; set; }
        public int U_maintenance { get; set; }
        public int U_ready { get; set; }
        public int U_costoReady { get; set; }
        public string u_docEntry { get; set; }        
    }

    public class MktDocHeader
    {
        public int objType { get; set; }
        public string docNum { get; set; }
        public string docEntry { get; set; }
        public int baseType { get; set; }
        public string baseEntry { get; set; }        
    }

    public class repoKardex
    {
        public string fecha { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string docEntry { get; set; }
        public string tipoDocumentoAsociado { get; set; }
        public string numeroDocumentoAsociado { get; set; }
        public string targetDocEntry { get; set; }
        public string codigoSocioNegocio { get; set; }
        public string socioNegocio { get; set; }
        public string codigoVendedor { get; set; }
        public string nombreVendedor { get; set; }
        public string codigoArticulo { get; set; }
        public string nombreArticulo { get; set; }
        public int entregado { get; set; }
        public int retornado { get; set; }
        public int enReacondicionamiento { get; set; }
        public int reacondicionado { get; set; }
        public int costoReacondicionamiento { get; set; }
    }
}


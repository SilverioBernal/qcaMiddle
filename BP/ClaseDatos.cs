using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Reflection;
using System.Deployment;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.CSharp;
using System.IO;
using System.Configuration;

namespace BP
{
    public static class ClaseDatos
    {

        #region declaraciones
        public static SAPbobsCOM.Company objCompany = null;

        private static string cUserDb = null;
        private static string cPassDb = null;
        private static string cSvrDb = null;
        private static string cSvrLicense = null;
        public static SqlConnection SqlConn = null;

        public static bool nSapConnected = false;
        public static string cStatus = null;
        public static bool salesEmpl = false;

        //expresiones para la ejecucion de variables de macro
        public const string EXPRESSION_ERROR = "~Exp~Error~";

        #endregion

        #region Manejo de base de datos via SQL Server
        //extrae la informacion de usuario y password para la conexion a la base de datos
        private static void ConnStr()
        {
            string[] vConnStr = new string[2];
            try
            {
                ////lectura de archivo XML con las propiedades de la conexion
                //XmlDocument xDoc = new XmlDocument();
                ////La ruta del documento XML esta en la ruta del ejecutable
                //string path = System.IO.Path.GetDirectoryName(
                //System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                //xDoc.Load(path + "\\cssConfig.xml");
                //XmlNodeList lista = xDoc.GetElementsByTagName("cDbConStr");
                //foreach (XmlElement nodo in lista)
                //{
                //    XmlNodeList nServer = nodo.GetElementsByTagName("cDbServer");
                //    XmlNodeList nUser = nodo.GetElementsByTagName("cDbUser");
                //    XmlNodeList nPass = nodo.GetElementsByTagName("cDbPass");
                //    XmlNodeList nSvrLicense = nodo.GetElementsByTagName("cSvrLicense");
                //    cSvrDb = nServer[0].InnerText;
                //    cUserDb = nUser[0].InnerText;
                //    cPassDb = nPass[0].InnerText;
                //    cSvrLicense = nSvrLicense[0].InnerText;
                //    break;
                //}

                cSvrDb = ConfigurationManager.AppSettings["cDbServer"].ToString();
                cUserDb = ConfigurationManager.AppSettings["cDbUser"].ToString();
                cPassDb = ConfigurationManager.AppSettings["cDbPass"].ToString();
                cSvrLicense = ConfigurationManager.AppSettings["cSvrLicense"].ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        //Conexion alterna con base de datos 
        //Evitar su uso. Trate de usar los recursos de acceso de datos de SAP
        public static void SqlConnex(string db)
        {
            try
            {
                // Hallo los datos de la cadena de conexion con el servidor SQL
                ConnStr();

                //hago la conexion con los datos obtenidos en el XML
                SqlConn =
                new SqlConnection("server=" + cSvrDb + ";uid=" + cUserDb + ";pwd=" + cPassDb + ";database= " + db + ";max pool size=60;min pool size=5;Connect Timeout=55");
                SqlConn.Open();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SqlUnConnex()
        {
            try
            {
                SqlConn.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Procesamiento de consultas tipo Select. 
        //Evitar su uso a menos que los recursos de SAP no sean suficientes
        public static DataSet procesaDataSet(string query)
        {
            DataSet oDs = new DataSet();
            SqlDataAdapter oDataAdapter = new SqlDataAdapter();
            try
            {
                oDataAdapter.SelectCommand = new SqlCommand(query, SqlConn);
                oDataAdapter.Fill(oDs);
                //ClaseDatos.SqlConn.Close();
                return oDs;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SqlConn.Close();
            }
            return oDs;
        }

        //Procesamiento de consultas tipo Select.         
        public static IDataReader procesaDataReader(string query)
        {
            IDataReader miDataReader = null;
            try
            {
                SqlCommand miComando = new SqlCommand(query, SqlConn);
                miDataReader = miComando.ExecuteReader();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return miDataReader;
        }

        //ejecucion de consultyas escalares que devuelven strings
        public static string scalarStringSql(string query)
        {
            string cRsl;
            SqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, SqlConn);
            cRsl = cmd.ExecuteScalar().ToString();
            SqlConn.Close();
            return cRsl;
        }

        //ejecucion de consultyas escalares que devuelven integer
        public static int scalarIntSql(string query)
        {

            int cRsl;
            SqlCommand cmd = new SqlCommand(query, SqlConn);
            SqlConn.Open();
            cRsl = 0 + (Int32)cmd.ExecuteScalar();
            SqlConn.Close();

            if (cRsl == 0 || cRsl == null)
            {

                cRsl = 0;
                return cRsl;

            }


            return cRsl;

        }




        //ejecucion de consultas que no devuelven datos
        public static string nonQuery(string query)
        {

            string cRsl;
            try
            {
                SqlCommand cmd = new SqlCommand(query, SqlConn);
                SqlConn.Open();
                cmd.ExecuteNonQuery();
                SqlConn.Close();
                cRsl = "Datos actualizados con exito";
            }
            catch (Exception er)
            {
                SqlConn.Close();
                cRsl = er.Message;
            }
            return cRsl;
        }

        #endregion

        #region hace la conexion con SAP

        public static SAPbobsCOM.Company SapCompany(string usuarioSap, string passwordSap, string company, string tipoSver)
        {
            string errMessage = "";
            int intErrCode = -1;
            int intRetCode = -1;

            try
            {
                //ejecucion de la conexion
                objCompany = new SAPbobsCOM.Company();
                objCompany.UseTrusted = false;
                objCompany.Server = cSvrDb;
                objCompany.DbUserName = cUserDb;
                objCompany.DbPassword = cPassDb;
                objCompany.UserName = usuarioSap;
                objCompany.Password = passwordSap;
                objCompany.LicenseServer = cSvrLicense;
                switch (tipoSver)
                {
                    case "MSSQL 2005":
                        objCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
                        break;
                    case "MSSQL 2008":
                        objCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                        break;
                    case "MSSQL 2012":
                        objCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                        break;
                    default:
                        break;
                }                
                objCompany.CompanyDB = company;


                intRetCode = objCompany.Connect();
                if (intRetCode != 0)
                {
                    objCompany.GetLastError(out intErrCode, out errMessage);
                    MessageBox.Show(errMessage, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error); nSapConnected = false;
                }
                else
                {
                    cStatus = "Connected to " + objCompany.CompanyName;
                    nSapConnected = true;
                }

                /////
                //cStatus = "Connected to " + objCompany.CompanyName;
                //nSapConnected = true;

                ////
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error); cStatus = er.Message;
                nSapConnected = false;
            }

            return objCompany;
        }
        #endregion

        #region valida si el usuario es empleado de ventas
        public static void validSalesEmploye(string cUser)
        {
            string salesPerson;
            string sql =
                "select CASE WHEN COUNT(*) > 0  THEN '1' ELSE '-1' END AS HITS from ohem where userid = (select internal_k from ousr where user_code = '" + cUser + "')";
            {
                SqlCommand cmd = new SqlCommand(sql, SqlConn);
                try
                {
                    SqlConn.Open();
                    salesPerson = cmd.ExecuteScalar().ToString();
                    if (salesPerson == "-1")
                    {
                        salesEmpl = false;
                    }
                    else
                    {
                        salesEmpl = true;
                    }

                    SqlConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SqlConn.Close();
                }

            }
        }
        #endregion

        public static void lanzaForm(string cform, Form unFormulario)
        {
            string ns = "BP."; // NAMESPACE 
            string typeName;
            typeName = ns + cform;
            try
            {
                // Referenciamos el ensamblado que se está ejecutando. 
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                // Obtenemos una referencia de un objeto Form que conocemos que se encuentra en el ensamblado actual. 
                //cform = ns + cform;
                Form frm = (Form)asm.CreateInstance(typeName);
                frm.MdiParent = unFormulario;
                frm.Show();
                frm.Focus();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message,"Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }
        }
        
        public static DataSet ImportaExcel(string query, string ruta)
        {
            DataSet oDs = new DataSet();
            try
            {

                //identificacion de la ruta del archivo

                if (ruta != "")
                {
                    string excelConnStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + ruta +
                        "; Extended Properties =Excel 8.0;";

                    OleDbConnection excelConn = new OleDbConnection(excelConnStr);
                    //excelConn.Open();

                    OleDbDataAdapter oDataAdapter = new OleDbDataAdapter();

                    oDataAdapter.SelectCommand = new OleDbCommand(query, excelConn);
                    oDataAdapter.Fill(oDs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se puede leer del disco original. Original error: " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return oDs;
        }

        public static DataSet importCommaFile(string ruta)
        {
            DataSet oDs = new DataSet();
            DataTable oDT = null;

            string[] ColumnNames = null;
            string[] oStreamDataValues = null;

            using (StreamReader reader = new StreamReader(ruta))
            {

                int lines = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (lines == 0)
                    {
                        ColumnNames = line.Split(',');
                        foreach (string csvcolumn in ColumnNames)
                        {
                            DataColumn oDataColumn = new DataColumn(csvcolumn.ToUpper(), typeof(string));

                            //setting the default value of empty.string to newly created column
                            oDataColumn.DefaultValue = string.Empty;

                            //adding the newly created column to the table
                            oDT.Columns.Add(oDataColumn);
                        }
                    }
                    else
                    {
                        oStreamDataValues = line.Split(',');

                        //creates a new DataRow with the same schema as of the oDataTable            
                        DataRow oDataRow = oDT.NewRow();

                        //using foreach looping through all the column names
                        for (int i = 0; i < ColumnNames.Length; i++)
                        {
                            oDataRow[ColumnNames[i]] = oStreamDataValues[i] == null ? string.Empty : oStreamDataValues[i].ToString();
                        }
                    }
                }

                oDs.Tables.Add(oDT);

                return oDs;
            }
        }

        public static DataSet importTabFile(string ruta)
        {
            DataSet oDs = new DataSet();
            DataTable oDT = new DataTable();

            string[] ColumnNames = null;
            string[] oStreamDataValues = null;

            using (StreamReader reader = new StreamReader(ruta))
            {

                int lines = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (lines == 0)
                    {
                        ColumnNames = line.Split('\t');
                        foreach (string csvcolumn in ColumnNames)
                        {
                            DataColumn oDataColumn = new DataColumn(csvcolumn.ToUpper(), typeof(string));

                            //setting the default value of empty.string to newly created column
                            oDataColumn.DefaultValue = string.Empty;

                            //adding the newly created column to the table
                            oDT.Columns.Add(oDataColumn);
                        }
                        lines++;
                    }
                    else
                    {
                        oStreamDataValues = line.Split('\t');

                        //creates a new DataRow with the same schema as of the oDataTable            
                        DataRow oDataRow = oDT.NewRow();

                        //using foreach looping through all the column names
                        for (int i = 0; i < ColumnNames.Length; i++)
                        {
                            oDataRow[ColumnNames[i]] = oStreamDataValues[i] == null ? string.Empty : oStreamDataValues[i].ToString();
                        }

                        oDT.Rows.Add(oDataRow);
                    }
                }

                oDs.Tables.Add(oDT);

                return oDs;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data;
using SAPbobsCOM;
using System.Configuration;

namespace BP
{
    /// <summary>
    /// Acceso a datos para el inventario
    /// </summary>
    public class ClsDataInventario
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
        public ClsDataInventario()
        {
            //this.baseDatos = DatabaseFactory.CreateDatabase("SAP");
        }
        #endregion
        #region Métodos
        /// <summary>
        /// Lista todos los artículos en SAP Business One
        /// </summary>
        /// <param name="tipo">Tipo de consulta</param>  
        /// <param name="fecha">Fecha inicial de la consulta</param>  
        /// <returns>Lista de Artículos</returns>
        public List<Articulo> ListarArticulos(string tipo, DateTime fecha)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT ItemCode, CodeBars, ItemName, FrozenFor, ItemType, T0.ItmsGrpCod, ItmsGrpNam, InvntItem, SellItem, PrchseItem, Phantom, BuyUnitMsr,  ");
            miSentencia.Append("PurPackMsr, NumInBuy, SalUnitMsr, SalPackMsr, NumInSale, ManBtchNum, ManSerNum ");
            miSentencia.Append("FROM OITM T0 ");
            miSentencia.Append("INNER JOIN OITB T1 ");
            miSentencia.Append("ON T0.ItmsGrpCod = T1.ItmsGrpCod ");
            if (tipo.Equals("FechaModificacion"))
                miSentencia.Append("AND (T0.UpdateDate >= '" + fecha.ToString("yyyyMMdd") + "' OR T0.CreateDate >= '" + fecha.ToString("yyyyMMdd") + "') ");

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());

            List<Articulo> listaArticulos = new List<Articulo>();
            Articulo articulo;
            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    articulo = new Articulo();
                    articulo.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    articulo.CodeBars = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    articulo.ItemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    articulo.Status = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    articulo.ItemType = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    articulo.ItmsGrpCode = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    articulo.ItmsGrpName = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    if (this.reader.GetValue(7).ToString().Equals("Y"))
                        articulo.InventoyItem = true;
                    if (this.reader.GetValue(8).ToString().Equals("Y"))
                        articulo.SalesItem = true;
                    if (this.reader.GetValue(9).ToString().Equals("Y"))
                        articulo.PurchaseItem = true;
                    if (this.reader.GetValue(10).ToString().Equals("Y"))
                        articulo.Panthom = true;
                    articulo.BuyUnitMsr = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    articulo.PurPackMsr = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    articulo.NumInBuy = this.reader.IsDBNull(13) ? 0 : Convert.ToDouble(this.reader.GetValue(13).ToString());
                    articulo.SalUnitMsr = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                    articulo.SalPackMsr = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                    articulo.NumInSale = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                    if (this.reader.GetValue(17).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Lotes;
                    else if (this.reader.GetValue(18).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Series;
                    else
                        articulo.Gestionado = Articulo.Gestion.Ninguno;
                    listaArticulos.Add(articulo);
                }
                ClaseDatos.SqlConn.Close();
            }
            return listaArticulos;
        }

        /// <summary>
        /// Lista todos los artículos en SAP Business One
        /// </summary>
        /// <param name="tipo">Tipo de consulta</param>  
        /// <param name="fecha">Fecha inicial de la consulta</param>  
        /// <returns>Lista de Artículos</returns>
        public List<Articulo> ListarArticulos(string familia)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT ItemCode, CodeBars, ItemName, FrozenFor, ItemType, T0.ItmsGrpCod, ItmsGrpNam, InvntItem, SellItem, PrchseItem, Phantom, BuyUnitMsr,  ");
            miSentencia.Append("PurPackMsr, NumInBuy, SalUnitMsr, SalPackMsr, NumInSale, ManBtchNum, ManSerNum ");
            miSentencia.Append("FROM OITM T0 ");
            miSentencia.Append("INNER JOIN OITB T1 ");
            miSentencia.Append("ON T0.ItmsGrpCod = T1.ItmsGrpCod ");
            miSentencia.Append(string.Format("where T1.ItmsGrpNam like '{0}%' ", familia));
            miSentencia.Append("order by ItemName");

            List<Articulo> listaArticulos = new List<Articulo>();
            Articulo articulo;
            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    articulo = new Articulo();
                    articulo.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    articulo.CodeBars = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    articulo.ItemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    articulo.Status = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    articulo.ItemType = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    articulo.ItmsGrpCode = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    articulo.ItmsGrpName = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    if (this.reader.GetValue(7).ToString().Equals("Y"))
                        articulo.InventoyItem = true;
                    if (this.reader.GetValue(8).ToString().Equals("Y"))
                        articulo.SalesItem = true;
                    if (this.reader.GetValue(9).ToString().Equals("Y"))
                        articulo.PurchaseItem = true;
                    if (this.reader.GetValue(10).ToString().Equals("Y"))
                        articulo.Panthom = true;
                    articulo.BuyUnitMsr = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    articulo.PurPackMsr = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    articulo.NumInBuy = this.reader.IsDBNull(13) ? 0 : Convert.ToDouble(this.reader.GetValue(13).ToString());
                    articulo.SalUnitMsr = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                    articulo.SalPackMsr = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                    articulo.NumInSale = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                    if (this.reader.GetValue(17).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Lotes;
                    else if (this.reader.GetValue(18).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Series;
                    else
                        articulo.Gestionado = Articulo.Gestion.Ninguno;
                    listaArticulos.Add(articulo);
                }
                ClaseDatos.SqlConn.Close();
            }
            return listaArticulos;
        }

        /// <summary>
        /// Lista todos los artículos en SAP Business One
        /// </summary>        
        /// <returns>Lista de Artículos</returns>
        public List<Articulo> ListarArticulos()
        {
            StringBuilder miSentencia = new StringBuilder("SELECT ItemCode, ItemName ");
            miSentencia.Append("FROM OITM T0 where [frozenFor] = 'N' AND [validTo] IS NULL ");
            miSentencia.Append("order by ItemName");

            List<Articulo> listaArticulos = new List<Articulo>();
            Articulo articulo;
            
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    articulo = new Articulo();
                    articulo.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();                    
                    articulo.ItemName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();                    
                    listaArticulos.Add(articulo);
                }
                ClaseDatos.SqlConn.Close();
            }
            return listaArticulos;
        }

        /// <summary>
        /// Consulta de un artículo por el código del Almacen
        /// </summary>
        /// <param name="almacen">Código del Almacen en SAP Business One</param>
        /// <returns>Lista de Articulos con la información de la consulta</returns>
        public List<Articulo> ConsultarArticulosXAlmacen(Almacen almacen)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT T0.ItemCode, CodeBars, ItemName, FrozenFor, ItemType, T0.ItmsGrpCod, ItmsGrpNam, InvntItem, SellItem, PrchseItem, Phantom, BuyUnitMsr,  ");
            miSentencia.Append("PurPackMsr, NumInBuy, SalUnitMsr, SalPackMsr, NumInSale, ManBtchNum, ManSerNum ");
            miSentencia.Append("FROM OITM T0 ");
            miSentencia.Append("INNER JOIN OITB T1 ");
            miSentencia.Append("ON T0.ItmsGrpCod = T1.ItmsGrpCod ");
            miSentencia.Append("INNER JOIN OITW T2 ");
            miSentencia.Append("ON T0.ItemCode = T2.ItemCode ");
            miSentencia.Append(string.Format("AND T2.WhsCode = '{0}' ", almacen.WhsCode));
            miSentencia.Append("where (OnHand + OnOrder - IsCommited) > 0");

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
            //this.baseDatos.AddInParameter(miComando, "Almacen", DbType.String, almacen.WhsCode);

            List<Articulo> listaArticulos = new List<Articulo>();
            Articulo articulo;

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))            
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    articulo = new Articulo();
                    articulo.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    articulo.CodeBars = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    articulo.ItemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    articulo.Status = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    articulo.ItemType = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    articulo.ItmsGrpCode = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    articulo.ItmsGrpName = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    if (this.reader.GetValue(7).ToString().Equals("Y"))
                        articulo.InventoyItem = true;
                    if (this.reader.GetValue(8).ToString().Equals("Y"))
                        articulo.SalesItem = true;
                    if (this.reader.GetValue(9).ToString().Equals("Y"))
                        articulo.PurchaseItem = true;
                    if (this.reader.GetValue(10).ToString().Equals("Y"))
                        articulo.Panthom = true;
                    articulo.BuyUnitMsr = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    articulo.PurPackMsr = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    articulo.NumInBuy = this.reader.IsDBNull(13) ? 0 : Convert.ToDouble(this.reader.GetValue(13).ToString());
                    articulo.SalUnitMsr = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                    articulo.SalPackMsr = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                    articulo.NumInSale = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                    if (this.reader.GetValue(17).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Lotes;
                    else if (this.reader.GetValue(18).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Series;
                    else
                        articulo.Gestionado = Articulo.Gestion.Ninguno;
                    listaArticulos.Add(articulo);
                }
                ClaseDatos.SqlConn.Close();
            }
            return listaArticulos;
        }

        public List<Articulo> ConsultarArticulosXAlmacen(Almacen almacen, bool manejoLote)
        {


            StringBuilder miSentencia = new StringBuilder("SELECT T0.ItemCode, CodeBars, ItemName, FrozenFor, ItemType, T0.ItmsGrpCod, ItmsGrpNam, InvntItem, SellItem, PrchseItem, Phantom, BuyUnitMsr,  ");
            miSentencia.Append("PurPackMsr, NumInBuy, SalUnitMsr, SalPackMsr, NumInSale, ManBtchNum, ManSerNum, t2.AvgPrice ");
            miSentencia.Append("FROM OITM T0 ");
            miSentencia.Append("INNER JOIN OITB T1 ");
            miSentencia.Append("ON T0.ItmsGrpCod = T1.ItmsGrpCod ");
            miSentencia.Append("INNER JOIN OITW T2 ");
            miSentencia.Append("ON T0.ItemCode = T2.ItemCode ");
            miSentencia.Append(string.Format("AND T2.WhsCode = '{0}' ", almacen.WhsCode));
            miSentencia.Append(string.Format("where ManBtchNum = '{0}' and (t2.OnHand + t2.OnOrder - t2.IsCommited) > 0", manejoLote ? "Y" : "N"));

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
            //this.baseDatos.AddInParameter(miComando, "Almacen", DbType.String, almacen.WhsCode);

            List<Articulo> listaArticulos = new List<Articulo>();
            Articulo articulo;

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))            
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    articulo = new Articulo();
                    articulo.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    articulo.CodeBars = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    articulo.ItemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    articulo.Status = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    articulo.ItemType = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    articulo.ItmsGrpCode = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    articulo.ItmsGrpName = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    
                    if (this.reader.GetValue(7).ToString().Equals("Y"))
                        articulo.InventoyItem = true;
                    
                    if (this.reader.GetValue(8).ToString().Equals("Y"))
                        articulo.SalesItem = true;
                    
                    if (this.reader.GetValue(9).ToString().Equals("Y"))
                        articulo.PurchaseItem = true;
                    
                    if (this.reader.GetValue(10).ToString().Equals("Y"))
                        articulo.Panthom = true;
                    
                    articulo.BuyUnitMsr = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    articulo.PurPackMsr = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    articulo.NumInBuy = this.reader.IsDBNull(13) ? 0 : Convert.ToDouble(this.reader.GetValue(13).ToString());
                    articulo.SalUnitMsr = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                    articulo.SalPackMsr = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                    articulo.NumInSale = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                    
                    if (this.reader.GetValue(17).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Lotes;
                    else if (this.reader.GetValue(18).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Series;
                    else
                        articulo.Gestionado = Articulo.Gestion.Ninguno;

                    articulo.AvgPrice = this.reader.IsDBNull(19) ? 0 : Convert.ToDouble(this.reader.GetValue(19).ToString());

                    listaArticulos.Add(articulo);
                }
                ClaseDatos.SqlConn.Close();
            }
            return listaArticulos;
        }

        /// <summary>
        /// Reorna los almacenes de SAP
        /// </summary>
        /// <returns></returns>
        public List<Almacen> ListarAlmacenes()
        {
            StringBuilder miSentencia = new StringBuilder("SELECT WhsCode, ISNULL(WhsName, '** Almacén sin nombre **') WhsName ");
            miSentencia.Append("FROM OWHS T0 order by WhsName");

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());

            List<Almacen> listaAlmacenes = new List<Almacen>();
            Almacen almacen;

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    almacen = new Almacen();
                    almacen.WhsCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    almacen.WhsName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    listaAlmacenes.Add(almacen);
                }

                ClaseDatos.SqlConn.Close();
            }
            return listaAlmacenes;
        }

        public List<Almacen> ListarAlmacenesReloteo()
        {
            StringBuilder miSentencia = new StringBuilder("select Code, U_nombreBodega from [@ORK_BODEGAS] ");

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());

            List<Almacen> listaAlmacenes = new List<Almacen>();
            Almacen almacen;

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    almacen = new Almacen();
                    almacen.WhsCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    almacen.WhsName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    listaAlmacenes.Add(almacen);
                }

                ClaseDatos.SqlConn.Close();
            }
            return listaAlmacenes;
        }

        /// <summary>
        /// Lista todos los almacenes en SAP Business One
        /// </summary>
        /// <param name="tipo">Tipo de consulta</param>  
        /// <param name="fecha">Fecha inicial de la consulta</param>  
        /// <returns>Lsita de Almacenes</returns>
        public List<Almacen> ListarAlmacenes(string tipo, DateTime fecha)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT WhsCode, WhsName ");
            miSentencia.Append("FROM OWHS T0 ");
            if (tipo.Equals("FechaModificacion"))
                miSentencia.Append("WHERE (T0.UpdateDate >= '" + fecha.ToString("yyyyMMdd") + "' OR T0.CreateDate >= '" + fecha.ToString("yyyyMMdd") + "') ");

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());

            List<Almacen> listaAlmacenes = new List<Almacen>();
            Almacen almacen;

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    almacen = new Almacen();
                    almacen.WhsCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    almacen.WhsName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    listaAlmacenes.Add(almacen);
                }
                ClaseDatos.SqlConn.Close();
            }
            return listaAlmacenes;
        }

        /// <summary>
        /// Lista todos los almacenes en SAP Business One
        /// </summary>
        /// <param name="almacen">Código del Almacen en SAP Business One</param>
        /// <returns>Lsita de Almacenes</returns>
        public bool ValidarExistenciaAlmacen(Almacen almacen)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT COUNT(*) FROM OWHS ");
            miSentencia.Append(string.Format("WHERE WhsCode = '{0}'", almacen.WhsCode));

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
            //this.baseDatos.AddInParameter(miComando, "Almacen", DbType.String, almacen.WhsCode);            

            //if (Convert.ToInt32(this.baseDatos.ExecuteScalar(miComando)) > 0)
            if (ClaseDatos.scalarIntSql(miSentencia.ToString()) > 0)
            {
                ClaseDatos.SqlConn.Close();
                return true;
            }
            else
            {
                ClaseDatos.SqlConn.Close();
                return false;
            }
        }

        /// <summary>
        /// Consulta un artículo en SAP Business One de acuerdo al código
        /// </summary>
        /// <param name="codigo">Código del Articulo en SAP Business One</param>
        /// <returns>Artículo</returns>
        public Articulo ConsultarArticulo(string codigo)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT ItemCode, CodeBars, ItemName, FrozenFor, ItemType, T0.ItmsGrpCod, ItmsGrpNam, InvntItem, SellItem, PrchseItem, Phantom, BuyUnitMsr,  ");
            miSentencia.Append("PurPackMsr, NumInBuy, SalUnitMsr, SalPackMsr, NumInSale, ManBtchNum, ManSerNum ");
            miSentencia.Append("FROM OITM T0 ");
            miSentencia.Append("INNER JOIN OITB T1 ");
            miSentencia.Append("ON T0.ItmsGrpCod = T1.ItmsGrpCod ");
            miSentencia.Append(string.Format("AND T0.ItemCode = '{0}'", codigo));

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
            //this.baseDatos.AddInParameter(miComando, "codigo", DbType.String, codigo);

            Articulo articulo = new Articulo();

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    articulo = new Articulo();
                    articulo.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    articulo.CodeBars = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    articulo.ItemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    articulo.Status = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    articulo.ItemType = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    articulo.ItmsGrpCode = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    articulo.ItmsGrpName = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    if (this.reader.GetValue(7).ToString().Equals("Y"))
                        articulo.InventoyItem = true;
                    if (this.reader.GetValue(8).ToString().Equals("Y"))
                        articulo.SalesItem = true;
                    if (this.reader.GetValue(9).ToString().Equals("Y"))
                        articulo.PurchaseItem = true;
                    if (this.reader.GetValue(10).ToString().Equals("Y"))
                        articulo.Panthom = true;
                    articulo.BuyUnitMsr = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    articulo.PurPackMsr = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    articulo.NumInBuy = this.reader.IsDBNull(13) ? 0 : Convert.ToDouble(this.reader.GetValue(13).ToString());
                    articulo.SalUnitMsr = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                    articulo.SalPackMsr = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                    articulo.NumInSale = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                    if (this.reader.GetValue(17).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Lotes;
                    else if (this.reader.GetValue(18).ToString().Equals("Y"))
                        articulo.Gestionado = Articulo.Gestion.Series;
                    else
                        articulo.Gestionado = Articulo.Gestion.Ninguno;
                }
                ClaseDatos.SqlConn.Close();
            }
            return articulo;
        }
        /// <summary>
        /// Consulta de lotes disponibles por artículo y bodega
        /// </summary>
        /// <param name="articulo">Código del artículo en SAP Business One</param>
        /// <param name="bodega">Código de la Bodega en SAP Business One</param>
        /// <returns>Lista de lotes disponibles con las cantidades</returns>
        public List<Lote> ConsultarLotesDisponibles(string articulo, string bodega)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT T1.DistNumber,T0.Quantity, T1.ExpDate, T1.MnfDate, T1.LotNumber, T1.MnfSerial ");
            miSentencia.Append("FROM OBTQ T0 INNER JOIN OBTN T1 ");
            miSentencia.Append("ON T0.SysNumber=T1.SysNumber ");
            miSentencia.Append("AND T0.ItemCode=T1.ItemCode ");
            miSentencia.Append(string.Format("AND T0.ItemCode = '{0}' ", articulo));
            miSentencia.Append(string.Format("AND WhsCode = '{0}' ", bodega));
            miSentencia.Append("AND T0.Quantity > 0 ");
            miSentencia.Append("AND T1.Status = 0 ");

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
            //this.baseDatos.AddInParameter(miComando, "ItemCode", DbType.String, articulo);
            //this.baseDatos.AddInParameter(miComando, "WhsCode", DbType.String, bodega);


            Lote lote = new Lote();
            List<Lote> listaLotes = new List<Lote>();

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    lote = new Lote();
                    lote.DistNumber = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    lote.Quantity = this.reader.IsDBNull(1) ? 0 : Convert.ToDouble(this.reader.GetValue(1).ToString());

                    if (!this.reader.IsDBNull(2))
                        lote.ExpDate = this.reader.GetValue(2).ToString();

                    if (!this.reader.IsDBNull(3))
                        lote.MfnDate = this.reader.GetValue(3).ToString();

                    listaLotes.Add(lote);
                }
                ClaseDatos.SqlConn.Close();
            }
            return listaLotes;
        }

        /// <summary>
        /// Crea una transferencia de inventario
        /// </summary>
        /// <param name="documento">Documento con la información de la  transferencia</param>  
        /// <param name="userWSSAP">Usuario del servicio SAP</param>  
        /// <returns>Número Interno generado por SAP</returns>
        public int CrearTransferenciaInventario(Transferencia documento)
        {
            try
            {
                ClaseDatos.objCompany.StartTransaction();
                //StockTransfer miDocumento = (StockTransfer)ConexionSAP.Conexion.compania.GetBusinessObject(BoObjectTypes.oStockTransfer);
                StockTransfer miDocumento = (StockTransfer)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);

                miDocumento.Series = GetSeriesDocumento("SerieTransferenciaEnvaseDevolutivo");

                miDocumento.DocDate = documento.DocDate;
                miDocumento.TaxDate = documento.TaxDate;
                miDocumento.FromWarehouse = documento.WhsCode;
                miDocumento.Comments = documento.Comments;
                foreach (TransferenciaLinea linea in documento.lineas)
                {
                    miDocumento.Lines.Quantity = linea.Quantity;
                    miDocumento.Lines.WarehouseCode = linea.WhsCode;
                    miDocumento.Lines.ItemCode = linea.ItemCode;
                    if (linea.BatchNumbers != null)
                        foreach (Lote lote in linea.BatchNumbers)
                        {
                            miDocumento.Lines.BatchNumbers.BatchNumber = lote.DistNumber;
                            miDocumento.Lines.BatchNumbers.Quantity = lote.Quantity;
                            miDocumento.Lines.BatchNumbers.Add();
                        }
                    if (linea.SerialNumbers != null)
                        foreach (SerialNumber serie in linea.SerialNumbers)
                        {
                            //miDocumento.Lines.SerialNumbers.SystemSerialNumber = 1;
                            miDocumento.Lines.SerialNumbers.InternalSerialNumber = serie.DisNumber;
                            //miDocumento.Lines.SerialNumbers.ExpiryDate = serie.ExpDate;
                            //miDocumento.Lines.SerialNumbers.ManufacturerSerialNumber = serie.MnfSerial;
                            //miDocumento.Lines.SerialNumbers.BatchID = serie.LotNumber;
                            //miDocumento.Lines.SerialNumbers.ManufactureDate = serie.MnfDate;
                            miDocumento.Lines.SerialNumbers.Add();
                        }
                    miDocumento.Lines.Add();
                }
                if (miDocumento.Add() != 0)
                {
                    throw new Exception(ClaseDatos.objCompany.GetLastErrorDescription());
                }

                if (ClaseDatos.objCompany.InTransaction)
                {
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                }

                documento.DocEntry = Convert.ToInt32(ClaseDatos.objCompany.GetNewObjectKey());
            }
            catch (Exception ex)
            {
                //ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);

                throw ex;
            }

            return documento.DocEntry;
        }


        public string CrearReloteoArticulo(LoteoArticulo documento)
        {
            string resSalida = "";
            string resEntradas = "";
            int numEntradas = 0;
            int numEntradasEsperadas = documento.lineas.Count();
            string res = "";

            try
            {
                #region Salida de almacen
                ClaseDatos.objCompany.StartTransaction();
                Documents oSalidaInventario = (Documents)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oInventoryGenExit);

                oSalidaInventario.DocDate = documento.DocDate;
                oSalidaInventario.TaxDate = documento.TaxDate;
                oSalidaInventario.Comments = documento.Comments;


                foreach (LoteoArticuloLinea linea in documento.lineas)
                {
                    oSalidaInventario.Lines.Quantity = linea.quantity;
                    oSalidaInventario.Lines.WarehouseCode = documento.FromWhsCode;
                    oSalidaInventario.Lines.ItemCode = linea.itemCode;

                    oSalidaInventario.Lines.BatchNumbers.BatchNumber = linea.originalBatchNumber;
                    oSalidaInventario.Lines.BatchNumbers.Quantity = linea.quantity;
                    oSalidaInventario.Lines.AccountCode = BusinessParametroAplicacion.GetParamValue("CuentaReloteo"); ;// ConfigurationManager.AppSettings["CuentaReloteo"].ToString();                    
                    oSalidaInventario.Lines.BatchNumbers.Add();

                    oSalidaInventario.Lines.Add();
                }

                if (oSalidaInventario.Add() != 0)
                    throw new Exception(string.Format("No se pudo crear la salida de inventario. Detalles: {0} - {1}", ClaseDatos.objCompany.GetLastErrorCode(), ClaseDatos.objCompany.GetLastErrorDescription()));

                if (ClaseDatos.objCompany.InTransaction)
                {
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    resSalida= "Salida de inventario creada con exito ";
                }
                #endregion

                #region Entrada a los diferentes almacenes
                string[] bodegas = documento.lineas.Select(x => x.toWhsCode).Distinct().ToArray();

                foreach (string bodega in bodegas)
                {
                    List<LoteoArticuloLinea> itemsEntrantes = documento.lineas.Where(x => x.toWhsCode == bodega).ToList();

                    ClaseDatos.objCompany.StartTransaction();
                    Documents oEntradaInventario = (Documents)ClaseDatos.objCompany.GetBusinessObject(BoObjectTypes.oInventoryGenEntry);

                    oEntradaInventario.DocDate = documento.DocDate;
                    oEntradaInventario.TaxDate = documento.TaxDate;
                    oEntradaInventario.Comments = documento.Comments;

                    foreach (LoteoArticuloLinea linea in itemsEntrantes)
                    {
                        oEntradaInventario.Lines.Quantity = linea.quantity;
                        oEntradaInventario.Lines.WarehouseCode = linea.toWhsCode;
                        oEntradaInventario.Lines.ItemCode = linea.itemCode;
                        oEntradaInventario.Lines.UnitPrice = linea.AvgPrice;

                        oEntradaInventario.Lines.BatchNumbers.BatchNumber = linea.finallyBatchNumber;
                        oEntradaInventario.Lines.BatchNumbers.Quantity = linea.quantity;
                        oEntradaInventario.Lines.BatchNumbers.ManufacturerSerialNumber = linea.iqNumber;
                        
                        if (!string.IsNullOrEmpty(linea.ExpDate))
                        oEntradaInventario.Lines.BatchNumbers.ExpiryDate = Convert.ToDateTime(linea.ExpDate);

                        if (!string.IsNullOrEmpty(linea.MnfDate))
                            oEntradaInventario.Lines.BatchNumbers.ManufacturingDate = Convert.ToDateTime(linea.MnfDate);

                        oEntradaInventario.Lines.AccountCode = BusinessParametroAplicacion.GetParamValue("CuentaReloteo"); //ConfigurationManager.AppSettings["CuentaReloteo"].ToString();  
                        oEntradaInventario.Lines.BatchNumbers.Add();

                        oEntradaInventario.Lines.Add();
                    }

                    if (oEntradaInventario.Add() != 0)
                        throw new Exception(string.Format("No se pudo crear la entrada de inventario. Detalles: {0} - {1}", ClaseDatos.objCompany.GetLastErrorCode(), ClaseDatos.objCompany.GetLastErrorDescription()));

                    if (ClaseDatos.objCompany.InTransaction)
                    {
                        ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                        numEntradas++;
                    }
                }

                res = string.Format("{0} Se crearon {1} entradas reloteadas.", resSalida, numEntradas.ToString());

                return res;

                #endregion
            }
            catch (Exception ex)
            {
                if (ClaseDatos.objCompany.InTransaction)
                    ClaseDatos.objCompany.EndTransaction(BoWfTransOpt.wf_RollBack);

                string tmpRes = "";

                if (!string.IsNullOrEmpty(resSalida))
                {
                    tmpRes = string.Format("La salida creada con exito. Sin embargo, solo se crearon {0} de {1} entradas esperadas. Detalles: {2}",
                        numEntradas.ToString(), numEntradasEsperadas.ToString(), ex.Message);

                    throw new Exception(tmpRes);
                }
                else
                    throw ex;
            }
        }

        /// <summary>
        /// Consulta de seriales disponibles por artículo y bodega
        /// </summary>
        /// <param name="articulo">Código del artículo en SAP Business One</param>
        /// <param name="bodega">Código de la Bodega en SAP Business One</param>
        /// <param name="conexion">Conexión con el servicio</param>
        /// <returns>Lista de seriales disponibles con las cantidades</returns>        
        public List<SerialNumber> ConsultarSerialesDisponibles(string articulo, string bodega)
        {
            StringBuilder miSentencia = new StringBuilder("SELECT MIN(T0.[DistNumber]) distnumber, MIN(T0.[MnfSerial]) MnfSerial, MIN(T0.[LotNumber]) Lotnumber, MIN(T0.[ExpDate]) expDate, min(T0.[ItemCode]), CASE WHEN T2.Quantity > 0 THEN 'Disponible' ELSE 'No Disponible' END STATUS ");
            miSentencia.Append("FROM OSRN T0 ");
            miSentencia.Append("INNER  JOIN OITM T1  ON  T1.ItemCode = T0.ItemCode ");
            miSentencia.Append(string.Format("AND T1.[InvntItem] = ('Y')  AND  T0.[ItemCode] = '{0}' ", articulo));
            miSentencia.Append("INNER JOIN OSRQ T2 ON T2.ItemCode = T0.ItemCode AND T2.SysNumber = T0.SysNumber AND T2.Quantity > 0 ");
            miSentencia.Append(string.Format("AND T2.WhsCode = '{0}' ", bodega));
            miSentencia.Append("GROUP BY T0.[AbsEntry] , T2.Quantity");

            //DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
            //this.baseDatos.AddInParameter(miComando, "ItemCode", DbType.String, articulo);
            //this.baseDatos.AddInParameter(miComando, "WhsCode", DbType.String, bodega);

            SerialNumber serial = new SerialNumber();
            List<SerialNumber> listaSeriales = new List<SerialNumber>();

            //using (this.reader = this.baseDatos.ExecuteReader(miComando))
            using (this.reader = ClaseDatos.procesaDataReader(miSentencia.ToString()))
            {
                while (this.reader.Read())
                {
                    serial = new SerialNumber();
                    serial.DisNumber = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    serial.MnfSerial = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    serial.LotNumber = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    serial.ExpDate = this.reader.IsDBNull(3) ? DateTime.MinValue : Convert.ToDateTime(this.reader.GetValue(3).ToString());
                    serial.ItemCode = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    serial.Status = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    listaSeriales.Add(serial);
                }
                this.reader.Close();
                ClaseDatos.SqlConn.Close();
            }
            return listaSeriales;
        }

        ///// <summary>
        ///// Consulta de seriales disponibles por rango de fechas
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio de la busqueda</param>
        ///// <param name="fechaFin">Fecha fin de la busqueda</param>        
        ///// <returns>Lista de seriales disponibles con las cantidades</returns>        
        //public List<SerialNumber> ConsultarSerialesporFechas(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    StringBuilder miSentencia = new StringBuilder("SELECT MIN(T0.[DistNumber]) distnumber, MIN(T0.[MnfSerial]) MnfSerial, MIN(T0.[LotNumber]) Lotnumber, MIN(T0.[ExpDate]) expDate, min(T0.[ItemCode]), CASE WHEN T2.Quantity > 0 THEN 'Disponible' ELSE 'No Disponible' END STATUS ");
        //    miSentencia.Append("FROM OSRN T0 ");
        //    miSentencia.Append("INNER  JOIN OITM T1  ON  T1.ItemCode = T0.ItemCode ");
        //    miSentencia.Append("AND T1.[InvntItem] = ('Y') ");
        //    miSentencia.Append("AND T0.InDate BETWEEN @fechaInicio AND @fechaFin ");
        //    miSentencia.Append("INNER JOIN OSRQ T2 ON T2.ItemCode = T0.ItemCode AND T2.SysNumber = T0.SysNumber ");
        //    miSentencia.Append("GROUP BY T0.[AbsEntry] , T2.Quantity");
        //    miSentencia.Append("ORDER BY 5");
        //    DbCommand miComando = this.baseDatos.GetSqlStringCommand(miSentencia.ToString());
        //    this.baseDatos.AddInParameter(miComando, "fechaInicio", DbType.DateTime, fechaInicio);
        //    this.baseDatos.AddInParameter(miComando, "fechaFin", DbType.DateTime, fechaFin);
        //    SerialNumber serial = new SerialNumber();
        //    List<SerialNumber> listaSeriales = new List<SerialNumber>();
        //    using (this.reader = this.baseDatos.ExecuteReader(miComando))
        //    {
        //        while (this.reader.Read())
        //        {
        //            serial = new SerialNumber();
        //            serial.DisNumber = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
        //            serial.MnfSerial = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
        //            serial.LotNumber = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
        //            serial.ExpDate = this.reader.IsDBNull(3) ? DateTime.MinValue : Convert.ToDateTime(this.reader.GetValue(3).ToString());
        //            serial.ItemCode = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
        //            serial.Status = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
        //            listaSeriales.Add(serial);
        //        }
        //        this.reader.Close();
        //    }
        //    return listaSeriales;
        //}

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
        #endregion
    }
}

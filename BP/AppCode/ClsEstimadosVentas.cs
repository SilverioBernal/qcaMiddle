using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BP.AppCode
{
    class ClsEstimadosVentas
    {
        public enum OrigenProductos { catalogoClientes, maestroArticulos }
    
        public ClsEstimadosVentas()
        {
           
        }

        public string Mensaje
        {
            get
            {
                return mensaje;
            }
            set
            {
                mensaje = value;
            }
        }

        public string FechaPresupuesto
        {
            get
            {
                return fechaPresupuesto;
            }
            set
            {
                fechaPresupuesto = value;
            }
        }

        public string CodigoIngenieroVentas
        {
            get
            {
                return codigoIngenieroVentas;
            }
            set
            {
                codigoIngenieroVentas = value;
            }
        }


        private string mensaje;
        private string fechaPresupuesto;
        private string codigoIngenieroVentas;

        /// <summary>
        /// Determina si la fecha de presupuesto es valida
        /// </summary>
        public bool FechaPresupuestoValida()
        {
            bool res = true;

            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

            try
            {                
                string cSql = "select U_CSS_ESTIM_FECHA from [@CSS_PRESUPUESTOS_PA] where U_CSS_MES=" + DateTime.Today.Month.ToString();
                DataSet ultimo = ClaseDatos.procesaDataSet(cSql);
                string fecha = ultimo.Tables[0].Rows[0][0].ToString();

                if (DateTime.Now.Date > Convert.ToDateTime(fecha).Date)
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                mensaje = "La fecha de corte del formulario impide que se pueda habilitar. " + ex.Message;
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }
            return res;
        }

        /// <summary>
        /// Determina si el usuario que esta intentando acceder es valido
        /// </summary>
        public bool IngenieroVentasValido(string usuario)
        {
            bool res = false;

            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

            try
            {
                DataSet dsEmpleado;
                string cSql = "select salesPrson from ohem where userid = (select internal_k from ousr where user_code='" + usuario + "')";

                dsEmpleado = ClaseDatos.procesaDataSet(cSql);

                if (dsEmpleado.Tables[0].Rows.Count > 0 )
                {
                    if (!string.IsNullOrEmpty(dsEmpleado.Tables[0].Rows[0][0].ToString()))
                    {
                        res = true;
                        codigoIngenieroVentas = dsEmpleado.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Se presento un problema al tratar de validar el ingeniero de ventas. " + ex.Message;                
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }

            return res;
        }

        /// <summary>
        /// Obtiene el(los) ingeniero de ventas permitidos para el usuario actual
        /// <para>MultiplesLineas</para>
        /// </summary>
        /// <returns>Dataset</returns>
        public DataSet ObtenerIngenierosVentas(bool MultiplesLineas)
        {
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());
            DataSet dsEmpleado = new DataSet();
            try
            {
                string nombreIngenieroVentas = ObtenerNombreIngenieroVentas();
                string cSql;
                if (!MultiplesLineas)
                {                    
                    cSql = "SELECT  salesPrson SlpCode, lastName SlpName FROM OHEM WHERE manager=(SELECT empID from OHEM where userId =(select internal_k from ousr where user_code = '" + nombreIngenieroVentas + "'))";
                    
                    dsEmpleado = ClaseDatos.procesaDataSet(cSql);
                    
                    
                }
                else
                {
                    cSql = "SELECT SlpCode,SlpName FROM OSLP WHERE U_CSS_IDUSER='" + loging.usrCode + "'";
                    dsEmpleado= ClaseDatos.procesaDataSet(cSql);

                    if (dsEmpleado.Tables[0].Rows.Count == 0)
                    {
                        cSql = "SELECT SlpCode,SlpName FROM OSLP WHERE U_CSS_ESPECIAL=" + codigoIngenieroVentas;
                        dsEmpleado = ClaseDatos.procesaDataSet(cSql);
                    }
                }

                if (dsEmpleado.Tables[0].Rows.Count == 0)
                {                    
                        cSql = "select SlpCode, SlpName from OSLP where SlpCode= " + codigoIngenieroVentas;
                        dsEmpleado = ClaseDatos.procesaDataSet(cSql);                    
                }
            }
            catch (Exception ex)
            {
                mensaje = "Se presento un problema al tratar obtener los ingenieros de ventas. " + ex.Message;
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }

            return dsEmpleado;
        }

        /// <summary>
        /// Devuelve un string con el nombre del ingeniero de ventas
        /// </summary>
        private string ObtenerNombreIngenieroVentas()
        {
            string nombreIngenieroVentas = "";
            try
            {
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

                DataSet dsEmpleado = new DataSet();
                string cSql = "select SlpName from OSLP where SlpCode= " + codigoIngenieroVentas;
                
                dsEmpleado = ClaseDatos.procesaDataSet(cSql);

                if (dsEmpleado.Tables[0].Rows.Count > 0)
                {
                    nombreIngenieroVentas = dsEmpleado.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Se presento un problema al tratar obtener el nombre del ingeniero de ventas. " + ex.Message;
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }

            return nombreIngenieroVentas;
        }

        /// <summary>
        /// Obtiene los clientes a los que tiene acceso el ingeniero de ventas
        /// </summary>
        public DataSet ObtenerClientesIngenieroVentas()
        {
            DataSet dsClientes = new DataSet();
            try
            {
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

                string cSql =
                    "select distinct " +
                    "a.cardcode, a.cardname " +
                    "from " +
                    "	ocrd a " +
                    "	inner join crd1 b on " +
                    "		a.cardcode = b.cardcode " +
                    "WHERE a.frozenFor = 'N' and a.validFor = 'Y' and b.BLOCK = " +
                    "	(" +
                    "		SELECT slpname " +
                    "		FROM OSLP " +
                    "		WHERE SLPCODE =		" +
                    "			(" +
                    "				select isnull(salesPrson ,'-1') " +
                    "				from ohem " +
                    "				where userid = " +
                    "					(" +
                    "						select internal_k from ousr where user_code = '" +
                                    loging.usrCode + "'" +
                    "					)" +
                    "			)" +
                    "	)" +
                    "union " +
                    "select cardcode, cardname " +
                    "from ocrd " +
                    "where frozenFor = 'N' and validFor = 'Y' and slpcode = " +
                    "	(" +
                    "		select isnull(salesPrson ,'-1') from ohem where userid = (select internal_k from ousr where user_code = '" +
                                    loging.usrCode + "')" +
                    "	) ORDER BY CARDNAME";

                dsClientes = ClaseDatos.procesaDataSet(cSql);
            }
            catch (Exception ex)
            {
                mensaje = "Se presento un problema al tratar obtener los clientes asociados al ingeniero de ventas. " + ex.Message;
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }

            return dsClientes;
        }

        /// <summary>
        /// Obtiene los clientes a los que tiene acceso el ingeniero de ventas
        /// </summary>
        public DataSet ObtenerClientesIngenieroVentas(string codigoIngeniero)
        {
            DataSet dsClientes = new DataSet();
            try
            {
                ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

                string cSql = 
                    " select distinct " +
                    " a.cardcode, a.cardname " +
                    " from " +
                    "	ocrd a " +
                    "	inner join crd1 b on " +
                    "		a.cardcode = b.cardcode " +
                    " WHERE a.frozenFor = 'N' and a.validFor = 'Y' and b.BLOCK = " +
                    "	(" +
                    "		SELECT slpname " +
                    "		FROM OSLP " +
                    "		WHERE SLPCODE =		" + codigoIngeniero +
                    "	)" +
                    " union " +
                    " select cardcode, cardname " +
                    " from ocrd " +
                    " where frozenFor = 'N' and validFor = 'Y' and slpcode = " + codigoIngeniero + 
                    " ORDER BY CARDNAME";

                dsClientes = ClaseDatos.procesaDataSet(cSql);
            }
            catch (Exception ex)
            {
                mensaje = "Se presento un problema al tratar obtener los clientes asociados al ingeniero de ventas. " + ex.Message;
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }

            return dsClientes;
        }

        /// <summary>
        /// Obtiene los productos que seran usados para el presupuesto
        /// </summary>
        public DataSet ObtenerProductos(OrigenProductos origenProductos, string codigoCliente)
        {
            DataSet dsProductos = new DataSet();
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

            try
            {
                switch (origenProductos)
                {
                    case OrigenProductos.catalogoClientes:
                        dsProductos = ClaseDatos.procesaDataSet("select b.itemname, a.itemcode from OSCN a inner join OITM b on a.ItemCode = b.ItemCode where a.CardCode = '" + codigoCliente + "' and frozenFor = 'N' and validFor = 'Y' order by itemname");
                        break;
                    case OrigenProductos.maestroArticulos:                        
                        dsProductos = ClaseDatos.procesaDataSet("select itemname, itemcode from oitm where frozenFor = 'N' and validFor = 'Y' order by itemname");
                        break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {
                mensaje = "Se presento un problema al tratar obtener los clientes asociados al ingeniero de ventas. " + ex.Message;
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }

            return dsProductos;
        }

        /// <summary>
        /// Obtiene las ciudades de las sucursales
        /// </summary>
        public DataSet ObtenerSucursales()
        {
            DataSet dsSucursales = new DataSet();
            ClaseDatos.SqlConnex(ClaseDatos.objCompany.CompanyDB.ToString());

            try
            {
                dsSucursales = ClaseDatos.procesaDataSet("Select Code,Name from [@CSS_SUCURSALES] order by Name");
            }
            catch (Exception ex)
            {
                mensaje = "Se presento un problema al tratar obtener llas sucursales. " + ex.Message;
            }
            finally
            {
                ClaseDatos.SqlConn.Close();
            }

            return dsSucursales;
        }
        
        /// <summary>
        /// Obtiene el numero de presupuesto segun los parametros enviados
        /// </summary>
        public int ObtenerNumeroPresupuesto(int año, int ingeniero)
        {
            int res = 0;

            try
            {
                string cSql = "Select U_id_estimado from [@CSS_ESTIMAVENT_HEAD] where U_ano_estim = " + año.ToString() + " and U_id_ing_slpcode = '" + ingeniero.ToString() + "'";
                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (dsPresupuesto.Tables[0].Rows.Count > 0) { res = (int)dsPresupuesto.Tables[0].Rows[0][0]; }
            }
            catch (Exception ex) { mensaje = "Se presento un error al intentar obtener el id del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Obtiene el numero de presupuesto por item
        /// </summary>
        public int ObtenerNumeroPresupuesto(int idPresupuesto, string idCliente, int mes, string idItem)
        {
            int res = 0;

            try
            {
                string cSql = "Select code from [@ESTIMADO_VENTA_LINE] where U_id_estima_venta = " + idPresupuesto.ToString()
                        + " and U_cliente = '" + idCliente + "' and U_mes = " + mes.ToString() + " and U_item = '" + idItem + "'";

                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (dsPresupuesto.Tables[0].Rows.Count > 0) { res = int.Parse(dsPresupuesto.Tables[0].Rows[0][0].ToString()); }
            }
            catch (Exception ex) { mensaje = "Se presento un error al intentar obtener el id del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Obtiene el numero siguiente de presupuesto
        /// </summary>
        public int ObtenerSiguienteNumeroPresupuesto()
        {
            int res = 0;

            try
            {
                string cSql = "Select isnull(max(cast(code as int)), 0) + 1 from [@CSS_ESTIMAVENT_HEAD] ";
                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (dsPresupuesto.Tables[0].Rows.Count > 0) { res = (int)dsPresupuesto.Tables[0].Rows[0][0]; }
            }
            catch (Exception ex) { mensaje = "Se presento un error al intentar asignar el id del presupuesto" + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }
        
        /// <summary>
        /// Obtiene el numero siguiente de item del presupuesto por articulo
        /// </summary>
        public int ObtenerSiguienteNumeroPresupuestoPorArticulo()
        {
            int res = 0;

            try
            {
                string cSql = "Select isnull(max(cast(code as int)), 0) + 1 from [@ESTIMADO_VENTA_LINE] ";
                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (dsPresupuesto.Tables[0].Rows.Count > 0) { res = (int)dsPresupuesto.Tables[0].Rows[0][0]; }
            }
            catch (Exception ex) { mensaje = "Se presento un error al intentar asignar el id del presupuesto por articulo. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Obtiene el numero siguiente de item del presupuesto por ciudad
        /// </summary>
        public int ObtenerSiguienteNumeroPresupuestoPorCiudad()
        {
            int res = 0;

            try
            {
                string cSql = "Select isnull(max(cast(code as int)), 0) + 1 from [@CSS_ESTVENTAS_LINE2] ";
                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (dsPresupuesto.Tables[0].Rows.Count > 0) { res = (int)dsPresupuesto.Tables[0].Rows[0][0]; }
            }
            catch (Exception ex) { mensaje = "Se presento un error al intentar asignar el id del presupuesto por ciudad. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Devuelva la estadistica del presupuesto 
        /// </summary>
        public DataSet ObtenerEstadistica(int ingenieroVtas, string idCliente, string idItem)
        {
            DataSet dsEstadistica = new DataSet();

            try
            {
                string cSql = " SET DATEFORMAT YMD declare  @fromSLP as CHAR(32)  set @fromSLP =" + ingenieroVtas.ToString() +
                    " declare  @Item as CHAR(32) set @Item ='" + idItem + "' declare @Cliente as CHAR(32) set @Cliente = '" + idCliente + "'" +
                    " declare  @todate as DATETIME 	set @todate = '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'    ";

                string cPath = System.Windows.Forms.Application.StartupPath + ConfigurationManager.AppSettings["QueryEstadistica"].ToString();

                StreamReader oReader = new StreamReader(cPath);

                string cQueryEstadistica = oReader.ReadToEnd();

                dsEstadistica = ClaseDatos.procesaDataSet(cSql + cQueryEstadistica);
            }
            catch (Exception ex) { mensaje = "Se presento un error al tratar de guardar el registro de items del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return dsEstadistica;
        }

        /// <summary>
        /// Inserta el registro de cabecera del presupuesto 
        /// </summary>
        public bool guardarPresupuesto(int año, int ingeniero)
        {
            bool res = false;

            try
            {
                int idPresupuesto = ObtenerSiguienteNumeroPresupuesto();

                string cSql = "Insert into  [@CSS_ESTIMAVENT_HEAD] select '" + idPresupuesto.ToString() + "', '" + idPresupuesto.ToString()
                    + "', " + año.ToString() + ", " + idPresupuesto.ToString() + ", '" +ingeniero.ToString() + "'";

                if (ClaseDatos.nonQuery(cSql) == "Datos actualizados con exito")
                {
                    res = true;
                }                
            }
            catch (Exception ex) { mensaje = "Se presento un error al tratar de guardar el presupuesto por ingeniero. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Inserta el presupuesto por articulo 
        /// </summary>
        public bool guardarPresupuesto(int idPresupuesto, string idCliente, int mes, string idItem, double cantidad, string nombreItem)
        {
            bool res = false;

            try
            {
                string cSql="", cSqlBorrarDetalleCiudad = "";
                int idPresupuestoArticulo;

                if (!existePresupuesto(idPresupuesto, idCliente, mes, idItem))
                {
                    idPresupuestoArticulo = ObtenerSiguienteNumeroPresupuestoPorArticulo();

                    cSql = "Insert into [@ESTIMADO_VENTA_LINE] (code, name, u_cliente, u_estimadokg, u_id_estima_venta, u_item, u_mes, u_nombre_item) select '" + idPresupuestoArticulo.ToString() + "', '" + idPresupuestoArticulo.ToString()
                        + "', '" + idCliente + "', " + cantidad.ToString(new System.Globalization.CultureInfo("en-US")) + ", " + idPresupuesto.ToString() + ", '" + idItem + "', " + mes.ToString() + ", '"
                        + nombreItem + "'";
                    if (ClaseDatos.nonQuery(cSql) == "Datos actualizados con exito")
                    {
                        res = true;
                    }
                }
                else
                {
                    cSql = "Update [@ESTIMADO_VENTA_LINE] set U_estimadokg = " + cantidad.ToString(new System.Globalization.CultureInfo("en-US")) + " where U_id_estima_venta = " + idPresupuesto.ToString() 
                        + " and U_cliente = '" + idCliente + "' and U_mes = " + mes.ToString() + " and U_item = '" + idItem + "'";

                    idPresupuestoArticulo = ObtenerNumeroPresupuesto(idPresupuesto, idCliente, mes, idItem);

                    cSqlBorrarDetalleCiudad = "Delete from [@CSS_ESTVENTAS_LINE2] where U_CSS_ID_PRESUPUESTO = " + idPresupuesto.ToString() + " and U_CSS_ID_PRE_LINES = " + idPresupuestoArticulo.ToString() ;

                    if (ClaseDatos.nonQuery(cSql) == "Datos actualizados con exito" && ClaseDatos.nonQuery(cSqlBorrarDetalleCiudad) == "Datos actualizados con exito") 
                    {
                        res = true;
                    }
                }
            }
            catch (Exception ex) { mensaje = "Se presento un error al tratar de guardar el registro de items del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Inserta el presupuesto por ciudad
        /// </summary>
        public bool guardarPresupuesto(int idPresupuesto, int idPresupuestoArticulo, int mes, string ciudad, double cantidad)
        {
            bool res = false;

            try
            {
                int idPresupuestoCiudad = ObtenerSiguienteNumeroPresupuestoPorCiudad();

                string cSql = "Insert into [@CSS_ESTVENTAS_LINE2] (code, name, u_css_id_presupuesto, u_css_id_pre_lines, u_css_mes_presupu, u_css_sucursal, u_css_cantidad1, u_css_cantidad) select '" + idPresupuestoCiudad.ToString() + "', '" + idPresupuestoCiudad.ToString()
                       + "', " + idPresupuesto.ToString() + ", " + idPresupuestoArticulo.ToString() + ", " + mes.ToString() + ", '" + ciudad + "', null, "
                       + cantidad.ToString(new System.Globalization.CultureInfo("en-US"));

                
                if (ClaseDatos.nonQuery(cSql) == "Datos actualizados con exito") {res = true;}
            }
            catch (Exception ex) { mensaje = "Se presento un error al tratar de guardar el registro de items del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Verifica si la fecha permite el ingreso de estimados
        /// </summary>
        public bool ValidaFechaEstimados()
        {
            bool res = false;

            try
            {
                string cSql = "select case when COUNT(*) > 0 then cast('true' as bit) else CAST('false' as bit) end  from [@CSS_PRESUPUESTOS_PA] where getdate() between U_CSS_ESTIM_FECHA and U_CSS_FECHA_CORTE";

                DataSet dsFechaValida = ClaseDatos.procesaDataSet(cSql);

                if (bool.Parse(dsFechaValida.Tables[0].Rows[0][0].ToString()) == true)
                {
                    res = true;
                }
            }
            catch (Exception ex) { mensaje = "Se presento un error al validar la existencia del detalle por ciudad de los items por cliente del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Valida si el mes evaluado es valido para ingresar datos
        /// </summary>
        public bool ValidaMes(int mes)
        {
            bool res = false;

            try
            {
                int mesActual = DateTime.Now.Month;

                if (mes > mesActual && mes - mesActual > 1 && mes - mesActual <= 4)
                {
                    res = true;
                }

                //if (mes < mesActual && mesActual - mes > 2 && mesActual - mes >= 10)
                if (mes < mesActual && mesActual - mes >= 8 && mesActual - mes <= 10)
                {
                    if (mesActual == 12)
                    {
                        if (mes > 1)
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    }
                    else
                    {
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            { mensaje = "Se presento un error al intentar validar el mes"; }

            return res;
        }

        /// <summary>
        /// Verifica si ya existe un registro para el ingeniero de ventas en el año actual
        /// </summary>
        public bool existePresupuesto(int año, int ingeniero)
        {
            bool res = false;

            try
            {
                string cSql = "Select count(*) from [@CSS_ESTIMAVENT_HEAD] where U_ano_estim = " + año.ToString() + " and U_id_ing_slpcode = '" + ingeniero.ToString() + "'";
                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (int.Parse(dsPresupuesto.Tables[0].Rows[0][0].ToString()) != 0) { res = true; }
            }
            catch (Exception ex) { mensaje = "Se presento un error al validar la existencia del presupuesto por ingeniero" + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Verifica si ya existe un registro para el cliente e item en un mes determinado 
        /// </summary>
        public bool existePresupuesto(int idPresupuesto, string idCliente, int mes, string idItem)
        {
            bool res = false;

            try
            {
                string cSql = "Select count(*) from [@ESTIMADO_VENTA_LINE] where U_id_estima_venta = " + idPresupuesto.ToString() + " and U_cliente = '" + idCliente
                    + "' and U_mes = " + mes.ToString() + " and U_item = '" + idItem + "'";

                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (int.Parse(dsPresupuesto.Tables[0].Rows[0][0].ToString()) != 0) { res = true; }
            }
            catch (Exception ex) { mensaje = "Se presento un error al validar la existencia del detalle de items por cliente del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }

        /// <summary>
        /// Verifica si ya existe un registro para el item en una ciudad determinado 
        /// </summary>
        public bool existePresupuesto(int idPresupuesto, int idPresupuestoArticulo, int mes, string ciudad)
        {
            bool res = false;

            try
            {
                string cSql = "Select count(*) from [@CSS_ESTVENTAS_LINE2] where U_CSS_ID_PRESUPUESTO = " + idPresupuesto.ToString() + " and U_CSS_ID_PRE_LINES = "
                    + idPresupuestoArticulo.ToString() + " and U_CSS_MES_PRESUPU = " + mes.ToString() + " and U_CSS_SUCURSAL = '" + ciudad + "'";

                DataSet dsPresupuesto = ClaseDatos.procesaDataSet(cSql);

                if (int.Parse(dsPresupuesto.Tables[0].Rows[0][0].ToString()) != 0) { res = true; }
            }
            catch (Exception ex) { mensaje = "Se presento un error al validar la existencia del detalle por ciudad de los items por cliente del presupuesto. " + ex.Message; }
            finally { ClaseDatos.SqlConn.Close(); }

            return res;
        }
    }
}

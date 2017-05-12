using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BP
{
    /// <summary>
    /// Clase que controla todo el negocio de las transacciones relacionadas con manejo de inventarios
    /// </summary>
    public static class BusinessInventario
    {
        #region Atributos
        /// <summary>
        /// Permite el acceso módulo de inventario
        /// </summary>
        private static ClsDataInventario accesoInventario;
        /// <summary>
        /// Conexión con SAP Business One
        /// </summary>
        //public DataConexionSAP midataConexion;
        #endregion

        #region Métodos

        /// <summary>
        /// Lista todos los artículos en SAP Business One
        /// </summary>
        /// <param name="conexion">Conexion del servicio. Para mayor informacion revise la documentacion de entidades</param>
        /// <param name="tipoConsulta">Tipo de consulta</param>
        /// <param name="fecha">Fecha desde la cual se consulta</param>
        /// <returns>Lista de Artículos. Para mayor informacion revise la documentacion de entidades</returns>
        public static List<Articulo> ListarArticulos(string tipoConsulta, DateTime fecha)
        {
            try
            {
                accesoInventario = new ClsDataInventario();
                return accesoInventario.ListarArticulos(tipoConsulta, fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// Lista todos los artículos en SAP Business One
        /// </summary>
        /// <param name="conexion">Conexion del servicio. Para mayor informacion revise la documentacion de entidades</param>
        /// <param name="tipoConsulta">Tipo de consulta</param>
        /// <param name="fecha">Fecha desde la cual se consulta</param>
        /// <returns>Lista de Artículos. Para mayor informacion revise la documentacion de entidades</returns>
        public static List<Articulo> ListarArticulos(string familia)
        {
            try
            {
                accesoInventario = new ClsDataInventario();
                return accesoInventario.ListarArticulos(familia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// Consulta de artículos por el código del almacén
        /// </summary>
        /// <param name="codigoAlmacen">Código del almacen en SAP Business One</param>
        /// <param name="conexion">Conexion del servicio. Para mayor informacion revise la documentacion de entidades</param>
        /// <returns>Lista de Articulos por almacen</returns>
        public static List<Articulo> ConsultarArticulosXAlmacen(string codigoAlmacen)
        {
            try
            {
                accesoInventario = new ClsDataInventario();
                List<Articulo> listaArticulos = new List<Articulo>();
                if (!accesoInventario.ValidarExistenciaAlmacen(new Almacen() { WhsCode = codigoAlmacen }))
                    throw new Exception("El almacen no se encuentra registrado en SAP Business One");
                else
                {
                    listaArticulos = accesoInventario.ConsultarArticulosXAlmacen(new Almacen() { WhsCode = codigoAlmacen });
                    if (listaArticulos.Count == 0)
                        throw new Exception("No Existen Artículos para el almacén");
                    else
                        return listaArticulos;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public static List<Almacen> ListarAlmacenes()
        {
            try
            {
                accesoInventario = new ClsDataInventario();
                return accesoInventario.ListarAlmacenes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// Lista todos los almacenes en SAP Business One
        /// </summary>
        /// <param name="conexion">Conexion del servicio. Para mayor informacion revise la documentacion de entidades</param>
        /// <param name="fecha">Fecha desde la cual se consulta</param>
        /// <param name="tipo">Tipo de consulta</param>
        /// <returns>Lista de Almacenes. Para mayor informacion revise la documentacion de entidades</returns>
        public static List<Almacen> ListarAlmacenes(DateTime fecha, string tipo)
        {
            try
            {
                accesoInventario = new ClsDataInventario();
                return accesoInventario.ListarAlmacenes(tipo, fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// Consulta de lotes disponibles por artículo y bodega
        /// </summary>
        /// <param name="articulo">Código del artículo en SAP Business One</param>
        /// <param name="bodega">Código de la Bodega en SAP Business One</param>
        /// <param name="conexion">Conexion del servicio. Para mayor informacion revise la documentacion de entidades</param>
        /// <returns>Lista de lotes disponibles por articulo y bodega con las cantidades</returns>
        public static List<Lote> ConsultarLotesDisponibles(string articulo, string bodega)
        {
            try
            {
                accesoInventario = new ClsDataInventario();

                Articulo articuloConsultado = accesoInventario.ConsultarArticulo(articulo);
                if (articuloConsultado.ItemCode.Length == 0)
                    throw new Exception("No existe el Item en SAP Business One como artículo de inventario");
                else if (articuloConsultado.Gestionado != Articulo.Gestion.Lotes)
                    throw new Exception("El artículo no es gestionado por lotes");
                List<Lote> lotes = accesoInventario.ConsultarLotesDisponibles(articulo, bodega);
                if (lotes.Count == 0)
                    throw new Exception("No existe lotes disponibles para el artículo");
                return lotes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// Crea una transferencia de inventario
        /// </summary>
        /// <param name="documento">Documento con la información de la  transferencia</param>
        /// <param name="conexion">Conexión con el servicio. Para mayor informacion revise la documentacion de entidades</param>        
        /// <returns>Transferencia de Inventario</returns>
        public static int CrearTransferenciaInventario(Transferencia documento)
        {
            try
            {
                accesoInventario = new ClsDataInventario();

                //Validación de existencia de los artículos que se envian para la orden de producción
                if (documento.lineas == null)
                    throw new Exception("El documento no tiene líneas");
                else if (documento.lineas.Count == 0)
                    throw new Exception("El documento no tiene líneas");
                if (!accesoInventario.ValidarExistenciaAlmacen(new Almacen() { WhsCode = documento.WhsCode }))
                    throw new Exception("El almacen no se encuentra registrado en SAP Business One: " + documento.WhsCode);
                foreach (TransferenciaLinea linea in documento.lineas)
                {
                    Articulo articulo = accesoInventario.ConsultarArticulo(linea.ItemCode);
                    if (articulo.ItemCode.Length == 0)
                        throw new Exception("No existe el Item en SAP Business One como artículo de inventario: " + linea.ItemCode);
                    if (!accesoInventario.ValidarExistenciaAlmacen(new Almacen() { WhsCode = linea.WhsCode }))
                        throw new Exception("El almacen no se encuentra registrado en SAP Business One: " + linea.WhsCode);
                    if (linea.WhsCode == documento.WhsCode)
                        throw new Exception("El almacen origen no puede ser igual al almacen destino en SAP Business One: " + linea.WhsCode);
                    if (linea.WhsCode == documento.Comments)
                        throw new Exception("Debe ingresar un comentario");

                    if (articulo.Gestionado == Articulo.Gestion.Lotes)
                    {

                        linea.BatchNumbers.Add(new Lote() { DistNumber = "EnvaseDevolutivo", Quantity = linea.Quantity });
                    }
                }

                accesoInventario.CrearTransferenciaInventario(documento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return documento.DocEntry;
        }

        /// <summary>
        /// Consulta de seriales disponibles por artículo y bodega
        /// </summary>
        /// <param name="articulo">Código del artículo en SAP Business One</param>
        /// <param name="bodega">Código de la Bodega en SAP Business One</param>
        /// <param name="conexion">Conexión con el servicio. Para mayor informacion revise la documentacion de entidades</param>
        /// <param name="fechaInicio">Fecha de Inicio</param>
        /// <param name="fechaFin">Fecha de Fin</param>
        /// <param name="tipoConsulta">Tipo de consulta (Fechas o Artículo)</param>
        /// <returns>Lista de seriales disponibles con las cantidades</returns>        
        public static List<SerialNumber> ConsultarSerialesDisponibles(string articulo, string bodega, DateTime fechaInicio, DateTime fechaFin, string tipoConsulta)
        {
            try
            {
                accesoInventario = new ClsDataInventario();

                if (tipoConsulta.Equals("Articulo"))
                {
                    Articulo articuloConsultado = accesoInventario.ConsultarArticulo(articulo);

                    if (articuloConsultado.ItemCode.Length == 0)
                        throw new Exception("No existe el Item en SAP Business One como artículo de inventario");
                    else if (articuloConsultado.Gestionado != Articulo.Gestion.Series)
                        throw new Exception("El artículo no es gestionado por Series");
                    List<SerialNumber> seriales = accesoInventario.ConsultarSerialesDisponibles(articulo, bodega);
                    if (seriales.Count == 0)
                        throw new Exception("No existe series disponibles para el artículo");
                    return seriales;
                }
                //else
                //{
                //    List<SerialNumber> seriales = accesoInventario.ConsultarSerialesporFechas(fechaInicio, fechaFin);
                //    if (seriales.Count == 0)
                //        throw new Exception(45, "No existen registros coincidentes de seriales en el rango de fechas enviado");
                //    return seriales;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        #endregion
    }
}

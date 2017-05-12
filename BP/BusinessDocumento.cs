using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BP
{
    public class BusinessDocumento
    {
        public static int CrearEntradaMercancia(DocumentoMktng documento)
        {
            try
            {
                ClsDataDocumentos gestionDocumentos = new ClsDataDocumentos();
                ClsDataInventario accesoInventario = new ClsDataInventario();

                if (documento.lineas == null)
                    throw new Exception("El documento no tiene líneas");
                else if (documento.lineas.Count == 0)
                    throw new Exception("El documento no tiene líneas");


                foreach (DocumentoLineas linea in documento.lineas)
                {
                    Articulo articulo = accesoInventario.ConsultarArticulo(linea.ItemCode);
                    if (articulo.ItemCode.Length == 0)
                        throw new Exception("No existe el Item en SAP Business One como artículo de inventario: " + linea.ItemCode);

                    if (!accesoInventario.ValidarExistenciaAlmacen(new Almacen() { WhsCode = linea.WhsCode }))
                        throw new Exception("El almacen no se encuentra registrado en SAP Business One: " + linea.WhsCode);

                    if (articulo.Gestionado == Articulo.Gestion.Lotes)
                    {
                        linea.BatchNumbers.Add(new Lote() { DistNumber = "EnvaseDevolutivo", Quantity = linea.Quantity });                    
                    }                    
                }

                gestionDocumentos.CrearEntradaMercancia(documento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return documento.DocEntry;
        }        
    }
}

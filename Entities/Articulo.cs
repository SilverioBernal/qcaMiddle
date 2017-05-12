using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Articulo
    {
        #region Atributos
        /// <summary>
        /// ID Producto
        /// </summary>        
        public string ItemCode { set; get; }

        /// <summary>
        /// Código de barras
        /// </summary>
        public string CodeBars { set; get; }
        
        /// <summary>
        /// Descripción del artículo
        /// </summary>
        public string ItemName { set; get; }
        
        /// <summary>
        ///Estado del artículo Valido o Inactivo para la fecha de consulta
        /// </summary>
        public string Status { set; get; }
        
        /// <summary>
        ///Clase Articulo 
        /// </summary>
        public string ItemType { set; get; }
        
        /// <summary>
        ///Grupo Artículo
        /// </summary>
        public string ItmsGrpCode { set; get; }
        
        /// <summary>
        /// Nombre Grupo Artículo
        /// </summary>
        public string ItmsGrpName { set; get; }
        
        /// <summary>
        /// Es de Inventario
        /// </summary>
        public bool InventoyItem { set; get; }
        
        /// <summary>
        /// Es de Venta
        /// </summary>
        public bool SalesItem { set; get; }
        
        /// <summary>
        /// Es de Compra
        /// </summary>
        public bool PurchaseItem { set; get; }
        
        /// <summary>
        /// Es artículo virtual
        /// </summary>
        public bool Panthom { set; get; }
        
        /// <summary>
        /// Unidad de medida de compra
        /// </summary>
        public string BuyUnitMsr { set; get; }
        
        /// <summary>
        /// Unidad de medida de empaque en compras
        /// </summary>
        public string PurPackMsr { set; get; }
        
        /// <summary>
        /// Artículos por unidad de compras
        /// </summary>
        public double NumInBuy { set; get; }
        
        /// <summary>
        /// Unidad de medida de ventas
        /// </summary>
        public string SalUnitMsr { set; get; }
        
        /// <summary>
        /// Unidad de medida de empaque en ventas
        /// </summary>
        public string SalPackMsr { set; get; }

        public double AvgPrice { get; set; }
        
        /// <summary>
        /// Artículos por unidad de ventas
        /// </summary>
        public double NumInSale { set; get; }
        
        /// <summary>
        /// Tipo de gestión del artículo
        /// </summary>
        public Gestion Gestionado { set; get; }
        
        /// <summary>
        /// Tipos de gestión que puede tener un artículo
        /// </summary>    
        public enum Gestion
        {
            /// <summary>
            /// Artículo gestionado por series
            /// </summary>
            Series,
            /// <summary>
            /// Artículo gestionado por lotes
            /// </summary>
            Lotes,
            /// <summary>
            /// Artículo sin método de gestión definido
            /// </summary>
            Ninguno
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Inicia los atributos de la clase
        /// </summary>
        public Articulo()
        {
            this.ItemCode = String.Empty;
            this.CodeBars = String.Empty;
            this.ItemName = String.Empty;
            this.Status = String.Empty;
            this.ItemType = String.Empty;
            this.ItmsGrpCode = String.Empty;
            this.ItmsGrpName = String.Empty;
            this.InventoyItem = false;
            this.SalesItem = false;
            this.PurchaseItem = false;
            this.Panthom = false;
            this.BuyUnitMsr = String.Empty;
            this.PurPackMsr = String.Empty;
            this.NumInBuy = 0;
            this.SalUnitMsr = String.Empty;
            this.SalPackMsr = String.Empty;
            this.NumInSale = 0;
            this.Gestionado = Gestion.Ninguno;
            
        }
        #endregion
    }
}

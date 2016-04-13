using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entities;

namespace BP
{
    public class ClsCommonData
    {
        #region Atributos
        /// <summary>
        /// Lector
        /// </summary>
        private IDataReader reader;
        #endregion

        #region Metodos
        public List<Territory> GetTerritories()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  territryID, descript FROM OTER T0 order by descript ");

            List<Territory> territories = new List<Territory>();

            using (this.reader = ClaseDatos.procesaDataReader(oSQL.ToString()))
            {
                while (this.reader.Read())
                {
                    Territory terrotiry = new Territory();
                    terrotiry.territryID = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    terrotiry.descript = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    territories.Add(terrotiry);
                }
            }

            return territories;
        }
        #endregion
    }
}

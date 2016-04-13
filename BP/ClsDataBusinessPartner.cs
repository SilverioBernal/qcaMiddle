using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Configuration;
using System.IO;
using System.Data;

namespace BP
{
    public class ClsDataBusinessPartner
    {
        #region Atributos
        /// <summary>
        /// Lector
        /// </summary>
        private IDataReader reader;
        #endregion

        #region Metodos
        public List<GenericBusinessPartner> GetList(CardType cardType)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  CardCode, CardName FROM OCRD T0 ");

            switch (cardType)
            {
                case CardType.Customer:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y' and [frozenFor] = 'N' AND [validTo] IS NULL", "C"));
                    break;
                case CardType.Supplier:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y' ", "S"));
                    break;
                case CardType.Lead:
                    oSQL.Append(string.Format("where CardType = '{0}' ", "L"));
                    break;
                default:
                    break;
            }

            List<GenericBusinessPartner> partners = new List<GenericBusinessPartner>();

            using (this.reader = ClaseDatos.procesaDataReader(oSQL.ToString()))
            {
                while (this.reader.Read())
                {
                    GenericBusinessPartner partner = new GenericBusinessPartner();
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    partners.Add(partner);
                }
            }

            return partners;
        }
        #endregion

        
    }
}

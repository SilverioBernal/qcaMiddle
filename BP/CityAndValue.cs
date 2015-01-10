using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP
{
   
        public class CityAndValue
        {
            string cityid = null;
            double valu = 0;



            public string Ciudad
            {

                get { return cityid; }

            }
            public double Valor
            {

                get { return valu; }

            }



            public CityAndValue(string cityID, double value)
            {

                this.cityid = cityID;
                this.valu = value;

            }
            public override string ToString()
            {
                return base.ToString();
            }


        }
    
}

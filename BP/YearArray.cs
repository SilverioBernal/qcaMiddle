using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP
{
    class YearArray
    {
        Array cityvalue;
        int _mes = 0;
    
        public YearArray(Array cityValueArr, int mes)
        {

            this.cityvalue = cityValueArr;
            this._mes = mes;
        
        }
    }

    class Month_ly
    {
        Array cityvalue;
        int _mes = 0;

        public Month_ly(Array cityValueArr, int mes)
        {

            this.cityvalue = cityValueArr;
            this._mes = mes;

        }
    }

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INV.Core
{
    public static class Common
    {
        public static List<dynamic> DatatableToObject(DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    string aa = column.ToString();
                    var dic = (IDictionary<string, object>)dyn;
                    var data = row[column];
                    if (data != System.DBNull.Value)
                    {
                        dic[column.ColumnName] = row[column];
                    }
                    else
                    {
                        dic[column.ColumnName] = "";
                    }
                }
            }
            return dynamicDt;
        }

        public static string GetPerAmt(float value, float amount)
        {
            float res = 0;
            if (!(amount > 0)) return res.ToString(CultureInfo.InvariantCulture);
            res = (value * amount) / 100;
            if (res < 0) { res = 0; }
            return res.ToString(CultureInfo.CurrentCulture);
        }
        

        public static string IsNumeric(string val)
        {
            var isNum = double.TryParse(val, out _);
            return !isNum ? "0" : val;
        }


    }
}

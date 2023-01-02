using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using ShoppingCart.DAL;
using System.Data.SqlClient;

using System.Configuration;


namespace ShoppingCart.BL
{
    public class Common
    {
        public Common()
        {
        
        }

        public static int ExcelColumnNameToNumber(string columnName)
        {

            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("columnName");

            char[] characters = columnName.ToUpperInvariant().ToCharArray();

            int sum = 0;

            for (int i = 0; i < characters.Length; i++)
            {
                sum *= 26;
                sum += (characters[i] - 'A' + 1);
            }

            return sum;
        }

        public static string RemoveComma(string str)
        {

            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);

            if(str.Length > 0)
            {
                if (str.Substring(str.Length - 1) == ",")
                {
                    str = str.Remove(str.Length - 1);
                }
                //str = str.Substring(0, str.Length);
            }
            //string strg = str;
            //strg = strg.LastIndexOf(",") == strg.Length - 1 ? strg.Substring(0, strg.Length - 1) : strg;

            return str;
        }

        public static DataSet ValidDataSet(DataSet ds)
        {
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
            }
            return ds;
        }

    }
}
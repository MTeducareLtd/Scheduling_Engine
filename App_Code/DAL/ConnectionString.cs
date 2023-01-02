
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
namespace ShoppingCart.BL
{
    sealed class ConnectionString
    {
        private ConnectionString()
        {
        }
        public static string GetConnectionString()
        {
            
            return System.Configuration.ConfigurationSettings.AppSettings["CONSTR"];
        }
    }
}

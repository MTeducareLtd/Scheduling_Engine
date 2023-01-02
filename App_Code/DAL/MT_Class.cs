using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;


public class MT_Class
{
    public string strConn = System.Configuration.ConfigurationSettings.AppSettings["strConn"];
    public string strRmConn = System.Configuration.ConfigurationSettings.AppSettings["strRmConn"];
    public string strConn1 = System.Configuration.ConfigurationSettings.AppSettings["strConn1"];
    public string ConnectIntranet = System.Configuration.ConfigurationSettings.AppSettings["connectIntranet"];
    public string ConnectCtcl = System.Configuration.ConfigurationSettings.AppSettings["connectCtcl"];
    public string ConnectSMS = System.Configuration.ConfigurationSettings.AppSettings["connectSMS"];
    public string ConnectMIS = System.Configuration.ConfigurationSettings.AppSettings["connectMIS"];
    public string connectUpload = System.Configuration.ConfigurationSettings.AppSettings["connectUpload"];
    public string ConnectKYC = System.Configuration.ConfigurationSettings.AppSettings["connectKYC"];
    public string connectHelpdesk = System.Configuration.ConfigurationSettings.AppSettings["connectHelpdesk"];
    public string connectps03 = System.Configuration.ConfigurationSettings.AppSettings["connectps03"];
    public string ConnectAnand = System.Configuration.ConfigurationSettings.AppSettings["ConnectAnand"];
    public string ConnectAnand1 = System.Configuration.ConfigurationSettings.AppSettings["ConnectAnand1"];
    public string ConnectIPO = System.Configuration.ConfigurationSettings.AppSettings["ConnectIPO"];
    public string ConnectBSE = System.Configuration.ConfigurationSettings.AppSettings["ConnectBSE"];
    public string ConnectNSE = System.Configuration.ConfigurationSettings.AppSettings["ConnectNSE"];
    public string ConnectOdin = System.Configuration.ConfigurationSettings.AppSettings["ConnectOdin"];
    public string ConnectGraph = System.Configuration.ConfigurationSettings.AppSettings["ConnectGraph"];
    public string ConnectNseCour = System.Configuration.ConfigurationSettings.AppSettings["ConnectNseCour"];
    public string ConnectMimansa = System.Configuration.ConfigurationSettings.AppSettings["ConnectMimansa"];

    public string ConnectError = System.Configuration.ConfigurationSettings.AppSettings["connectError"];
    public SqlCommand Sql_Com = new SqlCommand();
    public string[] Sql_Str = new string[101];
    public SqlDataReader Sql_Dr;
    public SqlDataAdapter Sql_Da = new SqlDataAdapter();
    public DataSet Sql_DS = new DataSet();

    public static DataSet sql_ds1 = new DataSet();

    public void Execute_Query(string Sql_Str, string Con_str)
    {
        SqlConnection Sql_Con = new SqlConnection(Con_str);
        Sql_Com.Connection = Sql_Con;
        Sql_Com.Connection.Open();
        Sql_Com.CommandText = Sql_Str;
        Sql_Com.CommandTimeout = 20000;
        Sql_Com.ExecuteNonQuery();

        Sql_Con.Close();
    }

    public object Execute_Query1(string Sql_Str, string Con_str)
    {
        string return_val = "";

        SqlConnection Sql_Con = new SqlConnection(Con_str);
        Sql_Com.Connection = Sql_Con;
        Sql_Com.Connection.Open();
        Sql_Com.CommandText = Sql_Str;
        Sql_Com.CommandTimeout = 20000;
        return_val = Sql_Com.ExecuteScalar().ToString();//changed by jayant

        Sql_Con.Close();
        return return_val;
    }



    public void Select_Records(string Sql_Str, string Con_str)
    {
        SqlConnection Sql_Con = new SqlConnection(Con_str);
        Sql_Com.Connection = Sql_Con;
        Sql_Com.Connection.Open();
        Sql_Com.CommandText = Sql_Str;
        Sql_Com.CommandTimeout = 2000;
        Sql_Dr = Sql_Com.ExecuteReader();
        Sql_Dr.Read();

    }

    public void Execute_Qry(string Sql_Str, string Con_str)
    {
        SqlConnection Sql_Con = new SqlConnection(Con_str);
        if (Sql_Con.State == ConnectionState.Closed)
            Sql_Con.Open();
        Sql_Com.Connection = Sql_Con;
        Sql_Com.CommandTimeout = 30000;
        Sql_Com.CommandText = Sql_Str;
        Sql_Da.SelectCommand = Sql_Com;
        Sql_DS.Clear();
        Sql_Da.Fill(Sql_DS, "Source");
        Sql_Con.Close();
    }

    public void Execute_sharedQry(string Sql_Str, string Con_str)
    {
        SqlConnection Sql_Con = new SqlConnection(Con_str);
        if (Sql_Con.State == ConnectionState.Closed)
            Sql_Con.Open();
        Sql_Com.Connection = Sql_Con;
        Sql_Com.CommandTimeout = 30000;
        Sql_Com.CommandText = Sql_Str;
        Sql_Da.SelectCommand = Sql_Com;
        sql_ds1.Clear();
        Sql_Da.Fill(sql_ds1, "Source");
        Sql_Con.Close();
    }

    public string RemoveComma(string str)
    {
        string strg = str;
        strg = strg.LastIndexOf(",") == strg.Length - 1 ? strg.Substring(0, strg.Length - 1) : strg;

        return strg;
    }


}

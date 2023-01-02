
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

public class ClsCommon
{
    public static string strDataSource;
    //to store the database
    public static string strDatabase;
    //to store the user id
    public static string strDBUserID;
    //to store the pwd
    public static string strDBPwd;
    //to store the command timeout
    public static long lngCmdTimeout;
    //to store the db connection string
    public static string strDBConn;
    //to store flag for password encryption
    public static string strIsEncrypt;
    //= "admin"
    public static string StrUserName;
    public static string strError;
    public static string strSelectedSheet;
    public static SqlConnection cn;
    public static string sMFDB;

    public static string GetCurrentDate()
    {
        //Data.DataSet dsDate = default(Data.DataSet);
        //dsDate = ExecuteSQL("SELECT CONVERT(VARCHAR, GETDATE(), 103) AS CurDate");
        //return dsDate.Tables(0).Rows(0)(0).ToString();

        return DateTime.Today.ToString("dd-MM-yyyy") ;
    }

    //Name:
    // GetDate
    //Parameter:
    // 1)Date
    // 2)Date Interval
    // 3)Flag
    //     P: Previous Date
    //     F: Future Date
    //Purpose:
    // This function return the Previous or future date depending upon the flag
    public static string GetDate(object strDate, int nInterval, string strFlag)
    {
        //if previous date is required 
        if (strFlag == "P")
        {
            //return previous date

            return Convert.ToDateTime(strDate).AddDays(-nInterval).ToString("dd-MM-yyyy");// DateAdd(DateInterval.DayOfYear, -nInterval, Convert.ToDateTime(strDate)).ToString("dd-MM-yyyy");
        }
        else
        {
            //return future date
            return Convert.ToDateTime(strDate).AddDays(nInterval).ToString("dd-MM-yyyy"); //DateAdd(DateInterval.DayOfYear, nInterval, Convert.ToDateTime(strDate)).ToString("dd-MM-yyyy");
        }
    }

    //Name:
    //	IsFutureDate
    //Parameters:
    //	1) Date
    //Return Type:
    //	1)Boolean
    //Details:
    //	This method is used to retrieve the current date using the stored procedure uspIBOGeneral
    //	by passing the flag as GET_CURRENT_DATE and then check if the date is greater or not
    public static bool IsFutureDate(System.DateTime dtDate)
    {
        //check for the future date validation and return the boolean value
        return CheckDate(dtDate, "F");
    }
    //Name:
    //	CheckDate
    //Parameters:
    //	1) Date
    //	2) Flag
    //		P: Past Date
    //		F: Future Date
    //Return Type:
    //	1)Boolean
    //Details:
    //	This function is used to retrieve the current date using the 
    //	stored procedure uspIBOGeneral by passing the flag as GET_CURRENT_DATE 
    //	and then check if the date is future date or past date as specified in the flag
    private static bool CheckDate(System.DateTime dtDate, string strFlag)
    {
        //if the specified date is greater than todays date
        if (dtDate > GetServerDate())
        {
            return (strFlag == "F" ? true : false);
            //if the specified date is less than todays date
        }
        else if (dtDate < GetServerDate())
        {
            return (strFlag == "F" ? false : true);
        }
        //if the specified date is same as todays date
        return (false);
    }
    //Name:
    // GetServerDate
    //Parameters:
    // None
    //Details:
    // This function returns the date on server
    public static System.DateTime GetServerDate()
    {
        //to store the dataset returned by the request
        //Data.DataSet dsDate = new Data.DataSet();
        //to store an instance of the http comm class
        //Dim obj As New clsHttpComm
        //'to store the input parameters
        //Dim arr(3) As String
        //'mode
        //arr(0) = "GET_CURRENT_DATE"
        //'parameter 1
        //arr(1) = ""
        //'parameter 2
        //arr(2) = ""
        //'parameter 3
        //arr(3) = ""

        //'execute the request and retrieve the current date
        //Return obj.ExecuteRequest(clsXML.FormXMLData(clsRIDs.Q_GENERAL, arr)).Tables(0).Rows(0)(0)
        return DateTime.Today ;
    }
    public static string FormatDate(string DateVariable)
    {

        string strFinalDate = null;
        try
        {
            //
            //           Global_Methods.LogMessage("FinalDateFormat_Start", 0, DateVariable, "Info")

            System.Globalization.CultureInfo dateFormat = new System.Globalization.CultureInfo("en-GB", true);

            DateTime dt = default(DateTime);
            dt = DateTime.Parse(DateVariable, dateFormat);
            //          Global_Methods.LogMessage("FinalDateFormat_Parse", 0, dt, "Info")
            strFinalDate = dt.ToString ("yyyy-MM-dd");
            //         Global_Methods.LogMessage("FinalDateFormat_FinalDate", 0, strFinalDate, "Info")
            int year = 0;
            year =  Convert.ToDateTime(strFinalDate).Year  ;
            if (year < 1900)
            {
                throw new ApplicationException("Date Should be greater than or equal to  01/01/1900");
            }


            //strFinalDate = Format(CDate(DateVariable), "yyyy-MM-dd")
            //        Global_Methods.LogMessage("FinalDateFormat_End", 0, strFinalDate, "Info")

            if (strFinalDate == string.Empty)
            {
                return null;
            }
            else
            {
                return strFinalDate;
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Invalid Date");
            return null;
        }

    }
}


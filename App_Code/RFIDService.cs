using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using ShoppingCart.BL;

/// <summary>
/// Summary description for RFIDService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RFIDService : System.Web.Services.WebService 
{

    public RFIDService () 
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    


    #region  Upload center RFID Data in database
    [WebMethod]
    public int UploadRFIDData(string Device_Code, string RFID_Card_ID, string Log_Date, string Log_Time)
        {
            int result = 0;
            int flag = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_SE_InsertRFID_CenterUpload", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //INPUT parameters
                    cmd.Parameters.AddWithValue("@Device_Code", Device_Code);
                    cmd.Parameters.AddWithValue("@RFID_Card_ID", RFID_Card_ID);
                    cmd.Parameters.AddWithValue("@Log_Date", Log_Date);
                    cmd.Parameters.AddWithValue("@Log_Time", Log_Time);
                    
                    con.Open();
                    flag = Convert.ToInt32(cmd.ExecuteScalar());
                    if (flag == 1)
                    {
                        result = 1;

                    }
                    else if (flag == -1)
                    {
                        result = 0;

                    }
                    else if (flag == 0)
                    {
                        result = 0;

                    }
                }
                catch (Exception ex)
                {
                    result = 0;
                }

            }

            //return usr.UserId; // if row present in dt then pass true else false

            return result;
        }
    
    #endregion

}

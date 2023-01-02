using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using ShoppingCart.DAL;
using System.Data.SqlClient;
using ShoppingCart.BL;
using System.Configuration;



namespace ShoppingCart.BL
{
    /// <summary>
    /// Summary description for ScheduleHorizonTypeController
    /// </summary>
    public class ScheduleHorizonTypeController
    {
        public ScheduleHorizonTypeController()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static void InsertScheduleHorizonType(string Type_Code, string Type_Name, int Is_Active, string Created_By)
        {
            SqlParameter p1 = new SqlParameter("@Schedule_Horizon_Type_Code", Type_Code);
            SqlParameter p2 = new SqlParameter("@Schedule_Horizon_Type_Name", Type_Name);
            SqlParameter p3 = new SqlParameter("@IsActive", Is_Active);
            SqlParameter p4 = new SqlParameter("@Created_By", Created_By);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Insert_Schedule_Horizon_Type", p1, p2, p3, p4);
        }

        public static void UpdateScheduleHorizonType(string Type_Code, string Type_Name,  string Edited_By)
        {
            SqlParameter p1 = new SqlParameter("@Schedule_Horizon_Type_Code", Type_Code);
            SqlParameter p2 = new SqlParameter("@Schedule_Horizon_Type_Name", Type_Name);
            SqlParameter p3 = new SqlParameter("@Edited_By", Edited_By);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_Schedule_Horizon_Type", p1, p2, p3);
        }

        public static void DeleteScheduleHorizonType(string Type_Code)
        {
            SqlParameter p1 = new SqlParameter("@Schedule_Horizon_Type_Code", Type_Code);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Delete_Schedule_Horizon_Type", p1);
        }

        public static void UpdateScheduleHorizonTypeStatus(string Type_Code,  int Is_Active, string Edited_By)
        {
            SqlParameter p1 = new SqlParameter("@Schedule_Horizon_Type_Code", Type_Code);
            SqlParameter p2 = new SqlParameter("@IsActive", Is_Active);
            SqlParameter p3 = new SqlParameter("@Edited_By", Edited_By);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_Schedule_Horizon_Type_Status", p1, p2, p3);
        }



        public static DataSet GetScheduleHorizonType(int Flag, string Type_Code= "")
        {
            
            SqlParameter p1 = new SqlParameter("@Schedule_Horizon_Type_Code", Type_Code);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_Schedule_Horizon_Type", p1,p2));

        }

        

    }
}
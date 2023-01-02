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
    

    public class TeacherAttendance_LMS_Response
    {
        public string TeacherCode { get; set; }
        public string LectureDetailsCode { get; set; }
        public string CenterCode { get; set; }
        public string BatchCode { get; set; }
        public string Status { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        
    }

    public class ProductController
    {
        public static string get_TeacherAttendance_LMSApiLink()
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_LMS_API_Link"));
        }

        #region "OLD Vinit"


        public static DataSet GetAllActivecenter()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Getallcenter"));
        }


        public static int InsertCounsellingreuqest(string txtName, string txtmobileno, string Email, string Pdate, string ptime, int center, int Registrationtype)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@Name", txtName);
            p[1] = new SqlParameter("@mobileno", txtmobileno);
            p[2] = new SqlParameter("@Email", Email);
            p[3] = new SqlParameter("@pCenter", center);
            p[4] = new SqlParameter("@pdate", Pdate);
            p[5] = new SqlParameter("@ptime", ptime);
            p[6] = new SqlParameter("@reg_type_id", Registrationtype);
            p[7] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertCounsellinginfo", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static int InsertQuery(string name, string Mobile, string email, string query)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@Name", name);
            p[1] = new SqlParameter("@mobileno", Mobile);
            p[2] = new SqlParameter("@Email", email);
            p[3] = new SqlParameter("@query", query);
            p[4] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertQuery", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static DataSet Getallactivetimeslot()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallActivetimeslot"));
        }


        public static DataSet Sendmail(string Center, int userid)
        {
            SqlParameter p1 = new SqlParameter("@Center", Center);
            SqlParameter p2 = new SqlParameter("@id", userid);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Sendmail", p1, p2));
        }

        public static DataSet Sendmailforquery(int Queryid)
        {
            SqlParameter p1 = new SqlParameter("@id", Queryid);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Sendmailforquery", p1));
        }

        public static DataSet GetallCourseName()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallActiveCourses"));
        }

        public static int InsertScholarshiprequest(string txtName, string txtmobileno, string Email, int center, int Registrationtype, int Cur_Course, string College_Name)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@Name", txtName);
            p[1] = new SqlParameter("@mobileno", txtmobileno);
            p[2] = new SqlParameter("@Email", Email);
            p[3] = new SqlParameter("@pCenter", center);
            p[4] = new SqlParameter("@Cur_Course", Cur_Course);
            p[5] = new SqlParameter("@College_Name", College_Name);
            p[6] = new SqlParameter("@reg_type_id", Registrationtype);
            p[7] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertScholarshipinfo", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static DataSet Scholarshipmail(string Center, int userid)
        {
            SqlParameter p1 = new SqlParameter("@Center", Center);
            SqlParameter p2 = new SqlParameter("@id", userid);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Scholarshipmail", p1, p2));
        }

        public static SqlDataReader GetLogin(string Username, string Password)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
            p[0].Value = Username;
            p[1] = new SqlParameter("@Password", SqlDbType.NVarChar);
            p[1].Value = Password;
            return (SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Login", p));
        }

        public static Object GetRoleByUsername(string Username)
        {
            SqlParameter p = new SqlParameter("@Username", SqlDbType.NVarChar);
            p.Value = Username;
            return (SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetRoleByUsername", p));
        }


        public static DataSet Getallproducttype()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_EC_Getallproducttype"));
        }

        public static DataSet Getallproducts()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_EC_Getallproducts"));
        }

        public static void AddContactUs(string fullName, string eId, string msg)
        {
            //Dim parr As SqlParameter() = New SqlParameter(20) {}
            //parr(0) = New SqlParameter("@fullName", SqlDbType.VarChar)
            //parr(0).Value = fullName
            //parr(1) = New SqlParameter("@eId", SqlDbType.VarChar)
            //parr(1).Value = eId
            //parr(1) = New SqlParameter("@msg", SqlDbType.VarChar)
            //parr(1).Value = msg
            //SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spaddnewuser", parr)

        }


        public static string getProductPrice(int ProductID)
        {
            string Price = "0";
            SqlDataReader drPrice = null;
            SqlParameter p = new SqlParameter("@ProductId", ProductID);
            drPrice = SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spGetProductbyproductId", p);
            if (drPrice != null)
            {
                if (drPrice.Read())
                {
                    Price = drPrice["Sale_Price"].ToString();
                }
            }
            return (Price);
        }

        public static DataSet GetQuantity(int ProductID, string SessionID, int Quantity, float Price)
        {
            SqlParameter p1 = new SqlParameter("@ProductID", ProductID);
            SqlParameter p2 = new SqlParameter("@p_sessionId", SessionID);
            SqlParameter p3 = new SqlParameter("@p_quantity", Quantity);
            SqlParameter p4 = new SqlParameter("@p_price", Price);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spQuantity", p1, p2, p3, p4));
        }

        public static DataSet GetShoppingCartDetails(string SessionID)
        {
            SqlParameter p = new SqlParameter("@SessionId", SessionID);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_CartProducts", p));
        }

        public static float GetCartTotal(string SID)
        {
            SqlParameter p = new SqlParameter("@SessionID", SID);
            return (float.Parse(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetCartTotal", p).ToString()));
        }

        public static int shoppingcartitems(string Sessionid)
        {
            //SqlParameter[] p = new SqlParameter[2];
            SqlParameter p = new SqlParameter("@SessionID", Sessionid);
            return (int.Parse(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetCartItems", p).ToString()));
        }


        public static void Deletecartlogout(string Sessionid)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@sessionId", SqlDbType.NVarChar);
            p[0].Value = Sessionid;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_DeleteCartlogout", p);
        }

        public static SqlDataReader getAccountDetails(string username)
        {
            SqlParameter p = new SqlParameter("@User", username);
            return (SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_ChangeDetails", p));
        }

        public static DataSet getAllStatedetails()
        {
            return SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetALLState");
        }

        public static DataSet getCountrydetails()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "usp_GetallCountry"));
        }

        public static DataSet getCitydetailsByStateID(int StateID)
        {
            SqlParameter p = new SqlParameter("@StateID", SqlDbType.Int);
            p.Value = StateID;
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllCitiesByStateID", p));
        }


        public static DataSet getCitydetails()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetALLCity"));
        }

        public static int[] spInsertAddressV2(string fname, string lname, string email, string gender, string contact, string nadrr, int ncountry, int nstate, int ncity)
        {
            int[] a = new int[2];
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@firstname", SqlDbType.NVarChar);
            p[0].Value = fname;
            p[1] = new SqlParameter("@lastname", SqlDbType.NVarChar);
            p[1].Value = lname;
            p[2] = new SqlParameter("@Email", SqlDbType.NVarChar);
            p[2].Value = email;
            p[3] = new SqlParameter("@Gender", SqlDbType.NVarChar);
            p[3].Value = gender;
            p[4] = new SqlParameter("@contactno", SqlDbType.NVarChar);
            p[4].Value = contact;
            p[5] = new SqlParameter("@Address", SqlDbType.NVarChar);
            p[5].Value = nadrr;
            p[6] = new SqlParameter("@CountryID", SqlDbType.Int);
            p[6].Value = ncountry;
            p[7] = new SqlParameter("@StateID", SqlDbType.Int);
            p[7].Value = nstate;
            p[8] = new SqlParameter("@CityID", SqlDbType.Int);
            p[8].Value = ncity;

            p[9] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[9].Direction = ParameterDirection.Output;
            p[10] = new SqlParameter("@Cid", SqlDbType.BigInt);
            p[10].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertAddressV2", p);
            if (p[9] != null)
            {
                a[0] = int.Parse(p[9].Value.ToString());
            }
            if (p[10] != null)
            {
                a[1] = int.Parse(p[10].Value.ToString());
            }
            return (a);
        }

        public static DataSet getAllStatesByCountryID(int CountryID)
        {
            SqlParameter p = new SqlParameter("@CountryID", SqlDbType.Int);
            p.Value = CountryID;
            return SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStatesByCountryID", p);
        }

        public static int GetIdFromtblUserAccount(string username)
        {
            SqlParameter p = new SqlParameter("@username", username);
            SqlParameter p1 = new SqlParameter("@customerid", SqlDbType.BigInt);
            p1.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetCustId", p, p1);
            return (int.Parse(p1.Value.ToString()));
        }

        public static int insertorder(int custid, float orderamnt, float taxamnt, float netamnt, int shipaddrId, int billaddrId, string sessionid, string billfname)
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@CustId", custid);
            p[1] = new SqlParameter("@OrderAmnt", orderamnt);
            p[2] = new SqlParameter("@TaxAmnt", taxamnt);
            p[3] = new SqlParameter("@NetAmnt", netamnt);
            p[4] = new SqlParameter("@ShipAddrId", shipaddrId);
            p[5] = new SqlParameter("@BillAddrId", billaddrId);
            p[6] = new SqlParameter("@SessionId", sessionid);
            p[7] = new SqlParameter("@OId", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            p[8] = new SqlParameter("@BillFname", billfname);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_insertorder", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        #endregion

        #region "Vivek New"
        public static DataSet GetAllActiveUser_Login(string User_ID, string Password, string DBName)
        {
            SqlParameter p1 = new SqlParameter("@usrid", User_ID);
            SqlParameter p2 = new SqlParameter("@pwd", Password);
            if (DBName == "DB03_Test_Engine")
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_USER_Login_MTEducare", p1, p2));
            }
            else
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_USER_Login", p1, p2));
            }
        }

        public static DataSet GetAllActiveTestMode()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestMode"));
        }

        public static DataSet GetAllActiveTestCategory()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestCategory"));
        }

        public static DataSet GetAllActiveTestType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestType"));
        }

        public static DataSet GetAllActiveTestAbsentReason()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestAbsentReason"));
        }

        public static DataSet GetAllActiveDiffLevel()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveDiffLevel"));
        }

        public static DataSet GetAllActiveUnitOfMeasurement()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveUnitOfMeasurement"));
        }



        public static DataSet GetAllActiveCountry()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllCountry"));
        }

        public static DataSet GetAllActiveState(string Country_Code)
        {
            SqlParameter p1 = new SqlParameter("@Countrycode", Country_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStatebyCountry", p1));
        }

        public static DataSet GetAllActiveCity(string State_Code)
        {
            SqlParameter p1 = new SqlParameter("@Statecode", State_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllCitybyState", p1));
        }

        public static DataSet GetAllActiveLocation(string City_Code)
        {
            SqlParameter p1 = new SqlParameter("@CityCode", City_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllLocationByCity", p1));
        }

        public static DataSet GetAllActiveClassroomType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveClassroomType"));
        }

        public static DataSet GetAllActivePremisesType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActivePremisesType"));
        }

        public static DataSet GetAllActivePartnerTitle()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActivePartnerTitle"));
        }

        public static DataSet GetAllActiveActivity(string ClassroomType_Id, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@ClassroomType_Id", ClassroomType_Id);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetAllActiveActivityType", p1, p2));
        }

        public static DataSet GetTest_Notification(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetTest_Notification", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet GetAllActiveUser_AcadYear()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallCurrentYear"));
        }

        public static DataSet GetAllActive_AllStandard(string Division_Code)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStandard_New", p1));
        }

        public static DataSet GetAllActive_Standard_Divisionwise(string Division_Code, int flag)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStandard_New", p1, p2));
        }

        public static DataSet GetAllActiveStreamsBy_Division_Year(string Division_Code, string Acad_Year, string AAG, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@AAG", AAG);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStreamsBy_Division_Year_AAG", p1, p2, p3, p4));
        }

        public static DataSet GetAllSubjectsBy_Division_Year_Standard(string Division_Code, string Acad_Year, string Standard_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllSubjectsBy_Division_Year_Standard", p1, p2, p3));
        }

        public static DataSet GetAllActiveSubjectsBy_Stream_AAG(string Stream_Code, string AAG, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@stream_code", Stream_Code);
            SqlParameter p2 = new SqlParameter("@AAG", AAG);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllSubjectsBy_Stream_AAG", p1, p2, p3));
        }

        public static int Insert_Batches(string DivisionCode, string YearName, string StandardCode, string ProductCode, string SubjectCode, string CentreCode, int MaxBatchStrength, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@ProductCode", ProductCode);
            p[4] = new SqlParameter("@SubjectCode", SubjectCode);
            p[5] = new SqlParameter("@CentreCode", CentreCode);
            p[6] = new SqlParameter("@MaxBatchStrength", MaxBatchStrength);
            p[7] = new SqlParameter("@CreatedBy", CreatedBy);
            p[8] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertBatch", p);
            return (int.Parse(p[8].Value.ToString()));
        }

        public static int Insert_Batches_LikeExistingBatch(string PKey, string CentreCode, int NewBatchCount, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@CentreCode", CentreCode);
            p[2] = new SqlParameter("@NewBatchCount", NewBatchCount);
            p[3] = new SqlParameter("@CreatedBy", CreatedBy);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertBatch_LikeExistingBatch", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Update_Batch(string PKey, string ProductCode, string SubjectCode, int MaxBatchStrength, string BatchShortName, int IsActiveFlag, string AlteredBy)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@ProductCode", ProductCode);
            p[2] = new SqlParameter("@SubjectCode", SubjectCode);
            p[3] = new SqlParameter("@MaxBatchStrength", MaxBatchStrength);
            p[4] = new SqlParameter("@BatchShortName", BatchShortName);
            p[5] = new SqlParameter("@IsActiveFlag", IsActiveFlag);
            p[6] = new SqlParameter("@AlteredBy", AlteredBy);
            p[7] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateBatch", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static DataSet GetAllActive_Standard_ForYear(string Division_Code, string YearName)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStandard", p1, p2, p3));
        }

        public static DataSet GetBatchBy_Division_Year_Standard_Centre(string Division_Code, string YearName, string StandardCode, string CentreCode, string BatchName)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@BatchName", BatchName);
            SqlParameter p6 = new SqlParameter("@Flag", 1);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatchBy_Division_Year_Standard_Centre", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetAllActive_Batch_ForStandard(string Division_Code, string YearName, string StandardCode, string CentreCode)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@Flag", 1);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForStandard", p1, p2, p3, p4, p5));
        }



        public static DataSet GetBatchBY_PKey(string PKey)
        {
            //Try
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            DataSet XYZ = SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatch_ByPKey", p1, p2, p3);
            return (XYZ);

            //Catch ex As Exception
            //    'Return nulldb

            //End Try

        }

        public static DataSet GetTestMasterBY_PKey(string PKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMaster_ByPKey", p1, p2));
        }

        public static DataSet GetTestQPSet_ByPKey(string TestPKey, string Set_Number, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@Set_Number", Set_Number);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestQPSet_ByPKey", p1, p2, p3));
        }

        public static DataSet GetTestMasterBy_Division_Year_Standard(string Division_Code, string YearName, string Standard_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMasterBy_Division_Year_Standard", p1, p2, p3, p4, p5, p6, p7,
            p8));
        }

        public static DataSet GetTestScheduleBy_Division_Year_Standard(string Division_Code, string YearName, string Standard_Code, string Batch_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate,
        int AttendClosureStatus_Flag, int MarksClosureStatus_Flag, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p6 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p7 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p8 = new SqlParameter("@TestName", TestName);
            SqlParameter p9 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p10 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p11 = new SqlParameter("@AttendClosureStatus_Flag", AttendClosureStatus_Flag);
            SqlParameter p12 = new SqlParameter("@MarksClosureStatus_Flag", MarksClosureStatus_Flag);
            SqlParameter p13 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestScheduleBy_Division_Year_Standard", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10, p11, p12, p13));
        }

        public static DataSet GetTestScheduleBy_Division_Year_Standard_Centre(string Division_Code, string YearName, string Standard_Code, string Batch_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate,
        int AttendClosureStatus_Flag, int MarksClosureStatus_Flag, string Centre_Code, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p6 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p7 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p8 = new SqlParameter("@TestName", TestName);
            SqlParameter p9 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p10 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p11 = new SqlParameter("@AttendClosureStatus_Flag", AttendClosureStatus_Flag);
            SqlParameter p12 = new SqlParameter("@MarksClosureStatus_Flag", MarksClosureStatus_Flag);
            SqlParameter p13 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p14 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestScheduleBy_Division_Year_Standard_Centre", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10, p11, p12, p13, p14));
        }

        public static DataSet GetTestForCancellationBy_Division_Year_Standard(string Division_Code, string YearName, string Standard_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p9 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p10 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestForCancellationBy_Division_Year_Standard", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10));
        }

        public static DataSet GetAnswerSheet_IssueBy_Division_Year_Standard_Centre(string Division_Code, string YearName, string Standard_Code, string Batch_Code, string TestMode_Id, string TestName, string FromDate, string ToDate, string Centre_Code, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p6 = new SqlParameter("@TestName", TestName);
            SqlParameter p7 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p8 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p9 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p10 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAnswerSheet_IssueBy_Division_Year_Standard_Centre", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10));
        }

        public static DataSet GetTestFor_Batch_Centre(string Division_Code, string YearName, string Centre_Code, string Standard_Code, string Batch_Code, int ReTest_Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p5 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p6 = new SqlParameter("@ReTest_Flag", ReTest_Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestFor_Batch_Centre", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetTestPresentStudent_ByPKey(string PKey, int Conduct_No, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", PKey);
            SqlParameter p2 = new SqlParameter("@Conduct_Number", Conduct_No);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestPresentStudent_ByPKey", p1, p2, p3));
        }

        public static DataSet GetStudent_ForTest_ByDivision_Centre_Standard(string Division_Code, string YearName, string Centre_Code, string Standard_Code, string Batch_Code, string DBSource, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p5 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p6 = new SqlParameter("@DBSource", DBSource);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByDivision_Centre_Standard", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet GetTest_AnswerUploadHistory(string Division_Code, string YearName, string Standard_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p9 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p10 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTest_AnswerUploadHistory", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10));
        }

        public static DataSet GetPartnerMaster_ByPKey(string PKey, string User_ID, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@User_ID", User_ID);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPartnerMaster_ByPKey", p1, p2, p3));
        }

        public static DataSet GetAnswerSheetIssueDetails_ByPKey(string PKey, string User_ID, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@User_ID", User_ID);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAnswerSheetIssueDetails_ByPKey", p1, p2, p3));
        }

        public static int Insert_Batch_Students(string PKey, string SBEntryCode, int ActionFlag, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[2] = new SqlParameter("@CreatedBy", CreatedBy);
            p[3] = new SqlParameter("@ActionFlag", ActionFlag);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertBatch_Student", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Update_Batch_ShortName(string PKey, string BatchShortName, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@BatchShortName", BatchShortName);
            p[2] = new SqlParameter("@CreatedBy", CreatedBy);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_Batch_ShortName", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static int Update_Batch_Student_RollNo(string PKey, string SBEntryCode, string NewRollNo, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[2] = new SqlParameter("@CreatedBy", CreatedBy);
            p[3] = new SqlParameter("@NewRollNo", NewRollNo);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_Batch_Student_RollNo", p);
            return (int.Parse(p[4].Value.ToString()));
        }
        public static int Update_Test(string TestCode, string DivisionCode, string YearName, string StandardCode, string TestModeCode, string TestCategoryCode, string TestTypeCode, string SubjectCode, string CentreCode, string ChapterCode,
        double MaxMarks, int TestDuration, int QPSetCount, int QuestionCount, string TestName, string TestDescription, string Remarks, int NegativeMarkingFlag, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[20];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@TestModeCode", TestModeCode);
            p[4] = new SqlParameter("@TestCategoryCode", TestCategoryCode);
            p[5] = new SqlParameter("@TestTypeCode", TestTypeCode);
            p[6] = new SqlParameter("@TestName", TestName);
            p[7] = new SqlParameter("@TestDescription", TestDescription);
            p[8] = new SqlParameter("@Remarks", Remarks);
            p[9] = new SqlParameter("@SubjectCode", SubjectCode);
            p[10] = new SqlParameter("@CentreCode", CentreCode);
            p[11] = new SqlParameter("@ChapterCode", ChapterCode);
            p[12] = new SqlParameter("@QPSetCnt", QPSetCount);
            p[13] = new SqlParameter("@MaxMarks", MaxMarks);
            p[14] = new SqlParameter("@TestDuration", TestDuration);
            p[15] = new SqlParameter("@CreatedBy", CreatedBy);
            p[16] = new SqlParameter("@QuestionCount", QuestionCount);
            //
            p[17] = new SqlParameter("@NegativeMarkingFlag", NegativeMarkingFlag);
            p[18] = new SqlParameter("@TestCode", TestCode);
            p[19] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[19].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTest", p);
            return (int.Parse(p[19].Value.ToString()));
        }
        public static int Insert_Test(string DivisionCode, string YearName, string StandardCode, string TestModeCode, string TestCategoryCode, string TestTypeCode, string SubjectCode, string CentreCode, string ChapterCode, double MaxMarks,
        int TestDuration, int QPSetCount, int QuestionCount, string TestName, string TestDescription, string Remarks, int NegativeMarkingFlag, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[19];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@TestModeCode", TestModeCode);
            p[4] = new SqlParameter("@TestCategoryCode", TestCategoryCode);
            p[5] = new SqlParameter("@TestTypeCode", TestTypeCode);
            p[6] = new SqlParameter("@TestName", TestName);
            p[7] = new SqlParameter("@TestDescription", TestDescription);
            p[8] = new SqlParameter("@Remarks", Remarks);
            p[9] = new SqlParameter("@SubjectCode", SubjectCode);
            p[10] = new SqlParameter("@CentreCode", CentreCode);
            p[11] = new SqlParameter("@ChapterCode", ChapterCode);
            p[12] = new SqlParameter("@QPSetCnt", QPSetCount);
            p[13] = new SqlParameter("@MaxMarks", MaxMarks);
            p[14] = new SqlParameter("@TestDuration", TestDuration);
            p[15] = new SqlParameter("@CreatedBy", CreatedBy);
            p[16] = new SqlParameter("@QuestionCount", QuestionCount);
            //
            p[17] = new SqlParameter("@NegativeMarkingFlag", NegativeMarkingFlag);
            p[18] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[18].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTest", p);
            return (int.Parse(p[18].Value.ToString()));
        }

        public static int Insert_Test_Set(string PKey, string Set_Number, int Question_No, string Subject_Code, string Chapter_Code, string Topic_Code, string Correct_Ans_Key, float Correct_Marks, float Wrong_Marks, string DiffLevel_Id,
        string Createdby, int ActionFlag)
        {

            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@Set_Number", Set_Number);
            p[2] = new SqlParameter("@Question_No", Question_No);
            p[3] = new SqlParameter("@Subject_Code", Subject_Code);
            p[4] = new SqlParameter("@Chapter_Code", Chapter_Code);
            p[5] = new SqlParameter("@Topic_Code", Topic_Code);
            p[6] = new SqlParameter("@Correct_Ans_Key", Correct_Ans_Key);
            p[7] = new SqlParameter("@Correct_Marks", Correct_Marks);
            p[8] = new SqlParameter("@Wrong_Marks", Wrong_Marks);
            p[9] = new SqlParameter("@DiffLevel_Id", DiffLevel_Id);
            p[10] = new SqlParameter("@Createdby", Createdby);
            p[11] = new SqlParameter("@ActionFlag", ActionFlag);
            p[12] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[12].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTest_Set", p);
            return (int.Parse(p[12].Value.ToString()));
        }

        public static int UpdateTest_Authorise_Block(string PKey, int Flag, string AlteredBy)
        {

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@ActionFlag", Flag);
            p[2] = new SqlParameter("@AlteredBy", AlteredBy);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTest_Authorise_Block", p);
            return (int.Parse(p[3].Value.ToString()));
        }
        public static int UpdateTest_Delete(string PKey, string AlteredBy)
        {

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@AlteredBy", AlteredBy);
            p[2] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTest_Delete", p);
            return (int.Parse(p[2].Value.ToString()));
        }

        public static int Insert_TestSchedule(string TestPKey, string CentreCode, string BatchCode, string Test_Date, int FromTime, int ToTime, string FromTimeStr, string ToTimeStr, float MaxMarks, string Remarks,
        string Createdby)
        {
            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@CentreCode", CentreCode);
            p[2] = new SqlParameter("@BatchCode", BatchCode);
            p[3] = new SqlParameter("@Test_Date", Test_Date);
            p[4] = new SqlParameter("@FromTime", FromTime);
            p[5] = new SqlParameter("@ToTime", ToTime);
            p[6] = new SqlParameter("@FromTimeStr", FromTimeStr);
            p[7] = new SqlParameter("@ToTimeStr", ToTimeStr);
            p[8] = new SqlParameter("@MaxMarks", MaxMarks);
            p[9] = new SqlParameter("@Remarks", Remarks);
            p[10] = new SqlParameter("@Createdby", Createdby);
            p[11] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[11].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTestSchedule", p);
            return (int.Parse(p[11].Value.ToString()));
        }

        public static int Update_TestSchedule(string TestPKey, string Test_Date, int FromTime, int ToTime, string FromTimeStr, string ToTimeStr, float MaxMarks, string Remarks, int IsActiveFlag, string AlteredBy,
        int Flag)
        {

            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Test_Date", Test_Date);
            p[2] = new SqlParameter("@FromTime", FromTime);
            p[3] = new SqlParameter("@ToTime", ToTime);
            p[4] = new SqlParameter("@FromTimeStr", FromTimeStr);
            p[5] = new SqlParameter("@ToTimeStr", ToTimeStr);
            p[6] = new SqlParameter("@MaxMarks", MaxMarks);
            p[7] = new SqlParameter("@Remarks", Remarks);
            p[8] = new SqlParameter("@IsActiveFlag", IsActiveFlag);
            p[9] = new SqlParameter("@AlteredBy", AlteredBy);
            p[10] = new SqlParameter("@Flag", Flag);
            p[11] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[11].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTestSchedule", p);
            return (int.Parse(p[11].Value.ToString()));
        }

        public static DataSet GetAllTestAttendanceActionType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestAttendanceActionType"));
        }

        public static DataSet GetAllTestAttendanceEntityType(string Action_Id, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Action_Id", Action_Id);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestAttendanceEntityType", p1, p2));
        }

        public static DataSet GetTest_AnswerUploadHistory_ByPKey(string PKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTest_AnswerUploadHistory_ByPKey", p1, p2, p3));
        }

        public static DataSet GetClassroomMaster_ByPKey(string PKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetClassroomMaster_ByPKey", p1, p2));
        }

        public static DataSet GetStudent_ForTest_ByTestPKey(string TestPKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByTestPKey", p1, p2, p3));
        }

        public static DataSet GetStudent_ForAnswerSheetIssue_ByTestPKey(string TestPKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForAnswerSheetIssue_ByTestPKey", p1, p2, p3));
        }

        public static int Insert_StudentTestAttendace(string TestPKey, string ActionFlag, string SBEntryCode, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[3] = new SqlParameter("@Createdby", Createdby);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestAttendace", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int InsertAnswerSheet_Issue(string TestPKey, string PartnerCode, string Issue_Date, int Issue_Quantity, string SBEntryCode, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@PartnerCode", PartnerCode);
            p[2] = new SqlParameter("@Issue_Date", Issue_Date);
            p[3] = new SqlParameter("@Issue_Quantity", Issue_Quantity);
            p[4] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[5] = new SqlParameter("@Createdby", Createdby);
            p[6] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[6].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertAnswerSheet_Issue", p);
            return (int.Parse(p[6].Value.ToString()));
        }

        public static int Insert_StudentTestMarks(string TestPKey, string SBEntryCode, string TestMarks, string MaxMarks, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[2] = new SqlParameter("@TestMarks", TestMarks);
            p[3] = new SqlParameter("@MaxMarks", MaxMarks);
            p[4] = new SqlParameter("@Createdby", Createdby);
            p[5] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestMarks", p);
            return (int.Parse(p[5].Value.ToString()));
        }

        public static int InsertStudentTestAttendace_Authorisation(string TestPKey, string ActionFlag, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@Createdby", Createdby);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestAttendace_Authorisation", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static int InsertStudentTestAbsentReason(string TestPKey, string AbsentReasonId, string SBEntryCode, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@AbsentReason_Id", AbsentReasonId);
            p[2] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[3] = new SqlParameter("@Createdby", Createdby);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestAbsentReason", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int InsertStudentTestMarks_Authorisation(string TestPKey, string ActionFlag, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@Createdby", Createdby);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestMarks_Authorisation", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static string InsertStudent_Answer_Import(string TestPKey, int Conduct_Number, string Import_FileName, string Student_ID_Column_Name, int Correct_Record_Cnt, int Warning_Record_Cnt, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Conduct_Number", Conduct_Number);
            p[2] = new SqlParameter("@Import_FileName", Import_FileName);
            p[3] = new SqlParameter("@Student_ID_Column_Name", Student_ID_Column_Name);
            p[4] = new SqlParameter("@Correct_Record_Cnt", Correct_Record_Cnt);
            p[5] = new SqlParameter("@Warning_Record_Cnt", Warning_Record_Cnt);
            p[6] = new SqlParameter("@Createdby", Createdby);
            p[7] = new SqlParameter("@Result", SqlDbType.VarChar, 20);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import", p);
            return (p[7].Value.ToString());
        }

        public static int InsertStudent_Answer_Import_StudentAnswerKey(string TestPKey, string Centre_Code, int Conduct_Number, string SBEntryCode, int MCQ_Set_Number, ref string MCQ_Import_Run_No, string MCQ_Answer_Key, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Centre_Code", Centre_Code);
            p[2] = new SqlParameter("@Conduct_Number", Conduct_Number);
            p[3] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[4] = new SqlParameter("@MCQ_Set_Number", MCQ_Set_Number);
            p[5] = new SqlParameter("@MCQ_Import_Run_No", MCQ_Import_Run_No);
            p[6] = new SqlParameter("@MCQ_Answer_Key", MCQ_Answer_Key);
            p[7] = new SqlParameter("@Createdby", Createdby);
            p[8] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import_StudentAnswerKey", p);
            return (int.Parse(p[8].Value.ToString()));
        }

        public static int InsertStudent_Answer_Import_Error_Item(string TestPKey, int Conduct_Number, string Import_Run_No, int Excel_Row_No, ref string Excel_Roll_No, string Excel_Set_No, string Excel_Error_Remarks)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Conduct_Number", Conduct_Number);
            p[2] = new SqlParameter("@Import_Run_No", Import_Run_No);
            p[3] = new SqlParameter("@Excel_Row_No", Excel_Row_No);
            p[4] = new SqlParameter("@Excel_Roll_No", Excel_Roll_No);
            p[5] = new SqlParameter("@Excel_Set_No", Excel_Set_No);
            p[6] = new SqlParameter("@Excel_Error_Remarks", Excel_Error_Remarks);
            p[7] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import_Error_Item", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static int InsertStudent_Answer_Import_Background_Process(string TestPKey, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Createdby", CreatedBy);
            p[2] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import_Background_Process", p);
            return (int.Parse(p[2].Value.ToString()));

        }

        public static int Insert_Standard_Subject(string PKey, string Subject_ShortName, string Subject_ShortCode, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@Subject_ShortName", Subject_ShortName);
            p[2] = new SqlParameter("@Subject_ShortCode", Subject_ShortCode);
            p[3] = new SqlParameter("@CreatedBy", CreatedBy);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStandardSubject", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Insert_Chapter(string DivisionCode, string YearName, string StandardCode, string SubjectCode, string ChapterName, double LectureCount, int LectureDuration, string ChapterShortName, string ChapterCodeForEdit, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@SubjectCode", SubjectCode);
            p[4] = new SqlParameter("@ChapterName", ChapterName);
            p[5] = new SqlParameter("@LectureCount", LectureCount);
            p[6] = new SqlParameter("@LectureDuration", LectureDuration);
            p[7] = new SqlParameter("@ChapterShortName", ChapterShortName);
            p[8] = new SqlParameter("@ChapterCodeForEdit", ChapterCodeForEdit);
            p[9] = new SqlParameter("@CreatedBy", CreatedBy);
            p[10] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[10].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertChapter", p);
            return (int.Parse(p[10].Value.ToString()));
        }

        public static string Insert_Classroom(string Classroom_LName, string Classroom_SName, double Length_inFeet, double Width_inFeet, double Height_inFeet, double Area_inSQFeet, string ClassroomType_Id, int IsActive, string Premises_Code, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@Classroom_LName", Classroom_LName);
            p[1] = new SqlParameter("@Classroom_SName", Classroom_SName);
            p[2] = new SqlParameter("@Length_inFeet", Length_inFeet);
            p[3] = new SqlParameter("@Width_inFeet", Width_inFeet);
            p[4] = new SqlParameter("@Height_inFeet", Height_inFeet);
            p[5] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[6] = new SqlParameter("@ClassroomType_Id", ClassroomType_Id);
            p[7] = new SqlParameter("@IsActive", IsActive);
            p[8] = new SqlParameter("@Premises_Code", Premises_Code);
            p[9] = new SqlParameter("@CreatedBy", CreatedBy);
            p[10] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[10].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertClassroom", p);
            return (p[10].Value.ToString());
        }

        public static int Insert_ClassroomCapacity(string Classroom_Code, string Activity_Id, int Capacity, string UOM)
        {

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@Classroom_Code", Classroom_Code);
            p[1] = new SqlParameter("@Activity_Id", Activity_Id);
            p[2] = new SqlParameter("@Capacity", Capacity);
            p[3] = new SqlParameter("@UOM", UOM);

            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertClassroom_Capacity", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Insert_ClassroomCentre(string Classroom_Code, string Primary_Centre_Code, string Centre_Code, string Created_By)
        {

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@Classroom_Code", Classroom_Code);
            p[1] = new SqlParameter("@Primary_Centre_Code", Primary_Centre_Code);
            p[2] = new SqlParameter("@Centre_Code", Centre_Code);
            p[3] = new SqlParameter("@Created_By", Created_By);

            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertClassroom_Centre", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int UpdateTestCancellation_Authorise(string PKey, int Flag, string Reason, string AlteredBy)
        {

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@TestPKey", PKey);
            p[1] = new SqlParameter("@ActionFlag", Flag);
            p[2] = new SqlParameter("@Reason", Reason);
            p[3] = new SqlParameter("@AlteredBy", AlteredBy);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTestCancellation_Authorise", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static DataSet GetClassroomMasterBy_Country_State_City(string Country_Code, string State_Code, string City_Code, string Location_Code, string ClassroomName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Country_Code", Country_Code);
            SqlParameter p2 = new SqlParameter("@State_Code", State_Code);
            SqlParameter p3 = new SqlParameter("@City_Code", City_Code);
            SqlParameter p4 = new SqlParameter("@Location_Code", Location_Code);
            SqlParameter p5 = new SqlParameter("@ClassroomName", ClassroomName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetClassroomMasterBy_Country_State_City", p1, p2, p3, p4, p5, p6));
        }

        public static string Update_Classroom(string Classroom_Code, string Premises_Code, string Classroom_LName, string Classroom_SName, double Length_inFeet, double Width_inFeet, double Height_inFeet, double Area_inSQFeet, string ClassroomType_Id, int IsActive,
        string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("@Classroom_Code", Classroom_Code);
            p[1] = new SqlParameter("@Premises_Code", Premises_Code);
            p[2] = new SqlParameter("@Classroom_LName", Classroom_LName);
            p[3] = new SqlParameter("@Classroom_SName", Classroom_SName);
            p[4] = new SqlParameter("@Length_inFeet", Length_inFeet);
            p[5] = new SqlParameter("@Width_inFeet", Width_inFeet);
            p[6] = new SqlParameter("@Height_inFeet", Height_inFeet);
            p[7] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[8] = new SqlParameter("@ClassroomType_Id", ClassroomType_Id);
            p[9] = new SqlParameter("@IsActive", IsActive);
            p[10] = new SqlParameter("@CreatedBy", CreatedBy);
            p[11] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[11].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateClassroom", p);
            return (p[11].Value.ToString());
        }

        public static string Insert_Premises(string CompanyCode, string Country_Code, string State_Code, string City_Code, string Location_Code, string Premises_LName, string Premises_SName, double Area_inSQFeet, string PremisesType_Id, int IsActive,
        string Premises_Address, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@CompanyCode", CompanyCode);
            p[1] = new SqlParameter("@Country_Code", Country_Code);
            p[2] = new SqlParameter("@State_Code", State_Code);
            p[3] = new SqlParameter("@City_Code", City_Code);
            p[4] = new SqlParameter("@Location_Code", Location_Code);
            p[5] = new SqlParameter("@Premises_LName", Premises_LName);
            p[6] = new SqlParameter("@Premises_SName", Premises_SName);
            p[7] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[8] = new SqlParameter("@PremisesType_Id", PremisesType_Id);
            p[9] = new SqlParameter("@IsActive", IsActive);
            p[10] = new SqlParameter("@CreatedBy", CreatedBy);
            p[11] = new SqlParameter("@Premises_Address", Premises_Address);
            p[12] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[12].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertPremises", p);
            return (p[12].Value.ToString());
        }

        public static string Update_Premises(string Premises_Code, string Location_Code, string Premises_LName, string Premises_SName, double Area_inSQFeet, string PremisesType_Id, int IsActive, string Premises_Address, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("@Premises_Code", Premises_Code);
            p[1] = new SqlParameter("@Location_Code", Location_Code);
            p[2] = new SqlParameter("@Premises_LName", Premises_LName);
            p[3] = new SqlParameter("@Premises_SName", Premises_SName);
            p[4] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[5] = new SqlParameter("@PremisesType_Id", PremisesType_Id);
            p[6] = new SqlParameter("@IsActive", IsActive);
            p[7] = new SqlParameter("@CreatedBy", CreatedBy);
            p[8] = new SqlParameter("@Premises_Address", Premises_Address);
            p[9] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[9].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdatePremises", p);
            return (p[9].Value.ToString());
        }

        public static DataSet GetPremisesMasterBy_Country_State_City(string Country_Code, string State_Code, string City_Code, string Location_Code, string PremisesName, string PremisesType_Id, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Country_Code", Country_Code);
            SqlParameter p2 = new SqlParameter("@State_Code", State_Code);
            SqlParameter p3 = new SqlParameter("@City_Code", City_Code);
            SqlParameter p4 = new SqlParameter("@Location_Code", Location_Code);
            SqlParameter p5 = new SqlParameter("@PremisesName", PremisesName);
            SqlParameter p6 = new SqlParameter("@PremisesType_Id", PremisesType_Id);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPremisesMasterBy_Country_State_City", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet GetPremisesMaster_ByPKey(string PKey, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPremisesMaster_ByPKey", p1, p2));
        }

        public static string Insert_Partner(string CompanyCode, string Title, string FirstName, string MiddleName, string LastName, string Gender, System.DateTime DOB, System.DateTime DOJ, string Qualification, string HandPhone1,
        string HandPhone2, string Landline, string EmailId, string FlatNo, string BuildingName, string StreetName, string Country_Code, string State_Code, string City_Code, string Location_Code,
        string Pincode, int IsActive, string CreatedBy, string ActivityCode, string DivisionCode, string EmployeeNo, string Area_Name, string Remarks, string PANNo, string Jobtype, string BankACNo, string BloodGroup, string ShortName, string IFECCode, string YearOfExp, string RefNo,
          string PTRegNo, string SubjectTaught, string Stream, string BankBranch, string ImagePath)
        {

            SqlParameter[] p = new SqlParameter[42];
            p[0] = new SqlParameter("@CompanyCode", CompanyCode);
            p[1] = new SqlParameter("@Title", Title);
            p[2] = new SqlParameter("@FirstName", FirstName);
            p[3] = new SqlParameter("@MiddleName", MiddleName);
            p[4] = new SqlParameter("@LastName", LastName);
            p[5] = new SqlParameter("@Gender", Gender);
            p[6] = new SqlParameter("@DOB", DOB);
            p[7] = new SqlParameter("@DOJ", DOJ);
            p[8] = new SqlParameter("@Qualification", Qualification);
            p[9] = new SqlParameter("@HandPhone1", HandPhone1);
            p[10] = new SqlParameter("@HandPhone2", HandPhone2);
            p[11] = new SqlParameter("@Landline", Landline);
            p[12] = new SqlParameter("@EmailId", EmailId);
            p[13] = new SqlParameter("@FlatNo", FlatNo);
            p[14] = new SqlParameter("@BuildingName", BuildingName);
            p[15] = new SqlParameter("@StreetName", StreetName);
            p[16] = new SqlParameter("@Country_Code", Country_Code);
            p[17] = new SqlParameter("@State_Code", State_Code);
            p[18] = new SqlParameter("@City_Code", City_Code);
            p[19] = new SqlParameter("@Location_Code", Location_Code);
            p[20] = new SqlParameter("@Pincode", Pincode);
            p[21] = new SqlParameter("@IsActive", IsActive);
            p[22] = new SqlParameter("@CreatedBy", CreatedBy);
            p[23] = new SqlParameter("@ActivityCode", ActivityCode);
            p[24] = new SqlParameter("@DivisionCode", DivisionCode);
            p[25] = new SqlParameter("@EmployeeNo", EmployeeNo);
            p[26] = new SqlParameter("@Area_Name", Area_Name);
            p[27] = new SqlParameter("@Remarks", Remarks);
            p[28] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[28].Direction = ParameterDirection.Output;
            p[29] = new SqlParameter("@PANNo", PANNo);
            p[30] = new SqlParameter("@BloodGroup", BloodGroup);
            p[31] = new SqlParameter("@RefNo", RefNo);
            p[32] = new SqlParameter("@PTRegNo", PTRegNo);
            p[33] = new SqlParameter("@BankACNo", BankACNo);
            p[34] = new SqlParameter("@BankIFSECode", IFECCode);
            p[35] = new SqlParameter("@BankBranch", BankBranch);
            p[36] = new SqlParameter("@SubjectTaught", SubjectTaught);
            p[37] = new SqlParameter("@Jobtype", Jobtype);
            p[38] = new SqlParameter("@TotalYearofexp", YearOfExp);
            p[39] = new SqlParameter("@ShortName", ShortName);
            p[40] = new SqlParameter("@Stream", Stream);
            p[41] = new SqlParameter("@ImagePath", ImagePath);

            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertPartner", p);
            return (p[28].Value.ToString());
        }

        public static string Update_Partner(string Partner_Code, string CompanyCode, string Title, string FirstName, string MiddleName, string LastName, string Gender, System.DateTime DOB, System.DateTime DOJ, string Qualification,
        string HandPhone1, string HandPhone2, string Landline, string EmailId, string FlatNo, string BuildingName, string StreetName, string Country_Code, string State_Code, string City_Code,
        string Location_Code, string Pincode, int IsActive, string CreatedBy, string ActivityCode, string DivisionCode, string EmployeeNo, string Area_Name, string Remarks, string PANNo, string Jobtype, string BankACNo, string BloodGroup, string ShortName, string IFECCode, string YearOfExp, string RefNo,
          string PTRegNo, string SubjectTaught, string Stream, string BankBranch, string ImagePath)
        {

            SqlParameter[] p = new SqlParameter[43];
            p[0] = new SqlParameter("@CompanyCode", CompanyCode);
            p[1] = new SqlParameter("@Title", Title);
            p[2] = new SqlParameter("@FirstName", FirstName);
            p[3] = new SqlParameter("@MiddleName", MiddleName);
            p[4] = new SqlParameter("@LastName", LastName);
            p[5] = new SqlParameter("@Gender", Gender);
            p[6] = new SqlParameter("@DOB", DOB);
            p[7] = new SqlParameter("@DOJ", DOJ);
            p[8] = new SqlParameter("@Qualification", Qualification);
            p[9] = new SqlParameter("@HandPhone1", HandPhone1);
            p[10] = new SqlParameter("@HandPhone2", HandPhone2);
            p[11] = new SqlParameter("@Landline", Landline);
            p[12] = new SqlParameter("@EmailId", EmailId);
            p[13] = new SqlParameter("@FlatNo", FlatNo);
            p[14] = new SqlParameter("@BuildingName", BuildingName);
            p[15] = new SqlParameter("@StreetName", StreetName);
            p[16] = new SqlParameter("@Country_Code", Country_Code);
            p[17] = new SqlParameter("@State_Code", State_Code);
            p[18] = new SqlParameter("@City_Code", City_Code);
            p[19] = new SqlParameter("@Location_Code", Location_Code);
            p[20] = new SqlParameter("@Pincode", Pincode);
            p[21] = new SqlParameter("@IsActive", IsActive);
            p[22] = new SqlParameter("@CreatedBy", CreatedBy);
            p[23] = new SqlParameter("@ActivityCode", ActivityCode);
            p[24] = new SqlParameter("@DivisionCode", DivisionCode);
            p[25] = new SqlParameter("@EmployeeNo", EmployeeNo);
            p[26] = new SqlParameter("@Area_Name", Area_Name);
            p[27] = new SqlParameter("@Remarks", Remarks);
            p[28] = new SqlParameter("@Partner_Code", Partner_Code);
            p[29] = new SqlParameter("@PANNo", PANNo);
            p[30] = new SqlParameter("@BloodGroup", BloodGroup);
            p[31] = new SqlParameter("@RefNo", RefNo);
            p[32] = new SqlParameter("@PTRegNo", PTRegNo);
            p[33] = new SqlParameter("@BankACNo", BankACNo);
            p[34] = new SqlParameter("@BankIFSECode", IFECCode);
            p[35] = new SqlParameter("@BankBranch", BankBranch);
            p[36] = new SqlParameter("@SubjectTaught", SubjectTaught);
            p[37] = new SqlParameter("@Jobtype", Jobtype);
            p[38] = new SqlParameter("@TotalYearofexp", YearOfExp);
            p[39] = new SqlParameter("@ShortName", ShortName);
            p[40] = new SqlParameter("@Stream", Stream);
            p[41] = new SqlParameter("@ImagePath", ImagePath);
            p[42] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[42].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdatePartner", p);
            return (p[42].Value.ToString());
        }

        public static DataSet GetPartnerMasterBy_Country_State_City(string Country_Code, string State_Code, string City_Code, string Location_Code, string PartnerName, string HandPhoneNo, int ActiveStatus, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Country_Code", Country_Code);
            SqlParameter p2 = new SqlParameter("@State_Code", State_Code);
            SqlParameter p3 = new SqlParameter("@City_Code", City_Code);
            SqlParameter p4 = new SqlParameter("@Location_Code", Location_Code);
            SqlParameter p5 = new SqlParameter("@PartnerName", PartnerName);
            SqlParameter p6 = new SqlParameter("@HandPhone", HandPhoneNo);
            SqlParameter p7 = new SqlParameter("@ActiveStatus", ActiveStatus);
            SqlParameter p8 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPartnerMasterBy_Country_State_City", p1, p2, p3, p4, p5, p6, p7,
            p8));
        }

        public static DataSet GetPartnerMasterBy_Division(string Division_Code, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPartnerMasterBy_Division", p1, p2));
        }

        public static int Insert_RemoveTestRequest(string PKey, string Reason, string Created_By)
        {

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@TestPKey", PKey);
            p[1] = new SqlParameter("@Reason", Reason);
            p[2] = new SqlParameter("@Createdby", Created_By);

            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Insert_RemoveTestRequest", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static string Raise_Error(string Error_Code)
        {
            string Error_Desc = null;

            switch (Error_Code)
            {
                case "0000":
                    Error_Desc = "Record saved successfully";
                    break;
                case "0001":
                    Error_Desc = "Select Division";
                    break;
                case "0002":
                    Error_Desc = "Select Academic Year";
                    break;
                case "0003":
                    Error_Desc = "Select Course";
                    break;
                case "0004":
                    Error_Desc = "Select Product";
                    break;
                case "0005":
                    Error_Desc = "Select Subject";
                    break;
                case "0006":
                    Error_Desc = "Select Centre";
                    break;
                case "0007":
                    Error_Desc = "Select Student(s)";
                    break;
                case "0008":
                    Error_Desc = "Number of Students selected is more than Maximum Batch Strength";
                    break;
                case "0009":
                    Error_Desc = "Enter New Batch Count";
                    break;
                case "0010":
                    Error_Desc = "Select Chapter(s)";
                    break;
                case "0011":
                    Error_Desc = "Select Test Mode";
                    break;
                case "0012":
                    Error_Desc = "Select Test Category";
                    break;
                case "0013":
                    Error_Desc = "Select Test Type";
                    break;
                case "0014":
                    Error_Desc = "Select Test Duration";
                    break;
                case "0015":
                    Error_Desc = "Select Batch";
                    break;
                case "0016":
                    Error_Desc = "Select Test Name";
                    break;
                case "0017":
                    Error_Desc = "Enter Maximum Marks";
                    break;
                case "0018":
                    Error_Desc = "Invalid Start Time";
                    break;
                case "0019":
                    Error_Desc = "Invalid End Time";
                    break;
                case "0020":
                    Error_Desc = "Start Time can't be after End Time";
                    break;
                case "0021":
                    Error_Desc = "Select Entity Type";
                    break;
                case "0022":
                    Error_Desc = "Select CSV File that you want to upload using Browse button";
                    break;
                case "0023":
                    Error_Desc = "File with the same name already processed for this test";
                    break;
                case "0024":
                    Error_Desc = "Invalid file format";
                    break;
                case "0025":
                    Error_Desc = "Enter Name of the column where Student ID is stored";
                    break;
                case "0026":
                    Error_Desc = "Invalid ID Column name";
                    break;
                case "0027":
                    Error_Desc = "No records found for importing";
                    break;
                case "0028":
                    Error_Desc = "Select Conduct Number";
                    break;
                case "0029":
                    Error_Desc = "Duplicate Test Name";
                    break;
                case "0030":
                    Error_Desc = "Select Student";
                    break;
                case "0031":
                    Error_Desc = "Attendance Authorisation can't be done as attendance of few Students is not marked";
                    break;
                case "0032":
                    Error_Desc = "Attendance Authorisation done successfully";
                    break;
                case "0033":
                    Error_Desc = "Attendance Authorisation removed successfully";
                    break;
                case "0034":
                    Error_Desc = "Marks Authorisation done successfully";
                    break;
                case "0035":
                    Error_Desc = "Marks Authorisation removed successfully";
                    break;
                case "0036":
                    Error_Desc = "Marks Authorisation can't be done as marks of few students are not entered";
                    break;
                case "0037":
                    Error_Desc = "File not found";
                    break;
                case "0038":
                    Error_Desc = "Test names matching with search options not found";
                    break;
                case "0039":
                    Error_Desc = "Student Answers reprocessed successfully";
                    break;
                case "0040":
                    Error_Desc = "Select Country";
                    break;
                case "0041":
                    Error_Desc = "Select State";
                    break;
                case "0042":
                    Error_Desc = "Select City";
                    break;
                case "0043":
                    Error_Desc = "Select Company";
                    break;
                case "0044":
                    Error_Desc = "Select Location";
                    break;
                case "0045":
                    Error_Desc = "Select Classroom Type";
                    break;
                case "0046":
                    Error_Desc = "Enter Classroom Length (in feet)";
                    break;
                case "0047":
                    Error_Desc = "Enter Classroom Width (in feet)";
                    break;
                case "0048":
                    Error_Desc = "Enter Classroom Height (in feet)";
                    break;
                case "0049":
                    Error_Desc = "Duplicate Classroom name";
                    break;
                case "0050":
                    Error_Desc = "Select Primary Owner Centre for the Classroom";
                    break;
                case "0051":
                    Error_Desc = "Select only 1 Centre as Primary Owner Centre for the Classroom";
                    break;
                case "0052":
                    Error_Desc = "Select Unit of Measurement for Classroom Capacity";
                    break;
                case "0053":
                    Error_Desc = "Select Title";
                    break;
                case "0054":
                    Error_Desc = "Enter First Name";
                    break;
                case "0055":
                    Error_Desc = "Enter Hand Phone number (1)";
                    break;
                case "0056":
                    Error_Desc = "Select Gender";
                    break;
                case "0057":
                    Error_Desc = "Select Activity";
                    break;
                case "0058":
                    Error_Desc = "Duplicate Partner details";
                    break;
                case "0059":
                    Error_Desc = "Invalid Hand Phone number (1)";
                    break;
                case "0060":
                    Error_Desc = "Invalid Hand Phone number (2)";
                    break;
                case "0061":
                    Error_Desc = "Enter Size of Premises in Sq. Feet";
                    break;
                case "0062":
                    Error_Desc = "Duplicate Premises name";
                    break;
                case "0063":
                    Error_Desc = "Select Premises Type";
                    break;
                case "0064":
                    Error_Desc = "Test Removal Request Approved successfully.";
                    break;
                case "0065":
                    Error_Desc = "Test Removal Request Rejected successfully";
                    break;
                case "0066":
                    Error_Desc = "Select Action";
                    break;
                case "0067":
                    Error_Desc = "Record deleted successfully";
                    break;
                case "0068":
                    Error_Desc = "Select Issuer Type";
                    break;
                case "0069":
                    Error_Desc = "Select Receiver Type";
                    break;
                case "0070":
                    Error_Desc = "Select Date Range";
                    break;
                default:
                    Error_Desc = Error_Code;
                    break;
            }
            return Error_Desc;
        }
        #endregion

        #region "Vivek Report"
        public static DataSet Report_Test_MCQ_Test_Subject_Student_Rank(string TestPKey, string SBEntryCode, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_MCQ_Test_Subject_Student_Rank", p1, p2, p3));
        }

        public static DataSet Report_Test_NonMCQ_Test_Subject_Student_Rank(string TestPKey, string SBEntryCode, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_NonMCQ_Test_Subject_Student_Rank", p1, p2, p3));
        }

        public static DataSet Report_Test_MCQ_Attendance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            SqlParameter p4 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@ToDate", ToDate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Absenteeism_New", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet Report_Test_MCQ_Ranking(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet Report_Test_MCQ_Ranking_Subject(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string Subject_Code)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_Subject", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet Report_Test_PendingAuthorisation(string TestPKey, string UserCode, int Flag, string Centre_Code)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            SqlParameter p4 = new SqlParameter("@Centre_Code", Centre_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_PendingAuthorisation", p1, p2, p3, p4));
        }
        #endregion

        #region "Vivek Dashboard"
        public static DataSet Dashboard_TestSchedule(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_TestSchedule", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet Dashboard_PendingAttendAuthorisation(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_PendingAttendAuthorisation", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet Dashboard_PendingMarksAuthorisation(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_PendingMarksAuthorisation", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet Dashboard_Test(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_Test", p1, p2, p3, p4, p5, p6));

        }

        #endregion

        #region vikram added new code
        public static DataSet GetSubject(string Standardcode)
        {
            SqlParameter p1 = new SqlParameter("@Standard_Code", Standardcode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSSubjectByStandard", p1));
        }
        public static int Insert_ChapterSequence(string DivisionCode, string YearName, string SubjectCode, string center, string orderno, string chapterno, string createdby)
        {

            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@Division", DivisionCode);
            p[1] = new SqlParameter("@acadyear", YearName);
            p[2] = new SqlParameter("@subject", SubjectCode);
            p[3] = new SqlParameter("@center", center);
            p[4] = new SqlParameter("@orderno", orderno);
            p[5] = new SqlParameter("@chapterno", chapterno);
            p[6] = new SqlParameter("@alertedby", createdby);
            p[7] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_Insert_Chapter_Sequence", p);
            return (int.Parse(p[7].Value.ToString()));
        }
        public static DataSet GetAllChapter(string Division_Code, string YearName, string SubjectCode, string center)
        {
            SqlParameter p1 = new SqlParameter("@divisoncode", Division_Code);
            SqlParameter p2 = new SqlParameter("@acadyear", YearName);
            SqlParameter p3 = new SqlParameter("@subjectcode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@center", center);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_GetChapterSequence", p1, p2, p3, p4));
        }

        public static int DelteChapterSequence(string ChapterCode)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Chaptercode", ChapterCode);
            p[1] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_Delete_Chapter_Sequence", p);
            return (int.Parse(p[1].Value.ToString()));
        }
        public static int DeleteLectureDuration(string slotid)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@slotid", slotid);
            p[1] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Delete_LectureDuration", p);
            return (int.Parse(p[1].Value.ToString()));
        }
        public static DataSet GetListOfDaysinMonth(string month)
        {
            SqlParameter p1 = new SqlParameter("@month", month);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_List_DaysinMonth", p1));
        }

        public static int Insert_LectureDuration(string Division_Code, string YearName, string fromtime, string frommin, string fromampm, string totime, string tomin, string toampm, string Is_Active, string Created_By, string PKey, string flag)
        {
            SqlParameter p1 = new SqlParameter("@Divisioncode", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acadyear", YearName);
            SqlParameter p3 = new SqlParameter("@Fromtime", fromtime);
            SqlParameter p4 = new SqlParameter("@Frommin", frommin);
            SqlParameter p5 = new SqlParameter("@fromampm", fromampm);
            SqlParameter p6 = new SqlParameter("@Totime", totime);
            SqlParameter p7 = new SqlParameter("@Tomin", tomin);
            SqlParameter p8 = new SqlParameter("@Toampm", toampm);
            SqlParameter p9 = new SqlParameter("@Is_Active", Is_Active);
            SqlParameter p10 = new SqlParameter("@slotid", PKey);
            SqlParameter p11 = new SqlParameter("@CreatedBy", Created_By);
            SqlParameter p12 = new SqlParameter("@Flag", flag);


            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Lecture_Duration", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12));

        }


        public static DataSet GetLectureDuration(string divisioncode, string acadyear, string slotid, string flag)
        {
            SqlParameter p1 = new SqlParameter("@Divisioncode", divisioncode);
            SqlParameter p2 = new SqlParameter("@Acadyear", acadyear);
            SqlParameter p3 = new SqlParameter("@slotid", slotid);
            SqlParameter p4 = new SqlParameter("@flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Get_LectureDuration", p1, p2, p3, p4));
        }

        #endregion

        #region Tripty added new code
        public static DataSet GetAllActiveFacultyTitle()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActivePartnerTitle"));
        }

        public static string RemoveComma(string str)
        {

            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);

            if (str.Length > 0)
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

        public static DataSet GetCourse_Subjects(string Subject_Name, string Course_Code)
        {
            SqlParameter p1 = new SqlParameter("@Subject_Name", Subject_Name);
            SqlParameter p2 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p3 = new SqlParameter("@ActionType", 4);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_CRUD_LMS_Course_Subjects", p1, p2, p3));
        }

        public static DataSet GetSubjectByPkey(string Pkey)
        {
            SqlParameter p1 = new SqlParameter("@Record_Number", Pkey);
            SqlParameter p2 = new SqlParameter("@ActionType", 3);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_CRUD_LMS_Course_Subjects", p1, p2));
        }

        public static DataSet UpdateSubject(string Pkey, string Subject_Name, string Subject_Display_Name, string Modified_By, int Is_Active)
        {
            SqlParameter p1 = new SqlParameter("@Record_Number", Pkey);
            SqlParameter p2 = new SqlParameter("@ActionType", 2);
            SqlParameter p3 = new SqlParameter("@Subject_Name", Subject_Name);
            SqlParameter p4 = new SqlParameter("@Subject_Display_Name", Subject_Display_Name);
            SqlParameter p5 = new SqlParameter("@Modified_By", Modified_By);
            SqlParameter p6 = new SqlParameter("@Is_Active", Is_Active);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_CRUD_LMS_Course_Subjects", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetAllSubjectsByStandard(string Standard_Code)
        {

            SqlParameter p1 = new SqlParameter("@Standard_Code", Standard_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSSubjectByStandard", p1));
        }

        public static DataSet GetLMSProductByCourse_AcadYear(string CourseCode, string AcademicYear)
        {
            SqlParameter p1 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p2 = new SqlParameter("@AcademicYear", AcademicYear);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSProductByCourse_AcadYear", p1, p2));
        }



        public static DataSet GetAllChaptersBy_Division_Year_Standard_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@SubjectCode", SubjectCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllChaptersBy_Division_Year_Standard_Subject_New", p1, p2, p3, p4));
        }

        public static DataSet GetFacultyByDivisionCode(string DivisionCode)
        {

            SqlParameter p1 = new SqlParameter("@DivisionCode", DivisionCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetFaculty_By_DivisionCode", p1));
        }

        public static DataSet GetFaculty_Subject(string PKeyId, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKeyId", PKeyId);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Get_Faculty_Subject", p1, p2));
        }

        public static int DeleteFaculty_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode, string FacultyCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p5 = new SqlParameter("@Partner_Code", FacultyCode);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Delete_Faculty_Subject", p1, p2, p3, p4, p5));


            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Delete_Faculty_Subject", p1, p2,p3,p4,p5));
        }

        public static int UpdateFaculty_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode, string FacultyCode, string Altered_By, string ColorCode, string PaymentRate, string ShortName)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p5 = new SqlParameter("@Partner_Code", FacultyCode);
            SqlParameter p6 = new SqlParameter("@Altered_By", Altered_By);
            SqlParameter p7 = new SqlParameter("@ColorCode", ColorCode);
            SqlParameter p8 = new SqlParameter("@PaymentRate", PaymentRate);
            SqlParameter p9 = new SqlParameter("@ShortName", ShortName);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Update_Faculty_Subject", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        public static int InsertFaculty_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode, string FacultyCode, string Created_By, string ColorCode, string PaymentRate, string ShortName)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p5 = new SqlParameter("@Partner_Code", FacultyCode);
            SqlParameter p6 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p7 = new SqlParameter("@ColorCode", ColorCode);
            SqlParameter p8 = new SqlParameter("@PaymentRate", PaymentRate);
            SqlParameter p9 = new SqlParameter("@ShortName", ShortName);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Faculty_Subject", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        public static DataSet GetSchedule_Horizon_Type()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetSchedule_Horizon_Type"));
        }

        public static DataSet GetActivityType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetActivityType"));
        }

        public static DataSet Get_Schedule_Horizon(string PKeyId, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKeyId);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Get_Schedule_Horizon", p1, p2));
        }

        public static int DeleteSchedule_Horizon(string PKey)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Delete_Schedule_Horizon", p1));

        }


        public static int InsertSchedule_Horizon(string Division_Code, string YearName, string StandardCode, string Schedule_Horizon_Type_Code, string Activity_Id, string Created_By, string Start_Date, string End_Date, string Schedule_Horizon_Name, int WeekDay_Count, int WeekDay_Session_Count_PerDay, int Holiday_Count, int Holiday_Session_Count_PerDay, int Total_Session_Count, int Session_Duration, string LMSProductCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Stream_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Schedule_Horizon_Type_Code", Schedule_Horizon_Type_Code);
            SqlParameter p5 = new SqlParameter("@Activity_Id", Activity_Id);
            SqlParameter p6 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p7 = new SqlParameter("@Start_Date", Start_Date);
            SqlParameter p8 = new SqlParameter("@End_Date", End_Date);
            SqlParameter p9 = new SqlParameter("@Schedule_Horizon_Name", Schedule_Horizon_Name);
            SqlParameter p10 = new SqlParameter("@WeekDay_Count", WeekDay_Count);
            SqlParameter p11 = new SqlParameter("@WeekDay_Session_Count_PerDay", WeekDay_Session_Count_PerDay);
            SqlParameter p12 = new SqlParameter("@Holiday_Count", Holiday_Count);
            SqlParameter p13 = new SqlParameter("@Holiday_Session_Count_PerDay", Holiday_Session_Count_PerDay);
            SqlParameter p14 = new SqlParameter("@Total_Session_Count", Total_Session_Count);
            SqlParameter p15 = new SqlParameter("@Session_Duration", Session_Duration);
            SqlParameter p16 = new SqlParameter("@LMSProductCode", LMSProductCode);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Schedule_Horizon", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16));
        }

        public static int UpdateSchedule_Horizon(string Division_Code, string YearName, string StandardCode, string Schedule_Horizon_Type_Code, string Activity_Id, string Created_By, string Start_Date, string End_Date, string Schedule_Horizon_Name, int WeekDay_Count, int WeekDay_Session_Count_PerDay, int Holiday_Count, int Holiday_Session_Count_PerDay, int Total_Session_Count, int Session_Duration, string LMSProductCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Stream_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Schedule_Horizon_Type_Code", Schedule_Horizon_Type_Code);
            SqlParameter p5 = new SqlParameter("@Activity_Id", Activity_Id);
            SqlParameter p6 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p7 = new SqlParameter("@Start_Date", Start_Date);
            SqlParameter p8 = new SqlParameter("@End_Date", End_Date);
            SqlParameter p9 = new SqlParameter("@Schedule_Horizon_Name", Schedule_Horizon_Name);
            SqlParameter p10 = new SqlParameter("@WeekDay_Count", WeekDay_Count);
            SqlParameter p11 = new SqlParameter("@WeekDay_Session_Count_PerDay", WeekDay_Session_Count_PerDay);
            SqlParameter p12 = new SqlParameter("@Holiday_Count", Holiday_Count);
            SqlParameter p13 = new SqlParameter("@Holiday_Session_Count_PerDay", Holiday_Session_Count_PerDay);
            SqlParameter p14 = new SqlParameter("@Total_Session_Count", Total_Session_Count);
            SqlParameter p15 = new SqlParameter("@Session_Duration", Session_Duration);
            SqlParameter p16 = new SqlParameter("@LMSProductCode", LMSProductCode);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Update_Schedule_Horizon", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16));
        }

        public static DataSet GetAllActiveUser_Company_Division_Zone_Center(string User_ID, string Company_Code, string Division_Code, string Zone_Code, string Flag, string DBName)
        {
            SqlParameter p1 = new SqlParameter("@user_id", User_ID);
            SqlParameter p2 = new SqlParameter("@company_code", Company_Code);
            SqlParameter p3 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p4 = new SqlParameter("@Zone_Code", Zone_Code);
            SqlParameter p5 = new SqlParameter("@Flag", Flag);

            if (DBName == "MTEducare")
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetUser_Company_Division_Zone_Center_MTEducare", p1, p2, p3, p4, p5));
            }
            else
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetUser_Company_Division_Zone_Center", p1, p2, p3, p4, p5));
            }

        }


        public static DataSet GetYearDistributionsheetBy_Division_Year_Standard_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode, string Center_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p5 = new SqlParameter("@Center_Code", Center_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetYearDistributionsheetBy_Division_Year_Standard_Subject", p1, p2, p3, p4, p5));
        }
        #endregion

        #region Digambar Added new code


        public static DataSet Get_Lecture_Schedule(string Division_Code, string Acad_Year, string ProductCode, string Centre_Code, string From_Date, string To_Date, string LectStatusFlag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p5 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p6 = new SqlParameter("@To_Date", To_Date);
            SqlParameter p7 = new SqlParameter("@LectStatusFlag", LectStatusFlag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureSchedule", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet Get_LectureScheduleByPKey(string PKeyId)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKeyId);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureScheduleByPKey", p1));
        }

        public static DataSet Get_LMSProduct_ByDivision_Year(string DivisionCode, string AcademicYear)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter p2 = new SqlParameter("@Acad_Year", AcademicYear);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSProductNameByDivisionCode_YearName", p1, p2));
        }

        public static DataSet Get_Course_ByLMSProduct(string ProductCode)
        {
            SqlParameter p1 = new SqlParameter("@ProductCode", ProductCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetCourse_ByLMSProduct", p1));
        }

        public static DataSet Delete_LectureScheduleByPKey(string PKeyId, int Flag, string Deleted_By)
        {
            SqlParameter p1 = new SqlParameter("@LectureSchedule_Id", PKeyId);
            SqlParameter p2 = new SqlParameter("@flag", Flag);
            SqlParameter p3 = new SqlParameter("@Deleted_By", Deleted_By);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_delete_Lecture", p1, p2, p3));
        }

        public static DataSet GetTeacherByCenterSubjectChapter(string CenterCode, string SubjectCode, string ChapterCode)
        {

            SqlParameter p1 = new SqlParameter("@Centre_Code", CenterCode);
            SqlParameter p2 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p3 = new SqlParameter("@Chapter_Code", ChapterCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTeacher_By_Center_Subject_Chapter", p1, p2, p3));
        }

        public static DataSet GetLessonPlanBySubjectChapter(string SubjectCode, string ChapterCode)
        {
            SqlParameter p1 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p2 = new SqlParameter("@Chapter_Code", ChapterCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLessonPlan_By_Subject_Chapter", p1, p2));
        }


        public static int InsertUpdateLectureShedule(string LectureSchedule_Id, string Division_Code, string Acad_Year, string Stream_Code, string ProductCode, string Centre_Code, string Batch_Code, string Session_Date, string Activity_Id, string Subject_Code, string Chapter_Code, string FromTIME, string ToTIME, string LessonPlanCode, string Partner_Code, string Created_By, string flag, string CancellationReasonCode, string replaceSubject_Code, string replaceChapter_Code, string replaceFaculty_Code, string Remarks)
        {
            SqlParameter p1 = new SqlParameter("@LectureSchedule_Id", LectureSchedule_Id);
            SqlParameter p2 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p3 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p4 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p7 = new SqlParameter("@Session_Date", Session_Date);
            SqlParameter p8 = new SqlParameter("@Activity_Id", Activity_Id);
            SqlParameter p9 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p10 = new SqlParameter("@Chapter_Code", Chapter_Code);
            SqlParameter p11 = new SqlParameter("@FromTIME", FromTIME);
            SqlParameter p12 = new SqlParameter("@ToTIME", ToTIME);
            SqlParameter p13 = new SqlParameter("@LessonPlanCode", LessonPlanCode);
            SqlParameter p14 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p15 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p16 = new SqlParameter("@flag", flag);
            SqlParameter p17 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p18 = new SqlParameter("@CancellationReasonCode", CancellationReasonCode);
            SqlParameter p19 = new SqlParameter("@replaceSubject_Code", replaceSubject_Code);
            SqlParameter p20 = new SqlParameter("@replaceChapter_Code", replaceChapter_Code);
            SqlParameter p21 = new SqlParameter("@replaceFaculty_Code", replaceFaculty_Code);
            SqlParameter p22 = new SqlParameter("@Remarks", Remarks);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Insert_Update_LectureShedule", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22));
        }

        #endregion

        #region Vivek Added new code
        public static DataSet GetYearDistributionsheetBy_Division_Year_Standard_Subject_Center(string Division_Code, string YearName, string SubjectCode, string Center_Code, string Stream_Code, string Scheduling_Horizon_TypeCode)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p6 = new SqlParameter("@Scheduling_Horizon_TypeCode", Scheduling_Horizon_TypeCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetYearDistributionsheetBy_Division_Year_Standard_Subject_Center", p1, p2, p3, p4, p5, p6));
        }
        public static int Insert_YearDistribution(string Division_Code, string Acad_Year, string Standard_Code, string LMSProductCode, string SchedulHorizonTypeCode, string CenterCode, string SubjectCode, string ChapterCode, string TeacherShortName, string Created_By, int Session_Count)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@SchedulHorizonTypeCode", SchedulHorizonTypeCode);
            SqlParameter p6 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p7 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p8 = new SqlParameter("@ChapterCode", ChapterCode);
            SqlParameter p9 = new SqlParameter("@TeacherShortName", TeacherShortName);
            SqlParameter p10 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p11 = new SqlParameter("@Session_Count", Session_Count);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_SE_Insert_YearDistribution", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
        }

        #endregion



        public static DataSet GetStudent_ForLectureAttendence(string LecturePKey)
        {
            SqlParameter p1 = new SqlParameter("@LecturePKey", LecturePKey);
            //SqlParameter p2 = new SqlParameter("@BatchCode", BatchCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetStudent_ForAttendence", p1));
        }
        public static int InsertStudentLectureAttendace_Authorisation(string PKey, string ActionFlag, string Createdby, string AttendClosureRemarks)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", @PKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@Createdby", Createdby);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4] = new SqlParameter("@AttendClosureRemarks", AttendClosureRemarks);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_InsertStudentLectureAttendace_Authorisation", p);
            return (int.Parse(p[3].Value.ToString()));
        }





        public static DataSet Get_GetLectureSchedule_PKey(string PKeyId)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKeyId);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetLectureSchedule_PKey", p1));
        }



        public static int Insert_UpdateStudentAttendace(string PKey, string ActionFlag, string SBEntryCode, string Batch_Code, string AbsentReason, string Remarks)
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@Batch_Code", Batch_Code);
            p[2] = new SqlParameter("@ActionFlag", ActionFlag);
            p[3] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[4] = new SqlParameter("@AbsentReason", AbsentReason);
            p[5] = new SqlParameter("@Remarks", Remarks);
            p[6] = new SqlParameter("@Result", SqlDbType.Int);
            p[6].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertUpdateStudentLectureAttendace", p);
            return (int.Parse(p[6].Value.ToString()));
        }


        public static DataSet GetLectureSchedule(string division, string centername, string batch, string fdate, string tdate)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@batchname", batch);
            SqlParameter p4 = new SqlParameter("@fdate", fdate);
            SqlParameter p5 = new SqlParameter("@tdate", tdate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_Get_Lecture_Schedule_Details", p1, p2, p3, p4, p5));
        }
        public static DataSet GetLectureDerivationReport(string division, string centername, string fdate, string tdate, string UID)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@UID", UID);
            SqlParameter p4 = new SqlParameter("@fdate", fdate);
            SqlParameter p5 = new SqlParameter("@tdate", tdate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_Get_Lecture_Deviation_Report", p1, p2, p3, p4, p5));
        }
        public static DataSet Get_LectureScheduleByDivisionCourse(string Division_Code, string CourseCode, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureScheduleByDivisionCourse", p1, p2, p3));
        }

        public static DataSet Get_LMSProduct_ByDivision_Year_Course(string DivisionCode, string AcademicYear, string CourseCode)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter p2 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p3 = new SqlParameter("@Acad_Year", AcademicYear);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSProductNameByDivisionCode_YearName_Course", p1, p2, p3));
        }

        public static DataSet Get_Lecture_Schedule(string Division_Code, string Acad_Year, string ProductCode, string Centre_Code, string From_Date, string To_Date, string LectStatusFlag, string Course_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p7 = new SqlParameter("@To_Date", To_Date);
            SqlParameter p8 = new SqlParameter("@LectStatusFlag", LectStatusFlag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureSchedule", p1, p2, p3, p4, p5, p6, p7, p8));
        }


        public static DataSet Get_Lecture_Schedule_Decentralized(string Division_Code, string Acad_Year, string ProductCode, string Centre_Code, string From_Date, string To_Date, string LectStatusFlag, string Course_Code, string LectType, string Batch_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p7 = new SqlParameter("@To_Date", To_Date);
            SqlParameter p8 = new SqlParameter("@LectStatusFlag", LectStatusFlag);
            SqlParameter p9 = new SqlParameter("@LectType", LectType);
            SqlParameter p10 = new SqlParameter("@Batch_Code", Batch_Code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureSchedule", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }



        public static DataSet GetStudentAbsentCenterWise(string division, string centre)
        {
            SqlParameter p1 = new SqlParameter("@Divsision", division);
            SqlParameter p2 = new SqlParameter("@centre", centre);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetStudentAbsent_Centerwise", p1, p2));
        }

        public static DataSet Get_Lecture_Approval(string Division_Code, string Acad_Year, string ProductCode, string Centre_Code, string From_Date, string To_Date, string LectApprovalFlag, string Course_Code, string User_Id)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p7 = new SqlParameter("@To_Date", To_Date);
            SqlParameter p8 = new SqlParameter("@LectApprovalFlag", LectApprovalFlag);
            SqlParameter p9 = new SqlParameter("@User_Id", User_Id);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureApproval", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        public static DataSet GetAllActive_Batch_ForDivYearProductCenter(string Division_Code, string YearName, string ProductCode, string CentreCode, string flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Product_Code", ProductCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionYearProductCenter", p1, p2, p3, p4, p5));
        }
        public static DataSet GetAllActive_Batch_ForDivYearProductCenter_ForMultiProduct(string Division_Code, string YearName, string ProductCode, string CentreCode, string courseCode, string flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Product_Code", ProductCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@courseCode", courseCode);
            SqlParameter p6 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionYearProductCenter", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetStudentAbsentDetails(string division, string centre)
        {
            SqlParameter p1 = new SqlParameter("@centre", centre);
            SqlParameter p2 = new SqlParameter("@DivisionCode", division);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_Get_StudentAbsent_Details", p1, p2));
        }

        public static DataSet GetStudentAbsentCenterWise(string division, string centre, string From_Date, string To_Date)
        {
            SqlParameter p1 = new SqlParameter("@Divsision", division);
            SqlParameter p2 = new SqlParameter("@centre", centre);
            SqlParameter p3 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p4 = new SqlParameter("@To_Date", To_Date);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetStudentAbsent_Centerwise", p1, p2, p3, p4));
        }


        public static DataSet Dashboard_SchedulingEngine(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_DashboardScheduleEngine", p1, p2, p3, p4, p5, p6));

        }


        public static DataSet GetStudentAbsentLectureWise(string division, string centre, string fromdate, string Todate)
        {
            SqlParameter p1 = new SqlParameter("@Divsision", division);
            SqlParameter p2 = new SqlParameter("@centre", centre);
            SqlParameter p3 = new SqlParameter("@From_Date", fromdate);
            SqlParameter p4 = new SqlParameter("@To_Date", Todate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_SE_GetStudentAbsent_Centerwise", p1, p2, p3, p4));
        }



        public static DataSet GetSchedule_Day_PlanningBy_Division_Year_Standard_Subject_Center(string Division_Code, string YearName, string SubjectCode, string Center_Code, string Stream_Code, string LMSProductCode, string Scheduling_Horizon_TypeCode, DateTime Fromdate, DateTime ToDate, string Batch_Code)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p6 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p7 = new SqlParameter("@Scheduling_Horizon_TypeCode", Scheduling_Horizon_TypeCode);
            SqlParameter p8 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p9 = new SqlParameter("@Todate", ToDate);
            SqlParameter p10 = new SqlParameter("@Batch_Code", Batch_Code);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetSchedule_Day_PlanningBy_Division_Year_Standard_Subject_Center", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }


        public static DataSet GetSchedule_Day_PlanningBy_Division_Year_Standard_Subject(string Division_Code, string YearName, string SubjectCode, string Center_Code, string Stream_Code, string LMSProductCode, string Scheduling_Horizon_TypeCode, DateTime Fromdate, DateTime ToDate)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p6 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p7 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p8 = new SqlParameter("@Todate", ToDate);
            SqlParameter p9 = new SqlParameter("@Scheduling_Horizon_TypeCode", Scheduling_Horizon_TypeCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetSchedule_Day_PlanningBy_Division_Year_Standard_Subject", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }


        public static DataSet Insert_Schedule_DayDistribution(string Division_Code, string Acad_Year, string Standard_Code, string LMSProductCode, string SchedulHorizonTypeCode, string CenterCode, string SubjectCode, string TeacherShortName, string Created_By, DateTime Session_Date, string Batch_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@SchedulHorizonTypeCode", SchedulHorizonTypeCode);
            SqlParameter p6 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p7 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p8 = new SqlParameter("@TeacherShortName", TeacherShortName);
            SqlParameter p9 = new SqlParameter("@Session_Date", Session_Date);
            SqlParameter p10 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p11 = new SqlParameter("@Batch_Code", Batch_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Schedule_Day_Planning", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
        }

        public static DataSet Insert_Schedule_Schedule_Day_Batchwise(string Division_Code, string Acad_Year, string Standard_Code, string LMSProductCode, string SchedulHorizonTypeCode, string CenterCode, string TeacherShortName, string Created_By, DateTime Session_Date, string Batch_Code, string Session_Slot_Code, string ExistLecture_Id, string Chapter_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@SchedulHorizonTypeCode", SchedulHorizonTypeCode);
            SqlParameter p6 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p7 = new SqlParameter("@TeacherShortName", TeacherShortName);
            SqlParameter p8 = new SqlParameter("@Session_Date", Session_Date);
            SqlParameter p9 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p10 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p11 = new SqlParameter("@Session_Slot_Code", Session_Slot_Code);
            SqlParameter p12 = new SqlParameter("@ExistLecture_Id", ExistLecture_Id);
            SqlParameter p13 = new SqlParameter("@Chapter_Code", Chapter_Code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Schedule_Day_Batchwise_New", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13));
        }


        public static DataSet GetLectureCancellationAdjustment(string division, string centername, string fdate, string tdate)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@fdate", fdate);
            SqlParameter p4 = new SqlParameter("@tdate", tdate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_Get_Lecture_Cancellation_Adjustment", p1, p2, p3, p4));
        }


        public static DataSet GetManageTimeTableDetails(string Division_Code, string YearName, string SubjectCode, string Center_Code, string Stream_Code, string LMSProductCode, string Scheduling_Horizon_TypeCode, DateTime Fromdate, DateTime ToDate, string Slot_Code, string Batch_Code)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p6 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p7 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p8 = new SqlParameter("@Todate", ToDate);
            SqlParameter p9 = new SqlParameter("@Scheduling_Horizon_TypeCode", Scheduling_Horizon_TypeCode);
            SqlParameter p10 = new SqlParameter("@Slot_Code", Slot_Code);
            SqlParameter p11 = new SqlParameter("@Batch_Code", Batch_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_ManageTimeTable", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
        }

        public static DataSet GetallSubjectGroupbyStreamCode(string streamcode)
        {
            SqlParameter p1 = new SqlParameter("@Stream_Code", streamcode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetSubjectGroup_StreamCode", p1));
        }

        public static DataSet GetUser_Company_Division_Zone_Center(int Flag, string Userid, string Divisioncode, string Zonecode, string Companycode)
        {
            SqlParameter p = new SqlParameter("@flag", SqlDbType.Int);
            p.Value = Flag;
            SqlParameter p1 = new SqlParameter("@user_id", SqlDbType.VarChar);
            p1.Value = Userid;
            SqlParameter p2 = new SqlParameter("@division_code", SqlDbType.VarChar);
            p2.Value = Divisioncode;
            SqlParameter p3 = new SqlParameter("@Zone_code", SqlDbType.VarChar);
            p3.Value = Zonecode;
            SqlParameter p4 = new SqlParameter("@Company_Code", SqlDbType.VarChar);
            p4.Value = Companycode;
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetUser_Company_Division_Zone_Center", p, p1, p2, p3, p4));
        }


        /// <summary>
        /// LMS Integration
        /// </summary>
        /// <param name="Pkey"></param>
        /// <returns></returns>
        public static DataSet LMS_PassAllStudentdetailstoLMSApp(string Pkey)
        {
            SqlParameter p1 = new SqlParameter("@PKey", Pkey);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_LMS_PassAllStudentdetailstoLMSApp", p1));
        }

        public static DataSet LMS_PassAllStudentdetailstoLMSApponConfirmation(string SBEntrycode)
        {
            SqlParameter p1 = new SqlParameter("@SBEntrycode", SBEntrycode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_LMS_PassAllStudentdetails", p1));
        }


        /// <summary>
        /// Cross Batch Functionality
        /// </summary>
        /// <param name="PKey"></param>
        /// <returns></returns>

        public static DataSet GetBatchBY_PKey_Cross_Batch(string PKey)
        {
            //Try
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            DataSet XYZ = SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatch_ByPKey_Cross_Batch", p1, p2, p3);
            return (XYZ);
        }

        public static DataSet LMS_PassAllStudentdetailstoLMSApp_Cross_Division(string Pkey)
        {
            SqlParameter p1 = new SqlParameter("@PKey", Pkey);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_LMS_PassAllStudentdetailstoLMSApp_Cross_batch", p1));
        }

        public static DataSet GetBatchBy_Division_Year_Standard_Centre_Cross_Batch(string Division_Code, string YearName, string StandardCode, string CentreCode, string BatchName)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@BatchName", BatchName);
            SqlParameter p6 = new SqlParameter("@Flag", 1);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatchBy_Division_Year_Standard_Centre_Cross_Batch", p1, p2, p3, p4, p5, p6));
        }


        public static DataSet GetBatchBY_PKey_SubjectGrp(string PKey, string subjectgrpup, string institute, string StreamCode)
        {
            //Try
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            SqlParameter p4 = new SqlParameter("@SubjectGroup", subjectgrpup);
            SqlParameter p5 = new SqlParameter("@InstituteName", institute);
            SqlParameter p6 = new SqlParameter("@StreamCode", StreamCode);
            DataSet XYZ = SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatch_ByPKey_Subjectgroup", p1, p2, p3, p4, p5, p6);
            return (XYZ);
        }
        public static DataSet GetallInstitutename()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllInstitution"));
        }

        public static DataSet GetAllClassroomProduct_ByPKEY(string PKey)
        {
            //Try
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            DataSet XYZ = SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatch_ByPKey_Classroom", p1, p2, p3);
            return (XYZ);
        }

        public static DataSet Get_Chapter_Sequence(string Division_Code, string Acad_Year, string CourseCode, string ProductCode, string Subject_Code, string Centre_Code, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", Division_Code);
            SqlParameter p2 = new SqlParameter("@AcadYear", Acad_Year);
            SqlParameter p3 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@SubjectCode", Subject_Code);
            SqlParameter p6 = new SqlParameter("@CentreCode", Centre_Code);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_Chapter_Sequence", p1, p2, p3, p4, p5, p6, p7));
        }

        public static int InsertUpdateChapterSequence(string Division_Code, string Acad_Year, string Stream_Code, string ProductCode, string Subject_Code, string Centre_Code, string Chapter_Code, string OrderNo, string Created_By, string flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year ", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Stream_Code ", Stream_Code);
            SqlParameter p4 = new SqlParameter("@Centre_Code ", Centre_Code);
            SqlParameter p5 = new SqlParameter("@Subject_Code ", Subject_Code);
            SqlParameter p6 = new SqlParameter("@Chapter_Code ", Chapter_Code);
            SqlParameter p7 = new SqlParameter("@ProductCode ", ProductCode);
            SqlParameter p8 = new SqlParameter("@Order_No ", OrderNo);
            SqlParameter p9 = new SqlParameter("@Created_By ", Created_By);
            SqlParameter p10 = new SqlParameter("@Flag", flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Insert_Update_Chapter_Sequencing", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }

        public static int InsertCopyChapterSequence(string Division_Code, string Acad_Year, string Stream_Code, string ProductCode, string Subject_Code, string RefCentre_Code, string SourceCentre_Code, string Created_By, string flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year ", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Stream_Code ", Stream_Code);
            SqlParameter p4 = new SqlParameter("@RefCentre_Code ", RefCentre_Code);
            SqlParameter p5 = new SqlParameter("@SourceCentre_Code ", SourceCentre_Code);
            SqlParameter p6 = new SqlParameter("@Subject_Code ", Subject_Code);
            SqlParameter p7 = new SqlParameter("@ProductCode ", ProductCode);
            SqlParameter p8 = new SqlParameter("@Created_By ", Created_By);
            SqlParameter p9 = new SqlParameter("@Flag", flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Copy_Chapter_Sequence", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }


        public static DataSet Get_TeacherByDivision(string Division_Code, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTeacherByDivision", p1, p2));
        }

        public static DataSet Get_DateDayByMonthYear(string Month, string Year, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Month", Month);
            SqlParameter p2 = new SqlParameter("@Year", Year);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetDateDayByMonthYear", p1, p2, p3));
        }

        public static int InsertUpdateFacultyConstraint(string Division_Code, string Partner_Code, string For_Date, string Session1_Available_Flag, string Session2_Available_Flag, string Session3_Available_Flag, string Session4_Available_Flag, string IsActive, string Created_By, string Flag, string PKey)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p3 = new SqlParameter("@For_Date", For_Date);
            SqlParameter p4 = new SqlParameter("@Session1_Available_Flag", Session1_Available_Flag);
            SqlParameter p5 = new SqlParameter("@Session2_Available_Flag", Session2_Available_Flag);
            SqlParameter p6 = new SqlParameter("@Session3_Available_Flag", Session3_Available_Flag);
            SqlParameter p7 = new SqlParameter("@Session4_Available_Flag", Session4_Available_Flag);
            SqlParameter p8 = new SqlParameter("@IsActive", IsActive);
            SqlParameter p9 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p10 = new SqlParameter("@Flag", Flag);
            SqlParameter p11 = new SqlParameter("@PKey", PKey);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Insert_Update_FacultyConstraint", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
        }

        public static DataSet Get_FacultyConstraint(string Division_Code, string Partner_Name, string For_Date, string Flag, string PKey)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Partner_Name", Partner_Name);
            SqlParameter p3 = new SqlParameter("@For_Date", For_Date);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);
            SqlParameter p5 = new SqlParameter("@PKey", PKey);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_FacultyConstraint", p1, p2, p3, p4, p5));
        }

        //15-07-2015
        public static DataSet Get_LectureScheduleByDivisionCourse(string Division_Code, string CourseCode, string SubjectCode, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureScheduleByDivisionCourse", p1, p2, p3, p4));
        }




        public static DataSet GetRFIDDevice(string flag, string Device_Code, string From_Date, string To_Date)
        {
            SqlParameter p1 = new SqlParameter("@flag", flag);
            SqlParameter p2 = new SqlParameter("@Device_Code", Device_Code);
            SqlParameter p3 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p4 = new SqlParameter("@To_Date", To_Date);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_RFID_Device", p1, p2, p3, p4));
        }


        public static DataSet Get_LectureScheduleBulkEntry(string Division_Code, string CourseCode, string SubjectCode, string Batch_Code, string Acad_Year, string ProductCode, string Centre_Code, string Activity_Id, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p6 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p7 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p8 = new SqlParameter("@Activity_Id", Activity_Id);
            SqlParameter p9 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureScheduleBulkEntry", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        public static DataSet GetFacultyLateComing(string division, string centername, string fdate, string tdate)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@fdate", fdate);
            SqlParameter p4 = new SqlParameter("@tdate", tdate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Se_Get_Faculty_LateComming", p1, p2, p3, p4));
        }



        public static int AssignChapter_To_Schedule_Batchwise(string Division_Code, string YearName, string Center_Code, string Stream_Code, string LMSProductCode, DateTime Fromdate, DateTime ToDate)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@StandardCode", Stream_Code);
            SqlParameter p4 = new SqlParameter("@LMSProduct", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p6 = new SqlParameter("@Session_DateFrom", Fromdate);
            SqlParameter p7 = new SqlParameter("@Session_DateTo", ToDate);
            return (Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_AssignChapter_To_Schedule_Batchwise", p1, p2, p3, p4, p5, p6, p7)));
        }



        public static int Insert_Update_LectureScheduleBy_Admin(string Division_Code, string YearName, string Center_Code, string Stream_Code, string LMSProductCode, DateTime Fromdate, DateTime ToDate, string Created_By, string Activity_Id, string Schedule_Horizon_Type_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p4 = new SqlParameter("@ProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Center_Code);
            SqlParameter p6 = new SqlParameter("@ToSession_Date", ToDate);
            SqlParameter p7 = new SqlParameter("@FromSession_Date", Fromdate);
            SqlParameter p8 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p9 = new SqlParameter("@Activity_Id", Activity_Id);
            SqlParameter p10 = new SqlParameter("@Schedule_Horizon_Type_Code", Schedule_Horizon_Type_Code);
            return (Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Insert_Update_LectureScheduleBy_Admin", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10)));
        }




        public static int Schedule_Day_Batchwise_Authorization(string Division_Code, string YearName, string Center_Code, string Stream_Code, string LMSProductCode, DateTime Session_Date, int IsAuthorised, string Created_By, string Authorised_By, DateTime Authorised_On, string Activity_Id, int Flag, DateTime ToSession_Date, string Schedule_Horizon_Type_Code, string Batch_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Course_Code", Stream_Code);
            SqlParameter p4 = new SqlParameter("@Schedule_Horizon_Type_Code", Schedule_Horizon_Type_Code);
            SqlParameter p5 = new SqlParameter("@Activity_Id", Activity_Id);
            SqlParameter p6 = new SqlParameter("@Centre_Code", Center_Code);
            SqlParameter p7 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p8 = new SqlParameter("@Session_Date", Session_Date);
            SqlParameter p9 = new SqlParameter("@IsAuthorised", IsAuthorised);
            SqlParameter p10 = new SqlParameter("@Authorised_By", Authorised_By);
            SqlParameter p11 = new SqlParameter("@Authorised_On", Authorised_On);
            SqlParameter p12 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p13 = new SqlParameter("@Flag", Flag);
            SqlParameter p14 = new SqlParameter("@ToSession_Date", ToSession_Date);
            SqlParameter p15 = new SqlParameter("@Batch_Code", Batch_Code);
            return (Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Update_Schedule_Day_Batchwise_Authorization", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15)));
        }


        public static DataSet GetLectureCancellation_Adjustment_RPT(string division, string centre, string acad_Year, string stream_code, string From_Date, string To_Date)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Center_Code", centre);
            SqlParameter p3 = new SqlParameter("@Acad_Year", acad_Year);
            SqlParameter p4 = new SqlParameter("@FromDate", From_Date);
            SqlParameter p5 = new SqlParameter("@ToDate", To_Date);
            SqlParameter p6 = new SqlParameter("@Stream_Code", stream_code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_LectureCancellation&Adjustment", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetLectureCancellation_Adjustment_RPT_Batchwise(string division, string centre, string acad_Year, string stream_code, string From_Date, string To_Date)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Center_Code", centre);
            SqlParameter p3 = new SqlParameter("@Acad_Year", acad_Year);
            SqlParameter p4 = new SqlParameter("@FromDate", From_Date);
            SqlParameter p5 = new SqlParameter("@ToDate", To_Date);
            SqlParameter p6 = new SqlParameter("@Stream_Code", stream_code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_LectureCancellation&Adjustment_Batchwise", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetAllActive_Batch_ForDivCenter(string Division_Code, string CentreCode, string standard, string AcadYear, string flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@StandardCode", standard);
            SqlParameter p4 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p5 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2, p3, p4, p5));
        }

        public static DataSet GetAll_Roll_ForDivCenterBatch(string Division_Code, string CentreCode, string Batch, string flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@Batch_Code", Batch);
            SqlParameter p4 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2, p3, p4));
        }

        public static DataSet Get_SearchGrid_ForDivCenterBatchRoll(string Division_Code, string CentreCode, string Batch, string roll, string Standard, string flag, string AcadYear, string fromdate, string todate)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@Batch_Code", Batch);
            SqlParameter p4 = new SqlParameter("@RollNo", roll);
            SqlParameter p5 = new SqlParameter("@StandardCode", Standard);
            SqlParameter p6 = new SqlParameter("@Flag", flag);
            SqlParameter p7 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p8 = new SqlParameter("@fromdate", fromdate);
            SqlParameter p9 = new SqlParameter("@todate", todate);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        public static DataSet Get_SearchGrid_ForMultipleBatchStudentwise(string Division_Code, string CentreCode, string Batch, string Standard, string flag, string AcadYear, string fromdate, string todate)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);//@Division_Code
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);//@Centre_Code
            SqlParameter p3 = new SqlParameter("@Batch_Code", Batch);//@Batch_Code
            SqlParameter p4 = new SqlParameter("@StandardCode", Standard);//@StandardCode
            SqlParameter p5 = new SqlParameter("@Flag", flag);
            SqlParameter p6 = new SqlParameter("@AcadYear", AcadYear);//@AcadYear
            SqlParameter p7 = new SqlParameter("@fromdate", fromdate);//@fromdate
            SqlParameter p8 = new SqlParameter("@todate", todate);//@todate

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet Get_FillStandard_Rpt(string Division_Code, string flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);

            SqlParameter p2 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2));
        }

        public static DataSet Get_SearchGrid_ForBatchwiseConcise_Rpt(string Division_Code, string CentreCode, string standard, string Batch, string FromDate, string ToDate, string flag, string AcadYear)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@StandardCode", standard);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch);
            SqlParameter p5 = new SqlParameter("@fromdate", FromDate);
            SqlParameter p6 = new SqlParameter("@todate", ToDate);
            SqlParameter p7 = new SqlParameter("@Flag", flag);
            SqlParameter p8 = new SqlParameter("@AcadYear", AcadYear);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet GetBranchWiseTTDetailed_Report(string division, string centre, string acad_Year, string stream_code, string From_Date, string To_Date)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Center_Code", centre);
            SqlParameter p3 = new SqlParameter("@Acad_Year", acad_Year);
            SqlParameter p4 = new SqlParameter("@FromDate", From_Date);
            SqlParameter p5 = new SqlParameter("@ToDate", To_Date);
            SqlParameter p6 = new SqlParameter("@Stream_Code", stream_code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_BranchWiseTTDetailed", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet PrintTimeTableDetails(string Division_Code, string YearName, string LMSProductCode, string Partner_Code, DateTime Fromdate, DateTime ToDate, string Subject_Code, string Center_Code, string BatchCode, string CourseCode)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p4 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p5 = new SqlParameter("@Todate", ToDate);
            SqlParameter p6 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p8 = new SqlParameter("@CenterCode", Center_Code);
            SqlParameter p9 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p10 = new SqlParameter("@CourseCode", CourseCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetData_ForTime_Table_NEW", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }

        public static DataSet PrintTimeTableDetails_LecturewiseEntry(string Division_Code, string YearName, string LMSProductCode, string Partner_Code, DateTime Fromdate, DateTime ToDate, string Subject_Code, string Center_Code, string BatchCode, string CourseCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p4 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p5 = new SqlParameter("@Todate", ToDate);
            SqlParameter p6 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p8 = new SqlParameter("@CenterCode", Center_Code);
            SqlParameter p9 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p10 = new SqlParameter("@CourseCode", CourseCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetData_ForTime_Table_Lecturewise", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }

        public static DataSet GetPrintTimeTableData(string Division_Code, string YearName, string LMSProductCode, string Partner_Code, DateTime Fromdate, DateTime ToDate, string Subject_Code, string Center_Code, string BatchCode, string CourseCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p4 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p5 = new SqlParameter("@Todate", ToDate);
            SqlParameter p6 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p8 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p9 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p10 = new SqlParameter("@CourseCode", CourseCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetPrintTimeTableData_NEW", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }


        public static DataSet GetFaculty(string Division_Code, string Acad_Year, int flag, string stream_code, string LMSProductCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Stream_Code", stream_code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@Flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetFacultyByDivision", p1, p2, p3, p4, p5));
        }

        public static DataSet GetMailDetails_ByCenter(string Center_Code, string MailType)
        {
            SqlParameter p1 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p2 = new SqlParameter("@MailType", MailType);
            SqlParameter p3 = new SqlParameter("@Flag", 1);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_MailDetails", p1, p2, p3));
        }

        public static int Insert_Mailog(string Mail_To, string Subject, string Body, int Att_Flag, string Att_FileName, string sendstatus, string sendby, int flag, string Center_Code, string MailType)
        {
            SqlParameter p0 = new SqlParameter("@Mail_To", Mail_To);
            SqlParameter p1 = new SqlParameter("@Subject", Subject);
            SqlParameter p2 = new SqlParameter("@Body", Body);
            SqlParameter p3 = new SqlParameter("@Att_Flag", Att_Flag);
            SqlParameter p4 = new SqlParameter("@Att_FileName", Att_FileName);
            SqlParameter p5 = new SqlParameter("@SendStatus", sendstatus);
            SqlParameter p6 = new SqlParameter("@SendBy", sendby);
            SqlParameter p7 = new SqlParameter("@flag", flag);
            SqlParameter p8 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p9 = new SqlParameter("@MailType", MailType);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertMailLog", p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }


        public static DataSet Check_MesageTemplate(string Message_Code, string Division_Code, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Message_Code", Message_Code);
            SqlParameter p3 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p1, p2, p3));
        }

        public static DataSet Check_SMSSendStatus(string PKey, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Pkey", PKey);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p1, p2));
        }

        public static DataSet Get_GetLectureSchedule_PKey_ForSMS(string PKeyId)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKeyId);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetLectureSchedule_PKey", p1));
        }

        public static int Insert_SMSLog(string Centre_Code, string Message_Code, string MobileNo, string SMSText, string SendStatus, string Sendby, string SMSType)
        {
            SqlParameter p0 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p1 = new SqlParameter("@Message_Code", Message_Code);
            SqlParameter p2 = new SqlParameter("@MobileNo", MobileNo);
            SqlParameter p3 = new SqlParameter("@SMSText", SMSText);
            SqlParameter p4 = new SqlParameter("@SendStatus", SendStatus);
            SqlParameter p5 = new SqlParameter("@Sendby", Sendby);
            SqlParameter p6 = new SqlParameter("@SMSType", SMSType);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertSMSLog", p0, p1, p2, p3, p4, p5, p6));
        }

        public static int Update_SMSSendStatus_T506(string Pkey, int Sentflag, string Mode, int flag)
        {
            SqlParameter p0 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p1 = new SqlParameter("@Sentflag", Sentflag);
            SqlParameter p2 = new SqlParameter("@SendingMode", Mode);
            SqlParameter p3 = new SqlParameter("@flag", flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p0, p1, p2, p3));
        }

        public static DataSet MessageTemplate_CreatePkey(string LectureID, int flag)
        {
            SqlParameter p1 = new SqlParameter("@LectureScheduleID", LectureID);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p1, p2));
        }

        public static int Update_LectureSMSSendStatus_T506(string Pkey, int Sentflag, string Mode, int flag)
        {
            SqlParameter p0 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p1 = new SqlParameter("@Sentflag", Sentflag);
            SqlParameter p2 = new SqlParameter("@SendingMode", Mode);
            SqlParameter p3 = new SqlParameter("@flag", flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p0, p1, p2, p3));
        }


        public static DataSet GetStudent_ForLectureSMS(string LecturePKey)
        {
            SqlParameter p1 = new SqlParameter("@LecturePKey", LecturePKey);
            //SqlParameter p2 = new SqlParameter("@BatchCode", BatchCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetStudent_ForLectureScheduleSMS", p1));
        }

        public static DataSet GetPartner_ForLectureSMS(string LecturePKey, int flag)
        {
            SqlParameter p1 = new SqlParameter("@LectureScheduleID", LecturePKey);
            SqlParameter p2 = new SqlParameter("@flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p1, p2));
        }

        public static DataSet GetStudent_ForLectureCHKSMS(string LecturePKey)
        {
            SqlParameter p1 = new SqlParameter("@LecturePKey", LecturePKey);
            //SqlParameter p2 = new SqlParameter("@BatchCode", BatchCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetStudent_ForLectureScheduleSMS", p1));
        }


        public static DataSet PrintTimeTableMTT(string Division_Code, string YearName, string LMSProductCode, DateTime Fromdate, DateTime ToDate, string Center_Code)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p4 = new SqlParameter("@FromDate", Fromdate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);

            SqlParameter p6 = new SqlParameter("@Center_Code", Center_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetPrintTimeTableDataMTT", p1, p2, p3, p4, p5, p6));
        }


        public static DataSet PrintTimeTableDetailsSavePrint(string Division_Code, string YearName, string LMSProductCode, string Partner_Code, DateTime Fromdate, DateTime ToDate, string Subject_Code, string Center_Code)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p4 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p5 = new SqlParameter("@Todate", ToDate);
            SqlParameter p6 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p8 = new SqlParameter("@CenterCode", Center_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetData_ForTime_Table_SavePrint", p1, p2, p3, p4, p5, p6, p7, p8));
        }


        //PC on 12 Oct


        public static DataSet GetChapterFacultyBy_Division_Year_Standard_Subject_Center(string Division_Code, string YearName, string SubjectCode, string Center_Code, string Stream_Code, string Scheduling_Horizon_TypeCode)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p6 = new SqlParameter("@Scheduling_Horizon_TypeCode", Scheduling_Horizon_TypeCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetChapterFaculty_Division_Year_Standard_Subject_Center", p1, p2, p3, p4, p5, p6));
        }


        public static DataSet GetFacultyBy_DivisionStandardSubjectCenter(string Division_Code, string YearName, string SubjectCode, string Center_Code, string Stream_Code, string ChapterCode)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p6 = new SqlParameter("@Chapter_Code", ChapterCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetFacultyCode_ByDivisionCenterSubjectChaoter", p1, p2, p3, p4, p5, p6));
        }


        public static string Insert_AdditionalLectureDetails(string Division_Code, string Acad_Year, string Stream_Code, string Centre_Code, string Subject_Code, string LMSProductCode, string Partner_Code, string Chapter_Code, string Batch_Code, int ExtraSession_Count, int Extra_Lecture_Duration, int IsActive, string Created_By, int Flag)
        {
            SqlParameter[] p = new SqlParameter[15];
            p[0] = new SqlParameter("@Division_Code", Division_Code);
            p[1] = new SqlParameter("@Acad_Year", Acad_Year);
            p[2] = new SqlParameter("@Stream_Code", Stream_Code);
            p[3] = new SqlParameter("@Centre_Code", Centre_Code);
            p[4] = new SqlParameter("@Subject_Code", Subject_Code);
            p[5] = new SqlParameter("@LMSProductCode", LMSProductCode);
            p[6] = new SqlParameter("@Partner_Code", Partner_Code);
            p[7] = new SqlParameter("@Chapter_Code", Chapter_Code);
            p[8] = new SqlParameter("@Batch_Code", Batch_Code);
            p[9] = new SqlParameter("@ExtraSession_Count", ExtraSession_Count);
            p[10] = new SqlParameter("@Extra_Lecture_Duration", Extra_Lecture_Duration);
            p[11] = new SqlParameter("@CreatedBy", Created_By);
            p[12] = new SqlParameter("@Is_Active", IsActive);
            p[13] = new SqlParameter("@Flag", Flag);

            p[14] = new SqlParameter("@Results", SqlDbType.VarChar, 50);
            p[14].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_AddiotionalLecture_Details", p);
            return (p[13].Value.ToString());

        }

        public static DataSet GetAdditionalLectureDetails(string Division_Code, string Acad_Year, string Stream_Code, string LMSProductCode, string Subject_Code, string Centre_Code, string Batch_Code, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p6 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p7 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p8 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_AddiotionalLecture_Details", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet Get_LectureDetails_ED(string Pkey, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_AddiotionalLecture_Details", p1, p2));
        }


        public static string Update_AdditionalLectureDetails(string Pkey, int ExtraSession_Count, int Extra_Lecture_Duration, int IsActive, string Created_By, int Flag)
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@Pkey", Pkey);
            p[1] = new SqlParameter("@ExtraSession_Count", ExtraSession_Count);
            p[2] = new SqlParameter("@Extra_Lecture_Duration", Extra_Lecture_Duration);
            p[3] = new SqlParameter("@CreatedBy", Created_By);
            p[4] = new SqlParameter("@Is_Active", IsActive);
            p[5] = new SqlParameter("@Flag", Flag);

            p[6] = new SqlParameter("@Results", SqlDbType.VarChar, 50);
            p[6].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_AddiotionalLecture_Details", p);
            return (p[6].Value.ToString());

        }

        public static DataSet GetFacultyManageTimeTable(string Division_Code, string YearName, string Center_Code, string Stream_Code, string LMSProductCode, string Scheduling_Horizon_TypeCode, string Session_date, string Batch_Code)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p4 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p5 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p6 = new SqlParameter("@SessionDate", Session_date);
            SqlParameter p7 = new SqlParameter("@Scheduling_Horizon_TypeCode", Scheduling_Horizon_TypeCode);
            SqlParameter p8 = new SqlParameter("@Batch_Code", Batch_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_ManageTimeTableFaculty", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet PrintTimeTableDetailsSavePrint(string Division_Code, string YearName, string LMSProductCode, string Partner_Code, DateTime Fromdate, DateTime ToDate, string Subject_Code, string Center_Code, string BatchCode)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p4 = new SqlParameter("@Fromdate", Fromdate);
            SqlParameter p5 = new SqlParameter("@Todate", ToDate);
            SqlParameter p6 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p8 = new SqlParameter("@CenterCode", Center_Code);
            SqlParameter p9 = new SqlParameter("@BatchCode", BatchCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetData_ForTime_Table_SavePrint", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        public static DataSet Get_LectureClosure_Rpt(string Division_Code, string FromDate, string ToDate, string AcadYear, string UserId)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", Division_Code);
            SqlParameter p2 = new SqlParameter("@From_Date", FromDate);
            SqlParameter p3 = new SqlParameter("@To_Date", ToDate);
            SqlParameter p4 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p5 = new SqlParameter("@UserId", UserId);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_rpt_LectureClosure", p1, p2, p3, p4, p5));
        }

        public static DataSet GetAllActive_Batch_ForDivCenterRPT(string Division_Code, string CentreCode, string flag, string User_Code)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@Flag", flag);
            SqlParameter p4 = new SqlParameter("@UserCode", User_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenterRPT", p1, p2, p3, p4));
        }
        public static DataSet GetStudentAbsentLectureWise(string division, string centre, string fromdate, string Todate, string BatchCode, string UserCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Center_Code", centre);
            SqlParameter p3 = new SqlParameter("@From_Date", fromdate);
            SqlParameter p4 = new SqlParameter("@To_Date", Todate);
            SqlParameter p5 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p6 = new SqlParameter("@UserCode", UserCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_SE_GetStudentAbsent_Centerwise", p1, p2, p3, p4, p5, p6));
        }
        public static DataSet GetStudentAbsentLectureWise(string division, string fromdate, string Todate, string centre, string BatchCode, string UserCode, string CourseCode, string AcadYear)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);

            SqlParameter p2 = new SqlParameter("@From_Date", fromdate);
            SqlParameter p3 = new SqlParameter("@To_Date", Todate);
            SqlParameter p4 = new SqlParameter("@Center_Code", centre);
            SqlParameter p5 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p6 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p7 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p8 = new SqlParameter("@AcadYear", AcadYear);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_SE_GetStudentAbsent_Centerwise", p1, p2, p3, p4, p5, p6, p7));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_rpt_StudentAbsentSummary_Lecturewise", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet GetRPTChapterSTDateENDate(string division, string acad_Year, string centre, string stream_code, string BatchCode, string SubjectCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Acad_Year", acad_Year);
            SqlParameter p3 = new SqlParameter("@Center_Code", centre);
            SqlParameter p4 = new SqlParameter("@Standard_Code", stream_code);
            SqlParameter p5 = new SqlParameter("@Batch_Code", BatchCode);
            SqlParameter p6 = new SqlParameter("@Subject_Code", SubjectCode);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_ChapterSTDateENDate", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetRPTFacultyAvailChartDT(string division, string acad_Year, string Month)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Acad_Year", acad_Year);
            SqlParameter p3 = new SqlParameter("@date", Month);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_FacultyAvailChartDT", p1, p2, p3));
        }
        //Digambar 18 Nov 2015
        //Assign Chaptercode (Manage Time Table)
        public static DataSet GetAssignChapter_ManageTimeTableforFaculty(string Division_Code, string Acad_Year, string Standard_Code, string LMSProductCode, string SchedulHorizonTypeCode, string CenterCode, string TeacherShortName, string Created_By, DateTime Session_Date, string Batch_Code, string Session_Slot_Code, string ExistLecture_Id)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@SchedulHorizonTypeCode", SchedulHorizonTypeCode);
            SqlParameter p6 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p7 = new SqlParameter("@TeacherShortName", TeacherShortName);
            SqlParameter p8 = new SqlParameter("@Session_Date", Session_Date);
            SqlParameter p9 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p10 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p11 = new SqlParameter("@Session_Slot_Code", Session_Slot_Code);
            SqlParameter p12 = new SqlParameter("@ExistLecture_Id", ExistLecture_Id);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_AssignChapter_Faculty_TimeTable", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12));
        }
        // 23 Dec 2015 Digambar
        public static DataSet Rpt_Faculty_Availability_Chart_Summary(string Division_Code, string For_Date, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@For_Date", For_Date);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_rpt_Faculty_Availability_Chart_Summary", p1, p2, p3));
        }

        public static DataSet GetAllActive_Batch_ForDivCenter_AllCeneter(string Division_Code, string CentreCode, string standard, string UserId, string flag, string AcadYear)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@StandardCode", standard);
            SqlParameter p4 = new SqlParameter("@User_ID", UserId);
            SqlParameter p5 = new SqlParameter("@Flag", flag);
            SqlParameter p6 = new SqlParameter("@AcadYear", AcadYear);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetAll_Roll_ForDivCenterBatch_AllCenterBatch(string Division_Code, string CentreCode, string StandardCode, string Batch, string UserId, string flag, string AcadYear)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@StandardCode", StandardCode);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch);
            SqlParameter p5 = new SqlParameter("@user_id", UserId);
            SqlParameter p6 = new SqlParameter("@Flag", flag);
            SqlParameter p7 = new SqlParameter("@AcadYear", AcadYear);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForDivisionCenter", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet Rpt_ModeOfAttendance(string division, string AcadYear, string fromdate, string Todate, string CourseCode, string UserCode, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p3 = new SqlParameter("@fromdate", fromdate);
            SqlParameter p4 = new SqlParameter("@todate", Todate);
            SqlParameter p5 = new SqlParameter("@User_ID", UserCode);
            SqlParameter p6 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p7 = new SqlParameter("@Flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_ModeOfAttendance", p1, p2, p3, p4, p5, p6, p7));
        }


        public static DataSet Get_SearchGrid_ForBatchwiseAbsenteeism_Summary(string Division_Code, string CentreCode, string standard, string Batch, string FromDate, string ToDate, string flag, string AcadYear)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@StandardCode", standard);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch);
            SqlParameter p5 = new SqlParameter("@fromdate", FromDate);
            SqlParameter p6 = new SqlParameter("@todate", ToDate);
            SqlParameter p7 = new SqlParameter("@Flag", flag);
            SqlParameter p8 = new SqlParameter("@AcadYear", AcadYear);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Batchwise_Absenteeism_Summary", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet Get_Lecture_CancellationSMSTemplate(string Message_Code, string SBEntryCode, string LectureScheduleId, string flag)
        {
            SqlParameter p1 = new SqlParameter("@Message_Code", Message_Code);
            SqlParameter p2 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p3 = new SqlParameter("@LectureScheduleId", LectureScheduleId);
            SqlParameter p4 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_Message_ReplaceKeywords", p1, p2, p3, p4));
        }


        public static DataSet Get_Rpt_CurrentPortionStatus(string Division_Code, string AcadYear, string Standard, string CentreCode, string fromdate, string todate, string Batch, string UserID, string flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p3 = new SqlParameter("@Course_Code", Standard);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@From_Date", fromdate);
            SqlParameter p6 = new SqlParameter("@To_Date", todate);
            SqlParameter p7 = new SqlParameter("@Batch_Code", Batch);
            SqlParameter p8 = new SqlParameter("@UserID", UserID);
            SqlParameter p9 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_CurrentPortionStatus", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }


        public static DataSet GetAllChaptersBy_Division_Year_Standard_Subject_Partner(string Division_Code, string YearName, string SubjectCode, string Partner_Code, string Center_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p4 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p5 = new SqlParameter("@Center_Code", Center_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllChaptersBy_Division_Year_Standard_Subject_Partner", p1, p2, p3, p4, p5));
        }



        //16-05-2016 Added for Lecture Cancellation and rejection Details Archana

        public static DataSet LectureCancellation_rejection(string division, string fdate, string tdate, string AcadYear, string CourseCode, string LmsProduct, string CenterCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@From_Date", fdate);
            SqlParameter p3 = new SqlParameter("@To_Date", tdate);
            SqlParameter p4 = new SqlParameter("@Acad_yr", AcadYear);
            SqlParameter p5 = new SqlParameter("@Course_Code", CourseCode);
            SqlParameter p6 = new SqlParameter("@LMS_Product", LmsProduct);
            SqlParameter p7 = new SqlParameter("@Center_Code", CenterCode);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_Rpt_LectureCancellation_Replacement]", p1, p2, p3, p4, p5, p6, p7));
        }








        //14 March 2016
        //Digambar
        public static DataSet Get_TodaysLecture_Schedule(string Flag, string UserCode)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@Created_By", UserCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTodaysLectureSchedule", p1, p2));
        }

        public static DataSet Get_Lecture_Schedule_Notification_Detail(string Flag, string UserCode)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@Created_By", UserCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureSchedule_Notification_Detail", p1, p2));
        }

        public static DataSet Get_Faculty_Utility_Daywise(string Divcode, string Acad_Year, string Course_Code, string ProductCode, string From_Date, string To_Date, string FacultyCode, string SubjectCode, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Divcode);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p6 = new SqlParameter("@To_Date", To_Date);
            SqlParameter p7 = new SqlParameter("@FacultyCode", FacultyCode);
            SqlParameter p8 = new SqlParameter("@SubjectCode", SubjectCode);
            SqlParameter p9 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetFaculty_Utility_Daywise", p1, p2, p3, p4, p5, p6, p7, p8, p9));

        }
        //Vinod


        public static DataSet GetAllActive_Standard_MultipleDivision(string DivCode)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", DivCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStandard_New_againstMultipleDivision", p1));

        }



        public static DataSet GetallFaculty(string Divcode)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", Divcode);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallFaculty", p1));

        }

        public static DataSet GetAllSubjectsByStandard_New(string Standard_Code)
        {

            SqlParameter p1 = new SqlParameter("@Standard_Code", Standard_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSSubjectByStandard_New", p1));
        }
        public static DataSet Get_LMSProduct_ByDivision_Year_Course_New(string DivisionCode, string AcademicYear, string CourseCode)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter p2 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p3 = new SqlParameter("@Acad_Year", AcademicYear);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSProductNameByDivisionCode_YearName_Course_New", p1, p2, p3));
        }
        //vinod 28 march 2016
        public static DataSet GetBranchWiseTTDetailed_Report(string division, string centre, string acad_Year, string stream_code, string From_Date, string To_Date, string Partner_Code, string UserCode, string SubjectCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Center_Code", centre);
            SqlParameter p3 = new SqlParameter("@Acad_Year", acad_Year);
            SqlParameter p4 = new SqlParameter("@FromDate", From_Date);
            SqlParameter p5 = new SqlParameter("@ToDate", To_Date);
            SqlParameter p6 = new SqlParameter("@Stream_Code", stream_code);
            SqlParameter p7 = new SqlParameter("@PartnerCode", Partner_Code);
            SqlParameter p8 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p9 = new SqlParameter("@SubjectCode", SubjectCode);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_BranchWiseTTDetailed", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }
        public static DataSet GetFacultyWiseDetails(string Division_Code, string Acad_Year, string CourceCode, string LmsProd_Code, string FromDate, string Todate, string Partner_Code, string Centre_Code, string Batch_Code, string User_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_code", CourceCode);
            SqlParameter p4 = new SqlParameter("@LMS_productCode", LmsProd_Code);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@Todate", Todate);
            SqlParameter p7 = new SqlParameter("@PartnerCode", Partner_Code);
            SqlParameter p8 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p9 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p10 = new SqlParameter("@User_Code", User_Code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_Facultywise_Detailed_Timetable", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }
        public static DataSet GetLectureClosureDetail(string Division_Code, string Acad_Year, string CourceCode, string LmsProd_Code, string FromDate, string Todate, string isdeleted, string iscancelled, string Centre_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_code", CourceCode);
            SqlParameter p4 = new SqlParameter("@LMS_productCode", LmsProd_Code);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@Todate", Todate);
            SqlParameter p7 = new SqlParameter("@IsDeleted", isdeleted);
            SqlParameter p8 = new SqlParameter("@IsCanceled", iscancelled);
            SqlParameter p10 = new SqlParameter("@Centre_Code", Centre_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_LectureClosure_Detailed", p1, p2, p3, p4, p5, p6, p7, p8, p10));
        }


        //Added by Digambar


        public static DataSet GetAllSubjectsByStandard_LMSProduct(string Standard_Code, string LMSProduct_Code)
        {

            SqlParameter p1 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p2 = new SqlParameter("@LMSProduct_Code", LMSProduct_Code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSSubjectByStandard_LMSProduct", p1, p2));
        }

        public static DataSet GetTeacherByCenterSubjectChapter(string CenterCode, string SubjectCode, string ChapterCode, string Acad_Year)
        {

            SqlParameter p1 = new SqlParameter("@Centre_Code", CenterCode);
            SqlParameter p2 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p3 = new SqlParameter("@Chapter_Code", ChapterCode);
            SqlParameter p4 = new SqlParameter("@Acad_Year", Acad_Year);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTeacher_By_Center_Subject_Chapter", p1, p2, p3, p4));
        }
        //till here












        //vinod  30 march for Rpt_Student_Remark.aspx
        //this method used for only get STUDENT NAME AND STUDENTCODE
        public static DataSet Getdatafor_RptStudentRemark(string division, string AcadYear, string StandardCode, string LmsProduct, string Center, string BatchCode, string FromDate, string Todate)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p3 = new SqlParameter("@Stream_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@LmsProductCode", LmsProduct);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Center);
            SqlParameter p6 = new SqlParameter("@Batch_Code", BatchCode);
            SqlParameter p7 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p8 = new SqlParameter("@ToDate", Todate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetdataForRpt_Student_Remark", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet GEtRollNo_Namewise(string StudentCode)
        {
            SqlParameter p1 = new SqlParameter("@StudentCode", StudentCode);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetRollNO_SrudentNamewise", p1));
        }
        //GetDataforGridStudentwise
        public static DataSet Getdatafor_RptStudentRemark_Studentwise(string division, string AcadYear, string StandardCode, string LmsProduct, string Center, string BatchCode, string FromDate, string Todate, string SbentryCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p3 = new SqlParameter("@Stream_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@LmsProductCode", LmsProduct);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Center);
            SqlParameter p6 = new SqlParameter("@Batch_Code", BatchCode);
            SqlParameter p7 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p8 = new SqlParameter("@ToDate", Todate);
            SqlParameter p9 = new SqlParameter("@SbentryCode", SbentryCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetData_ForStudentRemark_Studentwise", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        // vinod 01/04/2016
        public static DataSet GetFacultyLateComing_CONCISE(string division, string centername, string fdate, string tdate)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", division);
            SqlParameter p2 = new SqlParameter("@FromDate", fdate);
            SqlParameter p3 = new SqlParameter("@Todate", tdate);
            SqlParameter p4 = new SqlParameter("@CenterCode", centername);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetdataFor_FacultyLateComing_CONCISE", p1, p2, p3, p4));
        }

        //this is for StudentRemark Search
        public static DataSet GetStudentRemarkDetail(string Division_Code, string Acad_Year, string CourceCode, string LmsProd_Code, string FromDate, string Todate, string isdeleted, string iscancelled, string Centre_Code, string Batch_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_code", CourceCode);
            SqlParameter p4 = new SqlParameter("@LMS_productCode", LmsProd_Code);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@Todate", Todate);
            SqlParameter p7 = new SqlParameter("@IsDeleted", isdeleted);
            SqlParameter p8 = new SqlParameter("@IsCanceled", iscancelled);
            SqlParameter p10 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p11 = new SqlParameter("@Batch_Code", Batch_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetDataFor_StudentRemark", p1, p2, p3, p4, p5, p6, p7, p8, p10, p11));
        }

        //  04-04-2016 this is for student Remark Edit
        //public static DataSet GetDatabySchduleid_StudRemark_Edit(string LectSchdulId, string BatchCode, string SubjectCode, string PartnerCode, string DivisionCode, string AcadYear, string CenterCode, string LmsCode)
        public static DataSet GetDatabySchduleid_StudRemark_Edit(string LectSchdulId)
        {
            SqlParameter p1 = new SqlParameter("@LectureSchedule_Id", LectSchdulId);
            //SqlParameter p2 = new SqlParameter("@Batch_Code", BatchCode);
            //SqlParameter p3 = new SqlParameter("@Subject_Code", SubjectCode);
            //SqlParameter p4 = new SqlParameter("@Partner_Code", PartnerCode);
            //SqlParameter p5 = new SqlParameter("@Division_Code", DivisionCode);
            //SqlParameter p6 = new SqlParameter("@AcadYear", AcadYear);
            //SqlParameter p7 = new SqlParameter("@CenterCode", CenterCode);
            //SqlParameter p8 = new SqlParameter("@LmsProductCode", LmsCode);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetDataFor_StudentRemark_Edit", p1, p2, p3, p4, p5, p6, p7, p8));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetDataFor_StudentRemark_Edit", p1));
        }

        // 04-04-20116 for update Student Remark  

        //public static int Insert_UpdateStudentRemark(string PK, string Remarks)
        //{

        //    SqlParameter p1 = new SqlParameter("@pk", PK);
        //    SqlParameter p2 = new SqlParameter("@Remarks", Remarks);
        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_UpdateStudentLectureAttendace_Remark", p1, p2));

        //}

        public static int Insert_UpdateStudentRemark(string PK, string Remarks)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@pk", PK);
            p[1] = new SqlParameter("@Remarks", Remarks);
            //p[2] = new SqlParameter("@NewBatchCount", NewBatchCount);
            //p[3] = new SqlParameter("@CreatedBy", CreatedBy);
            p[2] = new SqlParameter("@Result", SqlDbType.Int);
            p[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_UpdateStudentLectureAttendace_Remark", p);
            return (int.Parse(p[2].Value.ToString()));
        }
        //06-04-2016

        public static DataSet GetFacultyUtilityLoadwise(string Division_Code, string Acad_Year, string CourceCode, string LmsProd_Code, string FromDate, string Todate, string Partner_Code, string Subject_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@AcadYear", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Sream_Code", CourceCode);
            SqlParameter p4 = new SqlParameter("@LmsProductCode", LmsProd_Code);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@Todate", Todate);
            //SqlParameter p7 = new SqlParameter("@IsDeleted", isdeleted);
            //SqlParameter p8 = new SqlParameter("@IsCanceled", iscancelled);
            SqlParameter p9 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p10 = new SqlParameter("@Subject_Code", Subject_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetDataFor_FacultyUtility_Loadwise", p1, p2, p3, p4, p5, p6, p9, p10));
        }

        // Added by Archana 11-04-2016 Attendance
        public static int Insert_UpdateStudentAttendace(string PKey, string ActionFlag, string SBEntryCode, string Batch_Code, string AbsentReason, string AbsentReasonid, string Remarks)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@Batch_Code", Batch_Code);
            p[2] = new SqlParameter("@ActionFlag", ActionFlag);
            p[3] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[4] = new SqlParameter("@AbsentReason", AbsentReason);
            p[5] = new SqlParameter("@AbsentReasonid", AbsentReasonid);
            p[6] = new SqlParameter("@Remarks", Remarks);

            p[7] = new SqlParameter("@Result", SqlDbType.Int);
            p[7].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertUpdateStudentLectureAttendace", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static DataSet GetAllAbsentreasons()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallAbsentReason"));
        }


        // Added by Vinod 14-04-2016
        //for Rpt_Faculty_Payment_Report
        public static DataSet GetFacultyPay_rpt(string division, string fdate, string tdate, string AcadYear, string CourseCode, string LmsProduct, string CenterCode)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            // SqlParameter p2 = new SqlParameter("@centername", centername);
            //SqlParameter p3 = new SqlParameter("@batchname", batch);
            SqlParameter p4 = new SqlParameter("@fdate", fdate);
            SqlParameter p5 = new SqlParameter("@tdate", tdate);
            SqlParameter p6 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p7 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p8 = new SqlParameter("@LmsProductCode", LmsProduct);
            SqlParameter p9 = new SqlParameter("@CenterCode", CenterCode);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_Faculty_Payement_Report", p1, p4, p5, p6, p7, p8, p9));
        }

        //20-04-2016 added forStudent absent summary lecturewise
        public static DataSet GetAllActive_Batch_ForDivCenterRPT_New(string Division_Code, string CentreCode, string flag, string User_Code, string CourseCode, string AcadYear)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p3 = new SqlParameter("@Flag", flag);
            SqlParameter p4 = new SqlParameter("@UserCode", User_Code);
            SqlParameter p5 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p6 = new SqlParameter("@AcadYear", AcadYear);
            
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_GetAllActive_Batch_ForDivisionCenterRPT_New]", p1, p2, p3, p4, p5, p6));
        }






        //06-05-2016 added for get GEt faculty lecture count

        public static DataSet GEtFaculty_Lecture_Count(string Division_Code, string AcadYear, string Stream_Code, string Center_Code, string FromDate, string Todate, string Batch_Code)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", AcadYear);
            SqlParameter p3 = new SqlParameter("@StreamCode", Stream_Code);
            //SqlParameter p4 = new SqlParameter("@StreamCode", Stream_Code);
            SqlParameter p5 = new SqlParameter("@CenterCode", Center_Code);
            SqlParameter p6 = new SqlParameter("@Fromdate", FromDate);
            SqlParameter p7 = new SqlParameter("@Todate", Todate);
            //SqlParameter p10 = new SqlParameter("@Slot_Code", Slot_Code);
            SqlParameter p8 = new SqlParameter("@Batch_Code", Batch_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetFacultyLectureCount_New", p1, p2, p3, p5, p6, p7, p8));
        }



        //04-may-2016 added for fill lecture cancel Reason
        public static DataSet GetAll_LectureCancelReason()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_GetallLectureCancel_Reason]"));
        }






        //public static DataSet getfacultyforLectureCount(string Division_Code, string YearName, string Center_Code, string Stream_Code,  string Session_date, string Batch_Code)
        //{

        //    SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
        //    SqlParameter p2 = new SqlParameter("@YearName", YearName);
        //    SqlParameter p3 = new SqlParameter("@Center_Code", Center_Code);
        //    SqlParameter p4 = new SqlParameter("@Stream_Code", Stream_Code);
        //    //SqlParameter p5 = new SqlParameter("@LMSProductCode", LMSProductCode);
        //    SqlParameter p6 = new SqlParameter("@SessionDate", Session_date);
        //    //SqlParameter p7 = new SqlParameter("@Scheduling_Horizon_TypeCode", Scheduling_Horizon_TypeCode);
        //    SqlParameter p8 = new SqlParameter("@Batch_Code", Batch_Code);
        //    return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_ManageTimeTableFaculty", p1, p2, p3, p4, p6, p8));
        //}


        // Student Attendance Remender Letter

        public static DataSet Attendance_Reminder_Letter(string Division_Code, string Course_code, string Batch_Code, string AcadYear, string Centre_Code, string FromDate, string ToDate, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Standard_code", Course_code);
            SqlParameter p3 = new SqlParameter("@Batch_code", Batch_Code);
            SqlParameter p4 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p7 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p8 = new SqlParameter("@flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Get_AbsentStudent_For_Attendance_Reminder", p1, p2, p3, p4, p5, p6, p7, p8));

        }
        // Student Attendance Remender Letter 

        public static DataSet Attendance_Reminder_Student(string Division_Code, string Course_code, string Batch_Code, string AcadYear, string Centre_Code, string FromDate, string ToDate, int flag, string SbentryCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Standard_code", Course_code);
            SqlParameter p3 = new SqlParameter("@Batch_code", Batch_Code);
            SqlParameter p4 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p7 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p8 = new SqlParameter("@flag", flag);
            SqlParameter p9 = new SqlParameter("@Sbentrycode", SbentryCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Get_AbsentStudent_For_Attendance_Reminder", p1, p2, p3, p4, p5, p6, p7, p8, p9));

        }

        public static DataSet GET_LECTURE_DETAILS(string Lecture_Schedule_Id)
        {
            SqlParameter p1 = new SqlParameter("@Flag", 1);
            SqlParameter p2 = new SqlParameter("@Lecture_Schedule_Id", Lecture_Schedule_Id);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GET_LECTURE_SCHEDULE_DETAILS_SYNC", p1, p2));
        }


        //public static DataSet UPDATE_DBSYNCFLAG_LMSSERVICE(int Flag, int Inner_Flag, string Pkey)
        //{
        //    SqlParameter p1 = new SqlParameter("@Flag", Flag);
        //    SqlParameter p2 = new SqlParameter("@Inner_Flag", Inner_Flag);
        //    SqlParameter p3 = new SqlParameter("@Pkey", Pkey);

        //    return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UPDATE_DBSYNCFLAG_WEBSERVICE", p1, p2,p3));
        //}

        public static DataSet Get_Lecture_Schedule_Decentralized_Azure(int Flag, string Session_Date)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@Session_Date", Session_Date);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GET_LECTURE_SCHEDULE_DETAILS_SYNC_AZURE", p1, p2));
        }

        public static DataSet UPDATE_DBSYNCFLAG_LMSSERVICE(int Flag, int Inner_Flag, string Pkey, string Status_Code, string Reason_Phrase, string Created_By)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@Inner_Flag", Inner_Flag);
            SqlParameter p3 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p4 = new SqlParameter("@Status_Code", Status_Code);
            SqlParameter p5 = new SqlParameter("@Reason_Phrase", Reason_Phrase);
            SqlParameter p6 = new SqlParameter("@Created_By", Created_By);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UPDATE_DBSYNCFLAG_WEBSERVICE", p1, p2, p3, p4, p5, p6));
        }


        public static DataSet GetTeacherByCenterSubjectChapter_New(string CenterCode, string SubjectCode, string ChapterCode, string Acad_Year, string Lect_ScheduleId, int flag)
        {

            SqlParameter p1 = new SqlParameter("@Centre_Code", CenterCode);
            SqlParameter p2 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p3 = new SqlParameter("@Chapter_Code", ChapterCode);
            SqlParameter p4 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p5 = new SqlParameter("@Lect_ScheduleId", Lect_ScheduleId);
            SqlParameter p6 = new SqlParameter("@flag", flag);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTeacher_By_Center_Subject_Chapter", p1, p2, p3, p4));
        }

        public static int InserTeacherLectureAttendace(string PKey, string TeacherInTime, string TeacherInOut)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@PKey", @PKey);
            p[1] = new SqlParameter("@TeacherInTime", TeacherInTime);
            p[2] = new SqlParameter("@TeacherInOut", TeacherInOut);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_InserTeacherLectureAttendace", p);
            return (int.Parse(p[3].Value.ToString()));
        }
        public static int InserTeacherLectureAttendace_New(string PKey, string TeacherInTime, string TeacherInOut, int flag)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", @PKey);
            p[1] = new SqlParameter("@TeacherInTime", TeacherInTime);
            p[2] = new SqlParameter("@TeacherInOut", TeacherInOut);
            p[3] = new SqlParameter("@Flag", flag);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_InserTeacherLectureAttendace", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static DataSet GetLectureCancellation_Adjustment_RPT(string division, string centre, string acad_Year, string stream_code, string From_Date, string To_Date, string UserId)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@Center_Code", centre);
            SqlParameter p3 = new SqlParameter("@Acad_Year", acad_Year);
            SqlParameter p4 = new SqlParameter("@FromDate", From_Date);
            SqlParameter p5 = new SqlParameter("@ToDate", To_Date);
            SqlParameter p6 = new SqlParameter("@Stream_Code", stream_code);
            SqlParameter p7 = new SqlParameter("@User_Code", UserId);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_LectureCancellation&Adjustment", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet CheckValidation_LectureReplace_Partner(string Lecture_Schedule_Id, string replaceFaculty_Code, string Subject_Code, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Lecture_Schedule_Id", Lecture_Schedule_Id);
            SqlParameter p2 = new SqlParameter("@replaceFaculty_Code", replaceFaculty_Code);
            SqlParameter p3 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_CheckValidation_LectureReplace_Partner", p1, p2, p3, p4));

        }


        //sujeer


        public static DataSet GetStudentLateComing(string division, string centername, string acadyear, string course, string fdate, string tdate,string userid)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@acadyear", acadyear);
            SqlParameter p4 = new SqlParameter("@course", course);
            SqlParameter p5 = new SqlParameter("@fdate", fdate);
            SqlParameter p6 = new SqlParameter("@tdate", tdate);
            SqlParameter p7 = new SqlParameter("@userid", userid);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[Usp_Se_Get_Student_LateComming]", p1, p2, p3, p4, p5, p6,p7));
        }

        public static DataSet Get_FillStandard_Rpt_by_ay(string Division_Code, string acadyear)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);

            SqlParameter p2 = new SqlParameter("@acadyear", acadyear);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_Course_Details_by_AY", p1, p2));
        }

        public static DataSet GetStudentLateComingforexport(string division, string centername, string acadyear, string course, string fdate, string tdate)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@acadyear", acadyear);
            SqlParameter p4 = new SqlParameter("@course", course);
            SqlParameter p5 = new SqlParameter("@fdate", fdate);
            SqlParameter p6 = new SqlParameter("@tdate", tdate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[Usp_Se_Get_Student_LateComming_ForExport]", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet Get_Rpt_TodaysAbsenteeism_List(string DivisionCode, string Acad_Year, string CourseCode, string ProductCode, string CenterCode, string SessionDate, string UserCode, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p6 = new SqlParameter("@SessionDate", SessionDate);
            SqlParameter p7 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p8 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Todays_Absenteeism_List", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet Get_Lecture_Schedule_AutoLessonPlan(string Division_Code, string Acad_Year, string ProductCode, string Centre_Code, string Course_Code, string Batch_Code, int Flag, string SubjectCode)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);
            SqlParameter p8 = new SqlParameter("@SubjectCode", SubjectCode);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLectureSchedule_AutoLessonPlan", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet Update_Lecture_Schedule_AutoLessonPlan(string Lecture_ScheduleId, string Created_By, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Lecture_ScheduleId", Lecture_ScheduleId);
            SqlParameter p2 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateLectureSchedule_AutoLessonPlan", p1, p2, p3));
        }
        //archana (23-02-2017) autolessonplan update
        public static DataSet Get_Lecture_Schedule_AutoLessonPlan1(string Division_Code, string Acad_Year, string Course_Code, string ProductCode, string Centre_Code, string Batch_Code, string Created_By, string LectureScheduleId, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Stream_Code", Course_Code);
            SqlParameter p4 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@Batch_Subject_Code", Batch_Code);
            SqlParameter p7 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p8 = new SqlParameter("@Lecture_ScheduleId", LectureScheduleId);
            SqlParameter p9 = new SqlParameter("@Flag", Flag);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateLectureSchedule_AutoLessonPlan", p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }




        //added for faculty lecture count sujeer -23-06-2017


        public static DataSet FillProduct_detials(string Division_Code, string acadyear)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);

            SqlParameter p2 = new SqlParameter("@acadyear", acadyear);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "GET_LMS_PRODUCT_BY_DIVISION_ACADYEAR", p1, p2));
        }
        public static DataSet GetLectureSchedule_detils(string division, string centername, string product, string fdate, string tdate, string acadyear)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@product", product);
            SqlParameter p4 = new SqlParameter("@fdate", fdate);
            SqlParameter p5 = new SqlParameter("@tdate", tdate);
            SqlParameter p6 = new SqlParameter("@acadyear", acadyear);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[RPT_FACULTYWISE_LECTURE_COUNT]", p1, p2, p3, p4, p5, p6));
        }


        //Digambar kadam
        public static DataSet Get_Insert_LinkedBatch(string XmlData, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@XMLData", XmlData);
            SqlParameter p2 = new SqlParameter("@flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_Insert_Batch_Link", p1, p2));
        }

        public static DataSet Report_Student_Attendance_Concise(string division, string centername, string acadyear, string course, string fdate, string tdate)
        {
            SqlParameter p1 = new SqlParameter("@Division", division);
            SqlParameter p2 = new SqlParameter("@centername", centername);
            SqlParameter p3 = new SqlParameter("@acadyear", acadyear);
            SqlParameter p4 = new SqlParameter("@course", course);
            SqlParameter p5 = new SqlParameter("@fdate", fdate);
            SqlParameter p6 = new SqlParameter("@tdate", tdate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Rpt_Student_Attendance_Concise", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetMenuList(string Flag, string User_Code, string Menu_Code)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@User_Code", User_Code);
            SqlParameter p3 = new SqlParameter("@Menu_Code", Menu_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_Menu_List", p1, p2, p3));
        }

        public static DataSet GetApplication_Url()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_Application_URL"));
        }

        //added sujeer for  add demo lecture -23-06-2017
        public static string Insert_DemoLectureDetails(string Division_Code, string Acad_Year, string Stream_Code, string Centre_Code, string Subject_Code, string LMSProductCode, string Partner_Code, string Chapter_Code, string Batch_Code,
            string fromtime, string totime, string date, string Created_By, int Flag)
        {
            SqlParameter[] p = new SqlParameter[15];
            p[0] = new SqlParameter("@Division_Code", Division_Code);
            p[1] = new SqlParameter("@Acad_Year", Acad_Year);
            p[2] = new SqlParameter("@Stream_Code", Stream_Code);
            p[3] = new SqlParameter("@Centre_Code", Centre_Code);
            p[4] = new SqlParameter("@Subject_Code", Subject_Code);
            p[5] = new SqlParameter("@LMSProductCode", LMSProductCode);
            p[6] = new SqlParameter("@Partner_Code", Partner_Code);
            p[7] = new SqlParameter("@Chapter_Code", Chapter_Code);
            p[8] = new SqlParameter("@Batch_Code", Batch_Code);
            p[9] = new SqlParameter("@Ftime", fromtime);
            p[10] = new SqlParameter("@Totime", totime);
            p[11] = new SqlParameter("@Sessiondate", date);
            p[12] = new SqlParameter("@CreatedBy", Created_By);
            // p[13] = new SqlParameter("@Is_Active", IsActive);
            p[13] = new SqlParameter("@Flag", Flag);
            p[14] = new SqlParameter("@Results", SqlDbType.VarChar, 50);
            p[14].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Demo_Lecture_Details", p);
            return (p[14].Value.ToString());

        }

        public static DataSet GetDemoLectureDetails(string Division_Code, string Acad_Year, string Stream_Code, string LMSProductCode, string Subject_Code, string Centre_Code, string Batch_Code, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p6 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p7 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p8 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Demo_Lecture_Details", p1, p2, p3, p4, p5, p6, p7, p8));
        }


        public static DataSet Get_DemoLectureDetails_ED(string Pkey, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Demo_Lecture_Details", p1, p2));
        }

        public static string Update_DemoLectureDetails(string Pkey, string fromtime, string totime, string date, int IsActive, string Created_By, int Flag)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@Pkey", Pkey);
            p[1] = new SqlParameter("@Ftime", fromtime);
            p[2] = new SqlParameter("@Totime", totime);
            p[3] = new SqlParameter("@Sessiondate", date);
            p[4] = new SqlParameter("@Is_Active", IsActive);
            p[5] = new SqlParameter("@CreatedBy", Created_By);
            p[6] = new SqlParameter("@Flag", Flag);
            p[7] = new SqlParameter("@Results", SqlDbType.VarChar, 50);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_Demo_Lecture_Details", p);
            return (p[7].Value.ToString());
        }

        //Digambar Added Code (21-07-2017)
        public static DataSet Report_Partner_Rates_Payment_ActualSheet_HR(string division, string fdate, string tdate,int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", division);
            SqlParameter p2 = new SqlParameter("@FromDate", fdate);
            SqlParameter p3 = new SqlParameter("@ToDate", tdate);
            SqlParameter p4 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_PaymentActualSheet_HR__Report", p1, p2, p3, p4));
        }
        //Digambar Added Code (27-07-2017)
        public static DataSet GetLectureData(string LecturePKey,int flag)
        {
            SqlParameter p1 = new SqlParameter("@LecturePKey", LecturePKey);
            SqlParameter p2 = new SqlParameter("@Flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetLectureData", p1,p2));
        }

        public static DataSet Copy_MultipleLecture_Attendance(string ActualLectureScheduleId, string SelectedLectureId, string CreatedBy, int flag)
        {
            SqlParameter p1 = new SqlParameter("@ActualLectureScheduleId", ActualLectureScheduleId);
            SqlParameter p2 = new SqlParameter("@SelectedLectureId", SelectedLectureId);
            SqlParameter p3 = new SqlParameter("@CreatedBy", CreatedBy);
            SqlParameter p4 = new SqlParameter("@Flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Copy_Multiple_LectureAttendance", p1, p2, p3, p4));
        }

        //Digambar (31 Jul 2017)
        public static DataSet GetAllActive_Student_ForBatch(string BatchCode,string UserId, int flag)
        {
            SqlParameter p1 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p2 = new SqlParameter("@User_ID", UserId);
            SqlParameter p3 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Student_ForBatch", p1, p2, p3));
        }

        public static DataSet GetStudent_Lecture__Remarks(string Div_Code, string AcadYear, string Course_Code, string LMS_Code, string Center_Code, string BatchCode,
                    string From_Date,string To_Date,string SBEntryCode,  string UserId, int flag)
        {
            SqlParameter p0 = new SqlParameter("@Div_Code", Div_Code);
            SqlParameter p1 = new SqlParameter("@AcadYear", AcadYear);//@Course_Code
            SqlParameter p2 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p3 = new SqlParameter("@LMS_Code", LMS_Code);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p6 = new SqlParameter("@From_Date", From_Date);
            SqlParameter p7 = new SqlParameter("@To_Date", To_Date);
            SqlParameter p8 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p9 = new SqlParameter("@User_ID", UserId);
            SqlParameter p10 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_Lecture_Remarks",p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
        }
        
        public static DataSet GetStudent_ForLectureAttendence_AbsentMessageTemplate(string LecturePKey, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@LecturePKey", LecturePKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_GetStudent_Absent_MessageTemplate", p1, p2));
        }

        public static DataSet InsertUpdate_Student_ForLectureAttendence_AbsentMessageTemplate(string LecturePKey,string SBEntryCode,string MessageTemplate,string CreatedBy, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@LecturePKey", LecturePKey);
            SqlParameter p2 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p3 = new SqlParameter("@MessageTemplate", MessageTemplate);
            SqlParameter p4 = new SqlParameter("@CreatedBy", CreatedBy);
            SqlParameter p5 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_InsertUpdate_Student_Absent_MessageTemplate", p1, p2, p3, p4, p5));
        }

        //ADDED BY SUJEER 15-01-2017
        public static DataSet GetAllActive_Product(string Division_Code, string YearName, string course, string Product, string center, string flag)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@coursecode", course);
            SqlParameter p4 = new SqlParameter("@productcode", Product);
            SqlParameter p5 = new SqlParameter("@Centercode", center);
            SqlParameter p6 = new SqlParameter("@Flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_GetLMSproduct_New]", p1, p2, p3, p4, p5, p6));
        }


        public static DataSet GetStudentBy_Division_Year_Standard_Centre(string Division_Code, string streamcode, string CentreCode, string productcode,string Exempted,string Subject, string flag)
        {
            SqlParameter p1 = new SqlParameter("@DIVISIONCODE", Division_Code);
            SqlParameter p2 = new SqlParameter("@STREAMCODE", streamcode);
            SqlParameter p3 = new SqlParameter("@CENTERCODE", CentreCode);
            SqlParameter p4 = new SqlParameter("@PRODUCTCODE", productcode);
            SqlParameter p5 = new SqlParameter("@Exempted", Exempted);
            SqlParameter p6 = new SqlParameter("@Subject", Subject);
            SqlParameter p7 = new SqlParameter("@Flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USB_GET_STUDENT_DETAILS_FOR_Exempt", p1, p2, p3, p4, p5, p6, p7));
        }


        public static int Insert_Rollnumber(string DivisionCode, string Acadyear, string StandardCode, string Productcode, string centercode, string sbentrycode, string spid, string Exemptedfor, string ISActive, string CreatedBy, string subjectcode, string flag)
        {
            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@DIVISIONCODE", DivisionCode);
            p[1] = new SqlParameter("@ACADYEAR", Acadyear);
            p[2] = new SqlParameter("@coursecode", StandardCode);
            p[3] = new SqlParameter("@Productcode", Productcode);
            p[4] = new SqlParameter("@CENTERCODE", centercode);
            p[5] = new SqlParameter("@sbentrycode", sbentrycode);
            p[6] = new SqlParameter("@spid", spid);
            p[7] = new SqlParameter("@Exemptedfor", Exemptedfor);
            p[8] = new SqlParameter("@ISactive", ISActive);
            p[9] = new SqlParameter("@CreatedBy", CreatedBy);
            p[10] = new SqlParameter("@Subjectcode", subjectcode);
            p[11] = new SqlParameter("@FLAG",flag );
            p[12] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[12].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USB_UPDATE_INSERT_STUDENT_DETAILS_FOR_EXEMPTION]", p);
            return (int.Parse(p[12].Value.ToString()));
        }

        //added by suspension

        public static int Insert_suspension(string DivisionCode, string Acadyear, string StandardCode, string Productcode, string centercode, string sbentrycode, string spid, string Suspensionfor, string ISActive, string CreatedBy, string Reason, 
          string Sus_Sdate, string Sus_Edate, string flag)
        {
            SqlParameter[] p = new SqlParameter[15];
            p[0] = new SqlParameter("@DIVISIONCODE", DivisionCode);
            p[1] = new SqlParameter("@ACADYEAR", Acadyear);
            p[2] = new SqlParameter("@coursecode", StandardCode);
            p[3] = new SqlParameter("@Productcode", Productcode);
            p[4] = new SqlParameter("@CENTERCODE", centercode);
            p[5] = new SqlParameter("@sbentrycode", sbentrycode);
            p[6] = new SqlParameter("@spid", spid);
            p[7] = new SqlParameter("@suspensionfor", Suspensionfor);
            p[8] = new SqlParameter("@ISactive", ISActive);
            p[9] = new SqlParameter("@CreatedBy", CreatedBy);
            p[10] = new SqlParameter("@Reason", Reason);
            p[11] = new SqlParameter("@Startdate", Sus_Sdate);
            p[12] = new SqlParameter("@Enddate", Sus_Edate);
            p[13] = new SqlParameter("@FLAG", flag);
            p[14] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[14].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USB_UPDATE_INSERT_STUDENT_DETAILS_FOR_SUSPENSION]", p);
            return (int.Parse(p[14].Value.ToString()));
        }


        //added by sujeer for ICB

        public static string Insert_ICBLectureDetails(string Division_Code, string Acad_Year, string Stream_Code, string Centre_Code, string Subject_Code, string LMSProductCode, string Partner_Code, string Chapter_Code, string Batch_Code,
        string fromtime, string totime, string date, string Created_By, int Flag)
        {
            SqlParameter[] p = new SqlParameter[15];
            p[0] = new SqlParameter("@Division_Code", Division_Code);
            p[1] = new SqlParameter("@Acad_Year", Acad_Year);
            p[2] = new SqlParameter("@Stream_Code", Stream_Code);
            p[3] = new SqlParameter("@Centre_Code", Centre_Code);
            p[4] = new SqlParameter("@Subject_Code", Subject_Code);
            p[5] = new SqlParameter("@LMSProductCode", LMSProductCode);
            p[6] = new SqlParameter("@Partner_Code", Partner_Code);
            p[7] = new SqlParameter("@Chapter_Code", Chapter_Code);
            p[8] = new SqlParameter("@Batch_Code", Batch_Code);
            p[9] = new SqlParameter("@Ftime", fromtime);
            p[10] = new SqlParameter("@Totime", totime);
            p[11] = new SqlParameter("@Sessiondate", date);
            p[12] = new SqlParameter("@CreatedBy", Created_By);
            // p[13] = new SqlParameter("@Is_Active", IsActive);
            p[13] = new SqlParameter("@Flag", Flag);
            p[14] = new SqlParameter("@Results", SqlDbType.VarChar, 50);
            p[14].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_ICB_Lecture_Details", p);
            return (p[14].Value.ToString());

        }

        public static DataSet GetICBLectureDetails(string Division_Code, string Acad_Year, string Stream_Code, string LMSProductCode, string Subject_Code, string Centre_Code, string Batch_Code, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Stream_Code", Stream_Code);
            SqlParameter p4 = new SqlParameter("@LMSProductCode", LMSProductCode);
            SqlParameter p5 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p6 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p7 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p8 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_ICB_Lecture_Details", p1, p2, p3, p4, p5, p6, p7, p8));
        }


        public static DataSet Get_ICBLectureDetails_ED(string Pkey, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_ICB_Lecture_Details", p1, p2));
        }

        public static string Update_ICBLectureDetails(string Pkey, string fromtime, string totime, string date, int IsActive, string Created_By, int Flag)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@Pkey", Pkey);
            p[1] = new SqlParameter("@Ftime", fromtime);
            p[2] = new SqlParameter("@Totime", totime);
            p[3] = new SqlParameter("@Sessiondate", date);
            p[4] = new SqlParameter("@Is_Active", IsActive);
            p[5] = new SqlParameter("@CreatedBy", Created_By);
            p[6] = new SqlParameter("@Flag", Flag);
            p[7] = new SqlParameter("@Results", SqlDbType.VarChar, 50);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_SE_Insert_ICB_Lecture_Details", p);
            return (p[7].Value.ToString());
        }

        public static DataSet GEtFaculty_Lecture_Count_OnTT(string Division_Code, string AcadYear, string Stream_Code, string Center_Code, string FromDate, string Todate, string Batch_Code, string status)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p3 = new SqlParameter("@Course_Code", Stream_Code);
            SqlParameter p4 = new SqlParameter("@Centre_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@From_Date", FromDate);
            SqlParameter p6 = new SqlParameter("@To_Date", Todate);
            SqlParameter p7 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p8 = new SqlParameter("@LectStatusFlag", status);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_GetLectureSchedule_For_Report]", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        //added by sujeer--

        public static DataSet GEtFaculty_Lecture_Overshoot_Count(string Division_Code, string AcadYear, string Stream_Code, string Center_Code, string LMSProduct)
        {

            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p3 = new SqlParameter("@Course_Code", Stream_Code);
            SqlParameter p4 = new SqlParameter("@Centre_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@ProductCode", LMSProduct);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_GetLectureOverShoot_For_Report]", p1, p2, p3, p4, p5));
        }

        public static DataSet GetexemptionsuspensionReport(string division, string centername, string acadyear,string status)
        {
            SqlParameter p1 = new SqlParameter("@Divisioncode", division);
            SqlParameter p2 = new SqlParameter("@centercode", centername);
            SqlParameter p3 = new SqlParameter("@Acadyear", acadyear);
            SqlParameter p4 = new SqlParameter("@status", status);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[Rpt_Get_Student_Exemption_Suspension_Details]", p1, p2, p3, p4));
        }

    }


}

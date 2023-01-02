using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic;
using ShoppingCart.BL;
using System.Web;

public partial class AdminTimeTable : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            Generate_Menu();
            FindUserCompany();
            FillTodaysLectureCount();
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Request.Cookies["MyCookiesLoginInfo"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {

                FindUserMessages();
            }

            

            
        }
        catch (Exception)
        {
            
            throw;
        }
        

        
    }

    protected void btnShortCut_TestSchedule_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_TestSchedule.aspx");
    }

    protected void btnShortCut_TestAttendance_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_Testattendance.aspx");
    }

    protected void btnShortCut_TestAnswerPaper_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_Testanswerpapers.aspx");
    }

    protected void btnShortCut_TestMarks_ServerClick(object sender, System.EventArgs e)
    {
        Response.Redirect("Tran_Testmarks.aspx");
    }


    private void FillTodaysLectureCount()
    {
        try
        {
            //lblHeader_User_Code
            DataSet dsGrid = ProductController.Get_TodaysLecture_Schedule("2", lblHeader_User_Code.Text);
            lblHeader_Notification_TodaysLecture.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["TodaysLecture"]);
            lblHeader_Notification_Attendance.Text = Convert.ToString(dsGrid.Tables[1].Rows[0]["AttendanceAuthorisation"]);
            lblHeader_Notification_CancelledLecture.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Cancelled_Lecture"]);
            lblHeader_Notification_PendingApprovalLecture.Text = Convert.ToString(dsGrid.Tables[3].Rows[0]["Pending_Approval_Lecture"]);
            lblHeader_Notification_RejectedLecture.Text = Convert.ToString(dsGrid.Tables[4].Rows[0]["Rejected_Lecture"]);

        }
        catch 
        {
            return;
        }
    }


    private void FindUserCompany()
    {
        try
        {


            // HttpCookie cookie = Request.Cookies.Get("UserInfo");
            string UserId = null;
            string UserName = null;
            string DBName = null;
            string UserTypeCode = null;

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                UserId = Request.Cookies["MyCookiesLoginInfo"]["UserID"];
                UserName = Request.Cookies["MyCookiesLoginInfo"]["UserName"];
                DBName = Request.Cookies["MyCookiesLoginInfo"]["DBName"];
                UserTypeCode = Request.Cookies["MyCookiesLoginInfo"]["UserTypeCode"];
                string role = Request.Cookies["MyCookiesLoginInfo"]["Role"];
                lblHeader_User_Name.Text = UserName;
                lblHeader_User_Code.Text = UserId;
                lblHeader_DBName.Text = DBName;

                DataSet dsUserCompany = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserId, "", "", "", "1", DBName);
                lblHeader_Company_Count.Text = Convert.ToString(dsUserCompany.Tables[0].Rows.Count);


                if (dsUserCompany.Tables[0].Rows.Count == 1)
                {
                    lblHeader_Company_Count_String.Text = "1 Company is assigned to you";
                }
                else
                {
                    lblHeader_Company_Count_String.Text = dsUserCompany.Tables[0].Rows.Count + " Companies are assigned to you";
                }

                if (dsUserCompany.Tables[0].Rows.Count > 0)
                {
                    lblHeader_Company_Name.Text = Convert.ToString(dsUserCompany.Tables[0].Rows[0]["Company_Name"]);
                    lblHeader_Company_Code.Text = Convert.ToString(dsUserCompany.Tables[0].Rows[0]["Company_code"]);
                }
                else
                {
                    lblHeader_Company_Name.Text = "Not assigned";
                    lblHeader_Company_Code.Text = "";
                }

                //Admin module


                //if (role == "R037")
                //{
                ////     if (role == "R032") //j@yesh
                ////if (role == "R026")
                ////{
                    

                //    btnShortCut_TestSchedule.Disabled = true;
                //    btnShortCut_TestAttendance.Disabled = true;
                //    btnShortCut_TestAnswerPaper.Disabled = true;
                //    btnShortCut_TestMarks.Disabled = true;

                //    //Admin module
                //    Menu_A_Master.Visible = false;
                //    Menu_A_YearDistributionsheet.Visible = false;
                //    Menu_A_PlanforSchedulingDates.Visible = false;
                //    Menu_A_PlanforSchedulingHorizon.Visible = false;
                //    Menu_A_Reports.Visible = true;
                //    Menu_A_Approval.Visible =true;
                //    Menu_A_LectureSchedule.Visible = true;


                //    //User module
                //    Menu_U_AssignStudentsToBatches.Visible = false;
                //    Menu_U_LectureClosure.Visible = false;
                //    Menu_U_ManageAttendance.Visible = false;
                //    Menu_U_ManageTimeTable.Visible = false;
                //    Menu_U_ManualAdjustment.Visible = false;
                //    Menu_U_MonthlyClosing.Visible = false;

                //    lnkLectureSchedule.HRef = "LectureScheduleBulkEntry.aspx";


                //}
                //else 
                //{
                //    //Admin module
                //    Menu_A_Master.Visible = false;
                //    Menu_A_YearDistributionsheet.Visible = false;
                //    Menu_A_PlanforSchedulingDates.Visible = false;
                //    Menu_A_PlanforSchedulingHorizon.Visible = false;
                //    Menu_A_Reports.Visible = true;
                //    Menu_A_Approval.Visible = false;
                //    Menu_A_LectureSchedule.Visible = true;

                //    //User module
                //    Menu_U_AssignStudentsToBatches.Visible = false;
                //    Menu_U_LectureClosure.Visible = false;
                //    Menu_U_ManageAttendance.Visible = true;
                //    Menu_U_ManageTimeTable.Visible = false;
                //    Menu_U_ManualAdjustment.Visible = false;
                //    Menu_U_MonthlyClosing.Visible = false;

                //    lnkLectureSchedule.HRef = "LecturescheduleDecentralized.aspx";
                //}
                

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {

            
        }

    }

    

    private void FindUserMessages()
    {
        lnk_Message.Visible = false;
    }

   

    protected void BtnLogOut_Click(object sender, System.EventArgs e)
    {
        Response.Cookies["MyCookiesLoginInfo"].Expires.TimeOfDay.ToString();        
        Session.RemoveAll();
        Response.Redirect("Login.aspx", false);
        
    }


    private void Generate_Menu()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        //Login_Service.LoginServiceSoapClient client = new Login_Service.LoginServiceSoapClient();
        if (Request.Cookies["MyCookiesLoginInfo"] != null)
        {
            string defaultpage = cookie.Values["Default_page"];
           // DataSet dtApplicatUrl = ProductController.GetApplication_Url();
            ////dtApplicatUrl = client.GetApplication_Url();
            ////dtApplicatUrl = ProductController.GetApplication_Url();
            //lblPath1.Text = dtApplicatUrl.Tables[0].Rows[0]["Homepage_Path"].ToString();
            //lblPath2.Text = dtApplicatUrl.Tables[0].Rows[1]["Homepage_Path"].ToString();
            //lblPath3.Text = dtApplicatUrl.Tables[0].Rows[2]["Homepage_Path"].ToString();
            //lblPath4.Text = dtApplicatUrl.Tables[0].Rows[3]["Homepage_Path"].ToString();
            //lblPath5.Text = dtApplicatUrl.Tables[0].Rows[4]["Homepage_Path"].ToString();
            ////lblPath6.Text = dtApplicatUrl.Rows[5]["Homepage_Path"].ToString();

            string Userid = cookie.Values["UserID"];
            string lstr = "";
            lstr = Convert.ToString(("<ul class='nav nav-list'>"));
            //DataTable dt = client.GetMenuList("1", Userid, "");
            DataSet ds = ProductController.GetMenuList("1", Userid, "");
           // lstr += Convert.ToString(("<li> <a href=' " + defaultpage + "'><i class='icon-home'></i><span>Dashboard</span></a></li>"));
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                string Application_no = Convert.ToString(ds.Tables[0].Rows[i]["Application_No"]);
                if (Application_no == "DB02")
                {
                    lstr += Convert.ToString(("<li> <a href=' " + ds.Tables[0].Rows[i]["Menu_link"] + "' class='" + ds.Tables[0].Rows[i]["Toggle"] + "'><i class='" + ds.Tables[0].Rows[i]["Menu_CSS"] + "'></i><span>"));
                    //lstr += Convert.ToString(("<li> <a href=' " + ds.Tables[0].Rows[i]["Menu_link"] + "'><i class='" + ds.Tables[0].Rows[i]["Menu_CSS"] + "'></i><span>"));
                    //lstr += Convert.ToString(("<li class=''> <a href='#' class='dropdown-toggle'><i class='" + dt.Rows[i]["Menu_CSS"] + "'></i><span>"));
                    lstr += (Convert.ToString(ds.Tables[0].Rows[i]["Menu_Name"]));
                    //DataTable dt1 = client.GetMenuList("2", Userid, ds.Tables[0].Rows.[i]["Menu_Code"].ToString());
                    DataSet ds1 = ProductController.GetMenuList("2", Userid, ds.Tables[0].Rows[i]["Menu_Code"].ToString());
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lstr += Convert.ToString(("</span><b class='arrow icon-angle-down'></b>"));
                        lstr += Convert.ToString(("</a><ul class='submenu'>"));
                        for (int j = 0; j <= ds1.Tables[0].Rows.Count - 1; j++)
                        {
                            lstr += Convert.ToString((((" <li><a href='") + ds1.Tables[0].Rows[j]["Menu_Link"] + "'><i></i>") + ds1.Tables[0].Rows[j]["Menu_Name"] + "</a>"));
                        }
                        lstr += Convert.ToString(("</ul></li>"));
                    }
                    lstr += Convert.ToString(("</span></a></li>"));
                    lblHeaderMenu.Text = lstr;
                }
            }
            lstr += Convert.ToString(("</ul>"));

        }

    }
 

    //private void Generate_Menu()
    //{
    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    Login_Service.LoginServiceSoapClient client = new Login_Service.LoginServiceSoapClient();
    //    if (Request.Cookies["MyCookiesLoginInfo"] != null)
    //    {
    //        string Userid = cookie.Values["UserID"];
    //        string lstr = "";
    //        lstr = Convert.ToString(("<ul class='nav nav-list'>"));
    //        DataTable dt = client.GetMenuList("1", Userid, "");


    //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
    //        {
    //            string Application_no = Convert.ToString(dt.Rows[i]["Application_No"]);
    //            if (Application_no == "DB02")
    //            {
    //                lstr += Convert.ToString(("<li> <a href=' " + dt.Rows[i]["Menu_link"] + "'><i class='" + dt.Rows[i]["Menu_CSS"] + "'></i><span>"));
    //                //lstr += Convert.ToString(("<li class=''> <a href='#' class='dropdown-toggle'><i class='" + dt.Rows[i]["Menu_CSS"] + "'></i><span>"));
    //                lstr += (Convert.ToString(dt.Rows[i]["Menu_Name"]));
    //                DataTable dt1 = client.GetMenuList("2", Userid, dt.Rows[i]["Menu_Code"].ToString());
    //                if (dt1.Rows.Count > 0)
    //                {
    //                    lstr += Convert.ToString(("</span><b class='arrow icon-angle-down'></b>"));
    //                    lstr += Convert.ToString(("</a><ul class='submenu'>"));
    //                    for (int j = 0; j <= dt1.Rows.Count - 1; j++)
    //                    {
    //                        lstr += Convert.ToString((((" <li><a href='") + dt1.Rows[j]["Menu_Link"] + "'><i></i>") + dt1.Rows[j]["Menu_Name"] + "</a>"));
    //                    }
    //                    lstr += Convert.ToString(("</ul></li>"));
    //                }
    //                lstr += Convert.ToString(("</span></a></li>"));
    //                lblHeaderMenu.Text = lstr;
    //            }
    //        }
    //        lstr += Convert.ToString(("</ul>"));

    //    }

    //}
    
}

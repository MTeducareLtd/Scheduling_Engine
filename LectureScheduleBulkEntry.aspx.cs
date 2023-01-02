using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

/// <summary>
/// Created By - Digambar
/// Date - 21 May 2015
/// </summary>
public partial class LectureScheduleBulkEntry : System.Web.UI.Page
{

    #region Events

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_LectureType();
        }
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid_LectureSchedule();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlBatch.Items.Clear();
        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            ddlCentre.Items.Clear();
            ddlLMSnonLMSProdct.Items.Clear();
            ddlDivision.Focus();
            return;
        }
        FillDDL_Centre();
        FillDDL_Course();
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlAcademicYear.Focus();
            return;
        }
        if (ddlCourse.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlCourse.Focus();
            return;
        }
        FillDDL_LMSNONLMSProduct();
        FillDDL_Batch();

    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlBatch.Items.Clear();
        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlDivision.Focus();
            return;
        }
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlAcademicYear.Focus();
            return;
        }
        if (ddlCourse.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlCourse.Focus();
            return;
        }
        FillDDL_LMSNONLMSProduct();
        FillDDL_Batch();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Subject();
        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlDivision.Focus();
            return;
        }
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlAcademicYear.Focus();
            return;
        }
        if (ddlCourse.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlCourse.Focus();
            return;
        }
        FillDDL_LMSNONLMSProduct();
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }

    protected void ddlLMSnonLMSProdct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }


    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlLectureType.SelectedIndex = 0;
        ddlCentre.Items.Clear();
        ddlLMSnonLMSProdct.Items.Clear();
        ddlCourse.Items.Clear();
        ddlBatch.Items.Clear();
        ddlSubject.Items.Clear();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box1();

        int SaveRecord = 0, TotalRecord = 0;
        lblSaveError.Visible = false;
        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");

            if (chkCheck.Checked == true)
            {
                TotalRecord = TotalRecord + 1;
            }
        }
        if (TotalRecord == 0)
        {
            //lblSaveError.Visible = true;
            //lblSaveError.ForeColor = System.Drawing.Color.Red;
            //lblSaveError.Text = "You have not Selected any Record";

            Show_Error_Success_Box1("E", "You have not Selected any Record");
            return;
        }

        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");

            if (chkCheck.Checked == true)
            {
                int error = 0;
                TextBox txtDLTeacherName = (TextBox)dtlItem.FindControl("txtDLTeacherName");
                Label lblLectScheduleId = (Label)dtlItem.FindControl("lblLectScheduleId");

                System.Web.UI.HtmlControls.HtmlInputText txtLectureDate = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("txtLectureDate");
                System.Web.UI.HtmlControls.HtmlInputText timepicker_From = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("timepicker_From");
                System.Web.UI.HtmlControls.HtmlInputText timepicker_To = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("timepicker_To");

                Label lblSuccess = (Label)dtlItem.FindControl("lblSuccess");
                lblSuccess.Visible = false;

                HtmlAnchor lbl_DLError = (HtmlAnchor)dtlItem.FindControl("lbl_DLError");
                Panel icon_Error = (Panel)dtlItem.FindControl("icon_Error");
                //Check To time validation
                string Time3 = timepicker_To.Value.Trim();
                string Time2 = "";
                if ((Time3 != "") && (Time3.Length == 8))
                {
                    Time2 = Time3.Substring(6, 2);
                    if ((Time2 == "am") || (Time2 == "AM") || (Time2 == "pm") || (Time2 == "PM"))
                    {
                        Time2 = Time3.Substring(2, 1);
                        if (Time2 != ":")
                        {
                            lbl_DLError.Title = "Lecture End time not entered correct format(hh:mm am/pm)";
                            icon_Error.Visible = true;
                            error = 1;
                        }
                        else if (Time3.Length != 8)
                        {
                            lbl_DLError.Title = "Lecture End time not entered correct format(hh:mm am/pm)";
                            icon_Error.Visible = true;
                            error = 1;
                        }
                        else
                        {
                            try
                            {
                                Time2 = Time3.Substring(0, 2);
                                int ToHourminute = Convert.ToInt32(Time2);
                                Time2 = Time3.Substring(3, 2);
                                ToHourminute = Convert.ToInt32(Time2);

                            }
                            catch
                            {
                                lbl_DLError.Title = "Enter Lecture End time Hour or Minute is numeric";
                                icon_Error.Visible = true;
                                error = 1;
                            }
                        }
                    }
                    else
                    {
                        lbl_DLError.Title = "Lecture End time not entered correct format(hh:mmam/pm)";
                        icon_Error.Visible = true;
                        error = 1;
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(timepicker_To.Value.Trim()))
                    {
                        lbl_DLError.Title = "Enter Lecture End time";
                        icon_Error.Visible = true;
                        error = 1;
                    }


                }
                //Check From time validation
                string Time1 = timepicker_From.Value.Trim();
                string Time = "";
                if ((Time1 != "") && (Time1.Length == 8))
                {
                    Time = Time1.Substring(6, 2);
                    if ((Time == "am") || (Time == "AM") || (Time == "pm") || (Time == "PM"))
                    {
                        Time = Time1.Substring(2, 1);
                        if (Time != ":")
                        {
                            lbl_DLError.Title = "Lecture Start time not entered correct format(hh:mmam/pm)";
                            icon_Error.Visible = true;
                            error = 1;
                        }
                    }
                    else
                    {
                        lbl_DLError.Title = "Lecture Start time not entered correct format(hh:mm am/pm)";
                        icon_Error.Visible = true;
                        error = 1;
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(timepicker_From.Value.Trim()))
                    {
                        lbl_DLError.Title = "Enter Lecture Start time";
                        icon_Error.Visible = true;
                        error = 1;
                    }
                    else if (Time1.Length != 8)
                    {
                        lbl_DLError.Title = "Lecture Start time not entered correct format(hh:mm am/pm)";
                        icon_Error.Visible = true;
                        error = 1;
                    }
                    else
                    {
                        try
                        {
                            Time = Time1.Substring(0, 2);
                            int FromHourminute = Convert.ToInt32(Time);
                            Time = Time1.Substring(3, 3);
                            FromHourminute = Convert.ToInt32(Time);

                        }
                        catch
                        {
                            lbl_DLError.Title = "Enter Lecture Start time Hour or Minute is numeric";
                            icon_Error.Visible = true;
                            error = 1;
                        }
                    }

                }
                //Check LectureDate Validtion
                string str1 = txtLectureDate.Value.Trim();
                string str = "";

                if ((str1 != "") && (str1.Length == 10))
                {
                    str = str1.Substring(5, 1);
                    if (str != "-")
                    {
                        lbl_DLError.Title = "Lecture Date  not entered correct format(dd-mm-yyyy)";
                        icon_Error.Visible = true;
                        error = 1;
                    }
                    else
                    {
                        str = str1.Substring(2, 1);
                        if (str != "-")
                        {
                            lbl_DLError.Title = "Lecture Date  not entered correct format(dd-mm-yyyy)";
                            icon_Error.Visible = true;
                            error = 1;
                        }
                        else
                        {
                            try
                            {
                                str = str1.Substring(0, 2);
                                int datemonthYear = Convert.ToInt32(str);
                                str = str1.Substring(3, 2);
                                datemonthYear = Convert.ToInt32(str);
                                str = str1.Substring(6, 4);
                                datemonthYear = Convert.ToInt32(str);
                            }
                            catch
                            {
                                lbl_DLError.Title = "Enter Lecture Date,Month or Year is numeric";
                                icon_Error.Visible = true;
                                error = 1;
                            }
                        }
                    }
                }
                else
                {
                    lbl_DLError.Title = "Lecture Date  not entered correct format(dd-mm-yyyy)";
                    icon_Error.Visible = true;
                    error = 1;
                }

                if (string.IsNullOrEmpty(txtLectureDate.Value.Trim()))
                {
                    lbl_DLError.Title = "Enter Lecture Date";
                    icon_Error.Visible = true;
                    error = 1;
                }

                if (string.IsNullOrEmpty(txtDLTeacherName.Text.Trim()))
                {
                    lbl_DLError.Title = "Enter Teacher Short Name";
                    icon_Error.Visible = true;
                    error = 1;
                }

                if (error == 1)
                {
                    icon_Error.Visible = true;
                }

                if (error == 0)  //if error not come then save
                {
                    int ResultId = 0;
                    Label lblSubjectCode = (Label)dtlItem.FindControl("lblSubjectCode");
                    Label lblChapterCode = (Label)dtlItem.FindControl("lblChapterCode");
                    Label lblLessonPlanCode = (Label)dtlItem.FindControl("lblLessonPlanCode");
                    Label lblHeader_User_Code = default(Label);
                    lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                    string CreatedBy = null;
                    CreatedBy = lblHeader_User_Code.Text;

                    string FromTime = "";
                    FromTime = timepicker_From.Value;
                    FromTime = FromTime.Substring(0, 5) + ":00 " + FromTime.Substring(6, 2);

                    string ToTime = "";
                    ToTime = timepicker_To.Value;
                    ToTime = ToTime.Substring(0, 5) + ":00 " + ToTime.Substring(6, 2);

                    string LectureDate = "", Month = "";
                    Month = txtLectureDate.Value;
                    Month = Month.Substring(3, 2);
                    switch (Month)
                    {
                        case "01":
                            Month = "Jan";
                            break;
                        case "02":
                            Month = "Feb";
                            break;
                        case "03":
                            Month = "Mar";
                            break;
                        case "04":
                            Month = "Apr";
                            break;
                        case "05":
                            Month = "May";
                            break;
                        case "06":
                            Month = "Jun";
                            break;
                        case "07":
                            Month = "Jul";
                            break;
                        case "08":
                            Month = "Aug";
                            break;
                        case "09":
                            Month = "Sep";
                            break;
                        case "10":
                            Month = "Oct";
                            break;
                        case "11":
                            Month = "Nov";
                            break;
                        case "12":
                            Month = "Dec";
                            break;
                        default:
                            break;
                    }
                    //LectureDate = LectureDate.Substring(6, 4) + "-" + LectureDate.Substring(3, 2) + "-" + LectureDate.Substring(0, 2);
                    LectureDate = txtLectureDate.Value;
                    LectureDate = LectureDate.Substring(0, 2) + " " + Month + " " + LectureDate.Substring(6, 4);

                    if (lblLectScheduleId.Text == "NEW")
                    {
                        ResultId = ProductController.InsertUpdateLectureShedule(lblLectScheduleId.Text, ddlDivision.SelectedValue, ddlAcademicYear.SelectedItem.ToString(), ddlCourse.SelectedValue, ddlLMSnonLMSProdct.SelectedValue, ddlCentre.SelectedValue, ddlBatch.SelectedValue, LectureDate, ddlLectureType.SelectedValue, lblSubjectCode.Text, lblChapterCode.Text, FromTime, ToTime, lblLessonPlanCode.Text, txtDLTeacherName.Text, CreatedBy, "6", "", "", "", "", "");
                    }
                    else
                        ResultId = ProductController.InsertUpdateLectureShedule(lblLectScheduleId.Text, ddlDivision.SelectedValue, ddlAcademicYear.SelectedItem.ToString(), ddlCourse.SelectedValue, ddlLMSnonLMSProdct.SelectedValue, ddlCentre.SelectedValue, ddlBatch.SelectedValue, LectureDate, ddlLectureType.SelectedValue, lblSubjectCode.Text, lblChapterCode.Text, FromTime, ToTime, lblLessonPlanCode.Text, txtDLTeacherName.Text, CreatedBy, "7", "", "", "", "", "");

                    if (ResultId == -1)
                    {
                        lbl_DLError.Title = "Record already Exist";
                        icon_Error.Visible = true;
                    }
                    else if (ResultId == -2)
                    {
                        lbl_DLError.Title = "Teacher short name not found";
                        icon_Error.Visible = true;
                    }
                    else if (ResultId == -3)
                    {
                        lbl_DLError.Title = "Teacher not allocated";
                        icon_Error.Visible = true;
                    }
                    else if (ResultId == -4)
                    {
                        lbl_DLError.Title = "This lecture time is not available for this teacher ";
                        icon_Error.Visible = true;
                    }
                    else if (ResultId == -5)
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "Travel Time is insufficient.";
                        UpdatePanelMsgBox.Update();
                        return;
                    }
                    else  //record is saved 
                    {
                        SaveRecord = SaveRecord + 1;
                        icon_Error.Visible = false;
                        lblSuccess.Visible = true;

                        chkCheck.Checked = false;
                        lblLectScheduleId.Text = Convert.ToString(ResultId);
                        Send_Details_LMS(ResultId.ToString());
                        //TextBox txtDLTeacherName = (TextBox)dtlItem.FindControl("txtDLTeacherName");
                        //System.Web.UI.HtmlControls.HtmlInputText txtLectureDate = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("txtLectureDate");
                        //System.Web.UI.HtmlControls.HtmlInputText timepicker_From = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("timepicker_From");
                        //System.Web.UI.HtmlControls.HtmlInputText timepicker_To = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("timepicker_To");
                        HtmlGenericControl spanFromTime = (HtmlGenericControl)dtlItem.FindControl("spanFromTime");
                        HtmlGenericControl spanToTime = (HtmlGenericControl)dtlItem.FindControl("spanToTime");

                        Label lblTeacherName = (Label)dtlItem.FindControl("lblTeacherName");
                        Label lblLectDate = (Label)dtlItem.FindControl("lblLectDate");
                        Label lblFromTime = (Label)dtlItem.FindControl("lblFromTime");
                        Label lblToTime = (Label)dtlItem.FindControl("lblToTime");

                        lblTeacherName.Text = txtDLTeacherName.Text;
                        lblLectDate.Text = txtLectureDate.Value;
                        lblFromTime.Text = timepicker_From.Value;
                        lblToTime.Text = timepicker_To.Value;

                        lblTeacherName.Visible = true;
                        lblLectDate.Visible = true;
                        lblFromTime.Visible = true;
                        lblToTime.Visible = true;

                        txtDLTeacherName.Visible = false;
                        txtLectureDate.Visible = false;
                        timepicker_From.Visible = false;
                        timepicker_To.Visible = false;
                        spanFromTime.Visible = false;
                        spanToTime.Visible = false;
                        //txtDLTeacherName.Text = "";
                        //txtLectureDate.Value = "";
                        //timepicker_From.Value = "";
                        //timepicker_To.Value = "";                        
                    }
                }

            }
        }

        lblSaveError.Visible = true;
        if (SaveRecord == TotalRecord)
        {
            //lblSaveError.ForeColor = System.Drawing.Color.Green;
            //lblSaveError.Text = " Records Saved Successfully";
            Show_Error_Success_Box1("S", "Records Saved Successfully");
            return;
        }
        else
        {

            Show_Error_Success_Box1("E", Convert.ToString(SaveRecord) + " Records Saved Successfully out of " + Convert.ToString(TotalRecord) + " Records.Check Individual Record for More Details.");
            return;
            //lblSaveError.ForeColor = System.Drawing.Color.Red;
            //lblSaveError.Text = Convert.ToString(SaveRecord) + " Records Saved Successfully out of " + Convert.ToString(TotalRecord) + " Records.Check Indivisual record for more details.";
        }


    }

    class lecturedetailsinsert
    {
        public string LectureGroupCode { get; set; }
        public string LectureDetailsCode { get; set; }
        public string CourseCode { get; set; }
        public string SubjectCode { get; set; }
        public string ChapterCode { get; set; }
        public string TopicCode { get; set; }
        public string SubTopicCode { get; set; }
        public string ModuleCode { get; set; }
        public string LessonPlanCode { get; set; }
        public string ProductCode { get; set; }
        public string CenterCode { get; set; }
        public List<string> BatchProductCode { get; set; }
        public string TeacherCode { get; set; }
        public string ClassRoomCode { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public Boolean IsRescheduled { get; set; }
        public DateTime RescheduledDate { get; set; }
        public Boolean IsCancelled { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }

        public Boolean IsDeleted { get; set; }


    }


    private void Send_Details_LMS(string Lecture_Schedule_Id)
    {
        DataSet dsdetails = ProductController.GET_LECTURE_DETAILS(Lecture_Schedule_Id);
        if (dsdetails.Tables[0].Rows.Count > 0)
        {


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ProductController.get_TeacherAttendance_LMSApiLink());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var lecturedetailsinsert = new lecturedetailsinsert();
            lecturedetailsinsert.LectureGroupCode = dsdetails.Tables[0].Rows[0]["LectureSchedule_Id"].ToString();
            lecturedetailsinsert.LectureDetailsCode = dsdetails.Tables[0].Rows[0]["LectureDetailsCode"].ToString();
            lecturedetailsinsert.CourseCode = dsdetails.Tables[0].Rows[0]["Stream_code"].ToString();
            lecturedetailsinsert.SubjectCode = dsdetails.Tables[0].Rows[0]["Subject_Code"].ToString();
            lecturedetailsinsert.ChapterCode = dsdetails.Tables[0].Rows[0]["Chapter_Code"].ToString();
            lecturedetailsinsert.TopicCode = dsdetails.Tables[0].Rows[0]["TopicCode"].ToString();
            lecturedetailsinsert.SubTopicCode = dsdetails.Tables[0].Rows[0]["SubTopicCode"].ToString();
            lecturedetailsinsert.ModuleCode = dsdetails.Tables[0].Rows[0]["ModuleCode"].ToString();
            lecturedetailsinsert.LessonPlanCode = dsdetails.Tables[0].Rows[0]["LessonPlanCode"].ToString();
            lecturedetailsinsert.ProductCode = dsdetails.Tables[0].Rows[0]["ProductCode"].ToString();
            lecturedetailsinsert.CenterCode = dsdetails.Tables[0].Rows[0]["Centre_Code"].ToString();

            lecturedetailsinsert.TeacherCode = dsdetails.Tables[0].Rows[0]["Partner_Code"].ToString();
            lecturedetailsinsert.ClassRoomCode = dsdetails.Tables[0].Rows[0]["Classroom_Code"].ToString();
            lecturedetailsinsert.StartDateTime = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["StartDatetime"]);
            lecturedetailsinsert.EndDateTime = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["EndDateTime"]);
            lecturedetailsinsert.IsRescheduled = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsRescheduled"]);
            lecturedetailsinsert.RescheduledDate = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["RescheduledDate"]);
            lecturedetailsinsert.IsCancelled = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsCancelled"]);
            lecturedetailsinsert.CancelledDate = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["CancelledDate"]);
            lecturedetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["CreatedOn"]);
            lecturedetailsinsert.CreatedBy = dsdetails.Tables[0].Rows[0]["Created_By"].ToString();
            lecturedetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["AlteredOn"]);
            lecturedetailsinsert.ModifiedBy = dsdetails.Tables[0].Rows[0]["Altered_By"].ToString();
            lecturedetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsActive"]);
            lecturedetailsinsert.IsDeleted = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsDeleted"]);



            lecturedetailsinsert.BatchProductCode = new List<string>();
            for (int cnt = 0; cnt <= dsdetails.Tables[1].Rows.Count - 1; cnt++)
            {
                lecturedetailsinsert.BatchProductCode.Add(dsdetails.Tables[1].Rows[cnt]["Batch_Code"].ToString());

            }
            var response = client.PostAsJsonAsync("lecture/addUpdLectureGroupOpt", lecturedetailsinsert).Result;
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            if (response.StatusCode.ToString() == "OK")
            {
                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, 1, Lecture_Schedule_Id, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }
            else
            {
                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Lecture_Schedule_Id, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }


        }

    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        //int rowcount=

        try
        {
            CheckBox s = sender as CheckBox;

            //Set checked status of hidden check box to items in grid
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");

                chkitemck.Checked = s.Checked;

                TextBox txtDLTeacherName = (TextBox)dtlItem.FindControl("txtDLTeacherName");
                System.Web.UI.HtmlControls.HtmlInputText txtLectureDate = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("txtLectureDate");
                System.Web.UI.HtmlControls.HtmlInputText timepicker_From = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("timepicker_From");
                System.Web.UI.HtmlControls.HtmlInputText timepicker_To = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("timepicker_To");
                HtmlGenericControl spanFromTime = (HtmlGenericControl)dtlItem.FindControl("spanFromTime");
                HtmlGenericControl spanToTime = (HtmlGenericControl)dtlItem.FindControl("spanToTime");

                Label lblTeacherName = (Label)dtlItem.FindControl("lblTeacherName");
                Label lblLectDate = (Label)dtlItem.FindControl("lblLectDate");
                Label lblFromTime = (Label)dtlItem.FindControl("lblFromTime");
                Label lblToTime = (Label)dtlItem.FindControl("lblToTime");

                if (s.Checked == false)
                {
                    HtmlAnchor lbl_DLError = (HtmlAnchor)dtlItem.FindControl("lbl_DLError");
                    Panel icon_Error = (Panel)dtlItem.FindControl("icon_Error");

                    txtDLTeacherName.Visible = false;
                    txtLectureDate.Visible = false;
                    timepicker_From.Visible = false;
                    timepicker_To.Visible = false;
                    spanFromTime.Visible = false;
                    spanToTime.Visible = false;
                    icon_Error.Visible = false;

                    lblTeacherName.Visible = true;
                    lblLectDate.Visible = true;
                    lblFromTime.Visible = true;
                    lblToTime.Visible = true;

                    txtDLTeacherName.Text = lblTeacherName.Text;
                    txtLectureDate.Value = lblLectDate.Text;
                    timepicker_From.Value = lblFromTime.Text;
                    timepicker_To.Value = lblToTime.Text;

                }
                else
                {
                    txtDLTeacherName.Visible = true;
                    txtLectureDate.Visible = true;
                    timepicker_From.Visible = true;
                    timepicker_To.Visible = true;
                    spanFromTime.Visible = true;
                    spanToTime.Visible = true;
                    lblTeacherName.Visible = false;
                    lblLectDate.Visible = false;
                    lblFromTime.Visible = false;
                    lblToTime.Visible = false;
                }

            }
            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void chkCheck_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox s = sender as CheckBox;
            dynamic row = (DataListItem)s.NamingContainer;

            TextBox txtDLTeacherName = row.FindControl("txtDLTeacherName");
            System.Web.UI.HtmlControls.HtmlInputText txtLectureDate = (System.Web.UI.HtmlControls.HtmlInputText)row.FindControl("txtLectureDate");
            System.Web.UI.HtmlControls.HtmlInputText timepicker_From = (System.Web.UI.HtmlControls.HtmlInputText)row.FindControl("timepicker_From");
            System.Web.UI.HtmlControls.HtmlInputText timepicker_To = (System.Web.UI.HtmlControls.HtmlInputText)row.FindControl("timepicker_To");
            HtmlGenericControl spanFromTime = (HtmlGenericControl)row.FindControl("spanFromTime");
            HtmlGenericControl spanToTime = (HtmlGenericControl)row.FindControl("spanToTime");

            Label lblTeacherName = row.FindControl("lblTeacherName");
            Label lblLectDate = row.FindControl("lblLectDate");
            Label lblFromTime = row.FindControl("lblFromTime");
            Label lblToTime = row.FindControl("lblToTime");

            if (s.Checked == false)
            {
                HtmlAnchor lbl_DLError = (HtmlAnchor)row.FindControl("lbl_DLError");
                Panel icon_Error = (Panel)row.FindControl("icon_Error");

                txtDLTeacherName.Visible = false;
                txtLectureDate.Visible = false;
                timepicker_From.Visible = false;
                timepicker_To.Visible = false;
                spanFromTime.Visible = false;
                spanToTime.Visible = false;
                icon_Error.Visible = false;

                lblTeacherName.Visible = true;
                lblLectDate.Visible = true;
                lblFromTime.Visible = true;
                lblToTime.Visible = true;

                txtDLTeacherName.Text = lblTeacherName.Text;
                txtLectureDate.Value = lblLectDate.Text;
                timepicker_From.Value = lblFromTime.Text;
                timepicker_To.Value = lblToTime.Text;
            }
            else
            {
                if (txtDLTeacherName.Text == "")
                    txtDLTeacherName.Enabled = true;
                else
                    txtDLTeacherName.Enabled = false;
                txtDLTeacherName.Visible = true;
                txtLectureDate.Visible = true;
                timepicker_From.Visible = true;
                timepicker_To.Visible = true;
                spanFromTime.Visible = true;
                spanToTime.Visible = true;

                lblTeacherName.Visible = false;
                lblLectDate.Visible = false;
                lblFromTime.Visible = false;
                lblToTime.Visible = false;
            }

            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }


    }

    #endregion

    #region Methods

    /// <summary>
    /// Visible panel base on Mode
    /// </summary>
    /// <param name="Mode">Mode</param>
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            divLectureWarning.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            divLectureWarning.Visible = true;
        }
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
    }

    /// <summary>
    /// Clear Error Success Box
    /// </summary>
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    private void Clear_Error_Success_Box1()
    {
        Msg_Error1.Visible = false;
        Msg_Success1.Visible = false;
        lblSuccess1.Text = "";
        lblerror1.Text = "";

    }


    /// <summary>
    /// Show Error or success box on top base on boxtype and Error code
    /// </summary>
    /// <param name="BoxType">BoxType</param>
    /// <param name="Error_Code">Error_Code</param>
    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
        else
        {
            Msg_Success.Visible = true;
            Msg_Error.Visible = false;
            lblSuccess.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
    }


    /// <summary>
    /// Show Error or success box on top base on boxtype and Error code
    /// </summary>
    /// <param name="BoxType">BoxType</param>
    /// <param name="Error_Code">Error_Code</param>
    private void Show_Error_Success_Box1(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error1.Visible = true;
            Msg_Success1.Visible = false;
            lblerror1.Text = ProductController.Raise_Error(Error_Code);

        }
        else
        {
            Msg_Success1.Visible = true;
            Msg_Error1.Visible = false;
            lblSuccess1.Text = ProductController.Raise_Error(Error_Code);

        }
    }




    /// <summary>
    /// Fill List box and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    /// <summary>
    /// Fill Division drop down list
    /// </summary>
    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
                Response.Redirect("Login.aspx");

            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
            BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    /// <summary>
    /// Fill Academic Year dropdown
    /// </summary>
    private void FillDDL_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
            ddlAcademicYear.Items.Insert(0, "Select");
            ddlAcademicYear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    /// <summary>
    /// Fill Course dropdownlist 
    /// </summary>
    private void FillDDL_Course()
    {

        try
        {

            Clear_Error_Success_Box();
            ddlCourse.Items.Clear();
            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlDivision.Focus();
                return;
            }
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourse.Items.Insert(0, "Select");
            ddlCourse.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    /// <summary>
    /// Fill Subject dropdown
    /// </summary>
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlCourse.SelectedValue;
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlSubject, dsSubject, "Subject_ShortName", "Subject_Code");
            ddlSubject.Items.Insert(0, "Select");
            ddlSubject.SelectedIndex = 0;

        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    /// Fill Centers Based on login user 
    /// </summary>
    private void FillDDL_Centre()
    {
        try
        {
            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);
            BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
            ddlCentre.Items.Insert(0, "Select");
            ddlCentre.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    private void FillDDL_LMSNONLMSProduct()
    {
        try
        {
            ddlLMSnonLMSProdct.Items.Clear();

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string CourseCode = null;
            CourseCode = ddlCourse.SelectedValue;

            DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course(Div_Code, YearName, CourseCode);
            BindDDL(ddlLMSnonLMSProdct, dsLMS, "ProductName", "ProductCode");
            ddlLMSnonLMSProdct.Items.Insert(0, "Select");
            ddlLMSnonLMSProdct.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    private void FillDDL_Batch()
    {
        try
        {
            ddlBatch.Items.Clear();
            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlDivision.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {
                ddlAcademicYear.Focus();
                return;
            }

            if (ddlLMSnonLMSProdct.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProdct.Focus();
                return;
            }
            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                ddlCentre.Focus();
                return;
            }

            if (ddlLMSnonLMSProdct.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProdct.Focus();
                return;
            }
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;


            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string ProductCode = null;
            ProductCode = ddlLMSnonLMSProdct.SelectedValue;

            string CentreCode = null;
            CentreCode = ddlCentre.SelectedValue;

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, CentreCode, "1");
            BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
            ddlBatch.Items.Insert(0, "Select");
            ddlBatch.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    /// <summary>
    /// Fill LectureType dropdown list
    /// </summary>
    private void FillDDL_LectureType()
    {
        try
        {
            DataSet dsLectureType = ProductController.GetActivityType();
            BindDDL(ddlLectureType, dsLectureType, "Activity_Name", "Activity_Id");
            ddlLectureType.Items.Insert(0, "Select");
            ddlLectureType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    /// <summary>
    /// Bind Chapter Datalist
    /// </summary>
    private void FillGrid_LectureSchedule()
    {
        try
        {
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();

            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }

            if (ddlAcademicYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                ddlAcademicYear.Focus();
                return;
            }

            if (ddlCourse.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlCourse.Focus();
                return;
            }

            if (ddlLMSnonLMSProdct.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select LMS NON LMS Product");
                ddlLMSnonLMSProdct.Focus();
                return;
            }

            if (ddlSubject.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Subject");
                ddlSubject.Focus();
                return;
            }

            if (ddlCentre.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCentre.Focus();
                return;
            }

            if (ddlBatch.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Batch");
                ddlBatch.Focus();
                return;
            }
            if (ddlLectureType.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Lecture Type");
                ddlLectureType.Focus();
                return;
            }
            ControlVisibility("Result");

            string DivisionCode = null, Aca_Year = null, LMSProdCode = null, CenterCode = null, BatchCode = null, LectureTypeCode = null;
            DivisionCode = ddlDivision.SelectedValue;
            Aca_Year = ddlAcademicYear.SelectedValue;
            LMSProdCode = ddlLMSnonLMSProdct.SelectedValue;
            CenterCode = ddlCentre.SelectedValue;
            BatchCode = ddlBatch.SelectedValue;
            LectureTypeCode = ddlLectureType.SelectedValue;

            DataSet dsGrid = ProductController.Get_LectureScheduleBulkEntry(DivisionCode, ddlCourse.SelectedValue, ddlSubject.SelectedValue, BatchCode, Aca_Year, LMSProdCode, CenterCode, LectureTypeCode, "1");
            // DataSet dsGrid = ProductController.Get_LectureScheduleByDivisionCourse(DivisionCode,ddlCourse.SelectedValue,ddlSubject.SelectedValue, "1");

            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcdYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();
            lblLMSProduct_Result.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
            lblSubject_Result.Text = ddlSubject.SelectedItem.ToString();
            lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
            lblBatch_Result.Text = ddlBatch.SelectedItem.ToString();
            lblLectureType_Result.Text = ddlLectureType.SelectedItem.ToString();

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                    }
                    else
                    {
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                lbltotalcount.Text = "0";
            }
        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    /// <summary>
    /// Fill drop down list and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }






    #endregion



}
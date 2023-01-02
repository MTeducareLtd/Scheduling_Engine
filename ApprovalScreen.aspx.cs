using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShoppingCart.BL;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public partial class ApprovalScreen : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();

        }
    }
    #endregion

    #region Methods

    private void Clear_Error_Success_Box1()
    {
        divErrorLectApprove.Visible = false;
        lblErrorLectApprove.Text = "";
    }

    private bool ValidationLectApproveContent()
    {
        bool flag = true;
        Clear_Error_Success_Box1();

        if (ddlAction.SelectedValue == "0")
        {
            Show_Error_Success_Box1("E", "Select Action");
            ddlAction.Focus();
            flag = false;
            return flag;
        }

        if (txtRemark.Text.Trim() == "")
        {
            Show_Error_Success_Box1("E", "Enter Remarks");
            txtRemark.Focus();
            flag = false;
            return flag;
        }
        return flag;
    }


    private void Show_Error_Success_Box1(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            divErrorLectApprove.Visible = true;
            lblErrorLectApprove.Text = ProductController.Raise_Error(Error_Code);
        }

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
            ddlCentre.Items.Insert(1, "All");
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

    /// <summary>
    /// Bind search  Datalist
    /// </summary>
    private void FillGrid()
    {
        try
        {
            Clear_Error_Success_Box();

            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlDivision.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                ddlAcademicYear.Focus();
                return;
            }
            if (ddlCourse.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlCourse.Focus();
                return;
            }
            if (ddlLMSnonLMSProdct.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select LMS Non LMS Product");
                ddlLMSnonLMSProdct.Focus();
                return;
            }
            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCentre.Focus();
                return;
            }
            if (ddlApprovaltype.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Approval Type");
                ddlApprovaltype.Focus();
                return;
            }
            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }


            ControlVisibility("Result");
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.Text;

            string CourseCode = null;
            CourseCode = ddlCourse.SelectedValue;


            string ProductCode = "";
            ProductCode = ddlLMSnonLMSProdct.SelectedValue;

            string CenterCode = "";
            CenterCode = ddlCentre.SelectedValue;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
                Response.Redirect("Login.aspx");

            DataSet dsGrid = ProductController.Get_Lecture_Approval(DivisionCode, YearName, ProductCode, CenterCode, FromDate, ToDate, ddlApprovaltype.SelectedValue, CourseCode, lblHeader_User_Code.Text);
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString(); ;
            lblLMSProduct_Result.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
            lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
            lblApprovalType_result.Text = ddlApprovaltype.SelectedItem.ToString();
            lblDate_result.Text = id_date_range_picker_1.Value;

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
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;


        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;

        }


    }

    #endregion

    #region Events
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            ddlCentre.Items.Clear();
            ddlLMSnonLMSProdct.Items.Clear();
            ddlCourse.Items.Clear();
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
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
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

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        int i = 0;
        //Find Atleast One checkbox is selected or not
        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            CheckBox chkLectApprove = (CheckBox)dtlItem.FindControl("chkLectApprove");
            if (chkLectApprove.Checked == true)
            {
                i = 1;
                break;
            }
        }
        if (i == 1)
        {
            ddlAction.SelectedValue = "0";
            txtRemark.Text = "";
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalApprove();", true);
        }
        else
            Show_Error_Success_Box("E", "Select Atleast one record");
    }


    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlLMSnonLMSProdct.Items.Clear();
        ddlCentre.Items.Clear();
        ddlApprovaltype.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidationLectApproveContent() == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalApprove();", true);
        }
        else
        {
            if (ddlAction.SelectedValue == "1") //Approve Cancellation Lectures
            {
                foreach (DataListItem dtlItem in dlGridDisplay.Items)
                {
                    CheckBox chkLectApprove = (CheckBox)dtlItem.FindControl("chkLectApprove");
                    if (chkLectApprove.Checked == true)
                    {
                        Label lblLectScheduleId = (Label)dtlItem.FindControl("lblLectScheduleId");
                        int ResultId = 0;
                        ResultId = ProductController.InsertUpdateLectureShedule(lblLectScheduleId.Text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "4", "", "", "", "", txtRemark.Text);
                        Send_Details_LMS(lblLectScheduleId.Text);
                    }
                }
                BtnSearch_Click(sender, e);
            }
            else if (ddlAction.SelectedValue == "2") //Reject Cancellation Lectures
            {
                foreach (DataListItem dtlItem in dlGridDisplay.Items)
                {
                    CheckBox chkLectApprove = (CheckBox)dtlItem.FindControl("chkLectApprove");
                    if (chkLectApprove.Checked == true)
                    {
                        Label lblLectScheduleId = (Label)dtlItem.FindControl("lblLectScheduleId");
                        int ResultId = 0;
                        ResultId = ProductController.InsertUpdateLectureShedule(lblLectScheduleId.Text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "5", "", "", "", "", txtRemark.Text);
                        Send_Details_LMS(lblLectScheduleId.Text);
                    }
                }
                BtnSearch_Click(sender, e);
            }
        }
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


    protected void chkApprovalAll_CheckedChanged(object sender, EventArgs e)
    {
        //int rowcount=

        try
        {
            CheckBox s = sender as CheckBox;
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                CheckBox chkLectApprove = (CheckBox)dtlItem.FindControl("chkLectApprove");
                chkLectApprove.Checked = s.Checked;
            }
            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    #endregion






    protected void btnApprovedMonthly_Click(object sender, EventArgs e)
    {

    }
}

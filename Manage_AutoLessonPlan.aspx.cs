using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public partial class Manage_AutoLessonPlan : System.Web.UI.Page
{

    #region PageLoad
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


    private void FillDDL_Batch_Search()
    {
        try
        {
            ddlBatch_Search.Items.Clear();

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string ProductCode = null;
            ProductCode = ddlLMSnonLMSProdct.SelectedValue;

            string CentreCode = null;
            CentreCode = ddlCentre.SelectedValue;

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, CentreCode, "1");
            BindListBox(ddlBatch_Search, dsBatch, "Batch_Name", "Batch_Code");
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

    private void BindDDLTable(DropDownList ddl, DataTable dt, string txtField, string valField)
    {
        ddl.DataSource = dt;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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

    private void BindListBoxTable(ListBox ddl, DataTable ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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

            
            string BatchCode = "";

            for (int cnt = 0; cnt <= ddlBatch_Search.Items.Count - 1; cnt++)
            {
                if (ddlBatch_Search.Items[cnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch_Search.Items[cnt].Value + ",";
                }
            }

            if (BatchCode != "")
            {
                BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);
            }
            else
            {
                BatchCode = "%%";
            }

            DataSet dsGrid = ProductController.Get_Lecture_Schedule_AutoLessonPlan(DivisionCode, YearName, ProductCode, CenterCode, CourseCode, BatchCode,1,"");
            if (dsGrid != null)
            {
                if (dsGrid.Tables[0].Rows.Count > 0)
                {
                    ControlVisibility("Result");
                    dlGridDisplay.DataSource = dsGrid.Tables[0];
                    dlGridDisplay.DataBind();

                    lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                    lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
                    lblCourse_Result.Text = ddlCourse.SelectedItem.ToString(); ;
                    lblLMSProduct_Result.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
                    lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
                    lblbatchadd.Text = ddlBatch_Search.SelectedItem.ToString();


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
                    Show_Error_Success_Box("E", "Records Not found");
                    return;
                }
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


    #endregion



    #region Event's

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;        
        ddlDivision_SelectedIndexChanged(sender, e);
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Search");
    }


   
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
           // DivAddlesonplan.Visible = false;
            DivAddreulstPanel.Visible = false;
            DivLinkBatch.Visible = false;
            btnLinkBatch.Visible = true;
            //btnaddlesonplan.Visible = true;
        }
        else if (Mode == "LinkBatch")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = false;
            // DivAddlesonplan.Visible = false;
            DivAddreulstPanel.Visible = false;
            btnLinkBatch.Visible = false;
            DivLinkBatch.Visible = true;
            //btnaddlesonplan.Visible = true;
        }
        else if (Mode == "Result")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = true;
          //  DivAddlesonplan.Visible = false;
            DivAddreulstPanel.Visible = false;
            btnLinkBatch.Visible = false;
        }
        else if (Mode == "Add")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = false;
           // DivAddlesonplan.Visible = true;
            DivAddreulstPanel.Visible = false;
            btnLinkBatch.Visible = false;
          //  btnaddlesonplan.Visible = false;
           // id_date_range_picker_1.Value = "";
        }
        else if (Mode == "AddSearch")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = false;
          //  DivAddlesonplan.Visible = false;
            DivAddreulstPanel.Visible = true;
            btnLinkBatch.Visible = false;
            //btnaddlesonplan.Visible = true;
        }

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
       // FillGrid();
        FillGrid1();
    }

   

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        
        ddlBatch_Search.Items.Clear();
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
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Batch_Search();       
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlBatch_Search.Items.Clear();
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
            ddlsubject.Items.Clear();
            ddlDivision.Focus();
            return;
        }
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlsubject.Items.Clear();
            ddlAcademicYear.Focus();
            return;
        }
        if (ddlCourse.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlsubject.Items.Clear();
            ddlCourse.Focus();
            return;
        }
        FillDDL_LMSNONLMSProduct();
        FillDDL_Subject();

    }
    private void FillDDL_Subject()
    {
        try
        {
            string StandardCode = null, LMSProductCode = null;
            StandardCode = ddlCourse.SelectedValue;
            LMSProductCode = ddlLMSnonLMSProdct.SelectedValue;
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard_LMSProduct(StandardCode, LMSProductCode);

            BindDDL(ddlsubject, dsSubject, "Subject_ShortName", "Subject_Code");
            ddlsubject.Items.Insert(0, "Select");
            ddlsubject.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlLMSnonLMSProdct_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Batch_Search();
        FillDDL_Subject();
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        //int rowcount=
        Clear_Error_Success_Box();
        try
        {
            CheckBox s = sender as CheckBox;

            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                chkitemck.Checked = s.Checked;                
            }            
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void chkAll1_CheckedChanged(object sender, EventArgs e)
    {
        //int rowcount=
        Clear_Error_Success_Box();
        try
        {
            CheckBox s = sender as CheckBox;

            foreach (DataListItem dtlItem in DlAddLessonPlan.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                chkitemck.Checked = s.Checked;
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void btnAutoLessonPlan_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            string LectureScheduleId = "";
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                if (chkitemck.Checked == true)
                {
                    Label lblLectScheduleId = (Label)dtlItem.FindControl("lblLectScheduleId");

                    LectureScheduleId = LectureScheduleId + lblLectScheduleId.Text + ",";
                }
            }

            if (LectureScheduleId == "")
            {
                Show_Error_Success_Box("E", "Select atleast one record");
                return;
            }


            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            DataSet dsGrid = ProductController.Update_Lecture_Schedule_AutoLessonPlan(LectureScheduleId, lblHeader_User_Code.Text, 1);
            if (dsGrid != null)
            {
                for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                {
                    foreach (DataListItem dtlItem in dlGridDisplay.Items)
                    {
                        Label lblLectScheduleId = (Label)dtlItem.FindControl("lblLectScheduleId");
                        if (lblLectScheduleId.Text == dsGrid.Tables[0].Rows[i]["LectureSchedule_Id"].ToString())
                        {
                            Label lblErrorSaveMessage = (Label)dtlItem.FindControl("lblErrorSaveMessage");
                            if (dsGrid.Tables[0].Rows[i]["ErrorSaveId"].ToString() == "1")
                            {
                                lblErrorSaveMessage.CssClass = "green";
                                Send_Details_LMS(lblLectScheduleId.Text);
                            }
                            else if (dsGrid.Tables[0].Rows[i]["ErrorSaveId"].ToString() == "-1")
                            {
                                lblErrorSaveMessage.CssClass = "red";
                            }
                            lblErrorSaveMessage.Text = dsGrid.Tables[0].Rows[i]["ErrorSaveMessage"].ToString();

                            Label lblLessonPlan = (Label)dtlItem.FindControl("lblLessonPlan");
                            lblLessonPlan.Text = dsGrid.Tables[0].Rows[i]["LessonPlanName"].ToString();
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    #endregion

    protected void btnaddsearch_Click(object sender, EventArgs e)
    {
        FillGrid1();
    }
    protected void btnaddclear_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        //id_date_range_picker_1.Value="";
    }
    private void FillGrid1()
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
            if (ddlsubject.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Subject");
                ddlCentre.Focus();
                return;
            }
            //if (id_date_range_picker_1.Value == "")
            //{
            //    Show_Error_Success_Box("E", "Select Date Range");
            //    id_date_range_picker_1.Focus();
            //    return;
            //}


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

            string SubjectCode = "";
            SubjectCode = ddlsubject.SelectedValue;
             string FromDate, ToDate;
             //if (id_date_range_picker_1.Value == "")
             //{
             //    FromDate = "";
             //    ToDate = "";
             //}
             //else
             //{
             //    string DateRange = "";
             //    DateRange = id_date_range_picker_1.Value;

             //    FromDate = DateRange.Substring(0, 10);
             //    ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
             //}
            
            string BatchCode = "";
            string Batch_Name = "";

            for (int cnt = 0; cnt <= ddlBatch_Search.Items.Count - 1; cnt++)
            {
                if (ddlBatch_Search.Items[cnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch_Search.Items[cnt].Value + ",";
                    Batch_Name = Batch_Name + ddlBatch_Search.Items[cnt].Text + ",";
                }
            }

            if (BatchCode != "")
            {
                BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);
            }
            else
            {
                BatchCode = "%%";
            }
            BatchCode = Common.RemoveComma(BatchCode);
            Batch_Name = Common.RemoveComma(Batch_Name);


            DataSet dsGrid = ProductController.Get_Lecture_Schedule_AutoLessonPlan(DivisionCode, YearName, ProductCode, CenterCode, CourseCode, BatchCode, 2, SubjectCode);
            if (dsGrid != null)
            {
                if (dsGrid.Tables[0].Rows.Count > 0)
                {
                    
                    ControlVisibility("AddSearch");
                    DlAddLessonPlan.DataSource = dsGrid.Tables[0];
                    DlAddLessonPlan.DataBind();
                    lbldiv.Text = ddlDivision.SelectedItem.ToString();
                    lblacadyr.Text = ddlAcademicYear.SelectedItem.ToString();
                    lblcourse.Text = ddlCourse.SelectedItem.ToString(); ;
                    lblLMS.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
                    lblcenter1.Text = ddlCentre.SelectedItem.ToString();
                    lblsubject.Text = ddlsubject.SelectedItem.ToString();
                    lblbatchadd.Text = Batch_Name;
                   

               
            

                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        lbltotalRecord.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                    }
                    else
                    {
                        lbltotalcount.Text = "0";

                    }
                    if (dsGrid.Tables[1].Rows.Count != 0)
                    {
                        lblbatchsubject.Text = dsGrid.Tables[1].Rows[0]["Batch_Subject"].ToString();
                    }
                }
                else
                {
                    Show_Error_Success_Box("E", "Records Not found");
                    return;
                }
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
    protected void btnautoleassion_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            string LectureScheduleId = "";
            foreach (DataListItem dtlItem in DlAddLessonPlan.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lblErrorSaveMessage = (Label)dtlItem.FindControl("lblErrorSaveMessage");
                lblErrorSaveMessage.Text = "";
              
                    Label lblLectScheduleId = (Label)dtlItem.FindControl("lblLectScheduleId");

                    LectureScheduleId = LectureScheduleId + lblLectScheduleId.Text + ",";
               
            }
            LectureScheduleId = Common.RemoveComma(LectureScheduleId);

            if (LectureScheduleId == "")
            {
                Show_Error_Success_Box("E", "Select atleast one record");
                return;
            }
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

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
            string Batch=lblbatchsubject.Text;
            //DataSet dsGrid= ProductController.Update_Lecture_Schedule_AutoLessonPlan(LectureScheduleId, lblHeader_User_Code.Text, 2);
            DataSet dsGrid = ProductController.Get_Lecture_Schedule_AutoLessonPlan1(DivisionCode, YearName, CourseCode, ProductCode, CenterCode, Batch, lblHeader_User_Code.Text, LectureScheduleId, 3);
                 
            if (dsGrid != null)
            {
                for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                {
                    foreach (DataListItem dtlItem in DlAddLessonPlan.Items)
                    {
                        Label lblLectScheduleId = (Label)dtlItem.FindControl("lblLectScheduleId");
                      
                        if (lblLectScheduleId.Text == dsGrid.Tables[0].Rows[i]["LectureSchedule_Id"].ToString())
                        {
                            Label lblErrorSaveMessage = (Label)dtlItem.FindControl("lblErrorSaveMessage");
                            
                            if (dsGrid.Tables[0].Rows[i]["ErrorSaveId"].ToString() == "1")
                            {
                                lblErrorSaveMessage.CssClass = "green";
                                if (dsGrid.Tables[0].Rows[i]["syncflag"].ToString() == "1")
                                {
                                    Send_Details_LMS(lblLectScheduleId.Text);
                                }
                                Label lblLessonPlan = (Label)dtlItem.FindControl("lblLessonPlan");
                                Label lblChapter = (Label)dtlItem.FindControl("lblChapter");
                                lblLessonPlan.Text = dsGrid.Tables[0].Rows[i]["LessonPlanName"].ToString();
                                lblChapter.Text = dsGrid.Tables[0].Rows[i]["Chapter_Name"].ToString();
                            }
                            else if (dsGrid.Tables[0].Rows[i]["ErrorSaveId"].ToString() == "-1")
                            {
                                Label lblChapter = (Label)dtlItem.FindControl("lblChapter");
                                if (lblChapter.Text != dsGrid.Tables[0].Rows[i]["Chapter_Name"].ToString())
                                {
                                    lblChapter.Text = dsGrid.Tables[0].Rows[i]["Chapter_Name"].ToString();
                                    Send_Details_LMS(lblLectScheduleId.Text);
                                }

                                lblErrorSaveMessage.CssClass = "red";
                            }
                            lblErrorSaveMessage.Text = dsGrid.Tables[0].Rows[i]["ErrorSaveMessage"].ToString();                           
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }

    protected void btnLinkBatch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
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
            Show_Error_Success_Box("E", "Select LMS Product");
            ddlLMSnonLMSProdct.Focus();
            return;
        }

        if (ddlCentre.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Center");
            ddlCentre.Focus();
            return;
        }


        lblResult_LinkBatchDiv.Text = ddlDivision.SelectedItem.ToString();
        lblResult_LinkBatchAcadYear.Text = ddlAcademicYear.SelectedItem.ToString();
        lblResult_LinkBatchCourse.Text = ddlCourse.SelectedItem.ToString();
        lblResult_LinkBatchLMSProduct.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
        lblResult_LinkBatchCenter.Text = ddlCentre.SelectedItem.ToString();

        string XMLData = "<LinkedBatches><LinkedBatch><DivCode>" + ddlDivision.SelectedValue + "</DivCode><AcadYear>" 
                + ddlAcademicYear.SelectedValue + "</AcadYear><CourseCode>" + ddlCourse.SelectedValue + "</CourseCode><LMSProductCode>" 
                + ddlLMSnonLMSProdct.SelectedValue + "</LMSProductCode><CenterCode>" + ddlCentre.SelectedValue + "</CenterCode></LinkedBatch></LinkedBatches>";

        DataSet dsGrid = ProductController.Get_Insert_LinkedBatch(XMLData,1);

        string AlreadyLinkedBatch = "";

        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count > 0)
            {
                dlLinkBatch.DataSource = dsGrid.Tables[0];
                dlLinkBatch.DataBind();

                foreach (DataListItem dtlItem in dlLinkBatch.Items)
                {
                    //DropDownList ddlLinkBatch = (DropDownList)dtlItem.FindControl("ddlLinkBatch");
                    ListBox ddlLinkBatch = (ListBox)dtlItem.FindControl("ddlLinkBatch");
                    Label lblLinkBatchCode = (Label)dtlItem.FindControl("lblLinkBatchCode");
                    Label lblSourceBatchCode = (Label)dtlItem.FindControl("lblSourceBatchCode");
                    Label lblAlreadyLinkedBatches = (Label)dtlItem.FindControl("lblAlreadyLinkedBatches");

                    //BindDDLTable(ddlLinkBatch, dsGrid.Tables[1], "Batch", "BatchCode");
                    //ddlLinkBatch.Items.Insert(0, "No Link Batch");
                    //ddlLinkBatch.SelectedIndex = 0;

                    BindListBoxTable(ddlLinkBatch, dsGrid.Tables[1], "Batch", "BatchCode");

                    AlreadyLinkedBatch = "";
                    
                    // ddlLessonPlan.SelectedIndex = ddlLessonPlan.Items.IndexOf(ddlLessonPlan.Items.FindByText(lblLessonPlanName.Text));

                    try
                    {
                        ddlLinkBatch.Items.Remove(ddlLinkBatch.Items.FindByValue(lblSourceBatchCode.Text));                        
                    }
                    catch { }
                    try
                    {
                        if (lblLinkBatchCode.Text != "")
                        {
                            //ddlLinkBatch.SelectedValue = lblLinkBatchCode.Text;
                           // ddlLinkBatch.Enabled = false;

                            string[] values = lblLinkBatchCode.Text.Split(',');
                            for (int i = 0; i < values.Length; i++)
                            {
                                //values[i] = values[i].Trim();
                                for (int rcnt = 0; rcnt <= ddlLinkBatch.Items.Count - 1; rcnt++)
                                {
                                    if (ddlLinkBatch.Items[rcnt].Value == values[i].ToString())
                                    {                                       
                                        AlreadyLinkedBatch = AlreadyLinkedBatch + ddlLinkBatch.Items[rcnt].ToString() + ",";
                                        //ddlLinkBatch.Items[rcnt].Selected = true;                                        
                                        try
                                        {
                                            ddlLinkBatch.Items.Remove(ddlLinkBatch.Items.FindByValue(values[i].ToString()));
                                        }
                                        catch { }
                                        break;
                                    }
                                }
                            }

                            if (AlreadyLinkedBatch != "")
                                AlreadyLinkedBatch = Common.RemoveComma(AlreadyLinkedBatch);

                            lblAlreadyLinkedBatches.Text = AlreadyLinkedBatch;
                        }
                    }
                    catch { }
                }

                ControlVisibility("LinkBatch");
            }
        }

        
    }
    protected void btnLinkBatchSave_Click(object sender, EventArgs e)
    {
        string LinkedBatchCode="",XMLData = "<LinkedBatches>";

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        foreach (DataListItem dtlItem in dlLinkBatch.Items)
        {
           // DropDownList ddlLinkBatch = (DropDownList)dtlItem.FindControl("ddlLinkBatch");
            ListBox ddlLinkBatch = (ListBox)dtlItem.FindControl("ddlLinkBatch");
            Label lblLinkBatchCode = (Label)dtlItem.FindControl("lblLinkBatchCode");
            Label lblSourceBatchCode = (Label)dtlItem.FindControl("lblSourceBatchCode");

            LinkedBatchCode = "";

            for (int cnt = 0; cnt <= ddlLinkBatch.Items.Count - 1; cnt++)
            {
                if (ddlLinkBatch.Items[cnt].Selected == true)
                {
                    LinkedBatchCode = LinkedBatchCode + ddlLinkBatch.Items[cnt].Value + ",";
                }
            }

            if (LinkedBatchCode != "")
                LinkedBatchCode = Common.RemoveComma(LinkedBatchCode); 

            //if ((ddlLinkBatch.Enabled == true) && (ddlLinkBatch.SelectedIndex != 0))
            //{
            //    if (ddlLinkBatch.SelectedValue != lblLinkBatchCode.Text)
            //    {
            if ((LinkedBatchCode != "") && (LinkedBatchCode != lblLinkBatchCode.Text))
            {
                if (lblLinkBatchCode.Text != "")
                    LinkedBatchCode = lblLinkBatchCode.Text + "," + LinkedBatchCode;

                XMLData = XMLData + "<LinkedBatch><DivCode>" + ddlDivision.SelectedValue + "</DivCode><AcadYear>"
                          + ddlAcademicYear.SelectedValue + "</AcadYear><CourseCode>" + ddlCourse.SelectedValue + "</CourseCode><LMSProductCode>"
                          + ddlLMSnonLMSProdct.SelectedValue + "</LMSProductCode><CenterCode>" + ddlCentre.SelectedValue
                          + "</CenterCode><SourceBatchCode>" + lblSourceBatchCode.Text + "</SourceBatchCode><LinkBatchCode>"
                          + LinkedBatchCode + "</LinkBatchCode><CreatedBy>" + lblHeader_User_Code.Text + "</CreatedBy></LinkedBatch>";
            }
            //    }
            //}
        }

        XMLData = XMLData + "</LinkedBatches>";

        if(XMLData != "<LinkedBatches></LinkedBatches>")
        {
            DataSet dsGrid = ProductController.Get_Insert_LinkedBatch(XMLData,2);

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count > 0)
                {
                    if (dsGrid.Tables[0].Rows[0]["Result"].ToString() == "1")
                    {
                        btnLinkBatch_Click(sender, e);
                        Show_Error_Success_Box("S", "Record saved successfully");
                    }
                }
            }
        }
        else
        {

        }
    }
}

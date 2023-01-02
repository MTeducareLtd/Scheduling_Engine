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


public partial class LecturescheduleDecentralized : System.Web.UI.Page
{

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_LectureType();
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

    private void FillDDL_Batch()
    {
        try
        {
            ddlBatch_add.Items.Clear();

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string ProductCode = null;
            ProductCode = ddlLMSnonLMSProdct.SelectedValue;

            string CentreCode = null;
            CentreCode = ddlCentre.SelectedValue;

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, CentreCode, "1");
            BindListBox(ddlBatch_add, dsBatch, "Batch_Name", "Batch_Code");
            BindListBox(ddlEditBatch, dsBatch, "Batch_Name", "Batch_Code");

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

            string StandardCode = null, LMSProductCode = null;
            StandardCode = ddlCourse.SelectedValue;
            LMSProductCode = ddlLMSnonLMSProdct.SelectedValue;
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard_LMSProduct(StandardCode, LMSProductCode);

            BindDDL(ddlSubject_Add, dsSubject, "Subject_ShortName", "Subject_Code");
            ddlSubject_Add.Items.Insert(0, "Select");
            ddlSubject_Add.SelectedIndex = 0;

            BindDDL(ddlEditSubject, dsSubject, "Subject_ShortName", "Subject_Code");
            ddlEditSubject.Items.Insert(0, "Select");
            ddlEditSubject.SelectedIndex = 0;

            BindDDL(ddlReplaceSubject, dsSubject, "Subject_ShortName", "Subject_Code");
            ddlReplaceSubject.Items.Insert(0, "Select");
            ddlReplaceSubject.SelectedIndex = 0;


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
    /// Fill Chapter dropdown
    /// </summary>
    private void FillDDL_Chapter(string SubCode)
    {
        try
        {
            Clear_Error_Success_Box();
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string SubjectCode = SubCode;
            DataSet dsChapter = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(DivisionCode, "", StandardCode, SubjectCode);

            BindDDL(ddlChapter_Add, dsChapter, "Chapter_Name", "Chapter_Code");
            ddlChapter_Add.Items.Insert(0, "Select");
            ddlChapter_Add.SelectedIndex = 0;

            BindDDL(ddlEditChapter, dsChapter, "Chapter_Name", "Chapter_Code");
            ddlEditChapter.Items.Insert(0, "Select");
            ddlEditChapter.SelectedIndex = 0;

            BindDDL(ddlReplaceChapter, dsChapter, "Chapter_Name", "Chapter_Code");
            ddlReplaceChapter.Items.Insert(0, "Select");
            ddlReplaceChapter.SelectedIndex = 0;
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
    /// Fill Chapter dropdown
    /// </summary>
    private void FillDDL_Chapter_Update(string SubCode, string PartnerCode, string CenterCode)
    {
        try
        {
            Clear_Error_Success_Box();
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string SubjectCode = SubCode;
            DataSet dsChapter = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject_Partner(DivisionCode, ddlAcademicYear.SelectedValue, SubjectCode, PartnerCode, CenterCode);

            BindDDL(ddlEditChapter, dsChapter, "Chapter_Name", "Chapter_Code");
            ddlEditChapter.Items.Insert(0, "Select");
            ddlEditChapter.SelectedIndex = 0;
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
    /// Fill LectureType dropdown list
    /// </summary>
    private void FillDDL_LectureType()
    {
        try
        {
            DataSet dsLectureType = ProductController.GetActivityType();
            BindDDL(ddlLectureType_Add, dsLectureType, "Activity_Name", "Activity_Id");
            ddlLectureType_Add.Items.Insert(0, "Select");
            ddlLectureType_Add.SelectedIndex = 0;

            BindDDL(ddlEditLectureType, dsLectureType, "Activity_Name", "Activity_Id");
            ddlEditLectureType.Items.Insert(0, "Select");
            ddlEditLectureType.SelectedIndex = 0;
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
    /// Fill AddTeacher dropdown list
    /// </summary>
    private void FillDDL_TeacherAdd()
    {
        try
        {
            Clear_Error_Success_Box();
            ddlFaculty.Items.Clear();
            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                return;
            }
            if (ddlSubject_Add.SelectedItem.ToString() == "Select")
            {
                return;
            }
            if (ddlChapter_Add.SelectedItem.ToString() == "Select")
            {
                return;
            }


            DataSet dsFaculty = ProductController.GetTeacherByCenterSubjectChapter(ddlCentre.SelectedValue, ddlSubject_Add.SelectedValue, ddlChapter_Add.SelectedValue, ddlAcademicYear.SelectedValue);

            BindDDL(ddlFaculty, dsFaculty, "TeacherName", "Partner_Code");
            ddlFaculty.Items.Insert(0, "Select");
            ddlFaculty.SelectedIndex = 0;


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
    /// Fill EditTeacher dropdown list
    /// </summary>
    private void FillDDL_TeacherEdit()
    {
        try
        {
            ddlEditFaculty.Items.Clear();
            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                ddlCentre.Focus();
                return;
            }
            if (ddlEditSubject.SelectedItem.ToString() == "Select")
            {
                ddlEditSubject.Focus();
                return;
            }
            if (ddlEditChapter.ToString() == "Select")
            {
                ddlEditChapter.Focus();
                return;
            }

            DataSet dsFaculty = ProductController.GetTeacherByCenterSubjectChapter(ddlCentre.SelectedValue, ddlEditSubject.SelectedValue, ddlEditChapter.SelectedValue, ddlAcademicYear.SelectedValue);

            BindDDL(ddlEditFaculty, dsFaculty, "TeacherName", "Partner_Code");
            ddlEditFaculty.Items.Insert(0, "Select");
            ddlEditFaculty.SelectedIndex = 0;

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
    /// Fill ReplaceTeacher dropdown list
    /// </summary>
    private void FillDDL_TeacherReplace()
    {
        try
        {
            ddlReplaceFaculty.Items.Clear();
            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                ddlCentre.Focus();
                return;
            }
            if (ddlReplaceSubject.SelectedItem.ToString() == "Select")
            {
                ddlReplaceSubject.Focus();
                return;
            }
            if (ddlReplaceChapter.ToString() == "Select")
            {
                ddlChapter_Add.Focus();
                return;
            }


            DataSet dsFaculty = ProductController.GetTeacherByCenterSubjectChapter(ddlCentre.SelectedValue, ddlReplaceSubject.SelectedValue, ddlReplaceChapter.SelectedValue, ddlAcademicYear.SelectedValue);
            // DataSet dsFaculty = ProductController.GetTeacherByCenterSubjectChapter_New(ddlCentre.SelectedValue, ddlReplaceSubject.SelectedValue, ddlReplaceChapter.SelectedValue, ddlAcademicYear.SelectedValue, lblPkey.Text,2);

            BindDDL(ddlReplaceFaculty, dsFaculty, "TeacherName", "Partner_Code");
            ddlReplaceFaculty.Items.Insert(0, "Select");
            ddlReplaceFaculty.SelectedIndex = 0;

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
    /// Fill AddLessonPlan dropdown list
    /// </summary>
    private void FillDDL_LessonPlanAdd()
    {
        try
        {
            Clear_Error_Success_Box();
            ddlLessonPlanAdd.Items.Clear();

            if (ddlSubject_Add.SelectedItem.ToString() == "Select")
            {
                return;
            }
            if (ddlChapter_Add.SelectedItem.ToString() == "Select")
            {
                return;
            }


            DataSet dsLessonPlan = ProductController.GetLessonPlanBySubjectChapter(ddlSubject_Add.SelectedValue, ddlChapter_Add.SelectedValue);
            BindDDL(ddlLessonPlanAdd, dsLessonPlan, "LessonPlanName", "LessonPlanCode");
            ddlLessonPlanAdd.Items.Insert(0, "Select");
            ddlLessonPlanAdd.SelectedIndex = 0;
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
    /// Fill EditLessonPlan dropdown list
    /// </summary>
    private void FillDDL_LessonPlanEdit()
    {
        try
        {
            Clear_Error_Success_Box();
            ddlEditLessonPlan.Items.Clear();

            if (ddlEditSubject.SelectedItem.ToString() == "Select")
            {
                return;
            }
            if (ddlEditChapter.SelectedItem.ToString() == "Select")
            {
                return;
            }


            DataSet dsLessonPlan = ProductController.GetLessonPlanBySubjectChapter(ddlEditSubject.SelectedValue, ddlEditChapter.SelectedValue);
            BindDDL(ddlEditLessonPlan, dsLessonPlan, "LessonPlanName", "LessonPlanCode");
            ddlEditLessonPlan.Items.Insert(0, "Select");
            ddlEditLessonPlan.SelectedIndex = 0;
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
    /// Fill ReplaceLessonPlan dropdown list
    /// </summary>
    private void FillDDL_LessonPlanReplace()
    {
        try
        {
            Clear_Error_Success_Box();
            ddlReplaceLessonPlan.Items.Clear();

            if (ddlReplaceSubject.SelectedItem.ToString() == "Select")
            {
                return;
            }
            if (ddlReplaceChapter.SelectedItem.ToString() == "Select")
            {
                return;
            }


            DataSet dsLessonPlan = ProductController.GetLessonPlanBySubjectChapter(ddlReplaceSubject.SelectedValue, ddlReplaceChapter.SelectedValue);
            BindDDL(ddlReplaceLessonPlan, dsLessonPlan, "LessonPlanName", "LessonPlanCode");
            ddlReplaceLessonPlan.Items.Insert(0, "Select");
            ddlReplaceLessonPlan.SelectedIndex = 0;
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
    /// Clear Add Panel 
    /// </summary>
    private void ClearAddPanel()
    {
        ddlBatch_add.ClearSelection();
        txtLectureDate.Value = "";
        ddlLectureType_Add.SelectedIndex = 0;
        ddlSubject_Add.SelectedIndex = 0;
        ddlChapter_Add.Items.Clear();
        ddlFromHour_Add.SelectedIndex = 0;
        ddlFromMinute_add.SelectedIndex = 0;
        ddlFromAmPm_add.SelectedIndex = 0;
        ddlToHour.SelectedIndex = 0;
        ddlToMinute.SelectedIndex = 0;
        ddlToAMPM.SelectedIndex = 0;
        ddlFaculty.Items.Clear();
        ddlLessonPlanAdd.Items.Clear();
        lblPkey.Text = "";
    }

    /// <summary>
    /// Clear Edit Panel 
    /// </summary>
    private void ClearEditPanel()
    {
        ddlEditBatch.ClearSelection();
        txtEditLectureDate.Value = "";
        ddlEditLectureType.SelectedIndex = 0;
        ddlEditSubject.SelectedIndex = 0;
        ddlEditChapter.Items.Clear();
        ddlEditFromHour.SelectedIndex = 0;
        ddlEditFromMinute.SelectedIndex = 0;
        ddlEditFromAMPM.SelectedIndex = 0;
        ddlEditToHour.SelectedIndex = 0;
        ddlEditToMinute.SelectedIndex = 0;
        ddlEditToAMPM.SelectedIndex = 0;
        ddlEditFaculty.Items.Clear();
        ddlEditLessonPlan.Items.Clear();


        lblEditDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblEditAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
        lblEditCourse_Result.Text = ddlCourse.SelectedItem.ToString();
        lblEditLMSProduct_Result.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
        lblEditCeter_Result.Text = ddlCentre.SelectedItem.ToString();

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
            if (ddlLectStatus.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Lecture Status");
                ddlLectStatus.Focus();
                return;
            }
            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }



            FillDDL_Subject();
            FillDDL_Batch();

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

            DataSet dsGrid = ProductController.Get_Lecture_Schedule_Decentralized(DivisionCode, YearName, ProductCode, CenterCode, FromDate, ToDate, ddlLectStatus.SelectedValue, CourseCode, ddlLectureEntryStatus.SelectedValue, BatchCode);
            dlGridDisplay.DataSource = dsGrid.Tables[0];
            dlGridDisplay.DataBind();

            dlGridExport.DataSource = dsGrid.Tables[0];
            dlGridExport.DataBind();

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString(); ;
            lblLMSProduct_Result.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
            lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
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


    /// <summary>
    /// Insert data
    /// </summary>
    private void SaveData()
    {
        try
        {
            Clear_Error_Success_Box();

            //Saving part
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.Text;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string CenterCode = "";
            CenterCode = ddlCentre.SelectedValue;

            string ProductCode = "";
            ProductCode = ddlLMSnonLMSProdct.SelectedValue;

            string BatchCode = "";
            for (int cnt = 0; cnt <= ddlBatch_add.Items.Count - 1; cnt++)
            {
                if (ddlBatch_add.Items[cnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch_add.Items[cnt].Value + ",";
                }
            }

            BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);

            string LectureDate = "";
            LectureDate = txtLectureDate.Value;

            string LectureTypeCode = "";
            LectureTypeCode = ddlLectureType_Add.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject_Add.SelectedValue;


            string ChapterCode = "";
            if (ddlChapter_Add.SelectedItem.ToString() != "Select")
                ChapterCode = ddlChapter_Add.SelectedValue;

            string LessonPlanCode = "";
            if (ddlLessonPlanAdd.SelectedItem.ToString() != "Select")
                LessonPlanCode = ddlLessonPlanAdd.SelectedValue;

            string FromTime = "";
            FromTime = ddlFromHour_Add.SelectedItem.ToString() + ":" + ddlFromMinute_add.SelectedItem.ToString() + ":00 " + ddlFromAmPm_add.SelectedItem.ToString();

            string ToTime = "";
            ToTime = ddlToHour.SelectedItem.ToString() + ":" + ddlToMinute.SelectedItem.ToString() + ":00 " + ddlToAMPM.SelectedItem.ToString();

            string FacultyCode = "";
            FacultyCode = ddlFaculty.SelectedValue;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            int ResultId = 0;
            ResultId = ProductController.InsertUpdateLectureShedule(lblPkey.Text, DivisionCode, YearName, StandardCode, ProductCode, CenterCode, BatchCode, LectureDate, LectureTypeCode, SubjectCode, ChapterCode, FromTime, ToTime, LessonPlanCode, FacultyCode, CreatedBy, "1", "", "", "", "", "");

            if (ResultId == -1)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record already exist!!";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (ResultId == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "This Lecture Time is not available for this Teacher!!";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (ResultId == -3)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Travel Time is insufficient.";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (ResultId == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record not saved";
                UpdatePanelMsgBox.Update();
                return;
            }
            else
            {
                lblPkey.Text = ResultId.ToString();

                Send_Details_LMS(lblPkey.Text);
                //ClearAddPanel();
                //ControlVisibility("Search");
                Show_Error_Success_Box("S", "0000");
                CHk_btnVisiblity();
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
    /// Update data
    /// </summary>
    private void UpdateData(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            //Saving part
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.Text;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string CenterCode = "";
            CenterCode = ddlCentre.SelectedValue;

            string ProductCode = "";
            ProductCode = ddlLMSnonLMSProdct.SelectedValue;

            string BatchCode = "";
            for (int cnt = 0; cnt <= ddlEditBatch.Items.Count - 1; cnt++)
            {
                if (ddlEditBatch.Items[cnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlEditBatch.Items[cnt].Value + ",";
                }
            }

            BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);

            string LectureDate = "";
            LectureDate = txtEditLectureDate.Value;

            string LectureTypeCode = "";
            LectureTypeCode = ddlEditLectureType.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlEditSubject.SelectedValue;


            string ChapterCode = "";
            if (ddlEditChapter.SelectedItem.ToString() != "Select")
                ChapterCode = ddlEditChapter.SelectedValue;

            string LessonPlanCode = "";
            if (ddlEditLessonPlan.SelectedItem.ToString() != "Select")
                LessonPlanCode = ddlEditLessonPlan.SelectedValue;

            string FromTime = "";
            FromTime = ddlEditFromHour.SelectedItem.ToString() + ":" + ddlEditFromMinute.SelectedItem.ToString() + ":00 " + ddlEditFromAMPM.SelectedItem.ToString();

            string ToTime = "";
            ToTime = ddlEditToHour.SelectedItem.ToString() + ":" + ddlEditToMinute.SelectedItem.ToString() + ":00 " + ddlEditToAMPM.SelectedItem.ToString();

            string FacultyCode = "";
            FacultyCode = ddlEditFaculty.SelectedValue;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;


            int ResultId = 0;
            ResultId = ProductController.InsertUpdateLectureShedule(lblPkey.Text, DivisionCode, YearName, StandardCode, ProductCode, CenterCode, BatchCode, LectureDate, LectureTypeCode, SubjectCode, ChapterCode, FromTime, ToTime, LessonPlanCode, FacultyCode, CreatedBy, "2", "", "", "", "", "");

            if (ResultId == -1)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record already exist!!";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (ResultId == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "This Lecture Time is not available for this faculty!!";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (ResultId == -3)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Travel Time is insufficient.";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (ResultId == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record not saved";
                UpdatePanelMsgBox.Update();

                return;

            }
            else
            {
                Send_Details_LMS(lblPkey.Text);
                //ClearAddPanel();
                //BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "0000");
                CHk_btnVisiblity();
            }

        }
        catch (Exception ex)
        {
            //ex.GetHashCode().ToString() +
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }

    }

    #endregion



    #region Event's

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        //Validation

        string BatchCode = "";
        for (int cnt = 0; cnt <= ddlBatch_add.Items.Count - 1; cnt++)
        {
            if (ddlBatch_add.Items[cnt].Selected == true)
            {
                BatchCode = BatchCode + ddlBatch_add.Items[cnt].Value + ",";
            }
        }
        if (BatchCode == "")
        {
            Show_Error_Success_Box("E", "Atleast one Batch should be selected");
            ddlBatch_add.Focus();
            return;
        }

        if (txtLectureDate.Value == "")
        {
            Show_Error_Success_Box("E", "Select Lecture Date");
            txtLectureDate.Focus();
            return;
        }

        if (ddlLectureType_Add.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Lecture Type");
            ddlLectureType_Add.Focus();
            return;
        }
        if (ddlSubject_Add.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Subject");
            ddlSubject_Add.Focus();
            return;
        }
        if (ddlChapter_Add.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Chapter");
            ddlChapter_Add.Focus();
            return;
        }
        if ((ddlFromHour_Add.Text == "--") || (ddlFromMinute_add.Text == "--"))
        {
            Show_Error_Success_Box("E", "Select From Time");
            return;
        }
        if ((ddlToHour.Text == "--") || (ddlToMinute.Text == "--"))
        {
            Show_Error_Success_Box("E", "Select To Time");
            return;
        }
        if (ddlFaculty.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Faculty");
            ddlFaculty.Focus();
            return;
        }

        SaveData();

    }

    protected void btnEditSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        //Validation

        string BatchCode = "";
        for (int cnt = 0; cnt <= ddlEditBatch.Items.Count - 1; cnt++)
        {
            if (ddlEditBatch.Items[cnt].Selected == true)
            {
                BatchCode = BatchCode + ddlEditBatch.Items[cnt].Value + ",";
            }
        }
        if (BatchCode == "")
        {
            Show_Error_Success_Box("E", "Atleast one Batch should be selected");
            ddlEditBatch.Focus();
            return;
        }

        if (txtEditLectureDate.Value == "")
        {
            Show_Error_Success_Box("E", "Select Lecture Date");
            txtEditLectureDate.Focus();
            return;
        }

        if (ddlEditLectureType.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Lecture Type");
            ddlEditLectureType.Focus();
            return;
        }
        if (ddlEditSubject.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Subject");
            ddlEditSubject.Focus();
            return;
        }
        if (ddlEditChapter.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Chapter");
            ddlEditChapter.Focus();
            return;
        }
        if ((ddlEditFromHour.Text == "--") || (ddlEditFromMinute.Text == "--"))
        {
            Show_Error_Success_Box("E", "Select From Time");
            return;
        }
        if ((ddlEditToHour.Text == "--") || (ddlEditToMinute.Text == "--"))
        {
            Show_Error_Success_Box("E", "Select To Time");
            return;
        }
        if (ddlEditFaculty.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Faculty");
            ddlEditFaculty.Focus();
            return;
        }
        UpdateData(sender, e);

    }

    protected void btn_Approve_Click(object sender, EventArgs e)
    {

        int ResultId = 0;
        ResultId = ProductController.InsertUpdateLectureShedule(lblPkey.Text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "4", "", "", "", "", "");

        BtnSearch_Click(sender, e);
        Show_Error_Success_Box("S", "Lecture cancellation request Approved successfully ");
    }

    protected void btn_CancelRequest_Click(object sender, EventArgs e)
    {
        int ResultId = 0;
        ResultId = ProductController.InsertUpdateLectureShedule(lblPkey.Text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "5", "", "", "", "", "");

        BtnSearch_Click(sender, e);
        Show_Error_Success_Box("S", "Lecture cancellation(replacement) request Cancelled ");
    }

    protected void btnCancelLectSave_Click(object sender, EventArgs e)
    {
        //Validation
        Clear_Error_Success_Box();
        string CancellationReasonCode = "", replaceSubject_Code = "", replaceChapter_Code = "", replaceFaculty_Code = "", LessonPlanCode = "";
        if ((optLectCancel1.Checked == false) && (optLectCancel2.Checked == false) && (optLectCancel3.Checked == false) && (optLectCancel4.Checked == false))
        {
            Show_Error_Success_Box("E", "Select Reson for Lecture cancellation");
            return;
        }
        //if (txtCancelRemark.Text.Trim() == "")
        //{
        //    Show_Error_Success_Box("E", "Enter Cancellation Remarks");
        //    txtCancelRemark.Focus();
        //    return;
        //}
        if (chkReplaceLect.Checked == true)
        {
            if (ddlReplaceSubject.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Replacement Subject");
                ddlReplaceSubject.Focus();
                return;
            }
            if (ddlReplaceChapter.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Replacement Chapter");
                ddlReplaceChapter.Focus();
                return;
            }
            if (ddlReplaceFaculty.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Replacement Teacher");
                ddlReplaceFaculty.Focus();
                return;
            }
            replaceSubject_Code = ddlReplaceSubject.SelectedValue;
            if (ddlReplaceChapter.SelectedItem.ToString() != "Select")
                replaceChapter_Code = ddlReplaceChapter.SelectedValue;
            replaceFaculty_Code = ddlReplaceFaculty.SelectedValue;
            if (ddlReplaceLessonPlan.SelectedItem.ToString() != "Select")
                LessonPlanCode = ddlReplaceLessonPlan.SelectedValue;
        }
        if (optLectCancel1.Checked == true)
            CancellationReasonCode = "1";
        else if (optLectCancel2.Checked == true)
            CancellationReasonCode = "2";
        else if (optLectCancel3.Checked == true)
            CancellationReasonCode = "3";
        else if (optLectCancel4.Checked == true)
            CancellationReasonCode = "4";



        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CancelledBy = null;
        CancelledBy = lblHeader_User_Code.Text;
        if (replaceSubject_Code == "")
        {
            replaceSubject_Code = "-1";
        }

        if (replaceSubject_Code != "-1")//if the Lecture is replaced then check validation
        {
            DataSet ds = ProductController.CheckValidation_LectureReplace_Partner(lblPkey.Text, replaceFaculty_Code, replaceSubject_Code, "1");
            if (ds.Tables[0].Rows[0]["Result"].ToString() == "-1")//if the error is come
            {
                Show_Error_Success_Box("E", ds.Tables[0].Rows[0]["ErrorMessage"].ToString());
                return;
            }
        }



        int ResultId = 0;
        if (ddlLect_Cancel_Reason.SelectedValue == "11")
        {
            //  ResultId = ProductController.InsertUpdateLectureShedule(lblPkey.Text, "", "", "", "", "", "", "", "", "", "", "", "",LessonPlanCode, "", CancelledBy, "3", CancellationReasonCode, replaceSubject_Code, replaceChapter_Code, replaceFaculty_Code,txtCancelRemark.Text);                
            ResultId = ProductController.InsertUpdateLectureShedule(lblPkey.Text, "", "", "", "", "", "", "", "", "", "", "", "", LessonPlanCode, "", CancelledBy, "3", CancellationReasonCode, replaceSubject_Code, replaceChapter_Code, replaceFaculty_Code, txtCancelRemark.Text);
        }
        else
        {
            ResultId = ProductController.InsertUpdateLectureShedule(lblPkey.Text, "", "", "", "", "", "", "", "", "", "", "", "", LessonPlanCode, "", CancelledBy, "3", CancellationReasonCode, replaceSubject_Code, replaceChapter_Code, replaceFaculty_Code, ddlLect_Cancel_Reason.SelectedItem.ToString());
        }
        //BtnSearch_Click(sender, e);
        if ((ResultId == -1) || (ResultId == -3))
        {
            Show_Error_Success_Box("E", "This Lecture Time is not available for this Teacher!!");
            return;
        }
        else if (ResultId != Convert.ToInt32(lblPkey.Text))
        {
            Send_Details_LMS(lblPkey.Text);
            Send_Details_LMS(ResultId.ToString());
            Show_Error_Success_Box("S", "Lecture is successfully replaced");
        }
        else
        {
            Send_Details_LMS(lblPkey.Text);
            Show_Error_Success_Box("S", "Lecture cancellation request has been processed to admin, once admin approves your cancellation request then lecture will be cancelled");
        }

        CHk_btnVisiblity_CancelLec();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
        ddlLectStatus.SelectedIndex = 0;
        ddlDivision_SelectedIndexChanged(sender, e);
    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void btnEditCancel_Click(object sender, EventArgs e)
    {
        BtnSearch_Click(sender, e);
    }

    protected void btnCancelLectClose_Click(object sender, EventArgs e)
    {
        BtnSearch_Click(sender, e);

    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
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
            Show_Error_Success_Box("E", "Select LMS Product");
            ddlLMSnonLMSProdct.Focus();
            return;
        }
        if (ddlCentre.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Center");
            ddlCentre.Focus();
            return;
        }

        FillDDL_Subject();
        FillDDL_Batch();
        ClearAddPanel();

        ControlVisibility("Add");

        lblDivision_Result2.Text = ddlDivision.SelectedItem.ToString();
        lblAcademicYear_Result2.Text = ddlAcademicYear.SelectedItem.ToString();
        lblCourse_Result2.Text = ddlCourse.SelectedItem.ToString();
        lblLMSProduct_Result2.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
        lblCenter_Result2.Text = ddlCentre.SelectedItem.ToString();
        lblPkey.Text = "";
        FillDDL_LectureType();
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivCancel.Visible = false;
            DivMergeLecture.Visible = false;
            StudentDetailsDIV.Visible = false;
            divLectureWarning.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;
            DivEditPanel.Visible = false;
            DivCancel.Visible = false;
            DivMergeLecture.Visible = false;
            StudentDetailsDIV.Visible = false;
            divLectureWarning.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivCancel.Visible = false;
            DivMergeLecture.Visible = false;
            StudentDetailsDIV.Visible = false;
            lblPkey.Text = "";
            divLectureWarning.Visible = false;
        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            DivEditPanel.Visible = true;
            DivCancel.Visible = false;
            DivMergeLecture.Visible = false;
            StudentDetailsDIV.Visible = false;
            divLectureWarning.Visible = true;
        }
        else if (Mode == "Cancel")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivCancel.Visible = true;
            DivMergeLecture.Visible = false;
            StudentDetailsDIV.Visible = false;
            divLectureWarning.Visible = false;
        }
        else if (Mode == "Merge")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivCancel.Visible = false;
            DivMergeLecture.Visible = true;
            StudentDetailsDIV.Visible = false;
            divLectureWarning.Visible = false;
        }

        else if (Mode == "StudentSMS_CHK")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivCancel.Visible = false;
            DivMergeLecture.Visible = false;
            StudentDetailsDIV.Visible = true;

        }

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "comEdit")
            {
                ControlVisibility("Edit");
                ClearEditPanel();
                CHk_btnVisiblity();
                lblPkey.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.Get_LectureScheduleByPKey(lblPkey.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Fill selected Batches
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt <= ds.Tables[1].Rows.Count - 1; cnt++)
                        {
                            for (int rcnt = 0; rcnt <= ddlEditBatch.Items.Count - 1; rcnt++)
                            {
                                if (ddlEditBatch.Items[rcnt].Value == ds.Tables[1].Rows[cnt]["Batch_Code"].ToString())
                                {
                                    ddlEditBatch.Items[rcnt].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (ds.Tables[0].Rows[0]["AttendClosureStatus_Flag"].ToString() == "1")
                    {
                        btnEditSave.Visible = false;
                        btnMesage_ManualSending_Edit.Visible = false;
                        divLectureWarning.Visible = true;
                    }
                    else
                    {
                        btnEditSave.Visible = true;
                        btnMesage_ManualSending_Edit.Visible = true;
                        divLectureWarning.Visible = false;
                    }

                    txtEditLectureDate.Value = ds.Tables[0].Rows[0]["Session_Date"].ToString();
                    lblEditDate.Text = ds.Tables[0].Rows[0]["Session_Date"].ToString();
                    ddlEditLectureType.SelectedValue = ds.Tables[0].Rows[0]["Activity_Id"].ToString();

                    ddlEditSubject.SelectedValue = ds.Tables[0].Rows[0]["Subject_Code"].ToString();

                    //FillDDL_Chapter(ddlEditSubject.SelectedValue);
                    FillDDL_Chapter_Update(ds.Tables[0].Rows[0]["Subject_Code"].ToString(), ds.Tables[0].Rows[0]["Partner_Code"].ToString(), ddlCentre.SelectedValue);
                    ddlEditFaculty.Items.Clear();
                    ddlEditLessonPlan.Items.Clear();
                    if (ds.Tables[0].Rows[0]["Chapter_Code"].ToString() != "")
                    {
                        if (ddlEditChapter.Items.FindByValue(ds.Tables[0].Rows[0]["Chapter_Code"].ToString()) != null)
                        {
                            ddlEditChapter.SelectedValue = ds.Tables[0].Rows[0]["Chapter_Code"].ToString();
                        }

                    }
                    lblEditTeacherName.Text = "";
                    FillDDL_TeacherEdit();
                    FillDDL_LessonPlanEdit();
                    if (ds.Tables[0].Rows[0]["Partner_Code"].ToString() != "")
                    {
                        if (ddlEditFaculty.Items.FindByValue(ds.Tables[0].Rows[0]["Partner_Code"].ToString()) != null)
                        {
                            ddlEditFaculty.SelectedValue = ds.Tables[0].Rows[0]["Partner_Code"].ToString();
                            lblEditTeacherName.Text = ddlEditFaculty.SelectedItem.ToString();
                        }
                    }

                    if (ds.Tables[0].Rows[0]["LessonPlanCode"].ToString() != "")
                        if (ddlEditLessonPlan.Items.FindByValue(ds.Tables[0].Rows[0]["LessonPlanCode"].ToString()) != null)
                        {
                            try
                            {
                                ddlEditLessonPlan.SelectedValue = ds.Tables[0].Rows[0]["LessonPlanCode"].ToString();
                            }
                            catch
                            {
                                ddlEditLessonPlan.SelectedIndex = 0;
                            }
                        }

                    string FromTime = ds.Tables[0].Rows[0]["FromTIME"].ToString();
                    string ToTime = ds.Tables[0].Rows[0]["ToTIME"].ToString();
                    string Hour = "";
                    string Minute = "";
                    if (FromTime.Contains(':'))
                    {
                        string[] words = FromTime.Split(':');
                        Hour = words[0];
                        Minute = words[1];
                        if (Hour.Length == 1)
                            ddlEditFromHour.SelectedValue = "0" + Hour;
                        else
                            ddlEditFromHour.SelectedValue = Hour;
                        ddlEditFromMinute.SelectedValue = Minute.Substring(0, 2);
                        ddlEditFromAMPM.SelectedValue = Minute.Substring(2, 2);
                    }
                    if (ToTime.Contains(':'))
                    {
                        string[] words1 = ToTime.Split(':');
                        Hour = words1[0];
                        Minute = words1[1];
                        if (Hour.Length == 1)
                            ddlEditToHour.SelectedValue = "0" + Hour;
                        else
                            ddlEditToHour.SelectedValue = Hour;
                        ddlEditToMinute.SelectedValue = Minute.Substring(0, 2);
                        ddlEditToAMPM.SelectedValue = Minute.Substring(2, 2);
                    }
                }
            }
            if (e.CommandName == "comDelete")
            {
                Clear_Error_Success_Box();
                lblPkey.Text = e.CommandArgument.ToString();
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
            }
            if (e.CommandName == "comRemove")
            {
                FillDDL_Lect_Cancel_Reason();
                btn_LecCancelSMSSent.Visible = false;
                ControlVisibility("Cancel");
                Clear_Error_Success_Box();
                lblPkey.Text = e.CommandArgument.ToString();
                lblCancelCenter.Text = ddlCentre.SelectedItem.ToString();
                lblCancelDivision.Text = ddlDivision.SelectedItem.ToString();
                lblCancelAcademicYear.Text = ddlAcademicYear.SelectedItem.ToString();
                lblCancelLMSProduct.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
                DataSet ds = ProductController.Get_LectureScheduleByPKey(lblPkey.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCancelLectDate.Text = ds.Tables[0].Rows[0]["Session_Date"].ToString();
                    lblCancelLectType.Text = ds.Tables[0].Rows[0]["Activity_Name"].ToString();
                    lblCancelLectSubject.Text = ds.Tables[0].Rows[0]["Subject_Name"].ToString();
                    lblCancelChapter.Text = ds.Tables[0].Rows[0]["Chapter_Name"].ToString();
                    lblCancelFromTime.Text = ds.Tables[0].Rows[0]["FromTIME"].ToString();
                    lblCancelToTime.Text = ds.Tables[0].Rows[0]["ToTIME"].ToString();
                    lblCancelFacultyName.Text = ds.Tables[0].Rows[0]["FacultyName"].ToString();
                    lblCancelLessonPlan.Text = ds.Tables[0].Rows[0]["LessonPlanName"].ToString();

                    lblCancelLectBatch.Text = "";
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt <= ds.Tables[1].Rows.Count - 1; cnt++)
                        {
                            lblCancelLectBatch.Text = lblCancelLectBatch.Text + ds.Tables[1].Rows[cnt]["Batch_Name"].ToString() + ",";
                        }
                    }
                    string Batch = lblCancelLectBatch.Text;
                    lblCancelLectBatch.Text = Batch.Substring(0, Batch.Length - 1);
                    chkReplaceLect.Checked = false;
                    chkReplaceLect_CheckedChanged(source, e);
                    optLectCancel1.Checked = false;
                    optLectCancel2.Checked = false;
                    optLectCancel3.Checked = false;
                    optLectCancel4.Checked = false;
                    txtCancelRemark.Text = "";
                }

            }
            if (e.CommandName == "comMerging")
            {
                ControlVisibility("Merge");
            }
            if (e.CommandName == "comCancelApprove")
            {
                Clear_Error_Success_Box();
                lblPkey.Text = e.CommandArgument.ToString();
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCancelApprove();", true);
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
        //if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        //{
        //    Show_Error_Success_Box("E", "Select Academic Year");
        //    ddlAcademicYear.Focus();
        //    return;
        //}
        //if (ddlCourse.SelectedItem.ToString() == "Select")
        //{
        //    Show_Error_Success_Box("E", "Select Course");
        //    ddlCourse.Focus();
        //    return;
        //}
        //if (ddlCentre.SelectedItem.ToString() == "Select")
        //{
        //    Show_Error_Success_Box("E", "Select Center");
        //    ddlCentre.Focus();
        //    return;
        //}
        //FillDDL_Batch();
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
    //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Msg_Error.Visible = false;
    //    Msg_Success.Visible = false;
    //    lblerror.Text = "";
    //    UpdatePanelMsgBox.Update();
    //    if (ddlCourse.SelectedItem.ToString() == "Select")
    //    {
    //        ddlCourse.Focus();
    //        return;
    //    }
    //    FillDDL_Subject();
    //    if (ddlAcademicYear.SelectedItem.ToString() == "Select")
    //    {
    //        ddlAcademicYear.Focus();
    //        return;
    //    }

    //    if (ddlCentre.SelectedItem.ToString() == "Select")
    //    {
    //        ddlCentre.Focus();
    //        return;
    //    }
    //    FillDDL_Batch();

    //}
    protected void ddlSubject_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Validate if all information is entered correctly
            if (ddlSubject_Add.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0005");
                ddlSubject_Add.Focus();
                return;
            }
            FillDDL_Chapter(ddlSubject_Add.SelectedValue);
            ddlFaculty.Items.Clear();
            ddlLessonPlanAdd.Items.Clear();
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

    protected void ddlChapter_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_TeacherAdd();
        FillDDL_LessonPlanAdd();
    }

    protected void ddlEditSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Validate if all information is entered correctly
            if (ddlEditSubject.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0005");
                ddlEditSubject.Focus();
                return;
            }
            FillDDL_Chapter(ddlEditSubject.SelectedValue);
            ddlEditFaculty.Items.Clear();
            ddlEditLessonPlan.Items.Clear();
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

    protected void ddlEditChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDDL_TeacherEdit();
        FillDDL_LessonPlanEdit();
    }

    protected void ddlReplaceSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Validate if all information is entered correctly
            if (ddlReplaceSubject.SelectedIndex == 0)
            {
                ddlReplaceSubject.Focus();
                ddlReplaceChapter.Items.Clear();
                ddlReplaceFaculty.Items.Clear();
                ddlReplaceLessonPlan.Items.Clear();
                return;
            }
            FillDDL_Chapter(ddlReplaceSubject.SelectedValue);
            ddlReplaceFaculty.Items.Clear();
            ddlReplaceLessonPlan.Items.Clear();
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

    protected void ddlReplaceChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_TeacherReplace();
        FillDDL_LessonPlanReplace();
    }

    protected void ddlLMSnonLMSProdct_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Batch_Search();
    }

    protected void btnDelete_Yes_Click(object sender, EventArgs e)
    {
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");


        DataSet ds = ProductController.Delete_LectureScheduleByPKey(lblPkey.Text, 1, lblHeader_User_Code.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["DelFlag"].ToString() == "0")
            {
                Show_Error_Success_Box("S", "Record Delete Successfully.");
                ControlVisibility("Search");
            }
            else
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Lecture Date and Time has been passed hence you cannot delete!!";
                UpdatePanelMsgBox.Update();
            }
        }
    }

    protected void chkReplaceLect_CheckedChanged(object sender, EventArgs e)
    {
        if (chkReplaceLect.Checked == true)
        {
            RowSubject.Visible = true;
            RowFaculty.Visible = true;
        }
        else
        {
            RowSubject.Visible = false;
            RowFaculty.Visible = false;
            ddlReplaceSubject_SelectedIndexChanged(sender, e);
            ddlReplaceChapter_SelectedIndexChanged(sender, e);
            ddlReplaceFaculty.Items.Clear();
        }

    }

    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "LectureSchedule_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='9'>Lecture Schedule - " + ddlLectStatus.SelectedItem.ToString() + "</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1' style='text-align:right;'>Division - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblDivision_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Acad Year - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblAcademicYear_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Course - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblCourse_Result.Text + "</b></TD><TD Colspan='3' style='text-align:right;'></b></TD></TR><TR style='color: #fff; background: black;'><TD Colspan='1' style='text-align:right;'>LMS/NONLMS Product - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblLMSProduct_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Center - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblCenter_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Period - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblDate_result.Text + "</b></TD><TD Colspan='3' style='text-align:right;'></b></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridExport.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

        dlGridExport.Visible = false;
    }

    protected void dlGridDisplay_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            if (Convert.ToInt32(ddlLectStatus.SelectedValue) != 1)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
                LinkButton lnlCancel = (LinkButton)e.Item.FindControl("lnlCancel");
                // LinkButton lnkMerge = (LinkButton)e.Item.FindControl("lnkMerge");
                lnkEdit.Visible = false;
                lnkDelete.Visible = false;
                lnlCancel.Visible = false;
                //lnkMerge.Visible = false;

                //if (Convert.ToInt32(ddlLectStatus.SelectedValue) == 2)
                //{
                //    if (Request.Cookies["MyCookiesLoginInfo"] != null)
                //    {
                //string UserTypeCode = Request.Cookies["MyCookiesLoginInfo"]["UserTypeCode"];
                //if (UserTypeCode == "1")
                //{
                //    LinkButton lnkCancelApprove = (LinkButton)e.Item.FindControl("lnkCancelApprove");
                //    lnkCancelApprove.Visible = true;
                //}
                //    }
                //}
            }
        }

    }
    #endregion

    protected void btnMesage_ManualSending_ServerClick(object sender, System.EventArgs e)
    {
        //// Sending Code
        //Message_Template();
        //Message_TemplateFor_Teachers();

        int resultid = 0, smscount = 0, count = 0;

        DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblPkey.Text, 4);
        if (DSSentStatus != null)
        {
            if (DSSentStatus.Tables[0].Rows.Count > 0)
            {
                smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["LectureSMSFlag"].ToString());
                if (smscount == 0)
                {
                    StudentDetailsDIV.Visible = true;
                    FillStudent_SMSCHK();
                    Message_TemplateFor_Teachers();
                }
                else
                {

                }
            }
        }

    }

    protected void btnMesage_ManualSending_Edit_Click(object sender, System.EventArgs e)
    {
        //// Sending Code
        //Message_Template_edit();
        //Message_TemplateFor_Teachers();

        int resultid = 0, smscount = 0, count = 0;

        DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblPkey.Text, 4);
        if (DSSentStatus != null)
        {
            if (DSSentStatus.Tables[0].Rows.Count > 0)
            {
                smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["LectureSMSFlag"].ToString());
                if (smscount == 0)
                {
                    StudentDetailsDIV.Visible = true;
                    FillStudent_SMSCHK_edit();
                    Message_TemplateFor_Teachers_edit();
                }
                else
                {

                }
            }
        }

    }

    protected void btn_LecCancelSMSSent_Click(object sender, System.EventArgs e)
    {
        try
        {
            FillStudent_SMSCHK_Cancel();
        }
        catch (Exception ex)
        {
        }
    }

    protected void Btn_TestMessage_Click(object sender, System.EventArgs e)
    {
        string template = lblMessage_Template_SMS.Text.ToString().Trim();
        string newTemplate = "";

        foreach (DataListItem dtlItem in dlStudent_CHK.Items)
        {
            Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
            Label lblFirstName = (Label)dtlItem.FindControl("lblFirstName");
            Label lblRollNo = (Label)dtlItem.FindControl("lblRollNo");
            Label lblDate1 = (Label)dtlItem.FindControl("lblDate1");
            Label lblNFromTime = (Label)dtlItem.FindControl("lblNFromTime");
            Label lblNToTime = (Label)dtlItem.FindControl("lblNToTime");
            Label lblcenter_code = (Label)dtlItem.FindControl("lblcenter_code");
            Label lblBatchName = (Label)dtlItem.FindControl("lblBatchName");
            Label lblMobileNo = (Label)dtlItem.FindControl("lblMobileNo");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            DataSet dsGrid = ProductController.Get_Lecture_CancellationSMSTemplate(lblMessage_Code_Fin.Text, lblSBEntryCode.Text, lblPkey.Text, "1");

            newTemplate = dsGrid.Tables[0].Rows[0]["Messagetemplate"].ToString();
            break;
        }

        Clear_Error_Success_Box();
        Label46.Text = newTemplate.ToString();
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTestSMS();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTestSMS();", true);
        UpdatePanel5.Update();
    }
    
    private void FillStudent_SMSCHK_Cancel()
    {
        ControlVisibility("StudentSMS_CHK");
        string Pkey = lblPkey.Text;
        string count = "", Template = "", newTemplate = "", Message_cd = "", MsgMode = "";
        lblStudentSMS_CHK.Text = Pkey;

        lblDivision_StudentSMS.Text = lblCancelDivision.Text.ToString().Trim();
        lblAcademicYear_StudentSMS.Text = lblCancelAcademicYear.Text.ToString().Trim();
        lblCourse_StudentSMS.Text = ddlCourse.SelectedItem.ToString().Trim();
        lblEditLMSProduct_StudentSMS.Text = lblCancelLMSProduct.Text.ToString().Trim();
        lblEditCeter_StudentSMS.Text = lblCancelCenter.Text.ToString().Trim();
        lblSubject_StudentSMS.Text = lblCancelLectSubject.Text.Trim();
        lblChapter_StudentSMS.Text = lblCancelChapter.Text.Trim();

        /////////////////////////////
        DataSet DSChk = new DataSet();

        if (chkReplaceLect.Checked == true)
        {
            /// For Cancel Lectures
            DSChk = ProductController.Check_MesageTemplate("010", ddlDivision.SelectedValue.ToString().Trim(), 1);
        }
        else
        {
            /// for Alter Lectures
            DSChk = ProductController.Check_MesageTemplate("009", ddlDivision.SelectedValue.ToString().Trim(), 1);
        }

        /////////////////////////////


        if (DSChk != null)
        {
            if (DSChk.Tables[0].Rows.Count > 0)
            {
                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                if (count == "0")
                {
                    //Disable
                }
                else
                {
                    Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                    Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                    MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();
                }
            }
        }

        newTemplate = Template.Replace("%2526", "&").Replace("%252D", "+").Replace("%25", "%").Replace("%23", "#").Replace("%3D", "=").Replace("%5E", "^").Replace("%7E", "~");

        lblMessage_Template_SMS.Text = newTemplate.ToString().Trim();
        lblMessage_Code_Fin.Text = Message_cd.ToString().Trim();
        lblMessage_Mode_Fin.Text = MsgMode.ToString().Trim();

        DataSet dsCRPkey = ProductController.MessageTemplate_CreatePkey(lblStudentSMS_CHK.Text, 5);
        if (dsCRPkey != null)
        {
            if (dsCRPkey.Tables[0].Rows.Count > 0)
            {
                Pkey = dsCRPkey.Tables[0].Rows[0]["Pkey"].ToString();
            }
        }

        lblPkeyforUpdateLecture.Text = Pkey;
        chkFacSMS.Checked = false;
        DataSet dsStudent = ProductController.GetStudent_ForLectureCHKSMS(Pkey);
        if (dsStudent != null)
        {
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                dlStudent_CHK.DataSource = dsStudent;
                dlStudent_CHK.DataBind();

            }
            else
            {
                dlStudent_CHK.DataSource = null;
                dlStudent_CHK.DataBind();

                Show_Error_Success_Box("E", "Student not found for this batch(s)");
            }
        }
    }

    private void FillStudent_SMSCHK()
    {
        ControlVisibility("StudentSMS_CHK");
        string Pkey = lblPkey.Text;
        string count = "", Template = "", newTemplate = "", Message_cd = "", MsgMode = "";
        lblStudentSMS_CHK.Text = Pkey;

        lblDivision_StudentSMS.Text = lblDivision_Result2.Text.ToString().Trim();
        lblAcademicYear_StudentSMS.Text = lblAcademicYear_Result2.Text.ToString().Trim();
        lblCourse_StudentSMS.Text = lblCourse_Result2.Text.ToString().Trim();
        lblEditLMSProduct_StudentSMS.Text = lblLMSProduct_Result2.Text.ToString().Trim();
        lblEditCeter_StudentSMS.Text = lblCenter_Result2.Text.ToString().Trim();
        lblSubject_StudentSMS.Text = ddlSubject_Add.SelectedItem.ToString().Trim();
        lblChapter_StudentSMS.Text = ddlChapter_Add.SelectedItem.ToString().Trim();


        DataSet DSChk = ProductController.Check_MesageTemplate("001", ddlDivision.SelectedValue.ToString().Trim(), 1);
        if (DSChk != null)
        {
            if (DSChk.Tables[0].Rows.Count > 0)
            {
                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                if (count == "0")
                {
                    //Disable
                }
                else
                {
                    Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                    Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                    MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();
                }
            }
        }

        newTemplate = Template.Replace("%2526", "&").Replace("%252D", "+").Replace("%25", "%").Replace("%23", "#").Replace("%3D", "=").Replace("%5E", "^").Replace("%7E", "~");

        lblMessage_Template_SMS.Text = newTemplate.ToString().Trim();
        lblMessage_Code_Fin.Text = Message_cd.ToString().Trim();
        lblMessage_Mode_Fin.Text = MsgMode.ToString().Trim();

        DataSet dsCRPkey = ProductController.MessageTemplate_CreatePkey(lblStudentSMS_CHK.Text, 5);
        if (dsCRPkey != null)
        {
            if (dsCRPkey.Tables[0].Rows.Count > 0)
            {
                Pkey = dsCRPkey.Tables[0].Rows[0]["Pkey"].ToString();
            }
        }

        lblPkeyforUpdateLecture.Text = Pkey;
        DataSet dsStudent = ProductController.GetStudent_ForLectureCHKSMS(Pkey);
        if (dsStudent != null)
        {
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                dlStudent_CHK.DataSource = dsStudent;
                dlStudent_CHK.DataBind();

            }
            else
            {
                dlStudent_CHK.DataSource = null;
                dlStudent_CHK.DataBind();

                Show_Error_Success_Box("E", "Student not found for this batch(s)");
            }
        }
    }

    private void FillStudent_SMSCHK_edit()
    {
        ControlVisibility("StudentSMS_CHK");
        string Pkey = lblPkey.Text;
        string count = "", Template = "", newTemplate = "", Message_cd = "", MsgMode = "";
        lblStudentSMS_CHK.Text = Pkey;

        lblDivision_StudentSMS.Text = lblEditDivision_Result.Text.ToString().Trim();
        lblAcademicYear_StudentSMS.Text = lblEditAcademicYear_Result.Text.ToString().Trim();
        lblCourse_StudentSMS.Text = lblEditCourse_Result.Text.ToString().Trim();
        lblEditLMSProduct_StudentSMS.Text = lblEditLMSProduct_Result.Text.ToString().Trim();
        lblEditCeter_StudentSMS.Text = lblEditCeter_Result.Text.ToString().Trim();
        lblSubject_StudentSMS.Text = ddlEditSubject.SelectedItem.ToString().Trim();
        lblChapter_StudentSMS.Text = ddlEditChapter.SelectedItem.ToString().Trim();


        DataSet DSChk = ProductController.Check_MesageTemplate("001", ddlDivision.SelectedValue.ToString().Trim(), 1);
        if (DSChk != null)
        {
            if (DSChk.Tables[0].Rows.Count > 0)
            {
                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                if (count == "0")
                {
                    //Disable
                }
                else
                {
                    Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                    Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                    MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();
                }
            }
        }

        newTemplate = Template.Replace("%2526", "&").Replace("%252D", "+").Replace("%25", "%").Replace("%23", "#").Replace("%3D", "=").Replace("%5E", "^").Replace("%7E", "~");

        lblMessage_Template_SMS.Text = newTemplate.ToString().Trim();
        lblMessage_Code_Fin.Text = Message_cd.ToString().Trim();
        lblMessage_Mode_Fin.Text = MsgMode.ToString().Trim();

        DataSet dsCRPkey = ProductController.MessageTemplate_CreatePkey(lblStudentSMS_CHK.Text, 5);
        if (dsCRPkey != null)
        {
            if (dsCRPkey.Tables[0].Rows.Count > 0)
            {
                Pkey = dsCRPkey.Tables[0].Rows[0]["Pkey"].ToString();
            }
        }

        lblPkeyforUpdateLecture.Text = Pkey;
        DataSet dsStudent = ProductController.GetStudent_ForLectureCHKSMS(Pkey);
        if (dsStudent != null)
        {
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                dlStudent_CHK.DataSource = dsStudent;
                dlStudent_CHK.DataBind();

            }
            else
            {
                dlStudent_CHK.DataSource = null;
                dlStudent_CHK.DataBind();

                Show_Error_Success_Box("E", "Student not found for this batch(s)");
            }
        }
    }

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlStudent_CHK.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
        }

    }

    private void CHk_btnVisiblity()
    {
        string count = "", Notification = "";

        DataSet DSChk = ProductController.Check_MesageTemplate("001", ddlDivision.SelectedValue.ToString().Trim(), 1);
        if (DSChk != null)
        {
            if (DSChk.Tables[0].Rows.Count > 0)
            {
                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                if (count == "0")
                {
                    //Disable
                }
                else
                {
                    Notification = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();
                    if (Notification == "Manual")
                    {
                        //Show Button
                        BtnAttendanceMessage.Visible = true;
                        btnMesage_ManualSending_Edit.Visible = true;

                    }
                    else
                    {
                        //Hide Button
                        BtnAttendanceMessage.Visible = false;
                        btnMesage_ManualSending_Edit.Visible = false;
                    }
                }


            }
            else
            {
                //Hide Button
                BtnAttendanceMessage.Visible = false;
                btnMesage_ManualSending_Edit.Visible = false;
            }
        }
    }

    private void CHk_btnVisiblity_CancelLec()
    {
        string count = "", Notification = "";

        DataSet DSChk = ProductController.Check_MesageTemplate("010", ddlDivision.SelectedValue.ToString().Trim(), 1);
        if (DSChk != null)
        {
            if (DSChk.Tables[0].Rows.Count > 0)
            {
                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                if (count == "0")
                {
                    //Disable
                }
                else
                {
                    Notification = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();
                    if (Notification == "Manual")
                    {
                        //Show Button
                        btn_LecCancelSMSSent.Visible = true;

                    }
                    else
                    {
                        //Hide Button
                        btn_LecCancelSMSSent.Visible = false;
                    }
                }


            }
            else
            {
                //Hide Button
                btn_LecCancelSMSSent.Visible = false;
            }
        }
    }

    private void Message_Template()
    {
        try
        {
            string count = "", Notification = "", newTemplate = "", firstname = "", date = "", StudentfullName = "", Centre_code = "", MobileNo = "", Pkey = "", RollNo = "", SubjectName = "", ChapterName = "", lectFromTime = "", lectToTime = "";
            int resultid = 0;
            int smscount = 0;

            DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblPkey.Text, 4);
            if (DSSentStatus != null)
            {
                if (DSSentStatus.Tables[0].Rows.Count > 0)
                {
                    smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["LectureSMSFlag"].ToString());
                    if (smscount == 0)
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;

                        DataSet DSChk = ProductController.Check_MesageTemplate("001", ddlDivision.SelectedValue.ToString().Trim(), 1);
                        if (DSChk != null)
                        {
                            if (DSChk.Tables[0].Rows.Count > 0)
                            {
                                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                                if (count == "0")
                                {
                                    //Disable
                                }
                                else
                                {
                                    string Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                                    string Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                                    string MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();

                                    string lectureShedule_ID = lblPkey.Text;

                                    DataSet dsCRPkey = ProductController.MessageTemplate_CreatePkey(lectureShedule_ID, 5);
                                    if (dsCRPkey != null)
                                    {
                                        if (dsCRPkey.Tables[0].Rows.Count > 0)
                                        {
                                            Pkey = dsCRPkey.Tables[0].Rows[0]["Pkey"].ToString();
                                        }
                                    }

                                    DataSet dsStudent = ProductController.GetStudent_ForLectureSMS(Pkey);

                                    for (int i = 0; i <= dsStudent.Tables[0].Rows.Count - 1; i++)
                                    {
                                        StudentfullName = dsStudent.Tables[0].Rows[i]["StudentName"].ToString();
                                        firstname = dsStudent.Tables[0].Rows[i]["FirstName"].ToString();
                                        RollNo = dsStudent.Tables[0].Rows[i]["RollNo"].ToString();
                                        date = dsStudent.Tables[0].Rows[i]["date1"].ToString();
                                        SubjectName = ddlSubject_Add.SelectedItem.ToString().Trim();
                                        ChapterName = ddlChapter_Add.SelectedItem.ToString().Trim();
                                        lectFromTime = dsStudent.Tables[0].Rows[i]["NfromtimeStr"].ToString();
                                        lectToTime = dsStudent.Tables[0].Rows[i]["NtotimeStr"].ToString();

                                        Centre_code = dsStudent.Tables[0].Rows[i]["Centre_Code"].ToString();
                                        MobileNo = dsStudent.Tables[0].Rows[i]["mobileno"].ToString();

                                        newTemplate = Template.Replace("[StudentFullName]", StudentfullName).Replace("[FirstName]", firstname).Replace("[ParentName]", "").Replace("[RollNo]", RollNo).Replace("[SessionDate]", date).Replace("[SubjectName]", SubjectName).Replace("[ChapterName]", ChapterName).Replace("[LectureFromTime]", lectFromTime).Replace("[LectureToTime]", lectToTime).Replace("%2526", "%26");

                                        if (MsgMode == "Auto")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", "Auto", "Transactional");
                                        }
                                        else if (MsgMode == "Manual")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", CreatedBy, "Transactional");
                                        }

                                    }

                                    int rowcount = dsStudent.Tables[0].Rows.Count;
                                    if (rowcount > 0)
                                    {
                                        resultid = ProductController.Update_LectureSMSSendStatus_T506(Pkey, 1, MsgMode, 6);
                                    }
                                    Message_TemplateFor_Teachers();
                                }


                            }
                        }

                    }
                    else
                    {
                        //Message is already sent
                    }
                }
            }

            //////////




        }
        catch (Exception ex)
        {
        }

    }

    private void Message_Template_edit()
    {
        try
        {
            string count = "", Notification = "", newTemplate = "", firstname = "", date = "", StudentfullName = "", Centre_code = "", MobileNo = "", Pkey = "", RollNo = "", SubjectName = "", ChapterName = "", lectFromTime = "", lectToTime = "";
            int resultid = 0;
            int smscount = 0;

            DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblPkey.Text, 4);
            if (DSSentStatus != null)
            {
                if (DSSentStatus.Tables[0].Rows.Count > 0)
                {
                    smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["LectureSMSFlag"].ToString());
                    if (smscount == 0)
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;

                        DataSet DSChk = ProductController.Check_MesageTemplate("001", ddlDivision.SelectedValue.ToString().Trim(), 1);
                        if (DSChk != null)
                        {
                            if (DSChk.Tables[0].Rows.Count > 0)
                            {
                                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                                if (count == "0")
                                {
                                    //Disable
                                }
                                else
                                {
                                    string Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                                    string Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                                    string MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();

                                    string lectureShedule_ID = lblPkey.Text;

                                    DataSet dsCRPkey = ProductController.MessageTemplate_CreatePkey(lectureShedule_ID, 5);
                                    if (dsCRPkey != null)
                                    {
                                        if (dsCRPkey.Tables[0].Rows.Count > 0)
                                        {
                                            Pkey = dsCRPkey.Tables[0].Rows[0]["Pkey"].ToString();
                                        }
                                    }

                                    DataSet dsStudent = ProductController.GetStudent_ForLectureSMS(Pkey);

                                    for (int i = 0; i <= dsStudent.Tables[0].Rows.Count - 1; i++)
                                    {
                                        StudentfullName = dsStudent.Tables[0].Rows[i]["StudentName"].ToString();
                                        firstname = dsStudent.Tables[0].Rows[i]["FirstName"].ToString();
                                        RollNo = dsStudent.Tables[0].Rows[i]["RollNo"].ToString();
                                        date = dsStudent.Tables[0].Rows[i]["date1"].ToString();
                                        SubjectName = ddlEditSubject.SelectedItem.ToString().Trim();
                                        ChapterName = ddlEditChapter.SelectedItem.ToString().Trim();
                                        lectFromTime = dsStudent.Tables[0].Rows[i]["NfromtimeStr"].ToString();
                                        lectToTime = dsStudent.Tables[0].Rows[i]["NtotimeStr"].ToString();

                                        Centre_code = dsStudent.Tables[0].Rows[i]["Centre_Code"].ToString();
                                        MobileNo = dsStudent.Tables[0].Rows[i]["mobileno"].ToString();

                                        newTemplate = Template.Replace("[StudentFullName]", StudentfullName).Replace("[FirstName]", firstname).Replace("[ParentName]", "").Replace("[RollNo]", RollNo).Replace("[SessionDate]", date).Replace("[SubjectName]", SubjectName).Replace("[ChapterName]", ChapterName).Replace("[LectureFromTime]", lectFromTime).Replace("[LectureToTime]", lectToTime).Replace("%2526", "%26");

                                        if (MsgMode == "Auto")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", "Auto", "Transactional");
                                        }
                                        else if (MsgMode == "Manual")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", CreatedBy, "Transactional");
                                        }

                                    }

                                    int rowcount = dsStudent.Tables[0].Rows.Count;
                                    if (rowcount > 0)
                                    {
                                        resultid = ProductController.Update_LectureSMSSendStatus_T506(Pkey, 1, MsgMode, 6);
                                    }
                                    Message_TemplateFor_Teachers_edit();
                                }


                            }
                        }

                    }
                    else
                    {
                        //Message is already sent
                    }
                }
            }

            //////////




        }
        catch (Exception ex)
        {
        }

    }

    protected void btn_FinalSMSSent_Click(object sender, System.EventArgs e)
    {

        try
        {
            string template = lblMessage_Template_SMS.Text.ToString().Trim();
            string newTemplate = "";
            int resultid = 0, smscount = 0, count = 0;

            DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblPkey.Text, 4);
            if (DSSentStatus != null)
            {
                if (DSSentStatus.Tables[0].Rows.Count > 0)
                {
                    smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["LectureSMSFlag"].ToString());
                    if (smscount == 0)
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;


                        string MsgMode = lblMessage_Mode_Fin.Text.ToString().Trim();
                        string Message_cd = lblMessage_Code_Fin.Text.ToString().Trim();
                        string SBEntryCode = "", CenterCode = "";

                        foreach (DataListItem dtlItem in dlStudent_CHK.Items)
                        {
                            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");
                            if (chkCheck.Checked == true)
                            {
                                //Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                                //Label lblFirstName = (Label)dtlItem.FindControl("lblFirstName");
                                //Label lblRollNo = (Label)dtlItem.FindControl("lblRollNo");
                                //Label lblDate1 = (Label)dtlItem.FindControl("lblDate1");
                                //Label lblNFromTime = (Label)dtlItem.FindControl("lblNFromTime");
                                //Label lblNToTime = (Label)dtlItem.FindControl("lblNToTime");
                                Label lblcenter_code = (Label)dtlItem.FindControl("lblcenter_code");
                                //Label lblBatchName = (Label)dtlItem.FindControl("lblBatchName");
                                //Label lblMobileNo = (Label)dtlItem.FindControl("lblMobileNo");
                                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                                SBEntryCode = SBEntryCode + lblSBEntryCode.Text + ",";
                                CenterCode = lblcenter_code.Text;
                                //newTemplate = template.Replace("[StudentFullName]", lblStudentName.Text.Trim()).Replace("[FirstName]", lblFirstName.Text.Trim()).Replace("[ParentName]", "").Replace("[RollNo]", lblRollNo.Text.Trim()).Replace("[SessionDate]", lblDate1.Text.Trim()).Replace("[SubjectName]", lblSubject_StudentSMS.Text.Trim()).Replace("[ChapterName]", lblChapter_StudentSMS.Text.Trim()).Replace("[LectureFromTime]", lblNFromTime.Text.Trim()).Replace("[LectureToTime]", lblNToTime.Text.Trim()).Replace("&", "%26").Replace("[AlterSubjectName]", ddlReplaceSubject.SelectedItem.ToString().Trim()).Replace("[AlterChapterName]", ddlReplaceChapter.SelectedItem.ToString().Trim()); 

                                //if (MsgMode == "Auto")
                                //{
                                //    resultid = ProductController.Insert_SMSLog(lblcenter_code.Text.ToString().Trim(), Message_cd, lblMobileNo.Text.ToString().Trim(), newTemplate, "0", "Auto", "Transactional");
                                //}
                                //else if (MsgMode == "Manual")
                                //{
                                //    resultid = ProductController.Insert_SMSLog(lblcenter_code.Text.ToString().Trim(), Message_cd, lblMobileNo.Text.ToString().Trim(), newTemplate, "0", CreatedBy, "Transactional");
                                //}

                                count = count + 1;
                            }
                        }

                        if (count > 0)
                        {

                            DataSet dsGrid = ProductController.Get_Lecture_CancellationSMSTemplate(lblMessage_Code_Fin.Text, SBEntryCode, lblPkey.Text, "1");
                            int i = 0;
                            while (i < Convert.ToInt32(dsGrid.Tables[0].Rows.Count.ToString()))
                            {
                                if (MsgMode == "Auto")
                                {
                                    if (dsGrid.Tables[0].Rows[i]["MobileNo"].ToString() != "")
                                    {
                                        resultid = ProductController.Insert_SMSLog(CenterCode, Message_cd, dsGrid.Tables[0].Rows[i]["MobileNo"].ToString(), dsGrid.Tables[0].Rows[i]["Messagetemplate"].ToString(), "0", "Auto", "Transactional");
                                    }
                                }
                                else if (MsgMode == "Manual")
                                {
                                    if (dsGrid.Tables[0].Rows[i]["MobileNo"].ToString() != "")
                                    {
                                        resultid = ProductController.Insert_SMSLog(CenterCode, Message_cd, dsGrid.Tables[0].Rows[i]["MobileNo"].ToString(), dsGrid.Tables[0].Rows[i]["Messagetemplate"].ToString(), "0", CreatedBy, "Transactional");
                                    }
                                }
                                i++;
                            }

                            // resultid = ProductController.Update_LectureSMSSendStatus_T506(lblPkeyforUpdateLecture.Text, 1, MsgMode, 6);
                            BtnSearch_Click(sender, e);
                        }
                        //else
                        //{
                        //    Show_Error_Success_Box("E", "Select atleast one student");
                        //}
                        if (chkFacSMS.Checked == true)
                        {

                            DataSet dsGrid1 = ProductController.Get_Lecture_CancellationSMSTemplate(lblMessage_Code_Fin.Text, SBEntryCode, lblPkey.Text, "2");
                            int j = 0;
                            while (j < Convert.ToInt32(dsGrid1.Tables[0].Rows.Count.ToString()))
                            {
                                if (MsgMode == "Auto")
                                {
                                    if (dsGrid1.Tables[0].Rows[j]["MobileNo"].ToString() != "")
                                    {
                                        resultid = ProductController.Insert_SMSLog(CenterCode, Message_cd, dsGrid1.Tables[0].Rows[j]["MobileNo"].ToString(), dsGrid1.Tables[0].Rows[j]["Messagetemplate"].ToString(), "0", "Auto", "Transactional");
                                    }
                                }
                                else if (MsgMode == "Manual")
                                {
                                    if (dsGrid1.Tables[0].Rows[j]["MobileNo"].ToString() != "")
                                    {
                                        resultid = ProductController.Insert_SMSLog(CenterCode, Message_cd, dsGrid1.Tables[0].Rows[j]["MobileNo"].ToString(), dsGrid1.Tables[0].Rows[j]["Messagetemplate"].ToString(), "0", CreatedBy, "Transactional");
                                    }
                                }
                                j++;
                            }
                        }
                    }
                }
            }



        }
        catch (Exception ex)
        {

        }
        finally
        {

        }

    }

    private void Message_TemplateFor_Teachers()
    {
        try
        {
            string count = "", Notification = "", newTemplate = "", firstname = "", date = "", StudentName = "", Centre_code = "", MobileNo = "", Pkey = "", SubjectName = "", ChapterName = "", LecFromTime = "", LecToTime = "";
            int resultid = 0;
            int smscount = 0;


            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            DataSet DSChk = ProductController.Check_MesageTemplate("006", ddlDivision.SelectedValue.ToString().Trim(), 1);
            if (DSChk != null)
            {
                if (DSChk.Tables[0].Rows.Count > 0)
                {
                    count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                    if (count == "0")
                    {
                        //Disable
                    }
                    else
                    {
                        string Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                        string Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                        string MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();

                        string lectureShedule_ID = lblPkey.Text;

                        DataSet dsStudent = ProductController.GetPartner_ForLectureSMS(lectureShedule_ID, 7);

                        for (int i = 0; i <= dsStudent.Tables[0].Rows.Count - 1; i++)
                        {
                            firstname = dsStudent.Tables[0].Rows[i]["FirstName"].ToString();
                            date = dsStudent.Tables[0].Rows[i]["date1"].ToString();
                            SubjectName = ddlSubject_Add.SelectedItem.ToString().Trim();
                            ChapterName = ddlChapter_Add.SelectedItem.ToString().Trim();
                            LecFromTime = dsStudent.Tables[0].Rows[i]["NfromtimeStr"].ToString();
                            LecToTime = dsStudent.Tables[0].Rows[i]["NtotimeStr"].ToString();

                            Centre_code = dsStudent.Tables[0].Rows[i]["Centre_Code"].ToString();
                            MobileNo = dsStudent.Tables[0].Rows[i]["mobileno"].ToString();

                            newTemplate = Template.Replace("[TeacherCode]", firstname).Replace("[SessionDate]", date).Replace("[SubjectName]", SubjectName).Replace("[ChapterName]", ChapterName).Replace("[LectureFromTime]", LecFromTime).Replace("[LectureToTime]", LecToTime).Replace("%2526", "%26");

                            if (MsgMode == "Auto")
                            {
                                resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", "Auto", "Transactional");
                            }
                            else if (MsgMode == "Manual")
                            {
                                resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", CreatedBy, "Transactional");
                            }

                        }

                    }


                }
            }






            //////////




        }
        catch (Exception ex)
        {
        }

    }

    private void Message_TemplateFor_Teachers_edit()
    {
        try
        {
            string count = "", Notification = "", newTemplate = "", firstname = "", date = "", StudentName = "", Centre_code = "", MobileNo = "", Pkey = "", SubjectName = "", ChapterName = "", LecFromTime = "", LecToTime = "";
            int resultid = 0;
            int smscount = 0;


            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            DataSet DSChk = ProductController.Check_MesageTemplate("006", ddlDivision.SelectedValue.ToString().Trim(), 1);
            if (DSChk != null)
            {
                if (DSChk.Tables[0].Rows.Count > 0)
                {
                    count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                    if (count == "0")
                    {
                        //Disable
                    }
                    else
                    {
                        string Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                        string Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                        string MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();

                        string lectureShedule_ID = lblPkey.Text;

                        DataSet dsStudent = ProductController.GetPartner_ForLectureSMS(lectureShedule_ID, 7);

                        for (int i = 0; i <= dsStudent.Tables[0].Rows.Count - 1; i++)
                        {
                            firstname = dsStudent.Tables[0].Rows[i]["FirstName"].ToString();
                            date = dsStudent.Tables[0].Rows[i]["date1"].ToString();
                            SubjectName = ddlEditSubject.SelectedItem.ToString().Trim();
                            ChapterName = ddlEditChapter.SelectedItem.ToString().Trim();
                            LecFromTime = dsStudent.Tables[0].Rows[i]["NfromtimeStr"].ToString();
                            LecToTime = dsStudent.Tables[0].Rows[i]["NtotimeStr"].ToString();

                            Centre_code = dsStudent.Tables[0].Rows[i]["Centre_Code"].ToString();
                            MobileNo = dsStudent.Tables[0].Rows[i]["mobileno"].ToString();

                            newTemplate = Template.Replace("[TeacherCode]", firstname).Replace("[SessionDate]", date).Replace("[SubjectName]", SubjectName).Replace("[ChapterName]", ChapterName).Replace("[LectureFromTime]", LecFromTime).Replace("[LectureToTime]", LecToTime).Replace("%2526", "%26");

                            if (MsgMode == "Auto")
                            {
                                resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", "Auto", "Transactional");
                            }
                            else if (MsgMode == "Manual")
                            {
                                resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", CreatedBy, "Transactional");
                            }

                        }

                    }


                }
            }






            //////////




        }
        catch (Exception ex)
        {
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        BtnSearch_Click(sender, e);
    }

    protected void ddlLectStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLectStatus.SelectedValue == "1")
        {
            trLectType.Visible = true;
        }
        else
            trLectType.Visible = false;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {

    }

    ///new method by Vinod
    private void FillDDL_Lect_Cancel_Reason()
    {
        try
        {
            ddlLect_Cancel_Reason.Items.Clear();
            DataSet dscancelReason = ProductController.GetAll_LectureCancelReason();
            BindDDL(ddlLect_Cancel_Reason, dscancelReason, "Lect_Cancel_Reason_Name", "Lect_Cancel_Reason_ID");
            ddlLect_Cancel_Reason.Items.Insert(0, "Select");
            ddlLect_Cancel_Reason.SelectedIndex = 0;
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

    protected void ddlLect_Cancel_Reason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLect_Cancel_Reason.SelectedValue == "11")
        {
            Resonarea.Visible = true;
        }
        else
        {
            Resonarea.Visible = false;
        }
    }

    //class lecturedetailsinsert
    //{
    //    public string LectureGroupCode { get; set; }
    //    public string CourseCode { get; set; }
    //    public string SubjectCode { get; set; }
    //    public string ChapterCode { get; set; }
    //    public string TopicCode { get; set; }
    //    public string SubTopicCode { get; set; }
    //    public string LessonPlanCode { get; set; }
    //    public string ProductCode { get; set; }
    //    public string CenterCode { get; set; }
    //    public string BatchProductCode { get; set; }
    //    public string TeacherCode { get; set; }
    //    public DateTime StartDateTime { get; set; }
    //    public DateTime EndDateTime { get; set; }

    //    public Boolean IsRescheduled { get; set; }
    //    public Boolean IsCancelled { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public Boolean IsActive { get; set; }

    //    public Boolean IsDeleted { get; set; }


    //}
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

    
}
